using domain.Models;
using domain.IRepositories;

namespace domain.Services
{
    public class ReceptionService
    {
        private IReceptionRepository _db;
        private IDoctorRepository _docDB;


        public ReceptionService(IReceptionRepository db, IDoctorRepository docDB)
        {
            _db = db;
            _docDB = docDB;
        }

        public Result<Reception> CreateReception(Reception reception)
        {
            var doctor = _docDB.GetItem(reception.DoctorID);
            var list = _db.GetReceptionByDoctor(doctor);

            if (list.Any(x => reception.StartTime < x.EndTime && reception.EndTime > x.StartTime))
                return Result.Fail<Reception>("Doctor is busy in this date");

            _db.Create(reception);
            return Result.Ok(reception);
        }

        public Result<Reception> CreateReception(DateTime dateTime, Specialization specialization)
        {
            var list = _db.GetReceptionBySpec(specialization);
            if (list.Any(x => dateTime < x.EndTime && dateTime.AddMinutes(60) > x.StartTime))
                return Result.Fail<Reception>("There are no doctors available at this time.");

            var reception = _db.CreateBySpec(dateTime, specialization);
            _db.Create(reception);
            return Result.Ok(reception);
        }


        public Result<IEnumerable<DateTime>> GetFreeBySpec(Specialization specialization, Schedule shedule)
        {
            var reception = _db.GetFreeReceptionBySpec(specialization, shedule);
            return Result.Ok(reception);
        }
    }
}