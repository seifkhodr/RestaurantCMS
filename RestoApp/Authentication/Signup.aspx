<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="RestoApp.Pages.Signup" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sign Up - RestoApp</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <link href="../Content/css/auth-theme.css?v=20260519-auth1" rel="stylesheet" />
</head>
<body class="auth-signup">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-5">
                <div class="card p-4">
                    <h3 class="text-center mb-4 font-weight-bold">Create an Account</h3>
                    <form id="form1" runat="server">
                        
                        <div class="mb-3">
                            <label class="form-label text-muted">Full Name</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control form-control-lg" placeholder="John Doe" />
                        </div>

                        <div class="mb-3">
                            <label class="form-label text-muted">Email Address</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-lg" placeholder="name@example.com" TextMode="Email" />
                        </div>

                        <div class="mb-4">
                            <label class="form-label text-muted">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control form-control-lg" TextMode="Password" placeholder="Create password" />
                        </div>

                        <asp:Button ID="btnSignup" runat="server" CssClass="btn btn-success btn-lg w-100 mb-3" Text="Sign Up" OnClick="btnSignup_Click" />

                        <div class="text-center">
                            <asp:Label ID="lblMessage" runat="server" CssClass="small fw-bold" />
                        </div>

                        <hr class="my-4" />
                        <div class="text-center">
                            <span class="text-muted">Already have an account?</span> <a href="Login.aspx" class="text-decoration-none fw-bold">Login</a>
                        </div>

                    </form>
                </div>
            </div>
        </div>
    </div>
    <script src="../Content/js/auth-enhance.js?v=20260519-auth1"></script>
</body>
</html>
