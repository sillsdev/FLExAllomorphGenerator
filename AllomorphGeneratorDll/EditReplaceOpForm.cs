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

        public void Initialize(Replace replace, List<WritingSystem> writingSystems)
        {
            ReplaceOp = replace;
            tbName.Text = replace.Name;
            tbDescription.Text = replace.Description;
            tbFrom.Text = replace.From;
            tbTo.Text = replace.To;
            cbRegEx.Checked = replace.Mode;
            foreach (WritingSystem ws in writingSystems)
            {
                clbWritingSystems.Items.Add(ws);
                int index = clbWritingSystems.Items.Count - 1;
                if (replace.WritingSystemRefs.Contains(ws.Name))
                {
                    clbWritingSystems.SetItemChecked(index, true);
                }
            }
            tbName.Select();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ReplaceOp.Name = tbName.Text;
            ReplaceOp.Description = tbDescription.Text;
            ReplaceOp.From = tbFrom.Text;
            ReplaceOp.To = tbTo.Text;
            ReplaceOp.Mode = cbRegEx.Checked;
            ReplaceOp.WritingSystemRefs.Clear();
            for (int i = 0; i < clbWritingSystems.Items.Count; i++)
            {
                if (clbWritingSystems.GetItemChecked(i))
                {
                    string ws = clbWritingSystems.Items[i] as string;
                    if (ws != null)
                    {
                        ReplaceOp.WritingSystemRefs.Add(ws);
                    }
                }
            }
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

    }
}
