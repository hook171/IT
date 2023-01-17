using domain.Models;
using domain.IRepositories;

namespace domain.Services
{
    public class DoctorService
    {
        private readonly IDoctorRepository _db;

        public DoctorService(IDoctorRepository db)
        {
            _db = db;
        }

        public Result<IEnumerable<Doctor>> GetAllDoctors()
        {
            return Result.Ok(_db.GetAll());
        }

        public Result<Doctor> CreateDoctor(Doctor doctor)
        {
            if (string.IsNullOrEmpty(doctor.Fio))
                return Result.Fail<Doctor>("Doctor's name entered incorrectly");

            if (_db.IsExists(doctor.Id))
                return Result.Fail<Doctor>("Doctor alredy exists");

            _db.Create(doctor);
            return Result.Ok(doctor);
        }

        public Result<Doctor> FindDoctorById(int id)
        {
            if (id < 0)
                return Result.Fail<Doctor>("Invalid id");

            return Result.Ok(_db.GetItem(id));
        }

        public Result<IEnumerable<Doctor>> GetBySpec(Specialization specialization)
        {
            return Result.Ok(_db.GetDoctorsBySpec(specialization));
        }

        public Result DeleteDoctor(int id)
        {
            if (!_db.IsExists(id))
                return Result.Fail<Doctor>("Invalid doctor");

            _db.Delete(id);
            return Result.Ok();
        }
    }
}