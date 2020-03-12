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

namespace WeaponsForm
{
    public class WeaponsForm : Form
    {
        private TableLayoutPanel tableLayoutPanel;
        //private TableLayoutPanel armourTableLayoutPanel;

        private FlowLayoutPanel headerFlowLayoutPanel;

        /// <summary>
        /// The top level rank textbox that needs to be updated when many different components change
        /// </summary>
        //TODO: Re-work this to remove the need for set - currently used in CreateHeaderFlowLayoutPanel
        public RankTextBox RankTextBox { get; private set; }

        private IList<WeaponRowControls> weaponRowControls;
        private IList<ArmourRowControls> armourRowControls;

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

            

            this.ResumeLayout();
        }

        private void CreatePhysicalMentalGroupBox(FlowLayoutPanel flowLayoutPanel)
        {
            GroupBox groupBox = new GroupBox
            {
                Dock = DockStyle.Left,
                Anchor = AnchorStyles.Left,
                Text = Constants.PhysicalMental,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            flowLayoutPanel.Controls.Add(groupBox);
            //Trying out no Suspend/ResumeLayout layout calls

            var innerFlowLayoutPanel = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Top,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };

            innerFlowLayoutPanel.SuspendLayout();
            groupBox.Controls.Add(innerFlowLayoutPanel);

            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                Name = SKILL_TABLE_IDENTIFIER,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };
            innerFlowLayoutPanel.Controls.Add(tableLayoutPanel);

            foreach(var skillType in JsonSkillReader.GetPhysicalMentalSkills())
            {
                //if(skillType.Levels != null)
                //{

                //}
                //else if(skillType.Threshold != null)
                //{
                  
                //}
                //else if (skillType.BuyOnce != null)
                //{

                //}
                //else if (skillType.PerLevelMultiplier != null)
                //{

                //}
                //else
                //{
                //    throw new Exception("Unrecognised skill found in CreatePhysicalMentalGroupBox: " + skillType.ToString());
                //}

            }


        }

        private const string SKILL_TABLE_IDENTIFIER = "skillTableLayoutPanelIdentifier";
        private void CreateSkillGroupBox(FlowLayoutPanel flowLayoutPanel, Type skillToCreate)
        {
            GroupBox groupBox = new GroupBox
            {
                Dock = DockStyle.Left,
                Anchor = AnchorStyles.Left,
                //Text = groupBoxText, // Moved to lower if statement
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };
            flowLayoutPanel.Controls.Add(groupBox);
            groupBox.SuspendLayout();

            //Add a flow control inside the groupbox to handle the table and button dynamically
            var innerFlowLayoutPanel = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Top,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };
            innerFlowLayoutPanel.SuspendLayout();
            groupBox.Controls.Add(innerFlowLayoutPanel);

            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                Name = SKILL_TABLE_IDENTIFIER,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };
            innerFlowLayoutPanel.Controls.Add(tableLayoutPanel);

            string groupBoxText;
            EventHandler onClickAddNewSkillRowEventHandler;
            if (skillToCreate == typeof(WeaponRowControls))
            {
                groupBoxText = Constants.Weapons;

                weaponRowControls = new List<WeaponRowControls>
                {
                //Default to having a single weapons row
                    new WeaponRowControls(tableLayoutPanel)
                };

                onClickAddNewSkillRowEventHandler = addWeaponButton_Click;
            }
            else
            {
                groupBoxText = Constants.Armour;

                armourRowControls = new List<ArmourRowControls>
                {
                    new ArmourRowControls(tableLayoutPanel)
                };

                onClickAddNewSkillRowEventHandler = addArmourButton_Click;
            }
            groupBox.Text = groupBoxText;

            var addSkillButton = new Button
            {
                Text = "Add " + groupBoxText,
                AutoSize = true,
            };
            //addSkillButton.Click += addWeaponButton_Click;
            addSkillButton.Click += onClickAddNewSkillRowEventHandler;

            innerFlowLayoutPanel.Controls.Add(addSkillButton);

            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();

            innerFlowLayoutPanel.ResumeLayout(false);
            innerFlowLayoutPanel.PerformLayout();
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


        //TODO: These _Click handlers can probably be refactored to get the type they are supposed to create from the object that was clicked 
        // e.g. by the Tag, so perhaps use that instead to avoid duplicate handlers?
        private void addWeaponButton_Click(object sender, System.EventArgs e)
        {
            Control[] tmpControl = (sender as Button).Parent.Controls.Find(SKILL_TABLE_IDENTIFIER, true);

            if (tmpControl.Length == 1 && tmpControl[0] is TableLayoutPanel)
            {
                weaponRowControls.Add(new WeaponRowControls(tmpControl[0] as TableLayoutPanel));
            }
            else
            {
                throw new Exception("Could not find table layout panel with identifier '" + SKILL_TABLE_IDENTIFIER + "' in addWeaponButton_Click");
            }

        }

        private void addArmourButton_Click(object sender, EventArgs e)
        {

            Control[] tmpControl = (sender as Button).Parent.Controls.Find(SKILL_TABLE_IDENTIFIER, true);

            if (tmpControl.Length == 1 && tmpControl[0] is TableLayoutPanel)
            {
                armourRowControls.Add(new ArmourRowControls(tmpControl[0] as TableLayoutPanel));
            }
            else
            {
                throw new Exception("Could not find table layout panel with identifier '" + SKILL_TABLE_IDENTIFIER + "' in addArmourButton_Click");
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new WeaponsForm());
        }
    }
}