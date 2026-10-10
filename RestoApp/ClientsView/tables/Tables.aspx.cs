using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoApp;


namespace RestoApp.ClientsView.Tables
{
    public partial class Tables : System.Web.UI.Page
    {
        private TableRepository _repository = new TableRepository();
        public int PageSize { get; set; } = 6;
        
        public int CurrentPage
        {
            get
            {
                if (int.TryParse(Request.QueryString["page"], out int page) && page > 0)
                {
                    return page;
                }
                return 1;
            }
        }

        public string SearchTerm
        {
            get
            {
                return Request.QueryString["search"] ?? string.Empty;
            }
        }

        public int TotalRecords { get; set; }
        
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalRecords / PageSize);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtSearch.Text = SearchTerm;
                BindTables();
            }
        }

        private void BindTables()
        {
            int totalRecords;
            var tables = _repository.GetTables(SearchTerm, CurrentPage, PageSize, out totalRecords);
            TotalRecords = totalRecords;

            if (tables.Count > 0)
            {
                rptTables.Visible = true;
                pnlNoResults.Visible = false;
                rptTables.DataSource = tables;
                rptTables.DataBind();
                
                BindPagination();
            }
            else
            {
                rptTables.Visible = false;
                pnlNoResults.Visible = true;
                pnlPagination.Visible = false;
            }
        }

        private void BindPagination()
        {
            if (TotalPages > 1)
            {
                pnlPagination.Visible = true;
                var pages = new List<object>();
                for (int i = 1; i <= TotalPages; i++)
                {
                    pages.Add(new { PageNumber = i });
                }
                rptPagination.DataSource = pages;
                rptPagination.DataBind();
            }
            else
            {
                pnlPagination.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string url = "Tables.aspx?";
            string search = txtSearch.Text.Trim();
            
            if (!string.IsNullOrEmpty(search))
            {
                url += "search=" + HttpUtility.UrlEncode(search) + "&";
            }
            
            url += "page=1";
            Response.Redirect(url);
        }

        public string GetPageUrl(int pageNumber)
        {
            string url = "Tables.aspx?page=" + pageNumber;
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                url += "&search=" + HttpUtility.UrlEncode(SearchTerm);
            }
            return url;
        }
    }
}
