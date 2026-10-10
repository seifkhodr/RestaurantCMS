<%@ Page Title="Manage Items" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminItems.aspx.cs" Inherits="RestoApp.AdminView.Items.AdminItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2><i class="fa-solid fa-burger"></i> Manage Items</h2>
            <a href="<%= ResolveUrl("~/AdminView/Dashboard/AdminDashboard.aspx") %>" class="btn btn-outline-secondary"><i class="fa-solid fa-arrow-left"></i> Dashboard</a>
        </div>

        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-info">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </asp:Panel>

        <!-- Add / Edit Form -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0"><asp:Literal ID="litFormTitle" runat="server">Add New Item</asp:Literal></h5>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfItemId" runat="server" Value="0" />
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <label class="form-label">Item Name <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" placeholder="e.g. Grilled Salmon"></asp:TextBox>
                    </div>
                    <div class="col-md-4 mb-3">
                        <label class="form-label">Menu Category <span class="text-danger">*</span></label>
                        <asp:DropDownList ID="ddlMenu" runat="server" CssClass="form-select">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 mb-3">
                        <label class="form-label">Price <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="19.99"></asp:TextBox>
                    </div>
                    <div class="col-md-2 mb-3">
                        <label class="form-label">Currency</label>
                        <asp:TextBox ID="txtCurrency" runat="server" CssClass="form-control" Text="$"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Description</label>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="A brief description..."></asp:TextBox>
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Ingredients</label>
                        <asp:TextBox ID="txtIngredients" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Comma-separated ingredients..."></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <label class="form-label">Origin</label>
                        <asp:TextBox ID="txtOrigin" runat="server" CssClass="form-control" placeholder="e.g. Italian"></asp:TextBox>
                    </div>
                    <div class="col-md-4 mb-3">
                        <label class="form-label">Photo URL (Primary)</label>
                        <asp:TextBox ID="txtPhotoUrl" runat="server" CssClass="form-control" placeholder="https://..."></asp:TextBox>
                    </div>
                    <div class="col-md-4 mb-3">
                        <label class="form-label">Availability</label>
                        <asp:DropDownList ID="ddlIsAvailable" runat="server" CssClass="form-select">
                            <asp:ListItem Value="1" Text="Available"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Unavailable"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <asp:Button ID="btnSave" runat="server" Text="Save Item" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-secondary ms-2" OnClick="btnCancel_Click" Visible="false" />
            </div>
        </div>

        <!-- Items Grid -->
        <div class="card shadow-sm">
            <div class="card-body">
                <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-striped table-hover align-middle" 
                    DataKeyNames="ItemId"
                    OnRowCommand="gvItems_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ItemId" HeaderText="ID" ItemStyle-CssClass="fw-bold" />
                        <asp:BoundField DataField="ItemName" HeaderText="Name" />
                        <asp:BoundField DataField="MenuName" HeaderText="Menu" />
                        <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:0.00}" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <span class='badge <%# Convert.ToBoolean(Eval("IsAvailable")) ? "bg-success" : "bg-secondary" %>'>
                                    <%# Convert.ToBoolean(Eval("IsAvailable")) ? "Available" : "Unavailable" %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" CommandName="EditItem" 
                                    CommandArgument='<%# Eval("ItemId") %>' 
                                    Text="Edit" CssClass="btn btn-sm btn-warning me-1" />
                                <asp:Button ID="btnDelete" runat="server" CommandName="DeleteItem" 
                                    CommandArgument='<%# Eval("ItemId") %>' 
                                    Text="Delete" CssClass="btn btn-sm btn-danger" 
                                    OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="text-center p-4">
                            <h5 class="text-muted">No items found. Add one above!</h5>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
