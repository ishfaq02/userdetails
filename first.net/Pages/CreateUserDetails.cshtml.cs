using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace first.net.Pages
{
    public class CreateUserDetailsModel : PageModel
    {

        public class UserDetails
        {
            public string Name;
            public string Email;
            public string Address;
            public string Description;
        }

        public UserDetails userdetails = new();
        public string errorMessage = "";
        public string successMessage = "";


        ////new error detail
        //[Required(ErrorMessage = "Name is required.")]
        //[StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        //public string Name { get; set; }

        //[Required(ErrorMessage = "Email is required.")]
        //[EmailAddress(ErrorMessage = "Invalid email address.")]
        //public string Email { get; set; }

        //[Required(ErrorMessage = "Address is required.")]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Address.")]
        //public string Address { get; set; }

        //[Required(ErrorMessage = "Description is required.")]
        //public string Description { get; set; }








        public void OnPost()
        {
            userdetails.Name = Request.Form["name"];
            userdetails.Email = Request.Form["email"];
            userdetails.Address = Request.Form["address"];
            userdetails.Description = Request.Form["description"];


            try
            {
                string connectionString = "Data Source=localhost\\SQLEXPRESS;Integrated Security=True";



                using (SqlConnection conn = new(connectionString))
                {
                    conn.Open();
                    string query = "Insert into UserDetails (Name, Email, Address, Description) values" +
                               "@Name, @Email, @Address, @Description";

                    using (SqlCommand command = new(query, conn))
                    {
                        command.Parameters.AddWithValue("@Name", userdetails.Name);
                        command.Parameters.AddWithValue("@Email", userdetails.Email);
                        command.Parameters.AddWithValue("@Address", userdetails.Address);
                        command.Parameters.AddWithValue("@Description", userdetails.Description);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {

                errorMessage = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            userdetails.Name = "";
            userdetails.Email = "";
            userdetails.Address = "";
            userdetails.Description = "";
            Response.Redirect("/Index");

        }
    }
}
