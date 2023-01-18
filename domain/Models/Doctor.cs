using domain.Services;

namespace domain.Models
{	
	public class Doctor
	{
		public int Id;

		public string Fio;

		public Specialization Spec;

		public Doctor(int id, string fio, Specialization specialization)
		{
			Id = id;
			Fio = fio;
			Spec = specialization;
		}

		public Result IsValid()
		{
			if (Id < 0)
				return Result.Fail("Invalid id");

			return Result.Ok();
		}
	}
}