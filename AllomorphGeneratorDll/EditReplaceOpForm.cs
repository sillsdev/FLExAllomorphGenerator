using SIL.AlloGenModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIL.AllomorphGenerator
{
    public partial class EditReplaceOpForm : Form
    {
        public Replace ReplaceOp { get; set; }
        public bool OkPressed { get; set; }

        public EditReplaceOpForm()
        {
            InitializeComponent();
        }

        public void Initialize(Replace replace)
        {
            ReplaceOp = replace;
            tbName.Text = replace.Name;
            tbDescription.Text = replace.Description;
            tbFrom.Text = replace.From;
            tbTo.Text = replace.To;
            cbRegEx.Checked = replace.Mode;
            cbAch.Checked = replace.Ach;
            cbAcl.Checked = replace.Acl;
            cbAkh.Checked = replace.Akh;
            cbAkl.Checked = replace.Akl;
            cbAme.Checked = replace.Ame;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ReplaceOp.Name = tbName.Text;
            ReplaceOp.Description = tbDescription.Text;
            ReplaceOp.From = tbFrom.Text;
            ReplaceOp.To = tbTo.Text;
            ReplaceOp.Mode = cbRegEx.Checked;
            ReplaceOp.Ach = cbAch.Checked;
            ReplaceOp.Acl = cbAcl.Checked;
            ReplaceOp.Akh = cbAkh.Checked;
            ReplaceOp.Akl = cbAkl.Checked;
            ReplaceOp.Ame = cbAme.Checked;
            OkPressed = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OkPressed = false;
            this.Close();
        }

        private void cbRegEx_CheckedChanged(object sender, EventArgs e)
        {
            ReplaceOp.Mode = cbRegEx.Checked;
        }

        private void cbAch_CheckedChanged(object sender, EventArgs e)
        {
            ReplaceOp.Ach = cbAch.Checked;
        }

        private void cbAcl_CheckedChanged(object sender, EventArgs e)
        {
            ReplaceOp.Acl = cbAcl.Checked;
        }

        private void cbAkh_CheckedChanged(object sender, EventArgs e)
        {
            ReplaceOp.Akh = cbAkh.Checked;
        }

        private void cbAkl_CheckedChanged(object sender, EventArgs e)
        {
            ReplaceOp.Akl = cbAkl.Checked;
        }

        private void cbAme_CheckedChanged(object sender, EventArgs e)
        {
            ReplaceOp.Ame = cbAme.Checked;
        }
    }
}
