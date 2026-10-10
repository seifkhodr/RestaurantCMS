using System;
using System.Data;
using System.Data.SqlClient;
using RestoApp.Helper;

namespace RestoApp.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            if (Request.QueryString["verified"] == "1")
            {
                ShowMessage("Email verified successfully. You can login now.", "success");
            }
            else if (Request.QueryString["reset"] == "1")
            {
                ShowMessage("Password changed successfully. You can login now.", "success");
            }

            if (Session["UserId"] != null && Session["Role"] != null)
            {
                RedirectByRole(Session["Role"].ToString());
                return;
            }

            if (AuthHelper.TrySignInFromRememberMeCookie(this))
            {
                RedirectByRole(Session["Role"].ToString());
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim().ToLower();
            string password = txtPassword.Text;

            if (!AuthHelper.IsValidEmail(email) || string.IsNullOrWhiteSpace(password))
            {
                ShowMessage("Please enter a valid email and password.", "danger");
                return;
            }

            using (SqlConnection con = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT UserId,
                       Email,
                       PasswordHash,
                       Role,
                       IsEmailVerified,
                       IsActive,
                       VerificationToken,
                       VerificationExpiry
                FROM Users
                WHERE Email = @Email", con))
            {
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = email;

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        ShowMessage("Invalid email or password.", "danger");
                        return;
                    }

                    int userId = Convert.ToInt32(reader["UserId"]);
                    string passwordHash = reader["PasswordHash"].ToString();
                    string role = reader["Role"].ToString();
                    bool isEmailVerified = Convert.ToBoolean(reader["IsEmailVerified"]);
                    bool isActive = Convert.ToBoolean(reader["IsActive"]);
                    string verificationToken = reader["VerificationToken"] == DBNull.Value
                        ? null
                        : reader["VerificationToken"].ToString();
                    DateTime? verificationExpiry = reader["VerificationExpiry"] == DBNull.Value
                        ? (DateTime?)null
                        : Convert.ToDateTime(reader["VerificationExpiry"]);

                    bool validPassword;
                    try
                    {
                        validPassword = BCrypt.Net.BCrypt.Verify(password, passwordHash);
                    }
                    catch
                    {
                        validPassword = false;
                    }

                    if (!validPassword)
                    {
                        ShowMessage("Invalid email or password.", "danger");
                        return;
                    }

                    if (!isActive)
                    {
                        ShowMessage("Your account is inactive. Please contact support.", "warning");
                        return;
                    }

                    if (!isEmailVerified)
                    {
                        HandleUnverifiedLogin(userId, email, verificationToken, verificationExpiry);
                        return;
                    }

                    AuthHelper.SetAuthSession(this, userId, role);

                    if (chkRememberMe.Checked)
                    {
                        AuthHelper.CreateRememberMeCookie(this, userId);
                    }

                    RedirectByRole(role);
                }
            }
        }

        private void HandleUnverifiedLogin(int userId, string email, string verificationToken, DateTime? verificationExpiry)
        {
            string tokenToSend = verificationToken;
            bool needsNewToken = string.IsNullOrWhiteSpace(tokenToSend)
                || !verificationExpiry.HasValue
                || verificationExpiry.Value <= DateTime.UtcNow;

            if (needsNewToken)
            {
                tokenToSend = AuthHelper.CreateVerificationToken();
                DateTime expiry = DateTime.UtcNow.AddHours(24);

                using (SqlConnection con = DbHelper.GetConnection())
                using (SqlCommand cmd = new SqlCommand(@"
                    UPDATE Users
                    SET VerificationToken = @VerificationToken,
                        VerificationExpiry = @VerificationExpiry
                    WHERE UserId = @UserId", con))
                {
                    cmd.Parameters.Add("@VerificationToken", SqlDbType.NVarChar, 256).Value = tokenToSend;
                    cmd.Parameters.Add("@VerificationExpiry", SqlDbType.DateTime).Value = expiry;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            try
            {
                AuthHelper.SendVerificationEmail(Request, email, tokenToSend);
                ShowMessage("Your email is not verified. A verification link has been sent to your email.", "warning");
            }
            catch
            {
                ShowMessage("Your email is not verified, and we could not send a verification link right now.", "danger");
            }
        }

        private void RedirectByRole(string role)
        {
            if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("~/AdminView/Dashboard/AdminDashboard.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (string.Equals(role, "Client", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("~/Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            ShowMessage("Role not recognized. Contact support.", "danger");
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "alert alert-" + type + " d-block py-2 mb-0 small fw-semibold";
        }

        protected void chkRememberMe_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
