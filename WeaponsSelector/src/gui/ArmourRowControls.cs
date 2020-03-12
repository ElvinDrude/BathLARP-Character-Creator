using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponsForm;
using WeaponsSelector;

namespace WeaponsForm
{
    public class ArmourRowControls : SkillRowControls
    {
        public ArmourRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel)
        {
            //Nothing to do!
        }

        internal override SkillType GetSkillType(string weaponType)
        {
            return (SkillLevelComboBox.FindForm() as WeaponsForm).JsonSkillReader.GetArmourType(weaponType);
        }

        internal override List<SkillType> GetSkillTypesList(TableLayoutPanel skillTableLayoutPanel)
        {
            return (skillTableLayoutPanel.FindForm() as WeaponsForm).JsonSkillReader.GetArmourSkills();
        }
    }
}
