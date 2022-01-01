using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeaponsSelector;
using WeaponsForm.Skills;
using WeaponsForm.character.record;

namespace WeaponsForm
{
    internal class PhysicalMentalRowControls : SkillRowControls
    {
        public PhysicalMentalRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel, Constants.PhysicalMental)
        {
        }

        protected override SkillRecord CreateRecord(long skillCost)
        {
            throw new NotImplementedException();
        }
    }
}