using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RestoApp.Models;
using RestoApp.Helper;

namespace RestoApp
{
    public class ItemMenuRepository
    {
        public List<Menu> GetActiveMenus()
        {
            var menus = new List<Menu>();
            string query = "SELECT MenuId, MenuName, Description FROM Menus WHERE IsActive = 1 ORDER BY DisplayOrder";

            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    menus.Add(new Menu
                    {
                        MenuId = (int)reader["MenuId"],
                        MenuName = reader["MenuName"].ToString(),
                        Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString()
                    });
                }
            }

            return menus;
        }

        public List<Item> GetItems(int? menuId, string searchKeyword, int pageIndex, int pageSize, out int totalCount)
        {
            List<Item> items = new List<Item>();
            totalCount = 0;

            string whereClause = "WHERE i.IsAvailable = 1 ";

            if (menuId.HasValue && menuId.Value > 0)
                whereClause += "AND i.MenuId = @MenuId ";

            if (!string.IsNullOrWhiteSpace(searchKeyword))
                whereClause += "AND (i.ItemName LIKE @Search OR i.Description LIKE @Search) ";

            int offset = (pageIndex - 1) * pageSize;

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();

                SqlCommand countCmd = new SqlCommand($"SELECT COUNT(*) FROM Items i {whereClause}", conn);

                if (menuId.HasValue && menuId.Value > 0)
                    countCmd.Parameters.AddWithValue("@MenuId", menuId.Value);

                if (!string.IsNullOrWhiteSpace(searchKeyword))
                    countCmd.Parameters.AddWithValue("@Search", "%" + searchKeyword.Trim() + "%");

                totalCount = (int)countCmd.ExecuteScalar();

                SqlCommand dataCmd = new SqlCommand($@"
            SELECT i.*, p.PhotoPath 
            FROM Items i
            LEFT JOIN ItemPhotos p ON i.ItemId = p.ItemId AND p.IsPrimary = 1
            {whereClause}
            ORDER BY i.ItemId DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY", conn);

                if (menuId.HasValue && menuId.Value > 0)
                    dataCmd.Parameters.AddWithValue("@MenuId", menuId.Value);

                if (!string.IsNullOrWhiteSpace(searchKeyword))
                    dataCmd.Parameters.AddWithValue("@Search", "%" + searchKeyword.Trim() + "%");

                dataCmd.Parameters.AddWithValue("@Offset", offset);

                dataCmd.Parameters.AddWithValue("@PageSize", pageSize);

                using (var reader = dataCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new Item
                        {
                            ItemId = (int)reader["ItemId"],
                            MenuId = (int)reader["MenuId"],
                            ItemName = reader["ItemName"].ToString(),
                            Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                            Price = (decimal)reader["Price"],
                            Currency = reader["Currency"].ToString(),
                            PrimaryPhotoPath = reader["PhotoPath"] == DBNull.Value
                                ? "https://via.placeholder.com/500x300?text=No+Image"
                                : reader["PhotoPath"].ToString()
                        });
                    }
                }
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
                            Origin = reader["Origin"] == DBNull.Value ? "House Special" : reader["Origin"].ToString(),
                            PrimaryPhotoPath = reader["PhotoPath"] == DBNull.Value ? "https://via.placeholder.com/500x300?text=No+Image" : reader["PhotoPath"].ToString()
                        };
                    }
                }
            }
            return item;
        }

        public List<Item> GetSimilarItems(int currentItemId, int menuId, int limit)
        {
            var items = new List<Item>();
            string query = $@"
                SELECT TOP {limit} i.*, p.PhotoPath 
                FROM Items i
                LEFT JOIN ItemPhotos p ON i.ItemId = p.ItemId AND p.IsPrimary = 1
                WHERE i.IsAvailable = 1 AND i.MenuId = @MenuId AND i.ItemId <> @ItemId
                ORDER BY i.ItemId DESC";

            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MenuId", menuId);
                    cmd.Parameters.AddWithValue("@ItemId", currentItemId);
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Item
                            {
                                ItemId = (int)reader["ItemId"],
                                MenuId = (int)reader["MenuId"],
                                ItemName = reader["ItemName"].ToString(),
                                Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                                Price = (decimal)reader["Price"],
                                Currency = reader["Currency"].ToString(),
                                PrimaryPhotoPath = reader["PhotoPath"] == DBNull.Value ? "https://via.placeholder.com/500x300?text=No+Image" : reader["PhotoPath"].ToString()
                            });
                        }
                    }
                }
            }
            return items;
        }
    }
}
