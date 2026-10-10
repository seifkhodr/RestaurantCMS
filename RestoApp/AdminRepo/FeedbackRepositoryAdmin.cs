using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RestoApp.Models;
using RestoApp.Helper;

namespace RestoApp.AdminRepo
{
    public class FeedbackRepositoryAdmin
    {
        public List<Feedback> GetAllFeedbacks()
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
                System.Diagnostics.Debug.WriteLine($"Error in GetAllFeedbacks: {ex.Message}");
            }
            return feedbacks;
        }

        public bool ApproveFeedback(int feedbackId, bool approve)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE Feedbacks SET IsApproved = @IsApproved WHERE FeedbackId = @FeedbackId";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IsApproved", approve ? 1 : 0);
                        cmd.Parameters.AddWithValue("@FeedbackId", feedbackId);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ApproveFeedback: {ex.Message}");
                return false;
            }
        }

        public bool DeleteFeedback(int feedbackId)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM Feedbacks WHERE FeedbackId = @FeedbackId";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FeedbackId", feedbackId);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in DeleteFeedback: {ex.Message}");
                return false;
            }
        }
    }
}
