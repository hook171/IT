using Xunit;
using Moq;
using domain;
using Castle.Core.Smtp;
using System.Collections.Generic;

namespace DoctorTests
{
	public class DoctorTests
	{

		private readonly DoctorService _doctorService;
		private readonly Mock<IDoctorRepository> _doctorRepository;

		public DoctorTests()
		{
			_doctorRepository = new Mock<IDoctorRepository>();
			_doctorService = new DoctorService(_doctorRepository.Object);
		}

		public Doctor GetDoctor(string name = "Alex Alexandrov")
		{
			return new Doctor(1, name, new Specialization(1, "Surgeon"));
		}

		[Fact]
		public void GetAll()
		{
			var result = _doctorService.GetAllDoctors();

			Assert.True(result.Success);
		}

		[Fact]
		public void CreateSuccefull()
		{
			_doctorRepository.Setup(repository => repository.IsExists(It.Is<int>(id => id == 1))).Returns(false);
			_doctorRepository.Setup(repository => repository.IsValid(It.IsAny<Doctor>())).Returns(true);

			var result = _doctorService.CreateDoctor(GetDoctor());

			Assert.True(result.Success);
		}

		[Fact]
		public void CreateAlreadyExists()
		{
			_doctorRepository.Setup(repository => repository.IsExists(It.Is<int>(id => id == 1))).Returns(true);
			_doctorRepository.Setup(repository => repository.IsValid(It.IsAny<Doctor>())).Returns(true);

			var result = _doctorService.CreateDoctor(GetDoctor());

			Assert.True(result.IsFailure);
			Assert.Equal("Doctor already is exists ", result.Error);
		}

		[Fact]
		public void GetByIdSuccefull()
		{
			_doctorRepository.Setup(repository => repository.IsExists(It.Is<int>(id => id == 1)))
				.Returns(true);

			var result = _doctorService.FindDoctorById(1);

			Assert.True(result.Success);
		}

		[Fact]
		public void GetByIdNotExists()
		{
			_doctorRepository.Setup(repository => repository.IsExists(It.Is<int>(id => id == 1))).Returns(false);

			var result = _doctorService.FindDoctorById(1);

			Assert.False(result.Success);
			Assert.Equal("Doctor not found ", result.Error);
		}

		[Fact]
		public void GetBySpecSuccefull()
		{
			var result = _doctorService.GetBySpec(new Specialization(1, "Alex Alexandrov"));
			Assert.True(result.Success);
		}


		[Fact]
		public void DeleteIdNotFound()
		{
			List<Reception> apps = new();

			var result = _doctorService.DeleteDoctor(0);

			Assert.True(result.IsFailure);
			Assert.Equal("Doctor not found ", result.Error);
		}

		[Fact]
		public void DeleteSuccess()
		{
			_doctorRepository.Setup(repository => repository.IsExists(It.Is<int>(id => id == 1))).Returns(true);
			_doctorRepository.Setup(repository => repository.IsValid(It.IsAny<Doctor>())).Returns(true);

			var result = _doctorService.DeleteDoctor(GetDoctor().Id);

			Assert.True(result.Success);
		}

		public void DeleteError()
		{
			_doctorRepository.Setup(repository => repository.GetItem(It.IsAny<int>())).Returns(() => new Doctor(0, "a", new Specialization(0,"DSFAFAFS")));
			_doctorRepository.Setup(repository => repository.Delete(It.IsAny<int>())).Returns(() => false);

			var result = _doctorService.DeleteDoctor(0);

			Assert.True(result.IsFailure);
			Assert.Equal("Unable to delete doctor ", result.Error);
		}

	}
}