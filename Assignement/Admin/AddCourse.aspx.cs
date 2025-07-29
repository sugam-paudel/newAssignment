using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace EduSphere.Admin
{
    public partial class AddCourse : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void AddCourseButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Get current user from session
                User currentUser = Session["User"] as User;
                if (currentUser == null)
                {
                    Response.Redirect("~/Login.aspx");
                    return;
                }

                // Handle image upload
                string imagePath = null;
                if (ImageFileUpload.HasFile)
                {
                    // Validate file size (max 5MB)
                    if (ImageFileUpload.PostedFile.ContentLength > 5 * 1024 * 1024)
                    {
                        ShowErrorMessage("Image file size must be less than 5MB.");
                        return;
                    }

                    // Create uploads directory if it doesn't exist
                    string uploadsFolder = Server.MapPath("~/Uploads/Courses");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generate unique filename
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFileUpload.FileName);
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    // Save file
                    ImageFileUpload.SaveAs(filePath);

                    // Set relative path for database
                    imagePath = "~/Uploads/Courses/" + fileName;
                }

                // Insert course into database
                string insertQuery = @"INSERT INTO Courses (Title, Description, Category, Difficulty, Duration, Price, ImagePath, CreatedBy, CreatedDate) 
                                    VALUES (@Title, @Description, @Category, @Difficulty, @Duration, @Price, @ImagePath, @CreatedBy, GETDATE())";

                SqlParameter[] insertParams = new SqlParameter[]
                {
                    new SqlParameter("@Title", TitleTextBox.Text),
                    new SqlParameter("@Description", DescriptionTextBox.Text),
                    new SqlParameter("@Category", CategoryDropDownList.SelectedValue),
                    new SqlParameter("@Difficulty", DifficultyDropDownList.SelectedValue),
                    new SqlParameter("@Duration", Convert.ToInt32(DurationTextBox.Text)),
                    new SqlParameter("@Price", Convert.ToDecimal(PriceTextBox.Text)),
                    new SqlParameter("@ImagePath", imagePath ?? (object)DBNull.Value),
                    new SqlParameter("@CreatedBy", currentUser.UserID)
                };

                int rowsAffected = Database.ExecuteNonQuery(insertQuery, insertParams);

                if (rowsAffected > 0)
                {
                    // Redirect to manage courses page
                    Response.Redirect("ManageCourses.aspx");
                }
                else
                {
                    ShowErrorMessage("Failed to add course. Please try again.");
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