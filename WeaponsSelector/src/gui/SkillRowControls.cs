using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeaponsSelector;

namespace WeaponsForm
{
    public abstract class SkillRowControls
    {
        public ComboBox SkillTypeComboBox { get; }
        public ComboBox SkillLevelComboBox { get; }
        public TextBox SkillCostTextBox { get; }

        /// <summary>
        /// Create a new set of components defining a skill, and make them a new row at the bottom of
        /// the given TableLayoutPanel
        /// </summary>
        /// <param name="rankTextBox">The global rank text box that updates with all rank information</param>
        /// <param name="skillTableLayoutPanel">The table to which the new weapons controls will be added. Must already be added to the parent form.</param>
        public SkillRowControls(TableLayoutPanel skillTableLayoutPanel)
        {
            skillTableLayoutPanel.RowCount += 1;

            SkillTypeComboBox = new ComboBox
            {
                Anchor = AnchorStyles.Top,
                Name = "SkillTypeComboBox#" + skillTableLayoutPanel.RowCount.ToString(),
                MaximumSize = new Size(100, 20),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Tag = this,
            };

            // Gather and list all possible skills in the combobox
            var skillTypesList = GetSkillTypesList(skillTableLayoutPanel);
            string[] weaponsStringList = skillTypesList.ToArray().Select(weap => weap.Name).ToArray();
            SkillTypeComboBox.Items.AddRange(weaponsStringList);
            SkillTypeComboBox.SelectedIndex = -1;

            SkillTypeComboBox.SelectedValueChanged += SkillType_SelectedValueChanged;

            skillTableLayoutPanel.Controls.Add(SkillTypeComboBox, 0, skillTableLayoutPanel.RowCount);

            SkillLevelComboBox = new ComboBox
            {
                Anchor = AnchorStyles.Top,
                Name = "SkillLevelComboBox#" + skillTableLayoutPanel.RowCount.ToString(),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Enabled = false, // Disabled until the SkillTypeComboBox has a value picked
                Tag = this,
            };

            skillTableLayoutPanel.Controls.Add(SkillLevelComboBox, 1, skillTableLayoutPanel.RowCount);

            SkillCostTextBox = new TextBox
            {
                Anchor = AnchorStyles.Top,
                Name = "SkillCostTextBox#" + skillTableLayoutPanel.RowCount.ToString(),
                Size = new Size(50, 20),
                ReadOnly = true,
                Tag = this,
            };

            skillTableLayoutPanel.Controls.Add(SkillCostTextBox, 2, skillTableLayoutPanel.RowCount);

            (SkillCostTextBox.FindForm() as WeaponsForm).RankTextBox.RegisterRankCostTextBox(SkillCostTextBox);

            SkillLevelComboBox.SelectedValueChanged += SkillSkillLevel_SelectedValueChanged;

        }

        /// <summary>
        /// Get the available skill types (e.g. 1H weapons, 2H weapons, or leather armour, plate armour, etc.) 
        /// </summary>
        /// <param name="skillTableLayoutPanel">The table that will be used to find the parent WeaponsForm.</param>
        /// <returns></returns>
        internal abstract List<SkillType> GetSkillTypesList(TableLayoutPanel skillTableLayoutPanel);

        internal abstract SkillType GetSkillType(string weaponType);

        internal void PopulateValidSkills(SkillType type)
        {
            SkillLevelComboBox.Items.Clear();
            SkillLevelComboBox.Items.AddRange(type.GetValidSkillLevels());
            SkillLevelComboBox.SelectedIndex = 0;
        }


        private void SkillType_SelectedValueChanged(object sender, EventArgs e)
        {
            //TODO: Extra check for whether the name matches the pattern we expect?
            if (sender is ComboBox)
            {
                ComboBox SkillTypeComboBox = sender as ComboBox;
                var weaponRowControls = SkillTypeComboBox.Tag as SkillRowControls;

                if (String.IsNullOrEmpty(SkillTypeComboBox.SelectedItem as string))
                {
                    weaponRowControls.SkillLevelComboBox.Enabled = false;
                }
                else
                {
                    var currType = GetSkillType((string)SkillTypeComboBox.SelectedItem);

                    (SkillTypeComboBox.Tag as SkillRowControls).PopulateValidSkills(currType);

                    weaponRowControls.SkillLevelComboBox.Enabled = true;
                }
            }
        }

        private void SkillSkillLevel_SelectedValueChanged(object sender, EventArgs e)
        {
            //TODO: Extra check for whether the name matches the pattern we expect?
            if (sender is ComboBox)
            {
                ComboBox skillComboBox = sender as ComboBox;

                SkillRowControls skillRowControl = skillComboBox.Tag as SkillRowControls;
                var SkillTypeComboBox = skillRowControl.SkillTypeComboBox;

                var currType = GetSkillType((string)SkillTypeComboBox.SelectedItem);

                //WOrking on: Creating a skill record at this point

                TextBox weaponCostTextBox = skillRowControl.SkillCostTextBox;

                long totalCost = currType.GetSkillLevelCost((string)skillComboBox.SelectedItem);

                weaponCostTextBox.Text = totalCost.ToString();


            }
        }

    }
}