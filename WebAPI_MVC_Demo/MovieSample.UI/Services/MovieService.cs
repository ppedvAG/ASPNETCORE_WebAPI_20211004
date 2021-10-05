using MovieSample.SharedLibrary.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieSample.UI.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _client;

        private string _baseUrl = "https://localhost:5001/api/Movie/";

        public MovieService(HttpClient client)  // Seperation of Concern IHttpClientFactory wird beim erstellen des Controllers verwendet 
        {
            _client = client;
        }

        // Delete -> https://localhost:5001/api/Movie/1
        public async Task DeleteMovie(int id)
        {
            string url = _baseUrl + id.ToString();
            HttpResponseMessage response = await _client.DeleteAsync(url);
        }


        //Achtung ich verwende die Allgemeine Methode SendAsync
        public async Task<List<Movie>> GetAll()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _baseUrl);

            HttpResponseMessage response = await _client.SendAsync(request);
            string jsonText = await response.Content.ReadAsStringAsync();

            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(jsonText);
            return movies;
        }

        // Get -> https://localhost:5001/api/Movie/1
        public async Task<Movie> GetById(int id)
        {
            string extendetURL = _baseUrl + id.ToString();
            HttpResponseMessage response = await _client.GetAsync(extendetURL);
            string jsonText = await response.Content.ReadAsStringAsync();

            Movie movies = JsonConvert.DeserializeObject<Movie>(jsonText);
            return movies;

        }

        // POST -> https://localhost:5001/api/Movie/
        public async Task InsertMovie(Movie movie)
        {
            string jsonText = JsonConvert.SerializeObject(movie);

            StringContent body = new StringContent(jsonText, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_baseUrl, body);
        }


        public async Task UpdateMovie(Movie movie)
        {
            string extendetURL = _baseUrl + movie.Id.ToString();
            string jsonText = JsonConvert.SerializeObject(movie);
            StringContent body = new StringContent(jsonText, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync(extendetURL, body);
        }
    }
}
