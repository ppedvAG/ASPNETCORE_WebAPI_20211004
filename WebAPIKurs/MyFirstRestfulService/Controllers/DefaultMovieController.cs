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
    public class DefaultMovieController : ControllerBase
    {
        private readonly MovieDbContext _dbContext;

        public DefaultMovieController(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IList<Movie>> Get()
        {
            IList<Movie> movieListe = new List<Movie>();
            movieListe.Add(new Movie { Id = 1, Title = "Batman 1", Description = "Jokers Rückkehr" });
            movieListe.Add(new Movie { Id = 1, Title = "Batman 2", Description = "Jokers und Batman werden Freunde" });
            movieListe.Add(new Movie { Id = 1, Title = "Batman 3", Description = "Pinguin ist auf Joker eifersüchtig." });

            return Ok(movieListe);
        }


       

        //public IActionResult Post(Movie movie)
        //{
        //    _dbContext.Add(movie);
        //    _dbContext.SaveChanges();

        //    return Ok(movie);
        //}

        //public IActionResult Put(Movie movie)
        //{
        //    _dbContext.Update(movie);
        //    _dbContext.SaveChanges();

        //    return Ok();
        //}

        //public IActionResult Delete(Movie movie)
        //{
        //    _dbContext.Remove(movie);
        //    _dbContext.SaveChanges();
        //    return Ok();
        //}
    }
}
