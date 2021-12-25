using System.Collections.Generic;
using System.Windows.Forms;
using WeaponsForm;
using WeaponsForm.Skills;

namespace WeaponsForm
{
    internal class MedicalRowControls : SkillRowControls
    {
        public MedicalRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel, Constants.Medical)
        {
        }
    }
}