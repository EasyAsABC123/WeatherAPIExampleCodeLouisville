using System;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace WeatherAPIExampleCodeLouisville
{
    class MainClass
    {
        /*
         * normal view:
         * {"key":"value","name":"Justin Schuhmann","age":37}
         * 
         * object
         * {
         *   "key": "value",
         *   "name": "Justin Schuhmann",
         *   "age": 37
         * }
         * 
         * array
         * [
         *   {
         *     "key": "value",
         *     "name": "Justin Schuhmann",
         *     "age": 37
         *   },
         *   {
         *     "key": "value",
         *     "name": "Tyler Buth",
         *     "age": 37
         *   }
         * ]
         * 
         * [
         *   "Justin Schuhmann",
         *   "Joanne",
         *   "Tyler Buth"
         * ]
         * 
         * [
         *   1,
         *   2,
         *   3,
         *   4,
         *   5,
         *   8
         * ]
         */
        private static readonly HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            // Fetch hourly weather from api
            var json = await GetWeather();
            // Convert JSON into object
            dynamic weather = JObject.Parse(json);

            // Read my ExampleJSONFile.json into a string
            var myJson = File.ReadAllText("../../ExampleJSONFile.json");
            // Convert the string into json
            dynamic person = JObject.Parse(myJson);

            // Print the person object name property and age property
            Console.WriteLine($"Hello {person.name} you are {person.age}");

            // Print the weather.properties.periods first element temperature
            Console.WriteLine("Next hour forecast");
            Console.WriteLine($"temperature: {weather.properties.periods[0].temperature} wind speed: {weather.properties.periods[0].windSpeed}");

            //var periods = weather.properties.periods;
            //int min = periods.Min(period => (int)(period.temperature));
            //var lowestValues = x.Where(entry => entry.Value == min);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static async Task<string> GetWeather()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://api.weather.gov/gridpoints/LMK/47,74/forecast/hourly");

            var msg = await stringTask;
            return msg;
        }

    }
}
