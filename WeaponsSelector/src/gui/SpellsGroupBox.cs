using System.Windows.Forms;
using WeaponsSelector.src.gui;

namespace WeaponsForm
{
    internal class SpellsGroupBox : SkillGroupBoxWrapper
    {

        public SpellsGroupBox(FlowLayoutPanel skillsFlowLayoutPanel) : base(skillsFlowLayoutPanel)
        {
        }

        protected override string getGroupBoxText()
        {
            return Constants.Spells;
        }

        internal override AbstractRowControls getNewSkillRowControl(TableLayoutPanel tableLayoutPanel)
        {
            return new SpellsRowControls(tableLayoutPanel);
        }
    }
}