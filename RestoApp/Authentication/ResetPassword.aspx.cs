using System;
using System.Data;
using System.Data.SqlClient;
using RestoApp.Helper;

namespace RestoApp.Pages
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            string token = Request.QueryString["token"];
            if (string.IsNullOrWhiteSpace(token) || !IsResetTokenValid(token))
            {
                ShowMessage("Invalid or expired reset token.", "danger");
                btnReset.Enabled = false;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(token))
            {
                ShowMessage("Invalid or missing reset token.", "danger");
                return;
            }

            if (!AuthHelper.IsValidPassword(newPassword))
            {
                ShowMessage("Password must be at least 8 characters.", "danger");
                return;
            }

            if (newPassword != confirmPassword)
            {
                ShowMessage("New password and confirm password do not match.", "danger");
                return;
            }

            string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
                UPDATE Users
                SET PasswordHash = @PasswordHash,
                    ResetToken = NULL,
                    ResetTokenExpiry = NULL,
                    AuthToken = NULL,
                    AuthTokenExpiry = NULL
                WHERE ResetToken = @Token
                  AND ResetTokenExpiry > GETUTCDATE()
                  AND IsActive = 1", conn))
            {
                cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256).Value = newPasswordHash;
                cmd.Parameters.Add("@Token", SqlDbType.NVarChar, 256).Value = token;

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Response.Redirect("Login.aspx?reset=1", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
            }

            ShowMessage("Invalid or expired reset token.", "danger");
        }

        private bool IsResetTokenValid(string token)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT COUNT(1)
                FROM Users
                WHERE ResetToken = @Token
                  AND ResetTokenExpiry > GETUTCDATE()
                  AND IsActive = 1", conn))
            {
                cmd.Parameters.Add("@Token", SqlDbType.NVarChar, 256).Value = token;

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "alert alert-" + type + " d-block py-2 mb-0 small fw-semibold";
        }
    }
}
