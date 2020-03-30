using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeaponsSelector;

namespace WeaponsForm
{
    public class WeaponRowControls : SkillRowControls
    {
        public WeaponRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel)
        {
            //Nothing to do
        }

        internal override SkillType GetSkillType(string weaponType)
        {
            return (SkillLevelControl.FindForm() as WeaponsForm).JsonSkillReader.GetWeaponType(weaponType);
        }

        internal override List<SkillType> GetSkillTypesList(TableLayoutPanel skillTableLayoutPanel)
        {
            return (skillTableLayoutPanel.FindForm() as WeaponsForm).JsonSkillReader.GetWeaponsSkills();
        }


    }
}