﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponsForm;

namespace WeaponsSelector.src.gui
{
    public abstract class SkillGroupBoxWrapper
    {
        protected GroupBox groupBox;
        protected List<SkillRowControls> skillRowControls;
        protected TableLayoutPanel tableLayoutPanel;

        protected SkillGroupBoxWrapper(FlowLayoutPanel flowLayoutPanel)
        {
            groupBox = new GroupBox
            {
                Dock = DockStyle.Left,
                Anchor = AnchorStyles.Left,
                Text = getGroupBoxText(),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };

            flowLayoutPanel.Controls.Add(groupBox);
            //TODO: Come back to whether this suspend is needed
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
            //innerFlowLayoutPanel.SuspendLayout();
            groupBox.Controls.Add(innerFlowLayoutPanel);

            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                //Name = "UniqueStrForNow", //TODO: This used to be a string defined in WeaponsForm, and accessed in handlers...
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };
            innerFlowLayoutPanel.Controls.Add(tableLayoutPanel);


            skillRowControls = new List<SkillRowControls>
            {
                getNewSkillRowControl(tableLayoutPanel)
            };

            var addSkillButton = new Button
            {
                Text = "Add " + getGroupBoxText(),
                AutoSize = true,
            };
            addSkillButton.Click += AddNewSkillRowEventHander;

            innerFlowLayoutPanel.Controls.Add(addSkillButton);

            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();
        }

        internal abstract SkillRowControls getNewSkillRowControl(TableLayoutPanel tableLayoutPanel);
        protected void AddNewSkillRowEventHander(object sender, System.EventArgs e)
        {
            groupBox.SuspendLayout();
            skillRowControls.Add(getNewSkillRowControl(tableLayoutPanel));
            groupBox.ResumeLayout(true);
        }
        protected abstract string getGroupBoxText();
    }


}
