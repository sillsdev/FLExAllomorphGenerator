// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using Microsoft.Win32;
using SIL.AlloGenModel;
using SIL.AlloGenService;
using SIL.AllomorphGenerator;
using SIL.LCModel;
using SIL.LCModel.Infrastructure;
using SIL.VarGenService;
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
        Button btnVariantTypes;
        ListBox lBoxVariantTypes;
        Label lbVariantTypes;
        CheckBox cbShowMinorEntry;
        VariantCreator variantCreator;

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
            if (plActions != null)
            {
                RemoveEnvironmentsAndStemName();
                InitializeVariantTypesControls();
                InitializeShowInMinorEntryControls();
                SetBackColor();
            }
            // Create an instance of a ListView column sorter and assign it
            // to the ListView control.
            lvwColumnSorter = new ListViewColumnSorter();
            lvPreview.ListViewItemSorter = lvwColumnSorter;
            lvwEditReplaceOpsColumnSorter = new ListViewColumnSorter();
            lvEditReplaceOps.ListViewItemSorter = lvwEditReplaceOpsColumnSorter;
            try
            {
                RememberFormState();
                Provider = new XmlBackEndProvider();
                Migrator = new DatabaseMigrator();
                LoadMigrateGetOperations();
                FillOperationsListBox();
                FillApplyToComboBox();
                SetupFontAndStyleInfo();
                SetUpOperationsCheckedListBox();
                SetUpPreviewCheckedListBox();
                FillApplyOperationsListView();
                SetUpEditReplaceOpsListView();
                FillReplaceOpsListView();
                BuildReplaceContextMenu();
                BuildEditReplaceOpContextMenu();
                BuildHelpContextMenu();
                BuildOperationsCheckBoxContextMenu();
                BuildPreviewCheckBoxContextMenu();
                lBoxMorphTypes.ClearSelected();
                lBoxEnvironments.ClearSelected();
                RememberTabSelection();
                MarkAsChanged(false);
                variantCreator = new VariantCreator(Cache, WritingSystems);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.StackTrace);
            }
        }

        private void InitializeVariantTypesControls()
        {
            btnVariantTypes = new Button();
            lBoxVariantTypes = new ListBox();
            lbVariantTypes = new Label();
            plActions.Controls.Add(btnVariantTypes);
            plActions.Controls.Add(lBoxVariantTypes);
            plActions.Controls.Add(lbVariantTypes);
            //
            // lbVariantTypes
            //
            lbVariantTypes.AutoSize = true;
            lbVariantTypes.Location = new Point(83, 119);
            lbVariantTypes.Margin = new Padding(2, 0, 2, 0);
            lbVariantTypes.Name = "lblbVariantTypes";
            lbVariantTypes.Size = new Size(71, 13);
            lbVariantTypes.TabIndex = 3;
            lbVariantTypes.Text = "Variant Types";
            //
            // lBoxVariantTypes
            //
            lBoxVariantTypes.Enabled = false;
            lBoxVariantTypes.FormattingEnabled = true;
            lBoxVariantTypes.Location = new Point(177, 119);
            lBoxVariantTypes.Margin = new Padding(2, 2, 2, 2);
            lBoxVariantTypes.Name = "lBoxVariantTypes";
            lBoxVariantTypes.Size = new Size(207, 82);
            lBoxVariantTypes.TabIndex = 4;
            lBoxVariantTypes.BringToFront();
            //
            // btnVariantTypes
            //
            btnVariantTypes.Location = new Point(399, 119);
            btnVariantTypes.Margin = new Padding(2, 2, 2, 2);
            btnVariantTypes.Name = "btnVariantTypes";
            btnVariantTypes.Size = new Size(33, 20);
            btnVariantTypes.TabIndex = 5;
            btnVariantTypes.Text = "Ed&it";
            btnVariantTypes.UseVisualStyleBackColor = true;
            btnVariantTypes.BringToFront();
            btnVariantTypes.Click += new System.EventHandler(this.btnVariantTypes_Click);
        }

        private void InitializeShowInMinorEntryControls()
        {
            cbShowMinorEntry = new CheckBox();
            plActions.Controls.Add(cbShowMinorEntry);
            //
            // cbShowInMinorEntry
            //
            cbShowMinorEntry.AutoSize = true;
            cbShowMinorEntry.Location = new Point(81, 214);
            cbShowMinorEntry.Name = "cbShowInMinorEntry";
            cbShowMinorEntry.Margin = new Padding(2, 0, 2, 0);
            cbShowMinorEntry.Size = new System.Drawing.Size(60, 13);
            cbShowMinorEntry.TabIndex = 6;
            cbShowMinorEntry.Text = "Show minor entry";
            cbShowMinorEntry.Checked = true;
            cbShowMinorEntry.Click += new EventHandler(this.cbShowMinorEntry_Click);
        }

        private void RemoveEnvironmentsAndStemName()
        {
            plActions.Controls.Remove(lbEnvironments);
            plActions.Controls.Remove(lBoxEnvironments);
            plActions.Controls.Remove(btnEnvironments);
            plActions.Controls.Remove(lbStemName);
            plActions.Controls.Remove(tbStemName);
            plActions.Controls.Remove(btnStemName);
        }

        private void SetBackColor()
        {
            Color tabBackColor = Color.Linen;
            tabEditOps.BackColor = tabBackColor;
            tabRunOps.BackColor = tabBackColor;
            tabEditReplaceOps.BackColor = tabBackColor;
            plPattern.BackColor = tabBackColor;
            plActions.BackColor = tabBackColor;
        }

        override protected void RememberFormState()
        {
            RegKey = "Software\\SIL\\VariantGenerator";
            base.RememberFormState();
        }

        protected void btnVariantTypes_Click(object sender, EventArgs e)
        {
            if (Cache != null)
            {
                VariantTypesChooser chooser = new VariantTypesChooser(Cache);
                chooser.setSelected(ActionOp.VariantTypes);
                chooser.FillVarianTypesListBox();
                chooser.ShowDialog();
                if (chooser.DialogResult == DialogResult.OK)
                {
                    ActionOp.VariantTypes.Clear();
                    ActionOp.VariantTypes.AddRange(chooser.SelectedVariantTypes);
                    RefreshVariantTypesListBox();
                    MarkAsChanged(true);
                }
            }
        }

        protected void cbShowMinorEntry_Click(object sender, EventArgs e)
        {
            if (Operation != null)
            {
                Operation.Action.ShowMinorEntry = cbShowMinorEntry.Checked;
            }
        }

        protected void RefreshVariantTypesListBox()
        {
            lBoxVariantTypes.Items.Clear();
            foreach (AlloGenModel.VariantType item in ActionOp.VariantTypes)
            {
                lBoxVariantTypes.Items.Add(item);
            }
        }

        protected override bool CheckForInvalidActionComponents()
        {
            return CheckForInvalidVariantTypes();
        }

        protected override string CreateUndoRedoPrompt(Operation op)
        {
            return " Variant Generation for '" + op.Name;
        }

        protected override void ApplyOperationToEntry(
            Operation op,
            ILexEntry entry,
            List<string> forms
        )
        {
            ILexEntryRef variantEntryRef = variantCreator.CreateVariant(entry, forms);
            variantCreator.SetShowMinorEntry(variantEntryRef, op.Action.ShowMinorEntry);
            if (op.Action.VariantTypes.Count > 0)
            {
                variantCreator.AddVariantTypes(variantEntryRef, op.Action.VariantTypes);
            }
        }

        protected bool CheckForInvalidVariantTypes()
        {
            bool allIsGood = true;
            foreach (ListViewItem lvItem in lvOperations.CheckedItems)
            {
                Operation op = (Operation)lvItem.Tag;
                if (op.Action.VariantTypes.Count > 0)
                {
                    foreach (AlloGenModel.VariantType varType in op.Action.VariantTypes)
                    {
                        var variantType =
                            Cache.ServiceLocator.ObjectRepository.GetObjectOrIdWithHvoFromGuid(
                                new Guid(varType.Guid)
                            );
                        if (variantType == null)
                        {
                            ReportMissingFLExItem("The variant type '", varType.Name, op.Name);
                            allIsGood = false;
                        }
                    }
                }
            }
            return allIsGood;
        }

        protected override void lBoxOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.lBoxOperations_SelectedIndexChanged(sender, e);
            if (Operation != null)
            {
                RefreshVariantTypesListBox();
                cbShowMinorEntry.Checked = Operation.Action.ShowMinorEntry;
            }
        }
    }
}
