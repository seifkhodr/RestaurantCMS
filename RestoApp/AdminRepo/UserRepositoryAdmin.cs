using System;
using System.Data;
using System.Data.SqlClient;

using RestoApp.Helper;

namespace RestoApp.AdminRepo
{
    public class UserRepositoryAdmin
    {
        public DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT UserId, FullName, Email, Phone, Role, IsEmailVerified, IsActive, CreatedAt FROM Users ORDER BY CreatedAt DESC";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    { da.Fill(dt); }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"Error in GetAllUsers: {ex.Message}"); }
            return dt;
        }

        public bool UpdateUserRole(int userId, string role)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Users SET Role=@Role WHERE UserId=@UserId", conn))
                    {
                        cmd.Parameters.AddWithValue("@Role", role);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"Error in UpdateUserRole: {ex.Message}"); return false; }
        }

        public bool ToggleUserActive(int userId, bool isActive)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Users SET IsActive=@IsActive WHERE UserId=@UserId", conn))
                    {
                        cmd.Parameters.AddWithValue("@IsActive", isActive ? 1 : 0);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"Error in ToggleUserActive: {ex.Message}"); return false; }
        }

        public int GetCount(string tableName)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM [{tableName}]", conn))
                    { return Convert.ToInt32(cmd.ExecuteScalar()); }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"Error in GetCount: {ex.Message}"); return 0; }
        }
    }
}
