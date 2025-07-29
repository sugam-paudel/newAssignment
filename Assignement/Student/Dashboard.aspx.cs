using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EduSphere.Student
{
    public partial class Default : Page
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
                LoadDashboardStats();
                LoadEnrolledCourses();
                LoadAvailableCourses();
                LoadRecentActivities();
            }
        }

        private void LoadDashboardStats()
        {
            User currentUser = Session["User"] as User;

            // Get enrolled courses count
            string enrolledQuery = "SELECT COUNT(*) FROM Enrollments WHERE UserID = @UserID";
            SqlParameter[] enrolledParams = new SqlParameter[]
            {
                new SqlParameter("@UserID", currentUser.UserID)
            };
            int enrolledCount = Convert.ToInt32(Database.ExecuteScalar(enrolledQuery, enrolledParams));
            EnrolledCoursesLabel.Text = enrolledCount.ToString();

            // Get completed courses count (for simplicity, we'll assume a course is completed if progress is 100%)
            string completedQuery = @"SELECT COUNT(*) FROM Enrollments e 
                                    INNER JOIN UserCourseProgress p ON e.EnrollmentID = p.EnrollmentID 
                                    WHERE e.UserID = @UserID AND p.ProgressPercentage = 100";
            SqlParameter[] completedParams = new SqlParameter[]
            {
                new SqlParameter("@UserID", currentUser.UserID)
            };
            int completedCount = Convert.ToInt32(Database.ExecuteScalar(completedQuery, completedParams));
            CompletedCoursesLabel.Text = completedCount.ToString();

            // Get certificates count (for simplicity, we'll assume a certificate is issued for each completed course)
            CertificatesLabel.Text = completedCount.ToString();
        }

        private void LoadEnrolledCourses()
        {
            User currentUser = Session["User"] as User;

            string query = @"SELECT c.CourseID, c.Title, e.EnrollmentDate, 
                            ISNULL(p.ProgressPercentage, 0) AS Progress 
                            FROM Enrollments e 
                            INNER JOIN Courses c ON e.CourseID = c.CourseID 
                            LEFT JOIN UserCourseProgress p ON e.EnrollmentID = p.EnrollmentID 
                            WHERE e.UserID = @UserID 
                            ORDER BY e.EnrollmentDate DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", currentUser.UserID)
            };

            DataTable dt = Database.ExecuteDataTable(query, parameters);
            EnrolledCoursesGridView.DataSource = dt;
            EnrolledCoursesGridView.DataBind();
        }

        private void LoadAvailableCourses()
        {
            User currentUser = Session["User"] as User;

            string query = @"SELECT c.CourseID, c.Title, c.Category, c.Difficulty 
                            FROM Courses c 
                            WHERE c.CourseID NOT IN (
                                SELECT CourseID FROM Enrollments WHERE UserID = @UserID
                            ) 
                            ORDER BY c.Title";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", currentUser.UserID)
            };

            DataTable dt = Database.ExecuteDataTable(query, parameters);
            AvailableCoursesGridView.DataSource = dt;
            AvailableCoursesGridView.DataBind();
        }

        private void LoadRecentActivities()
        {
            User currentUser = Session["User"] as User;

            // This is a simplified version. In a real application, you would have an activities log table.
            // For now, we'll get the latest enrollments as recent activities.
            string query = @"SELECT 'Enrolled in course' AS Activity, c.Title AS Course, 
                            CONVERT(varchar, e.EnrollmentDate, 120) AS Date 
                            FROM Enrollments e 
                            INNER JOIN Courses c ON e.CourseID = c.CourseID 
                            WHERE e.UserID = @UserID 
                            ORDER BY e.EnrollmentDate DESC 
                            OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", currentUser.UserID)
            };

            DataTable dt = Database.ExecuteDataTable(query, parameters);
            RecentActivitiesGridView.DataSource = dt;
            RecentActivitiesGridView.DataBind();
        }

        protected void EnrollButton_Click(object sender, EventArgs e)
        {
            User currentUser = Session["User"] as User;
            Button enrollButton = (Button)sender;
            int courseID = Convert.ToInt32(enrollButton.CommandArgument);

            // Check if already enrolled
            string checkQuery = "SELECT COUNT(*) FROM Enrollments WHERE UserID = @UserID AND CourseID = @CourseID";
            SqlParameter[] checkParams = new SqlParameter[]
            {
                new SqlParameter("@UserID", currentUser.UserID),
                new SqlParameter("@CourseID", courseID)
            };
            int count = Convert.ToInt32(Database.ExecuteScalar(checkQuery, checkParams));

            if (count == 0)
            {
                // Enroll the user
                string insertQuery = "INSERT INTO Enrollments (UserID, CourseID, EnrollmentDate) VALUES (@UserID, @CourseID, GETDATE())";
                SqlParameter[] insertParams = new SqlParameter[]
                {
                    new SqlParameter("@UserID", currentUser.UserID),
                    new SqlParameter("@CourseID", courseID)
                };
                Database.ExecuteNonQuery(insertQuery, insertParams);

                // Refresh the data
                LoadDashboardStats();
                LoadEnrolledCourses();
                LoadAvailableCourses();
            }
        }
    }
}