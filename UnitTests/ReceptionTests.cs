using Xunit;
using Moq;
using domain;
using System;
using System.Collections.Generic;

namespace ReceptionTests
{
	public class ReceptionTests
	{
        private readonly ReceptionService _receptionService;
        private readonly Mock<IDoctorRepository> _doctorRepository;
        private readonly Mock<IReceptionRepository> _receptionRepostitory;

        public ReceptionTests()
        {
            _receptionRepostitory = new Mock<IReceptionRepository>();
            _receptionService = new ReceptionService(_receptionRepostitory.Object,_doctorRepository.Object);
        }

        public Reception GetReception1()
        {
            return new Reception(1,DateTime.Now.AddMinutes(15), DateTime.Now.AddMinutes(30), 1,1);
        }
        public Reception GetReception2()
        {
            return new Reception(1,DateTime.Now.AddMinutes(45), DateTime.Now.AddMinutes(60), 1, 1);
        }
        public Reception GetReception3()
        {
            return new Reception(1,DateTime.Now.AddMinutes(75), DateTime.Now.AddMinutes(90), 1, 1);
        }

        [Fact]
        public void CreateReceptionSuccess()
        {
            List<Reception> test = new()
            {
                GetReception1(),
                GetReception2()
            };

            _doctorRepository.Setup(repository => repository.IsExists(It.Is<int>(id => id == 1))).Returns(true);
            _doctorRepository.Setup(repository => repository.GetItem(It.Is<int>(id => id == 1))).Returns(new Doctor(1,"FIO", new Specialization(1, "Doctor")));
            _receptionRepostitory.Setup(repository => repository.GetReceptionByDoctor(It.IsAny<Doctor>())).Returns(() => test);

            var result = _receptionService.CreateReception(GetReception3());

            Assert.True(result.Success);
        }

        public void CreateReceptionFail()
        {
            List<Reception> test = new()
            {
                GetReception1(),
                GetReception3()
            };

            _doctorRepository.Setup(repository => repository.IsExists(It.Is<int>(id => id == 1))).Returns(true);
            _doctorRepository.Setup(repository => repository.GetItem(It.Is<int>(id => id == 1))).Returns(new Doctor(1, "FIO", new Specialization(1, "Doctor")));
            _receptionRepostitory.Setup(repository => repository.GetReceptionByDoctor(It.IsAny<Doctor>())).Returns(() => test);

            var result = _receptionService.CreateReception(GetReception2());

            Assert.True(result.IsFailure);
            Assert.Equal("This doctor is busy on this date ", result.Error);
        }

        [Fact]
        public void CreateReceptionDoctorIsNotExists()
        {
            _doctorRepository.Setup(repository => repository.IsExists(It.Is<int>(id => id == 1))).Returns(false);
            _doctorRepository.Setup(repository => repository.GetItem(It.Is<int>(id => id == 1))).Returns(new Doctor(1, "FIO", new Specialization(1, "Doctor")));

            var result = _receptionService.CreateReception(GetReception1());

            Assert.True(result.IsFailure);
            Assert.Equal("Doctor is not exists ", result.Error);
        }

        [Fact]
        public void GetFreeBySpecSuccess()
        {
            var x = _receptionService.GetFreeBySpec(new Specialization(1, "Doctor"), new Schedule(1, 1, DateTime.Today, DateTime.Today, new DateOnly()));
            Assert.True(x.Success);
        }

    }
}