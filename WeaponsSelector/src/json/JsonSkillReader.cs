﻿using System.Collections.Generic;
using System.IO;


using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;

namespace WeaponsForm
{
    public class JsonSkillReader
    {
        //private readonly List<Weapon> weapons;
        private readonly AllSkills allSkills;

        public JsonSkillReader()
        {
            string fileLoc = "C:\\Users\\Elvin\\source\\repos\\PictureViewer\\WeaponsSelector\\JSON\\WeaponSkills.json";
            var fileContentsList = File.ReadAllLines(fileLoc).ToList();
            fileContentsList.RemoveAll(x => x.Trim().StartsWith("//"));
            string fileContentsString = String.Join("", fileContentsList);
            allSkills = AllSkills.FromJson(fileContentsString);
        }

        internal SkillType GetMedicalType(string medicalType)
        {
            return allSkills.SkillCategories.Find(x => x.Category == Constants.Medical).Skills.Find(x => x.Name == medicalType);
        }

        internal SkillType GetPhysicalMentalType(string physicalMentalType)
        {
            return allSkills.SkillCategories.Find(x => x.Category == Constants.PhysicalMental).Skills.Find(x => x.Name == physicalMentalType);
        }

        internal SkillType GetArmourType(string armourName)
        {
            return allSkills.SkillCategories.Find(x => x.Category == Constants.Armour).Skills.Find(x => x.Name == armourName);
        }

        /// <summary>
        /// Return the Type object with the given weaponType in the Weapons category
        /// </summary>
        /// <param name="weaponType">the name of the type to search for</param>
        /// <returns></returns>
        internal SkillType GetWeaponType(string weaponName)
        {
            return allSkills.SkillCategories.Find(x => x.Category == Constants.Weapons).Skills.Find(x => x.Name == weaponName);
        }

        internal List<SkillType> GetArmourSkills()
        {
            return allSkills.SkillCategories.Find(x => x.Category == Constants.Armour).Skills;
        }        

        /// <summary>
        /// Return the list of skills (claw, 1H, etc.) in the Weapons category
        /// </summary>
        /// <returns></returns>
        public List<SkillType> GetWeaponsSkills()
        {            
            return allSkills.SkillCategories.Find(x => x.Category == Constants.Weapons).Skills;
        }

        internal List<SkillType> GetPhysicalMentalSkills()
        {
            return allSkills.SkillCategories.Find(x => x.Category == Constants.PhysicalMental).Skills;
        }

        internal List<SkillType> GetMedicalSkills()
        {
            return allSkills.SkillCategories.Find(x => x.Category == Constants.Medical).Skills;
        }
    }

    /// <summary>
    /// Holder for all skills in the json file - the root node
    /// </summary>
    public class AllSkills
    {
        [JsonProperty("Skill Categories", Required = Required.Always)]
        public List<SkillCategory> SkillCategories { get; set; }

        public static AllSkills FromJson(string json) => JsonConvert.DeserializeObject<AllSkills>(json, Converter.Settings);
    }

    /// <summary>
    /// A particular skill category e.g. Weapons, Physical/Mental, etc.
    /// </summary>
    public partial class SkillCategory
    {
        [JsonProperty("Category", Required = Required.Always)]
        public string Category { get; set; }

        [JsonProperty("Type", Required = Required.Always)]
        public List<SkillType> Skills { get; set; }
    }

    public enum PurchaseType
    {
        Levels,                //e.g. 1H weapons have Prof, Spec, etc.
        Threshold,             //e.g. Life
        Once,                  //e.g. Ambidex
        CostPlusLevel,         //e.g. Talisman
        CostTimesLevel,        //e.g. Willpower
        CostTimesLevelPlusOne  //e.g. Spells
    }


    /// <summary>
    /// A particular skill grouping within a category e.g. 1H Weapons. This contains all information about the 
    /// levels and associated costs of this skill that you can buy.
    /// </summary>
    public partial class SkillType
    {
        [JsonProperty("Name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("Purchase type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public PurchaseType PurchaseType { get; set; }

        /// <summary>
        /// A list of the skill levels, if they exist for the current SkillType. The order is always from "lowest" to "highest" i.e. from None to Legendary Mastery.
        /// </summary>
        [JsonProperty("Levels", Required = Required.DisallowNull)]
        public List<Level> Levels { get; set; }

        /// <summary>
        /// The cost for skills that do not have Levels e.g. Enhance Life, Ambidexterity.
        /// </summary>
        [JsonProperty("Cost", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Cost { get; set; }

        /// <summary>
        /// If a field has BuyOnce == false, there will be a threshold value for how many times the skill can be bought before its cost increases. E.g. Enhance Life.
        /// </summary>
        [JsonProperty("Threshold", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Threshold { get; set; }

        /// <summary>
        /// Get the valid skill level strings (none, prof, mast, etc.) in a form consumable by a ComboBox's AddRange method.
        /// </summary>
        /// <returns></returns>
        internal object[] GetValidSkillLevels()
        {
            return Levels.ToArray().Select(level => level.LevelString).ToArray();
        }

        /// <summary>
        /// Calculate the cumulative cost of the given skill level (e.g. cost of Prof + Spec) for this SkillType.
        /// </summary>
        /// <param name="skillLevel"></param>
        /// <returns></returns>
        internal long GetSkillLevelCost(string skillLevel)
        {
            long totalCost = 0;

            foreach (var level in Levels)
            {
                totalCost += level.Cost;

                if (skillLevel.Equals(level.LevelString))
                {
                    break;
                }
            }

            return totalCost;


        }
    }

    /// <summary>
    /// A particular level of a skill e.g. Prof and its associated cost. Note the cost is NOT cumulative, it is for this skill level only.
    /// </summary>
    public partial class Level
    {
        [JsonProperty("Level", Required = Required.Always)]
        public string LevelString { get; set; }

        [JsonProperty("Cost", Required = Required.Always)]
        public long Cost { get; set; }
    }



    //Generated from https://app.quicktype.io/#l=cs&r=json2csharp
    // Also see http://json2csharp.com/


    public static class Serialize
    {
        public static string ToJson(this AllSkills self) => JsonConvert.SerializeObject(self, Converter.Settings);
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