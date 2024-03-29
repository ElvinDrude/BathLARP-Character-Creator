﻿using System;
using System.Collections.Generic;
using System.Linq;

using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WeaponsForm.Skills;

namespace WeaponsForm.Spells
{
    /// <summary>
    /// JSON reader handler for Miracles and Spells
    /// Splitting the Spells and Miracles to make the JSON files smaller, and remove the need to filter between the two 
    /// separate lists at runtime.
    /// </summary>
    public class JsonCastingReader
    {
        private readonly AllSpells allSpells;

        public JsonCastingReader(string fileLoc)
        {
            var fileContentsList = File.ReadAllLines(fileLoc).ToList();
            fileContentsList.RemoveAll(x => x.Trim().StartsWith("//"));
            string fileContentsString = String.Join("", fileContentsList);
            allSpells = AllSpells.FromJson(fileContentsString);
        }

        internal List<SpellSchool> GetSpellSchools()
        {
            return allSpells.SpellSchools;
        }

        internal List<SpellChain> GetSpells(string spellSchool)
        {
            return allSpells.SpellSchools.Find(x => x.School == spellSchool).SpellChain;
        }

        internal List<Level> GetSpellLevels(string spellSchool, string spell)
        {
            return allSpells.SpellSchools.Find(x => x.School == spellSchool).SpellChain.Find(x => x.Name == spell).Levels;
        }

    }

    public class JsonSpellReader : JsonCastingReader
    {
        public JsonSpellReader() : base("C:\\Users\\Elvin\\source\\repos\\PictureViewer\\WeaponsSelector\\JSON\\Spells.json")
        {

        }
    }

    public class JsonMiracleReader : JsonCastingReader
    {
        public JsonMiracleReader() : base("C:\\Users\\Elvin\\source\\repos\\PictureViewer\\WeaponsSelector\\JSON\\Miracles.json")
        {

        }
    }


    public partial class AllSpells
    {
        [JsonProperty("Spell Schools", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<SpellSchool> SpellSchools { get; set; }
    }

    public partial class SpellSchool
    {
        [JsonProperty("School", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string School { get; set; }

        [JsonProperty("Spell Chain", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<SpellChain> SpellChain { get; set; }
    }

    public partial class SpellChain
    {
        [JsonProperty("Name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("Levels", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<Level> Levels { get; set; }
    }

    public partial class Level
    {
        [JsonProperty("Description", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("Level", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? LevelLevel { get; set; }
    }

    public partial class AllSpells
    {
        public static AllSpells FromJson(string json) => JsonConvert.DeserializeObject<AllSpells>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this AllSpells self) => JsonConvert.SerializeObject(self, Converter.Settings);
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

