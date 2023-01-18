using domain.Models;
using BD.Models;

namespace BD.Convert
{
    public static class UserConvert
    {
        public static User? ToDomain(this UserModel entity)
        {
            return new User(

                entity.Username,
                entity.Password,
                entity.Id,
                entity.Phone,
                entity.FIO,
                entity.Role);
        }

        public static UserModel ToModel(this User entity)
        {
            return new UserModel
            {
                Username = entity.Username,
                Password = entity.Password,
                Id = entity.Id,
                Phone = entity.Phone,
                FIO = entity.FIO,
                Role = entity.Role,
            };
        }
    }
}