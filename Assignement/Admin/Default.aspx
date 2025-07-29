<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EduSphere.Admin.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Admin Dashboard</h2>
    <p>Welcome, Administrator!</p>
    
    <div class="row">
        <div class="col-md-3">
            <div class="card bg-primary text-white mb-3">
                <div class="card-body">
                    <h5 class="card-title">Total Users</h5>
                    <p class="card-text display-4">
                        <asp:Label ID="TotalUsersLabel" runat="server">0</asp:Label>
                    </p>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card bg-success text-white mb-3">
                <div class="card-body">
                    <h5 class="card-title">Total Courses</h5>
                    <p class="card-text display-4">
                        <asp:Label ID="TotalCoursesLabel" runat="server">0</asp:Label>
                    </p>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card bg-info text-white mb-3">
                <div class="card-body">
                    <h5 class="card-title">Total Resources</h5>
                    <p class="card-text display-4">
                        <asp:Label ID="TotalResourcesLabel" runat="server">0</asp:Label>
                    </p>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card bg-warning text-white mb-3">
                <div class="card-body">
                    <h5 class="card-title">Total Discussions</h5>
                    <p class="card-text display-4">
                        <asp:Label ID="TotalDiscussionsLabel" runat="server">0</asp:Label>
                    </p>
                </div>
            </div>
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-6">
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Quick Actions</h5>
                </div>
                <div class="card-body">
                    <div class="d-grid gap-2">
                        <asp:HyperLink runat="server" NavigateUrl="~/Admin/ManageUsers.aspx" CssClass="btn btn-outline-primary">
                            <i class="fas fa-users me-2"></i> Manage Users
                        </asp:HyperLink>
                        <asp:HyperLink runat="server" NavigateUrl="~/Admin/ManageCourses.aspx" CssClass="btn btn-outline-primary">
                            <i class="fas fa-book me-2"></i> Manage Courses
                        </asp:HyperLink>
                        <asp:HyperLink runat="server" NavigateUrl="~/Admin/AddUser.aspx" CssClass="btn btn-outline-primary">
                            <i class="fas fa-folder-open me-2"></i> Add Users
                        </asp:HyperLink>
                        <asp:HyperLink runat="server" NavigateUrl="~/Admin/AddCourse.aspx" CssClass="btn btn-outline-primary">
                            <i class="fas fa-comments me-2"></i> Add Course
                        </asp:HyperLink>
                        <asp:HyperLink runat="server" NavigateUrl="~/Admin/EditCourse.aspx" CssClass="btn btn-outline-primary">
                            <i class="fas fa-tasks me-2"></i> Edit Course
                        </asp:HyperLink>
                        <asp:HyperLink runat="server" NavigateUrl="~/Admin/EditUser.aspx" CssClass="btn btn-outline-primary">
                        <i class="fas fa-tasks me-2"></i> Edit User
                         </asp:HyperLink>

                    </div>
                </div>
            </div>
        </div>
        
        <div class="col-md-6">
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Recent Activities</h5>
                </div>
                <div class="card-body">
                    <asp:GridView ID="RecentActivitiesGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="Activity" HeaderText="Activity" />
                            <asp:BoundField DataField="User" HeaderText="User" />
                            <asp:BoundField DataField="Date" HeaderText="Date" />
                        </Columns>
                        <EmptyDataTemplate>
                            <p class="text-center">No recent activities found.</p>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>