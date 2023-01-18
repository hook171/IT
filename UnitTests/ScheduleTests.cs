using Xunit;
using Moq;
using domain;
using System;


namespace ScheduleTests
{
	public class ScheduleTests
	{
		private readonly ScheduleService _scheduleService;
		private readonly Mock<IDoctorRepository> _doctorRepository;
		private readonly Mock<IScheduleRepository> _scheduleRepository;

		public ScheduleTests()
		{
			_doctorRepository = new Mock<IDoctorRepository>();
			_scheduleService = new ScheduleService(_scheduleRepository.Object,_doctorRepository.Object);
		}

		[Fact]
		public void AddScheduleSuccess()
		{
			_scheduleRepository.Setup(repository => repository.Create(It.IsAny<Schedule>())).Returns(() => true);
			var date = new Schedule(1, 1, DateTime.Today, DateTime.Today, new DateOnly());
			var res = _scheduleService.Add(date);
			Assert.True(res.Success);
		}

		[Fact]
		public void AddScheduleFail()
		{
			_scheduleRepository.Setup(repository => repository.Create(It.IsAny<Schedule>())).Returns(() => false);

			var schedule = new Schedule(-1,-1, DateTime.Today, DateTime.Today, new DateOnly());
			var result = _scheduleService.Add(schedule);

			Assert.True(result.IsFailure);
			Assert.Contains("Invalid schedule ", result.Error);
		}

		[Fact]
		public void UpdateScheduleSuccess()
		{
			_scheduleRepository.Setup(repository => repository.Update(It.IsAny<Schedule>())).Returns(() => true);

			var schedule = new Schedule(1,1,DateTime.Today,DateTime.Today, new DateOnly());
			var result = _scheduleService.Update(schedule);

			Assert.True(result.Success);
		}

		[Fact]
		public void UpdateScheduleError()
		{
			_scheduleRepository.Setup(repository => repository.Update(It.IsAny<Schedule>())).Returns(() => false);

			var schedule = new Schedule(1, 1, DateTime.Today, DateTime.Today, new DateOnly());
			var result = _scheduleService.Update(schedule);

			Assert.True(result.IsFailure);
			Assert.Equal("Unable to update schedule ", result.Error);
		}

		[Fact]
		public void UpdateInvalidSchedule()
		{
			_scheduleRepository.Setup(repository => repository.Update(It.IsAny<Schedule>())).Returns(() => false);

			var schedule = new Schedule(1, 1, DateTime.Today, DateTime.Today, new DateOnly());
			var result = _scheduleService.Update(schedule);

			Assert.True(result.IsFailure);
			Assert.Contains("Invalid schedule ", result.Error);
		}

		[Fact]
		public void DeleteSuccefull()
		{
			_scheduleRepository.Setup(repository => repository.IsExists(It.Is<int>(id => id == 1))).Returns(true);

			var schedule = new Schedule(1, 1, DateTime.Today, DateTime.Today, new DateOnly());
			var result = _scheduleService.Delete(schedule);

			Assert.True(result.Success);
		}

		public void DeleteNotExists()
		{
			_scheduleRepository.Setup(repository => repository.IsExists(It.Is<int>(id => id == 1))).Returns(false);

			var schedule = new Schedule(1, 1, DateTime.Today, DateTime.Today, new DateOnly());
			var result = _scheduleService.Delete(schedule);

			Assert.True(result.IsFailure);
			Assert.Equal("No such schedule exists ", result.Error);
		}
	}
}