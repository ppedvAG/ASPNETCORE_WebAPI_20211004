using Microsoft.EntityFrameworkCore;
using OData80SampleService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData80SampleService.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Press> Presses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            //modelBuilder.Entity<Book>().HasData(
            //    new Book
            //    {
            //        Id = 1,
            //        ISBN = "978-0-321-87758-1",
            //        Title = "Essential C#5.0",
            //        Author = "Mark Michaelis",
            //        Price = 59.99m,
            //        Location = new Address { City = "Redmond", Street = "156TH AVE NE" },
            //        Press = new Press
            //        {
            //            Id = 1,
            //            Name = "Addison-Wesley",
            //            Category = Category.Book
            //        }
            //    },
            //    new Book
            //    {
            //        Id = 2,
            //        ISBN = "063-6-920-02371-5",
            //        Title = "Enterprise Games",
            //        Author = "Michael Hugos",
            //        Price = 49.99m,
            //        Location = new Address { City = "Bellevue", Street = "Main ST" },
            //        Press = new Press
            //        {
            //            Id = 2,
            //            Name = "O'Reilly",
            //            Category = Category.EBook,
            //        }
            //    });


            modelBuilder.Entity<Book>().OwnsOne(c => c.Location);
        }

        public void Temp()
        {
            Book book = new Book
            {
                Id = 1,
                ISBN = "978-0-321-87758-1",
                Title = "Essential C#5.0",
                Author = "Mark Michaelis",
                Price = 59.99m,
                Location = new Address { City = "Redmond", Street = "156TH AVE NE" },
                Press = new Press
                {
                    Id = 1,
                    Name = "Addison-Wesley",
                    Category = Category.Book
                }
            };
        }
    }
}
