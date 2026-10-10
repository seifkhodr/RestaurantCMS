<%@ Page Title="Table Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TableDetails.aspx.cs" Inherits="RestoApp.ClientsView.TableDetails.TableDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Table Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .detail-img {
            width: 100%;
            height: 400px;
            object-fit: cover;
            border-radius: 10px;
            box-shadow: 0 10px 20px rgba(0,0,0,0.1);
        }
        .capacity-tag {
            font-size: 2rem;
            color: #0dcaf0;
            font-weight: bold;
        }
        .location-badge {
            background-color: #f8f9fa;
            border: 1px solid #dee2e6;
            padding: 5px 15px;
            border-radius: 20px;
            font-size: 0.9rem;
            color: #6c757d;
        }
        .similar-card {
            transition: transform 0.3s;
        }
        .similar-card:hover {
            transform: translateY(-5px);
        }
        .similar-img {
            height: 180px;
            object-fit: cover;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        
        <div class="mb-4">
            <a href="../tables/Tables.aspx" class="text-decoration-none text-secondary"><i class="fa-solid fa-arrow-left"></i> Back to Tables</a>
        </div>

        <!-- Main Details -->
        <asp:Panel ID="pnlDetails" runat="server" CssClass="row align-items-center">
            
            <div class="col-lg-6 mb-4 mb-lg-0">
                <asp:Image ID="imgMain" runat="server" CssClass="detail-img" />
            </div>
            
            <div class="col-lg-6">
                <div class="mb-2">
                    <span class="badge bg-dark"><asp:Literal ID="litLocationBadge" runat="server"></asp:Literal></span>
                </div>
                <h1 class="display-5 fw-bold mb-3">Table <asp:Literal ID="litTableNumber" runat="server"></asp:Literal></h1>
                
                <div class="d-flex align-items-center mb-4">
                    <div class="capacity-tag me-4"><i class="fa-solid fa-users"></i> <asp:Literal ID="litCapacity" runat="server"></asp:Literal> Seats</div>
                    <div class="location-badge"><i class="fa-solid fa-location-dot"></i> <asp:Literal ID="litLocation" runat="server"></asp:Literal></div>
                </div>
                
                <p class="lead text-muted mb-4">A perfect spot in our <asp:Literal ID="litLocationDesc" runat="server"></asp:Literal> area allowing up to <asp:Literal ID="litCapacityDesc" runat="server"></asp:Literal> guests to enjoy an unforgettable dining experience.</p>
                
                <div class="d-grid gap-2 d-md-flex justify-content-md-start">
                    <a href="../Reservationn/ReservationRequest.aspx?table=<%= CurrentTableId %>" class="btn btn-primary btn-lg px-5">
                        <i class="fa-solid fa-calendar-check"></i> Book Now
                    </a>
                </div>
            </div>
            
        </asp:Panel>

        <!-- Error State -->
        <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="text-center py-5">
            <h2 class="text-danger">Table Not Found</h2>
            <p>The table you are looking for does not exist or is currently unavailable.</p>
        </asp:Panel>
        
        <!-- Similar Tables -->
        <asp:Panel ID="pnlSimilarTables" runat="server" CssClass="mt-5 pt-5 border-top">
            <h3 class="fw-bold mb-4">Other Tables You Might Like</h3>
            <div class="row row-cols-1 row-cols-md-3 g-4">
                <asp:Repeater ID="rptSimilar" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <a href='TableDetails.aspx?id=<%# Eval("TableId") %>' class="text-decoration-none text-dark">
                                <div class="card h-100 similar-card border-0 shadow-sm">
                                    <asp:PlaceHolder runat="server" Visible='<%# !string.IsNullOrEmpty(Eval("PhotoPath")?.ToString()) %>'>
                                        <img src='<%# Eval("PhotoPath") %>' class="card-img-top similar-img" alt='Table <%# Eval("TableNumber") %>'>
                                    </asp:PlaceHolder>
                                    <asp:PlaceHolder runat="server" Visible='<%# string.IsNullOrEmpty(Eval("PhotoPath")?.ToString()) %>'>
                                        <div class="card-img-top bg-light" style="height:180px; display:flex; align-items:center; justify-content:center; color:#adb5bd; font-size:3rem;">
                                            <i class="fa-solid fa-chair"></i>
                                        </div>
                                    </asp:PlaceHolder>
                                    <div class="card-body">
                                        <h6 class="card-title fw-bold mb-1">Table <%# Eval("TableNumber") %></h6>
                                        <div class="text-info fw-bold"><i class="fa-solid fa-users"></i> <%# Eval("SeatingCapacity") %> Seats</div>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>

    </div>
</asp:Content>