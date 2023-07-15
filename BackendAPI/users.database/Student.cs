using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace users.database
{
    public class Student
    {
        [Key]
        public int id { get; set; } 

        [Required]
        public string name { get; set; } = string.Empty;
        public int dni { get; set; }
        [Required]
        public string email { get; set; } = string.Empty;
    }

}
