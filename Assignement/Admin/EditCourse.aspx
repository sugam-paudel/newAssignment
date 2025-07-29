<%@ Page Title="Edit Course" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditCourse.aspx.cs" Inherits="EduSphere.Admin.EditCourse" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit Course</h2>
    
    <div class="row">
        <div class="col-md-8">
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Course Information</h5>
                </div>
                <div class="card-body">
                    <div class="form-horizontal">
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="CourseIDLabel" CssClass="col-md-3 col-form-label">Course ID</asp:Label>
                            <div class="col-md-9">
                                <asp:Label ID="CourseIDLabel" runat="server" CssClass="form-control-plaintext"></asp:Label>
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="TitleTextBox" CssClass="col-md-3 col-form-label">Title</asp:Label>
                            <div class="col-md-9">
                                <asp:TextBox runat="server" ID="TitleTextBox" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="TitleTextBox"
                                    CssClass="text-danger" ErrorMessage="The title field is required." />
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="DescriptionTextBox" CssClass="col-md-3 col-form-label">Description</asp:Label>
                            <div class="col-md-9">
                                <asp:TextBox runat="server" ID="DescriptionTextBox" TextMode="MultiLine" Rows="5" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="DescriptionTextBox"
                                    CssClass="text-danger" ErrorMessage="The description field is required." />
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="CategoryDropDownList" CssClass="col-md-3 col-form-label">Category</asp:Label>
                            <div class="col-md-9">
                                <asp:DropDownList runat="server" ID="CategoryDropDownList" CssClass="form-select">
                                    <asp:ListItem Text="Programming" Value="Programming" />
                                    <asp:ListItem Text="Web Development" Value="Web Development" />
                                    <asp:ListItem Text="Data Science" Value="Data Science" />
                                    <asp:ListItem Text="Design" Value="Design" />
                                    <asp:ListItem Text="Business" Value="Business" />
                                    <asp:ListItem Text="Marketing" Value="Marketing" />
                                    <asp:ListItem Text="Other" Value="Other" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="DifficultyDropDownList" CssClass="col-md-3 col-form-label">Difficulty</asp:Label>
                            <div class="col-md-9">
                                <asp:DropDownList runat="server" ID="DifficultyDropDownList" CssClass="form-select">
                                    <asp:ListItem Text="Beginner" Value="Beginner" />
                                    <asp:ListItem Text="Intermediate" Value="Intermediate" />
                                    <asp:ListItem Text="Advanced" Value="Advanced" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="DurationTextBox" CssClass="col-md-3 col-form-label">Duration (hours)</asp:Label>
                            <div class="col-md-9">
                                <asp:TextBox runat="server" ID="DurationTextBox" CssClass="form-control" TextMode="Number" min="1" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="DurationTextBox"
                                    CssClass="text-danger" ErrorMessage="The duration field is required." />
                                <asp:RangeValidator runat="server" ControlToValidate="DurationTextBox"
                                    CssClass="text-danger" ErrorMessage="Duration must be a positive number." 
                                    MinimumValue="1" MaximumValue="1000" Type="Integer" />
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="PriceTextBox" CssClass="col-md-3 col-form-label">Price ($)</asp:Label>
                            <div class="col-md-9">
                                <asp:TextBox runat="server" ID="PriceTextBox" CssClass="form-control" TextMode="Number" min="0" step="0.01" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="PriceTextBox"
                                    CssClass="text-danger" ErrorMessage="The price field is required." />
                                <asp:RangeValidator runat="server" ControlToValidate="PriceTextBox"
                                    CssClass="text-danger" ErrorMessage="Price must be a positive number." 
                                    MinimumValue="0" MaximumValue="10000" Type="Double" />
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="CurrentImagePanel" CssClass="col-md-3 col-form-label">Current Image</asp:Label>
                            <div class="col-md-9">
                                <asp:Panel ID="CurrentImagePanel" runat="server" CssClass="mb-2">
                                    <asp:Image ID="CurrentCourseImage" runat="server" CssClass="img-thumbnail" Width="200" />
                                </asp:Panel>
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="ImageFileUpload" CssClass="col-md-3 col-form-label">New Image</asp:Label>
                            <div class="col-md-9">
                                <asp:FileUpload runat="server" ID="ImageFileUpload" CssClass="form-control" accept="image/*" />
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="ImageFileUpload"
                                    CssClass="text-danger" ErrorMessage="Please upload a valid image file (jpg, jpeg, png, gif)."
                                    ValidationExpression="(.*?)\.(jpg|jpeg|png|gif)$" />
                                <small class="form-text text-muted">Leave empty to keep current image</small>
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <asp:Label runat="server" AssociatedControlID="CreatedDateLabel" CssClass="col-md-3 col-form-label">Created Date</asp:Label>
                            <div class="col-md-9">
                                <asp:Label ID="CreatedDateLabel" runat="server" CssClass="form-control-plaintext"></asp:Label>
                            </div>
                        </div>
                        
                        <div class="mb-3 row">
                            <div class="offset-md-3 col-md-9">
                                <asp:Button ID="UpdateCourseButton" runat="server" Text="Update Course" CssClass="btn btn-primary" OnClick="UpdateCourseButton_Click" />
                                <asp:HyperLink runat="server" NavigateUrl="~/Admin/ManageCourses.aspx" CssClass="btn btn-secondary">Cancel</asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>