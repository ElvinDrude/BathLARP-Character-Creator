using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponsForm.character.record;

namespace WeaponsSelector.src.gui.components
{
    class CharacterSheetTextBox : TextBox
    {
        public CharacterSheetTextBox(ObservableCollection<SkillRecord> skillsList) : base()
        {
            skillsList.CollectionChanged += CharacterSheetTextBox_SkillsListChanged;


        }

        private void CharacterSheetTextBox_SkillsListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Text = "";

            var skillsList = (sender as ObservableCollection<SkillRecord>);

            CreateArmourText(skillsList);

        }

        private void CreateArmourText(ObservableCollection<SkillRecord> skillsList)
        {
            var list = skillsList.OfType<ArmourRecord>().ToList();

            int totalArmour = 0;

            if(list.Count > 1)
            {
                // Stacking two layers of armour gives a -1 penalty.
                // Note this isn't perfect logic due to how Combination works...
                totalArmour -= 1;
            }

            foreach(var record in list)
            {
                totalArmour += record.ArmourAmount;
            }


            this.Text += "Armour: " + totalArmour + "\n";
        }
    }
}
