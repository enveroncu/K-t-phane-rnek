using System.Collections.Generic;

namespace Kütüphane.Models.Entities
{
    public class Author
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public virtual List<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
        public override string ToString()
        {
            return Name + " " + LastName;
        }

    }
}