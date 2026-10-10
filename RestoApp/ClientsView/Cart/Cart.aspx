<%@ Page Title="Your Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="RestoApp.ClientsView.CartPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Shopping Cart
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <h2 class="fw-bold mb-4"><i class="fa-solid fa-cart-shopping"></i> Your Cart</h2>

        <asp:Panel ID="pnlCartItems" runat="server">
            <div class="table-responsive">
                <table class="table table-hover align-middle">
                    <thead class="table-dark">
                        <tr>
                            <th>Item Name</th>
                            <th>Unit Price</th>
                            <th class="text-center">Quantity</th>
                            <th class="text-end">Total Price</th>
                            <th class="text-center">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptCartItems" runat="server" OnItemCommand="rptCartItems_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td class="fw-bold"><%# Eval("ItemName") %></td>
                                    <td>$<%# Eval("Price", "{0:0.00}") %></td>
                                    <td class="text-center"><%# Eval("Quantity") %></td>
                                    <td class="text-end fw-bold text-success">$<%# Eval("TotalPrice", "{0:0.00}") %></td>
                                    <td class="text-center">
                                        <asp:LinkButton ID="btnRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("ItemId") %>' CssClass="btn btn-sm btn-outline-danger">
                                            <i class="fa-solid fa-trash"></i> Remove
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>

            <!-- Total and Checkout Section -->
            <div class="row justify-content-end mt-4">
                <div class="col-md-6 col-lg-4">
                    <div class="card bg-light border-0 shadow-sm">
                        <div class="card-body">
                            <div class="d-flex justify-content-between mb-3">
                                <h5>Subtotal:</h5>
                                <h5><asp:Literal ID="litSubtotal" runat="server"></asp:Literal></h5>
                            </div>
                            <div class="d-flex justify-content-between mb-3 border-bottom pb-3">
                                <h5>Tax (8%):</h5>
                                <h5><asp:Literal ID="litTax" runat="server"></asp:Literal></h5>
                            </div>
                            <div class="d-flex justify-content-between mb-4">
                                <h3 class="fw-bold text-dark">Total:</h3>
                                <h3 class="fw-bold text-success"><asp:Literal ID="litTotal" runat="server"></asp:Literal></h3>
                            </div>
                            <!-- PLACEHOLDER CHECKOUT BUTTON -->
                            <div class="d-grid">
                                <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Checkout" CssClass="btn btn-success btn-lg" OnClick="btnCheckout_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Empty Cart Panel -->
        <asp:Panel ID="pnlEmptyCart" runat="server" Visible="false" CssClass="text-center py-5">
            <i class="fa-solid fa-cart-arrow-down fa-4x text-muted mb-3"></i>
            <h3>Your cart is empty!</h3>
            <p class="text-muted">Looks like you haven't added any delicious items yet.</p>
            <a href="../Menu/Menu.aspx" class="btn btn-primary mt-3">Browse Menu</a>
        </asp:Panel>
    </div>
</asp:Content>
