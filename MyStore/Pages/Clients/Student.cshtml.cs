using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class studentModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {

        }
        public void OnPost()
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.phone = Request.Form["phone"];
            if (clientInfo.id.Length == 0 || clientInfo.name.Length == 0 || clientInfo.phone.Length == 0)
            {
                errorMessage = "all the fields are required";
                return;
            }
            //save  data
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "insert into student" + "(id,name,phone) values" + "(@id,@name,@phone)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id",clientInfo.id);
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.ExecuteNonQuery();
                    }

                    
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            clientInfo.id = "";clientInfo.name = "";clientInfo.phone = "";
            successMessage = "New StudentAdded Correctly";
            Response.Redirect("/");
        }

    }
  
}
