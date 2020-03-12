using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeaponsForm;

namespace WeaponsSelector.src.character.record
{
    abstract class Record
    {



    }

    class GuildRecord : Record
    {
        public string Guild { get; set; }

        GuildRecord(string GuildName)
        {
            Guild = GuildName;
        }
    }

    abstract class SkillRecord : Record
    {
        public PurchaseType PurchaseType { get; set; }
        SkillRecord(PurchaseType type)
        {
            PurchaseType = type;
        }

        /// <summary>
        /// Returns the cost of the current skill, including any modifiers e.g. guild bonuses
        /// </summary>
        /// <returns></returns>
        public abstract int GetCost();
    }



}
