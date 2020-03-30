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
    }

    abstract class AbstractSkillLevelControl : Control, ISkillLevelControl
    {
        public abstract event EventHandler ValueChanged;
    }

    //public class MyEventArgs : EventArgs
    //{
    //    // class members  
    //}

    public class SkillLevelComboBox : ComboBox, ISkillLevelControl
    {
        public event EventHandler ValueChanged;
    }



}
