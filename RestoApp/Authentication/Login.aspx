<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RestoApp.Pages.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login - RestoApp</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <link href="../Content/css/auth-theme.css?v=20260519-auth1" rel="stylesheet" />
</head>
<body class="auth-login">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-5">
                <div class="card p-4">
                    <h3 class="text-center mb-4 font-weight-bold">Welcome Back</h3>
                    <form id="form1" runat="server">
                        
                        <div class="mb-3">
                            <label class="form-label text-muted">Email Address</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-lg" placeholder="name@example.com" TextMode="Email" />
                        </div>

                        <div class="mb-3">
                            <label class="form-label text-muted">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control form-control-lg" TextMode="Password" placeholder="Enter password" />
                        </div>

                        <div class="d-flex justify-content-between align-items-center mb-4">
                            <div class="form-check">
                                <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="form-check-input" OnCheckedChanged="chkRememberMe_CheckedChanged" />
                                <label class="form-check-label text-muted" for="chkRememberMe">Remember me</label>
                            </div>
                            <a href="ForgotPassword.aspx" class="text-decoration-none small">Forgot Password?</a>
                        </div>

                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btn-lg w-100 mb-3" Text="Login" OnClick="btnLogin_Click" />

                        <div class="text-center">
                            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger small fw-bold" />
                        </div>

                        <hr class="my-4" />
                        <div class="text-center">
                            <span class="text-muted">Don't have an account?</span> <a href="Signup.aspx" class="text-decoration-none fw-bold">Sign up</a>
                        </div>

                    </form>
                </div>
            </div>
        </div>
    </div>
    <script src="../Content/js/auth-enhance.js?v=20260519-auth1"></script>
</body>
</html>
