namespace WeaponsSelector
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.clawLabel = new System.Windows.Forms.Label();
            this.clawSkillLevel = new System.Windows.Forms.ComboBox();
            this.clawCost = new System.Windows.Forms.TextBox();
            this.skillLevelSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skillLevelSourceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.clawLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.clawSkillLevel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.clawCost, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // clawLabel
            // 
            this.clawLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.clawLabel.AutoSize = true;
            this.clawLabel.Location = new System.Drawing.Point(116, 106);
            this.clawLabel.Name = "clawLabel";
            this.clawLabel.Size = new System.Drawing.Size(33, 13);
            this.clawLabel.TabIndex = 0;
            this.clawLabel.Text = "Claw:";
            // 
            // clawSkillLevel
            // 
            this.clawSkillLevel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.clawSkillLevel.Enabled = false;
            this.clawSkillLevel.FormattingEnabled = true;
            this.clawSkillLevel.Location = new System.Drawing.Point(338, 102);
            this.clawSkillLevel.Name = "clawSkillLevel";
            this.clawSkillLevel.Size = new System.Drawing.Size(121, 21);
            this.clawSkillLevel.TabIndex = 1;
            this.clawSkillLevel.SelectedValueChanged += new System.EventHandler(this.clawSkillLevel_SelectedValueChanged);
            // 
            // clawCost
            // 
            this.clawCost.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.clawCost.Location = new System.Drawing.Point(616, 102);
            this.clawCost.Name = "clawCost";
            this.clawCost.ReadOnly = true;
            this.clawCost.Size = new System.Drawing.Size(100, 20);
            this.clawCost.TabIndex = 2;
            // 
            // skillLevelSourceBindingSource
            // 
            this.skillLevelSourceBindingSource.DataSource = typeof(WeaponsSelector.SkillLevelSource);
            this.skillLevelSourceBindingSource.Position = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skillLevelSourceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label clawLabel;
        private System.Windows.Forms.ComboBox clawSkillLevel;
        private System.Windows.Forms.TextBox clawCost;
        private System.Windows.Forms.BindingSource skillLevelSourceBindingSource;
    }
}

