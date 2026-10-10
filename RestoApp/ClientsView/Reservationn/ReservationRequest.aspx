<%@ Page Title="Request a Reservation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReservationRequest.aspx.cs" Inherits="RestoApp.ClientsView.Reservationn.ReservationRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row mb-4">
        <div class="col-md-12 text-center text-white p-5 mx-0 rounded" style="background-color: darkolivegreen; border-radius: 12px; box-shadow: 0 4px 6px rgba(0,0,0,0.1);">
            <h1 class="display-4 fw-bold">Book a Table</h1>
            <p class="lead">Fill out the details below to secure your reservation.</p>
        </div>
    </div>

    <div class="row">
        <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger" Visible="false">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </asp:Panel>
        
        <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="false">
            <h4><i class="bi bi-check-circle"></i> Reservation Successful!</h4>
            <asp:Label ID="lblSuccess" runat="server"></asp:Label>
            <div class="mt-3">
                <a href="../Tables/Tables.aspx" class="btn btn-outline-success">Browse More Tables</a>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlForm" runat="server" CssClass="col-md-8 offset-md-2">
            
            <div class="card mb-4 shadow-sm border-0">
                <div class="card-body bg-light rounded d-flex align-items-center">
                    <img id="imgTable" runat="server" class="img-thumbnail me-4" style="width: 150px; height: 100px; object-fit: cover;" src="" alt="Table Picture" />
                    <div>
                        <h4 class="mb-1"><asp:Label ID="lblTableNumber" runat="server" Text="Selected Table"></asp:Label></h4>
                        <p class="text-muted mb-0">Location: <asp:Label ID="lblTableLocation" runat="server" Text="N/A"></asp:Label></p>
                        <p class="text-muted mb-0">Max Capacity: <strong><asp:Label ID="lblTableCapacity" runat="server" Text="0"></asp:Label> Guests</strong></p>
                    </div>
                </div>
            </div>

            <div class="card shadow-sm border-0">
                <div class="card-body p-4">
                    <h5 class="card-title text-success mb-4">Guest Information</h5>
                    
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label for="txtGuestName" class="form-label">Full Name <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtGuestName" runat="server" CssClass="form-control" placeholder="John Doe" Required="true"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label for="txtGuestEmail" class="form-label">Email Address <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtGuestEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="john@example.com" Required="true"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label for="txtGuestPhone" class="form-label">Phone Number <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtGuestPhone" runat="server" CssClass="form-control" placeholder="(555) 123-4567" Required="true"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label for="txtNumberOfGuests" class="form-label">Number of Guests <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtNumberOfGuests" runat="server" CssClass="form-control" TextMode="Number" min="1" placeholder="2" Required="true"></asp:TextBox>
                        </div>
                    </div>

                    <h5 class="card-title text-success mt-5 mb-4">Reservation Details</h5>

                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label for="txtReservationDate" class="form-label">Date <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtReservationDate" runat="server" CssClass="form-control" TextMode="Date" Required="true"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label for="txtReservationTime" class="form-label">Time <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtReservationTime" runat="server" CssClass="form-control" TextMode="Time" Required="true"></asp:TextBox>
                        </div>
                    </div>

                    <div class="mb-4">
                        <label for="txtNotes" class="form-label">Special Requests / Notes</label>
                        <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Allergies, wheelchair access, etc."></asp:TextBox>
                    </div>

                    <div class="d-grid gap-2">
                        <asp:Button ID="btnSubmitReservation" runat="server" Text="Confirm Reservation" CssClass="btn btn-success btn-lg" OnClick="btnSubmitReservation_Click" />
                        <a href="../tables/Tables.aspx" class="btn btn-outline-secondary">Cancel</a>
                    </div>
                </div>
            </div>
            
        </asp:Panel>
    </div>
</asp:Content>