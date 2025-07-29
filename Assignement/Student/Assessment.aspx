<%@ Page Title="Course Assessment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Assessment.aspx.cs" Inherits="EduSphere.Student.Assessment" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0"><asp:Label ID="AssessmentTitleLabel" runat="server"></asp:Label></h5>
                </div>
                <div class="card-body">
                    <p><asp:Label ID="AssessmentDescriptionLabel" runat="server"></asp:Label></p>
                    
                    <asp:Panel ID="QuestionsPanel" runat="server">
                        <asp:Repeater ID="QuestionsRepeater" runat="server" OnItemDataBound="QuestionsRepeater_ItemDataBound">
                            <ItemTemplate>
                                <div class="card mb-3">
                                    <div class="card-body">
                                        <h6 class="card-title">Question <%# Container.ItemIndex + 1 %>: <%# Eval("QuestionText") %></h6>
                                        
                                        <asp:HiddenField ID="QuestionIDHiddenField" runat="server" Value='<%# Eval("QuestionID") %>' />
                                        <asp:HiddenField ID="QuestionTypeHiddenField" runat="server" Value='<%# Eval("QuestionType") %>' />
                                        
                                        <!-- Multiple Choice Options -->
                                        <asp:Panel ID="MultipleChoicePanel" runat="server">
                                            <asp:RadioButtonList ID="OptionsRadioButtonList" runat="server" CssClass="form-check">
                                            </asp:RadioButtonList>
                                        </asp:Panel>
                                        
                                        <!-- True/False Options -->
                                        <asp:Panel ID="TrueFalsePanel" runat="server">
                                            <asp:RadioButtonList ID="TrueFalseRadioButtonList" runat="server" CssClass="form-check">
                                                <asp:ListItem Text="True" Value="True"></asp:ListItem>
                                                <asp:ListItem Text="False" Value="False"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </asp:Panel>
                                        
                                        <!-- Short Answer -->
                                        <asp:Panel ID="ShortAnswerPanel" runat="server">
                                            <asp:TextBox ID="AnswerTextBox" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        
                        <div class="text-center">
                            <asp:Button ID="SubmitAssessmentButton" runat="server" Text="Submit Assessment" CssClass="btn btn-primary" OnClick="SubmitAssessmentButton_Click" />
                        </div>
                    </asp:Panel>
                    
                    <asp:Panel ID="ResultsPanel" runat="server" Visible="false">
                        <div class="alert alert-info">
                            <h4>Assessment Results</h4>
                            <p>Your score: <asp:Label ID="ScoreLabel" runat="server"></asp:Label>%</p>
                            <p>Status: <asp:Label ID="StatusLabel" runat="server"></asp:Label></p>
                        </div>
                        
                        <asp:Button ID="BackToCourseButton" runat="server" Text="Back to Course" CssClass="btn btn-secondary" OnClick="BackToCourseButton_Click" />
                    </asp:Panel>
                </div>
            </div>
        </div>
        
        <div class="col-md-4">
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Assessment Information</h5>
                </div>
                <div class="card-body">
                    <ul class="list-group list-group-flush">
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            Course
                            <span><asp:Label ID="CourseNameLabel" runat="server"></asp:Label></span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            Time Limit
                            <span><asp:Label ID="TimeLimitLabel" runat="server"></asp:Label></span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            Questions
                            <span><asp:Label ID="QuestionsCountLabel" runat="server"></asp:Label></span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            Passing Score
                            <span><asp:Label ID="PassingScoreLabel" runat="server"></asp:Label>%</span>
                        </li>
                    </ul>
                </div>
            </div>
            
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="mb-0">Timer</h5>
                </div>
                <div class="card-body text-center">
                    <h3 id="timer" class="display-4">00:00:00</h3>
                    <p class="text-muted">Time remaining</p>
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        // Timer functionality
        var timeLimit = parseInt('<asp:Literal ID="TimeLimitLiteral" runat="server" />'); // in seconds
        var timerElement = document.getElementById('timer');
        
        if (timeLimit > 0) {
            var countdown = setInterval(function() {
                var hours = Math.floor(timeLimit / 3600);
                var minutes = Math.floor((timeLimit % 3600) / 60);
                var seconds = timeLimit % 60;
                
                timerElement.textContent = 
                    (hours < 10 ? "0" + hours : hours) + ":" + 
                    (minutes < 10 ? "0" + minutes : minutes) + ":" + 
                    (seconds < 10 ? "0" + seconds : seconds);
                
                if (timeLimit <= 0) {
                    clearInterval(countdown);
                    alert("Time's up! Your assessment will be submitted automatically.");
                    document.getElementById('<%= SubmitAssessmentButton.ClientID %>').click();
                }
                
                timeLimit--;
            }, 1000);
        }
    </script>
</asp:Content>