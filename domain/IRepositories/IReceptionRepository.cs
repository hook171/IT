using domain.Models;

namespace domain.IRepositories
{
    public interface IReceptionRepository : IRepository<Reception>

    {
        IEnumerable<Reception> GetReceptionByDoctor(Doctor doctor);

        IEnumerable<Reception> GetReceptionBySpec(Specialization specialization);

        IEnumerable<DateTime> GetFreeReceptionBySpec(Specialization specialization, Schedule shedule);

    }
}