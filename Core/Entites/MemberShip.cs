using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites
{
    public class Membership: BaseEntity
    {

        public string Type { get; set; } // e.g., "Monthly", "Quarterly", "Yearly"
        public decimal Price { get; set; }
        public int DurationInMonths { get; set; }

        // Navigation property
        public ICollection<Member> Members { get; set; } = new HashSet<Member>();
    }

}
