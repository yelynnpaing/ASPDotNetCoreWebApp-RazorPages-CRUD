using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ASPDotNetCoreWebApp_RazorPages_CRUD.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string ErrorMessage = "";
        public string SuccessMessage = "";
        
        public void OnGet()
        {
            string id = Request.Query["id"];

            string connnectionString = "Data Source=DESKTOP-L3SMK21\\SQLEXPRESS;Initial Catalog=ASPDotNetCoreWebApp(RazorPages)CRUDDb;User ID=sa;Password=sasa@123;Trust Server Certificate=True";
            using(SqlConnection connection = new SqlConnection(connnectionString))
            {
                connection.Open();
                string query = @"SELECT * FROM clients WHERE id=@id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            clientInfo.id = ""+ reader.GetInt32(0);
                            clientInfo.name = reader.GetString(1);
                            clientInfo.email = reader.GetString(2);
                            clientInfo.phone = reader.GetString(3);
                            clientInfo.address = reader.GetString(4);
                        }
                    }

                }
            }
        }

        public void OnPost()
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if(clientInfo.name.Length == 0 | clientInfo.email.Length == 0 | clientInfo.phone.Length == 0 | clientInfo.address.Length == 0)
            {
                ErrorMessage = "All fields are require!";
                return;
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-L3SMK21\\SQLEXPRESS;Initial Catalog=ASPDotNetCoreWebApp(RazorPages)CRUDDb;User ID=sa;Password=sasa@123;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"UPDATE clients SET name=@name, email=@email, phone=@phone, address=@address
                                    WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", clientInfo.name);
                        cmd.Parameters.AddWithValue("@email", clientInfo.email);
                        cmd.Parameters.AddWithValue("@phone", clientInfo.phone);
                        cmd.Parameters.AddWithValue("@address", clientInfo.address);
                        cmd.Parameters.AddWithValue("@id", clientInfo.id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clients/Index");
        }
    }
}
