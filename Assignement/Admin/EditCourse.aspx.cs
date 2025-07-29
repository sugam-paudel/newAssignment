using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.IO;

namespace EduSphere.Admin
{
    public partial class EditCourse : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get course ID from query string
                if (Request.QueryString["CourseID"] != null && int.TryParse(Request.QueryString["CourseID"], out int courseID))
                {
                    LoadCourseData(courseID);
                }
                else
                {
                    // Redirect to manage courses if no course ID is provided
                    Response.Redirect("ManageCourses.aspx");
                }
            }
        }

        private void LoadCourseData(int courseID)
        {
            string query = @"SELECT CourseID, Title, Description, Category, Difficulty, Duration, Price, ImagePath, CreatedDate 
                            FROM Courses 
                            WHERE CourseID = @CourseID";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CourseID", courseID)
            };

            using (SqlDataReader reader = Database.ExecuteReader(query, parameters))
            {
                if (reader.Read())
                {
                    CourseIDLabel.Text = reader["CourseID"].ToString();
                    TitleTextBox.Text = reader["Title"].ToString();
                    DescriptionTextBox.Text = reader["Description"].ToString();
                    CategoryDropDownList.SelectedValue = reader["Category"].ToString();
                    DifficultyDropDownList.SelectedValue = reader["Difficulty"].ToString();
                    DurationTextBox.Text = reader["Duration"].ToString();
                    PriceTextBox.Text = reader["Price"].ToString();
                    CreatedDateLabel.Text = Convert.ToDateTime(reader["CreatedDate"]).ToString("yyyy-MM-dd");

                    // Display current image if available
                    if (!reader.IsDBNull(reader.GetOrdinal("ImagePath")))
                    {
                        string imagePath = reader["ImagePath"].ToString();
                        CurrentCourseImage.ImageUrl = ResolveUrl(imagePath);
                        CurrentCourseImage.Visible = true;
                    }
                    else
                    {
                        CurrentImagePanel.Visible = false;
                    }
                }
                else
                {
                    // Course not found, redirect to manage courses
                    Response.Redirect("ManageCourses.aspx");
                }
            }
        }

        protected void UpdateCourseButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Get course ID from the label
                if (int.TryParse(CourseIDLabel.Text, out int courseID))
                {
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

                        // Set relative path