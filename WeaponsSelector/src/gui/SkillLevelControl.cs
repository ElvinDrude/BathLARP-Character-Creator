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

    //public class MyEventArgs : EventArgs
    //{
    //    // class members  
    //}

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



}
