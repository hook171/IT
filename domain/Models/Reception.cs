    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain
{
    public class Reception
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public Reception(DateTime starttime,DateTime endtime, int patientid, int doctorid)
        {
            StartTime = starttime;
            EndTime = endtime;
            PatientID = patientid;
            DoctorID = doctorid;
        }
    }
}
