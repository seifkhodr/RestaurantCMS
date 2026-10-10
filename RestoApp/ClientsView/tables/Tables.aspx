<%@ Page Title="Reserve a Table" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tables.aspx.cs" Inherits="RestoApp.ClientsView.Tables.Tables" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Reserve a Table
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .menu-header {
            background: linear-gradient(rgba(0, 0, 0, 0.7), rgba(0, 0, 0, 0.7)), url('https://images.unsplash.com/photo-1550966871-c06db12c29fb?auto=format&fit=crop&w=1920&q=80') no-repeat center center;
            background-size: cover;
            color: #fff;
            padding: 80px 0;
            text-align: center;
            margin-bottom: 40px;
        }
        .table-card {
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            border: none;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 4px 6px rgba(0,0,0,0.05);
        }
        .table-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 12px 20px rgba(0,0,0,0.1);
        }
        .table-photo {
            height: 220px;
            object-fit: cover;
            width: 100%;
        }
        .img-placeholder {
            height: 220px;
            background-color: #f8f9fa;
            display: flex;
            align-items: center;
            justify-content: center;
            color: #adb5bd;
            font-size: 3rem;
        }
        .category-pill {
            margin: 0 5px 10px 5px;
            border-radius: 30px;
            padding: 8px 20px;
            font-weight: 500;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <!-- Header -->
    <div class="menu-header">
        <div class="container">
            <h1 class="display-4 fw-bold"><i class="fa-solid fa-chair"></i> Reserve a Table</h1>
            <p class="lead">Find the perfect spot for your next amazing dining experience.</p>
        </div>
    </div>

    <div class="container mb-5">
        <div class="row mb-4 align-items-center">
            <div class="col-lg-8 mb-3 mb-lg-0">
                <!-- Spacing equivalent to categories in Menu page -->
            </div>
            
            <!-- Search Bar -->
            <div class="col-lg-4">
                <asp:Panel DefaultButton="btnSearch" runat="server" CssClass="input-group">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control rounded-start-pill" placeholder="Search by location or number..."></asp:TextBox>
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary rounded-end-pill px-4" OnClick="btnSearch_Click">
                        <i class="fa-solid fa-search"></i> Search
                    </asp:LinkButton>
                </asp:Panel>
            </div>
        </div>

        <!-- Table Grid -->
    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
        <asp:Repeater ID="rptTables" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100 border-0 shadow-sm table-card">
                        
                        <asp:PlaceHolder runat="server" Visible='<%# !string.IsNullOrEmpty(Eval("PhotoPath")?.ToString()) %>'>
                            <img src='<%# Eval("PhotoPath") %>' class="card-img-top table-photo" alt='Table <%# Eval("TableNumber") %>' onerror="this.src='/Content/img/no-image.png'" />
                        </asp:PlaceHolder>
                        
                        <asp:PlaceHolder runat="server" Visible='<%# string.IsNullOrEmpty(Eval("PhotoPath")?.ToString()) %>'>
                            <div class="card-img-top img-placeholder">
                                <i class="fa-solid fa-utensils"></i>
                            </div>
                        </asp:PlaceHolder>

                        <div class="card-body d-flex flex-column">
                            <h5 class="card-title fw-bold">Table <%# Eval("TableNumber") %></h5>
                            
                            <p class="card-text text-muted mb-2">
                                <i class="fa-solid fa-location-dot"></i> <%# string.IsNullOrEmpty(Eval("Location")?.ToString()) ? "General Area" : Eval("Location") %>
                            </p>
                            
                            <div class="mb-3">
                                <span class="badge bg-info text-dark">
                                    <i class="fa-solid fa-users"></i> Seats: <%# Eval("SeatingCapacity") %>
                                </span>
                            </div>
                            
                            <div class="mt-auto d-flex justify-content-between">
                                <a href='../TableDetails/TableDetails.aspx?id=<%# Eval("TableId") %>' class="btn btn-outline-secondary w-50 me-2">
                                    <i class="fa-regular fa-eye"></i> View
                                </a>
                                <a href='../Reservationn/ReservationRequest.aspx?table=<%# Eval("TableId") %>' class="btn btn-primary w-50">
                                    <i class="fa-solid fa-calendar-check"></i> Book
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    
    <!-- Empty State -->
    <asp:Panel ID="pnlNoResults" runat="server" Visible="false" CssClass="text-center py-5">
        <i class="fa-solid fa-search fa-3x text-muted mb-3"></i>
        <h4>No tables found</h4>
        <p class="text-muted">Try adjusting your search criteria.</p>
        <a href="Tables.aspx" class="btn btn-outline-secondary mt-2">Clear Search</a>
    </asp:Panel>

    <!-- Pagination -->
    <asp:Panel ID="pnlPagination" runat="server" CssClass="app-pagination-wrap mt-5">
        <nav class="app-pagination-nav" aria-label="Table pagination">
            <ul class="pagination app-pagination justify-content-center align-items-center mb-0">
                <li class='page-item app-page-arrow <%= CurrentPage <= 1 ? "disabled" : "" %>'>
                    <a class="page-link" href='<%= GetPageUrl(CurrentPage - 1) %>' aria-label="Previous page">
                        <i class="fa-solid fa-chevron-left"></i>
                        <span class="d-none d-sm-inline">Previous</span>
                    </a>
                </li>
                
                <asp:Repeater ID="rptPagination" runat="server">
                    <ItemTemplate>
                        <li class='page-item <%# Convert.ToInt32(Eval("PageNumber")) == CurrentPage ? "active" : "" %>'>
                            <a class="page-link" href='<%# GetPageUrl(Convert.ToInt32(Eval("PageNumber"))) %>'>
                                <%# Eval("PageNumber") %>
                            </a>
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
