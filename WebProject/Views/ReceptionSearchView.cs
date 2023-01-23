using System.Text.Json.Serialization;
using domain.Models;
namespace WebProject.Views
{
    public class ReceptionSearchView
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("patient_id")]
        public int PatientID { get; set; }

        [JsonPropertyName("doctor_id")]
        public int DoctorID { get; set; }

    }
}
