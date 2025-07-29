using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace EduSphere.Student
{
    public partial class PaymentConfirmation : Page
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EduSphereDB"].ConnectionString;
        private int courseId;
        private bool paymentSuccess;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get parameters from query string
                bool.TryParse(Request.QueryString["Success"], out paymentSuccess);
                int.TryParse(Request.QueryString["CourseID"], out courseId);

                if (paymentSuccess)
                {
                    SuccessPanel.Visible = true;
                    FailurePanel.Visible = false;

                    // Load course details and transaction info
                    LoadConfirmationDetails();
                }
                else
                {
                    SuccessPanel.Visible = false;
                    FailurePanel.Visible = true;

                    // Load error message if provided
                    string errorMessage = Request.QueryString["Error"];
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        ErrorMessageLabel.Text = errorMessage;
                    }
                    else
                    {
                        ErrorMessageLabel.Text = "Payment failed for unknown reasons.";
                    }
                }
            }
        }

        private void LoadConfirmationDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Get course details
                    SqlCommand cmd = new SqlCommand("sp_GetCourseDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseID", courseId);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        CourseNameLabel.Text = reader["Title"].ToString();
                    }
                    reader.Close();

                    // Get transaction details
                    int studentId = GetCurrentStudentId();
                    cmd = new SqlCommand("sp_GetLatestTransaction", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.Parameters.AddWithValue("@CourseID", courseId);

                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        TransactionIDLabel.Text = reader["TransactionID"].ToString();
                        AmountPaidLabel.Text = Convert.ToDecimal(reader["Amount"]).ToString("C");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                // Show error message to user
                ErrorPanel.Visible = true;
                ErrorMessageLabel.Text = "Error loading confirmation details: " + ex.Message;
            }
        }

        protected void GoToCourseButton_Click(object sender, EventArgs e)
        {
            // Redirect to the enrolled course page
            Response.Redirect($"EnrolledCourse.aspx?CourseID={courseId}");
        }

        protected void BrowseCoursesButton_Click(object sender, EventArgs e)
        {
            // Redirect to courses page
            Response.Redirect("~/Courses.aspx");
        }

        protected void RetryPaymentButton_Click(object sender, EventArgs e)
        {
            // Redirect back to payment page
            Response.Redirect($"CoursePayment.aspx?CourseID={courseId}");
        }

        protected void ContactSupportButton_Click(object sender, EventArgs e)
        {
            // Redirect to support page
            Response.Redirect("~/Support.aspx");
        }

        private int GetCurrentStudentId()
        {
            // In a real application, this would get the current user's ID from the authentication system
            // For demo purposes, we'll return a hardcoded value
            return 1;
        }
    }
}