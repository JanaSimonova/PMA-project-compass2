using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pololetni_projekt.Models;

namespace Pololetni_projekt.Services
{
    public class ApiService
    {
        public async Task<Root> GetWeather(double latitude, double longitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid=9cd2cb299a8218f07926893c58d696cb", latitude, longitude));
            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}
