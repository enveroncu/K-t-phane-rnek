using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kütüphane.Models.Entities
{
    public class Book
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public int PublishYear { get; set; }
        public int Stock { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public int PublisherID { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual List<Author> Authors { get; set; }

        public Book()
        {
            Authors = new List<Author>();
        }
        public string GetAuthors()
        {
            string str=string.Empty;
            for(int i=0;i<Authors.Count;i++)
            {
                str += Authors[i].ToString() ;
                if (i < Authors.Count-1)
                    str += " ,";
            }
            return str;
        }

        public override string ToString()
        {
            return Name;
        }

    }

}
