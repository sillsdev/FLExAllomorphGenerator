// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using SIL.AlloGenService;
using SIL.AllomorphGenerator;
using SIL.LCModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCore;

namespace SIL.VariantGenerator
{
	public partial class VariantGenForm : AlloGenForm
	{
		public VariantGenForm(LcmCache cache, PropertyTable propTable, Mediator mediator)
		{
			Cache = cache;
			PropTable = propTable;
			Mediator = mediator;
			FLExCustomFieldsObtainer obtainer = new FLExCustomFieldsObtainer(cache);
			customFields = obtainer.CustomFields;
			VarGenInitForm();
		}

		public VariantGenForm()
		{
			VarGenInitForm();
		}

		protected void VarGenInitForm()
		{
			InitForm();
			// TODO: add var gen items and hide the allo gen ones not needed
			this.Text = "Variant Generator";
			Color tabBackColor = Color.Linen;
			tabEditOps.BackColor = tabBackColor;
			tabRunOps.BackColor = tabBackColor;
			tabEditReplaceOps.BackColor = tabBackColor;
			plPattern.BackColor = tabBackColor;
			plActions.BackColor = tabBackColor;
		}

		override protected void btnEnvironments_Click(object sender, EventArgs e)
		{
			if (Cache != null)
			{
				EnvironmentsChooser chooser = new EnvironmentsChooser(Cache);
				chooser.setSelected(ActionOp.Environments);
				chooser.FillEnvironmentsListBox();
				chooser.ShowDialog();
				if (chooser.DialogResult == DialogResult.OK)
				{
					ActionOp.Environments.Clear();
					ActionOp.Environments.AddRange(chooser.SelectedEnvironments);
					RefreshEnvironmentsListBox();
					MarkAsChanged(true);
				}
			}
		}

	}
}
