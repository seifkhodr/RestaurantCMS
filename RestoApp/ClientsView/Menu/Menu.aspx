<%@ Page Title="Our Menu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="RestoApp.ClientsView.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Our Menu
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .menu-header {
            background: linear-gradient(rgba(0, 0, 0, 0.7), rgba(0, 0, 0, 0.7)), url('https://images.unsplash.com/photo-1414235077428-338989a2e8c0?auto=format&fit=crop&w=1920&q=80') no-repeat center center;
            background-size: cover;
            color: #fff;
            padding: 80px 0;
            text-align: center;
            margin-bottom: 40px;
        }
        .item-card {
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            border: none;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 4px 6px rgba(0,0,0,0.05);
        }
        .item-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 12px 20px rgba(0,0,0,0.1);
        }
        .item-img-top {
            height: 220px;
            object-fit: cover;
            width: 100%;
        }
        .price-badge {
            position: absolute;
            top: 15px;
            right: 15px;
            background-color: rgba(0, 0, 0, 0.8);
            color: white;
            padding: 8px 15px;
            border-radius: 25px;
            font-weight: bold;
            font-size: 1.1rem;
        }
        .category-pill {
            margin: 0 5px 10px 5px;
            border-radius: 30px;
            padding: 8px 20px;
            font-weight: 500;
        }
        .empty-state {
            padding: 60px 0;
            text-align: center;
            color: #6c757d;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <!-- Header -->
    <div class="menu-header">
        <div class="container">
            <h1 class="display-4 fw-bold">Explore Our Menu</h1>
            <p class="lead">Discover culinary delights crafted with passion and the finest ingredients.</p>
        </div>
    </div>

    <div class="container mb-5">
        <!-- Search and Filters row -->
        <div class="row mb-4 align-items-center">
            
            <div class="col-lg-8 mb-3 mb-lg-0">
                <!-- Categories -->
                <div class="d-flex flex-wrap justify-content-center justify-content-lg-start">
                    <asp:HyperLink runat="server" ID="lnkAllCategories" CssClass="btn btn-outline-dark category-pill" NavigateUrl="~/ClientsView/Menu/Menu.aspx">All</asp:HyperLink>
                    
                    <asp:Repeater ID="rptCategories" runat="server">
                        <ItemTemplate>
                            <a href='<%# GetCategoryUrl(Eval("MenuId")) %>' class='<%# GetCategoryCssClass(Eval("MenuId")) %>'>
                                <%# Eval("MenuName") %>
                            </a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            
            <div class="col-lg-4">
                <!-- Search bar -->
                <div class="input-group">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control rounded-start-pill" placeholder="Search dish or ingredients..."></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary rounded-end-pill px-4" OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>

        <!-- Items Grid -->
        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
            <asp:Repeater ID="rptItems" runat="server" OnItemDataBound="rptItems_ItemDataBound" OnItemCommand="rptItems_ItemCommand">
                <ItemTemplate>
                    <div class="col">
                        <div class="card item-card h-100">
                            <div class="position-relative">
                                <img src='<%# Eval("PrimaryPhotoPath") %>' class="item-img-top" alt='<%# Eval("ItemName") %>'>
                                <span class="price-badge"><%# Eval("Currency") %><%# Eval("Price", "{0:0.00}") %></span>
                            </div>
                            <div class="card-body d-flex flex-column">
                                <h5 class="card-title fw-bold"><%# Eval("ItemName") %></h5>
                                <p class="card-text text-muted flex-grow-1"><%# Eval("Description") %></p>
                                <div class="mt-3 d-flex justify-content-between">
                                    <a href='../Items/ItemDetails.aspx?id=<%# Eval("ItemId") %>' class="btn btn-outline-secondary w-50 me-2"><i class="fa-regular fa-eye"></i> View</a>
                                    <asp:LinkButton ID="btnAddToCart" runat="server" CommandName="AddToCart" CommandArgument='<%# Eval("ItemId") %>' CssClass="btn btn-primary w-50"><i class="fa-solid fa-cart-plus"></i> Add</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- Empty State -->
        <asp:Panel ID="pnlNoResults" runat="server" Visible="false" CssClass="empty-state">
            <i class="fa-solid fa-utensils fa-3x mb-3 text-muted"></i>
            <h3>No items found</h3>
            <p>We couldn't find any dishes matching your search criteria. Try a different keyword or category.</p>
            <a href="Menu.aspx" class="btn btn-outline-primary mt-2">View All Items</a>
        </asp:Panel>

        <!-- Pagination -->
        <asp:Panel ID="pnlPagination" runat="server" CssClass="app-pagination-wrap mt-5">
            <nav class="app-pagination-nav" aria-label="Menu pagination">
                <ul class="pagination app-pagination justify-content-center align-items-center mb-0">
                    <li class='page-item app-page-arrow <%= CurrentPage <= 1 ? "disabled" : "" %>'>
                        <a class="page-link" href='<%= GetPageUrl(CurrentPage - 1) %>' aria-label="Previous page">
                            <i class="fa-solid fa-chevron-left"></i>
                            <span class="d-none d-sm-inline">Previous</span>
                        </a>
                    </li>

                    <asp:Repeater ID="rptPagination" runat="server">
                        <ItemTemplate>
                            <li class='<%# Convert.ToBoolean(Eval("IsActive")) ? "page-item active" : "page-item" %>'>
                                <a class="page-link" href='<%# GetPageUrl(Eval("PageIndex")) %>'><%# Eval("PageIndex") %></a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>

                    <li class='page-item app-page-arrow <%= CurrentPage >= TotalPages ? "disabled" : "" %>'>
                        <a class="page-link" href='<%= GetPageUrl(CurrentPage + 1) %>' aria-label="Next page">
                            <span class="d-none d-sm-inline">Next</span>
                            <i class="fa-solid fa-chevron-right"></i>
                        </a>
                    </li>
                </ul>
            </nav>
        </asp:Panel>
    </div>
</asp:Content>
