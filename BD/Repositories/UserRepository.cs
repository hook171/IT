using domain.IRepositories;
using domain.Models;
using BD.Convert;
namespace BD.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationContext _bd;

        public UserRepository(ApplicationContext bd)
        {
            _bd = bd;
        }

        public bool IsExist(string login)
        {
            return _bd.Users.Any(user => user.Username == login);
        }
        public bool IsExist(string login, string password)
        {
            return _bd.Users.Any(user => user.Username == login && user.Password == password);
        }

        public IEnumerable<User> GetAll()
        {
            return _bd.Users.Select(t => t.ToDomain()).ToList();
        }

        public bool IsValid(User user)
        {
            if (user.Id < 0)
                return false;

            if (string.IsNullOrEmpty(user.Username)|| string.IsNullOrEmpty(user.Password))
                return false;

            if (string.IsNullOrEmpty(user.Phone)|| string.IsNullOrEmpty(user.FIO))
                return false;

            
            return true;
        }

        public User? GetByLogin(string login)
        {
            var user = _bd.Users.FirstOrDefault(user => user.Username == login);
            return user?.ToDomain();
        }

        public User? GetItem(int id)
        {
            var user = _bd.Users.FirstOrDefault(user => user.Id == id);
            return user?.ToDomain();
        }

        public User Create(User user)
        {
            _bd.Users.Add(user.ToModel());
            return user;
        }

        public User Update(User user)
        {
            _bd.Users.Update(user.ToModel());
            return user;
        }

        public bool Delete(int id)
        {
            var user = _bd.Users.FirstOrDefault(user => user.Id == id);
            if (user == default)
            {
                return false;
            }
            _bd.Users.Remove(user);
            return true;
        }

        public bool IsExists(int id)
        {
            return _bd.Users.Any(user => user.Id == id);
        }
    }
}
