using domain.Models;

namespace BD.Models
{
    public class UserModel
    {

        public int Id { get; set; }

        public string Phone { get; set; }

        public string FIO { get; set; }
        public Role Role { get; set; }

        public string Username;

        public string Password;
    }
}
