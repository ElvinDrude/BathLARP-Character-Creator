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
                    return CreateThresholdNumericField(skillType);
                case PurchaseType.Once:
                    return CreateCheckbox(skillType);
                case PurchaseType.CostPlusLevel:
                    throw new NotImplementedException("No CostPlusLevel yet");
                case PurchaseType.CostTimesLevel:
                    return CreateCostTimesLevelNumericField(skillType);
                case PurchaseType.CostTimesLevelPlusOne:
                    throw new NotImplementedException("No CostTimesLevelPlusOne yet");
                default:
                    throw new NotImplementedException("Unimplemented PurchaseType " + purchaseType);
            }
        }

        private static Control CreateCheckbox(SkillType skillType)
        {
            return new SkillLevelCheckbox(skillType)
            {
                Anchor = AnchorStyles.Top,
            };
        }

        private static Control CreateCostTimesLevelNumericField(SkillType skillType)
        {
            return new SkillLevelCostTimesLevelNumericField(skillType)
            {
                Anchor = AnchorStyles.Top,
                Minimum = 0,
                Value = 0,
            };
        }

        private static Control CreateThresholdNumericField(SkillType skillType)
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
