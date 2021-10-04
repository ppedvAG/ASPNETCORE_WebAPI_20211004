using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstRestfulService.Data;
using MyFirstRestfulService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstRestfulService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieDbContext _dbContext;

        public MovieController(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IList<Movie> GetAllMovies()
        {
            IList<Movie> movieListe = new List<Movie>();
            movieListe.Add(new Movie { Id = 1, Title = "Batman 1", Description = "Jokers Rückkehr" });
            movieListe.Add(new Movie { Id = 1, Title = "Batman 2", Description = "Jokers und Batman werden Freunde" });
            movieListe.Add(new Movie { Id = 1, Title = "Batman 3", Description = "Pinguin ist auf Joker eifersüchtig." });


            //_dbContext.Movies.ToList();

            return movieListe;
        }

        //[HttpPost]
        //public void AddMovie(Movie movie)
        //{
        //    _dbContext.Add(movie);
        //    _dbContext.SaveChanges();
        //}

        //[HttpPut]
        //public void Update(Movie movie)
        //{
        //    _dbContext.Update(movie);
        //    _dbContext.SaveChanges();
        //}

        [HttpPost("/CreateOrUpdate")]
        [HttpPut("/CreateOrUpdate")]
        public void CreateOrUpdate(Movie movie)
        {
            if (movie.Id != 0)
            {
                // Update
            }
            else
            {
                // Insert
            }
        }

        [HttpDelete]
        public void Delete(Movie movie)
        {
            _dbContext.Remove(movie);
            _dbContext.SaveChanges();
        }
    }
}
