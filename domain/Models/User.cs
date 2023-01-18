using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Phone { get; set; }

        public string FIO { get; set; }

        public Role Role { get; set; }

        public string Username;

        public string Password;

        public User(string username, string password, int id, string phone, string fio, Role role)
        {
            Username = username;
            Password = password;
            Id = id;
            Phone = phone;
            FIO = fio;
            Role = role;
        }

    }
}
