using RestoApp.Helper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;

namespace RestoApp.Pages
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void btnSignup_Click(object sender, EventArgs e)
        {
            string fullName = txtName.Text.Trim();
            string email = txtEmail.Text.Trim().ToLower();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(fullName))
            {
                ShowMessage("Full name is required.", "danger");
                return;
            }

            if (!AuthHelper.IsValidEmail(email))
            {
                ShowMessage("Please enter a valid email address.", "danger");
                return;
            }

            if (!AuthHelper.IsValidPassword(password))
            {
                ShowMessage("Password must be at least 8 characters.", "danger");
                return;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

           // A token in software / security is basically a unique piece of data(usually a random string)
           // that represents identity, permission, or a temporary right to do something.
           // Think of it like a digital keycard: instead of carrying your password everywhere,
           // you carry a token that proves you’re allowed in.

            string token = AuthHelper.CreateVerificationToken();

            DateTime expiry = DateTime.UtcNow.AddHours(1);

            try
            {
                using (SqlConnection con = DbHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Users
                    (
                        FullName,
                        Email,
                        PasswordHash,
                        VerificationToken,
                        VerificationExpiry,
                        Role
                    )
                    VALUES
                    (
                        @FullName,
                        @Email,
                        @PasswordHash,
                        @VerificationToken,
                        @VerificationExpiry,
                        @Role
                    )", con))
                {
                    cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value = fullName;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = email;
                    cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256).Value = passwordHash;
                    cmd.Parameters.Add("@VerificationToken", SqlDbType.NVarChar, 256).Value = token;
                    cmd.Parameters.Add("@VerificationExpiry", SqlDbType.DateTime).Value = expiry;
                    cmd.Parameters.Add("@Role", SqlDbType.NVarChar, 20).Value = "Client";

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                ShowMessage("An account with this email already exists.", "warning");
                return;
            }

            try
            {
                AuthHelper.SendVerificationEmail(Request, email, token);
                ShowMessage("Signup successful. Check your email to verify your account.", "success");
            }
            catch
            {
                ShowMessage("Signup successful, but the verification email could not be sent right now.", "warning");
            }
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "alert alert-" + type + " d-block py-2 mb-0 small fw-semibold";
        }
    }
}
