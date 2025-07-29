using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using EduSphere.Models;

namespace EduSphere.Student
{
    public partial class EnrolledCourse : Page
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EduSphereDB"].ConnectionString;
        private int courseId;
        private int studentId;
        private Course course;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get course ID and student ID
                if (int.TryParse(Request.QueryString["CourseID"], out courseId))
                {
                    studentId = GetCurrentStudentId();
                    LoadCourseDetails();
                    LoadCourseProgress();
                    LoadCourseContent();
                    LoadCourseResources();
                    LoadUpcomingEvents();
                    CheckCertificateEligibility();
                }
                else
                {
                    // Redirect to enrolled courses page if no course ID provided
                    Response.Redirect("~/Student/EnrolledCourses.aspx");
                }
            }
        }

        private void LoadCourseDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetCourseDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseID", courseId);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        course = new Course
                        {
                            CourseID = courseId,
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            Category = reader["Category"].ToString(),
                            Difficulty = reader["Difficulty"].ToString(),
                            Duration = Convert.ToInt32(reader["Duration"]),
                            Instructor = reader["Instructor"].ToString()
                        };

                        CourseTitleLabel.Text = course.Title;
                        CourseDescriptionLabel.Text = course.Description;
                        CourseCategoryLabel.Text = course.Category;
                        CourseDifficultyLabel.Text = course.Difficulty;
                        CourseDurationLabel.Text = course.Duration.ToString();
                        InstructorLabel.Text = course.Instructor;
                    }
                    else
                    {
                        // Course not found
                        Response.Redirect("~/Student/EnrolledCourses.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                // Show error message to user
                ErrorPanel.Visible = true;
                ErrorMessageLabel.Text = "Error loading course details: " + ex.Message;
            }
        }

        private void LoadCourseProgress()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetStudentCourseProgress", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.Parameters.AddWithValue("@CourseID", courseId);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int progressPercentage = Convert.ToInt32(reader["ProgressPercentage"]);
                        DateTime enrolledDate = Convert.ToDateTime(reader["EnrolledDate"]);
                        DateTime? lastAccessed = reader["LastAccessed"] as DateTime?;

                        ProgressLiteral.Text = progressPercentage.ToString();
                        ProgressValueLiteral.Text = progressPercentage.ToString();
                        ProgressTextLiteral.Text = progressPercentage.ToString();

                        EnrolledDateLabel.Text = enrolledDate.ToString("yyyy-MM-dd");
                        LastAccessedLabel.Text = lastAccessed.HasValue ? lastAccessed.Value.ToString("yyyy-MM-dd") : "Never";
                        CompletionLabel.Text = progressPercentage + "%";
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                // Show error message to user
                ErrorPanel.Visible = true;
                ErrorMessageLabel.Text = "Error loading course progress: " + ex.Message;
            }
        }

        private void LoadCourseContent()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetCourseContent", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseID", courseId);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        ModulesRepeater.DataSource = dt;
                        ModulesRepeater.DataBind();
                    }
                    else
                    {
                        NoContentPanel.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                // Show error message to user
                ErrorPanel.Visible = true;
                ErrorMessageLabel.Text = "Error loading course content: " + ex.Message;
            }
        }

        protected void ModulesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater lessonsRepeater = (Repeater)e.Item.FindControl("LessonsRepeater");
                if (lessonsRepeater != null)
                {
                    DataRowView row = (DataRowView)e.Item.DataItem;
                    int moduleId = Convert.ToInt32(row["ModuleID"]);

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("sp_GetModuleLessons", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ModuleID", moduleId);
                        cmd.Parameters.AddWithValue("@StudentID", studentId);

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        lessonsRepeater.DataSource = dt;
                        lessonsRepeater.DataBind();
                    }
                }
            }
        }

        private void LoadCourseResources()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetCourseResources", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseID", courseId);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ResourcesGridView.DataSource = dt;
                    ResourcesGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Log error
                // Show error message to user
                ErrorPanel.Visible = true;
                ErrorMessageLabel.Text = "Error loading course resources: " + ex.Message;
            }
        }

        private void LoadUpcomingEvents()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetUpcomingCourseEvents", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseID", courseId);

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        EventsRepeater.DataSource = dt;
                        EventsRepeater.DataBind();
                    }
                    else
                    {
                        NoEventsPanel.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                // Show error message to user
                ErrorPanel.Visible = true;
                ErrorMessageLabel.Text = "Error loading upcoming events: " + ex.Message;
            }
        }

        private void CheckCertificateEligibility()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_CheckCertificateEligibility", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.Parameters.AddWithValue("@CourseID", courseId);

                    con.Open();
                    bool isEligible = Convert.ToBoolean(cmd.ExecuteScalar());

                    if (isEligible)
                    {
                        ViewCertificateButton.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                // Show error message to user
                ErrorPanel.Visible = true;
                ErrorMessageLabel.Text = "Error checking certificate eligibility: " + ex.Message;
            }
        }

        protected void ContinueCourseButton_Click(object sender, EventArgs e)
        {
            // Redirect to the last accessed lesson or the first lesson
            Response.Redirect($"Lesson.aspx?CourseID={courseId}");
        }

        protected void ViewCertificateButton_Click(object sender, EventArgs e)
        {
            // Redirect to certificate page
            Response.Redirect($"Certificate.aspx?CourseID={courseId}");
        }

        protected void RequestCertificateButton_Click(object sender, EventArgs e)
        {
            // Redirect to certificate request page
            Response.Redirect($"CertificateRequest.aspx?CourseID={courseId}");
        }

        private int GetCurrentStudentId()
        {
            // In a real application, this would get the current user's ID from the authentication system
            // For demo purposes, we'll return a hardcoded value
            return 1;
        }
    }
} 