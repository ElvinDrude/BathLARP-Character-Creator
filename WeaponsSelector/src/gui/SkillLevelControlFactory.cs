using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeaponsForm
{
    static class SkillLevelControlFactory
    {
        internal static Control CreateNewSkillLevelControl(SkillType skillType)
        {
            PurchaseType purchaseType = skillType.PurchaseType;

            switch (purchaseType)
            {
                case PurchaseType.Levels:
                    return CreateComboBox(skillType);
                case PurchaseType.Threshold:
                    return CreateNumericField(skillType);
                    break;
                case PurchaseType.Once:
                    break;
                case PurchaseType.CostPlusLevel:
                    break;
                case PurchaseType.CostTimesLevel:
                    break;
                case PurchaseType.CostTimesLevelPlusOne:
                    break;
                default:
                    throw new NotImplementedException("Unimplemented PurchaseType " + purchaseType);
            }

            return CreateComboBox(skillType);
            //return new Control();

        }

        private static Control CreateNumericField(SkillType skillType)
        {
            return new SkillLevelThresholdNumericField(skillType)
            {
                Anchor = AnchorStyles.Top,
                Minimum = 0,
                Value = 0,
            };
        }

        private static Control CreateComboBox(SkillType skillType)
        {
            SkillLevelComboBox skillLevelComboBox = new SkillLevelComboBox(skillType)
            {
                Anchor = AnchorStyles.Top,
                //Name = "SkillLevelComboBox#" + skillTableLayoutPanel.RowCount.ToString(),
                DropDownStyle = ComboBoxStyle.DropDownList,
                //Enabled = false, // Disabled until the SkillTypeComboBox has a value picked
            };

            skillLevelComboBox.Items.Clear();
            skillLevelComboBox.Items.AddRange(skillType.GetValidSkillLevels());
            skillLevelComboBox.SelectedIndex = 0;

            return skillLevelComboBox;
        }
    }
}
