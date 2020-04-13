using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponsForm;
using WeaponsSelector;
using WeaponsForm.Skills;

namespace WeaponsForm
{
    public class ArmourRowControls : SkillRowControls
    {
        public ArmourRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel, Constants.Armour)
        {
        }
    }
}
