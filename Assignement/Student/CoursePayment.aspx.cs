using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using EduSphere.Models; // Assuming you have a Models folder for your data classes

namespace EduSphere.Student
{
    public partial class CoursePayment : Page
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EduSphereDB"].ConnectionString;
        private int courseId;
        private Course course;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get course ID from query string
                if (int.TryParse(Request.QueryString["CourseID"], out courseId))
                {
                    LoadCourseDetails();
                    CalculatePaymentDetails();
                }
                else
                {
                    // Redirect to courses page if no course ID provided
                    Response.Redirect("~/Courses.aspx");
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
                            Price = Convert.ToDecimal(reader["Price"]),
                            Category = reader["Category"].ToString(),
                            Difficulty = reader["Difficulty"].ToString(),
                            Duration = Convert.ToInt32(reader["Duration"]),
                            Instructor = reader["Instructor"].ToString()
                        };

                        CourseTitleLabel.Text = course.Title;
                        CourseFeeLabel.Text = course.Price.ToString("C");
                    }
                    else
                    {
                        // Course not found
                        Response.Redirect("~/Courses.aspx");
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

        private void CalculatePaymentDetails()
        {
            // For simplicity, we'll assume no discount and 10% tax
            decimal courseFee = course.Price;
            decimal discount = 0; // Could be calculated based on promotions
            decimal taxRate = 0.1m; // 10% tax
            decimal tax = courseFee * taxRate;
            decimal totalAmount = courseFee - discount + tax;

            DiscountLabel.InnerText = discount.ToString("C");
            TaxLabel.InnerText = tax.ToString("C");
            TotalAmountLabel.Text = totalAmount.ToString("C");
        }

        protected void PayButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    // Process payment (in a real application, this would integrate with a payment gateway)
                    bool paymentSuccess = ProcessPayment();

                    if (paymentSuccess)
                    {
                        // Enroll student in course
                        EnrollStudent();

                        // Redirect to confirmation page
                        Response.Redirect($"PaymentConfirmation.aspx?CourseID={courseId}&Success=true");
                    }
                    else
                    {
                        // Show payment failure message
                        ErrorPanel.Visible = true;
                        ErrorMessageLabel.Text = "Payment failed. Please try again.";
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                    ErrorPanel.Visible = true;
                    ErrorMessageLabel.Text = "Error processing payment: " + ex.Message;
                }
            }
        }

        private bool ProcessPayment()
        {
            // In a real application, this would integrate with a payment gateway like Stripe, PayPal, etc.
            // For demo purposes, we'll simulate a successful payment
            return true;
        }

        private void EnrollStudent()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_EnrollStudent", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Get current user ID (assuming authentication is implemented)
                    int studentId = GetCurrentStudentId();

                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    cmd.Parameters.AddWithValue("@EnrollmentDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PaymentAmount", course.Price);
                    cmd.Parameters.AddWithValue("@TransactionID", Guid.NewGuid().ToString());

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log error
                throw new Exception("Error enrolling student: " + ex.Message);
            }
        }

        private int GetCurrentStudentId()
        {
            // In a real application, this would get the current user's ID from the authentication system
            // For demo purposes, we'll return a hardcoded value
            return 1;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to course details page
            Response.Redirect($"CourseDetails.aspx?CourseID={courseId}");
        }

        protected void ContactSupportButton_Click(object sender, EventArgs e)
        {
            // Redirect to support page
            Response.Redirect("~/Support.aspx");
        }
    }
}