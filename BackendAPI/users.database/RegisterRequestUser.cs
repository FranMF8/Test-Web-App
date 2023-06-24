using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace users.database
{
    public class ControlUser : UserDto
    {
        public string controlPassword { get; set; } = null!;
    }
}
