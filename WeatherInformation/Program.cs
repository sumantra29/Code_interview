using System;
namespace WeatherInformation
{
    class Program
    {
         public static void Main(string[] args)
        {
            Console.WriteLine("Enter city name: ");
            var city = Console.ReadLine();
            Dictionary<string, (double latitude, double longitude)> cityCoordinates = new Dictionary<string, (double latitude, double longitude)>();
            double latitude = 0.0;
            double longitude = 0.0;
            using (var reader = new StreamReader("D:\\API-WEATHER\\WeatherInformation\\Database_File\\in.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    var city_table = values[0].Trim();
                    if (!double.TryParse(values[1].Trim(), out latitude))
                    {
                        Console.WriteLine("Error: Invalid latitude value for city " + city_table);
                        continue;
                    }
                    if (!double.TryParse(values[2].Trim(), out longitude))
                    {
                        Console.WriteLine("Error: Invalid longitude value for city " + city_table);
                        continue;
                    }
                     cityCoordinates[city_table] = (latitude, longitude);
                }
            }
            foreach (var cityPair in cityCoordinates)
            {
                var city_table = cityPair.Key;
                var coordinates = cityPair.Value;
                if (string.Equals(city, city_table, StringComparison.OrdinalIgnoreCase))
                    WeatherInformation.GetWeatherInformation(latitude, longitude);
            }
            Console.ReadKey();
        }
    }
}