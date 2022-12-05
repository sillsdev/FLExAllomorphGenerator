// Copyright (c) 2022 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

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
    public partial class StemNameChooser : Form
    {
        public List<string> StemNames { get; set; }
        public string SelectedStemName { get; set; } = "";

        public StemNameChooser()
        {
            StemNames = new List<string>();
            InitializeComponent();
            lBoxStemNames.Sorted = true;
        }

        public void FillStemNamesListBox()
        {
            foreach (string stemName in StemNames)
            {
                lBoxStemNames.Items.Add(stemName);
            }
        }

        public void SelectStemName(int index)
        {
            if (index > -1 && index < StemNames.Count)
            {
                lBoxStemNames.SelectedIndex = index;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedStemName = (string)lBoxStemNames.SelectedItem;
        }

    }
}
