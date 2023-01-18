using domain.Models;

namespace domain.IRepositories
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        IEnumerable<Schedule> GetScheduleByDate(Doctor doctor, DateOnly date);
}
}