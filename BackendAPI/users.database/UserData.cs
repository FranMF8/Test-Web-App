using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace users.database
{
    public class UserData
    {
        [Key]
        public string email { get; set; } = null!;
        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;

        [ForeignKey("email")]
        public virtual DBUser user { get; set; } = null!;
    }
}
