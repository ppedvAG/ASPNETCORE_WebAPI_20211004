using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MyFirstRestfulService.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string baseUrl = "https://localhost:5001/api/Movie/";

            Thread.Sleep(15000);
            using HttpClient client = new HttpClient();

            // Client möchte eine Anfrage stellen und erstellt seinen Request 
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/vcard"));


            HttpResponseMessage response = await client.SendAsync(request);
            
           
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
