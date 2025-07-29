
<%@ Page Title="Course Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseDetails.aspx.cs" Inherits="EduSphere.Student.CourseDetails" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">
            <div class="card mb-4">
                <div class="card-body">
                    <h2><asp:Label ID="CourseTitleLabel" runat="server"></asp:Label></h2>
                    <p class="text-muted">
                        <asp:Label ID="CourseCategoryLabel" runat="server"></asp:Label> | 
                        <asp:Label ID="CourseDifficultyLabel" runat="server"></asp:Label> | 
                        <asp:Label ID="CourseDurationLabel" runat="server"></asp:Label> hours
                    </p>
                    
                    <div class="mb-3">
                        <asp:Label ID="CourseDescriptionLabel" runat="server"></asp:Label>
                    </div>
                    
                    <div class="mb-3">
                        <h5>Course Progress</h5>
                        <div class="progress">
                            <div class="progress-bar" role="progressbar" style="width: <asp:Literal ID="ProgressLiteral" runat="server" />%" 
                                aria-valuenow="<asp:Literal ID="ProgressValueLiteral" runat="server" />" aria-valuemin="0" aria-valuemax="100">
                                <asp:Literal ID="ProgressTextLiteral" runat="server" />%
                            </div>
                        </div>
                    </div>
                    
                    <div class="mb-3">
                        <asp:Button ID="StartCourseButton" runat="server" Text="Start Course" CssClass="btn btn-primary" OnClick="StartCourseButton_Click" />
                        <asp:Button ID="ContinueCourseButton" runat="server" Text="Continue Course" CssClass="btn btn-primary" OnClick="ContinueCourseButton_Click" />
                    </div>
                </div>
            </div>
            
            <!-- Course Resources Section -->
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Course Resources</h5>
                </div>
                <div class="card-body">
                    <asp:GridView ID="ResourcesGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="Title" HeaderText="Title" />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:BoundField DataField="ResourceType" HeaderText="Type" />
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:HyperLink ID="DownloadLink" runat="server" NavigateUrl='<%# Eval("FilePath") %>' 
                                        CssClass="btn btn-sm btn-outline-primary" Target="_blank">
                                        <i class="fas fa-download"></i> Download
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <p class="text-center">No resources available for this course.</p>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
            
            <!-- Course Discussions Section -->
            <div class="card mb-4">
                <div class="card-header d-flex justify-content-between align-items-center">
                    <h5 class="mb-0">Course Discussions</h5>
                    <asp:Button ID="NewDiscussionButton" runat="server" Text="Start New Discussion" CssClass="btn btn-sm btn-primary" OnClick="NewDiscussionButton_Click" />
                </div>
                <div class="card-body">
                    <asp:GridView ID="DiscussionsGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="Title" HeaderText="Title" />
                            <asp:BoundField DataField="PostedBy" HeaderText="Posted By" />
                            <asp:BoundField DataField="PostedDate" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="ReplyCount" HeaderText="Replies" />
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:HyperLink ID="ViewDiscussionLink" runat="server" 
                                        NavigateUrl='<%# "Discussion.aspx?DiscussionID=" + Eval("DiscussionID") %>' 
                                        CssClass="btn btn-sm btn-outline-primary">
                                        <i class="fas fa-comments"></i> View
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <p class="text-center">No discussions yet. Be the first to start a discussion!</p>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
            
            <!-- Course Assessments Section -->
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Course Assessments</h5>
                </div>
                <div class="card-body">
                    <asp:GridView ID="AssessmentsGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="Title" HeaderText="Title" />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="StatusLabel" runat="server" Text='<%# GetAssessmentStatus(Eval("AssessmentID")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:HyperLink ID="TakeAssessmentLink" runat="server" 
                                        NavigateUrl='<%# "Assessment.aspx?AssessmentID=" + Eval("AssessmentID") %>' 
                                        CssClass="btn btn-sm btn-outline-primary">
                                        <i class="fas fa-tasks"></i> Take Assessment
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <p class="text-center">No assessments available for this course.</p>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
        
        <div class="col-md-4">
            <!-- Course Sidebar -->
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Course Information</h5>
                </div>
                <div class="card-body">
                    <ul class="list-group list-group-flush">
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            Created By
                            <span><asp:Label ID="CreatedByLabel" runat="server"></asp:Label></span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            Created Date
                            <span><asp:Label ID="CreatedDateLabel" runat="server"></asp:Label></span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            Price
                            <span><asp:Label ID="PriceLabel" runat="server"></asp:Label></span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            Enrolled On
                            <span><asp:Label ID="EnrolledDateLabel" runat="server"></asp:Label></span>
                        </li>
                    </ul>
                </div>
            </div>
            
            <!-- Course Modules -->
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Course Modules</h5>
                </div>
                <div class="card-body">
                    <asp:Repeater ID="ModulesRepeater" runat="server">
                        <ItemTemplate>
                            <div class="card mb-2">
                                <div class="card-body">
                                    <h6 class="card-title"><%# Eval("Title") %></h6>
                                    <p class="card-text"><%# Eval("Description") %></p>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="NoModulesPanel" runat="server" Visible="false">
                        <p class="text-center">No modules available for this course.</p>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>