namespace BD.Models
{
    public class ReceptionModel
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
    }
}
