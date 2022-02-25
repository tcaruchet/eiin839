using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCDecaux.Api.Models;

namespace JCDecaux.Api
{
    public static class JCDController
    {
        public static async Task<List<object>> GetAllContracts()
        {
            return JsonConvert.DeserializeObject<List<object>>(
                await JCDAPIAccess.Client.GetStringAsync(JCDAPIAccess.GetUrl("contracts")));
        }

        public static async Task<T> GetStations<T>(string contractName)
        {
            return JsonConvert.DeserializeObject<T>(
                await JCDAPIAccess.Client.GetStringAsync(JCDAPIAccess.GetUrl($"stations?contract={contractName}")));
        }

        public static async Task<Station> GetStationDetails(string contractName, int number)
        {
            List<Station> stations = await GetStations<List<Station>>(contractName);
            return stations.FirstOrDefault(s => s.Number.Equals(number));
        }
    }
}
