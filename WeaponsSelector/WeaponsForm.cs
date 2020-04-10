using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeaponsSelector;
using WeaponsSelector.src.gui;
using WeaponsForm.Skills;

namespace WeaponsForm
{
    public class WeaponsForm : Form
    {
        //private TableLayoutPanel tableLayoutPanel;
        //private TableLayoutPanel armourTableLayoutPanel;

        private FlowLayoutPanel headerFlowLayoutPanel;

        /// <summary>
        /// The top level rank textbox that needs to be updated when many different components change
        /// </summary>
        //TODO: Re-work this to remove the need for set - currently used in CreateHeaderFlowLayoutPanel
        public RankTextBox RankTextBox { get; private set; }

        //private IList<WeaponRowControls> weaponRowControls;
        //private IList<ArmourRowControls> armourRowControls;

        /// <summary>
        /// The current version of the JSON skill costs that all parts of the application should read from
        /// </summary>
        public JsonSkillReader JsonSkillReader { get; }

        public WeaponsForm()
        {
            //Top level form setup
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 600);
            this.Name = "Monster Generator";
            this.Text = "Monster Generator";

            this.SuspendLayout();

            JsonSkillReader = new JsonSkillReader();

            CreateHeaderFlowLayoutPanel();

            // Trying out a vertical flowlayoutpanel for weapons+armour skills...
            var skillsFlowLayoutPanel = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Left,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Location = new Point(0, headerFlowLayoutPanel.Height),
            };

            skillsFlowLayoutPanel.SuspendLayout();
            this.Controls.Add(skillsFlowLayoutPanel);

            new WeaponGroupBoxWrapper(skillsFlowLayoutPanel);
            new ArmourGroupBoxWrapper(skillsFlowLayoutPanel);

            skillsFlowLayoutPanel.ResumeLayout(false);
            skillsFlowLayoutPanel.PerformLayout();

            //Start creating a box for the Physical/Mental skills - these introduce at least three new challenges...
            //CreatePhysicalMentalGroupBox(skillsFlowLayoutPanel);
            new PhysicalMentalGroupBox(skillsFlowLayoutPanel);

            new MedicalGroupBox(skillsFlowLayoutPanel);



            this.ResumeLayout();
        }


        private void CreateHeaderFlowLayoutPanel()
        {
            headerFlowLayoutPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                //AutoSize = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Name = "HeaderFlowLayoutPanel",
                Size = new Size(this.Width, 50)
            };
            headerFlowLayoutPanel.SuspendLayout();

            Label nameLabel = new Label
            {
                //Dock = DockStyle.Left,
                Anchor = AnchorStyles.None,
                Text = "Name:",
                AutoSize = true,
            };
            headerFlowLayoutPanel.Controls.Add(nameLabel);

            TextBox nameTextBox = new TextBox
            {
                Anchor = AnchorStyles.Top,
                //Dock = DockStyle.Left
            };
            headerFlowLayoutPanel.Controls.Add(nameTextBox);

            Label rankLabel = new Label
            {
                //Dock = DockStyle.Left
                Anchor = AnchorStyles.None,
                Text = "Rank:",
                AutoSize = true
            };
            headerFlowLayoutPanel.Controls.Add(rankLabel);


            RankTextBox = new RankTextBox
            {
                Anchor = AnchorStyles.Top,
            };
            headerFlowLayoutPanel.Controls.Add(RankTextBox);

            this.Controls.Add(headerFlowLayoutPanel);

            headerFlowLayoutPanel.ResumeLayout(false);
            headerFlowLayoutPanel.PerformLayout();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new WeaponsForm());
        }
    }
}