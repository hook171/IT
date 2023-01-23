using System.Text.Json.Serialization;
using domain.Models;
namespace WebProject.Views
{
    public class DoctorSearchView
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("FIO")]
        public string Fio { get; set; }

        [JsonPropertyName("spec")]
        public Specialization Spec { get; set; }

    }
}
