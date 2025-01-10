using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Common;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ASPDotNetCoreWebApp_RazorPages_CRUD.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-L3SMK21\\SQLEXPRESS;Initial Catalog=ASPDotNetCoreWebApp(RazorPages)CRUDDb;User ID=sa;Password=sasa@123;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM clients";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = ""+reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5);
                                listClients.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception : " + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public DateTime created_at { get; set; }    
    }
}
