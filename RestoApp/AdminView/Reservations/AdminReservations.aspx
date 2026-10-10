<%@ Page Title="Manage Reservations" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminReservations.aspx.cs" Inherits="RestoApp.AdminView.Reservations.AdminReservations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>Manage Reservations</h2>
        </div>

        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert alert-info">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </asp:Panel>

        <div class="card shadow-sm">
            <div class="card-body">
                <asp:GridView ID="gvReservations" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-striped table-hover align-middle" 
                    DataKeyNames="ReservationId"
                    OnRowCommand="gvReservations_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ReservationId" HeaderText="ID" ItemStyle-CssClass="fw-bold" />
                        <asp:BoundField DataField="GuestName" HeaderText="Guest" />
                        <asp:BoundField DataField="TableNumber" HeaderText="Table" />
                        <asp:BoundField DataField="NumberOfGuests" HeaderText="Pax" />
                        <asp:BoundField DataField="ReservationDate" HeaderText="Date & Time" DataFormatString="{0:g}" />
                        
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <span class='badge <%# GetStatusBadgeClass(Eval("Status").ToString()) %>'>
                                    <%# Eval("Status") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="btn-group" role="group">
                                    <asp:Button ID="btnApprove" runat="server" CommandName="ApproveStatus" 
                                        CommandArgument='<%# Eval("ReservationId") %>' 
                                        Text="Approve" CssClass="btn btn-sm btn-success" 
                                        Visible='<%# Eval("Status").ToString() == "Pending" %>' />
                                        
                                    <asp:Button ID="btnCancel" runat="server" CommandName="CancelStatus" 
                                        CommandArgument='<%# Eval("ReservationId") %>' 
                                        Text="Cancel" CssClass="btn btn-sm btn-danger" 
                                        Visible='<%# Eval("Status").ToString() == "Pending" || Eval("Status").ToString() == "Approved" %>' />

                                    <asp:Button ID="btnComplete" runat="server" CommandName="CompleteStatus" 
                                        CommandArgument='<%# Eval("ReservationId") %>' 
                                        Text="Complete" CssClass="btn btn-sm btn-info" 
                                        Visible='<%# Eval("Status").ToString() == "Approved" %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="text-center p-4">
                            <h5 class="text-muted">No reservations found.</h5>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>