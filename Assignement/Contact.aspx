<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Assignement.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container mt-5">
        <!-- Header -->
        <section class="text-center mb-5">
            <h1 class="display-5">Contact Us</h1>
            <p class="lead">Have questions, feedback, or need help? Reach out to our support team.</p>
        </section>

        <!-- Contact Information -->
        <div class="row mb-5">
            <div class="col-md-6">
                <h4>Contact Information</h4>
                <address>
                    <strong>LearnHub Platform</strong><br />
                    Asia Pacific University<br />
                    Kuala Lumpur, Malaysia<br />
                    <abbr title="Phone">Phone:</abbr> +60 123 456 789
                </address>
                <address>
                    <strong>Email:</strong> <a href="mailto:support@learnhub.com">support@learnhub.com</a><br />
                    <strong>Marketing:</strong> <a href="mailto:marketing@learnhub.com">marketing@learnhub.com</a>
                </address>
            </div>

            <!-- Contact Form -->
            <div class="col-md-6">
                <h4>Send us a message</h4>
                <form>
                    <div class="form-group mb-3">
                        <label for="name">Full Name</label>
                        <input type="text" class="form-control" id="name" placeholder="Your name" required>
                    </div>
                    <div class="form-group mb-3">
                        <label for="email">Email address</label>
                        <input type="email" class="form-control" id="email" placeholder="Your email" required>
                    </div>
                    <div class="form-group mb-3">
                        <label for="message">Message</label>
                        <textarea class="form-control" id="message" rows="4" placeholder="Type your message here..." required></textarea>
                    </div>
                    <button type="submit" class="btn btn-primary">Send Message</button>
                </form>
            </div>
        </div>
    </main>
</asp:Content>
