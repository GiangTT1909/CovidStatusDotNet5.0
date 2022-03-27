using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
namespace DataAccess.API
{
    public class API
    {
        public static async Task<DataSumaryModel> GetCountryDataAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.covid19api.com/summary");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetStringAsync("");
            var data = JsonConvert.DeserializeObject<DataSumaryModel>(response);
            DataSumaryModel data1 = data;
            return data1;
        }

        public static async Task<List<CountryByDay>> GetCountryByDayAsync(string from, string to,string country) {
            HttpClient client = new HttpClient();
            string Url = "https://api.covid19api.com/country/"+country+"/status/confirmed";
            Url += "?from=" + from + "&to=" + to;
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetStringAsync("");
            var data = JsonConvert.DeserializeObject<List<CountryByDay>>(response);
            List<CountryByDay> data1 = data;
            return data1;
        }
    }
}
