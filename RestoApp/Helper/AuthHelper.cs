using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace RestoApp.Helper
{
    public static class AuthHelper
    {
        private const string AuthCookieName = "AuthCookie";
        private const int RememberMeDays = 7;

        public static void SetAuthSession(Page page, int userId, string role)
        {
            page.Session["UserId"] = userId;
            page.Session["Role"] = role;
        }

        // Purpose: Check if someone is logged in and load their info.
        public static bool TryGetCurrentUser(Page page, out string fullName, out string role)
        {
            fullName = null;
            role = null;

            if (page.Session["UserId"] == null)
            {
                TrySignInFromRememberMeCookie(page);
            }

            if (page.Session["UserId"] == null)
            {
                return false;
            }

            int userId;
            if (!int.TryParse(page.Session["UserId"].ToString(), out userId))
            {
                SignOut(page);
                return false;
            }

            if (!LoadActiveVerifiedUser(userId, out fullName, out role))
            {
                SignOut(page);
                return false;
            }

            page.Session["Role"] = role;
            return true;
        }

        public static bool TrySignInFromRememberMeCookie(Page page)
        {
            HttpCookie cookie = page.Request.Cookies[AuthCookieName];
            if (cookie == null || string.IsNullOrWhiteSpace(cookie.Value))
            {
                return false;
            }

            int userId;
            string role;

            if (!LoadUserFromRememberMeToken(cookie.Value, out userId, out role))
            {
                ClearRememberMeCookie(page);
                return false;
            }

            SetAuthSession(page, userId, role);
            return true;
        }

        public static bool RequireAdmin(Page page)
        {
            string fullName;
            string role;

            if (!TryGetCurrentUser(page, out fullName, out role))
            {
                Redirect(page, "~/Authentication/Login.aspx");
                return false;
            }

            if (!string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                Redirect(page, "~/Default.aspx");
                return false;
            }

            return true;
        }

        public static void CreateRememberMeCookie(Page page, int userId)
        {
            // Remember-me is just one random code saved in the database and browser.
            string token = Guid.NewGuid().ToString("N");
            DateTime expiry = DateTime.UtcNow.AddDays(RememberMeDays);

            using (SqlConnection con = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
                UPDATE Users
                SET AuthToken = @AuthToken,
                    AuthTokenExpiry = @AuthTokenExpiry
                WHERE UserId = @UserId", con))
            {
                cmd.Parameters.Add("@AuthToken", SqlDbType.NVarChar, 256).Value = token;
                cmd.Parameters.Add("@AuthTokenExpiry", SqlDbType.DateTime).Value = expiry;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                con.Open();
                cmd.ExecuteNonQuery();
            }

            HttpCookie cookie = new HttpCookie(AuthCookieName, token)
            {
                Expires = DateTime.Now.AddDays(RememberMeDays),
                HttpOnly = true,
                Path = "/"
            };

            page.Response.Cookies.Add(cookie);
        }

        public static void SignOut(Page page)
        {
            ClearStoredRememberMeToken(page);
            ClearRememberMeCookie(page);
            page.Session.Clear();
            page.Session.Abandon();
        }

        public static string CreateVerificationToken()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static string BuildVerificationUrl(HttpRequest request, string token)
        {
            string path = VirtualPathUtility.ToAbsolute("~/Authentication/Verify.aspx");
            return request.Url.GetLeftPart(UriPartial.Authority)
                + path
                + "?token="
                + HttpUtility.UrlEncode(token);
        }

        public static string BuildResetPasswordUrl(HttpRequest request, string token)
        {
            string path = VirtualPathUtility.ToAbsolute("~/Authentication/ResetPassword.aspx");
            return request.Url.GetLeftPart(UriPartial.Authority)
                + path
                + "?token="
                + HttpUtility.UrlEncode(token);
        }

        public static void SendVerificationEmail(HttpRequest request, string email, string token)
        {
            string verifyUrl = BuildVerificationUrl(request, token);

            EmailService.SendEmail(
                email,
                "Verify Account",
                "Click here to verify your account:\n\n" + verifyUrl);
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                return !string.IsNullOrWhiteSpace(email)
                    && new System.Net.Mail.MailAddress(email).Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 8;
        }

        private static bool LoadActiveVerifiedUser(int userId, out string fullName, out string role)
        {
            fullName = null;
            role = null;

            using (SqlConnection con = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT FullName, Role
                FROM Users
                WHERE UserId = @UserId
                  AND IsActive = 1
                  AND IsEmailVerified = 1", con))
            {
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return false;
                    }

                    fullName = reader["FullName"].ToString();
                    role = reader["Role"].ToString();
                    return true;
                }
            }
        }

        private static bool LoadUserFromRememberMeToken(string token, out int userId, out string role)
        {
            userId = 0;
            role = null;

            using (SqlConnection con = DbHelper.GetConnection())
            //GETUTCDATE() → a SQL Server function that returns the current date and time in UTC.
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT UserId, Role
                FROM Users
                WHERE AuthToken = @AuthToken
                  AND AuthTokenExpiry > GETUTCDATE()
                  AND IsActive = 1
                  AND IsEmailVerified = 1", con))
            {
                cmd.Parameters.Add("@AuthToken", SqlDbType.NVarChar, 256).Value = token;

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return false;
                    }

                    userId = Convert.ToInt32(reader["UserId"]);
                    role = reader["Role"].ToString();
                    return true;
                }
            }
        }

        private static void ClearStoredRememberMeToken(Page page)
        {
            object userId = page.Session["UserId"];
            HttpCookie cookie = page.Request.Cookies[AuthCookieName];

            using (SqlConnection con = DbHelper.GetConnection())
            using (SqlCommand cmd = con.CreateCommand())
            {
                int parsedUserId;
                if (userId != null && int.TryParse(userId.ToString(), out parsedUserId))
                {
                    cmd.CommandText = "UPDATE Users SET AuthToken = NULL, AuthTokenExpiry = NULL WHERE UserId = @UserId";
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = parsedUserId;
                }
                else if (cookie != null && !string.IsNullOrWhiteSpace(cookie.Value))
                {
                    cmd.CommandText = "UPDATE Users SET AuthToken = NULL, AuthTokenExpiry = NULL WHERE AuthToken = @AuthToken";
                    cmd.Parameters.Add("@AuthToken", SqlDbType.NVarChar, 256).Value = cookie.Value;
                }
                else
                {
                    return;
                }

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static void ClearRememberMeCookie(Page page)
        {
            HttpCookie cookie = new HttpCookie(AuthCookieName, string.Empty)
            {
                Expires = DateTime.Now.AddDays(-1),
                HttpOnly = true,
                Path = "/"
            };

            page.Response.Cookies.Add(cookie);
        }

        private static void Redirect(Page page, string url)
        {
            page.Response.Redirect(url, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //It means “stop the request immediately after redirect and send the response back to the browser.”
        }
    }
}

/*
 * 
 * Why do we pass Page page?
Because inside a page, you have access to:
page.Session → the session memory for that user.
page.Request → the incoming request (cookies, query string, headers).
page.Response → the outgoing response (redirects, cookies).
page.Server → server utilities.
Instead of writing code directly inside every page, we pass the page object into our helper so the 
helper can use its Session, Request, Response.
 * 
 */