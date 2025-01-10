using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ASPDotNetCoreWebApp_RazorPages_CRUD.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string ErrorMessage = "";
        public string SuccessMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if(clientInfo.name.Length == 0 | clientInfo.email.Length == 0 | clientInfo.phone.Length == 0 | clientInfo.address.Length == 0)
            {
                ErrorMessage = "All fileds are require!";
                return;
            }

            try
            {
                string connnectionString = "Data Source=DESKTOP-L3SMK21\\SQLEXPRESS;Initial Catalog=ASPDotNetCoreWebApp(RazorPages)CRUDDb;User ID=sa;Password=sasa@123;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connnectionString))
                {
                    connection.Open();
                    string query = @"INSERT INTO clients (name, email, phone, address)
                                VALUES (@name, @email, @phone, @address)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", clientInfo.name);
                        cmd.Parameters.AddWithValue("@email", clientInfo.email);
                        cmd.Parameters.AddWithValue("@phone", clientInfo.phone);
                        cmd.Parameters.AddWithValue("@address", clientInfo.address);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }

            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";
            SuccessMessage = "New client is successfully added.";
            Response.Redirect("/Clients/Index");
        }
    }
}
