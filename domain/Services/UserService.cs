using domain.Models;
using domain.IRepositories;

namespace domain.Services
{
    public class UserService
    {
        private readonly IUserRepository _db;

        public UserService(IUserRepository db)
        {
            _db = db;
        }
        public Result<User> CreateUser(User user)
        {
            if (!_db.IsValid(user))
                return Result.Fail<User>("User data is not valid");

            if (_db.IsExist(user.Username))
                return Result.Fail<User>("User with that username already exists");

            return Result.Ok<User>(user);
        }

        public Result<User> GetByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Result.Fail<User>("Empty login");

            if (!_db.IsExist(login))
                return Result.Fail<User>("User with this login doesn't exists");

            return Result.Ok<User>(_db.GetByLogin(login));

        }

        public Result<bool> CheckExist(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return Result.Fail<bool>("Empty login/password");

            return Result.Ok<bool>(_db.IsExist(login, password));
        }
    }
}