<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="EduSphere.Admin.ManageUsers" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Users</h2>
    <p>
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/AddUser.aspx" Text="Add New User" CssClass="btn btn-primary" />
    </p>
    
    <div class="card mb-4">
        <div class="card-header">
            <div class="row align-items-center">
                <div class="col-md-6">
                    <h5 class="mb-0">User List</h5>
                </div>
                <div class="col-md-6">
                    <div class="input-group">
                        <asp:TextBox ID="SearchUsersTextBox" runat="server" CssClass="form-control" placeholder="Search users..."></asp:TextBox>
                        <asp:Button ID="SearchUsersButton" runat="server" Text="Search" CssClass="btn btn-outline-secondary" OnClick="SearchUsersButton_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="card-body">
            <asp:GridView ID="UsersGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID" 
                CssClass="table table-striped table-hover" OnRowDeleting="UsersGridView_RowDeleting" 
                OnRowEditing="UsersGridView_RowEditing" OnPageIndexChanging="UsersGridView_PageIndexChanging" 
                AllowPaging="True" PageSize="10">
                <Columns>
                    <asp:BoundField DataField="UserID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Username" HeaderText="Username" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Role" HeaderText="Role" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="RegistrationDate" HeaderText="Registration Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-outline-primary">
                                <i class="fas fa-edit"></i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CssClass="btn btn-sm btn-outline-danger"
                                OnClientClick="return confirm('Are you sure you want to delete this user?');">
                                <i class="fas fa-trash"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <p class="text-center">No users found.</p>
                </EmptyDataTemplate>
                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" />
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>