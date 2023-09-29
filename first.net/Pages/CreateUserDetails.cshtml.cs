
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace first.net.Pages
{
    public class CreateUserDetailsModel : PageModel
    {

        public class UserDetails
        {
            public string name;
            public string email;
            public string address;
            public string description;
        }

        public UserDetails userdetails = new();
        public string errorMessage = "";
        public string successMessage = "";



        public void OnPost()
        {
            userdetails.name = Request.Form["name"];
            userdetails.email = Request.Form["email"];
            userdetails.address = Request.Form["address"];
            userdetails.description = Request.Form["description"];


            try
            {
                string connectionString = "put your data source ";



                using (SqlConnection conn = new(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO userDetails (name, email, address, description)" +
                                   "VALUES (@name, @email, @address, @description)";

                    using (SqlCommand command = new(query, conn))
                    {
                        command.Parameters.AddWithValue("@name", userdetails.name);
                        command.Parameters.AddWithValue("@email", userdetails.email);
                        command.Parameters.AddWithValue("@address", userdetails.address);
                        command.Parameters.AddWithValue("@description", userdetails.description);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            userdetails.name = "";
            userdetails.email = "";
            userdetails.address = "";
            userdetails.description = "";
            Response.Redirect("/Index");

        }
    }
}
