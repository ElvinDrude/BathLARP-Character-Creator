using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponsForm;

namespace WeaponsSelector.src.gui
{
    class WeaponGroupBoxWrapper : SkillGroupBoxWrapper
    {
        public WeaponGroupBoxWrapper(FlowLayoutPanel flowLayoutPanel) : base(flowLayoutPanel)
        {
        }

        protected override string getGroupBoxText()
        {
            return Constants.Weapons;
        }

        internal override AbstractRowControls getNewSkillRowControl(TableLayoutPanel tableLayoutPanel)
        {
            return new WeaponRowControls(tableLayoutPanel);
        }
    }
}
