using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace KamiHealthCare.Pages.Patients
{
    public class EditModel : PageModel
    {
        public PatientInfo patientInfo = new PatientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=DESKTOP-2042M6B\\SQLEXPRESS;Initial Catalog=KamiHeathCareDB;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sqlQuery = "SELECT * FROM Patients WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader()) 
                        {
                            if(reader.Read())
                            {
                                patientInfo.id = "" + reader.GetInt32(0);
                                patientInfo.name = reader.GetString(1);
                                patientInfo.email =reader.GetString(2);
                                patientInfo.phone = reader.GetString(3);
                                patientInfo.address= reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }   
        }
        public void OnPost()
        {
            patientInfo.name = Request.Form["name"];
            patientInfo.email = Request.Form["email"];
            patientInfo.phone = Request.Form["phone"];
            patientInfo.address = Request.Form["address"];
        }

    }
}
