using Kütüphane.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kütüphane.Models
{
    public class LibraryModel : DbContext
    {
        public LibraryModel():base("name=LibraryDB")
        {
           
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<AuthorizationGroup> AuthorizationGroups { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RentedBook> RentedBooks { get; set; }

    }
}
