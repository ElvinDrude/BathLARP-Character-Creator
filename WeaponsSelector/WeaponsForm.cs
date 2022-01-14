using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using WeaponsSelector;
using WeaponsSelector.src.gui;
using WeaponsForm.Skills;
using WeaponsForm.Spells;
using WeaponsForm.character.record;
using WeaponsSelector.src.gui.components;
using System.Collections.Concurrent;

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

        public ObservableConcurrentDictionary<string, SkillRecord> SkillsDict { get; }

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

            SkillsDict = new ObservableConcurrentDictionary<string, SkillRecord>();

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


            CreateHeader(mainFlowLayoutPanel);

            CreateBody(mainFlowLayoutPanel);

            this.ResumeLayout();
        }


        private void CreateHeader(FlowLayoutPanel mainFlowLayoutPanel)
        {
            /* The header should span the full width of the Form, and contains
             * the rank textbox as well as a place to put save/load buttons in the future*/
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
            mainFlowLayoutPanel.Controls.Add(headerFlowLayoutPanel);

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


            RankTextBox = new RankTextBox(headerFlowLayoutPanel)
            {
                Anchor = AnchorStyles.Top,
            };
            headerFlowLayoutPanel.Controls.Add(RankTextBox);

            

            headerFlowLayoutPanel.ResumeLayout(false);
            headerFlowLayoutPanel.PerformLayout();
        }

        private void CreateBody(FlowLayoutPanel mainFlowLayoutPanel)
        {
            /* The body contains the skills selection along with the output character sheet */

            var bodyFlowLayoutPanel = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };
            mainFlowLayoutPanel.Controls.Add(bodyFlowLayoutPanel);

            CreateSkills(bodyFlowLayoutPanel);

            CreateCharacterSheet(bodyFlowLayoutPanel.Controls);
        }

        private static void CreateSkills(FlowLayoutPanel bodyFlowLayoutPanel)
        {
            var skillsFlowLayoutPanel = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Left,
                Dock = DockStyle.Left,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BorderStyle = BorderStyle.FixedSingle,
            };

            skillsFlowLayoutPanel.SuspendLayout();
            bodyFlowLayoutPanel.Controls.Add(skillsFlowLayoutPanel);

            new WeaponGroupBoxWrapper(skillsFlowLayoutPanel);
            new ArmourGroupBoxWrapper(skillsFlowLayoutPanel);

            skillsFlowLayoutPanel.ResumeLayout(false);
            skillsFlowLayoutPanel.PerformLayout();

            new PhysicalMentalGroupBox(skillsFlowLayoutPanel);

            new MedicalGroupBox(skillsFlowLayoutPanel);

            new MagicGroupBox(skillsFlowLayoutPanel);

            new SpellsGroupBox(skillsFlowLayoutPanel);

            new MiraclesGroupBox(skillsFlowLayoutPanel);
        }


        private void CreateCharacterSheet(Control.ControlCollection parent)
        {
            var outputFlowLayoutPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BorderStyle = BorderStyle.FixedSingle,
            };
            this.Controls.Add(outputFlowLayoutPanel);


            var characterSheetTextBox = new CharacterSheetTextBox(SkillsDict)
            {
                AutoSize = true,
                Multiline = true,
                Height = 200,
                ScrollBars = ScrollBars.Both,

            };
            outputFlowLayoutPanel.Controls.Add(characterSheetTextBox);

            parent.Add(outputFlowLayoutPanel);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new WeaponsForm());
        }
    }
}