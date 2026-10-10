using System;
using RestoApp;
using RestoApp.Models;

namespace RestoApp.ClientsView.TableDetails
{
    public partial class TableDetails : System.Web.UI.Page
    {
        private TableRepository _repository = new TableRepository();

        public int CurrentTableId
        {
            get
            {
                if (int.TryParse(Request.QueryString["id"], out int id)) return id;
                return 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrentTableId > 0)
                {
                    LoadTableDetails();
                }
                else
                {
                    ShowError();
                }
            }
        }

        private void LoadTableDetails()
        {
            var table = _repository.GetTableById(CurrentTableId);

            if (table != null)
            {
                pnlDetails.Visible = true;
                pnlError.Visible = false;

                litTableNumber.Text = table.TableNumber;
                litCapacity.Text = table.SeatingCapacity.ToString();
                litCapacityDesc.Text = table.SeatingCapacity.ToString();
                
                string loc = string.IsNullOrEmpty(table.Location) ? "General Dining" : table.Location;
                litLocation.Text = loc;
                litLocationBadge.Text = loc;
                litLocationDesc.Text = loc;

                imgMain.ImageUrl = string.IsNullOrEmpty(table.PhotoPath) ? "/Content/img/no-image.png" : table.PhotoPath;
                
                var similar = _repository.GetSimilarTables(table.TableId, table.Location, 3);
                if (similar != null && similar.Count > 0)
                {
                    pnlSimilarTables.Visible = true;
                    rptSimilar.DataSource = similar;
                    rptSimilar.DataBind();
                }
                else
                {
                    pnlSimilarTables.Visible = false;
                }
            }
            else
            {
                ShowError();
            }
        }

        private void ShowError()
        {
            pnlDetails.Visible = false;
            pnlSimilarTables.Visible = false;
            pnlError.Visible = true;
        }
    }
}