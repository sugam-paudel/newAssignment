using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EduSphere.Admin
{
    public partial class AddUser : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void AddUserButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (PasswordTextBox.Text != ConfirmPasswordTextBox.Text)
                {
                    // This should be caught by the CompareValidator, but just in case
                    ShowErrorMessage("Passwords do not match.");
                    return;
                }

                // Check if username already exists
                string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                SqlParameter[] checkUserParams = new SqlParameter[]
                {
                    new SqlParameter("@Username", UsernameTextBox.Text)
                };
                int userCount = Convert.ToInt32(Database.ExecuteScalar(checkUserQuery, checkUserParams));

                if (userCount > 0)
                {
                    ShowErrorMessage("Username already exists. Please choose a different username.");
                    return;
                }

                // Check if email already exists
                string checkEmailQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                SqlParameter[] checkEmailParams = new SqlParameter[]
                {
                    new SqlParameter("@Email", EmailTextBox.Text)
                };
                int emailCount = Convert.ToInt32(Database.ExecuteScalar(checkEmailQuery, checkEmailParams));

                if (emailCount > 0)
                {
                    ShowErrorMessage("Email already exists. Please use a different email address.");
                    return;
                }

                // Create new user
                User newUser = new User
                {
                    Username = UsernameTextBox.Text,
                    Password = PasswordTextBox.Text, // In production, hash this password
                    Email = EmailTextBox.Text,
                    Role = RoleDropDownList.SelectedValue,
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text
                };

                // Insert user into database
                bool success = User.Register(newUser);

                if (success)
                {
                    // Redirect to manage users page
                    Response.Redirect("ManageUsers.aspx");
                }
                else
                {
                    ShowErrorMessage("Failed to add user. Please try again.");
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