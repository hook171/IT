using domain.Models;

namespace domain.IRepositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        IEnumerable <Doctor> GetDoctorsBySpec (Specialization specialization);
    }
}