using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RestoApp.Helper;
using RestoApp.Models;

namespace RestoApp
{
    public class TableRepository
    {
        public RestaurantTable GetTableById(int id)
        {
            RestaurantTable table = null;
            string sql = "SELECT * FROM RestaurantTables WHERE TableId = @TableId AND IsActive = 1";
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TableId", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            table = new RestaurantTable
                            {
                                TableId = Convert.ToInt32(reader["TableId"]),
                                TableNumber = reader["TableNumber"].ToString(),
                                SeatingCapacity = Convert.ToInt32(reader["SeatingCapacity"]),
                                Location = reader["Location"] != DBNull.Value ? reader["Location"].ToString() : null,
                                PhotoPath = reader["PhotoPath"] != DBNull.Value ? reader["PhotoPath"].ToString() : null,
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            };
                        }
                    }
                }
            }
            return table;
        }

        public List<RestaurantTable> GetSimilarTables(int currentTableId, string location, int limit)
        {
            var tables = new List<RestaurantTable>();
       
            
            string sql = $@"SELECT TOP {limit} * FROM RestaurantTables WHERE TableId != @CurrentTableId AND IsActive = 1 
                AND (Location = @Location OR @Location IS NULL) ORDER BY NEWID()";
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CurrentTableId", currentTableId);
                    cmd.Parameters.AddWithValue("@Location", (object)location ?? DBNull.Value);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(new RestaurantTable
                            {
                                TableId = Convert.ToInt32(reader["TableId"]),
                                TableNumber = reader["TableNumber"].ToString(),
                                SeatingCapacity = Convert.ToInt32(reader["SeatingCapacity"]),
                                Location = reader["Location"] != DBNull.Value ? reader["Location"].ToString() : null,
                                PhotoPath = reader["PhotoPath"] != DBNull.Value ? reader["PhotoPath"].ToString() : null,
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
            }
            return tables;
        }

        public List<RestaurantTable> GetTables(string searchTerm, int pageNumber, int pageSize, out int totalRecords)
        {
            var tables = new List<RestaurantTable>();
            totalRecords = 0;

            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();

                string countSql = @"
                    SELECT COUNT(*) 
                    FROM RestaurantTables 
                    WHERE IsActive = 1
                    AND (@SearchTerm = '' OR Location LIKE '%' + @SearchTerm + '%' OR TableNumber LIKE '%' + @SearchTerm + '%')";
                
                using (var cmdCount = new SqlCommand(countSql, conn))
                {
                    cmdCount.Parameters.AddWithValue("@SearchTerm", string.IsNullOrEmpty(searchTerm) ? "" : searchTerm);
                    totalRecords = (int)cmdCount.ExecuteScalar();
                }

                string dataSql = @"
                    SELECT TableId, TableNumber, SeatingCapacity, Location, PhotoPath, IsActive
                    FROM RestaurantTables
                    WHERE IsActive = 1
                      AND (@SearchTerm = '' OR Location LIKE '%' + @SearchTerm + '%' OR TableNumber LIKE '%' + @SearchTerm + '%')
                    ORDER BY SeatingCapacity ASC, TableNumber ASC
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY";

                using (var cmdData = new SqlCommand(dataSql, conn))
                {
                    cmdData.Parameters.AddWithValue("@SearchTerm", string.IsNullOrEmpty(searchTerm) ? "" : searchTerm);
                    cmdData.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                    cmdData.Parameters.AddWithValue("@PageSize", pageSize);

                    using (var reader = cmdData.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(new RestaurantTable
                            {
                                TableId = Convert.ToInt32(reader["TableId"]),
                                TableNumber = reader["TableNumber"].ToString(),
                                SeatingCapacity = Convert.ToInt32(reader["SeatingCapacity"]),
                                Location = reader["Location"] != DBNull.Value ? reader["Location"].ToString() : null,
                                PhotoPath = reader["PhotoPath"] != DBNull.Value ? reader["PhotoPath"].ToString() : null,
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
            }

            return tables;
        }
    }
}
