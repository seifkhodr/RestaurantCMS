using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RestoApp.Models;
using RestoApp.Helper;

namespace RestoApp
{
    public class ReservationRepository
    {
        public int CreateReservation(Reservation model)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO Reservations 
                            (UserId, TableId, GuestName, GuestEmail, GuestPhone, ReservationDate, EndTime, NumberOfGuests, Status, Notes)
                        VALUES 
                            (@UserId, @TableId, @GuestName, @GuestEmail, @GuestPhone, @ReservationDate, @EndTime, @NumberOfGuests, @Status, @Notes);
                        SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", model.UserId.HasValue ? (object)model.UserId.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@TableId", model.TableId.HasValue ? (object)model.TableId.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@GuestName", string.IsNullOrEmpty(model.GuestName) ? DBNull.Value : (object)model.GuestName);
                        cmd.Parameters.AddWithValue("@GuestEmail", string.IsNullOrEmpty(model.GuestEmail) ? DBNull.Value : (object)model.GuestEmail);
                        cmd.Parameters.AddWithValue("@GuestPhone", string.IsNullOrEmpty(model.GuestPhone) ? DBNull.Value : (object)model.GuestPhone);
                        cmd.Parameters.AddWithValue("@ReservationDate", model.ReservationDate);
                        cmd.Parameters.AddWithValue("@EndTime", model.EndTime.HasValue ? (object)model.EndTime.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@NumberOfGuests", model.NumberOfGuests);
                        cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(model.Status) ? "Pending" : model.Status);
                        cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(model.Notes) ? DBNull.Value : (object)model.Notes);

                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in CreateReservation: {ex.Message}");
                return 0;
            }
        }

        public bool IsTableAvailable(int tableId, DateTime requestedTime, int estimatedDurationMinutes = 120)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT COUNT(1) 
                        FROM Reservations 
                        WHERE TableId = @TableId 
                          AND Status NOT IN ('Cancelled', 'Completed')
                          AND 
                          (
                              (@RequestedTime >= ReservationDate AND @RequestedTime < ISNULL(EndTime, DATEADD(MINUTE, @Duration, ReservationDate)))
                              OR 
                              (DATEADD(MINUTE, @Duration, @RequestedTime) > ReservationDate AND DATEADD(MINUTE, @Duration, @RequestedTime) <= ISNULL(EndTime, DATEADD(MINUTE, @Duration, ReservationDate)))
                              OR
                              (@RequestedTime <= ReservationDate AND DATEADD(MINUTE, @Duration, @RequestedTime) >= ISNULL(EndTime, DATEADD(MINUTE, @Duration, ReservationDate)))
                          )";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TableId", tableId);
                        cmd.Parameters.AddWithValue("@RequestedTime", requestedTime);
                        cmd.Parameters.AddWithValue("@Duration", estimatedDurationMinutes);

                        int overlapCount = Convert.ToInt32(cmd.ExecuteScalar());
                        return overlapCount == 0; // Available if no overlaps
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in IsTableAvailable: {ex.Message}");
                return false; 
            }
        }
    }
}
