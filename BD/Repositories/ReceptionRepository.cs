using domain.IRepositories;
using domain.Models;
using BD.Convert;
namespace BD.Repositories
{
    public class ReceptionRepository : IReceptionRepository
    {
        private readonly ApplicationContext _bd;

        public ReceptionRepository(ApplicationContext bd)
        {
            _bd = bd;
        }

        IEnumerable<Reception> IReceptionRepository.GetReceptionByDoctor(Doctor doctor)
        {
            return _bd.Receptions.Where(t => t.DoctorID == doctor.Id).Select(t => t.ToDomain()).ToList();
        }

        public IEnumerable<DateTime> GetFreeReceptionBySpec(Specialization specialization, Schedule schedule)
        {
            var doctors = _bd.Doctors.Where(t => t.Spec.Id == specialization.Id && t.Id == schedule.DoctorId);
            var receptions = _bd.Receptions.Where(t => doctors.Any(z => z.Id == t.Id)).Select(t => t.StartTime);
            var result_arr = new List<DateTime>();

            for (DateTime i = schedule.StartTime; i < schedule.EndTime; i.AddMinutes(30))
            {
                if (receptions.All(t => t != i))
                    result_arr.Append(i);
            }
            return result_arr;
        }

        IEnumerable<Reception> IReceptionRepository.GetReceptionBySpec(Specialization spec)
        {
            var doctors = _bd.Doctors.Where(t => t.Spec.Id == spec.Id);

            return _bd.Receptions.Where(t => doctors.Any(z => t.Id == z.Id)).Select(t => t.ToDomain()).ToList();
        }

        public IEnumerable<Reception> GetAll()
        {
            return _bd.Receptions.Select(t => t.ToDomain()).ToList();
        }

        public Reception? GetItem(int id)
        {
            return _bd.Receptions.FirstOrDefault(t => t.Id == id).ToDomain();
        }

        public Reception Create(Reception reception)
        {
            _bd.Receptions.Add(reception.ToModel());
            return reception;
        }

        public Reception Update(Reception reception)
        {
            _bd.Receptions.Update(reception.ToModel());
            return reception;
        }

        public bool Delete(int id)
        {
            var rec = _bd.Receptions.FirstOrDefault(t => t.Id == id);
            if (rec == default)
                return false;
            _bd.Receptions.Remove(rec);
            return true;
        }

        public bool IsExists(int id)
        {
            return _bd.Receptions.Any(t => t.Id == id);
        }

        public bool IsValid(Reception reception)
        {
            if (reception.Id < 0)
                return false;

            if (reception.StartTime >= reception.EndTime)
                return false;

            return true;
        }
    }
}