using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeaponsSelector
{
    public partial class Form1 : Form
    {

        WeaponSkillController weaponSkillController = new WeaponSkillController();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.clawSkillLevel.DataSource = new SkillLevelSource();
            JsonSkillReader jsonSkillReader = new JsonSkillReader();
        }

        private void clawSkillLevel_SelectedValueChanged(object sender, EventArgs e)
        {
            string currLevel = (string)clawSkillLevel.SelectedItem;

            int totalCost = weaponSkillController.GetTotalCost("Claw", currLevel);

            this.clawCost.Text = totalCost.ToString();
        }
    }
}
