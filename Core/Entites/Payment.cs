using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites
{
    public class Payment
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string Method { get; set; } // e.g., "Credit Card", "Cash"
    }

}
