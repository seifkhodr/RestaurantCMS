<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="RestoApp.Pages.ForgotPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Forgot Password - RestoApp</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <link href="../Content/css/auth-theme.css?v=20260519-auth1" rel="stylesheet" />
</head>
<body class="auth-forgot">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-5">
                <div class="card p-4">
                    <h3 class="text-center mb-2 font-weight-bold">Forgot Password</h3>
                    <p class="text-center text-muted small mb-4">Enter your email address to receive a password reset link.</p>
                    
                    <form id="form1" runat="server">
                        
                        <div class="mb-4">
                            <label class="form-label text-muted">Email Address</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-lg" TextMode="Email" placeholder="name@example.com"></asp:TextBox>
                        </div>
                        
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-warning btn-lg w-100 text-white mb-3" Text="Send Reset Link" OnClick="btnSubmit_Click" />
                        
                        <div class="text-center">
                            <asp:Label ID="lblMessage" runat="server" CssClass="small fw-bold"></asp:Label>
                        </div>

                        <hr class="my-4" />
                        <div class="text-center">
                            <a href="Login.aspx" class="text-decoration-none fw-bold text-secondary">&larr; Back to Login</a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <script src="../Content/js/auth-enhance.js?v=20260519-auth1"></script>
</body>
</html>
