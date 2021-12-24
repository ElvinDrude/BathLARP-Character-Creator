using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponsForm;

namespace WeaponsSelector.src.gui
{
    class MagicGroupBox : SkillGroupBoxWrapper
    {
        public MagicGroupBox(FlowLayoutPanel flowLayoutPanel) : base(flowLayoutPanel)
        {
        }

        protected override string getGroupBoxText()
        {
            return Constants.Magic;
        }

        internal override AbstractRowControls getNewSkillRowControl(TableLayoutPanel tableLayoutPanel)
        {
            return new MagicRowControls(tableLayoutPanel);
        }
    }
}
