using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kütüphane.Models.Entities
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int AuthorizationID { get; set; }
        public virtual AuthorizationGroup Authorization { get; set; }

        public override string ToString()
        {
            return Name + " " + LastName;
        }
    }
}
