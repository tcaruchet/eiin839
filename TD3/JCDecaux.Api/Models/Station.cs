using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JCDecaux.Api.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Position
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"{nameof(Latitude)}: {Latitude}, {nameof(Longitude)}: {Longitude}";
        }
    }

    public class Availabilities
    {
        [JsonProperty("bikes")]
        public int Bikes { get; set; }

        [JsonProperty("stands")]
        public int Stands { get; set; }

        [JsonProperty("mechanicalBikes")]
        public int MechanicalBikes { get; set; }

        [JsonProperty("electricalBikes")]
        public int ElectricalBikes { get; set; }

        [JsonProperty("electricalInternalBatteryBikes")]
        public int ElectricalInternalBatteryBikes { get; set; }

        [JsonProperty("electricalRemovableBatteryBikes")]
        public int ElectricalRemovableBatteryBikes { get; set; }

        public override string ToString()
        {
            return $"{nameof(Bikes)}: {Bikes}, {nameof(Stands)}: {Stands}, {nameof(MechanicalBikes)}: {MechanicalBikes}, {nameof(ElectricalBikes)}: {ElectricalBikes}, {nameof(ElectricalInternalBatteryBikes)}: {ElectricalInternalBatteryBikes}, {nameof(ElectricalRemovableBatteryBikes)}: {ElectricalRemovableBatteryBikes}";
        }
    }

    public class TotalStands
    {
        [JsonProperty("availabilities")]
        public Availabilities Availabilities { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }

        public override string ToString()
        {
            return $"{nameof(Availabilities)}: {Availabilities}, {nameof(Capacity)}: {Capacity}";
        }
    }

    public class MainStands
    {
        [JsonProperty("availabilities")]
        public Availabilities Availabilities { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }

        public override string ToString()
        {
            return $"{nameof(Availabilities)}: {Availabilities}, {nameof(Capacity)}: {Capacity}";
        }
    }

    public class Station
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("contractName")]
        public string ContractName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("banking")]
        public bool Banking { get; set; }

        [JsonProperty("bonus")]
        public bool Bonus { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("lastUpdate")]
        public DateTime LastUpdate { get; set; }

        [JsonProperty("connected")]
        public bool Connected { get; set; }

        [JsonProperty("overflow")]
        public bool Overflow { get; set; }

        [JsonProperty("shape")]
        public object Shape { get; set; }

        [JsonProperty("totalStands")]
        public TotalStands TotalStands { get; set; }

        [JsonProperty("mainStands")]
        public MainStands MainStands { get; set; }

        [JsonProperty("overflowStands")]
        public object OverflowStands { get; set; }


        public override string ToString()
        {
            return $"{nameof(Number)}: {Number}, {nameof(ContractName)}: {ContractName}, {nameof(Name)}: {Name}, {nameof(Address)}: {Address}, {nameof(Position)}: {Position}, {nameof(Banking)}: {Banking}, {nameof(Bonus)}: {Bonus}, {nameof(Status)}: {Status}, {nameof(LastUpdate)}: {LastUpdate}, {nameof(Connected)}: {Connected}, {nameof(Overflow)}: {Overflow}, {nameof(Shape)}: {Shape}, {nameof(TotalStands)}: {TotalStands}, {nameof(MainStands)}: {MainStands}, {nameof(OverflowStands)}: {OverflowStands}";
        }
    }

}
