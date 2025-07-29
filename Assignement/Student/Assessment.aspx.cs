using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EduSphere.Student
{
    public partial class Assessment : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is logged in
            if (Session["User"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                // Get assessment ID from query string
                if (Request.QueryString["AssessmentID"] != null && int.TryParse(Request.QueryString["AssessmentID"], out int assessmentID))
                {
                    LoadAssessmentDetails(assessmentID);
                    LoadAssessmentQuestions(assessmentID);
                }
                else
                {
                    // Redirect to course details if no assessment ID is provided
                    Response.Redirect("MyCourses.aspx");
                }
            }
        }

        private void LoadAssessmentDetails(int assessmentID)
        {
            string assessmentQuery = @"SELECT a.AssessmentID, a.Title, a.Description, a.TimeLimit, a.PassingScore, 
                                       c.Title AS CourseName, c.CourseID
                                       FROM Assessments a
                                       INNER JOIN Courses c ON a.CourseID = c.CourseID
                                       WHERE a.AssessmentID = @AssessmentID";

            SqlParameter[] assessmentParams = new SqlParameter[]
            {
                new SqlParameter("@AssessmentID", assessmentID)
            };

            using (SqlDataReader reader = Database.ExecuteReader(assessmentQuery, assessmentParams))
            {
                if (reader.Read())
                {
                    AssessmentTitleLabel.Text = reader["Title"].ToString();
                    AssessmentDescriptionLabel.Text = reader["Description"].ToString();
                    CourseNameLabel.Text = reader["CourseName"].ToString();
                    TimeLimitLabel.Text = reader["TimeLimit"].ToString() + " minutes";
                    QuestionsCountLabel.Text = "0"; // Will be updated after loading questions
                    PassingScoreLabel.Text = reader["PassingScore"].ToString();

                    // Set time limit for the timer (convert minutes to seconds)
                    TimeLimitLiteral.Text = (Convert.ToInt32(reader["TimeLimit"]) * 60).ToString();

                    // Store course ID for navigation
                    ViewState["CourseID"] = reader["CourseID"];
                }
                else
                {
                    // Assessment not found, redirect to my courses
                    Response.Redirect("MyCourses.aspx");
                }
            }
        }

        private void LoadAssessmentQuestions(int assessmentID)
        {
            string questionsQuery = @"SELECT q.QuestionID, q.QuestionText, q.QuestionType 
                                      FROM Questions q 
                                      WHERE q.AssessmentID = @AssessmentID 
                                      ORDER BY q.QuestionID";

            SqlParameter[] questionsParams = new SqlParameter[]
            {
                new SqlParameter("@AssessmentID", assessmentID)
            };

            DataTable dt = Database.ExecuteDataTable(questionsQuery, questionsParams);
            QuestionsRepeater.DataSource = dt;
            QuestionsRepeater.DataBind();

            // Update questions count
            QuestionsCountLabel.Text = dt.Rows.Count.ToString();
        }

        protected void QuestionsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField questionTypeHiddenField = (HiddenField)e.Item.FindControl("QuestionTypeHiddenField");
                string questionType = questionTypeHiddenField.Value;

                Panel multipleChoicePanel = (Panel)e.Item.FindControl("MultipleChoicePanel");
                Panel trueFalsePanel = (Panel)e.Item.FindControl("TrueFalsePanel");
                Panel shortAnswerPanel = (Panel)e.Item.FindControl("ShortAnswerPanel");

                // Hide all panels first
                multipleChoicePanel.Visible = false;
                trueFalsePanel.Visible = false;
                shortAnswerPanel.Visible = false;

                // Show the appropriate panel based on question type
                if (questionType == "MultipleChoice")
                {
                    multipleChoicePanel.Visible = true;
                    RadioButtonList optionsRadioButtonList = (RadioButtonList)e.Item.FindControl("OptionsRadioButtonList");

                    // Load options for this question
                    HiddenField questionIDHiddenField = (HiddenField)e.Item.FindControl("QuestionIDHiddenField");
                    int questionID = Convert.ToInt32(questionIDHiddenField.Value);

                    string optionsQuery = @"SELECT OptionID, OptionText 
                                          FROM Options 
                                          WHERE QuestionID = @QuestionID 
                                          ORDER BY OptionID";

                    SqlParameter[] optionsParams = new SqlParameter[]
                    {
                        new SqlParameter("@QuestionID", questionID)
                    };

                    DataTable dt = Database.ExecuteDataTable(optionsQuery, optionsParams);
                    optionsRadioButtonList.DataSource = dt;
                    optionsRadioButtonList.DataTextField = "OptionText";
                    optionsRadioButtonList.DataValueField = "OptionID";
                    optionsRadioButtonList.DataBind();
                }
                else if (questionType == "TrueFalse")
                {
                    trueFalsePanel.Visible = true;
                }
                else if (questionType == "ShortAnswer")
                {
                    shortAnswerPanel.Visible = true;
                }
            }
        }

        protected void SubmitAssessmentButton_Click(object sender, EventArgs e)
        {
            User currentUser = Session["User"] as User;
            int assessmentID = Convert.ToInt32(Request.QueryString["AssessmentID"]);

            // Calculate score
            int totalQuestions = 0;
            int correctAnswers = 0;
            List<Answer> answers = new List<Answer>();

            foreach (RepeaterItem item in QuestionsRepeater.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    totalQuestions++;

                    HiddenField questionIDHiddenField = (HiddenField)item.FindControl("QuestionIDHiddenField");
                    HiddenField questionTypeHiddenField = (HiddenField)item.FindControl("QuestionTypeHiddenField");
                    int questionID = Convert.ToInt32(questionIDHiddenField.Value);
                    string questionType = questionTypeHiddenField.Value;

                    Answer answer = new Answer();
                    answer.QuestionID = questionID;
                    answer.UserID = currentUser.UserID;

                    if (questionType == "MultipleChoice")
                    {
                        RadioButtonList optionsRadioButtonList = (RadioButtonList)item.FindControl("OptionsRadioButtonList");
                        if (optionsRadioButtonList.SelectedItem != null)
                        {
                            answer.OptionID = Convert.ToInt32(optionsRadioButtonList.SelectedItem.Value);
                        }
                    }
                    else if (questionType == "TrueFalse")
                    {
                        RadioButtonList trueFalseRadioButtonList = (RadioButtonList)item.FindControl("TrueFalseRadioButtonList");
                        if (trueFalseRadioButtonList.SelectedItem != null)
                        {
                            answer.AnswerText = trueFalseRadioButtonList.SelectedItem.Value;
                        }
                    }
                    else if (questionType == "ShortAnswer")
                    {
                        TextBox answerTextBox = (TextBox)item.FindControl("AnswerTextBox");
                        answer.AnswerText = answerTextBox.Text;
                    }

                    // Check if the answer is correct
                    if (IsAnswerCorrect(answer, questionType))
                    {
                        correctAnswers++;
                    }

                    answers.Add(answer);
                }
            }

            // Save answers to the database
            foreach (Answer answer in answers)
            {
                string insertQuery = @"INSERT INTO Answers (QuestionID, UserID, AnswerText, OptionID, AnswerDate) 
                                      VALUES (@QuestionID, @UserID, @AnswerText, @OptionID, GETDATE())";

                SqlParameter[] insertParams = new SqlParameter[]
                {
                    new SqlParameter("@QuestionID", answer.QuestionID),
                    new SqlParameter("@UserID", answer.UserID),
                    new SqlParameter("@AnswerText", answer.AnswerText ?? (object)DBNull.Value),
                    new SqlParameter("@OptionID", answer.OptionID ?? (object)DBNull.Value)
                };

                Database.ExecuteNonQuery(insertQuery, insertParams);
            }

            // Calculate score percentage
            int scorePercentage = totalQuestions > 0 ? (correctAnswers * 100) / totalQuestions : 0;

            // Show results
            QuestionsPanel.Visible = false;
            ResultsPanel.Visible = true;
            ScoreLabel.Text = scorePercentage.ToString();

            // Determine pass/fail status
            string assessmentQuery = "SELECT PassingScore FROM Assessments WHERE AssessmentID = @AssessmentID";
            SqlParameter[] assessmentParams = new SqlParameter[]
            {
                new SqlParameter("@AssessmentID", assessmentID)
            };

            int passingScore = Convert.ToInt32(Database.ExecuteScalar(assessmentQuery, assessmentParams));

            if (scorePercentage >= passingScore)
            {
                StatusLabel.Text = "Passed";
                StatusLabel.CssClass = "text-success";
            }
            else
            {
                StatusLabel.Text = "Failed";
                StatusLabel.CssClass = "text-danger";
            }
        }

        private bool IsAnswerCorrect(Answer answer, string questionType)
        {
            if (questionType == "MultipleChoice" && answer.OptionID.HasValue)
            {
                string checkQuery = @"SELECT COUNT(*) FROM Options 
                                      WHERE OptionID = @OptionID AND IsCorrect = 1";

                SqlParameter[] checkParams = new SqlParameter[]
                {
                    new SqlParameter("@OptionID", answer.OptionID.Value)
                };

                int count = Convert.ToInt32(Database.ExecuteScalar(checkQuery, checkParams));
                return count > 0;
            }
            else if (questionType == "TrueFalse" && !string.IsNullOrEmpty(answer.AnswerText))
            {
                // For True/False, we would need to know the correct answer
                // This is a simplified version. In a real application, you would store the correct answer for each question.
                // For now