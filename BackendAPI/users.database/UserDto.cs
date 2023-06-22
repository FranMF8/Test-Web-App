using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace users.database
{
    public class UserDto
    {
        public int id { get; set; }
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
    }
}
