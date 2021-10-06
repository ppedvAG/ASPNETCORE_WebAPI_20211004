using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstRestfulService.Data;
using MyFirstRestfulService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstRestfulService.Controllers
{
    [Route("api/[controller]")]
    //[ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieDbContext _dbContext;

        public MovieController(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet] //https://localhost:44319/api/movie
        //[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [Produces("application/xml", "application/json")]
        public IList<Movie> GetAllMovies()
        {
            IList<Movie> movieListe = new List<Movie>();
            movieListe.Add(new Movie { Id = 1, Title = "Batman 1", Description = "Jokers Rückkehr" });
            movieListe.Add(new Movie { Id = 1, Title = "Batman 2", Description = "Jokers und Batman werden Freunde" });
            movieListe.Add(new Movie { Id = 1, Title = "Batman 3", Description = "Pinguin ist auf Joker eifersüchtig." });


            //_dbContext.Movies.ToList();

            return movieListe;
        }

        [HttpGet("GetOnMovieFilms")] //https://localhost:44319/api/movie -> https://localhost:44319/api/Movie/GetOnMovieFilms  (besser)
        public IEnumerable<Movie> GetOnMovieFilms()
        {
            IList<Movie> movies = _dbContext.Movies.ToList();

            foreach (Movie currentMovie in movies)
            {
                yield return currentMovie;
            }
        }


        [HttpGet("asyncsale")]
        public async IAsyncEnumerable<Movie> GetMovielFilmsAsny()
        {
            IAsyncEnumerable<Movie> products = _dbContext.Movies.AsAsyncEnumerable();

            await foreach (Movie movie in products)
            {
                yield return movie;
            }
        }


        [HttpGet("{id}")]

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Movie> GetMovie(int? id)
        {
            if (!id.HasValue)
                return BadRequest(); //Http Fehlercode -> 400 zurück liefern


            Movie currentMovie = _dbContext.Movies.Find(id.Value);

            if (currentMovie == null)
            {
                return NotFound(); //Http Fehlercode -> 400 zurück liefern
            }


            return Ok(currentMovie);
        
        }

        [HttpPost]
        //Konvetionen können seperat unterhalb des jeweiligen HTTP-Verbs definiert werden
        public void AddMovie(Movie movie)
        {
            _dbContext.Add(movie);
            _dbContext.SaveChanges();
        }

        [HttpPut]
        public void Update(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Update(movie);
                _dbContext.SaveChanges();
            }
        }

        //Kombination von Verben funktionieren. Allerdings kann dann keine expliziete Konvetionen an den jeweiligen Methoden angewendet werden. 
        //[HttpPost("/CreateOrUpdate")]
        //[HttpPut("/CreateOrUpdate")]
        //public void CreateOrUpdate(Movie movie)
        //{
        //    if (movie.Id != 0)
        //    {
        //        // Update
        //    }
        //    else
        //    {
        //        // Insert
        //    }
        //}

        [HttpDelete]
        public void Delete(Movie movie)
        {
            _dbContext.Remove(movie);
            _dbContext.SaveChanges();
        }
    }
}
