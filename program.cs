using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MovieDetails
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter a movie title:");
            string movieTitle = Console.ReadLine();

            string apiKey = "3ef75743";
            string url = $"http://www.omdbapi.com/?apikey={apiKey}&t={movieTitle}";

            StringBuilder sb = new StringBuilder();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(url);
                    var result = JsonConvert.DeserializeObject<dynamic>(response);
                    if (result.Response == "False")
                    {
                        Console.WriteLine("Error: Could not find movie with the given title. Please check the title and try again.");
                        return;
                    }

                    sb.AppendLine("Title: " + result.Title);
                    sb.AppendLine("Director: " + result.Director);
                    sb.AppendLine("Release year: " + result.Year);
                    sb.AppendLine("Running time: " + result.Runtime);
                    sb.AppendLine("Genre: " + result.Genre);
                    sb.AppendLine("Actors: " + result.Actors);
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine("Error: Could not retrieve movie details. Please check your internet connection and try again.");
                    return;
                }
            }

            Console.WriteLine(sb.ToString());
            Console.ReadKey();
        }
    }
}
