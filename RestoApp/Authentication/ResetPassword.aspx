<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="RestoApp.Pages.ResetPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Reset Password - RestoApp</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <link href="../Content/css/auth-theme.css?v=20260519-auth1" rel="stylesheet" />
</head>
<body class="auth-reset">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-5">
                <div class="card p-4">
                    <h3 class="text-center mb-2 font-weight-bold">Reset Password</h3>
                    <p class="text-center text-muted small mb-4">Please enter your new password below.</p>
                    
                    <form id="form1" runat="server">
                        
                        <div class="text-center mb-3">
                            <asp:Label ID="lblMessage" runat="server" CssClass="small fw-bold"></asp:Label>
                        </div>
                        
                        <div class="mb-4">
                            <label class="form-label text-muted">New Password</label>
                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control form-control-lg" TextMode="Password" placeholder="New password"></asp:TextBox>
                        </div>

                        <div class="mb-4">
                            <label class="form-label text-muted">Confirm Password</label>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control form-control-lg" TextMode="Password" placeholder="Confirm password"></asp:TextBox>
                        </div>
                         
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-lg w-100" Text="Reset Password" OnClick="btnReset_Click" />
                    </form>
                </div>
            </div>
        </div>
    </div>
    <script src="../Content/js/auth-enhance.js?v=20260519-auth1"></script>
</body>
</html>
