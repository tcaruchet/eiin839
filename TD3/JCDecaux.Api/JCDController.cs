using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Device.Location;
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

        public static async Task<Station> GetStationNearest(string contractName, Position position)
        {
            List<Station> stations = await GetStations<List<Station>>(contractName);
            //return stations.FirstOrDefault(s => s.Position.Equals(position));
            var nearest = stations.Select(x => new GeoCoordinate(x.Position.Latitude, x.Position.Longitude))
                .OrderBy(x => x.GetDistanceTo(new GeoCoordinate(position.Latitude, position.Longitude)))
                .First();
            return stations.FirstOrDefault(s =>
                new GeoCoordinate(s.Position.Latitude, s.Position.Longitude).Equals(nearest));
        }
    }
}
