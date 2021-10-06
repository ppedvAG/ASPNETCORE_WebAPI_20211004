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
    [ApiController]
    public class PaggingSortingController : ControllerBase
    {
        private readonly MovieDbContext _dbContext;

        public PaggingSortingController(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("EasyPageingList")]
        public async Task<ActionResult<IEnumerable<Movie>>> EasyPagingList(int pageNumber, int pageSize)
        {
            return await _dbContext.Movies.OrderBy(o => o.Title)
                                          .Skip((pageNumber - 1) * pageSize)
                                          .Take(pageSize).ToListAsync();
        }


        [HttpGet("PagingListWithPageParametersObject")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies([FromQuery] PageParameters parameters)
        {
            return await _dbContext.Movies.OrderBy(o => o.Title)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
        }


    }
}
