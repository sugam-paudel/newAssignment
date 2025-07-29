<%@ Page Title="Member Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="EduSphere.Student.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Member Dashboard</h2>
    <p>Welcome back, <asp:LoginName runat="server" />!</p>
    
    <div class="row">
        <div class="col-md-4">
            <div class="card bg-primary text-white mb-3">
                <div class="card-body">
                    <h5 class="card-title">Enrolled Courses</h5>
                    <p class="card-text display-4">
                        <asp:Label ID="EnrolledCoursesLabel" runat="server">0</asp:Label>
                    </p>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="card bg-success text-white mb-3">
                <div class="card-body">
                    <h5 class="card-title">Completed Courses</h5>
                    <p class="card-text display-4">
                        <asp:Label ID="CompletedCoursesLabel" runat="server">0</asp:Label>
                    </p>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="card bg-info text-white mb-3">
                <div class="card-body">
                    <h5 class="card-title">Certificates Earned</h5>
                    <p class="card-text display-4">
                        <asp:Label ID="CertificatesLabel" runat="server">0</asp:Label>
                    </p>
                </div>
            </div>
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-6">
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">My Enrolled Courses</h5>
                </div>
                <div class="card-body">
                    <asp:GridView ID="EnrolledCoursesGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="Title" HeaderText="Course" />
                            <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrolled On" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:TemplateField HeaderText="Progress">
                                <ItemTemplate>
                                    <div class="progress">
                                        <div class="progress-bar" role="progressbar" style="width: <%# Eval("Progress") %>%" aria-valuenow="<%# Eval("Progress") %>" aria-valuemin="0" aria-valuemax="100">
                                            <%# Eval("Progress") %>%
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:HyperLinkField DataNavigateUrlFields="CourseID" 
                                DataNavigateUrlFormatString="CourseDetails.aspx?CourseID={0}" 
                                Text="View Details" />
                        </Columns>
                        <EmptyDataTemplate>
                            <p class="text-center">You are not enrolled in any courses yet.</p>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
        
        <div class="col-md-6">
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Available Courses</h5>
                </div>
                <div class="card-body">
                    <asp:GridView ID="AvailableCoursesGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="Title" HeaderText="Course" />
                            <asp:BoundField DataField="Category" HeaderText="Category" />
                            <asp:BoundField DataField="Difficulty" HeaderText="Difficulty" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Button ID="EnrollButton" runat="server" Text="Enroll" 
                                        CommandArgument='<%# Eval("CourseID") %>' OnClick="EnrollButton_Click" 
                                        CssClass="btn btn-sm btn-primary" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <p class="text-center">No available courses at the moment.</p>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-12">
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Recent Activities</h5>
                </div>
                <div class="card-body">
                    <asp:GridView ID="RecentActivitiesGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="Activity" HeaderText="Activity" />
                            <asp:BoundField DataField="Course" HeaderText="Course" />
                            <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
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