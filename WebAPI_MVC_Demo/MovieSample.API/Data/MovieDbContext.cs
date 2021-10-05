using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieSample.SharedLibrary.Entities;

namespace MovieSample.API.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext (DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        public DbSet<MovieSample.SharedLibrary.Entities.Movie> Movie { get; set; }
    }
}
