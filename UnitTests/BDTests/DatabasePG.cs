using BD.Repositories;
using BD;
using BD.Convert;
using BD.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UnitTests.BDTests
{
    public class EfPlayground
    {
        private readonly DbContextOptionsBuilder<ApplicationContext> _optionsBuilder;

        public EfPlayground()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseNpgsql(
                $"Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=325991");
            _optionsBuilder = optionsBuilder;
        }

        public Specialization GetSpecialization()
        {
            return new Specialization(99, "Хирург");
        }

        [Fact]
        public void PlaygroundMethod1()
        {
            using var context = new ApplicationContext(_optionsBuilder.Options);
            context.Users.Add(new UserModel
            {
                Id = 55,
                Phone = "899988877666",
                FIO = "Alex Alex",
                Role = Role.Patient,
                Username = "username",
                Password = "password",

            });
            context.SaveChanges();

            Assert.True(context.Users.Any(u => u.Username == "username"));
        }

        [Fact]
        public void PlaygroundMethod2()
        {
            var context = new ApplicationContext(_optionsBuilder.Options);
            var userRep = new UserRepository(context);

            userRep.Create(new User("username2", "password", 1, "89998887766", "Maxim Maxim", Role.Patient));

            context.SaveChanges();

            Assert.True(context.Users.Any(u => u.Username == "username2"));
        }

        [Fact]
        public void PlaygroundMethod3()
        {
            using var context = new ApplicationContext(_optionsBuilder.Options);
            context.Doctors.Add(new DoctorModel
            {
                Id = 5,
                Fio = "Anton Anton",
                Spec = GetSpecialization().ToModel(),

            });

            context.SaveChanges();
            Assert.True(context.Doctors.Any(u => u.Fio == "Anton Anton"));
        }
    }
}