using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Assignement
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT Role FROM Users WHERE (Username=@Username OR Email=@Username) AND Password=@Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                try
                {
                    conn.Open();
                    var role = cmd.ExecuteScalar();
                    if (role != null)
                    {
                        Session["Username"] = username;
                        Session["Role"] = role.ToString();

                        // Redirect based on role
                        if (role.ToString() == "Admin")
                            Response.Redirect("Admin/Dashboard.aspx");
                        else
                            Response.Redirect("Student/Dashboard.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid login credentials');</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Login error: " + ex.Message + "');</script>");
                }
            }
        }
    }
}
