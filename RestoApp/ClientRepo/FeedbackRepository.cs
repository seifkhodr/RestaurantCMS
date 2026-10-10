using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RestoApp.Models;
using RestoApp.Helper;

namespace RestoApp
{
    public class FeedbackRepository
    {
        public int InsertFeedback(Feedback model)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO Feedbacks (UserId, ReservationId, GuestName, Comment, VisitRating, IsApproved)
                        VALUES (@UserId, @ReservationId, @GuestName, @Comment, @VisitRating, 0);
                        SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", model.UserId.HasValue ? (object)model.UserId.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@ReservationId", model.ReservationId.HasValue ? (object)model.ReservationId.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@GuestName", string.IsNullOrEmpty(model.GuestName) ? DBNull.Value : (object)model.GuestName);
                        cmd.Parameters.AddWithValue("@Comment", model.Comment);
                        cmd.Parameters.AddWithValue("@VisitRating", model.VisitRating);

                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in InsertFeedback: {ex.Message}");
                return 0;
            }
        }

        public List<Feedback> GetApprovedFeedbacks()
        {
            List<Feedback> feedbacks = new List<Feedback>();
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT FeedbackId, UserId, ReservationId, GuestName, Comment, VisitRating, CreatedAt, IsApproved 
                        FROM Feedbacks 
                        WHERE IsApproved = 1 
                        ORDER BY CreatedAt DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                feedbacks.Add(new Feedback
                                {
                                    FeedbackId = Convert.ToInt32(reader["FeedbackId"]),
                                    UserId = reader["UserId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["UserId"]) : null,
                                    ReservationId = reader["ReservationId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ReservationId"]) : null,
                                    GuestName = reader["GuestName"] != DBNull.Value ? reader["GuestName"].ToString() : "Anonymous",
                                    Comment = reader["Comment"].ToString(),
                                    VisitRating = Convert.ToInt32(reader["VisitRating"]),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                    IsApproved = Convert.ToBoolean(reader["IsApproved"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetApprovedFeedbacks: {ex.Message}");
            }
            return feedbacks;
        }
    }
}
