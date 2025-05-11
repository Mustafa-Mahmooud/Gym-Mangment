using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites
{
    public class Trainers : BaseEntity
    {
        public string FullName { get; set; }
        public string Specialty { get; set; }
     
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int Salary { get;set; }

        public ICollection<Member>? Members { get; set; } = new List<Member>();
    }
}
