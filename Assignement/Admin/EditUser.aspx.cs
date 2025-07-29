
using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EduSphere.Admin
{
    public partial class EditUser : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get user ID from query string
                if (Request.QueryString["UserID"] != null && int.TryParse(Request.QueryString["UserID"], out int userID))
                {
                    LoadUserData(userID);
                }
                else
                {
                    // Redirect to manage users if no user ID is provided
                    Response.Redirect("ManageUsers.aspx");
                }
            }
        }

        private void LoadUserData(int userID)
        {
            User user = User.GetUserByID(userID);

            if (user != null)
            {
                UserIDLabel.Text = user.UserID.ToString();
                UsernameTextBox.Text = user.Username;
                EmailTextBox.Text = user.Email;
                RoleDropDownList.SelectedValue = user.Role;
                FirstNameTextBox.Text = user.FirstName;
                LastNameTextBox.Text = user.LastName;
                RegistrationDateLabel.Text = user.RegistrationDate.ToString("yyyy-MM-dd");
            }
            else
            {
                // User not found, redirect to manage users
                Response.Redirect("ManageUsers.aspx");
            }
        }

        protected void UpdateUserButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Get user ID from the label
                if (int.TryParse(UserIDLabel.Text, out int userID))
                {
                    // Check if username already exists (excluding current user)
                    string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND UserID <> @UserID";
                    SqlParameter[] checkUserParams = new SqlParameter[]
                    {
                        new SqlParameter("@Username", UsernameTextBox.Text),
                        new SqlParameter("@UserID", userID)
                    };
                    int userCount = Convert.ToInt32(Database.ExecuteScalar(checkUserQuery, checkUserParams));

                    if (userCount > 0)
                    {
                        ShowErrorMessage("Username already exists. Please choose a different username.");
                        return;
                    }

                    // Check if email already exists (excluding current user)
                    string checkEmailQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND UserID <> @UserID";
                    SqlParameter[] checkEmailParams = new SqlParameter[]
                    {
                        new SqlParameter("@Email", EmailTextBox.Text),
                        new SqlParameter("@UserID", userID)
                    };
                    int emailCount = Convert.ToInt32(Database.ExecuteScalar(checkEmailQuery, checkEmailParams));

                    if (emailCount > 0)
                    {
                        ShowErrorMessage("Email already exists. Please use a different email address.");
                        return;
                    }

                    // Update user in database
                    string updateQuery = @"UPDATE Users SET 
                                        Username = @Username, 
                                        Email = @Email, 
                                        Role = @Role, 
                                        FirstName = @FirstName, 
                                        LastName = @LastName 
                                        WHERE UserID = @UserID";

                    SqlParameter[] updateParams = new SqlParameter[]
                    {
                        new SqlParameter("@Username", UsernameTextBox.Text),
                        new SqlParameter("@Email", EmailTextBox.Text),
                        new SqlParameter("@Role", RoleDropDownList.SelectedValue),
                        new SqlParameter("@FirstName", FirstNameTextBox.Text),
                        new SqlParameter("@LastName", LastNameTextBox.Text),
                        new SqlParameter("@UserID", userID)
                    };

                    int rowsAffected = Database.ExecuteNonQuery(updateQuery, updateParams);

                    if (rowsAffected > 0)
                    {
                        // Redirect to manage users page
                        Response.Redirect("ManageUsers.aspx");
                    }
                    else
                    {
                        ShowErrorMessage("Failed to update user. Please try again.");
                    }
                }
                else
                {
                    ShowErrorMessage("Invalid user ID.");
                }
            }
        }

        private void ShowErrorMessage(string message)
        {
            // Create a panel to display the error message
            Panel errorPanel = new Panel();
            errorPanel.CssClass = "alert alert-danger alert-dismissible fade show";
            errorPanel.Attributes.Add("role", "alert");

            Literal messageLiteral = new Literal();
            messageLiteral.Text = message;

            Button closeButton = new Button();
            closeButton.Text = "×";
            closeButton.CssClass = "btn-close";
            closeButton.Attributes.Add("data-bs-dismiss", "alert");
            closeButton.Attributes.Add("aria-label", "Close");

            errorPanel.Controls.Add(messageLiteral);
            errorPanel.Controls.Add(closeButton);

            // Add the error panel to the page
            this.Controls.AddAt(0, errorPanel);
        }
    }
}