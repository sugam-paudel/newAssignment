<%@ Page Title="My Course" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EnrolledCourse.aspx.cs" Inherits="EduSphere.Student.EnrolledCourse" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">
            <div class="card mb-4">
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h2><asp:Label ID="CourseTitleLabel" runat="server"></asp:Label></h2>
                        <span class="badge bg-success">Enrolled</span>
                    </div>
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
                        <asp:Button ID="ContinueCourseButton" runat="server" Text="Continue Course" CssClass="btn btn-primary" OnClick="ContinueCourseButton_Click" />
                        <asp:Button ID="ViewCertificateButton" runat="server" Text="View Certificate" CssClass="btn btn-success" OnClick="ViewCertificateButton_Click" Visible="false" />
                    </div>
                </div>
            </div>
            
            <!-- Course Content Section -->
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Course Content</h5>
                </div>
                <div class="card-body">
                    <asp:Repeater ID="ModulesRepeater" runat="server" OnItemDataBound="ModulesRepeater_ItemDataBound">
                        <ItemTemplate>
                            <div class="accordion mb-2" id="moduleAccordion<%# Eval("ModuleID") %>">
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="heading<%# Eval("ModuleID") %>">
                                        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapse<%# Eval("ModuleID") %>" aria-expanded="true" aria-controls="collapse<%# Eval("ModuleID") %>">
                                            <%# Eval("Title") %> 
                                            <span class="badge bg-secondary ms-2"><%# Eval("Duration") %> min</span>
                                        </button>
                                    </h2>
                                    <div id="collapse<%# Eval("ModuleID") %>" class="accordion-collapse collapse show" aria-labelledby="heading<%# Eval("ModuleID") %>" data-bs-parent="#moduleAccordion<%# Eval("ModuleID") %>">
                                        <div class="accordion-body">
                                            <p><%# Eval("Description") %></p>
                                            <asp:Repeater ID="LessonsRepeater" runat="server">
                                                <ItemTemplate>
                                                    <div class="d-flex justify-content-between align-items-center mb-2 p-2 bg-light rounded">
                                                        <div>
                                                            <i class="fas fa-play-circle me-2"></i>
                                                            <%# Eval("Title") %>
                                                        </div>
                                                        <div>
                                                            <span class="badge bg-info"><%# Eval("Duration") %> min</span>
                                                            <asp:HyperLink ID="LessonLink" runat="server" CssClass="btn btn-sm btn-outline-primary ms-2">
                                                                <i class="fas fa-play"></i> Start
                                                            </asp:HyperLink>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="NoContentPanel" runat="server" Visible="false">
                        <p class="text-center">No course content available yet.</p>
                    </asp:Panel>
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
                            Instructor
                            <span><asp:Label ID="InstructorLabel" runat="server"></asp:Label></span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            Enrolled On
                            <span><asp:Label ID="EnrolledDateLabel" runat="server"></asp:Label></span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            Last Accessed
                            <span><asp:Label ID="LastAccessedLabel" runat="server"></asp:Label></span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            Completion
                            <span><asp:Label ID="CompletionLabel" runat="server"></asp:Label></span>
                        </li>
                    </ul>
                </div>
            </div>
            
            <!-- Upcoming Events -->
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Upcoming Events</h5>
                </div>
                <div class="card-body">
                    <asp:Repeater ID="EventsRepeater" runat="server">
                        <ItemTemplate>
                            <div class="d-flex mb-3">
                                <div class="me-3">
                                    <div class="bg-primary text-white text-center rounded p-2" style="width: 60px;">
                                        <div><%# Convert.ToDateTime(Eval("EventDate")).ToString("MMM") %></div>
                                        <div class="fs-5"><%# Convert.ToDateTime(Eval("EventDate")).ToString("dd") %></div>
                                    </div>
                                </div>
                                <div>
                                    <h6><%# Eval("Title") %></h6>
                                    <p class="text-muted small mb-0"><%# Eval("Time") %> | <%# Eval("Location") %></p>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="NoEventsPanel" runat="server" Visible="false">
                        <p class="text-center">No upcoming events for this course.</p>
                    </asp:Panel>
                </div>
            </div>
            
            <!-- Course Certificate -->
            <div class="card">
                <div class="card-header">
                    <h5 class="mb-0">Course Certificate</h5>
                </div>
                <div class="card-body text-center">
                    <i class="fas fa-certificate fa-4x text-warning mb-3"></i>
                    <p>Complete all course modules to earn your certificate</p>
                    <asp:Button ID="RequestCertificateButton" runat="server" Text="Request Certificate" CssClass="btn btn-outline-warning" OnClick="RequestCertificateButton_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>