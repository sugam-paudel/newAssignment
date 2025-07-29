using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EduSphere.Admin
{
    public partial class ManageUsers : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUsers();
            }
        }

        private void BindUsers(string searchTerm = "")
        {
            string query = "SELECT UserID, Username, Email, Role, FirstName, LastName, RegistrationDate FROM Users";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += " WHERE Username LIKE @SearchTerm OR Email LIKE @SearchTerm OR FirstName LIKE @SearchTerm OR LastName LIKE @SearchTerm";
            }

            query += " ORDER BY RegistrationDate DESC";

            SqlParameter[] parameters = null;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@SearchTerm", "%" + searchTerm + "%")
                };
            }

            DataTable dt = Database.ExecuteDataTable(query, parameters);
            UsersGridView.DataSource = dt;
            UsersGridView.DataBind();
        }

        protected void SearchUsersButton_Click(object sender, EventArgs e)
        {
            BindUsers(SearchUsersTextBox.Text);
        }

        protected void UsersGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userID = Convert.ToInt32(UsersGridView.DataKeys[e.RowIndex].Value);
            string query = "DELETE FROM Users WHERE UserID = @UserID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userID)
            };
            Database.ExecuteNonQuery(query, parameters);
            BindUsers();
        }

        protected void UsersGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int userID = Convert.ToInt32(UsersGridView.DataKeys[e.NewEditIndex].Value);
            Response.Redirect($"EditUser.aspx?UserID={userID}");
        }

        protected void UsersGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UsersGridView.PageIndex = e.NewPageIndex;
            BindUsers(SearchUsersTextBox.Text);
        }
    }
}