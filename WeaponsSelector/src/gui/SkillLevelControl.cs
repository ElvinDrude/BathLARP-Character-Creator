using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeaponsForm
{
    /// <summary>
    /// Interface for unifying all the possible GUI components necessary to deal with all the different PurchaseType s
    /// </summary>
    interface ISkillLevelControl
    {
        event EventHandler ValueChanged;

        long GetSkillLevelCost();

        SkillType SkillType
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Pseudo-base class for creating various Skill Level components. As the components we want to use are actually system ones,
    /// we can't both have Control as our base class and inherit the extra interface. So some casting is necessary when using it...
    /// TODO: This should be re-done in a way that doesn't need casting. Maybe create facades for each of the components?
    /// </summary>
    abstract class AbstractSkillLevelControl : Control, ISkillLevelControl
    {
        public abstract SkillType SkillType { get; set; }

        public abstract event EventHandler ValueChanged;

        public abstract long GetSkillLevelCost();
    }

    public class SkillLevelComboBox : ComboBox, ISkillLevelControl
    {
        public SkillType SkillType { get; set; }

        public event EventHandler ValueChanged;

        public SkillLevelComboBox(SkillType skillType) : base()
        {
            this.SelectedValueChanged += mapSelectedValueChangedToValueChanged;
            SkillType = skillType;
        }

        private void mapSelectedValueChangedToValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(sender, e);
        }

        public long GetSkillLevelCost()
        {
            return SkillType.GetSkillLevelCost((string)this.SelectedItem);
        }
    }

    public abstract class SkillLevelNumericField : NumericUpDown, ISkillLevelControl
    {
        public SkillType SkillType { get; set; }

        //The base event type for NumericUpDown is called ValueChanged, the same as in the ISkillLevelControl interface
        //public event EventHandler ValueChanged;

        public SkillLevelNumericField(SkillType skillType) : base()
        {
            SkillType = skillType;
        }

        public abstract long GetSkillLevelCost();
    }

    public class SkillLevelThresholdNumericField : SkillLevelNumericField
    {
        public SkillLevelThresholdNumericField(SkillType skill) : base(skill)
        {

        }

        public override long GetSkillLevelCost()
        {
            //TODO: Work out how to properly deal with the ?. operators   
            var thresholdIncrement = SkillType.Threshold.GetValueOrDefault();
            var startCost = SkillType.Cost;

            int currThreshold = 0;

            var currCostOfEachPoint = startCost + currThreshold;

            //Check these casts
            var tmp = (double)Decimal.ToInt64(this.Value) / thresholdIncrement;
            int maxThreshold = (int)Math.Ceiling(tmp);

            long runningLifeBought = 0;
            long runningCost = 0;

            for (int i = 0; i < maxThreshold; i++)
            {
                while (runningLifeBought < thresholdIncrement * (currThreshold + 1) && runningLifeBought < Decimal.ToInt64(this.Value))
                {
                    runningCost += currCostOfEachPoint.GetValueOrDefault();
                    runningLifeBought += 1;
                }
                currThreshold += 1;
                currCostOfEachPoint++;
            }

            return runningCost;

        }
    }

}
