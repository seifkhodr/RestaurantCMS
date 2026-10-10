<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verify.aspx.cs" Inherits="RestoApp.Pages.Verify" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Verify Account - RestoApp</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <link href="../Content/css/auth-theme.css?v=20260519-auth1" rel="stylesheet" />
</head>
<body class="auth-verify">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-5">
                <div class="card p-4 text-center">
                    <h3 class="mb-3 font-weight-bold">Account Verification</h3>

                    <form id="form1" runat="server">
                        <asp:Label ID="lblMessage" runat="server" />

                        <a href="Login.aspx" class="btn btn-primary btn-lg w-100 mt-2">Go to Login</a>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <script src="../Content/js/auth-enhance.js?v=20260519-auth1"></script>
</body>
</html>
