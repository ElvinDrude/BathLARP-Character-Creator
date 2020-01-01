using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WeaponsSelector
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class WeaponSkills : BindingList<WeaponSkillController>
    {

        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }
        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            // Ignore the prop value and search by family name.
            for (int i = 0; i < Count; ++i)
            {
                //if (Items[i].FontFamily.Name.ToLower() == ((string)key).ToLower())
                //    return i;

            }
            return -1;
        }


    }

    public class JsonSkillReader
    {
        public JsonSkillReader()
        {
            string fileLoc = "C:\\Users\\Elvin\\source\\repos\\PictureViewer\\WeaponsSelector\\JSON\\WeaponSkills.json";
            string fileContents = File.ReadAllText(fileLoc);
            dynamic skillsJson = JsonConvert.DeserializeObject(fileContents);

            foreach( var entry in skillsJson)
            {
                Console.WriteLine(entry);
            }

        }


    }

    public class WeaponSkillController
    {
        IList weaponTypes = new List<string>()
        {
            "Claw",
            "Dagger",
            "1H"
        };

        IDictionary<string, int> skillLevelCosts = new Dictionary<string, int>()
        {
            {"None", 0 },
            {"Prof", 2 },
            {"Spec", 4 },
            {"Exp", 8 }
        };

        private readonly IDictionary<string, IDictionary<string, int>> weaponSkillCosts;

        public WeaponSkillController()
        {
            weaponSkillCosts = new Dictionary<string, IDictionary<string, int>>();

            foreach (string weapon in weaponTypes)
            {                
                weaponSkillCosts.Add(weapon, skillLevelCosts);
            }
        }

        public int GetTotalCost(string weaponType, string currLevel)
        {
            int totalCost = 0;
            IDictionary<string, int> skillCosts = new Dictionary<string, int>();
            var didItWork = weaponSkillCosts.TryGetValue(weaponType, out skillCosts);

            foreach (var skill in skillCosts)
            {
                totalCost += skill.Value;

                if (currLevel.Equals(skill.Key))
                {
                    break;
                }
            }

            return totalCost;
        }

    }

    public class SkillLevelSource : Component, IListSource
    {
        readonly BindingList<string> skillList;

        public SkillLevelSource()
        {
            if (!this.DesignMode)
            {
                skillList = new BindingList<string>() { "None", "Prof", "Spec", "Exp" };
            }

        }

        public SkillLevelSource(IContainer container)
        {
            container.Add(this);
        }

        #region IListSource Members

        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }

        System.Collections.IList IListSource.GetList()
        {
            return skillList;
        }

        #endregion
    }




}
