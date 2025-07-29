using System;
using System.Data;
using System.Web.UI;

namespace EduSphere.Admin
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboardStats();
                LoadRecentActivities();
            }
        }

        private void LoadDashboardStats()
        {
            // Get total users count
            string usersQuery = "SELECT COUNT(*) FROM Users";
            int totalUsers = Convert.ToInt32(Database.ExecuteScalar(usersQuery));
            TotalUsersLabel.Text = totalUsers.ToString();

            // Get total courses count
            string coursesQuery = "SELECT COUNT(*) FROM Courses";
            int totalCourses = Convert.ToInt32(Database.ExecuteScalar(coursesQuery));
            TotalCoursesLabel.Text = totalCourses.ToString();

            // Get total resources count
            string resourcesQuery = "SELECT COUNT(*) FROM LearningResources";
            int totalResources = Convert.ToInt32(Database.ExecuteScalar(resourcesQuery));
            TotalResourcesLabel.Text = totalResources.ToString();

            // Get total discussions count
            string discussionsQuery = "SELECT COUNT(*) FROM Discussions";
            int totalDiscussions = Convert.ToInt32(Database.ExecuteScalar(discussionsQuery));
            TotalDiscussionsLabel.Text = totalDiscussions.ToString();
        }

        private void LoadRecentActivities()
        {
            // This is a simplified version. In a real application, you would have an activities log table.
            // For now, we'll get the latest registrations as recent activities.
            string query = @"SELECT 'New User Registration' AS Activity, Username AS User, 
                            CONVERT(varchar, RegistrationDate, 120) AS Date 
                            FROM Users 
                            ORDER BY RegistrationDate DESC 
                            OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY";

            DataTable dt = Database.ExecuteDataTable(query);
            RecentActivitiesGridView.DataSource = dt;
            RecentActivitiesGridView.DataBind();
        }
    }
}