<%@ Page Title="Item Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemDetails.aspx.cs" Inherits="RestoApp.ClientsView.Items.ItemDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Item Details
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
        .price-tag {
            font-size: 2rem;
            color: #198754;
            font-weight: bold;
        }
        .origin-badge {
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
            <a href="../Menu/Menu.aspx" class="text-decoration-none text-secondary"><i class="fa-solid fa-arrow-left"></i> Back to Menu</a>
        </div>

        <!-- Main Details -->
        <asp:Panel ID="pnlDetails" runat="server" CssClass="row align-items-center">
            
            <div class="col-lg-6 mb-4 mb-lg-0">
                <asp:Image ID="imgMain" runat="server" CssClass="detail-img" />
            </div>
            
            <div class="col-lg-6">
                <div class="mb-2">
                    <span class="badge bg-dark"><asp:Literal ID="litCategory" runat="server"></asp:Literal></span>
                </div>
                <h1 class="display-5 fw-bold mb-3"><asp:Literal ID="litItemName" runat="server"></asp:Literal></h1>
                
                <div class="d-flex align-items-center mb-4">
                    <div class="price-tag me-4"><asp:Literal ID="litCurrency" runat="server"></asp:Literal><asp:Literal ID="litPrice" runat="server"></asp:Literal></div>
                    <div class="origin-badge"><i class="fa-solid fa-earth-americas"></i> Origin: <asp:Literal ID="litOrigin" runat="server"></asp:Literal></div>
                </div>
                
                <p class="lead text-muted mb-4"><asp:Literal ID="litDescription" runat="server"></asp:Literal></p>
                
                <div class="card bg-light border-0 mb-4">
                    <div class="card-body">
                        <h5 class="card-title fw-bold"><i class="fa-solid fa-basket-shopping"></i> Ingredients</h5>
                        <p class="card-text"><asp:Literal ID="litIngredients" runat="server"></asp:Literal></p>
                    </div>
                </div>
                
                <div class="d-grid gap-2 d-md-flex justify-content-md-start">
                    <asp:LinkButton ID="btnAddToCart" runat="server" CssClass="btn btn-primary btn-lg px-5" OnClick="btnAddToCart_Click"><i class="fa-solid fa-cart-plus"></i> Add to Cart</asp:LinkButton>
                </div>
            </div>
            
        </asp:Panel>

        <!-- Error State -->
        <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="text-center py-5">
            <h2 class="text-danger">Item Not Found</h2>
            <p>The item you are looking for does not exist or is currently unavailable.</p>
        </asp:Panel>

        <!-- Similar Items -->
        <asp:Panel ID="pnlSimilarItems" runat="server" CssClass="mt-5 pt-5 border-top">
            <h3 class="fw-bold mb-4">You might also like</h3>
            <div class="row row-cols-1 row-cols-md-3 g-4">
                <asp:Repeater ID="rptSimilar" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <a href='ItemDetails.aspx?id=<%# Eval("ItemId") %>' class="text-decoration-none text-dark">
                                <div class="card h-100 similar-card border-0 shadow-sm">
                                    <img src='<%# Eval("PrimaryPhotoPath") %>' class="card-img-top similar-img" alt='<%# Eval("ItemName") %>'>
                                    <div class="card-body">
                                        <h6 class="card-title fw-bold mb-1"><%# Eval("ItemName") %></h6>
                                        <div class="text-success fw-bold"><%# Eval("Currency") %><%# Eval("Price", "{0:0.00}") %></div>
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
