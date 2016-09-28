using System.Collections.Generic;

namespace Kütüphane.Models.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual List<Book> Books { get; set; }

        public Category()
        {
            Books = new List<Book>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}