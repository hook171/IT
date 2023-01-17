using domain.Models;

namespace domain.IRepositories
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        IEnumerable<Schedule> GetSheduleByDate(Doctor doctor, DateOnly date);
}
}