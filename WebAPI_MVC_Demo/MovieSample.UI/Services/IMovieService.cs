using MovieSample.SharedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSample.UI.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> GetAll();

        Task<Movie> GetById(int id);

        Task InsertMovie(Movie movie);

        Task UpdateMovie(Movie movie);

        Task DeleteMovie(int id);
    }
}
