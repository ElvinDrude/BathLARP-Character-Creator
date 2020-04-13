using System.Collections.Generic;
using System.Windows.Forms;
using WeaponsForm;
using WeaponsForm.Skills;

namespace WeaponsForm
{
    internal class MagicRowControls : SkillRowControls
    {
        public MagicRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel, Constants.Magic)
        {
        }

    }
}