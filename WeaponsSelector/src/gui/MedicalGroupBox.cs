using System.Windows.Forms;
using WeaponsForm;

namespace WeaponsSelector.src.gui
{
    internal class MedicalGroupBox : SkillGroupBoxWrapper
    {
        public MedicalGroupBox(FlowLayoutPanel skillsFlowLayoutPanel) : base(skillsFlowLayoutPanel)
        {
        }

        protected override string getGroupBoxText()
        {
            return Constants.Medical;
        }

        internal override AbstractRowControls getNewSkillRowControl(TableLayoutPanel tableLayoutPanel)
        {
            return new MedicalRowControls(tableLayoutPanel);
        }
    }
}