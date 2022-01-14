using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeaponsSelector;
using WeaponsForm.Skills;
using WeaponsForm.character.record;

namespace WeaponsForm
{
    public class WeaponRowControls : SkillRowControls
    {
        public WeaponRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel, Constants.Weapons)
        {
        }

        protected override SkillRecord CreateRecord(long skillCost)
        {
            //TODO: Weapon Adepts! Kinda important!

            string skillType = (string)SkillTypeComboBox.SelectedItem;

            int baseDamage = 0;
            string shieldType = null;
            switch(skillType)
            {
                case "One Handed":
                    baseDamage = 4;
                    break;
                case "Two Handed":
                    baseDamage = 7;
                    break;
                case "Fist":
                    baseDamage = 1;
                    break;
                case "Claw":
                    baseDamage = 2;
                    break;
                case "Dagger":
                    baseDamage = 2;
                    break;
                case "Staff":
                    baseDamage = 6;
                    break;
                case "Throwing":
                    baseDamage = 2;
                    break;
                case "Longbow":
                    baseDamage = 6;
                    break;
                case "One Hand Crossbow":
                    baseDamage = 4;
                    break;
                case "Two Hand Crossbow":
                    baseDamage = 7;
                    break;

                case "Small Shield":
                    shieldType = "Small";
                    break;
                case "Medium Shield":
                    shieldType = "Medium";
                    break;
                case "Large Shield":
                    shieldType = "Large";
                    break;
                default:
                    throw new Exception("Unrecognised skillType '" + skillType + "'");
            }

            string skillLevel = (string)(SkillLevelControl as ComboBox).SelectedItem;

            if (shieldType != null)
            {
                int shieldBreak = 13;

                switch (skillLevel)
                {
                    case Constants.None:
                        return null;
                    case Constants.Proficiency:
                        break; // Prof gives no additional protection above base
                    case Constants.Specialisation:
                        shieldBreak = 16;
                        break;
                    case Constants.Expertise:
                        shieldBreak = 19;
                        break;
                    case Constants.Mastery:
                        shieldBreak = 22;
                        break;
                    case Constants.AdvancedMastery:
                        shieldBreak = 25;
                        break;
                    case Constants.LegendaryMastery:
                        shieldBreak = 28;
                        break;
                    default:
                        throw new Exception("Unrecognised skillLevel '" + skillLevel + "'");
                }

                return new ShieldRecord(skillCost, shieldBreak, shieldType);
            }

            // Have a weapon, not a shield

            int skillModifier = 0;

            switch (skillLevel)
            {
                case Constants.None:
                    return null;
                case Constants.Proficiency:
                    break; // Prof gives no extra armour ontop of the base
                case Constants.Specialisation:
                    skillModifier = 2;
                    break;
                case Constants.Expertise:
                    skillModifier = 4;
                    break;
                case Constants.Mastery:
                    skillModifier = 6;
                    break;
                case Constants.AdvancedMastery:
                    skillModifier = 8;
                    break;
                case Constants.LegendaryMastery:
                    skillModifier = 10;
                    break;
                default:
                    throw new Exception("Unrecognised skillLevel '" + skillLevel + "'");
            }

            return new WeaponRecord(skillCost, baseDamage + skillModifier, skillType);
        }
    }
}