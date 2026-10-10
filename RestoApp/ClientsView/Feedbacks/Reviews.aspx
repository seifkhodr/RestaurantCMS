<%@ Page Title="Customer Reviews" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reviews.aspx.cs" Inherits="RestoApp.ClientsView.Feedbacks.Reviews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <div class="row text-center mb-5">
            <div class="col-12">
                <h1 class="display-4 fw-bold">Customer Reviews</h1>
                <p class="lead text-muted">See what our guests are saying or share your own experience!</p>
            </div>
        </div>

        <div class="row">
            <!-- Reviews List -->
            <div class="col-md-8 mb-4">
                <h4 class="mb-4">Recent Feedback</h4>
                <asp:Repeater ID="rptReviews" runat="server">
                    <ItemTemplate>
                        <div class="card mb-3 shadow-sm border-0">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center mb-2">
                                    <h5 class="card-title mb-0 fw-bold">
                                        <i class="fa-solid fa-user-circle text-secondary me-2"></i>
                                        <%# Eval("GuestName") %>
                                    </h5>
                                    <span class="text-muted small"><%# Convert.ToDateTime(Eval("CreatedAt")).ToString("MMM dd, yyyy") %></span>
                                </div>
                                <div class="mb-3 text-warning">
                                    <%# GetStarsHtml(Convert.ToInt32(Eval("VisitRating"))) %>
                                </div>
                                <p class="card-text"><%# Eval("Comment") %></p>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblEmptyData" runat="server" Visible='<%# rptReviews.Items.Count == 0 %>' 
                                   Text="<div class='alert alert-light text-center'>Be the first to leave a review!</div>">
                        </asp:Label>
                    </FooterTemplate>
                </asp:Repeater>
            </div>

            <!-- Submit Review Form -->
            <div class="col-md-4">
                <div class="card shadow-sm border-0 bg-light">
                    <div class="card-body">
                        <h4 class="card-title mb-4">Leave Feedback</h4>
                        
                        <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="false">
                            <i class="bi bi-check-circle"></i> Thank you! Your feedback has been submitted and is pending moderation.
                        </asp:Panel>

                        <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger" Visible="false">
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </asp:Panel>

                        <asp:Panel ID="pnlForm" runat="server">
                            <div class="mb-3">
                                <label for="txtGuestName" class="form-label">Your Name (Optional)</label>
                                <asp:TextBox ID="txtGuestName" runat="server" CssClass="form-control" placeholder="Anonymous"></asp:TextBox>
                            </div>
                            
                            <div class="mb-3">
                                <label class="form-label">Rating <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlRating" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="5" Text="5 - Excellent"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4 - Very Good"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3 - Average"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2 - Poor"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1 - Terrible"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="mb-3">
                                <label for="txtComment" class="form-label">Comment <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="How was your visit?"></asp:TextBox>
                            </div>

                            <div class="d-grid">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit Review" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>