using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherApp
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.Write("Enter city name: ");
            string city = Console.ReadLine();

            Console.Write("Enter latitude: ");
            double lat = double.Parse(Console.ReadLine());

            Console.Write("Enter longitude: ");
            double lon = double.Parse(Console.ReadLine());

            string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";

            try
            {
                string json = await client.GetStringAsync(url);
                WeatherResponse data = JsonSerializer.Deserialize<WeatherResponse>(json);

                Console.WriteLine($"\nCurrent weather in {city}:");
                Console.WriteLine($"Temperature: {data.CurrentWeather.Temperature}°C");
                Console.WriteLine($"Wind speed: {data.CurrentWeather.WindSpeed} km/h");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching weather: {ex.Message}");
            }
        }
    }

    public class WeatherResponse
    {
        [JsonPropertyName("current_weather")]
        public CurrentWeather CurrentWeather { get; set; }
    }

    public class CurrentWeather
    {
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("windspeed")]
        public double WindSpeed { get; set; }
    }
}