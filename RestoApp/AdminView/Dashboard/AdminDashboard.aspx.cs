using System;
using RestoApp.AdminRepo;
using RestoApp.Helper;

namespace RestoApp.AdminView.Dashboard
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        private UserRepositoryAdmin _userRepo = new UserRepositoryAdmin();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthHelper.RequireAdmin(this))
            {
                return;
            }

            if (!IsPostBack)
            {
                LoadStats();
            }
        }

        private void LoadStats()
        {
            litUserCount.Text = _userRepo.GetCount("Users").ToString();
            litReservationCount.Text = _userRepo.GetCount("Reservations").ToString();
            litItemCount.Text = _userRepo.GetCount("Items").ToString();
            litFeedbackCount.Text = _userRepo.GetCount("Feedbacks").ToString();
        }
    }
}
