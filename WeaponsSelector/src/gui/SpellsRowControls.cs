﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WeaponsForm.Skills;
using System.Drawing;
using System.Linq;
using WeaponsForm.Spells;

namespace WeaponsForm
{
    internal class SpellsRowControls : AbstractRowControls
    {
        //TODO: review visibility modifiers
        public ComboBox SpellSchoolComboBox { get; }

        /// <summary>
        /// The combo box for whichever spell is currently selected
        /// </summary>
        public ComboBox SpellComboBox { get; set; }

        public ComboBox SpellLevelComboBox { get; set; }

        public TextBox SkillCostTextBox { get; }

        public SpellsRowControls(TableLayoutPanel skillTableLayoutPanel) : base(skillTableLayoutPanel)
        {
            skillTableLayoutPanel.RowCount += 1;

            SpellSchoolComboBox = new ComboBox
            {
                Anchor = AnchorStyles.Top,
                //Name = "SkillTypeComboBox#" + skillTableLayoutPanel.RowCount.ToString(),
                MaximumSize = new Size(100, 20),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Tag = this,
            };

            // Gather and list all possible skills in the combobox
            var spellSchoolsList = GetSpellSchools(skillTableLayoutPanel);
            string[] schoolsArray = spellSchoolsList.ToArray().Select(item => item.School).ToArray();
            SpellSchoolComboBox.Items.AddRange(schoolsArray);
            SpellSchoolComboBox.SelectedIndex = -1;

            SpellSchoolComboBox.SelectedValueChanged += SpellSchool_SelectedValueChanged;

            skillTableLayoutPanel.Controls.Add(SpellSchoolComboBox, 0, skillTableLayoutPanel.RowCount);

            SpellComboBox = new ComboBox
            {
                Anchor = AnchorStyles.Top,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Enabled = false, // Only enable once a school is selected
                Tag = this, //TODO: Check if I even need this
            };

            SpellComboBox.SelectedValueChanged += Spell_SelectedValueChanged;

            skillTableLayoutPanel.Controls.Add(SpellComboBox, 1, skillTableLayoutPanel.RowCount);




            SpellLevelComboBox = new ComboBox
            {
                Anchor = AnchorStyles.Top,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Enabled = false, // Only enable once a spell is selected
                Tag = this, //TODO: Check if I even need this
            };

            SpellLevelComboBox.SelectedValueChanged += SpellLevel_SelectedValueChanged;

            skillTableLayoutPanel.Controls.Add(SpellLevelComboBox, 2, skillTableLayoutPanel.RowCount);


            SkillCostTextBox = new TextBox
            {
                Anchor = AnchorStyles.Top,
                //Name = "SkillCostTextBox#" + skillTableLayoutPanel.RowCount.ToString(),
                Size = new Size(50, 20),
                ReadOnly = true,
                Tag = this,
            };

            skillTableLayoutPanel.Controls.Add(SkillCostTextBox, 3, skillTableLayoutPanel.RowCount);

            (SkillCostTextBox.FindForm() as WeaponsForm).RankTextBox.RegisterRankCostTextBox(SkillCostTextBox);
        }

        internal List<SpellSchool> GetSpellSchools(TableLayoutPanel skillTableLayoutPanel)
        {
            return (skillTableLayoutPanel.FindForm() as WeaponsForm).JsonSpellReader.GetSpellSchools();
        }

        /// <summary>
        /// Change handler responsible for setting the list of valid spells based on the selected School
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpellSchool_SelectedValueChanged(object sender, EventArgs e)
        {
            var spellsList = (SpellSchoolComboBox.FindForm() as WeaponsForm).JsonSpellReader.GetSpells((string)SpellSchoolComboBox.SelectedItem);

            string[] spellsArray = spellsList.ToArray().Select(item => item.Name).ToArray();
            SpellComboBox.Items.Clear();
            SpellComboBox.Items.AddRange(spellsArray);
            SpellComboBox.SelectedIndex = -1;

            SpellComboBox.Enabled = true;
        }


        /// <summary>
        /// Change handler responsible for setting the valid spell levels based on the selected Spell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Spell_SelectedValueChanged(object sender, EventArgs e)
        {
            var levelsList = (SpellComboBox.FindForm() as WeaponsForm).JsonSpellReader.GetSpellLevels((string)SpellSchoolComboBox.SelectedItem, (string)SpellComboBox.SelectedItem);

            string[] spellsArray = levelsList.ToArray().Select(item => item.Description).ToArray();
            SpellLevelComboBox.Items.AddRange(spellsArray);
            SpellLevelComboBox.SelectedIndex = -1;

            SpellLevelComboBox.Enabled = true;
        }


        private void SpellLevel_SelectedValueChanged(object sender, EventArgs e)
        {
            //TODO: Should constant the "Learn Spell" one, and also one for "Learn Miracle"
            var spellLevelCost = (SpellComboBox.FindForm() as WeaponsForm).JsonSkillReader.GetSkillType(Constants.Special, "Learn Spell");

            var levelsList = (SpellComboBox.FindForm() as WeaponsForm).JsonSpellReader.GetSpellLevels((string)SpellSchoolComboBox.SelectedItem, (string)SpellComboBox.SelectedItem);

            // Keep track of the cost up to and including the purchased level
            long runningCost = 0;
            foreach (Spells.Level l in levelsList)
            {
                //TODO: Revisit the ? operators in teh skill definitions...
                runningCost += spellLevelCost.Cost.GetValueOrDefault() * (l.LevelLevel.GetValueOrDefault() + 1);

                if (l.Description == (string)SpellLevelComboBox.SelectedItem)
                {
                    //Found the selected spell level, stop counting
                    break;
                }
            }

            SkillCostTextBox.Text = runningCost.ToString();

        }
    }
}