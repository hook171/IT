namespace domain.Models
{	
	public class Doctor
	{
		public int Id;
		public string Fio;
		public Specialization Specialization;
;
		public Doctor(int id, string fio, Specialization specialization)
		{
			Id = id;
			Fio = fio;
			Specialization = specialization;
		}

	}
}