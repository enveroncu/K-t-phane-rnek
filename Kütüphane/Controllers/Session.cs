using Kütüphane.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kütüphane.Controllers
{
    public class Session
    {
        public Employee Employee { get; set; }
        public DateTime LastLogin { get; set; }
        public Form form { get; set; }

        public Session(Employee e)
        {
            Employee = e;
            LastLogin = DateTime.Now;
        }
    }
}
