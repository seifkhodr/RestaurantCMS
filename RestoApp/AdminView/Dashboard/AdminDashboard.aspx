<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="RestoApp.AdminView.Dashboard.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2><i class="fa-solid fa-gauge-high"></i> Admin Dashboard</h2>
        </div>

        <!-- Stats Cards -->
        <div class="row mb-4">
            <div class="col-md-3 mb-3">
                <div class="card border-0 shadow-sm bg-primary text-white">
                    <div class="card-body text-center">
                        <i class="fa-solid fa-users fa-2x mb-2"></i>
                        <h3 class="fw-bold"><asp:Literal ID="litUserCount" runat="server">0</asp:Literal></h3>
                        <p class="mb-0">Total Users</p>
                    </div>
                </div>
            </div>
            <div class="col-md-3 mb-3">
                <div class="card border-0 shadow-sm bg-success text-white">
                    <div class="card-body text-center">
                        <i class="fa-solid fa-calendar-check fa-2x mb-2"></i>
                        <h3 class="fw-bold"><asp:Literal ID="litReservationCount" runat="server">0</asp:Literal></h3>
                        <p class="mb-0">Reservations</p>
                    </div>
                </div>
            </div>
            <div class="col-md-3 mb-3">
                <div class="card border-0 shadow-sm bg-warning text-dark">
                    <div class="card-body text-center">
                        <i class="fa-solid fa-utensils fa-2x mb-2"></i>
                        <h3 class="fw-bold"><asp:Literal ID="litItemCount" runat="server">0</asp:Literal></h3>
                        <p class="mb-0">Menu Items</p>
                    </div>
                </div>
            </div>
            <div class="col-md-3 mb-3">
                <div class="card border-0 shadow-sm bg-info text-dark">
                    <div class="card-body text-center">
                        <i class="fa-solid fa-comments fa-2x mb-2"></i>
                        <h3 class="fw-bold"><asp:Literal ID="litFeedbackCount" runat="server">0</asp:Literal></h3>
                        <p class="mb-0">Feedbacks</p>
                    </div>
                </div>
            </div>
        </div>

        <!-- Quick Links -->
        <div class="row">
            <div class="col-md-4 mb-3">
                <a href="<%= ResolveUrl("~/AdminView/Reservations/AdminReservations.aspx") %>" class="text-decoration-none">
                    <div class="card border-0 shadow-sm h-100">
                        <div class="card-body text-center p-4">
                            <i class="fa-solid fa-calendar-days fa-3x text-success mb-3"></i>
                            <h5 class="fw-bold">Manage Reservations</h5>
                            <p class="text-muted mb-0">Approve, cancel, or complete guest bookings.</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-4 mb-3">
                <a href="<%= ResolveUrl("~/AdminView/Menus/AdminMenus.aspx") %>" class="text-decoration-none">
                    <div class="card border-0 shadow-sm h-100">
                        <div class="card-body text-center p-4">
                            <i class="fa-solid fa-book-open fa-3x text-primary mb-3"></i>
                            <h5 class="fw-bold">Manage Menus</h5>
                            <p class="text-muted mb-0">Create, edit, and organize menu categories.</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-4 mb-3">
                <a href="<%= ResolveUrl("~/AdminView/Items/AdminItems.aspx") %>" class="text-decoration-none">
                    <div class="card border-0 shadow-sm h-100">
                        <div class="card-body text-center p-4">
                            <i class="fa-solid fa-burger fa-3x text-warning mb-3"></i>
                            <h5 class="fw-bold">Manage Items</h5>
                            <p class="text-muted mb-0">Add, update, or remove menu items.</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-4 mb-3">
                <a href="<%= ResolveUrl("~/AdminView/Tables/AdminTables.aspx") %>" class="text-decoration-none">
                    <div class="card border-0 shadow-sm h-100">
                        <div class="card-body text-center p-4">
                            <i class="fa-solid fa-chair fa-3x text-secondary mb-3"></i>
                            <h5 class="fw-bold">Manage Tables</h5>
                            <p class="text-muted mb-0">Configure restaurant table layout.</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-4 mb-3">
                <a href="<%= ResolveUrl("~/AdminView/Feedbacks/AdminFeedbacks.aspx") %>" class="text-decoration-none">
                    <div class="card border-0 shadow-sm h-100">
                        <div class="card-body text-center p-4">
                            <i class="fa-solid fa-star fa-3x text-info mb-3"></i>
                            <h5 class="fw-bold">Manage Reviews</h5>
                            <p class="text-muted mb-0">Moderate and approve customer reviews.</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-4 mb-3">
                <a href="<%= ResolveUrl("~/AdminView/Users/AdminUsers.aspx") %>" class="text-decoration-none">
                    <div class="card border-0 shadow-sm h-100">
                        <div class="card-body text-center p-4">
                            <i class="fa-solid fa-user-gear fa-3x text-danger mb-3"></i>
                            <h5 class="fw-bold">Manage Users</h5>
                            <p class="text-muted mb-0">View accounts and manage user roles.</p>
                        </div>
                    </div>
                </a>
            </div>
        </div>
    </div>
</asp:Content>
