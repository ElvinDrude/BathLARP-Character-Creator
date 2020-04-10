using System.Collections.Generic;
using System.Windows.Forms;
using WeaponsForm;
using WeaponsForm.Skills;

namespace WeaponsForm
{
    internal class MedicalRowControls : SkillRowControls
    {
        public MedicalRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel)
        {
            //Nothing to do!
        }

        internal override SkillType GetSkillType(string weaponType)
        {
            return (SkillLevelControl.FindForm() as WeaponsForm).JsonSkillReader.GetMedicalType(weaponType);
        }

        internal override List<SkillType> GetSkillTypesList(TableLayoutPanel skillTableLayoutPanel)
        {
            return (skillTableLayoutPanel.FindForm() as WeaponsForm).JsonSkillReader.GetMedicalSkills();
        }
    }
}