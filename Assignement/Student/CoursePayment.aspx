<%@ Page Title="Course Payment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CoursePayment.aspx.cs" Inherits="EduSphere.Student.CoursePayment" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">
            <div class="card mb-4">
                <div class="card-header">
                    <h4 class="mb-0">Course Payment</h4>
                </div>
                <div class="card-body">
                    <div class="alert alert-info">
                        <h5>Course: <asp:Label ID="CourseTitleLabel" runat="server"></asp:Label></h5>
                        <p>Complete your payment to enroll in this course</p>
                    </div>
                    
                    <div class="row mb-4">
                        <div class="col-md-6">
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="card-title">Order Summary</h5>
                                    <ul class="list-group list-group-flush">
                                        <li class="list-group-item d-flex justify-content-between">
                                            <span>Course Fee</span>
                                            <span><asp:Label ID="CourseFeeLabel" runat="server"></asp:Label></span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between">
                                            <span>Discount</span>
                                            <span id="DiscountLabel" runat="server">$0.00</span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between">
                                            <span>Tax</span>
                                            <span id="TaxLabel" runat="server">$0.00</span>
                                        </li>
                                        <li class="list-group-item d-flex justify-content-between fw-bold">
                                            <span>Total Amount</span>
                                            <span><asp:Label ID="TotalAmountLabel" runat="server"></asp:Label></span>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        
                        <div class="col-md-6">
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="card-title">Payment Information</h5>
                                    <div class="mb-3">
                                        <label for="CardNumberTextBox" class="form-label">Card Number</label>
                                        <asp:TextBox ID="CardNumberTextBox" runat="server" CssClass="form-control" placeholder="1234 5678 9012 3456" MaxLength="19"></asp:TextBox>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-6">
                                            <label for="ExpiryDateTextBox" class="form-label">Expiry Date</label>
                                            <asp:TextBox ID="ExpiryDateTextBox" runat="server" CssClass="form-control" placeholder="MM/YY" MaxLength="5"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="CVVTextBox" class="form-label">CVV</label>
                                            <asp:TextBox ID="CVVTextBox" runat="server" CssClass="form-control" placeholder="123" MaxLength="3"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="mb-3">
                                        <label for="CardholderNameTextBox" class="form-label">Cardholder Name</label>
                                        <asp:TextBox ID="CardholderNameTextBox" runat="server" CssClass="form-control" placeholder="John Doe"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="d-flex justify-content-between">
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="CancelButton_Click" />
                        <asp:Button ID="PayButton" runat="server" Text="Pay Now" CssClass="btn btn-primary" OnClick="PayButton_Click" />
                    </div>
                </div>
            </div>
        </div>
        
        <div class="col-md-4">
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Payment Security</h5>
                </div>
                <div class="card-body">
                    <p>Your payment information is encrypted and secure. We use industry-standard security measures to protect your data.</p>
                    <div class="d-flex justify-content-around">
                        <i class="fab fa-cc-visa fa-2x text-primary"></i>
                        <i class="fab fa-cc-mastercard fa-2x text-danger"></i>
                        <i class="fab fa-cc-amex fa-2x text-info"></i>
                        <i class="fab fa-cc-paypal fa-2x text-primary"></i>
                    </div>
                </div>
            </div>
            
            <div class="card">
                <div class="card-header">
                    <h5 class="mb-0">Need Help?</h5>
                </div>
                <div class="card-body">
                    <p>If you have any issues with your payment, please contact our support team.</p>
                    <asp:Button ID="ContactSupportButton" runat="server" Text="Contact Support" CssClass="btn btn-outline-primary w-100" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>