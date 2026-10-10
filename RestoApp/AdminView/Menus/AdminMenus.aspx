<%@ Page Title="Manage Menus" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminMenus.aspx.cs" Inherits="RestoApp.AdminView.Menus.AdminMenus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2><i class="fa-solid fa-book-open"></i> Manage Menus</h2>
            <a href="<%= ResolveUrl("~/AdminView/dashboard/AdminDashboard.aspx") %>" class="btn btn-outline-secondary"><i class="fa-solid fa-arrow-left"></i> Dashboard</a>
        </div>

        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-info">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </asp:Panel>

        <!-- Add / Edit Form -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0"><asp:Literal ID="litFormTitle" runat="server">Add New Menu</asp:Literal></h5>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfMenuId" runat="server" Value="0" />
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <label class="form-label">Menu Name <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtMenuName" runat="server" CssClass="form-control" placeholder="e.g. Main Course"></asp:TextBox>
                    </div>
                    <div class="col-md-4 mb-3">
                        <label class="form-label">Display Order</label>
                        <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="form-control" TextMode="Number" Text="0"></asp:TextBox>
                    </div>
                    <div class="col-md-4 mb-3">
                        <label class="form-label">Status</label>
                        <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="form-select">
                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="mb-3">
                    <label class="form-label">Description</label>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Optional description..."></asp:TextBox>
                </div>
                <asp:Button ID="btnSave" runat="server" Text="Save Menu" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-secondary ms-2" OnClick="btnCancel_Click" Visible="false" />
            </div>
        </div>

        <!-- Menus Grid -->
        <div class="card shadow-sm">
            <div class="card-body">
                <asp:GridView ID="gvMenus" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-striped table-hover align-middle" 
                    DataKeyNames="MenuId"
                    OnRowCommand="gvMenus_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="MenuId" HeaderText="ID" ItemStyle-CssClass="fw-bold" />
                        <asp:BoundField DataField="MenuName" HeaderText="Name" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="DisplayOrder" HeaderText="Order" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <span class='badge <%# Convert.ToBoolean(Eval("IsActive")) ? "bg-success" : "bg-secondary" %>'>
                                    <%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "Inactive" %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" CommandName="EditMenu" 
                                    CommandArgument='<%# Eval("MenuId") + "|" + Eval("MenuName") + "|" + Eval("Description") + "|" + Eval("DisplayOrder") + "|" + Eval("IsActive") %>' 
                                    Text="Edit" CssClass="btn btn-sm btn-warning me-1" />
                                <asp:Button ID="btnDelete" runat="server" CommandName="DeleteMenu" 
                                    CommandArgument='<%# Eval("MenuId") %>' 
                                    Text="Delete" CssClass="btn btn-sm btn-danger" 
                                    OnClientClick="return confirm('Are you sure you want to delete this menu? All associated items will also be deleted.');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="text-center p-4">
                            <h5 class="text-muted">No menus found. Add one above!</h5>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
