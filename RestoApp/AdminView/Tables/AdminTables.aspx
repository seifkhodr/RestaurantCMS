<%@ Page Title="Manage Tables" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminTables.aspx.cs" Inherits="RestoApp.AdminView.Tables.AdminTables" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2><i class="fa-solid fa-chair"></i> Manage Tables</h2>
            <a href="<%= ResolveUrl("~/AdminView/dashboard/AdminDashboard.aspx") %>" class="btn btn-outline-secondary"><i class="fa-solid fa-arrow-left"></i> Dashboard</a>
        </div>

        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-info">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </asp:Panel>

        <!-- Add / Edit Form -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0"><asp:Literal ID="litFormTitle" runat="server">Add New Table</asp:Literal></h5>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfTableId" runat="server" Value="0" />
                <div class="row">
                    <div class="col-md-3 mb-3">
                        <label class="form-label">Table Number <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtTableNumber" runat="server" CssClass="form-control" placeholder="e.g. T-01"></asp:TextBox>
                    </div>
                    <div class="col-md-3 mb-3">
                        <label class="form-label">Seating Capacity <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtSeatingCapacity" runat="server" CssClass="form-control" TextMode="Number" min="1" placeholder="4"></asp:TextBox>
                    </div>
                    <div class="col-md-3 mb-3">
                        <label class="form-label">Location</label>
                        <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" placeholder="e.g. Patio"></asp:TextBox>
                    </div>
                    <div class="col-md-3 mb-3">
                        <label class="form-label">Status</label>
                        <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="form-select">
                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="mb-3">
                    <label class="form-label">Photo URL</label>
                    <asp:TextBox ID="txtPhotoPath" runat="server" CssClass="form-control" placeholder="https://..."></asp:TextBox>
                </div>
                <asp:Button ID="btnSave" runat="server" Text="Save Table" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-secondary ms-2" OnClick="btnCancel_Click" Visible="false" />
            </div>
        </div>

        <!-- Tables Grid -->
        <div class="card shadow-sm">
            <div class="card-body">
                <asp:GridView ID="gvTables" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-striped table-hover align-middle" 
                    DataKeyNames="TableId"
                    OnRowCommand="gvTables_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="TableId" HeaderText="ID" ItemStyle-CssClass="fw-bold" />
                        <asp:BoundField DataField="TableNumber" HeaderText="Number" />
                        <asp:BoundField DataField="SeatingCapacity" HeaderText="Capacity" />
                        <asp:BoundField DataField="Location" HeaderText="Location" NullDisplayText="N/A" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <span class='badge <%# Convert.ToBoolean(Eval("IsActive")) ? "bg-success" : "bg-secondary" %>'>
                                    <%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "Inactive" %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" CommandName="EditTable" 
                                    CommandArgument='<%# Eval("TableId") + "|" + Eval("TableNumber") + "|" + Eval("SeatingCapacity") + "|" + Eval("Location") + "|" + Eval("PhotoPath") + "|" + Eval("IsActive") %>' 
                                    Text="Edit" CssClass="btn btn-sm btn-warning me-1" />
                                <asp:Button ID="btnDelete" runat="server" CommandName="DeleteTable" 
                                    CommandArgument='<%# Eval("TableId") %>' 
                                    Text="Delete" CssClass="btn btn-sm btn-danger" 
                                    OnClientClick="return confirm('Are you sure you want to delete this table?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="text-center p-4">
                            <h5 class="text-muted">No tables found. Add one above!</h5>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
