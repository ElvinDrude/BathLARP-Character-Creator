using System.Collections.Generic;
using System.Windows.Forms;
using WeaponsForm.Skills;

namespace WeaponsForm
{
    internal class SpellsRowControls : SkillRowControls
    {
        public SpellsRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel)
        {
        }

        internal override SkillType GetSkillType(string weaponType)
        {
            throw new System.NotImplementedException();
        }

        internal override List<SkillType> GetSkillTypesList(TableLayoutPanel skillTableLayoutPanel)
        {
            throw new System.NotImplementedException();
        }
    }
}