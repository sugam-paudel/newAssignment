using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EduSphere.Admin
{
    public partial class ManageCourses : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCourses();
            }
        }

        private void BindCourses(string searchTerm = "")
        {
            string query = @"SELECT c.CourseID, c.Title, c.Description, u.Username AS CreatedBy, c.CreatedDate 
                            FROM Courses c 
                            INNER JOIN Users u ON c.CreatedBy = u.UserID";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += " WHERE c.Title LIKE @SearchTerm OR c.Description LIKE @SearchTerm";
            }

            query += " ORDER BY c.CreatedDate DESC";

            SqlParameter[] parameters = null;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@SearchTerm", "%" + searchTerm + "%")
                };
            }

            DataTable dt = Database.ExecuteDataTable(query, parameters);
            CoursesGridView.DataSource = dt;
            CoursesGridView.DataBind();
        }

        protected void SearchCoursesButton_Click(object sender, EventArgs e)
        {
            BindCourses(SearchCoursesTextBox.Text);
        }

        protected void CoursesGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int courseID = Convert.ToInt32(CoursesGridView.DataKeys[e.RowIndex].Value);

            // First, delete related records in other tables
            // Delete enrollments
            string deleteEnrollmentsQuery = "DELETE FROM Enrollments WHERE CourseID = @CourseID";
            SqlParameter[] enrollmentParams = new SqlParameter[]
            {
                new SqlParameter("@CourseID", courseID)
            };
            Database.ExecuteNonQuery(deleteEnrollmentsQuery, enrollmentParams);

            // Delete learning resources
            string deleteResourcesQuery = "DELETE FROM LearningResources WHERE CourseID = @CourseID";
            SqlParameter[] resourceParams = new SqlParameter[]
            {
                new SqlParameter("@CourseID", courseID)
            };
            Database.ExecuteNonQuery(deleteResourcesQuery, resourceParams);

            // Delete discussions
            string deleteDiscussionsQuery = "DELETE FROM Discussions WHERE CourseID = @CourseID";
            SqlParameter[] discussionParams = new SqlParameter[]
            {
                new SqlParameter("@CourseID", courseID)
            };
            Database.ExecuteNonQuery(deleteDiscussionsQuery, discussionParams);

            // Delete assessments
            string deleteAssessmentsQuery = "DELETE FROM Assessments WHERE CourseID = @CourseID";
            SqlParameter[] assessmentParams = new SqlParameter[]
            {
                new SqlParameter("@CourseID", courseID)
            };
            Database.ExecuteNonQuery(deleteAssessmentsQuery, assessmentParams);

            // Finally, delete the course
            string deleteCourseQuery = "DELETE FROM Courses WHERE CourseID = @CourseID";
            SqlParameter[] courseParams = new SqlParameter[]
            {
                new SqlParameter("@CourseID", courseID)
            };
            Database.ExecuteNonQuery(deleteCourseQuery, courseParams);

            BindCourses();
        }

        protected void CoursesGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int courseID = Convert.ToInt32(CoursesGridView.DataKeys[e.NewEditIndex].Value);
            Response.Redirect($"EditCourse.aspx?CourseID={courseID}");
        }

        protected void CoursesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CoursesGridView.PageIndex = e.NewPageIndex;
            BindCourses(SearchCoursesTextBox.Text);
        }
    }
}