using System;
using System.Text;
using RestoApp.Models;

namespace RestoApp.ClientsView.Feedbacks
{
    public partial class Reviews : System.Web.UI.Page
    {
        private FeedbackRepository _repo = new FeedbackRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReviews();
            }
        }

        private void LoadReviews()
        {
            var reviews = _repo.GetApprovedFeedbacks();
            rptReviews.DataSource = reviews;
            rptReviews.DataBind();
        }

        protected string GetStarsHtml(int rating)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= 5; i++)
            {
                if (i <= rating)
                {
                    sb.Append("<i class=\"fa-solid fa-star\"></i>");
                }
                else
                {
                    sb.Append("<i class=\"fa-regular fa-star\"></i>");
                }
            }
            return sb.ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            /*
            // Session protection snippet
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Authentication/Login.aspx");
                return;
            }
            */

            pnlError.Visible = false;
            pnlSuccess.Visible = false;

            if (string.IsNullOrWhiteSpace(txtComment.Text))
            {
                lblError.Text = "Please write a comment before submitting.";
                pnlError.Visible = true;
                return;
            }

            Feedback newFeedback = new Feedback
            {
                GuestName = string.IsNullOrWhiteSpace(txtGuestName.Text) ? "Anonymous" : txtGuestName.Text.Trim(),
                Comment = txtComment.Text.Trim(),
                VisitRating = Convert.ToInt32(ddlRating.SelectedValue),
                CreatedAt = DateTime.Now
                // IsApproved defaults to 0 safely in schema and repo
            };

            int id = _repo.InsertFeedback(newFeedback);

            if (id > 0)
            {
                pnlSuccess.Visible = true;
                pnlForm.Visible = false; // Hide form on success
            }
            else
            {
                lblError.Text = "An error occurred while submitting your review. Please try again.";
                pnlError.Visible = true;
            }
        }
    }
}