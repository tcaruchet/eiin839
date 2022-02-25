using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCDecaux.Api
{
    public static class JCDController
    {
        public static async Task<object> GetAllContracts()
        {
            return JsonConvert.DeserializeObject<object>(
                await JCDAPIAccess.Client.GetStringAsync(JCDAPIAccess.GetUrl("contracts")));
        }

        public static async Task<object> GetStations(string contractName)
        {
            return JsonConvert.DeserializeObject<object>(
                await JCDAPIAccess.Client.GetStringAsync(JCDAPIAccess.GetUrl($"stations?contract={contractName}")));
        }
    }
}
