using domain.IRepositories;
using domain.Models;
using BD.Convert;
namespace BD.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationContext _bd;

        public DoctorRepository(ApplicationContext bd)
        {
            _bd = bd;
        }

        public IEnumerable<Doctor> GetDoctorsBySpec(Specialization specialization)
        {
            return _bd.Doctors.Where(user => user.Spec == specialization.ToModel()).Select(t => t.ToDomain());
        }

        public IEnumerable<Doctor> GetAll()
        {

            return _bd.Doctors.Select(user => user.ToDomain()).ToList();
        }

        public Doctor? GetItem(int id)
        {
            var user = _bd.Doctors.FirstOrDefault(t => t.Id == id);
            return user?.ToDomain();
        }

        public Doctor Create(Doctor user)
        {
            _bd.Doctors.Add(user.ToModel());
            return user;
        }

        public Doctor Update(Doctor user)
        {
            _bd.Doctors.Update(user.ToModel());
            return user;
        }

        public bool Delete(int id)
        {
            var user = _bd.Doctors.FirstOrDefault(user => user.Id == id);
            if (user == default)
            {
                return false;
            }
            _bd.Doctors.Remove(user);
            return true;
        }

        public bool IsExists(int id)
        {
            return _bd.Doctors.Any(t => t.Id == id);
        }

        public bool IsValid(Doctor user)
        {
            if (user.Id < 0)
                return false;

            if (string.IsNullOrEmpty(user.Fio))
                return false;

            return true;
        }
    }
}