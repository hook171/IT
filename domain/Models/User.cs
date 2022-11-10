using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain
{
    public class User
    {
        public int Id { get; set; }

        public string Phone { get; set; }

        public string FIO { get; set; }

        public Role Role { get; set; }

        public User(int id, string phone, string fio, Role role, string username, string password)
        {
            Id = id;
            Phone = phone;
            FIO = fio;
            Role = role;    
            
        }

    }
}
