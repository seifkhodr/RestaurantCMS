using System;
using System.Web.UI.WebControls;
using RestoApp;
using RestoApp.AdminRepo;
using RestoApp.Helper;

namespace RestoApp.AdminView.Reservations
{
    public partial class AdminReservations : System.Web.UI.Page
    {
        private ReservationRepositoryAdmin _repo = new ReservationRepositoryAdmin();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthHelper.RequireAdmin(this))
            {
                return;
            }

            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gvReservations.DataSource = _repo.GetAllReservations();
            gvReservations.DataBind();
        }

        protected string GetStatusBadgeClass(string status)
        {
            switch (status?.ToLower())
            {
                case "pending": return "bg-warning text-dark";
                case "approved": return "bg-success";
                case "cancelled": return "bg-danger";
                case "completed": return "bg-info text-dark";
                default: return "bg-secondary";
            }
        }

        protected void gvReservations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ApproveStatus" || e.CommandName == "CancelStatus" || e.CommandName == "CompleteStatus")
            {
                int reservationId = Convert.ToInt32(e.CommandArgument);
                string newStatus = "";

                if (e.CommandName == "ApproveStatus") newStatus = "Approved";
                if (e.CommandName == "CancelStatus") newStatus = "Cancelled";
                if (e.CommandName == "CompleteStatus") newStatus = "Completed";

                if (!string.IsNullOrEmpty(newStatus))
                {
                    bool success = _repo.UpdateReservationStatus(reservationId, newStatus);
                    if (success)
                    {
                        if (newStatus == "Approved" || newStatus == "Cancelled")
                        {
                            var reservation = _repo.GetAllReservations().Find(r => r.ReservationId == reservationId);
                            if (reservation != null && !string.IsNullOrEmpty(reservation.GuestEmail))
                            {
                                string subject = "";
                                string body = "";

                                if (newStatus == "Approved")
                                {
                                    subject = "Reservation Confirmation";
                                    body = $"Dear {reservation.GuestName},\n\nYour reservation for {reservation.NumberOfGuests} guests on {reservation.ReservationDate:g} has been approved and confirmed.\n\nThank you for choosing us!";
                                }
                                else if (newStatus == "Cancelled")
                                {
                                    subject = "Reservation Cancelled";
                                    body = $"Dear {reservation.GuestName},\n\nWe regret to inform you that your reservation for {reservation.NumberOfGuests} guests on {reservation.ReservationDate:g} has been cancelled.\n\nPlease contact the administrator for more information.";
                                }

                                try
                                {
                                    EmailService.SendEmail(reservation.GuestEmail, subject, body);
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine($"Failed to send email: {ex.Message}");
                                }
                            }
                        }

                        ShowMessage($"Reservation #{reservationId} marked as {newStatus}.", true);
                        BindGrid();
                    }
                    else
                    {
                        ShowMessage("Failed to update status. Please try again.", false);
                    }
                }
            }
        }

        private void ShowMessage(string msg, bool isSuccess)
        {
            pnlMessage.Visible = true;
            pnlMessage.CssClass = isSuccess ? "alert alert-success" : "alert alert-danger";
            lblMessage.Text = msg;
        }
    }
}
