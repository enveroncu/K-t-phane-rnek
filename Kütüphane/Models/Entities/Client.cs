using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kütüphane.Models.Entities
{
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return Name + " " + LastName;
        }
    }
}
