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
            this.btnCategories = new System.Windows.Forms.Button();
            this.btnMorphTypes = new System.Windows.Forms.Button();
            this.lbReplaceOps = new System.Windows.Forms.Label();
            this.btnStemName = new System.Windows.Forms.Button();
            this.tbStemName = new System.Windows.Forms.TextBox();
            this.lbStemName = new System.Windows.Forms.Label();
            this.btnInflectionFeatures = new System.Windows.Forms.Button();
            this.tbInflectionFeatures = new System.Windows.Forms.TextBox();
            this.lbInflectionFeatures = new System.Windows.Forms.Label();
            this.btnEnvironments = new System.Windows.Forms.Button();
            this.tbEnvironments = new System.Windows.Forms.TextBox();
            this.lbEnvironments = new System.Windows.Forms.Label();
            this.lBoxReplaceOps = new System.Windows.Forms.ListBox();
            this.cbRegEx = new System.Windows.Forms.CheckBox();
            this.lBoxCategories = new System.Windows.Forms.ListBox();
            this.lBoxMorphTypes = new System.Windows.Forms.ListBox();
            this.lbCategories = new System.Windows.Forms.Label();
            this.lbMorphTypes = new System.Windows.Forms.Label();
            this.tbMatch = new System.Windows.Forms.TextBox();
            this.lbMatch = new System.Windows.Forms.Label();
            this.lbPattern = new System.Windows.Forms.Label();
            this.lBoxOperations = new System.Windows.Forms.ListBox();
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
            this.lbRightClickToEdit = new System.Windows.Forms.Label();
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
            this.tabEditOps.Controls.Add(this.lbRightClickToEdit);
            this.tabEditOps.Controls.Add(this.btnCategories);
            this.tabEditOps.Controls.Add(this.btnMorphTypes);
            this.tabEditOps.Controls.Add(this.lbReplaceOps);
            this.tabEditOps.Controls.Add(this.btnStemName);
            this.tabEditOps.Controls.Add(this.tbStemName);
            this.tabEditOps.Controls.Add(this.lbStemName);
            this.tabEditOps.Controls.Add(this.btnInflectionFeatures);
            this.tabEditOps.Controls.Add(this.tbInflectionFeatures);
            this.tabEditOps.Controls.Add(this.lbInflectionFeatures);
            this.tabEditOps.Controls.Add(this.btnEnvironments);
            this.tabEditOps.Controls.Add(this.tbEnvironments);
            this.tabEditOps.Controls.Add(this.lbEnvironments);
            this.tabEditOps.Controls.Add(this.lBoxReplaceOps);
            this.tabEditOps.Controls.Add(this.cbRegEx);
            this.tabEditOps.Controls.Add(this.lBoxCategories);
            this.tabEditOps.Controls.Add(this.lBoxMorphTypes);
            this.tabEditOps.Controls.Add(this.lbCategories);
            this.tabEditOps.Controls.Add(this.lbMorphTypes);
            this.tabEditOps.Controls.Add(this.tbMatch);
            this.tabEditOps.Controls.Add(this.lbMatch);
            this.tabEditOps.Controls.Add(this.lbPattern);
            this.tabEditOps.Controls.Add(this.lBoxOperations);
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
            // btnCategories
            // 
            this.btnCategories.Location = new System.Drawing.Point(917, 131);
            this.btnCategories.Name = "btnCategories";
            this.btnCategories.Size = new System.Drawing.Size(30, 28);
            this.btnCategories.TabIndex = 26;
            this.btnCategories.Text = "...";
            this.btnCategories.UseVisualStyleBackColor = true;
            // 
            // btnMorphTypes
            // 
            this.btnMorphTypes.Location = new System.Drawing.Point(605, 131);
            this.btnMorphTypes.Name = "btnMorphTypes";
            this.btnMorphTypes.Size = new System.Drawing.Size(30, 28);
            this.btnMorphTypes.TabIndex = 25;
            this.btnMorphTypes.Text = "...";
            this.btnMorphTypes.UseVisualStyleBackColor = true;
            // 
            // lbReplaceOps
            // 
            this.lbReplaceOps.AutoSize = true;
            this.lbReplaceOps.Location = new System.Drawing.Point(497, 345);
            this.lbReplaceOps.Name = "lbReplaceOps";
            this.lbReplaceOps.Size = new System.Drawing.Size(147, 20);
            this.lbReplaceOps.TabIndex = 24;
            this.lbReplaceOps.Text = "Replace operations";
            // 
            // btnStemName
            // 
            this.btnStemName.Location = new System.Drawing.Point(1092, 720);
            this.btnStemName.Name = "btnStemName";
            this.btnStemName.Size = new System.Drawing.Size(30, 28);
            this.btnStemName.TabIndex = 23;
            this.btnStemName.Text = "...";
            this.btnStemName.UseVisualStyleBackColor = true;
            // 
            // tbStemName
            // 
            this.tbStemName.Location = new System.Drawing.Point(660, 720);
            this.tbStemName.Name = "tbStemName";
            this.tbStemName.ReadOnly = true;
            this.tbStemName.Size = new System.Drawing.Size(425, 26);
            this.tbStemName.TabIndex = 22;
            this.tbStemName.Text = "undergoes lowering";
            // 
            // lbStemName
            // 
            this.lbStemName.AutoSize = true;
            this.lbStemName.Location = new System.Drawing.Point(493, 720);
            this.lbStemName.Name = "lbStemName";
            this.lbStemName.Size = new System.Drawing.Size(91, 20);
            this.lbStemName.TabIndex = 21;
            this.lbStemName.Text = "Stem name";
            // 
            // btnInflectionFeatures
            // 
            this.btnInflectionFeatures.Location = new System.Drawing.Point(1092, 662);
            this.btnInflectionFeatures.Name = "btnInflectionFeatures";
            this.btnInflectionFeatures.Size = new System.Drawing.Size(30, 28);
            this.btnInflectionFeatures.TabIndex = 20;
            this.btnInflectionFeatures.Text = "...";
            this.btnInflectionFeatures.UseVisualStyleBackColor = true;
            // 
            // tbInflectionFeatures
            // 
            this.tbInflectionFeatures.Location = new System.Drawing.Point(660, 662);
            this.tbInflectionFeatures.Name = "tbInflectionFeatures";
            this.tbInflectionFeatures.ReadOnly = true;
            this.tbInflectionFeatures.Size = new System.Drawing.Size(425, 26);
            this.tbInflectionFeatures.TabIndex = 19;
            this.tbInflectionFeatures.Text = "[tense:past]";
            // 
            // lbInflectionFeatures
            // 
            this.lbInflectionFeatures.AutoSize = true;
            this.lbInflectionFeatures.Location = new System.Drawing.Point(493, 662);
            this.lbInflectionFeatures.Name = "lbInflectionFeatures";
            this.lbInflectionFeatures.Size = new System.Drawing.Size(142, 20);
            this.lbInflectionFeatures.TabIndex = 18;
            this.lbInflectionFeatures.Text = "Inflection Features";
            // 
            // btnEnvironments
            // 
            this.btnEnvironments.Location = new System.Drawing.Point(1091, 591);
            this.btnEnvironments.Name = "btnEnvironments";
            this.btnEnvironments.Size = new System.Drawing.Size(30, 28);
            this.btnEnvironments.TabIndex = 17;
            this.btnEnvironments.Text = "...";
            this.btnEnvironments.UseVisualStyleBackColor = true;
            // 
            // tbEnvironments
            // 
            this.tbEnvironments.Location = new System.Drawing.Point(660, 593);
            this.tbEnvironments.Multiline = true;
            this.tbEnvironments.Name = "tbEnvironments";
            this.tbEnvironments.ReadOnly = true;
            this.tbEnvironments.Size = new System.Drawing.Size(425, 26);
            this.tbEnvironments.TabIndex = 16;
            this.tbEnvironments.Text = "/ _ #";
            // 
            // lbEnvironments
            // 
            this.lbEnvironments.AutoSize = true;
            this.lbEnvironments.Location = new System.Drawing.Point(497, 593);
            this.lbEnvironments.Name = "lbEnvironments";
            this.lbEnvironments.Size = new System.Drawing.Size(106, 20);
            this.lbEnvironments.TabIndex = 15;
            this.lbEnvironments.Text = "Environments";
            // 
            // lBoxReplaceOps
            // 
            this.lBoxReplaceOps.FormattingEnabled = true;
            this.lBoxReplaceOps.ItemHeight = 20;
            this.lBoxReplaceOps.Location = new System.Drawing.Point(497, 371);
            this.lBoxReplaceOps.Name = "lBoxReplaceOps";
            this.lBoxReplaceOps.Size = new System.Drawing.Size(625, 184);
            this.lBoxReplaceOps.TabIndex = 14;
            this.lBoxReplaceOps.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lBoxReplaceOps_MouseUp);
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
            // lBoxCategories
            // 
            this.lBoxCategories.FormattingEnabled = true;
            this.lBoxCategories.ItemHeight = 20;
            this.lBoxCategories.Location = new System.Drawing.Point(813, 175);
            this.lBoxCategories.Name = "lBoxCategories";
            this.lBoxCategories.Size = new System.Drawing.Size(309, 124);
            this.lBoxCategories.TabIndex = 12;
            // 
            // lBoxMorphTypes
            // 
            this.lBoxMorphTypes.FormattingEnabled = true;
            this.lBoxMorphTypes.ItemHeight = 20;
            this.lBoxMorphTypes.Location = new System.Drawing.Point(497, 175);
            this.lBoxMorphTypes.Name = "lBoxMorphTypes";
            this.lBoxMorphTypes.Size = new System.Drawing.Size(264, 124);
            this.lBoxMorphTypes.TabIndex = 11;
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
            // lbMorphTypes
            // 
            this.lbMorphTypes.AutoSize = true;
            this.lbMorphTypes.Location = new System.Drawing.Point(497, 139);
            this.lbMorphTypes.Name = "lbMorphTypes";
            this.lbMorphTypes.Size = new System.Drawing.Size(100, 20);
            this.lbMorphTypes.TabIndex = 9;
            this.lbMorphTypes.Text = "Morph Types";
            // 
            // tbMatch
            // 
            this.tbMatch.Location = new System.Drawing.Point(592, 85);
            this.tbMatch.Name = "tbMatch";
            this.tbMatch.Size = new System.Drawing.Size(204, 26);
            this.tbMatch.TabIndex = 8;
            this.tbMatch.Text = "My match";
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
            // lbPattern
            // 
            this.lbPattern.AutoSize = true;
            this.lbPattern.Location = new System.Drawing.Point(364, 85);
            this.lbPattern.Name = "lbPattern";
            this.lbPattern.Size = new System.Drawing.Size(61, 20);
            this.lbPattern.TabIndex = 4;
            this.lbPattern.Text = "Pattern";
            // 
            // lBoxOperations
            // 
            this.lBoxOperations.Dock = System.Windows.Forms.DockStyle.Left;
            this.lBoxOperations.FormattingEnabled = true;
            this.lBoxOperations.ItemHeight = 20;
            this.lBoxOperations.Location = new System.Drawing.Point(3, 3);
            this.lBoxOperations.Name = "lBoxOperations";
            this.lBoxOperations.Size = new System.Drawing.Size(355, 802);
            this.lBoxOperations.TabIndex = 0;
            this.lBoxOperations.SelectedIndexChanged += new System.EventHandler(this.lbOperations_SelectedIndexChanged);
            this.lBoxOperations.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lBoxOperations_MouseUp);
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
            this.lbAction.Location = new System.Drawing.Point(364, 346);
            this.lbAction.Name = "lbAction";
            this.lbAction.Size = new System.Drawing.Size(62, 20);
            this.lbAction.TabIndex = 6;
            this.lbAction.Text = "Actions";
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
            // lbRightClickToEdit
            // 
            this.lbRightClickToEdit.AutoSize = true;
            this.lbRightClickToEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRightClickToEdit.Location = new System.Drawing.Point(677, 344);
            this.lbRightClickToEdit.Name = "lbRightClickToEdit";
            this.lbRightClickToEdit.Size = new System.Drawing.Size(140, 20);
            this.lbRightClickToEdit.TabIndex = 27;
            this.lbRightClickToEdit.Text = "(Right-click to edit)";
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
        private System.Windows.Forms.ListBox lBoxOperations;
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
        private System.Windows.Forms.ListBox lBoxReplaceOps;
        private System.Windows.Forms.Button btnEnvironments;
        private System.Windows.Forms.TextBox tbEnvironments;
        private System.Windows.Forms.Label lbEnvironments;
        private System.Windows.Forms.Button btnStemName;
        private System.Windows.Forms.TextBox tbStemName;
        private System.Windows.Forms.Label lbStemName;
        private System.Windows.Forms.Button btnInflectionFeatures;
        private System.Windows.Forms.TextBox tbInflectionFeatures;
        private System.Windows.Forms.Label lbInflectionFeatures;
        private System.Windows.Forms.Label lbReplaceOps;
        private System.Windows.Forms.Button btnCategories;
        private System.Windows.Forms.Button btnMorphTypes;
        private System.Windows.Forms.Label lbRightClickToEdit;
    }
}

