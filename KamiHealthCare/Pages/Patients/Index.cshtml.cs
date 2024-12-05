using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace KamiHealthCare.Pages.Patients
{
    public class IndexModel : PageModel
    {
        public List<PatientInfo> ListPatients = new List<PatientInfo>();

        public void OnGet()
        {
            ListPatients.Clear();
            try
            {
                string conString = "Data Source=DESKTOP-2042M6B\\SQLEXPRESS;Initial Catalog=KamiHeathCareDB;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection con = new SqlConnection(conString))  // Correct instantiation
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM Patients";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PatientInfo patientInfo = new PatientInfo
                                {
                                    id = reader.GetInt32(0).ToString(),                // Convert int to string
                                    name = reader.GetString(1),
                                    email = reader.GetString(2),
                                    phone = reader.GetString(3),
                                    address = reader.GetString(4),
                                    createdAt = reader.GetDateTime(5).ToString()       // Convert DateTime to string
                                };

                                ListPatients.Add(patientInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }

    public class PatientInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string createdAt { get; set; }
    }
}
