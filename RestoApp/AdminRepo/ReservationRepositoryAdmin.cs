using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RestoApp.Models;
using RestoApp.Helper;

namespace RestoApp.AdminRepo
{
    public class ReservationRepositoryAdmin
    {
        public List<ReservationAdminView> GetAllReservations()
        {
            List<ReservationAdminView> list = new List<ReservationAdminView>();
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    string sql = @"
                        SELECT r.*, t.TableNumber, t.SeatingCapacity
                        FROM Reservations r
                        LEFT JOIN RestaurantTables t ON r.TableId = t.TableId
                        ORDER BY r.ReservationDate DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new ReservationAdminView
                                {
                                    ReservationId = Convert.ToInt32(reader["ReservationId"]),
                                    UserId = reader["UserId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["UserId"]) : null,
                                    TableId = reader["TableId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["TableId"]) : null,
                                    GuestName = reader["GuestName"]?.ToString(),
                                    GuestEmail = reader["GuestEmail"]?.ToString(),
                                    GuestPhone = reader["GuestPhone"]?.ToString(),
                                    ReservationDate = Convert.ToDateTime(reader["ReservationDate"]),
                                    EndTime = reader["EndTime"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["EndTime"]) : null,
                                    NumberOfGuests = Convert.ToInt32(reader["NumberOfGuests"]),
                                    Status = reader["Status"]?.ToString(),
                                    Notes = reader["Notes"]?.ToString(),
                                    TableNumber = reader["TableNumber"]?.ToString(),
                                    SeatingCapacity = reader["SeatingCapacity"] != DBNull.Value ? Convert.ToInt32(reader["SeatingCapacity"]) : 0
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetAllReservations: {ex.Message}");
            }
            return list;
        }

        public bool UpdateReservationStatus(int reservationId, string newStatus)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE Reservations SET Status = @Status WHERE ReservationId = @ReservationId";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateReservationStatus: {ex.Message}");
                return false;
            }
        }
    }
}
