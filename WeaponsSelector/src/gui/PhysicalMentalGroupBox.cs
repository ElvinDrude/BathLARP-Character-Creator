using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponsForm;

namespace WeaponsSelector.src.gui
{
    class PhysicalMentalGroupBox : SkillGroupBoxWrapper
    {
        public PhysicalMentalGroupBox(FlowLayoutPanel flowLayourPanel) : base(flowLayourPanel)
        { }
        protected override string getGroupBoxText()
        {
            return Constants.PhysicalMental;
        }

        internal override SkillRowControls getNewSkillRowControl(TableLayoutPanel tableLayoutPanel)
        {
            return new PhysicalMentalRowControls(tableLayoutPanel);
        }
    }
}
