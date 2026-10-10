using System;
using System.Data;
using System.Data.SqlClient;
using RestoApp.Helper;

namespace RestoApp.Pages
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim().ToLower();

            if (!AuthHelper.IsValidEmail(email))
            {
                ShowMessage("Please enter a valid email address.", "danger");
                return;
            }

            string resetToken = AuthHelper.CreateVerificationToken();
            DateTime expiry = DateTime.UtcNow.AddHours(1);
            bool shouldSendEmail;

            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
                UPDATE Users
                SET ResetToken = @Token,
                    ResetTokenExpiry = @Expiry
                WHERE Email = @Email
                  AND IsActive = 1
                  AND IsEmailVerified = 1", conn))
            {
                cmd.Parameters.Add("@Token", SqlDbType.NVarChar, 256).Value = resetToken;
                cmd.Parameters.Add("@Expiry", SqlDbType.DateTime).Value = expiry;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = email;

                conn.Open();
                shouldSendEmail = cmd.ExecuteNonQuery() > 0;
            }

            if (shouldSendEmail)
            {
                try
                {
                    string resetUrl = AuthHelper.BuildResetPasswordUrl(Request, resetToken);
                    string body = "Click the following link to reset your password:\n\n" + resetUrl;

                    EmailService.SendEmail(email, "Password Reset", body);
                }
                catch
                {
                    ShowMessage("We could not send the reset email right now. Please try again later.", "danger");
                    return;
                }
            }

            ShowMessage("If this email exists and is verified, a password reset link has been sent.", "success");
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "alert alert-" + type + " d-block py-2 mb-0 small fw-semibold";
        }
    }
}
