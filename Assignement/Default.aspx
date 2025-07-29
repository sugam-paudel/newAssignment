<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Assignement._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container mt-5">
        <!-- Hero Section -->
        <section class="text-center bg-light p-5 rounded">
            <h1 class="display-4">Welcome to LearnHub</h1>
            <p class="lead">Your Digital Gateway to Interactive and Engaging Learning Resources</p>
            <asp:HyperLink NavigateUrl="~/Register.aspx" CssClass="btn btn-primary btn-lg" runat="server">Get Started</asp:HyperLink>
        </section>

        <!-- Features Section -->
        <div class="row text-center mt-5">
            <div class="col-md-4 mb-4">
                <div class="card h-100 shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title">Interactive Courses</h5>
                        <p class="card-text">Access a variety of online courses, simulations, and self-assessments to enhance your knowledge anytime, anywhere.</p>
                        <asp:HyperLink NavigateUrl="~/Courses.aspx" CssClass="btn btn-outline-primary" runat="server">Explore Courses</asp:HyperLink>
                    </div>
                </div>
            </div>

            <div class="col-md-4 mb-4">
                <div class="card h-100 shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title">Register & Learn</h5>
                        <p class="card-text">Create an account to track your progress, take quizzes, and unlock new resources tailored just for you.</p>
                        <asp:HyperLink NavigateUrl="~/Register.aspx" CssClass="btn btn-outline-success" runat="server">Join Now</asp:HyperLink>
                    </div>
                </div>
            </div>

            <div class="col-md-4 mb-4">
                <div class="card h-100 shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title">Admin Control</h5>
                        <p class="card-text">Admins can manage users and course materials through a secure, easy-to-use admin dashboard.</p>
                        <asp:HyperLink NavigateUrl="~/Admin/Login.aspx" CssClass="btn btn-outline-danger" runat="server">Admin Login</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>

        <!-- Call-to-Action -->
        <section class="mt-5 text-center">
            <h2 class="mb-3">Start Your Learning Journey Today!</h2>
            <p>Our platform is designed for students and educators to thrive in a digital-first world. Interactive, engaging, and effective.</p>
            <asp:HyperLink NavigateUrl="~/Login.aspx" CssClass="btn btn-dark btn-lg" runat="server">Login</asp:HyperLink>
        </section>
    </main>
</asp:Content>
