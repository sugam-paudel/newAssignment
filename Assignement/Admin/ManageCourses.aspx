<%@ Page Title="Manage Courses" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" Inherits="EduSphere.Admin.ManageCourses" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Courses</h2>
    <p>
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/AddCourse.aspx" Text="Add New Course" CssClass="btn btn-primary" />
    </p>
    
    <div class="card mb-4">
        <div class="card-header">
            <div class="row align-items-center">
                <div class="col-md-6">
                    <h5 class="mb-0">Course List</h5>
                </div>
                <div class="col-md-6">
                    <div class="input-group">
                        <asp:TextBox ID="SearchCoursesTextBox" runat="server" CssClass="form-control" placeholder="Search courses..."></asp:TextBox>
                        <asp:Button ID="SearchCoursesButton" runat="server" Text="Search" CssClass="btn btn-outline-secondary" OnClick="SearchCoursesButton_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="card-body">
            <asp:GridView ID="CoursesGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="CourseID" 
                CssClass="table table-striped table-hover" OnRowDeleting="CoursesGridView_RowDeleting" 
                OnRowEditing="CoursesGridView_RowEditing" OnPageIndexChanging="CoursesGridView_PageIndexChanging" 
                AllowPaging="True" PageSize="10">
                <Columns>
                    <asp:BoundField DataField="CourseID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-outline-primary">
                                <i class="fas fa-edit"></i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" CssClass="btn btn-sm btn-outline-danger"
                                OnClientClick="return confirm('Are you sure you want to delete this course?');">
                                <i class="fas fa-trash"></i>
                            </asp:LinkButton>
                            <asp:HyperLink ID="ManageResourcesLink" runat="server" NavigateUrl='<%# "ManageResources.aspx?CourseID=" + Eval("CourseID") %>' 
                                CssClass="btn btn-sm btn-outline-info">
                                <i class="fas fa-folder-open"></i>
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <p class="text-center">No courses found.</p>
                </EmptyDataTemplate>
                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" />
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>