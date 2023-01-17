using domain.Models;
using domain.IRepositories;

namespace domain.Services
{
    public class ScheduleService
    {
        private readonly IScheduleRepository _db;
        private IDoctorRepository _docDB;

        public ScheduleService(IScheduleRepository db, IDoctorRepository docDB)  
        {
            _db = db;
            _docDB = docDB;
        }

        public Result<IEnumerable<Schedule>> GetByDoctor(Doctor doctor, DateOnly date)
        {
            if (!_docDB.IsExists(doctor.Id))
                return Result.Fail<IEnumerable<Schedule>>("Doctor is not exists.");
            if (!_docDB.IsValid(doctor))
                return Result.Fail<IEnumerable<Schedule>>("Doctor is not valid");

            return Result.Ok<IEnumerable<Schedule>>(_db.GetSheduleByDate(doctor, date));
        }

        public Result<Schedule> Add(Schedule schedule)
        {
            if (_db.IsExists(schedule.Id))
                return Result.Fail<Schedule>("Schedule is already exists");

            _db.Create(schedule);
            return Result.Ok<Schedule>(schedule);
        }

        public Result<Schedule> Update(Schedule schedule)
        {
            if (!_db.IsExists(schedule.Id))
                return Result.Fail<Schedule>("Schedule is not exists");

            _db.Update(schedule);
            return Result.Ok<Schedule>(schedule);
        }

        public Result<Schedule> Delete(Schedule schedule)
        {
            if (!_db.IsExists(schedule.Id))
                return Result.Fail<Schedule>("Schedule is not exists");

            _db.Delete(schedule.Id);
            return Result.Ok<Schedule>(schedule);
        }
    }
}