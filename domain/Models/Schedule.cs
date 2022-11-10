using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models
{
    internal class Schedule
    {
        public int DoctorId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Schedule(int doctorid, DateTime starttime, DateTime endtime)
        {
            DoctorId = doctorid;   
            StartTime = starttime;
            EndTime = endtime;
        }
    }
}
