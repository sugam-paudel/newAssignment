<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Assignement.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container mt-5 mb-5">
        <div class="row justify-content-center">
            <div class="col-md-5">
                <h2 class="text-center mb-4">Login to Your Account</h2>

                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />

                <div class="form-group mb-3">
                    <label>Username or Email</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter username or email" />
                </div>

                <div class="form-group mb-4">
                    <label>Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter password" />
                </div>

                <asp:Button ID="btnLogin" runat="server" Text="Login"
                    CssClass="btn btn-success w-100"
                    OnClick="btnLogin_Click" />

                <p class="text-center mt-3">
                    Don't have an account? <a href="Register.aspx">Register here</a>
                </p>
            </div>
        </div>
    </main>
</asp:Content>
