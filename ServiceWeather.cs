using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Weather_API
{
    public class ServiceWeather
    {
        IDistributedCache cache;
        private const string API_KEY = "ZLDLUUZMKWNSEKYJQ74CEB6D6";
        private const string BASE_URL = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/";
        private const string SECONDARY_URL = "?" +
            "unitGroup=metric" +
            "&include=days%2Ccurrent%2Calerts" +
            "&key=ZLDLUUZMKWNSEKYJQ74CEB6D6" +
            "&contentType=json";
        public ServiceWeather(IDistributedCache distributedCache)
        {
            cache = distributedCache;
        }

        
        
        public async Task<string> GetWeather(string city)
        {
            string url = $"{BASE_URL}{city}{SECONDARY_URL}";
            Console.WriteLine(url);
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
                httpResponseMessage.EnsureSuccessStatusCode();
                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                //return responseBody;

                JsonDocument jsonDocument = JsonDocument.Parse(responseBody);

                JsonElement jsonElement = jsonDocument.RootElement.GetProperty("days");

                //Console.WriteLine(jsonElement.ToString());

                //return jsonElement.ToString();

                List<Weather> weathers = new List<Weather>();

                foreach (JsonElement element in jsonElement.EnumerateArray())
                {
                    Weather weather = new Weather();
                    weather.DateTime = element.GetProperty("datetime").GetDateTime();
                    weather.temp = element.GetProperty("temp").GetDecimal();
                    weather.sunrise = element.GetProperty("sunrise").GetString();
                    weather.sunset = element.GetProperty("sunset").GetString();
                    weather.conditions = element.GetProperty("conditions").GetString();
                    weather.description = element.GetProperty("description").GetString();
                    weathers.Add(weather);
                }
                Console.WriteLine(String.Join(", ", weathers));

                return JsonSerializer.Serialize(weathers);
            }
        }
    }
}
