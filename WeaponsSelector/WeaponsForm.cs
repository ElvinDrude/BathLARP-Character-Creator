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
using WeaponsForm.Spells;

namespace WeaponsForm
{
    public class WeaponsForm : Form
    {
        private FlowLayoutPanel headerFlowLayoutPanel;

        /// <summary>
        /// The top level rank textbox that needs to be updated when many different components change
        /// </summary>
        //TODO: Re-work this to remove the need for set - currently used in CreateHeaderFlowLayoutPanel
        public RankTextBox RankTextBox { get; private set; }


        /// <summary>
        /// The current version of the JSON skill costs that all parts of the application should read from
        /// </summary>
        public JsonSkillReader JsonSkillReader { get; }

        public JsonSpellReader JsonSpellReader { get; }

        public JsonMiracleReader JsonMiracleReader { get; }

        public WeaponsForm()
        {
            //Top level form setup
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1000, 1000);
            this.Name = "Monster Generator";
            this.Text = "Monster Generator";

            JsonSkillReader = new JsonSkillReader();
            JsonSpellReader = new JsonSpellReader();
            JsonMiracleReader = new JsonMiracleReader();

            this.SuspendLayout();

            var mainFlowLayoutPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
            };
            this.Controls.Add(mainFlowLayoutPanel);


            CreateHeaderFlowLayoutPanel(mainFlowLayoutPanel);

            var skillsFlowLayoutPanel = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Left,
                Dock = DockStyle.Left,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                //Location = new Point(0, headerFlowLayoutPanel.Height),
                BorderStyle = BorderStyle.FixedSingle,
            };

            skillsFlowLayoutPanel.SuspendLayout();
            mainFlowLayoutPanel.Controls.Add(skillsFlowLayoutPanel);

            new WeaponGroupBoxWrapper(skillsFlowLayoutPanel);
            new ArmourGroupBoxWrapper(skillsFlowLayoutPanel);

            skillsFlowLayoutPanel.ResumeLayout(false);
            skillsFlowLayoutPanel.PerformLayout();

            new PhysicalMentalGroupBox(skillsFlowLayoutPanel);

            new MedicalGroupBox(skillsFlowLayoutPanel);

            new MagicGroupBox(skillsFlowLayoutPanel);

            new SpellsGroupBox(skillsFlowLayoutPanel);

            new MiraclesGroupBox(skillsFlowLayoutPanel);


            //var outputFlowLayoutPanel = new FlowLayoutPanel
            //{
            //    Dock = DockStyle.Right,
            //    Anchor = AnchorStyles.Right | AnchorStyles.Top,
            //    FlowDirection = FlowDirection.TopDown,
            //    AutoSize = true,
            //    AutoSizeMode = AutoSizeMode.GrowAndShrink,
            //};
            //this.Controls.Add(outputFlowLayoutPanel);


            //var outputTextBox = new TextBox
            //{
            //    AutoSize = true,
            //    Multiline = true,
            //    ScrollBars = ScrollBars.Both,

            //};
            //outputFlowLayoutPanel.Controls.Add(outputTextBox);


            this.ResumeLayout();
        }


        private void CreateHeaderFlowLayoutPanel(FlowLayoutPanel mainFlowLayoutPanel)
        {
            headerFlowLayoutPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Name = "HeaderFlowLayoutPanel",
                BorderStyle = BorderStyle.FixedSingle,
                Width = this.Width,
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

            mainFlowLayoutPanel.Controls.Add(headerFlowLayoutPanel);

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