using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WeaponsForm;
using WeaponsForm.character.record;
using WeaponsForm.Skills;

namespace WeaponsForm
{
    internal class MagicRowControls : SkillRowControls
    {
        public MagicRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel, Constants.Magic)
        {
        }

        protected override SkillRecord CreateRecord(long skillCost)
        {
            string skillType = (string)SkillTypeComboBox.SelectedItem;


            //TODO: I'm going to assume this will never be called when things like checkboxes are NOT set, 
            //thus skipping a bunch of error checking and if/else handling. Bet I'll find problems with this approach later...
            switch(skillType)
            {
                case "Recognise Magic":
                case "Read/Write School Runes":
                case "Meditate":
                    return new MagicRecord(skillCost, skillType);

                case "Enhance Mana":
                    var boughtMana = Decimal.ToInt64((SkillLevelControl as SkillLevelThresholdNumericField).Value);
                    return new ManaRecord(skillCost, boughtMana);

                case "Create Talisman":
                case "Transcend Armour":
                case "Enchant Item":
                case "Magic Ritual":
                case "Work with Cabal":
                    return new MagicRecord(skillCost, skillType + " " + (SkillLevelControl as NumericUpDown).Value);


                default:
                    throw new System.Exception("Unrecognised Magic skill type: '" + skillType + "'");

            }

        }
    }
}