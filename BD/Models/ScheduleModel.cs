﻿namespace BD.Models
{
    public class ScheduleModel
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateOnly Date { get; set; }

    }
}
