namespace domain.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateOnly Date { get; set; }

        public Schedule(int id, int doctorid, DateTime starttime, DateTime endtime, DateOnly date)
        {
            Id = id;
            DoctorId = doctorid;   
            StartTime = starttime;
            EndTime = endtime;
            Date = date;
        }
    }
}
