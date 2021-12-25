using System.Windows.Forms;
using WeaponsSelector.src.gui;

namespace WeaponsForm
{
    internal class MiraclesGroupBox : SkillGroupBoxWrapper
    {

        public MiraclesGroupBox(FlowLayoutPanel skillsFlowLayoutPanel) : base(skillsFlowLayoutPanel)
        {
        }

        protected override string getGroupBoxText()
        {
            return Constants.Miracles;
        }

        internal override AbstractRowControls getNewSkillRowControl(TableLayoutPanel tableLayoutPanel)
        {
            return new SpellsRowControls(tableLayoutPanel, (tableLayoutPanel.FindForm() as WeaponsForm).JsonMiracleReader);
        }
    }
}