using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace users.database
{
    public class DBUser
    {
        [Key]
        public string email { get; set; } = string.Empty;
        public byte[] passwordHash { get; set; } = null!;
        public byte[] passwordSalt { get; set; } = null!;
        public bool deleted { get; set; } = false;
    }
}
