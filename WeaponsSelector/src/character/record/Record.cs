using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeaponsForm;
using WeaponsForm.Skills;

namespace WeaponsForm.character.record
{
    public class SkillRecord
    {
        /// <summary>
        /// The cost, in character points, of this skill
        /// </summary>
        public long Cost;

        public SkillRecord(long cost)
        {
            Cost = cost;
        }
    }

    /// <summary>
    /// Class for any skill that just needs a cost and a simple string display
    /// </summary>
    public class StringSkillRecord : SkillRecord
    {
        public string DisplayString { get; }
        public StringSkillRecord(long cost, string displayString) : base(cost)
        {
            DisplayString = displayString;
        }
    }

    public class CastingRecord : StringSkillRecord
    {
        public CastingRecord(long cost, string displayString) : base(cost, displayString)
        {
        }
    }

    public class MagicRecord : StringSkillRecord
    {
        public MagicRecord(long cost, string displayString) : base(cost, displayString)
        {
        }
    }

    public class ManaRecord : SkillRecord
    {
        public long BoughtMana { get; }
        public ManaRecord(long cost, long boughtMana) : base(cost)
        {
            BoughtMana = boughtMana;
        }
    }

    public class ArmourRecord : SkillRecord
    {
        public int ArmourAmount { get; }
        public ArmourRecord(long cost, int armourAmount) : base(cost)
        {
            ArmourAmount = armourAmount;
        }

    }

    public class ShieldRecord : SkillRecord
    {
        public int ShieldBreak { get; }
        public string ShieldType { get; }

        public ShieldRecord(long cost, int shieldBreak, string shieldType) : base(cost)
        {
            ShieldBreak = shieldBreak;
            ShieldType = shieldType;
        }
    }

    public class WeaponRecord : SkillRecord
    {
        public int WeaponDamage { get; }
        public string WeaponType { get; }

        public WeaponRecord(long cost, int weaponDamage, string weaponType) : base(cost)
        {
            WeaponDamage = weaponDamage;
            WeaponType = weaponType;
        }
    }

}
