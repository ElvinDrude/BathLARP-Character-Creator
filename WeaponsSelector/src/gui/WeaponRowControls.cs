using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeaponsSelector;
using WeaponsForm.Skills;

namespace WeaponsForm
{
    public class WeaponRowControls : SkillRowControls
    {
        public WeaponRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel, Constants.Weapons)
        {
        }


    }
}