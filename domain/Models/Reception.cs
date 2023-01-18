namespace domain.Models
{
    public class Reception
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public Reception(int id, DateTime starttime,DateTime endtime, int patientid, int doctorid)
        {
            Id = id;
            StartTime = starttime;
            EndTime = endtime;
            PatientID = patientid;
            DoctorID = doctorid;
        }
    }
}
