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

    public enum NumHands
    {
        OneHanded,
        TwoHanded
    }

    public enum StrengthLevel
    {
        None,
        Single = 1, // Specified so that we can use numeric value for maths operations
        Double,
        Triple,
        Quadruple,
        Quintuple,
        Sextuple,
        Septuple,
        Octuple,
        Nonuple,
        Dectuple
    }

    public class WeaponRecord : SkillRecord
    {
        public string WeaponType { get; }
        private int BaseDamage { get; }
        private int SkillDamage { get; }

        private NumHands NumHands { get; }

        public WeaponRecord(long cost, int baseDamage, int skillModifier, NumHands numHands, string weaponType) : base(cost)
        {
            WeaponType = weaponType;
            BaseDamage = baseDamage;
            SkillDamage = skillModifier;
            NumHands = numHands;
        }

        public int GetDamage(StrengthLevel strLevel)
        {
            int damage = BaseDamage + SkillDamage;

            // Strength may only add damage up (and including) to the base damage
            if ((int)strLevel > BaseDamage)
            {
                strLevel = (StrengthLevel)BaseDamage;
            }

            if (NumHands == NumHands.OneHanded)
            {
                damage += (int)strLevel;
            }
            else
            {
                damage += 2 * (int)strLevel;
            }

            return damage;
        }
    }

    public class StrengthRecord : SkillRecord
    {
        public StrengthLevel StrengthLevel { get; }

        public StrengthRecord(long cost, string strengthLevel) : base(cost)
        {
            var strLevel = StrengthLevel.None;
            var success = Enum.TryParse<StrengthLevel>(strengthLevel, out strLevel);
            if (success)
            {
                StrengthLevel = strLevel;
            }
            else
            {
                throw new Exception("Unrecognised Strength value " + strengthLevel);
            }


        }


    }

    public class EnhanceLifeRecord : SkillRecord
    {
        public int LifeBought { get; }

        public EnhanceLifeRecord(long cost, int lifeBought) : base(cost)
        {
            LifeBought = lifeBought;
        }
    }

}
