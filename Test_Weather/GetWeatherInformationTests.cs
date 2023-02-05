using Newtonsoft.Json;

namespace Test_Weather
{
    [TestClass]
    public class GetWeatherInformationTests
    {
        [TestMethod]
        public async Task TestGetWeatherInformationSuccess()
        {
            double latitude = 19.3316;
            double longitude = 79.4661;
            var expectedWeather = " {\r\n  \"temperature\": 21.5,\r\n  \"windspeed\": 3.9,\r\n  \"winddirection\": 292.0,\r\n  \"weathercode\": 0,\r\n  \"time\": \"2023-02-05T16:00\"\r\n}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var weatherInformation = JsonConvert.DeserializeObject<dynamic>(result);

                    Assert.AreEqual(expectedWeather, weatherInformation.current_weather);
                }
                else
                {
                    Assert.Fail("Request was not successful: " + response.StatusCode + " - " + response.ReasonPhrase);
                }
            }
        }
    }

}