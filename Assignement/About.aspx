<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Assignement.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container mt-5">
        <!-- Mission Section -->
        <section class="text-center mb-5">
            <h1 class="display-5">About LearnHub</h1>
            <p class="lead mt-3">Empowering students through accessible and engaging digital education.</p>
        </section>

        <!-- Platform Description -->
        <section class="mb-5">
            <h3>Our Mission</h3>
            <p>
                LearnHub is designed to support modern education by providing interactive and accessible digital learning resources. 
                Whether it's self-paced quizzes, real-time discussions, or multimedia content, our platform ensures students can learn 
                in ways that suit them best.
            </p>
        </section>

        <!-- Platform Objectives -->
        <section class="mb-5">
            <h3>What We Offer</h3>
            <ul>
                <li>Access to a wide range of digital learning materials</li>
                <li>User-friendly and secure learning environment</li>
                <li>Interactive self-assessments and quizzes</li>
                <li>Admin and student dashboards for easy content management</li>
            </ul>
        </section>

        <!-- Team Info -->
        <section>
            <h3>Meet the Team</h3>
            <div class="row mt-3">
                <div class="col-md-4">
                    <div class="card shadow-sm">
                        <div class="card-body text-center">
                            <h5 class="card-title">Student 1</h5>
                            <p class="card-text">[Name & ID]</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card shadow-sm">
                        <div class="card-body text-center">
                            <h5 class="card-title">Student 2</h5>
                            <p class="card-text">[Name & ID]</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card shadow-sm">
                        <div class="card-body text-center">
                            <h5 class="card-title">Student 3</h5>
                            <p class="card-text">[Name & ID]</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
