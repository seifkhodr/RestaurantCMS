using RestoApp.Helper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RestoApp.Pages
{
    public partial class Verify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            string token = Request.QueryString["token"];

            if (string.IsNullOrWhiteSpace(token))
            {
                ShowMessage("Invalid verification link.", "danger");
                return;
            }

            using (SqlConnection con = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT UserId, Email, VerificationExpiry
                FROM Users
                WHERE VerificationToken = @VerificationToken
                  AND IsEmailVerified = 0
                  AND IsActive = 1", con))
            {
                cmd.Parameters.Add("@VerificationToken", SqlDbType.NVarChar, 256).Value = token;

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        ShowMessage("Invalid or already used verification link.", "danger");
                        return;
                    }

                    int userId = Convert.ToInt32(reader["UserId"]);
                    string email = reader["Email"].ToString();
                    DateTime? expiry = reader["VerificationExpiry"] == DBNull.Value
                        ? (DateTime?)null
                        : Convert.ToDateTime(reader["VerificationExpiry"]);

                    reader.Close();

                    if (!expiry.HasValue || expiry.Value <= DateTime.UtcNow)
                    {
                        ResendVerification(con, userId, email);
                        return;
                    }

                    MarkEmailVerified(con, token);
                }
            }

            ShowMessage("Email verified successfully. You will be redirected to login in 5 seconds.", "success");
            RedirectToLoginAfterDelay();
        }

        private void ResendVerification(SqlConnection con, int userId, string email)
        {
            string newToken = AuthHelper.CreateVerificationToken();
            DateTime newExpiry = DateTime.UtcNow.AddHours(24);

            using (SqlCommand updateCmd = new SqlCommand(@"
                UPDATE Users
                SET VerificationToken = @VerificationToken,
                    VerificationExpiry = @VerificationExpiry
                WHERE UserId = @UserId", con))
            {
                updateCmd.Parameters.Add("@VerificationToken", SqlDbType.NVarChar, 256).Value = newToken;
                updateCmd.Parameters.Add("@VerificationExpiry", SqlDbType.DateTime).Value = newExpiry;
                updateCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                updateCmd.ExecuteNonQuery();
            }

            try
            {
                AuthHelper.SendVerificationEmail(Request, email, newToken);
                ShowMessage("This verification link expired. A new link has been sent to your email.", "warning");
            }
            catch
            {
                ShowMessage("This verification link expired, and we could not send a new link right now.", "danger");
            }
        }

        private void MarkEmailVerified(SqlConnection con, string token)
        {
            using (SqlCommand updateCmd = new SqlCommand(@"
                UPDATE Users
                SET IsEmailVerified = 1,
                    VerificationToken = NULL,
                    VerificationExpiry = NULL
                WHERE VerificationToken = @VerificationToken", con))
            {
                updateCmd.Parameters.Add("@VerificationToken", SqlDbType.NVarChar, 256).Value = token;
                updateCmd.ExecuteNonQuery();
            }
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "alert alert-" + type + " d-block py-2 mb-3 small fw-semibold";
        }

        private void RedirectToLoginAfterDelay()
        {
            string loginUrl = ResolveUrl("~/Authentication/Login.aspx?verified=1");
            string script = "setTimeout(function(){ window.location.href = '" + loginUrl + "'; }, 2000);";

            ClientScript.RegisterStartupScript(GetType(), "redirectToLogin", script, true);
        }
    }
}
