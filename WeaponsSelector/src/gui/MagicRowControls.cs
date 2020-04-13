using System.Collections.Generic;
using System.Windows.Forms;
using WeaponsForm;
using WeaponsForm.Skills;

namespace WeaponsForm
{
    internal class MagicRowControls : SkillRowControls
    {
        public MagicRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel)
        {
        }

        internal override SkillType GetSkillType(string weaponType)
        {
            return (SkillLevelControl.FindForm() as WeaponsForm).JsonSkillReader.GetMagicType(weaponType);
        }

        internal override List<SkillType> GetSkillTypesList(TableLayoutPanel skillTableLayoutPanel)
        {
            return (skillTableLayoutPanel.FindForm() as WeaponsForm).JsonSkillReader.GetMagicSkills();
        }
    }
}