namespace SIL.VariantGenerator
{
	partial class VariantGenForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer varGenComponents = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (varGenComponents != null))
			{
				varGenComponents.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void VarGenInitializeComponent()
		{
			this.varGenComponents = new System.ComponentModel.Container();
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(859, 644);
			this.Text = "VariantGenForm";
			this.btnVariantTypes = new System.Windows.Forms.Button();
			this.lBoxVariantTypes = new System.Windows.Forms.ListBox();
			this.lbVariantTypes = new System.Windows.Forms.Label();
			this.tabControl.SuspendLayout();
			this.tabEditOps.SuspendLayout();
			this.plPattern.SuspendLayout();
			this.plActions.SuspendLayout();
			this.tabRunOps.SuspendLayout();
			this.tabEditReplaceOps.SuspendLayout();
			this.SuspendLayout();

			this.plActions.Controls.Add(this.btnVariantTypes);
			this.plActions.Controls.Add(this.lBoxVariantTypes);
			this.plActions.Controls.Add(this.lbVariantTypes);

			// 
			// lBoxVariantTypes
			// 
			this.lBoxVariantTypes.Enabled = false;
			this.lBoxVariantTypes.FormattingEnabled = true;
			this.lBoxVariantTypes.Location = new System.Drawing.Point(177, 119);
			this.lBoxVariantTypes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.lBoxVariantTypes.Name = "lBoxVariantTypes";
			this.lBoxVariantTypes.Size = new System.Drawing.Size(207, 82);
			this.lBoxVariantTypes.TabIndex = 4;
			this.lBoxVariantTypes.BringToFront();
			// 
			// lbVariantTypes
			// 
			this.lbVariantTypes.AutoSize = true;
			this.lbVariantTypes.Location = new System.Drawing.Point(83, 119);
			this.lbVariantTypes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbVariantTypes.Name = "lblbVariantTypes";
			this.lbVariantTypes.Size = new System.Drawing.Size(71, 13);
			this.lbVariantTypes.TabIndex = 3;
			this.lbVariantTypes.Text = "Variant Types";
			this.lbVariantTypes.BringToFront();
			// 
			// btnVariantTypes
			// 
			this.btnVariantTypes.Location = new System.Drawing.Point(399, 119);
			this.btnVariantTypes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnVariantTypes.Name = "btnVariantTypes";
			this.btnVariantTypes.Size = new System.Drawing.Size(33, 20);
			this.btnVariantTypes.TabIndex = 5;
			this.btnVariantTypes.Text = "Ed&it";
			this.btnVariantTypes.UseVisualStyleBackColor = true;
			this.btnVariantTypes.BringToFront();
			this.btnVariantTypes.Click += new System.EventHandler(this.btnVariantTypes_Click);
			this.plActions.ResumeLayout(false);
			this.plActions.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.tabEditOps.ResumeLayout(false);
			this.tabEditOps.PerformLayout();
			this.plPattern.ResumeLayout(false);
			this.plPattern.PerformLayout();
			this.plActions.ResumeLayout(false);
			this.plActions.PerformLayout();
			this.tabRunOps.ResumeLayout(false);
			this.tabRunOps.PerformLayout();
			this.tabEditReplaceOps.ResumeLayout(false);
			this.tabEditReplaceOps.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		protected System.Windows.Forms.Button btnVariantTypes;
		protected System.Windows.Forms.ListBox lBoxVariantTypes;
		protected System.Windows.Forms.Label lbVariantTypes;
	}
}