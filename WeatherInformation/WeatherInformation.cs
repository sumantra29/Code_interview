using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace WeatherInformation
{
    class WeatherInformation
    {
        public static async void GetWeatherInformation(double latitude, double longitude)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true");

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var weatherInformation = JsonConvert.DeserializeObject<dynamic>(result);

                        Console.WriteLine("Current Weather: " + weatherInformation.current_weather);
                        //Console.WriteLine("Wind Speed: " + weatherInformation.windspeed + "m/s");
                    }
                    else
                    {
                        Console.WriteLine("Error: " + response.StatusCode + " - " + response.ReasonPhrase);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

            }
        }
    }
}
