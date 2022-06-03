using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponsForm.Races
{
    public class JsonRaceReader
    {
        private readonly AllRaces allRaces;

        public JsonRaceReader()
        {
            string fileLoc = "C:\\Users\\Elvin\\source\\repos\\PictureViewer\\WeaponsSelector\\JSON\\Races.json";
            var fileContentsList = File.ReadAllLines(fileLoc).ToList();
            fileContentsList.RemoveAll(x => x.Trim().StartsWith("//"));
            string fileContentsString = String.Join("", fileContentsList);
            allRaces = AllRaces.FromJson(fileContentsString);
        }

        public List<Race> GetRaces()
        {
            return allRaces.Races;
        }
    }

    public class AllRaces
    {
        [JsonProperty("Races", Required = Required.Always)]
        public List<Race> Races { get; set; }

        public static AllRaces FromJson(string json) => JsonConvert.DeserializeObject<AllRaces>(json, Converter.Settings);
    }

    public partial class Race
    {
        [JsonProperty("Race", Required = Required.Always)]
        public string RaceName { get; set; }

        [JsonProperty("Death Threshold", Required = Required.Always)]
        public long DeathThreshold { get; set; }

        [JsonProperty("Life", Required = Required.Always)]
        public RacialAttribute Life { get; set; }

        [JsonProperty("Mana", Required = Required.Always)]
        public RacialAttribute Mana { get; set; }

        [JsonProperty("Standing", Required = Required.Always)]
        public RacialAttribute Standing { get; set; }
    }

    public partial class RacialAttribute
    {
        [JsonProperty("Base", Required = Required.Always)]
        public long Base { get; set; }

        [JsonProperty("Threshold", Required = Required.Always)]
        public long Threshold { get; set; }
    }


    public static class Serialize
    {
        public static string ToJson(this AllRaces self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
