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
    public partial class CategoryChooser : Form
    {
        public List<string> Categories { get; set; }
        public string SelectedCategory { get; set; } = "";

        public CategoryChooser()
        {
            Categories = new List<string>();
            InitializeComponent();
            lBoxCategories.Sorted = true;
            FillCategoriesListBox();
        }

        public void FillCategoriesListBox()
        {
            lBoxCategories.Items.Clear();
            foreach (string category in Categories)
            {
                lBoxCategories.Items.Add(category);
            }
        }

        public void SelectCategory(int index)
        {
            if (index > -1 && index < Categories.Count)
            {
                lBoxCategories.SelectedIndex = index;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedCategory = (string)lBoxCategories.SelectedItem;
        }

    }
}
