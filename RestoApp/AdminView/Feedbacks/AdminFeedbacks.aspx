<%@ Page Title="Manage Reviews" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminFeedbacks.aspx.cs" Inherits="RestoApp.AdminView.Feedbacks.AdminFeedbacks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2><i class="fa-solid fa-star"></i> Manage Reviews</h2>
            <a href="<%= ResolveUrl("~/AdminView/Dashboard/AdminDashboard.aspx") %>" class="btn btn-outline-secondary"><i class="fa-solid fa-arrow-left"></i> Dashboard</a>
        </div>

        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-info">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </asp:Panel>

        <div class="card shadow-sm">
            <div class="card-body">
                <asp:GridView ID="gvFeedbacks" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-striped table-hover align-middle" 
                    DataKeyNames="FeedbackId"
                    OnRowCommand="gvFeedbacks_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="FeedbackId" HeaderText="ID" ItemStyle-CssClass="fw-bold" />
                        <asp:BoundField DataField="GuestName" HeaderText="Guest" />
                        <asp:BoundField DataField="VisitRating" HeaderText="Rating" />
                        <asp:TemplateField HeaderText="Comment">
                            <ItemTemplate>
                                <span title='<%# Eval("Comment") %>'>
                                    <%# Eval("Comment").ToString().Length > 80 ? Eval("Comment").ToString().Substring(0, 80) + "..." : Eval("Comment") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreatedAt" HeaderText="Date" DataFormatString="{0:MMM dd, yyyy}" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <span class='badge <%# Convert.ToBoolean(Eval("IsApproved")) ? "bg-success" : "bg-warning text-dark" %>'>
                                    <%# Convert.ToBoolean(Eval("IsApproved")) ? "Approved" : "Pending" %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="btn-group" role="group">
                                    <asp:Button ID="btnApprove" runat="server" CommandName="ApproveFeedback" 
                                        CommandArgument='<%# Eval("FeedbackId") %>' 
                                        Text="Approve" CssClass="btn btn-sm btn-success" 
                                        Visible='<%# !Convert.ToBoolean(Eval("IsApproved")) %>' />
                                    <asp:Button ID="btnReject" runat="server" CommandName="RejectFeedback" 
                                        CommandArgument='<%# Eval("FeedbackId") %>' 
                                        Text="Reject" CssClass="btn btn-sm btn-secondary" 
                                        Visible='<%# Convert.ToBoolean(Eval("IsApproved")) %>' />
                                    <asp:Button ID="btnDelete" runat="server" CommandName="DeleteFeedback" 
                                        CommandArgument='<%# Eval("FeedbackId") %>' 
                                        Text="Delete" CssClass="btn btn-sm btn-danger" 
                                        OnClientClick="return confirm('Are you sure you want to delete this review?');" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="text-center p-4">
                            <h5 class="text-muted">No reviews found.</h5>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
