using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kütüphane.Models.Entities
{
    public class RentedBook
    {
        public int ID { get; set; }
        public Book Book { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime DueTo { get; set; }
        public decimal Fee { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }



    }
}
