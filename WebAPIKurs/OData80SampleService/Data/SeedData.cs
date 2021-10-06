using OData80SampleService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData80SampleService.Data
{
    public static class SeedData
    {
        public static void Initialize(BookStoreContext context)
        {
            IList<Book> testdatenBooks = DataSource.GetBooks();

            foreach (Book currentBook in testdatenBooks)
                context.Books.Add(currentBook);

            context.SaveChanges();
        }
    }
}
