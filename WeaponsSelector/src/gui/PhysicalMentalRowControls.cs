using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeaponsSelector;

namespace WeaponsForm
{
    internal class PhysicalMentalRowControls : SkillRowControls
    {
        public PhysicalMentalRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel)
        {
        }

        internal override SkillType GetSkillType(string physicalMentalType)
        {
            return (SkillLevelComboBox.FindForm() as WeaponsForm).JsonSkillReader.GetPhysicalMentalType(physicalMentalType);
        }

        internal override List<SkillType> GetSkillTypesList(TableLayoutPanel skillTableLayoutPanel)
        {
            return (skillTableLayoutPanel.FindForm() as WeaponsForm).JsonSkillReader.GetPhysicalMentalSkills();
        }
    }
}