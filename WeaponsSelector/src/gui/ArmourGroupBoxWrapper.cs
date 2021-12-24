using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponsForm;

namespace WeaponsSelector.src.gui
{
    class ArmourGroupBoxWrapper : SkillGroupBoxWrapper
    {
        public ArmourGroupBoxWrapper(FlowLayoutPanel flowLayoutPanel) : base(flowLayoutPanel)
        {
        }
        protected override string getGroupBoxText()
        {
            return Constants.Armour;
        }

        internal override AbstractRowControls getNewSkillRowControl(TableLayoutPanel tableLayoutPanel)
        {
            return new ArmourRowControls(tableLayoutPanel);
        }
    }
}
