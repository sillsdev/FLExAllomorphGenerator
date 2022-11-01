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
            this.tlpOperation = new System.Windows.Forms.TableLayoutPanel();
            this.lbName = new System.Windows.Forms.Label();
            this.lbDescription = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lbPattern = new System.Windows.Forms.Label();
            this.tlpPattern = new System.Windows.Forms.TableLayoutPanel();
            this.lbMatch = new System.Windows.Forms.Label();
            this.tbMatch = new System.Windows.Forms.TextBox();
            this.lbOperations = new System.Windows.Forms.ListBox();
            this.tabRunOps = new System.Windows.Forms.TabPage();
            this.lbAction = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbReplace = new System.Windows.Forms.Label();
            this.tbFrom = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lbAlloGenFile = new System.Windows.Forms.Label();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabEditOps.SuspendLayout();
            this.tlpOperation.SuspendLayout();
            this.tlpPattern.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.tabEditOps.Controls.Add(this.tlpOperation);
            this.tabEditOps.Controls.Add(this.lbOperations);
            this.tabEditOps.Location = new System.Drawing.Point(4, 29);
            this.tabEditOps.Name = "tabEditOps";
            this.tabEditOps.Padding = new System.Windows.Forms.Padding(3);
            this.tabEditOps.Size = new System.Drawing.Size(1281, 808);
            this.tabEditOps.TabIndex = 0;
            this.tabEditOps.Text = "Edit Operations";
            this.tabEditOps.UseVisualStyleBackColor = true;
            // 
            // tlpOperation
            // 
            this.tlpOperation.AutoScroll = true;
            this.tlpOperation.AutoSize = true;
            this.tlpOperation.ColumnCount = 2;
            this.tlpOperation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpOperation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpOperation.Controls.Add(this.lbName, 0, 0);
            this.tlpOperation.Controls.Add(this.lbDescription, 0, 1);
            this.tlpOperation.Controls.Add(this.tbName, 1, 0);
            this.tlpOperation.Controls.Add(this.textBox2, 1, 1);
            this.tlpOperation.Controls.Add(this.lbPattern, 0, 2);
            this.tlpOperation.Controls.Add(this.tlpPattern, 1, 2);
            this.tlpOperation.Controls.Add(this.lbAction, 0, 3);
            this.tlpOperation.Controls.Add(this.tableLayoutPanel1, 1, 3);
            this.tlpOperation.Location = new System.Drawing.Point(486, 0);
            this.tlpOperation.Name = "tlpOperation";
            this.tlpOperation.RowCount = 4;
            this.tlpOperation.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperation.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperation.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOperation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpOperation.Size = new System.Drawing.Size(793, 908);
            this.tlpOperation.TabIndex = 1;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbName.Location = new System.Drawing.Point(3, 0);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(89, 32);
            this.lbName.TabIndex = 0;
            this.lbName.Text = "Name";
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDescription.Location = new System.Drawing.Point(3, 32);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(89, 32);
            this.lbDescription.TabIndex = 1;
            this.lbDescription.Text = "Description";
            // 
            // tbName
            // 
            this.tbName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbName.Location = new System.Drawing.Point(98, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(692, 26);
            this.tbName.TabIndex = 2;
            this.tbName.Text = "My name";
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(98, 35);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(692, 26);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "My description";
            // 
            // lbPattern
            // 
            this.lbPattern.AutoSize = true;
            this.lbPattern.Location = new System.Drawing.Point(3, 64);
            this.lbPattern.Name = "lbPattern";
            this.lbPattern.Size = new System.Drawing.Size(61, 20);
            this.lbPattern.TabIndex = 4;
            this.lbPattern.Text = "Pattern";
            // 
            // tlpPattern
            // 
            this.tlpPattern.AutoSize = true;
            this.tlpPattern.ColumnCount = 2;
            this.tlpPattern.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPattern.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPattern.Controls.Add(this.lbMatch, 0, 0);
            this.tlpPattern.Controls.Add(this.tbMatch, 1, 0);
            this.tlpPattern.Location = new System.Drawing.Point(98, 67);
            this.tlpPattern.Name = "tlpPattern";
            this.tlpPattern.RowCount = 2;
            this.tlpPattern.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPattern.Size = new System.Drawing.Size(692, 52);
            this.tlpPattern.TabIndex = 5;
            // 
            // lbMatch
            // 
            this.lbMatch.AutoSize = true;
            this.lbMatch.Location = new System.Drawing.Point(3, 0);
            this.lbMatch.Name = "lbMatch";
            this.lbMatch.Size = new System.Drawing.Size(53, 20);
            this.lbMatch.TabIndex = 0;
            this.lbMatch.Text = "Match";
            // 
            // tbMatch
            // 
            this.tbMatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMatch.Location = new System.Drawing.Point(62, 3);
            this.tbMatch.Name = "tbMatch";
            this.tbMatch.Size = new System.Drawing.Size(627, 26);
            this.tbMatch.TabIndex = 1;
            this.tbMatch.Text = "My match";
            // 
            // lbOperations
            // 
            this.lbOperations.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbOperations.FormattingEnabled = true;
            this.lbOperations.ItemHeight = 20;
            this.lbOperations.Location = new System.Drawing.Point(3, 3);
            this.lbOperations.Name = "lbOperations";
            this.lbOperations.Size = new System.Drawing.Size(476, 802);
            this.lbOperations.TabIndex = 0;
            // 
            // tabRunOps
            // 
            this.tabRunOps.Location = new System.Drawing.Point(4, 29);
            this.tabRunOps.Name = "tabRunOps";
            this.tabRunOps.Padding = new System.Windows.Forms.Padding(3);
            this.tabRunOps.Size = new System.Drawing.Size(1281, 913);
            this.tabRunOps.TabIndex = 1;
            this.tabRunOps.Text = "Run Operations";
            this.tabRunOps.UseVisualStyleBackColor = true;
            // 
            // lbAction
            // 
            this.lbAction.AutoSize = true;
            this.lbAction.Location = new System.Drawing.Point(3, 122);
            this.lbAction.Name = "lbAction";
            this.lbAction.Size = new System.Drawing.Size(54, 20);
            this.lbAction.TabIndex = 6;
            this.lbAction.Text = "Action";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 228F));
            this.tableLayoutPanel1.Controls.Add(this.lbReplace, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbFrom, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(98, 125);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(689, 103);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // lbReplace
            // 
            this.lbReplace.AutoSize = true;
            this.lbReplace.Location = new System.Drawing.Point(3, 0);
            this.lbReplace.Name = "lbReplace";
            this.lbReplace.Size = new System.Drawing.Size(68, 20);
            this.lbReplace.TabIndex = 0;
            this.lbReplace.Text = "Replace";
            // 
            // tbFrom
            // 
            this.tbFrom.Location = new System.Drawing.Point(233, 3);
            this.tbFrom.Name = "tbFrom";
            this.tbFrom.Size = new System.Drawing.Size(185, 26);
            this.tbFrom.TabIndex = 1;
            this.tbFrom.Text = "My from";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(463, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(151, 26);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "My to";
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
            this.tabControl.ResumeLayout(false);
            this.tabEditOps.ResumeLayout(false);
            this.tabEditOps.PerformLayout();
            this.tlpOperation.ResumeLayout(false);
            this.tlpOperation.PerformLayout();
            this.tlpPattern.ResumeLayout(false);
            this.tlpPattern.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabEditOps;
        private System.Windows.Forms.TabPage tabRunOps;
        private System.Windows.Forms.TableLayoutPanel tlpOperation;
        private System.Windows.Forms.ListBox lbOperations;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lbPattern;
        private System.Windows.Forms.TableLayoutPanel tlpPattern;
        private System.Windows.Forms.Label lbMatch;
        private System.Windows.Forms.TextBox tbMatch;
        private System.Windows.Forms.Label lbAction;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbReplace;
        private System.Windows.Forms.TextBox tbFrom;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbAlloGenFile;
        private System.Windows.Forms.TextBox tbFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnHelp;
    }
}

