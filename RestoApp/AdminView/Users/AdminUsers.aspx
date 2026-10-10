<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminUsers.aspx.cs" Inherits="RestoApp.AdminView.Users.AdminUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2><i class="fa-solid fa-user-gear"></i> Manage Users</h2>
            <a href="<%= ResolveUrl("~/AdminView/dashboard/AdminDashboard.aspx") %>" class="btn btn-outline-secondary"><i class="fa-solid fa-arrow-left"></i> Dashboard</a>
        </div>

        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-info">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </asp:Panel>

        <div class="card shadow-sm">
            <div class="card-body">
                <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-striped table-hover align-middle" 
                    DataKeyNames="UserId"
                    OnRowCommand="gvUsers_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="UserId" HeaderText="ID" ItemStyle-CssClass="fw-bold" />
                        <asp:BoundField DataField="FullName" HeaderText="Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" NullDisplayText="N/A" />
                        <asp:TemplateField HeaderText="Role">
                            <ItemTemplate>
                                <span class='badge <%# Eval("Role").ToString() == "Admin" ? "bg-danger" : "bg-primary" %>'>
                                    <%# Eval("Role") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified">
                            <ItemTemplate>
                                <span class='badge <%# Convert.ToBoolean(Eval("IsEmailVerified")) ? "bg-success" : "bg-secondary" %>'>
                                    <%# Convert.ToBoolean(Eval("IsEmailVerified")) ? "Yes" : "No" %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <span class='badge <%# Convert.ToBoolean(Eval("IsActive")) ? "bg-success" : "bg-secondary" %>'>
                                    <%# Convert.ToBoolean(Eval("IsActive")) ? "Yes" : "No" %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreatedAt" HeaderText="Joined" DataFormatString="{0:MMM dd, yyyy}" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="btn-group" role="group">
                                    <asp:Button ID="btnMakeAdmin" runat="server" CommandName="MakeAdmin" 
                                        CommandArgument='<%# Eval("UserId") %>' 
                                        Text="Make Admin" CssClass="btn btn-sm btn-outline-danger" 
                                        Visible='<%# Eval("Role").ToString() != "Admin" %>' 
                                        OnClientClick="return confirm('Are you sure you want to promote this user to Admin?');" />
                                    <asp:Button ID="btnMakeClient" runat="server" CommandName="MakeClient" 
                                        CommandArgument='<%# Eval("UserId") %>' 
                                        Text="Make Client" CssClass="btn btn-sm btn-outline-primary" 
                                        Visible='<%# Eval("Role").ToString() == "Admin" %>' />
                                    <asp:Button ID="btnToggleActive" runat="server" 
                                        CommandName='<%# Convert.ToBoolean(Eval("IsActive")) ? "Deactivate" : "Activate" %>' 
                                        CommandArgument='<%# Eval("UserId") %>' 
                                        Text='<%# Convert.ToBoolean(Eval("IsActive")) ? "Deactivate" : "Activate" %>' 
                                        CssClass='<%# Convert.ToBoolean(Eval("IsActive")) ? "btn btn-sm btn-outline-secondary" : "btn btn-sm btn-outline-success" %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="text-center p-4">
                            <h5 class="text-muted">No users found.</h5>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
