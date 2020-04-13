using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WeaponsForm.Skills;

namespace WeaponsForm
{
    internal class SpellsRowControls : SkillRowControls
    {
        public SpellsRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel, null)
        {
            // SkillCategoryIdentifier deliberately left as null, as we don't structure the Spells JSON file in the same way - 
            // it's one level flatter, so no need
        }

        //TODO: This is too tightly 
        internal override List<SkillType> GetSkillTypesList(TableLayoutPanel skillTableLayoutPanel)
        {
            // For now I'm going to fudge this by converting SpellSchool objects into SkillType objects.
            // TODO: Create an AbstractRowControls class, and then have a SkillRowControls and a CastingsRowControls subclasses.

            var spellSchools = (skillTableLayoutPanel.FindForm() as WeaponsForm).JsonSpellReader.GetSpellSchools();

            var skillTypes = new List<SkillType>();

            foreach ( var spellSchool in spellSchools)
            {
                skillTypes.Add(new SkillType { Name = spellSchool.School });
            }

            return skillTypes;
        }

        internal override SkillType GetSkillType(string spellSchool)
        {
            var spell =  (SkillLevelControl.FindForm() as WeaponsForm).JsonSpellReader.GetSpells(spellSchool);


        }
    }
}