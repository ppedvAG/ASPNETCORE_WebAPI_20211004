
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstRestfulService.Data;
using MyFirstRestfulService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MyFirstRestfulService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnValuesController : ControllerBase
    {
        private readonly MovieDbContext _ctx;
        public ReturnValuesController(MovieDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet] //Komplexe Returnwerte IList<T>, List<T> oder auch nur Movie sind komplexe Objekte und werden als JSON zurück gegeben (Default) 
        public IList<Movie> Get() => _ctx.Movies.ToList();


        //Serialisierung
        [HttpGet("GetComedyMovies")]
        public IEnumerable<Movie> GetComedyMovies ()
        {
            var allMovies = _ctx.Movies.ToList();

            foreach(Movie currentMovie in allMovies)
            {
                yield return currentMovie; //
            }
        }


        


        [HttpGet("asyncsale")]
        public async IAsyncEnumerable<Movie> GetOnSaleProductsAsync()
        {
            var allMovies = await _ctx.Movies.ToListAsync();

            foreach (var currentMovie in allMovies)
            {
                    yield return currentMovie;
            }
        }



        //IActionResult-Typ
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Movie))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            //Movie currentMovie = _ctx.Movies.Find(id);

            //if (currentMovie == null)
            //    return BadRequest($"Fehler bei der Suche- Wahrscheinlich eine Falsche Id Angabe -> id: {id} ");


            Movie movie = new Movie();
            movie.Title = "Asterix und Obelix";
            movie.Description = "Obelix darf zaubertrank trinken";
            movie.Price = 10;

            return Ok(movie);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(Movie movie)
        {
            if (ModelState.IsValid)
            {
                //Hinzufügen eines Films
                _ctx.Movies.Add(movie);
                await _ctx.SaveChangesAsync();


                return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
            }
            return BadRequest();
        }


        [HttpGet("GenericActionResult")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<Movie>> GetMovies() =>
            _ctx.Movies.ToList();

        //ASP.NET Core 2.1 wurde der Rückgabetyp ActionResult <T> für Web-API-Controlleraktionen eingeführt.
        //Damit wird die Rückgabe eines von ActionResult abgeleiteten Typs oder eines spezifischen Typs ermöglicht.
        //ActionResult<T> besitzt gegenüber dem IActionResult-Typ die folgenden Vorteile:

        //Die[ProducesResponseType] -Eigenschaft des Attributs Type kann ausgeschlossen werden.
        // [ProducesResponseType(200, Type = typeof(Product))] wird beispielsweise zu[ProducesResponseType(200)] vereinfacht.
        // Der erwartete Rückgabetyp der Aktion wird stattdessen von T in ActionResult<T> abgeleitet.

        [HttpGet("GetByIdentifier/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Movie> GetByIdentifier(int id)
        {
            Movie movie = _ctx.Movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpPost("Create2Async")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Movie>> Create2Async(Movie movie)
        {

            _ctx.Movies.Add(movie);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByIdentifier), new { id = movie.Id }, movie);
        }

    }
}
