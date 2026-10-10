using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RestoApp.Models;
using RestoApp.Helper;

namespace RestoApp.AdminRepo
{
    public class MenuRepositoryAdmin
    {
        public List<Menu> GetAllMenus()
        {
            var menus = new List<Menu>();
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    
                    // OPTIMIZATION SUGGESTION: Fetch strictly the required columns rather than 'SELECT *'. Avoids grabbing unnecessary data and protects against schema alterations causing column ordering issues.
                    /*
                    string sql = "SELECT MenuId, MenuName, Description, DisplayOrder, IsActive, CreatedAt FROM Menus ORDER BY DisplayOrder, MenuName";
                    */
                    string sql = "SELECT * FROM Menus ORDER BY DisplayOrder, MenuName";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                menus.Add(new Menu
                                {
                                    MenuId = Convert.ToInt32(reader["MenuId"]),
                                    MenuName = reader["MenuName"].ToString(),
                                    Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                                    DisplayOrder = Convert.ToInt32(reader["DisplayOrder"]),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetAllMenus: {ex.Message}");
            }
            return menus;
        }

        public int InsertMenu(Menu model)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO Menus (MenuName, Description, DisplayOrder, IsActive)
                        VALUES (@MenuName, @Description, @DisplayOrder, @IsActive);
                        SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MenuName", model.MenuName);
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(model.Description) ? DBNull.Value : (object)model.Description);
                        cmd.Parameters.AddWithValue("@DisplayOrder", model.DisplayOrder);
                        cmd.Parameters.AddWithValue("@IsActive", model.IsActive ? 1 : 0);

                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in InsertMenu: {ex.Message}");
                return 0;
            }
        }

        public bool UpdateMenu(Menu model)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        UPDATE Menus SET 
                            MenuName = @MenuName, Description = @Description, 
                            DisplayOrder = @DisplayOrder, IsActive = @IsActive
                        WHERE MenuId = @MenuId";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MenuId", model.MenuId);
                        cmd.Parameters.AddWithValue("@MenuName", model.MenuName);
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(model.Description) ? DBNull.Value : (object)model.Description);
                        cmd.Parameters.AddWithValue("@DisplayOrder", model.DisplayOrder);
                        cmd.Parameters.AddWithValue("@IsActive", model.IsActive ? 1 : 0);

                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateMenu: {ex.Message}");
                return false;
            }
        }

        public bool DeleteMenu(int menuId)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM Menus WHERE MenuId = @MenuId";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MenuId", menuId);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in DeleteMenu: {ex.Message}");
                return false;
            }
        }
    }
}
