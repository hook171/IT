using domain.IRepositories;
using domain.Models;
using BD.Convert;
using BD.Models;
namespace BD.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ApplicationContext _bd;

        public ScheduleRepository(ApplicationContext bd)
        {
            _bd = bd;
        }

        public IEnumerable<Schedule?> GetAll()
        {
            var shedules = _bd.Schedules.ToList().Select(schedule => schedule.ToDomain()).ToList();
            return shedules;
        }

        public Schedule? GetItem(int id)
        {
            return _bd.Schedules.FirstOrDefault(t => t.Id == id).ToDomain();
        }

        public Schedule Create(Schedule schedule)
        {
            _bd.Schedules.Add(schedule.ToModel());
            return schedule;
        }

        public Schedule Update(Schedule schedule)
        {
            _bd.Schedules.Update(schedule.ToModel());
            return schedule;
        }

        public bool Delete(int id)
        {
            var schedule = _bd.Schedules.FirstOrDefault(t => t.Id == id);
            if (schedule == default)
            {
                return false;
            }
            _bd.Schedules.Remove(schedule);
            return true;
        }

        public bool IsExists(int id)
        {
            return _bd.Schedules.Any(schedule => schedule.Id == id);
        }

        public bool IsValid(Schedule schedule)
        {
            if (schedule.Id < 0)
                return false;

            if (schedule.StartTime >= schedule.EndTime)
                return false;

            return true;
        }

        public IEnumerable<Schedule> GetScheduleByDate(Doctor doctor, DateOnly date)
        {
            return _bd.Schedules.Where(t => t.DoctorId == doctor.Id && t.Date == date).Select(t => t.ToDomain());
        }
    }
}