using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RestoApp.Models;
using RestoApp.Helper;

namespace RestoApp.AdminRepo
{
    public class TableRepositoryAdmin
    {
        public List<RestaurantTable> GetAllTables()
        {
            var tables = new List<RestaurantTable>();
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM RestaurantTables ORDER BY TableNumber";
                    using (var cmd = new SqlCommand(sql, conn))
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
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"Error in GetAllTables: {ex.Message}"); }
            return tables;
        }

        public int InsertTable(RestaurantTable model)
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"INSERT INTO RestaurantTables (TableNumber, SeatingCapacity, Location, PhotoPath, IsActive) VALUES (@TableNumber, @SeatingCapacity, @Location, @PhotoPath, @IsActive); SELECT SCOPE_IDENTITY();";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TableNumber", model.TableNumber);
                        cmd.Parameters.AddWithValue("@SeatingCapacity", model.SeatingCapacity);
                        cmd.Parameters.AddWithValue("@Location", string.IsNullOrEmpty(model.Location) ? DBNull.Value : (object)model.Location);
                        cmd.Parameters.AddWithValue("@PhotoPath", string.IsNullOrEmpty(model.PhotoPath) ? DBNull.Value : (object)model.PhotoPath);
                        cmd.Parameters.AddWithValue("@IsActive", model.IsActive ? 1 : 0);
                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"Error in InsertTable: {ex.Message}"); return 0; }
        }

        public bool UpdateTable(RestaurantTable model)
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"UPDATE RestaurantTables SET TableNumber=@TableNumber, SeatingCapacity=@SeatingCapacity, Location=@Location, PhotoPath=@PhotoPath, IsActive=@IsActive WHERE TableId=@TableId";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TableId", model.TableId);
                        cmd.Parameters.AddWithValue("@TableNumber", model.TableNumber);
                        cmd.Parameters.AddWithValue("@SeatingCapacity", model.SeatingCapacity);
                        cmd.Parameters.AddWithValue("@Location", string.IsNullOrEmpty(model.Location) ? DBNull.Value : (object)model.Location);
                        cmd.Parameters.AddWithValue("@PhotoPath", string.IsNullOrEmpty(model.PhotoPath) ? DBNull.Value : (object)model.PhotoPath);
                        cmd.Parameters.AddWithValue("@IsActive", model.IsActive ? 1 : 0);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"Error in UpdateTable: {ex.Message}"); return false; }
        }

        public bool DeleteTable(int tableId)
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("DELETE FROM RestaurantTables WHERE TableId=@TableId", conn))
                    {
                        cmd.Parameters.AddWithValue("@TableId", tableId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"Error in DeleteTable: {ex.Message}"); return false; }
        }
    }
}
