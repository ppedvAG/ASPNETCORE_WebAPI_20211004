using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstRestfulService.Data;
using MyFirstRestfulService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCORE_WEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    //Microsoft.AspNetCore.Mvc.ApiConventionTypeAttribute, auf einen Controller angewendet — Wendet den angegebenen Konventionstyp auf alle Controlleraktionen an.Eine Konventionsmethode ist mit Hinweisen markiert, mit denen die Aktionen bestimmt werden, für die die Konventionsmethode gilt.Weitere Informationen zu Hinweisen finden Sie unter Erstellen von Web-API-Konventionen).
    public class ConventionController : ControllerBase
    {
        private readonly MovieDbContext _ctx;

        public ConventionController(MovieDbContext ctx)
        {
            _ctx = ctx;
        }

        //Minimale Anforderung, die Microsoft einen Programmierer abverlangt
        //Methoden Namen muss Get/Post/Delete/Put im MethodenNamen verwenden.



        //Swagger kann nicht ohne HTTP-Verbs (Swagger-Konvention)
        /// <summary>
        /// [HttpGet("/GetAll")] -> GetAll, gilt als Alias um eine doppelte Get-Belegung auf einer URL zu vermeiden.
        /// </summary>
        /// <returns></returns>


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status102Processing)]
        [ProducesResponseType(StatusCodes.Status303SeeOther)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public List<Movie> GetAll()
        {
            return _ctx.Movies.ToList();
        }


        
        [HttpGet("/ApiConventionMethodSample/AllComedyMovies")] //-> AllComedyMovies, gilt als Alias um eine doppelte Get-Belegung auf einer URL zu vermeiden.
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public List<Movie> GetAllComeyMovies()
        {
            return _ctx.Movies.Where(n => n.Price > 3).ToList();
        }

        [HttpGet("{id:int}")] // {id:int} sagt, dass eine Id exisiteren muss (keine nullable toleranz) + Constraint ist ein integer = Id muss vom Typ integeser sein
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public IActionResult GetById(int id)
        {
            if (id == default(int))
                return BadRequest("Id hat den Wert: " + id.ToString());

            Movie currentMovie = _ctx.Movies.Find(id);

            if (currentMovie == null)
                return BadRequest("Used Key " + id.ToString());

            return Ok(currentMovie);
        }

        [HttpPost]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        //[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))] //-> Doppelte ApiConventionMethoden auf einer API-Methode kann nicht angewendet werden
        // Bei HttpPost + HttpPut wäre es sinnvoller die Menge der Rückgabe-StatusCodes via ProducesResponseType zu definieren. 
        public IActionResult MoviePost(Movie movie)
        {
            if (movie == null)
                return BadRequest();

            if (ModelState.IsValid)
            {


                if (movie.Id == default(int))
                {
                    _ctx.Movies.Add(movie);
                }
                else
                {
                    _ctx.Update(movie);
                }
                _ctx.SaveChangesAsync();
                return Ok();


            }

            return BadRequest();
        }


        [HttpDelete]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public IActionResult MovieDelete(int? id)
        {
            Movie currentMovie = _ctx.Movies.Find(id.Value);
            _ctx.Movies.Find(currentMovie);
            _ctx.SaveChangesAsync();

            return Ok();
        }
    }
}
