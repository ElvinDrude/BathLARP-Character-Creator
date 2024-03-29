﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponsForm.character.record;
using System.ComponentModel;

namespace WeaponsSelector.src.gui.components
{
    class CharacterSheetTextBox : TextBox
    {
        //TODO: Go over what part of the ObserveableConcurrentDictionary I actually need and use that - probably jus the ICollection interface
        public CharacterSheetTextBox(ObservableConcurrentDictionary<string, SkillRecord> skillsDict) : base()
        {
            skillsDict.CollectionChanged += CharacterSheetTextBox_SkillsListChanged;
        }


        private void CharacterSheetTextBox_SkillsListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Note that this can get called twice - once for removal of key and once for adding new one. 
            CreateText(sender);
        }

        private void CreateText(object sender)
        {
            this.Text = "";

            var skillsDict = (sender as ObservableConcurrentDictionary<string, SkillRecord>);

            CreateArmourText(skillsDict);

            CreateWeaponText(skillsDict);

            CreatePhysicalMentalText(skillsDict);
            return;
        }


        private void CreateArmourText(ObservableConcurrentDictionary<string, SkillRecord> skillsDict)
        {
            var list = skillsDict.Values.OfType<ArmourRecord>().ToList();

            int totalArmour = 0;

            if (list.Count > 1)
            {
                // Stacking two layers of armour gives a -1 penalty.
                // Note this isn't perfect logic due to how Combination works...
                totalArmour -= 1;
            }

            foreach (var record in list)
            {
                totalArmour += record.ArmourAmount;
            }


            this.Text += "Armour: " + totalArmour + "\r\n";
        }


        private void CreateWeaponText(ObservableConcurrentDictionary<string, SkillRecord> skillsDict)
        {
            StrengthLevel strLevel = StrengthLevel.None;


            var list = skillsDict.Values.OfType<StrengthRecord>().ToList();
            if (list.Count > 0)
            {
                strLevel = list.First().StrengthLevel;
            }

            var weaponsList = skillsDict.Values.OfType<WeaponRecord>().ToList();

            foreach (var weapon in weaponsList)
            {
                this.Text += weapon.WeaponType + " " + weapon.GetDamage(strLevel) + "\r\n";
            }

            //TODO: Test all the shield stuff, there's none in the Skills JSON at time of writing
            var shieldList = skillsDict.Values.OfType<ShieldRecord>().ToList();

            foreach (var shield in shieldList)
            {
                this.Text += shield.ShieldType + " breaks on " + shield.ShieldBreak + "\r\n";
            }
        }
        private void CreatePhysicalMentalText(ObservableConcurrentDictionary<string, SkillRecord> skillsDict)
        {
            var lifeRecordList = skillsDict.Values.OfType<EnhanceLifeRecord>().ToList();
            var lifeBought = 0;
            if (lifeRecordList.Count > 0)
            {
                lifeBought = lifeRecordList.First().LifeBought;
            }
            var form = (FindForm() as WeaponsForm.WeaponsForm);
            this.Text += "Life: " + (form.JsonRaceReader.GetRaces()[form.RaceComboBox.SelectedIndex].Life.Base + lifeBought);

        }
    }
}
