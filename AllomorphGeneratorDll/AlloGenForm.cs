// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using Microsoft.Win32;
using SIL.AlloGenModel;
using SIL.AlloGenService;
using SIL.AllomorphGenerator;
using SIL.LCModel;
using SIL.LCModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCore;

namespace SIL.AllomorphGenerator
{
    public partial class AlloGenForm : AlloGenFormBase
    {
        AllomorphCreator variantCreator;

        protected new const string OperationsFilePrompt =
            "Allomorph Generator Operations File (*.agf)|*.agf|" + "All Files (*.*)|*.*";

        public AlloGenForm(LcmCache cache, PropertyTable propTable, Mediator mediator)
        {
            Cache = cache;
            PropTable = propTable;
            Mediator = mediator;
            FLExCustomFieldsObtainer obtainer = new FLExCustomFieldsObtainer(cache);
            customFields = obtainer.CustomFields;
            AlloGenInitForm();
        }

        public AlloGenForm()
        {
            AlloGenInitForm();
        }

        protected void AlloGenInitForm()
        {
            if (plActions != null)
            {
                this.Text = "Allomorph Generator";
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
                variantCreator = new AllomorphCreator(Cache, WritingSystems);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.StackTrace);
            }
        }

		override protected void RememberFormState()
		{
			RegKey = "Software\\SIL\\AllomorphGenerator";
			base.RememberFormState();
		}

        protected override string CreateUndoRedoPrompt(Operation op)
        {
            return " Allomorph Generation for '" + op.Name;
        }

        protected override void GetMatchingEntries(
            PatternMatcher patMatcher,
            out IList<ILexEntry> matchingEntries,
            out IList<ILexEntry> matchingEntriesWithItems
        )
        {
            matchingEntries = patMatcher
                .MatchPattern(patMatcher.NonVariantMainEntries, Operation.Pattern)
                .ToList();
            // following gets all main entries that already have a variant that matches the replace op
            matchingEntriesWithItems = patMatcher
                .MatchEntriesWithVariantsPerPattern(Operation, Pattern)
                .ToList();
            foreach (ILexEntry entry in matchingEntriesWithItems)
            {
                if (matchingEntries.Contains(entry))
                {
                    // it is already there, so remove it
                    matchingEntries.Remove(entry);
                }
            }
        }

        protected override string GetOperationsFilePrompt()
        {
            return OperationsFilePrompt;
        }

        protected override Form BuildAboutBox()
        {
            return new AboutBox();
        }

        protected override Form BuildCreateNewOpenCancelDialog()
        {
            var dlg = new CreateNewOpenCancelDialog();
            dlg.Text = "Variant Generator";
            return dlg;
        }

		protected override string GetUserDocPath()
		{
			String basedir = GetAppBaseDir();
			return Path.Combine(basedir, "doc", "AlloGenUserDocumentation.pdf");
		}

		protected override Uri GetBaseUri()
		{
			return new Uri(Assembly.GetExecutingAssembly().CodeBase);
		}
	}
}
