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
    public class WeaponRowControls : SkillRowControls
    {
        public WeaponRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel, Constants.Weapons)
        {
        }

        protected override SkillRecord CreateRecord(long skillCost)
        {
            throw new NotImplementedException();
        }
    }
}