using domain.Models;
using BD.Models;

namespace BD.Convert
{
    public static class ScheduleConvert
    {
        public static Schedule? ToDomain(this ScheduleModel entity)
        {
            return new Schedule(

                entity.Id,
                entity.DoctorId,
                entity.StartTime,
                entity.EndTime,
                entity.Date);
        }

        public static ScheduleModel ToModel(this Schedule entity)
        {
            return new ScheduleModel
            {
                Id = entity.Id,
                DoctorId = entity.DoctorId,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Date = entity.Date,
            };
        }
    }
}