using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Schedule(int id, int doctorid, DateTime starttime, DateTime endtime)
        {
            Id = id;
            DoctorId = doctorid;   
            StartTime = starttime;
            EndTime = endtime;
        }
    }
}
