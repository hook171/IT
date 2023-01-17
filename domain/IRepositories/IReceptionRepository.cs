using domain.Models;

namespace domain.IRepositories
{
    public interface IReceptionRepository : IRepository<Reception>

    {
        IEnumerable<Reception> GetReceptionByDoctor(Doctor doctor);

        IEnumerable<Reception> GetReceptionBySpec(Specialization specialization);

        IEnumerable<DateTime> GetFreeReceptionBySpec(Specialization specialization, Schedule shedule);

        IEnumerable<Reception> GetReceptions(int doctorId);

        Reception CreateBySpec(DateTime dateTime, Specialization specialization);

    }
}