namespace BD.Models
{
    public class DoctorModel
    {

        public int Id { get; set; }

        public string Fio { get; set; }

        public SpecializationModel Spec { get; set; }
    }
}
