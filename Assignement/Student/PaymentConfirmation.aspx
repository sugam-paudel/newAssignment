<%@ Page Title="Payment Confirmation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentConfirmation.aspx.cs" Inherits="EduSphere.Student.PaymentConfirmation" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card">
                <div class="card-body text-center p-5">
                    <asp:Panel ID="SuccessPanel" runat="server" Visible="false">
                        <i class="fas fa-check-circle fa-5x text-success mb-4"></i>
                        <h2 class="mb-3">Payment Successful!</h2>
                        <p class="lead mb-4">Thank you for your payment. You have successfully enrolled in the course.</p>
                        <div class="alert alert-success mb-4">
                            <p class="mb-1"><strong>Course:</strong> <asp:Label ID="CourseNameLabel" runat="server"></asp:Label></p>
                            <p class="mb-1"><strong>Transaction ID:</strong> <asp:Label ID="TransactionIDLabel" runat="server"></asp:Label></p>
                            <p class="mb-0"><strong>Amount Paid:</strong> <asp:Label ID="AmountPaidLabel" runat="server"></asp:Label></p>
                        </div>
                        <p>A confirmation email has been sent to your registered email address.</p>
                        <div class="d-flex justify-content-center gap-3 mt-4">
                            <asp:Button ID="GoToCourseButton" runat="server" Text="Go to Course" CssClass="btn btn-primary" OnClick="GoToCourseButton_Click" />
                            <asp:Button ID="BrowseCoursesButton" runat="server" Text="Browse More Courses" CssClass="btn btn-outline-primary" OnClick="BrowseCoursesButton_Click" />
                        </div>
                    </asp:Panel>
                    
                    <asp:Panel ID="FailurePanel" runat="server" Visible="false">
                        <i class="fas fa-times-circle fa-5x text-danger mb-4"></i>
                        <h2 class="mb-3">Payment Failed</h2>
                        <p class="lead mb-4">We're sorry, but your payment could not be processed at this time.</p>
                        <div class="alert alert-danger mb-4">
                            <p class="mb-0"><strong>Error:</strong> <asp:Label ID="ErrorMessageLabel" runat="server"></asp:Label></p>
                        </div>
                        <p>Please try again or contact support if the problem persists.</p>
                        <div class="d-flex justify-content-center gap-3 mt-4">
                            <asp:Button ID="RetryPaymentButton" runat="server" Text="Try Again" CssClass="btn btn-primary" OnClick="RetryPaymentButton_Click" />
                            <asp:Button ID="ContactSupportButton" runat="server" Text="Contact Support" CssClass="btn btn-outline-primary" OnClick="ContactSupportButton_Click" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>