using System;

namespace SIL.AllomorphGenerator
{
    partial class AlloGenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlloGenForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabEditOps = new System.Windows.Forms.TabPage();
            this.lbPattern = new System.Windows.Forms.Label();
            this.lbOperations = new System.Windows.Forms.ListBox();
            this.lbName = new System.Windows.Forms.Label();
            this.lbDescription = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.lbAction = new System.Windows.Forms.Label();
            this.tabRunOps = new System.Windows.Forms.TabPage();
            this.lbAlloGenFile = new System.Windows.Forms.Label();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.lbMatch = new System.Windows.Forms.Label();
            this.tbMatch = new System.Windows.Forms.TextBox();
            this.lbMorphTypes = new System.Windows.Forms.Label();
            this.lbCategories = new System.Windows.Forms.Label();
            this.lBoxMorphTypes = new System.Windows.Forms.ListBox();
            this.lBoxCategories = new System.Windows.Forms.ListBox();
            this.cbRegEx = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.tabEditOps.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabEditOps);
            this.tabControl.Controls.Add(this.tabRunOps);
            this.tabControl.Location = new System.Drawing.Point(0, 105);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1289, 841);
            this.tabControl.TabIndex = 0;
            // 
            // tabEditOps
            // 
            this.tabEditOps.Controls.Add(this.cbRegEx);
            this.tabEditOps.Controls.Add(this.lBoxCategories);
            this.tabEditOps.Controls.Add(this.lBoxMorphTypes);
            this.tabEditOps.Controls.Add(this.lbCategories);
            this.tabEditOps.Controls.Add(this.lbMorphTypes);
            this.tabEditOps.Controls.Add(this.tbMatch);
            this.tabEditOps.Controls.Add(this.lbMatch);
            this.tabEditOps.Controls.Add(this.lbPattern);
            this.tabEditOps.Controls.Add(this.lbOperations);
            this.tabEditOps.Controls.Add(this.lbName);
            this.tabEditOps.Controls.Add(this.lbDescription);
            this.tabEditOps.Controls.Add(this.tbName);
            this.tabEditOps.Controls.Add(this.tbDescription);
            this.tabEditOps.Controls.Add(this.lbAction);
            this.tabEditOps.Location = new System.Drawing.Point(4, 29);
            this.tabEditOps.Name = "tabEditOps";
            this.tabEditOps.Padding = new System.Windows.Forms.Padding(3);
            this.tabEditOps.Size = new System.Drawing.Size(1281, 808);
            this.tabEditOps.TabIndex = 0;
            this.tabEditOps.Text = "Edit Operations";
            this.tabEditOps.UseVisualStyleBackColor = true;
            // 
            // lbPattern
            // 
            this.lbPattern.AutoSize = true;
            this.lbPattern.Location = new System.Drawing.Point(364, 85);
            this.lbPattern.Name = "lbPattern";
            this.lbPattern.Size = new System.Drawing.Size(61, 20);
            this.lbPattern.TabIndex = 4;
            this.lbPattern.Text = "Pattern";
            // 
            // lbOperations
            // 
            this.lbOperations.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbOperations.FormattingEnabled = true;
            this.lbOperations.ItemHeight = 20;
            this.lbOperations.Location = new System.Drawing.Point(3, 3);
            this.lbOperations.Name = "lbOperations";
            this.lbOperations.Size = new System.Drawing.Size(355, 802);
            this.lbOperations.TabIndex = 0;
            this.lbOperations.SelectedIndexChanged += new System.EventHandler(this.lbOperations_SelectedIndexChanged);
            // 
            // lbName
            // 
            this.lbName.Location = new System.Drawing.Point(364, 6);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(89, 26);
            this.lbName.TabIndex = 0;
            this.lbName.Text = "Name";
            // 
            // lbDescription
            // 
            this.lbDescription.Location = new System.Drawing.Point(364, 43);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(89, 20);
            this.lbDescription.TabIndex = 1;
            this.lbDescription.Text = "Description";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(497, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(625, 26);
            this.tbName.TabIndex = 2;
            this.tbName.Text = "My name";
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(497, 37);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(625, 26);
            this.tbDescription.TabIndex = 3;
            this.tbDescription.Text = "My description";
            // 
            // lbAction
            // 
            this.lbAction.AutoSize = true;
            this.lbAction.Location = new System.Drawing.Point(364, 382);
            this.lbAction.Name = "lbAction";
            this.lbAction.Size = new System.Drawing.Size(54, 20);
            this.lbAction.TabIndex = 6;
            this.lbAction.Text = "Action";
            // 
            // tabRunOps
            // 
            this.tabRunOps.Location = new System.Drawing.Point(4, 29);
            this.tabRunOps.Name = "tabRunOps";
            this.tabRunOps.Padding = new System.Windows.Forms.Padding(3);
            this.tabRunOps.Size = new System.Drawing.Size(1281, 808);
            this.tabRunOps.TabIndex = 1;
            this.tabRunOps.Text = "Run Operations";
            this.tabRunOps.UseVisualStyleBackColor = true;
            // 
            // lbAlloGenFile
            // 
            this.lbAlloGenFile.AutoSize = true;
            this.lbAlloGenFile.Location = new System.Drawing.Point(23, 13);
            this.lbAlloGenFile.Name = "lbAlloGenFile";
            this.lbAlloGenFile.Size = new System.Drawing.Size(38, 20);
            this.lbAlloGenFile.TabIndex = 1;
            this.lbAlloGenFile.Text = "File:";
            // 
            // tbFile
            // 
            this.tbFile.Location = new System.Drawing.Point(101, 13);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(995, 26);
            this.tbFile.TabIndex = 2;
            this.tbFile.Text = "XML file location";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(1135, 15);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(85, 38);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(1135, 61);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(85, 38);
            this.btnHelp.TabIndex = 4;
            this.btnHelp.Text = "&Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // lbMatch
            // 
            this.lbMatch.AutoSize = true;
            this.lbMatch.Location = new System.Drawing.Point(497, 85);
            this.lbMatch.Name = "lbMatch";
            this.lbMatch.Size = new System.Drawing.Size(53, 20);
            this.lbMatch.TabIndex = 7;
            this.lbMatch.Text = "Match";
            // 
            // tbMatch
            // 
            this.tbMatch.Location = new System.Drawing.Point(592, 85);
            this.tbMatch.Name = "tbMatch";
            this.tbMatch.Size = new System.Drawing.Size(204, 26);
            this.tbMatch.TabIndex = 8;
            this.tbMatch.Text = "My match";
            // 
            // lbMorphTypes
            // 
            this.lbMorphTypes.AutoSize = true;
            this.lbMorphTypes.Location = new System.Drawing.Point(497, 139);
            this.lbMorphTypes.Name = "lbMorphTypes";
            this.lbMorphTypes.Size = new System.Drawing.Size(100, 20);
            this.lbMorphTypes.TabIndex = 9;
            this.lbMorphTypes.Text = "Morph Types";
            // 
            // lbCategories
            // 
            this.lbCategories.AutoSize = true;
            this.lbCategories.Location = new System.Drawing.Point(809, 139);
            this.lbCategories.Name = "lbCategories";
            this.lbCategories.Size = new System.Drawing.Size(86, 20);
            this.lbCategories.TabIndex = 10;
            this.lbCategories.Text = "Categories";
            // 
            // lBoxMorphTypes
            // 
            this.lBoxMorphTypes.FormattingEnabled = true;
            this.lBoxMorphTypes.ItemHeight = 20;
            this.lBoxMorphTypes.Location = new System.Drawing.Point(497, 175);
            this.lBoxMorphTypes.Name = "lBoxMorphTypes";
            this.lBoxMorphTypes.Size = new System.Drawing.Size(264, 204);
            this.lBoxMorphTypes.TabIndex = 11;
            // 
            // lBoxCategories
            // 
            this.lBoxCategories.FormattingEnabled = true;
            this.lBoxCategories.ItemHeight = 20;
            this.lBoxCategories.Location = new System.Drawing.Point(813, 175);
            this.lBoxCategories.Name = "lBoxCategories";
            this.lBoxCategories.Size = new System.Drawing.Size(309, 204);
            this.lBoxCategories.TabIndex = 12;
            // 
            // cbRegEx
            // 
            this.cbRegEx.AutoSize = true;
            this.cbRegEx.Location = new System.Drawing.Point(837, 86);
            this.cbRegEx.Name = "cbRegEx";
            this.cbRegEx.Size = new System.Drawing.Size(171, 24);
            this.cbRegEx.TabIndex = 13;
            this.cbRegEx.Text = "Regular expression";
            this.cbRegEx.UseVisualStyleBackColor = true;
            // 
            // AlloGenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 946);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbFile);
            this.Controls.Add(this.lbAlloGenFile);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AlloGenForm";
            this.Text = "Allomorph Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabEditOps.ResumeLayout(false);
            this.tabEditOps.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabEditOps;
        private System.Windows.Forms.TabPage tabRunOps;
        private System.Windows.Forms.ListBox lbOperations;
        private System.Windows.Forms.Label lbAlloGenFile;
        private System.Windows.Forms.TextBox tbFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Label lbPattern;
        private System.Windows.Forms.Label lbAction;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.CheckBox cbRegEx;
        private System.Windows.Forms.ListBox lBoxCategories;
        private System.Windows.Forms.ListBox lBoxMorphTypes;
        private System.Windows.Forms.Label lbCategories;
        private System.Windows.Forms.Label lbMorphTypes;
        private System.Windows.Forms.TextBox tbMatch;
        private System.Windows.Forms.Label lbMatch;
    }
}

