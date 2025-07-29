<%@ Page Title="Edit User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="EduSphere.Admin.EditUser" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit User</h2>
    
    <div class="row">
        <div class="col-md-8">
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">User Information</h5>
                </div>
                <div class="card-body">
                    <div class="form-horizontal">
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="UserIDLabel" CssClass="col-md-3 col-form-label">User ID</asp:Label>
                            <div class="col-md-9">
                                <asp:Label ID="UserIDLabel" runat="server" CssClass="form-control-plaintext"></asp:Label>
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="UsernameTextBox" CssClass="col-md-3 col-form-label">Username</asp:Label>
                            <div class="col-md-9">
                                <asp:TextBox runat="server" ID="UsernameTextBox" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="UsernameTextBox"
                                    CssClass="text-danger" ErrorMessage="The username field is required." />
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="EmailTextBox" CssClass="col-md-3 col-form-label">Email</asp:Label>
                            <div class="col-md-9">
                                <asp:TextBox runat="server" ID="EmailTextBox" CssClass="form-control" TextMode="Email" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="EmailTextBox"
                                    CssClass="text-danger" ErrorMessage="The email field is required." />
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="RoleDropDownList" CssClass="col-md-3 col-form-label">Role</asp:Label>
                            <div class="col-md-9">
                                <asp:DropDownList runat="server" ID="RoleDropDownList" CssClass="form-select">
                                    <asp:ListItem Text="Member" Value="Member" />
                                    <asp:ListItem Text="Admin" Value="Admin" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="FirstNameTextBox" CssClass="col-md-3 col-form-label">First Name</asp:Label>
                            <div class="col-md-9">
                                <asp:TextBox runat="server" ID="FirstNameTextBox" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstNameTextBox"
                                    CssClass="text-danger" ErrorMessage="The first name field is required." />
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="LastNameTextBox" CssClass="col-md-3 col-form-label">Last Name</asp:Label>
                            <div class="col-md-9">
                                <asp:TextBox runat="server" ID="LastNameTextBox" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="LastNameTextBox"
                                    CssClass="text-danger" ErrorMessage="The last name field is required." />
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="RegistrationDateLabel" CssClass="col-md-3 col-form-label">Registration Date</asp:Label>
                            <div class="col-md-9">
                                <asp:Label ID="RegistrationDateLabel" runat="server" CssClass="form-control-plaintext"></asp:Label>
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <div class="offset-md-3 col-md-9">
                                <asp:Button ID="UpdateUserButton" runat="server" Text="Update User" CssClass="btn btn-primary" OnClick="UpdateUserButton_Click" />
                                <asp:HyperLink runat="server" NavigateUrl="~/Admin/ManageUsers.aspx" CssClass="btn btn-secondary">Cancel</asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>