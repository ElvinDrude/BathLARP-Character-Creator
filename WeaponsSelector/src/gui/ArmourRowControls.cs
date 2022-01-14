using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponsForm;
using WeaponsSelector;
using WeaponsForm.Skills;
using WeaponsForm.character.record;

namespace WeaponsForm
{
    public class ArmourRowControls : SkillRowControls
    {

        public ArmourRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel, Constants.Armour)
        {
        }

        protected override SkillRecord CreateRecord(long skillCost)
        {
            string skillType = (string)SkillTypeComboBox.SelectedItem;

            if (skillType == "Combination")
            {
                //TODO: These inner if checks may not be necessary - this code should only execute 
                // when both the type and level are active.
                // +2 as Combination both removes the stacking penalty AND provides an extra armour
                if ((SkillLevelControl as CheckBox).Checked)
                {
                    return new ArmourRecord(skillCost, 2);
                }
                else
                {
                    return null;
                }    
            }
            else if(skillType == "Enhanced Combination")
            {
                if ((SkillLevelControl as CheckBox).Checked)
                {
                    return new ArmourRecord(skillCost, 1);
                }
                else
                {
                    return null;
                }
            }
                        
            int baseArmour = 0;
            
            switch(skillType)
            {
                //TODO: Skills JSON needs to split Furs and Leather into separate entries
                case "Furs":
                    baseArmour = 1;
                    break;
                case "Leather":
                    baseArmour = 2;
                    break;
                case "Studded Leather":
                    baseArmour = 3;
                    break;
                case "Chain":
                    baseArmour = 4;
                    break;
                case "Banded":
                    baseArmour = 5;
                    break;
                case "Plate":
                    baseArmour = 6;
                    break;

                default:
                    throw new Exception("Unrecognised skillType '" + skillType + "'");
            }

            string skillLevel = (string)(SkillLevelControl as ComboBox).SelectedItem;
            int skillModifier = 0;

            switch(skillLevel)
            {
                case Constants.None:
                    return null;
                case Constants.Proficiency:
                    break; // Prof gives no extra armour ontop of the base
                case Constants.Specialisation:
                    skillModifier = 1;
                    break;
                case Constants.Expertise:
                    skillModifier = 2;
                    break;
                case Constants.Mastery:
                    skillModifier = 3;
                    break;
                case Constants.AdvancedMastery:
                    skillModifier = 4;
                    break;
                case Constants.LegendaryMastery:
                    skillModifier = 5;
                    break;
                default:
                    throw new Exception("Unrecognised skillLevel '" + skillLevel + "'");
            }

            // Rule of double means skill cannot add more than base already provided
            if (skillModifier > baseArmour)
            {
                skillModifier = baseArmour;
            }

            return new ArmourRecord(skillCost, baseArmour + skillModifier);
        }
    }
}
