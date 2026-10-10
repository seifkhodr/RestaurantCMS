using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RestoApp.Models;
using RestoApp.Helper;

namespace RestoApp.AdminRepo
{
    public class ItemRepositoryAdmin
    {
        public List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    
                    // OPTIMIZATION SUGGESTION: Instead of SELECT i.*, explicitly state columns to reduce memory overhead from the DB to the app.
                    /* 
                    string sql = @"
                        SELECT i.ItemId, i.MenuId, i.ItemName, i.Description, i.Price, i.Currency, 
                               i.Ingredients, i.Origin, i.IsAvailable, i.CreatedAt, 
                               m.MenuName, p.PhotoPath 
                        FROM Items i
                        INNER JOIN Menus m ON i.MenuId = m.MenuId
                        LEFT JOIN ItemPhotos p ON i.ItemId = p.ItemId AND p.IsPrimary = 1
                        ORDER BY i.ItemId DESC";
                    */

                    string sql = @"
                        SELECT i.*, m.MenuName, p.PhotoPath 
                        FROM Items i
                        INNER JOIN Menus m ON i.MenuId = m.MenuId
                        LEFT JOIN ItemPhotos p ON i.ItemId = p.ItemId AND p.IsPrimary = 1
                        ORDER BY i.ItemId DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(new Item
                                {
                                    ItemId = (int)reader["ItemId"],
                                    MenuId = (int)reader["MenuId"],
                                    MenuName = reader["MenuName"].ToString(),
                                    ItemName = reader["ItemName"].ToString(),
                                    Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                                    Price = (decimal)reader["Price"],
                                    Currency = reader["Currency"].ToString(),
                                    Ingredients = reader["Ingredients"] == DBNull.Value ? null : reader["Ingredients"].ToString(),
                                    Origin = reader["Origin"] == DBNull.Value ? null : reader["Origin"].ToString(),
                                    IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                    PrimaryPhotoPath = reader["PhotoPath"] == DBNull.Value ? null : reader["PhotoPath"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetAllItems: {ex.Message}");
            }
            return items;
        }

        public Item GetItemById(int id)
        {
            Item item = null;
            string query = @"
                SELECT i.*, m.MenuName, p.PhotoPath 
                FROM Items i
                INNER JOIN Menus m ON i.MenuId = m.MenuId
                LEFT JOIN ItemPhotos p ON i.ItemId = p.ItemId AND p.IsPrimary = 1
                WHERE i.ItemId = @ItemId";

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemId", id);
                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = new Item
                        {
                            ItemId = (int)reader["ItemId"],
                            MenuId = (int)reader["MenuId"],
                            MenuName = reader["MenuName"].ToString(),
                            ItemName = reader["ItemName"].ToString(),
                            Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                            Price = (decimal)reader["Price"],
                            Currency = reader["Currency"].ToString(),
                            Ingredients = reader["Ingredients"] == DBNull.Value ? null : reader["Ingredients"].ToString(),
                            Origin = reader["Origin"] == DBNull.Value ? null : reader["Origin"].ToString(),
                            IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                            PrimaryPhotoPath = reader["PhotoPath"] == DBNull.Value ? null : reader["PhotoPath"].ToString()
                        };
                    }
                }
            }
            return item;
        }

        public int InsertItem(Item model)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO Items (MenuId, ItemName, Description, Price, Currency, Ingredients, Origin, IsAvailable)
                        VALUES (@MenuId, @ItemName, @Description, @Price, @Currency, @Ingredients, @Origin, @IsAvailable);
                        SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MenuId", model.MenuId);
                        cmd.Parameters.AddWithValue("@ItemName", model.ItemName);
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(model.Description) ? DBNull.Value : (object)model.Description);
                        cmd.Parameters.AddWithValue("@Price", model.Price);
                        cmd.Parameters.AddWithValue("@Currency", string.IsNullOrEmpty(model.Currency) ? "$" : model.Currency);
                        cmd.Parameters.AddWithValue("@Ingredients", string.IsNullOrEmpty(model.Ingredients) ? DBNull.Value : (object)model.Ingredients);
                        cmd.Parameters.AddWithValue("@Origin", string.IsNullOrEmpty(model.Origin) ? DBNull.Value : (object)model.Origin);
                        cmd.Parameters.AddWithValue("@IsAvailable", model.IsAvailable ? 1 : 0);

                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in InsertItem: {ex.Message}");
                return 0;
            }
        }

        public bool UpdateItem(Item model)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        UPDATE Items SET 
                            MenuId = @MenuId, ItemName = @ItemName, Description = @Description, 
                            Price = @Price, Currency = @Currency, Ingredients = @Ingredients, 
                            Origin = @Origin, IsAvailable = @IsAvailable
                        WHERE ItemId = @ItemId";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ItemId", model.ItemId);
                        cmd.Parameters.AddWithValue("@MenuId", model.MenuId);
                        cmd.Parameters.AddWithValue("@ItemName", model.ItemName);
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(model.Description) ? DBNull.Value : (object)model.Description);
                        cmd.Parameters.AddWithValue("@Price", model.Price);
                        cmd.Parameters.AddWithValue("@Currency", string.IsNullOrEmpty(model.Currency) ? "$" : model.Currency);
                        cmd.Parameters.AddWithValue("@Ingredients", string.IsNullOrEmpty(model.Ingredients) ? DBNull.Value : (object)model.Ingredients);
                        cmd.Parameters.AddWithValue("@Origin", string.IsNullOrEmpty(model.Origin) ? DBNull.Value : (object)model.Origin);
                        cmd.Parameters.AddWithValue("@IsAvailable", model.IsAvailable ? 1 : 0);

                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateItem: {ex.Message}");
                return false;
            }
        }

        public bool DeleteItem(int itemId)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM Items WHERE ItemId = @ItemId";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ItemId", itemId);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in DeleteItem: {ex.Message}");
                return false;
            }
        }

        public int InsertItemPhoto(int itemId, string photoPath, bool isPrimary)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO ItemPhotos (ItemId, PhotoPath, IsPrimary)
                        VALUES (@ItemId, @PhotoPath, @IsPrimary);
                        SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ItemId", itemId);
                        cmd.Parameters.AddWithValue("@PhotoPath", photoPath);
                        cmd.Parameters.AddWithValue("@IsPrimary", isPrimary ? 1 : 0);

                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in InsertItemPhoto: {ex.Message}");
                return 0;
            }
        }
    }
}
