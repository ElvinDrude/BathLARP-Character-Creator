using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeaponsForm
{
    public class RankTextBox : TextBox
    {
        private List<Control> knownControls = new List<Control>();

        public void RegisterRankCostTextBox(TextBox textBox)
        {
            textBox.TextChanged += new EventHandler(UpdateTotalRank);

            knownControls.Add(textBox);
        }

        private void UpdateTotalRank(object sender, EventArgs e)
        {
            int totalRank = 0;
            foreach (Control control in knownControls)
            {
                if (Int32.TryParse(control.Text, out int result))
                {
                    totalRank += result;
                }
                // We don't catch a failure as we often have a lot of empty textboxes - perhaps specifically look for ""?
                //else
                //{
                //    throw new Exception("Unable to parse as integer: " + control.Text);
                //}

            }

            this.Text = totalRank.ToString();
        }
    }
}
