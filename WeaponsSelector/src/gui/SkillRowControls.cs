using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeaponsSelector;
using WeaponsForm.Skills;
using WeaponsSelector.src.gui;
using WeaponsForm.character.record;

namespace WeaponsForm
{
    public abstract class SkillRowControls : AbstractRowControls
    {
        public ComboBox SkillTypeComboBox { get; }
        //public SkillLevelComboBox SkillLevelComboBox { get; }
        public Control SkillLevelControl { get; set; }
        public TextBox SkillCostTextBox { get; }

        // A string that identifies the subclass's actual type (weapons, armour, etc.).
        protected string SkillCategoryIdentifier;

        // Track the row index of this item inside the parent table. Used as part of a unique identifier.
        protected int RowCount;

        /// <summary>
        /// Create a new set of components defining a skill, and make them a new row at the bottom of
        /// the given TableLayoutPanel
        /// </summary>
        /// <param name="skillTableLayoutPanel">The table to which the new weapons controls will be added. Must already be added to the parent form.</param>
        public SkillRowControls(TableLayoutPanel skillTableLayoutPanel, string skillCategoryIdentifier) : base(skillTableLayoutPanel)
        {
            SkillCategoryIdentifier = skillCategoryIdentifier;

            skillTableLayoutPanel.RowCount += 1;
            RowCount = skillTableLayoutPanel.RowCount;

            SkillTypeComboBox = new ComboBox
            {
                Anchor = AnchorStyles.Top,
                //Name = "SkillTypeComboBox#" + skillTableLayoutPanel.RowCount.ToString(),
                MaximumSize = new Size(100, 20),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Tag = this,
            };

            // Gather and list all possible skills in the combobox
            var skillTypesList = GetSkillTypesList(skillTableLayoutPanel);
            string[] skillTypesArray = skillTypesList.ToArray().Select(weap => weap.Name).ToArray();
            SkillTypeComboBox.Items.AddRange(skillTypesArray);
            SkillTypeComboBox.SelectedIndex = -1;

            SkillTypeComboBox.SelectedValueChanged += SkillType_SelectedValueChanged;

            skillTableLayoutPanel.Controls.Add(SkillTypeComboBox, 0, RowCount);

            //This is basically a placeholder, to be replaced later with the right kind of control for the selected skill.
            //All this really serves as is a spacer. 
            //TODO: See if there's a proper spacer control!
            SkillLevelControl = new SkillLevelComboBox(null)
            {
                Anchor = AnchorStyles.Top,
                //Name = "SkillLevelComboBox#" + skillTableLayoutPanel.RowCount.ToString(),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Enabled = false, // Disabled until the SkillTypeComboBox has a value picked
                Tag = this,
            };

            skillTableLayoutPanel.Controls.Add(SkillLevelControl, 1, RowCount);

            SkillCostTextBox = new TextBox
            {
                Anchor = AnchorStyles.Top,
                //Name = "SkillCostTextBox#" + skillTableLayoutPanel.RowCount.ToString(),
                Size = new Size(50, 20),
                ReadOnly = true,
                Tag = this,
            };

            skillTableLayoutPanel.Controls.Add(SkillCostTextBox, 2, RowCount);

            //(SkillCostTextBox.FindForm() as WeaponsForm).RankTextBox.RegisterRankCostTextBox(SkillCostTextBox);


            //TODO; Remove this, as skill level combo box is now a placeholder
            //SkillLevelControl.SelectedValueChanged += SkillSkillLevel_SelectedValueChanged;

        }

        /// <summary>
        /// Get the available skill types (e.g. 1H weapons, 2H weapons, or leather armour, plate armour, etc.) 
        /// </summary>
        /// <param name="skillTableLayoutPanel">The table that will be used to find the parent WeaponsForm.</param>
        /// <returns></returns>
        //internal abstract List<SkillType> GetSkillTypesList(TableLayoutPanel skillTableLayoutPanel);
        //TODO: SpellRowControls need this to be virtual. Might hae to split those out more sensibly?
        internal virtual List<SkillType> GetSkillTypesList(TableLayoutPanel skillTableLayoutPanel)
        {
            if (SkillCategoryIdentifier == null)
            {
                throw new ArgumentNullException("Skill category identifier not set!");
            }
            return (skillTableLayoutPanel.FindForm() as WeaponsForm).JsonSkillReader.GetSkillTypeList(SkillCategoryIdentifier);
        }

        internal virtual SkillType GetSkillType(string skillType)
        {
            if (SkillCategoryIdentifier == null)
            {
                throw new ArgumentNullException("Skill category identifier not set!");
            }
            return (SkillLevelControl.FindForm() as WeaponsForm).JsonSkillReader.GetSkillType(SkillCategoryIdentifier, skillType);
        }

        private void SkillType_SelectedValueChanged(object sender, EventArgs e)
        {
            //TODO: Extra check for whether the name matches the pattern we expect?
            if (sender is ComboBox)
            {
                ComboBox SkillTypeComboBox = sender as ComboBox;
                //var weaponRowControls = SkillTypeComboBox.Tag as SkillRowControls;

                //TODO: What's this doing? Why do I disable the box?
                if (String.IsNullOrEmpty(SkillTypeComboBox.SelectedItem as string))
                {
                    SkillLevelControl.Enabled = false;
                }
                else
                {
                    var currType = GetSkillType((string)SkillTypeComboBox.SelectedItem);

                    // Remove the placeholder SkillLevelComboBox
                    TableLayoutPanel parent = SkillLevelControl.Parent as TableLayoutPanel;
                    parent.SuspendLayout();
                    parent.Controls.Remove(SkillLevelControl);

                    SkillLevelControl = SkillLevelControlFactory.CreateNewSkillLevelControl(currType);
                    (SkillLevelControl as ISkillLevelControl).ValueChanged += SkillSkillLevel_SelectedValueChanged;

                    parent.Controls.Add(SkillLevelControl, 1, parent.RowCount);
                    parent.ResumeLayout(true);

                }
            }
        }

        private void SkillSkillLevel_SelectedValueChanged(object sender, EventArgs e)
        {
            long skillCost = (SkillLevelControl as ISkillLevelControl).GetSkillLevelCost();
            SkillCostTextBox.Text = skillCost.ToString();

            var record = CreateRecord(skillCost);

            var skillsDict = (SkillCostTextBox.FindForm() as WeaponsForm).SkillsDict;

            var recordName = SkillCategoryIdentifier + RowCount;

            skillsDict.Add(recordName, record);
        }

        protected abstract SkillRecord CreateRecord(long skillCost);

    }
}