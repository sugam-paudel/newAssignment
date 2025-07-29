using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EduSphere.Student
{
    public partial class CourseDetails : Page
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
                // Get course ID from query string
                if (Request.QueryString["CourseID"] != null && int.TryParse(Request.QueryString["CourseID"], out int courseID))
                {
                    LoadCourseDetails(courseID);
                    LoadCourseResources(courseID);
                    LoadCourseDiscussions(courseID);
                    LoadCourseAssessments(courseID);
                    LoadCourseModules(courseID);
                }
                else
                {
                    // Redirect to my courses if no course ID is provided
                    Response.Redirect("MyCourses.aspx");
                }
            }
        }

        private void LoadCourseDetails(int courseID)
        {
            User currentUser = Session["User"] as User;

            // Get course details
            string courseQuery = @"SELECT c.CourseID, c.Title, c.Description, c.Category, c.Difficulty, c.Duration, 
                                  c.Price, c.CreatedDate, u.Username AS CreatedBy, e.EnrollmentDate,
                                  ISNULL(p.ProgressPercentage, 0) AS Progress
                                  FROM Courses c
                                  INNER JOIN Users u ON c.CreatedBy = u.UserID
                                  LEFT JOIN Enrollments e ON c.CourseID = e.CourseID AND e.UserID = @UserID
                                  LEFT JOIN UserCourseProgress p ON e.EnrollmentID = p.EnrollmentID
                                  WHERE c.CourseID = @CourseID";

            SqlParameter[] courseParams = new SqlParameter[]
            {
                new SqlParameter("@CourseID", courseID),
                new SqlParameter("@UserID", currentUser.UserID)
            };

            using (SqlDataReader reader = Database.ExecuteReader(courseQuery, courseParams))
            {
                if (reader.Read())
                {
                    CourseTitleLabel.Text = reader["Title"].ToString();
                    CourseDescriptionLabel.Text = reader["Description"].ToString();
                    CourseCategoryLabel.Text = reader["Category"].ToString();
                    CourseDifficultyLabel.Text = reader["Difficulty"].ToString();
                    CourseDurationLabel.Text = reader["Duration"].ToString();
                    PriceLabel.Text = "$" + reader["Price"].ToString();
                    CreatedByLabel.Text = reader["CreatedBy"].ToString();
                    CreatedDateLabel.Text = Convert.ToDateTime(reader["CreatedDate"]).ToString("yyyy-MM-dd");

                    // Check if enrolled
                    if (!reader.IsDBNull(reader.GetOrdinal("EnrollmentDate")))
                    {
                        EnrolledDateLabel.Text = Convert.ToDateTime(reader["EnrollmentDate"]).ToString("yyyy-MM-dd");
                        int progress = Convert.ToInt32(reader["Progress"]);
                        ProgressLiteral.Text = progress.ToString();
                        ProgressValueLiteral.Text = progress.ToString();
                        ProgressTextLiteral.Text = progress.ToString();

                        // Show/hide buttons based on progress
                        if (progress == 0)
                        {
                            StartCourseButton.Visible = true;
                            ContinueCourseButton.Visible = false;
                        }
                        else
                        {
                            StartCourseButton.Visible = false;
                            ContinueCourseButton.Visible = true;
                        }
                    }
                    else
                    {
                        // Not enrolled, redirect to my courses
                        Response.Redirect("MyCourses.aspx");
                    }
                }
                else
                {
                    // Course not found, redirect to my courses
                    Response.Redirect("MyCourses.aspx");
                }
            }
        }

        private void LoadCourseResources(int courseID)
        {
            string resourcesQuery = @"SELECT ResourceID, Title, Description, ResourceType, FilePath 
                                      FROM LearningResources 
                                      WHERE CourseID = @CourseID 
                                      ORDER BY UploadDate DESC";

            SqlParameter[] resourcesParams = new SqlParameter[]
            {
                new SqlParameter("@CourseID", courseID)
            };

            DataTable dt = Database.ExecuteDataTable(resourcesQuery, resourcesParams);
            ResourcesGridView.DataSource = dt;
            ResourcesGridView.DataBind();
        }

        private void LoadCourseDiscussions(int courseID)
        {
            string discussionsQuery = @"SELECT d.DiscussionID, d.Title, u.Username AS PostedBy, d.PostedDate,
                                        (SELECT COUNT(*) FROM DiscussionReplies WHERE DiscussionID = d.DiscussionID) AS ReplyCount
                                        FROM Discussions d
                                        INNER JOIN Users u ON d.UserID = u.UserID
                                        WHERE d.CourseID = @CourseID
                                        ORDER BY d.PostedDate DESC";

            SqlParameter[] discussionsParams = new SqlParameter[]
            {
                new SqlParameter("@CourseID", courseID)
            };

            DataTable dt = Database.ExecuteDataTable(discussionsQuery, discussionsParams);
            DiscussionsGridView.DataSource = dt;
            DiscussionsGridView.DataBind();
        }

        private void LoadCourseAssessments(int courseID)
        {
            string assessmentsQuery = @"SELECT AssessmentID, Title, Description 
                                       FROM Assessments 
                                       WHERE CourseID = @CourseID 
                                       ORDER BY CreatedDate DESC";

            SqlParameter[] assessmentsParams = new SqlParameter[]
            {
                new SqlParameter("@CourseID", courseID)
            };

            DataTable dt = Database.ExecuteDataTable(assessmentsQuery, assessmentsParams);
            AssessmentsGridView.DataSource = dt;
            AssessmentsGridView.DataBind();
        }

        private void LoadCourseModules(int courseID)
        {
            // For simplicity, we'll assume modules are stored in a table called CourseModules
            string modulesQuery = @"SELECT ModuleID, Title, Description 
                                   FROM CourseModules 
                                   WHERE CourseID = @CourseID 
                                   ORDER BY ModuleOrder";

            SqlParameter[] modulesParams = new SqlParameter[]
            {
                new SqlParameter("@CourseID", courseID)
            };

            DataTable dt = Database.ExecuteDataTable(modulesQuery, modulesParams);

            if (dt.Rows.Count > 0)
            {
                ModulesRepeater.DataSource = dt;
                ModulesRepeater.DataBind();
                NoModulesPanel.Visible = false;
            }
            else
            {
                ModulesRepeater.Visible = false;
                NoModulesPanel.Visible = true;
            }
        }

        protected string GetAssessmentStatus(object assessmentID)
        {
            User currentUser = Session["User"] as User;
            int id = Convert.ToInt32(assessmentID);

            // Check if user has taken the assessment
            string checkQuery = @"SELECT COUNT(*) FROM Answers a 
                                INNER JOIN Questions q ON a.QuestionID = q.QuestionID 
                                WHERE q.AssessmentID = @AssessmentID AND a.UserID = @UserID";

            SqlParameter[] checkParams = new SqlParameter[]
            {
                new SqlParameter("@AssessmentID", id),
                new SqlParameter("@UserID", currentUser.UserID)
            };

            int count = Convert.ToInt32(Database.ExecuteScalar(checkQuery, checkParams));

            return count > 0 ? "Completed" : "Not Started";
        }

        protected void StartCourseButton_Click(object sender, EventArgs e)
        {
            // Get course ID from query string
            if (Request.QueryString["CourseID"] != null && int.TryParse(Request.QueryString["CourseID"], out int courseID))
            {
                // Initialize progress for the course
                User currentUser = Session["User"] as User;

                // Get enrollment ID
                string enrollmentQuery = "SELECT EnrollmentID FROM Enrollments WHERE UserID = @UserID AND CourseID = @CourseID";
                SqlParameter[] enrollmentParams = new SqlParameter[]
                {
                    new SqlParameter("@UserID", currentUser.UserID),
                    new SqlParameter("@CourseID", courseID)
                };

                object enrollmentIDObj = Database.ExecuteScalar(enrollmentQuery, enrollmentParams);

                if (enrollmentIDObj != null)
                {
                    int enrollmentID = Convert.ToInt32(enrollmentIDObj);

                    // Insert initial progress
                    string progressQuery = @"INSERT INTO UserCourseProgress (EnrollmentID, ProgressPercentage, LastUpdatedDate) 
                                          VALUES (@EnrollmentID, 0, GETDATE())";

                    SqlParameter[] progressParams = new SqlParameter[]
                    {
                        new SqlParameter("@EnrollmentID", enrollmentID)
                    };

                    Database.ExecuteNonQuery(progressQuery, progressParams);

                    // Refresh the page
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        protected void ContinueCourseButton_Click(object sender, EventArgs e)
        {
            // This button is just for show. In a real application, it would redirect to the next lesson.
            // For now, we'll just refresh the page.
            Response.Redirect(Request.RawUrl);
        }

        protected void NewDiscussionButton_Click(object sender, EventArgs e)
        {
            // Get course ID from query string
            if (Request.QueryString["CourseID"] != null && int.TryParse(Request.QueryString["CourseID"], out int courseID))
            {
                Response.Redirect($"NewDiscussion.aspx?CourseID={courseID}");
            }
        }
    }
}