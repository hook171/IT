using domain.Models;
using BD.Models;

namespace BD.Convert
{
    public static class ReceptionConvert
    {
        public static Reception? ToDomain(this ReceptionModel entity)
        {
            return new Reception(

                entity.Id,
                entity.StartTime,
                entity.EndTime,
                entity.PatientID,
                entity.DoctorID);
        }

        public static ReceptionModel ToModel(this Reception entity)
        {
            return new ReceptionModel
            {
                Id = entity.Id,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                PatientID = entity.PatientID,
                DoctorID = entity.DoctorID,
            };
        }
    }
}