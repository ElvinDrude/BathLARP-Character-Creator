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

    public class SkillLevelCheckbox : CheckBox, ISkillLevelControl
    {
        public SkillType SkillType { get; set; }

        public event EventHandler ValueChanged;

        public SkillLevelCheckbox(SkillType skillType) : base()
        {
            SkillType = skillType;
            this.CheckedChanged += mapCheckedChangedToValueChanged;
        }

        private void mapCheckedChangedToValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(sender, e);
        }

        public long GetSkillLevelCost()
        {
            // Checkboxes indicate a one-time only skill purchase
            return this.Checked ? SkillType.Cost.GetValueOrDefault() : 0;
        }
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

            // Count up from 0 to the maximum number of thresholds they've bought through (including any partial ones, so 
            // maxThreshold is always greater than 0 if life greater than 0)
            for (int i = 0; i < maxThreshold; i++)
            {
                while (runningLifeBought < thresholdIncrement * (currThreshold + 1) && runningLifeBought < Decimal.ToInt64(this.Value))
                {
                    runningCost += currCostOfEachPoint.GetValueOrDefault();
                    runningLifeBought += 1;
                }
                currThreshold += 1;
                currCostOfEachPoint++; // Each time we reach a threshold, increase future cost by one.
            }

            return runningCost;

        }
    }

    public class SkillLevelCostTimesLevelNumericField : SkillLevelNumericField
    {
        public SkillLevelCostTimesLevelNumericField(SkillType skill) : base(skill)
        {

        }

        public override long GetSkillLevelCost()
        {
            // Triangular numbers formula: cost * .5 * N * (N+1), where N is number of levels to purchase
            double cost = SkillType.Cost.GetValueOrDefault() * 0.5 * Decimal.ToDouble(this.Value) * Decimal.ToDouble(this.Value + 1);
            return (long)cost;

        }
    }

}
