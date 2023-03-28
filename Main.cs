using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        Console.WriteLine("Which city are you looking for ?");
       string City = Console.ReadLine();
        string apiKey = "API-KEY";
        string city = $"{City}";
        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}";

        HttpResponseMessage response = await client.GetAsync(apiUrl);
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(json);

            Console.WriteLine($"City: {weatherData.Name}");
            Console.WriteLine($"Temperature: {weatherData.Main.Temp}°F");
            Console.WriteLine($"Weather: {weatherData.Weather[0].Main}");
        }
        else
        {
            Console.WriteLine($"Error retrieving weather data: {response.StatusCode}");
        }
        Console.ReadKey();
    }
}

public class WeatherData
{
    public string Name { get; set; }
    public MainData Main { get; set; }
    public Weather[] Weather { get; set; }
}

public class MainData
{
    public float Temp { get; set; }
}

public class Weather
{
    public string Main { get; set; }
}
