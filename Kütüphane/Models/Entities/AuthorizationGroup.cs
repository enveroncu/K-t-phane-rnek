using System.Collections.Generic;

namespace Kütüphane.Models.Entities
{
    public class AuthorizationGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}