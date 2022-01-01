using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponsForm.character.record;

namespace WeaponsForm
{
    public class RankTextBox : TextBox
    {
        //private List<Control> knownControls = new List<Control>();

        //TODO: Its silly to pass the panel here. Just pass the list!
        public RankTextBox(FlowLayoutPanel headerFlowLayoutPanel) : base()
        {
            (headerFlowLayoutPanel.FindForm() as WeaponsForm).SkillsList.CollectionChanged += RankTextBox_SkillsListChanged;
        }


        //public void RegisterRankCostTextBox(TextBox textBox)
        //{
        //    textBox.TextChanged += new EventHandler(UpdateTotalRank);

        //    knownControls.Add(textBox);
        //}

        private void RankTextBox_SkillsListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            long totalRank = 0;
            var skillsList = (sender as ObservableCollection<SkillRecord>);

            // The list may have null entries, filter them before costing
            var filteredList = skillsList.Where(x => x != null);

            foreach (SkillRecord skill in filteredList)
            {
                totalRank += skill.Cost;
            }

            this.Text = totalRank.ToString();

        }
      
    }
}
