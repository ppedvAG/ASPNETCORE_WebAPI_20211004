using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstRestfulService.Data;
using MyFirstRestfulService.Models;

namespace MyFirstRestfulService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MicrosoftMovieWebAPIController : ControllerBase
    {
        private readonly MovieDbContext _context;

        public MicrosoftMovieWebAPIController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: api/MicrosoftMovieWebAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // GET: api/MicrosoftMovieWebAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/MicrosoftMovieWebAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MicrosoftMovieWebAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }
        
        // DELETE: api/MicrosoftMovieWebAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
