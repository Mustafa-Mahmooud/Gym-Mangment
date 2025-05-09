using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites
{
    public class Member : BaseEntity
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
       
        //Trainer
        public Trainers? Trainer { get; set; }
        public int? TrainerId { get; set; }

        //MemberShip
        public int? MembershipId { get; set; }
        public Membership? MembershipType { get; set; }

        //Payment
        public ICollection<Payment>? Payments { get; set; }
    }
}
