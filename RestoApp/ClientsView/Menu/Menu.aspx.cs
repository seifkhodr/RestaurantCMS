using RestoApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace RestoApp.ClientsView
{
    public partial class Menu : System.Web.UI.Page
    {
        private ItemMenuRepository _repository = new ItemMenuRepository();
        
        public int? CurrentMenuId 
        {
            get 
            {
                if (int.TryParse(Request.QueryString["cat"], out int id)) return id;
                return null;
            }
        }

        public int CurrentPage
        {
            get
            {
                if (int.TryParse(Request.QueryString["page"], out int page) && page > 0) return page;
                return 1;
            }
        }

        public int TotalPages { get; set; }

        public string SearchKeyword
        {
            get { return Request.QueryString["q"] ?? string.Empty; }
        }

        private const int PageSize = 6; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtSearch.Text = SearchKeyword;
                BindCategories();
                BindItems();
            }
        }

        private void BindCategories()
        {
            var menus = _repository.GetActiveMenus();
            rptCategories.DataSource = menus;
            rptCategories.DataBind();

            if (CurrentMenuId == null)
            {
                lnkAllCategories.CssClass = "btn btn-dark category-pill";
            }
            else
            {
                lnkAllCategories.CssClass = "btn btn-outline-dark category-pill";
            }
        }

        private void BindItems()
        {
            int totalCount;
            var items = _repository.GetItems(CurrentMenuId, SearchKeyword, CurrentPage, PageSize, out totalCount);

            if (items.Count > 0)
            {
                rptItems.DataSource = items;
                rptItems.DataBind();
                rptItems.Visible = true;
                pnlNoResults.Visible = false;
                
                BindPagination(totalCount);
            }
            else
            {
                rptItems.Visible = false;
                pnlNoResults.Visible = true;
                pnlPagination.Visible = false;
            }
        }

        private void BindPagination(int totalCount)
        {
            TotalPages = (int)Math.Ceiling((double)totalCount / PageSize);
            
            if (TotalPages <= 1)
            {
                pnlPagination.Visible = false;
                return;
            }

            pnlPagination.Visible = true;
            
            var pages = new List<object>();
            for (int i = 1; i <= TotalPages; i++)
            {
                pages.Add(new {
                    PageIndex = i,
                    IsActive = (i == CurrentPage)
                });
            }

            rptPagination.DataSource = pages;
            rptPagination.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            string url = "Menu.aspx?";
            
            if (CurrentMenuId.HasValue)
                url += $"cat={CurrentMenuId.Value}&";
                
            if (!string.IsNullOrEmpty(keyword))
                url += $"q={Server.UrlEncode(keyword)}";
                
            Response.Redirect(url.TrimEnd('?', '&'));
        }

        protected string GetCategoryUrl(object menuIdObj)
        {
            int menuId = Convert.ToInt32(menuIdObj);
            string url = $"Menu.aspx?cat={menuId}";
            if (!string.IsNullOrEmpty(SearchKeyword))
                url += $"&q={Server.UrlEncode(SearchKeyword)}";
            return url;
        }

        protected string GetCategoryCssClass(object menuIdObj)
        {
            int menuId = Convert.ToInt32(menuIdObj);
            if (CurrentMenuId.HasValue && CurrentMenuId.Value == menuId)
                return "btn btn-dark category-pill";
            
            return "btn btn-outline-dark category-pill";
        }

        protected string GetPageUrl(object pageIndexObj)
        {
            int pageIndex = Convert.ToInt32(pageIndexObj);
            string url = $"Menu.aspx?page={pageIndex}";
            
            if (CurrentMenuId.HasValue)
                url += $"&cat={CurrentMenuId.Value}";
                
            if (!string.IsNullOrEmpty(SearchKeyword))
                url += $"&q={Server.UrlEncode(SearchKeyword)}";
                
            return url;
        }

        protected void rptItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }

        protected void rptItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {

                //if (Session["UserId"] == null)
                //{
                //    Response.Redirect("~/Authentication/Login.aspx");
                //    return;
                //}


                int itemId = Convert.ToInt32(e.CommandArgument);
                var item = _repository.GetItemById(itemId);

                if (item != null)
                {
                    CartManager.AddItem(item);
                    Response.Redirect(Request.RawUrl);
                }
            }
        }
    }
}
