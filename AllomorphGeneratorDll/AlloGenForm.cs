// Copyright (c) 2022-2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using Microsoft.Win32;
using SIL.AlloGenModel;
using SIL.AlloGenService;
using SIL.FieldWorks.Common.Controls;
using SIL.FieldWorks.Common.FwUtils;
using SIL.FieldWorks.Common.ViewsInterfaces;
using SIL.FieldWorks.Common.Widgets;
using SIL.FieldWorks.Filters;
using SIL.FieldWorks.FwCoreDlgs;
using SIL.FieldWorks.LexText.Controls;
using SIL.LCModel;
using SIL.LCModel.Core.KernelInterfaces;
using SIL.LCModel.Core.WritingSystems;
using SIL.LCModel.DomainServices;
using SIL.LCModel.Infrastructure;
using SIL.Utils;
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

namespace SIL.AllomorphGenerator
{
    public partial class AlloGenForm : Form
    {
        public LcmCache Cache { get; set; }
        public Mediator Mediator { get; set; }
        public PropertyTable PropTable { get; set; }

        private RegistryKey regkey;
        public static string m_strRegKey = "Software\\SIL\\AllomorphGenerator";
        const string m_strLastDatabase = "LastDatabase";
        const string m_strLastOperationsFile = "LastOperationsFile";
        const string m_strLastOperation = "Lastoperation";
        const string m_strLastApplyOperation = "LastApplyOperation";
        const string m_strLastTab = "LastTab";
        const string m_strLocationX = "LocationX";
        const string m_strLocationY = "LocationY";
        const string m_strSizeHeight = "SizeHeight";
        const string m_strSizeWidth = "SizeWidth";
        const string m_strWindowState = "WindowState";

        private ContextMenuStrip helpContextMenu;
        const string UserDocumentation = "User Documentation";
        const string About = "About";

        public Rectangle RectNormal { get; set; }

        public string LastDatabase { get; set; }
        public string LastOperationsFile { get; set; }
        public int LastOperation { get; set; }
        public int LastApplyOperation { get; set; }
        public int LastTab { get; set; }
        public int RetrievedLastOperation { get; set; }
        public int RetrievedLastApplyOperation { get; set; }

        XmlBackEndProvider Provider { get; set; }
        String OperationsFile { get; set; }
        AllomorphGenerators AlloGens { get; set; }
        List<Operation> Operations { get; set; }
        Operation Operation { get; set; }
        Operation LastOperationShown { get; set; } = null;
        List<Replace> ReplaceOps { get; set; }
        AlloGenModel.Action ActionOp { get; set; }
        StemName StemName { get; set; }
        Pattern Pattern { get; set; }
        Category Category { get; set; }
        bool ChangesMade { get; set; } = false;
        Font fontForDefaultCitationForm;
        FontInfo fontInfoForDefaultCitationForm;
        Color colorForDefaultCitationForm;
        Font fontForAkh;
        FontInfo fontInfoForAkh;
        Color colorForAkh;
        int wsForAkh = -1;
        Font fontForAcl;
        FontInfo fontInfoForAcl;
        Color colorForAcl;
        int wsForAcl = -1;
        Font fontForAkl;
        FontInfo fontInfoForAkl;
        Color colorForAkl;
        int wsForAkl = -1;
        Font fontForAch;
        FontInfo fontInfoForAch;
        Color colorForAch;
        int wsForAch = -1;
        Font fontForAme;
        FontInfo fontInfoForAme;
        Color colorForAme;
        int wsForAme = -1;
        Dictionary<Operation, List<ILexEntry>> dictNonChosen = new Dictionary<Operation, List<ILexEntry>>();

        private ListBox currentListBox;
        private ContextMenuStrip editContextMenu;
        const string formTitle = "Allomorph Generator";
        const string cmEdit = "Edit";
        const string cmInsertBefore = "Insert before";
        const string cmInsertAfter = "Insert after";
        const string cmMoveUp = "Move up";
        const string cmMoveDown = "Move down";
        const string cmDelete = "Delete";
        const string cmDuplicate = "Duplicate";

        private ContextMenuStrip operationsCheckBoxContextMenu;
        private ContextMenuStrip previewCheckBoxContextMenu;
        const string cmSelectAll = "Select All";
        const string cmClearAll = "Clear All";
        const string cmToggle = "Toggle";
        private ListViewColumnSorter lvwColumnSorter;

        public AlloGenForm(LcmCache cache, PropertyTable propTable, Mediator mediator)
        {
            Cache = cache;
            PropTable = propTable;
            Mediator = mediator;
            InitForm();
        }

        public AlloGenForm()
        {
            InitForm();
        }

        private void InitForm()
        {
            InitializeComponent();
            // Create an instance of a ListView column sorter and assign it
            // to the ListView control.
            lvwColumnSorter = new ListViewColumnSorter();
            this.lvPreview.ListViewItemSorter = lvwColumnSorter;
            try
            {
                RememberFormState();
                Provider = new XmlBackEndProvider();
                Provider.LoadDataFromFile(OperationsFile);
                AlloGens = Provider.AlloGens;
                if (AlloGens != null)
                {
                    Operations = AlloGens.Operations;
                }
                FillOperationsListBox();
                SetupFontAndStyleInfo();
                SetUpOperationsCheckedListBox();
                SetUpPreviewCheckedListBox();
                FillApplyOperationsListBox();
                BuildReplaceContextMenu();
                BuildHelpContextMenu();
                BuildOperationsCheckBoxContextMenu();
                BuildPreviewCheckBoxContextMenu();
                lBoxMorphTypes.ClearSelected();
                lBoxEnvironments.ClearSelected();
                RememberTabSelection();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.StackTrace);
            }
        }

        private void SetUpOperationsCheckedListBox()
        {
            lvOperations.SmallImageList = ilPreview;
            lvOperations.Columns.Add("", "", 25, HorizontalAlignment.Left, 0);
            lvOperations.Columns.Add("Operations", -2, HorizontalAlignment.Left);
        }

        private void SetUpPreviewCheckedListBox()
        {
            lvPreview.SmallImageList = ilPreview;
            lvPreview.Columns.Add("", "", 25, HorizontalAlignment.Left, 0);
            lvPreview.Columns.Add("Citation Form", -2, HorizontalAlignment.Left);
            lvPreview.Columns.Add("Akh          ", -2, HorizontalAlignment.Left);
            lvPreview.Columns.Add("Acl          ", -2, HorizontalAlignment.Left);
            lvPreview.Columns.Add("Akl          ", -2, HorizontalAlignment.Left);
            lvPreview.Columns.Add("Ach          ", -2, HorizontalAlignment.Left);
            lvPreview.Columns.Add("Ame          ", -2, HorizontalAlignment.Left);
        }

        private ListViewItem SetFontInfoForItem(ListViewItem item)
        {
            item.SubItems[1].Font = fontForDefaultCitationForm;
            item.SubItems[1].ForeColor = colorForDefaultCitationForm;
            item.SubItems[2].Font = fontForAkh;
            item.SubItems[2].ForeColor = colorForAkh;
            item.SubItems[3].Font = fontForAcl;
            item.SubItems[3].ForeColor = colorForAcl;
            item.SubItems[4].Font = fontForAkl;
            item.SubItems[4].ForeColor = colorForAkl;
            item.SubItems[5].Font = fontForAch;
            item.SubItems[5].ForeColor = colorForAch;
            item.SubItems[6].Font = fontForAme;
            item.SubItems[6].ForeColor = colorForAme;
            return item;
        }

        private void SetupFontAndStyleInfo()
        {
            if (Cache != null)
            {
                var styles = Cache.LangProject.StylesOC.ToDictionary(style => style.Name);
                IStStyle normal = Cache.LangProject.StylesOC.FirstOrDefault(style => style.Name == "Normal");
                if (normal != null)
                {
                    SIL.FieldWorks.FwCoreDlgControls.StyleInfo styleInfo = new SIL.FieldWorks.FwCoreDlgControls.StyleInfo(normal);
                    IList<CoreWritingSystemDefinition> vernWses = Cache.LangProject.CurrentVernacularWritingSystems;
                    foreach (CoreWritingSystemDefinition def in vernWses)
                    {
                        float fontSize = Math.Max(def.DefaultFontSize, 10);
                        // TODO: consider using a dictionary for these
                        switch (def.Abbreviation.Substring(def.Abbreviation.Length - 3))
                        {
                            case "akh":
                                wsForAkh = def.Handle;
                                fontForAkh = new Font(def.DefaultFontName, fontSize);
                                fontInfoForAkh = styleInfo.FontInfoForWs(def.Handle);
                                colorForAkh = fontInfoForAkh.FontColor.Value;
                                SetFontAndStyleInfoForDefaultCitationForm(wsForAkh, fontForAkh, fontInfoForAkh, colorForAkh);
                                break;
                            case "acl":
                                wsForAcl = def.Handle;
                                fontForAcl = new Font(def.DefaultFontName, fontSize);
                                fontInfoForAcl = styleInfo.FontInfoForWs(def.Handle);
                                colorForAcl = fontInfoForAcl.FontColor.Value;
                                SetFontAndStyleInfoForDefaultCitationForm(wsForAcl, fontForAcl, fontInfoForAcl, colorForAcl);
                                break;
                            case "akl":
                                wsForAkl = def.Handle;
                                fontForAkl = new Font(def.DefaultFontName, fontSize);
                                fontInfoForAkl = styleInfo.FontInfoForWs(def.Handle);
                                colorForAkl = fontInfoForAkl.FontColor.Value;
                                SetFontAndStyleInfoForDefaultCitationForm(wsForAkl, fontForAkl, fontInfoForAkl, colorForAkl);
                                break;
                            case "ach":
                                wsForAch = def.Handle;
                                fontForAch = new Font(def.DefaultFontName, fontSize);
                                fontInfoForAch = styleInfo.FontInfoForWs(def.Handle);
                                colorForAch = fontInfoForAch.FontColor.Value;
                                SetFontAndStyleInfoForDefaultCitationForm(wsForAch, fontForAch, fontInfoForAch, colorForAch);
                                break;
                            case "ame":
                                wsForAme = def.Handle;
                                fontForAme = new Font(def.DefaultFontName, fontSize);
                                fontInfoForAme = styleInfo.FontInfoForWs(def.Handle);
                                colorForAme = fontInfoForAme.FontColor.Value;
                                SetFontAndStyleInfoForDefaultCitationForm(wsForAme, fontForAme, fontInfoForAme, colorForAme);
                                break;
                        }
                    }
                }
            }
        }

        private void SetFontAndStyleInfoForDefaultCitationForm(int ws, Font font, FontInfo fontInfo, Color color)
        {
            if (ws == Cache.DefaultVernWs)
            {
                fontForDefaultCitationForm = font;
                fontInfoForDefaultCitationForm = fontInfo;
                colorForDefaultCitationForm = color;
            }
        }

        private void RememberTabSelection()
        {
            if (LastTab < 0 || LastTab > tabControl.TabCount)
                LastTab = 0;
            tabControl.SelectedIndex = LastTab;
        }

        private void BuildReplaceContextMenu()
        {
            editContextMenu = new ContextMenuStrip();
            editContextMenu.Name = "ReplaceOps";
            ToolStripMenuItem editItem = new ToolStripMenuItem(cmEdit);
            editItem.Click += new EventHandler(EditContextMenuReplace_Click);
            editItem.Name = cmEdit;
            ToolStripMenuItem insertBefore = new ToolStripMenuItem(cmInsertBefore);
            insertBefore.Click += new EventHandler(InsertBeforeContextMenu_Click);
            insertBefore.Name = cmInsertBefore;
            ToolStripMenuItem insertAfter = new ToolStripMenuItem(cmInsertAfter);
            insertAfter.Click += new EventHandler(InsertAfterContextMenu_Click);
            insertAfter.Name = cmInsertAfter;
            ToolStripMenuItem moveUp = new ToolStripMenuItem(cmMoveUp);
            moveUp.Click += new EventHandler(MoveUpContextMenu_Click);
            moveUp.Name = cmMoveUp;
            ToolStripMenuItem moveDown = new ToolStripMenuItem(cmMoveDown);
            moveDown.Click += new EventHandler(MoveDownContextMenu_Click);
            moveDown.Name = cmMoveDown;
            ToolStripMenuItem deleteItem = new ToolStripMenuItem(cmDelete);
            deleteItem.Click += new EventHandler(DeleteContextMenu_Click);
            deleteItem.Name = cmDelete;
            ToolStripMenuItem duplicateItem = new ToolStripMenuItem(cmDuplicate);
            duplicateItem.Click += new EventHandler(DuplicateContextMenu_Click);
            duplicateItem.Name = cmDuplicate;
            editContextMenu.Items.Add(editItem);
            editContextMenu.Items.Add("-");
            editContextMenu.Items.Add(duplicateItem);
            editContextMenu.Items.Add(insertBefore);
            editContextMenu.Items.Add(insertAfter);
            editContextMenu.Items.Add("-");
            editContextMenu.Items.Add(moveUp);
            editContextMenu.Items.Add(moveDown);
            editContextMenu.Items.Add("-");
            editContextMenu.Items.Add(deleteItem);
        }

        private void BuildHelpContextMenu()
        {
            helpContextMenu = new ContextMenuStrip();
            ToolStripMenuItem userDoc = new ToolStripMenuItem(UserDocumentation);
            userDoc.Click += new EventHandler(UserDoc_Click);
            userDoc.Name = UserDocumentation;
            ToolStripMenuItem about = new ToolStripMenuItem(About);
            about.Click += new EventHandler(About_Click);
            about.Name = About;
            helpContextMenu.Items.Add(userDoc);
            helpContextMenu.Items.Add("-");
            helpContextMenu.Items.Add(about);
        }

        private void BuildOperationsCheckBoxContextMenu()
        {
            operationsCheckBoxContextMenu = new ContextMenuStrip();
            ToolStripMenuItem selectAll = new ToolStripMenuItem(cmSelectAll);
            selectAll.Click += new EventHandler(OperationsSelectAll_Click);
            selectAll.Name = cmSelectAll;
            ToolStripMenuItem clearAll = new ToolStripMenuItem(cmClearAll);
            clearAll.Click += new EventHandler(OperationsClearAll_Click);
            clearAll.Name = cmClearAll;
            ToolStripMenuItem toggle = new ToolStripMenuItem(cmToggle);
            toggle.Click += new EventHandler(OperationsToggle_Click);
            toggle.Name = cmToggle;
            operationsCheckBoxContextMenu.Items.Add(selectAll);
            operationsCheckBoxContextMenu.Items.Add(clearAll);
            operationsCheckBoxContextMenu.Items.Add(toggle);
        }

        private void OperationsClearAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvItem in lvOperations.Items)
            {
                lvItem.Checked = false;
            }
        }

        private void OperationsSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvItem in lvOperations.Items)
            {
                lvItem.Checked = true;
            }
        }

        private void OperationsToggle_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvItem in lvOperations.Items)
            {
                lvItem.Checked = !lvItem.Checked;
            }
        }

        private void BuildPreviewCheckBoxContextMenu()
        {
            previewCheckBoxContextMenu = new ContextMenuStrip();
            ToolStripMenuItem selectAll = new ToolStripMenuItem(cmSelectAll);
            selectAll.Click += new EventHandler(PreviewSelectAll_Click);
            selectAll.Name = cmSelectAll;
            ToolStripMenuItem clearAll = new ToolStripMenuItem(cmClearAll);
            clearAll.Click += new EventHandler(PreviewClearAll_Click);
            clearAll.Name = cmClearAll;
            ToolStripMenuItem toggle = new ToolStripMenuItem(cmToggle);
            toggle.Click += new EventHandler(PreviewToggle_Click);
            toggle.Name = cmToggle;
            previewCheckBoxContextMenu.Items.Add(selectAll);
            previewCheckBoxContextMenu.Items.Add(clearAll);
            previewCheckBoxContextMenu.Items.Add(toggle);
        }

        private void PreviewClearAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvItem in lvPreview.Items)
            {
                lvItem.Checked = false;
            }
        }

        private void PreviewSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvItem in lvPreview.Items)
            {
                lvItem.Checked = true;
            }
        }

        private void PreviewToggle_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvItem in lvPreview.Items)
            {
                lvItem.Checked = !lvItem.Checked;
            }
        }

        private void lBoxReplaceOps_MouseUp(object sender, MouseEventArgs e)
        {
            HandleContextMenu(sender, e);
        }

        private void lBoxOperations_MouseUp(object sender, MouseEventArgs e)
        {
            HandleContextMenu(sender, e);
        }

        private void HandleContextMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListBox lBoxSender = (ListBox)sender;
                currentListBox = lBoxSender;
                int indexAtMouse = lBoxSender.IndexFromPoint(e.X, e.Y);
                if (indexAtMouse > -1)
                {
                    AdjustContextMenuContent(lBoxSender, indexAtMouse);
                    lBoxSender.SelectedIndex = indexAtMouse;
                    Point ptClickedAt = e.Location;
                    ptClickedAt = lBoxSender.PointToScreen(ptClickedAt);
                    editContextMenu.Show(ptClickedAt);
                }
            }
        }

        private void AdjustContextMenuContent(ListBox lBoxSender, int indexAtMouse)
        {
            int indexLast = lBoxSender.Items.Count - 1;
            if (lBoxSender.Name == "lBoxOperations")
            {
                // Do not show Edit and its separator
                editContextMenu.Items[0].Visible = false;
                editContextMenu.Items[1].Visible = false;
            }
            else
            {
                editContextMenu.Items[0].Visible = true;
                editContextMenu.Items[1].Visible = true;
            }
            if (indexAtMouse == 0)
                // move up does not work
                editContextMenu.Items[5].Enabled = false;
            else
                editContextMenu.Items[5].Enabled = true;
            if (indexAtMouse == 0 && indexLast == 0)
                // delete does not work
                editContextMenu.Items[8].Enabled = false;
            else
                editContextMenu.Items[8].Enabled = true;
            if (indexAtMouse == indexLast)
                // move down does not work
                editContextMenu.Items[6].Enabled = false;
            else
                editContextMenu.Items[6].Enabled = true;
        }

        void EditContextMenuReplace_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmEdit)
            {
                InvokeEditRelaceOpForm();
            }
        }

        private void InvokeEditRelaceOpForm()
        {
            using (var dialog = new EditReplaceOpForm())
            {
                Replace replace = (Replace)lBoxReplaceOps.SelectedItem;
                dialog.Initialize(replace);
                dialog.ShowDialog();
                if (dialog.DialogResult == DialogResult.OK)
                {
                    int index = lBoxReplaceOps.SelectedIndex;
                    replace = dialog.ReplaceOp;
                    ReplaceOps[index] = replace;
                    lBoxReplaceOps.Items[index] = replace;
                    MarkAsChanged();
                }
            }
        }

        void lBoxReplaceOps_DoubleClick(object sender, EventArgs e)
        {
            InvokeEditRelaceOpForm();
        }

        void InsertBeforeContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmInsertBefore)
            {
                DoContextMenuInsert(currentListBox.SelectedIndex);
            }
        }

        void InsertAfterContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmInsertAfter)
            {
                DoContextMenuInsert(currentListBox.SelectedIndex + 1);
            }
        }

        private void DoContextMenuInsert(int index)
        {
            if (currentListBox.Name == "lBoxReplaceOps")
            {
                Replace replace = new Replace();
                ReplaceOps.Insert(index, replace);
                currentListBox.Items.Insert(index, replace);
            }
            else
            {
                Operation op = new Operation();
                Operations.Insert(index, op);
                currentListBox.Items.Insert(index, op);
            }
            currentListBox.SetSelected(index, true);
            MarkAsChanged();
        }

        void MoveUpContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmMoveUp)
            {
                int index = currentListBox.SelectedIndex;
                DoContextMenuMove(index, index - 1);
            }
        }

        void MoveDownContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmMoveDown)
            {
                int index = currentListBox.SelectedIndex;
                DoContextMenuMove(index, index + 1);
            }
        }

        private void DoContextMenuMove(int index, int otherIndex)
        {
            Object selectedItem = currentListBox.SelectedItem;
            Object otherItem = currentListBox.Items[otherIndex];
            if (currentListBox.Name == "lBoxReplaceOps")
            {
                ReplaceOps[index] = (Replace)otherItem;
                ReplaceOps[otherIndex] = (Replace)selectedItem;
            }
            else
            {
                Operations[index] = (Operation)otherItem;
                Operations[otherIndex] = (Operation)selectedItem;
            }
            currentListBox.Items[index] = otherItem;
            currentListBox.Items[otherIndex] = selectedItem;
            MarkAsChanged();
        }

        void DeleteContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmDelete)
            {
                int index = currentListBox.SelectedIndex;
                if (currentListBox.Name == "lBoxReplaceOps")
                    ReplaceOps.RemoveAt(index);
                else
                    Operations.RemoveAt(index);
                currentListBox.Items.RemoveAt(index);
            }
            MarkAsChanged();
        }

        void DuplicateContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmDuplicate)
            {
                int index = currentListBox.SelectedIndex + 1;
                if (currentListBox.Name == "lBoxReplaceOps")
                {
                    Replace thisReplace = lBoxReplaceOps.SelectedItem as Replace;
                    Replace replace = thisReplace.Duplicate();
                    ReplaceOps.Insert(index, replace);
                    currentListBox.Items.Insert(index, replace);
                }
                else
                {
                    Operation op = Operation.Duplicate();
                    Operations.Insert(index, op);
                    currentListBox.Items.Insert(index, op);
                }
            }
            MarkAsChanged();
        }

        private void RememberFormState()
        {
            regkey = Registry.CurrentUser.OpenSubKey(m_strRegKey);
            if (regkey != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();
                RetrieveRegistryInfo();
                regkey.Close();
                DesktopBounds = RectNormal;
                WindowState = WindowState;
                StartPosition = FormStartPosition.Manual;
                if (!String.IsNullOrEmpty(LastOperationsFile))
                    tbFile.Text = LastOperationsFile;
                Cursor.Current = Cursors.Default;
            }
        }

        void RetrieveRegistryInfo()
        {
            // Window location
            int iX = (int)regkey.GetValue(m_strLocationX, 100);
            int iY = (int)regkey.GetValue(m_strLocationY, 100);
            int iWidth = (int)regkey.GetValue(m_strSizeWidth, 809); // 1228);
            int iHeight = (int)regkey.GetValue(m_strSizeHeight, 670); // 947);
            RectNormal = new Rectangle(iX, iY, iWidth, iHeight);
            // Set form properties
            WindowState = (FormWindowState)regkey.GetValue(m_strWindowState, 0);

            LastDatabase = (string)regkey.GetValue(m_strLastDatabase);
            OperationsFile = LastOperationsFile = (string)regkey.GetValue(m_strLastOperationsFile);
            RetrievedLastOperation = LastOperation = (int)regkey.GetValue(m_strLastOperation, 0);
            RetrievedLastApplyOperation = LastApplyOperation = (int)regkey.GetValue(m_strLastApplyOperation, 0);
            LastTab = (int)regkey.GetValue(m_strLastTab, 0);
        }

        public void SaveRegistryInfo()
        {
            regkey = Registry.CurrentUser.OpenSubKey(m_strRegKey, true);
            if (regkey == null)
            {
                regkey = Registry.CurrentUser.CreateSubKey(m_strRegKey);
            }

            if (LastDatabase != null)
                regkey.SetValue(m_strLastDatabase, LastDatabase);
            if (LastOperationsFile != null)
                regkey.SetValue(m_strLastOperationsFile, LastOperationsFile);
            regkey.SetValue(m_strLastOperation, LastOperation);
            regkey.SetValue(m_strLastApplyOperation, LastApplyOperation);
            regkey.SetValue(m_strLastTab, LastTab);
            // Window position and location
            regkey.SetValue(m_strWindowState, (int)WindowState);
            regkey.SetValue(m_strLocationX, RectNormal.X);
            regkey.SetValue(m_strLocationY, RectNormal.Y);
            regkey.SetValue(m_strSizeWidth, RectNormal.Width);
            regkey.SetValue(m_strSizeHeight, RectNormal.Height);
            regkey.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Allomorph Generator Operations File (*.agf)|*.agf|" +
            "All Files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                OperationsFile = dlg.FileName;
                LastOperationsFile = OperationsFile;
                tbFile.Text = OperationsFile;
                Provider.LoadDataFromFile(OperationsFile);
                AlloGens = Provider.AlloGens;
                FillOperationsListBox();
            }
        }
        private void OnFormClosing(object sender, EventArgs e)
        {
            SaveAnyChanges();
            SaveRegistryInfo();
        }

        private void SaveAnyChanges()
        {
            if (ChangesMade)
            {
                DialogResult res = MessageBox.Show("Changes have been made.  Do you want to save them?", "Changes made", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    Provider.SaveDataToFile(OperationsFile);
                }
            }
        }

        protected override void OnMove(EventArgs ea)
        {
            base.OnMove(ea);

            if (WindowState == FormWindowState.Normal)
                RectNormal = DesktopBounds;
        }
        protected override void OnResize(EventArgs ea)
        {
            base.OnResize(ea);

            if (WindowState == FormWindowState.Normal)
                RectNormal = DesktopBounds;
        }

        public void FillOperationsListBox()
        {
            lBoxOperations.Items.Clear();
            foreach (Operation op in Operations)
            {
                lBoxOperations.Items.Add(op);
            }
            if (Operations.Count > 0)
            {
                // select last used operation, if any
                if (LastOperation < 0 || LastOperation >= Operations.Count)
                    LastOperation = 0;
                lBoxOperations.SetSelected(LastOperation, true);
            }
        }

        public void FillApplyOperationsListBox()
        {
            lvOperations.Items.Clear();
            foreach (Operation op in Operations)
            {
                ListViewItem lvItem = new ListViewItem("");
                lvItem.UseItemStyleForSubItems = false;
                lvItem.Tag = op;
                lvItem.SubItems.Add(op.Name);
                lvItem.Checked = op.Active;
                lvOperations.Items.Add(lvItem);
            }
            if(Operations.Count > 0)
            {
                // select last used operation, if any
                if (LastApplyOperation < 0 || LastApplyOperation >= Operations.Count)
                    LastApplyOperation = 0;
                lvOperations.Items[LastApplyOperation].Selected = true;
                lvOperations.Select();
            }
        }

        private void lBoxOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            Operation = lBoxOperations.SelectedItem as Operation;
            if (Operation != null)
            {
                LastOperation = lBoxOperations.SelectedIndex;
                tbName.Text = Operation.Name;
                tbDescription.Text = Operation.Description;
                Pattern = Operation.Pattern;
                tbMatch.Text = Pattern.Matcher.Pattern;
                RefreshMorphTypesListBox();
                if (Pattern.MorphTypes.Count > 0)
                {
                    var selectedMorphType = Pattern.MorphTypes[0];
                }
                Category = Pattern.Category;
                tbCategory.Text = Category.Name;
                ActionOp = Operation.Action;
                RefreshEnvironmentsListBox();
                ReplaceOps = Operation.Action.ReplaceOps;
                RefreshReplaceListBox();
                if (ActionOp.ReplaceOps.Count > 0)
                {
                    var selectedReplace = ActionOp.ReplaceOps[0];
                }
                else
                {
                    // need at least one replace action
                    Replace replace = new Replace();
                    ActionOp.ReplaceOps.Add(replace);
                }
                StemName = ActionOp.StemName;
                tbStemName.Text = StemName.Name;
            }
        }

        private void RefreshReplaceListBox()
        {
            int selectedItem = Math.Max(0, lBoxReplaceOps.SelectedIndex);
            lBoxReplaceOps.Items.Clear();
            foreach (Replace item in ReplaceOps)
            {
                lBoxReplaceOps.Items.Add(item);
            }
            if (ReplaceOps.Count > 0)
                lBoxReplaceOps.SetSelected(selectedItem, true);
        }

        private void RefreshMorphTypesListBox()
        {
            lBoxMorphTypes.Items.Clear();
            foreach (MorphType item in Pattern.MorphTypes)
            {
                lBoxMorphTypes.Items.Add(item);
            }
        }

        private void RefreshEnvironmentsListBox()
        {
            lBoxEnvironments.Items.Clear();
            foreach (AlloGenModel.Environment item in ActionOp.Environments)
            {
                lBoxEnvironments.Items.Add(item);
            }
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            if (Cache != null)
            {
                var allPoses = Cache.LanguageProject.PartsOfSpeechOA.ReallyReallyAllPossibilities
                    .OrderBy(pos => pos.Name.BestAnalysisAlternative.Text);

                CategoryChooser chooser = new CategoryChooser();
                foreach (ICmPossibility pos in allPoses)
                {
                    Category cat = new Category();
                    cat.Name = pos.Name.BestAnalysisAlternative.Text;
                    cat.Guid = pos.Guid.ToString();
                    chooser.Categories.Add(cat);
                }
                chooser.FillCategoriesListBox();
                Category = Pattern.Category;
                if (Category.Name != null)
                {
                    var catFound = chooser.Categories.FirstOrDefault(cat => cat.Name == Category.Name);
                    int index = chooser.Categories.IndexOf(catFound);
                    if (index > -1)
                        chooser.SelectCategory(index);
                    else
                        chooser.SelectCategory(chooser.Categories.Count);
                }
                chooser.ShowDialog();
                if (chooser.DialogResult == DialogResult.OK)
                {
                    Category cat = chooser.SelectedCategory;
                    if (cat == chooser.NoneChosen)
                    {
                        Category.Name = "";
                        Category.Guid = "";
                        StemName = ActionOp.StemName;
                        ClearStemNameValues();
                        // if there's no category, there's no stem name
                        tbStemName.Text = StemName.Name;
                    }
                    else
                    {
                        if (Category.Guid != cat.Guid)
                        {
                            ClearStemNameValues();
                            tbStemName.Text = "";
                        }
                        Category.Name = cat.Name;
                        Category.Guid = new Guid(cat.Guid).ToString();
                    }
                    tbCategory.Text = Category.Name;
                    MarkAsChanged();
                }

            }
        }

        private void ClearStemNameValues()
        {
            StemName.Name = "";
            StemName.Guid = "";
        }

        private void btnStemName_Click(object sender, EventArgs e)
        {
            IPartOfSpeech pos = GetPartOfSpeechToUse(Pattern.Category.Guid);
            if (pos == null)
            {
                MessageBox.Show("The category '" + Pattern.Category.Name + "' was not found in the FLEx database");
                return;
            }
            StemNameChooser chooser = new StemNameChooser();
            foreach (IMoStemName msn in pos.AllStemNames.OrderBy(sn => sn.Name.BestAnalysisAlternative.Text))
            {
                StemName stemName = new StemName();
                stemName.Name = msn.Name.BestAnalysisAlternative.Text;
                stemName.Guid = msn.Guid.ToString();
                chooser.StemNames.Add(stemName);
            }
            chooser.FillStemNamesListBox();
            StemName = ActionOp.StemName;
            if (StemName.Name != null)
            {
                var snFound = chooser.StemNames.FirstOrDefault(sn => sn.Name == StemName.Name);
                int index = chooser.StemNames.IndexOf(snFound);
                if (index > -1)
                    chooser.SelectStemName(index);
                else
                    chooser.SelectStemName(chooser.StemNames.Count);
            }
            chooser.ShowDialog();
            if (chooser.DialogResult == DialogResult.OK)
            {
                StemName sn = chooser.SelectedStemName;
                if (sn == chooser.NoneChosen)
                {
                    ClearStemNameValues();
                }
                else
                {
                    StemName.Name = sn.Name;
                    StemName.Guid = new Guid(sn.Guid).ToString();
                }
                tbStemName.Text = StemName.Name;
                MarkAsChanged();
            }
        }

        private IPartOfSpeech GetPartOfSpeechToUse(string poaGuid)
        {
            IPartOfSpeech pos = (IPartOfSpeech)Cache.LangProject.PartsOfSpeechOA.ReallyReallyAllPossibilities.FirstOrDefault(p => p.Guid.ToString() == poaGuid);
            return pos;

        }

        private void btnEnvironments_Click(object sender, EventArgs e)
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
                    MarkAsChanged();
                }
            }
        }

        private void btnMorphTypes_Click(object sender, EventArgs e)
        {
            if (Cache != null)
            {
                MorphTypesChooser chooser = new MorphTypesChooser(Cache);
                chooser.setSelected(Pattern.MorphTypes);
                chooser.FillMorphTypesListBox();
                chooser.ShowDialog();
                if (chooser.DialogResult == DialogResult.OK)
                {
                    Pattern.MorphTypes.Clear();
                    Pattern.MorphTypes.AddRange(chooser.SelectedMorphTypes);
                    RefreshMorphTypesListBox();
                    MarkAsChanged();
                }
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                Operation.Name = tb.Text;
                MarkAsChanged();
                int selectedOp = lBoxOperations.SelectedIndex;
                if (selectedOp > -1)
                {
                    lBoxOperations.Items.Insert(selectedOp, Operation);
                    lBoxOperations.Items.RemoveAt(selectedOp + 1);
                    lBoxOperations.SelectedIndex = selectedOp;
                }
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            Provider.SaveDataToFile(OperationsFile);
            ChangesMade = false;
            ShowChangeStatusOnForm();
        }

        private void tbDescription_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                Operation.Description = tb.Text;
                MarkAsChanged();
            }
        }

        private void MarkAsChanged()
        {
            ChangesMade = true;
            ShowChangeStatusOnForm();
        }

        private void ShowChangeStatusOnForm()
        {
            AlloGenForm.ActiveForm.Text = formTitle;
            if (ChangesMade)
                AlloGenForm.ActiveForm.Text += "*";
        }

        private void btnNewFile_Click(object sender, EventArgs e)
        {
            SaveAnyChanges();
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Allomorph Generator Operations File (*.agf)|*.agf|" +
            "All Files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                OperationsFile = dlg.FileName;
                LastOperationsFile = OperationsFile;
                tbFile.Text = OperationsFile;
                AlloGens = new AllomorphGenerators();
                Operations = AlloGens.Operations;
                //MessageBox.Show("Operation count=" + Operations.Count);
                Operation op = new Operation();
                Operations.Add(op);
                FillOperationsListBox();
            }

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            helpContextMenu.Show(ptLowerLeft);
        }

        void About_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == About)
            {
                var dialog = new AboutBox();
                // for some reason the following is needed to keep the dialog within the form
                Point pt = dialog.PointToClient(System.Windows.Forms.Cursor.Position);
                dialog.Location = new Point(this.Location.X + 20, this.Location.Y + 20);
                Console.WriteLine("dialog result=" + dialog.Location.X + "," + dialog.Location.Y);
                dialog.Show();
            }
        }

        void UserDoc_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == UserDocumentation)
            {
                //String basedir = GetAppBaseDir();
                //Process.Start(Path.Combine(basedir, "doc", "UserDocumentation.pdf"));
                MessageBox.Show("Sorry, the user documentation is not yet available.");
            }
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            IVwStylesheet stylesheet = FontHeightAdjuster.StyleSheetFromPropertyTable(PropTable);

            using (SimpleMatchDlgAlloGen dlg = new SimpleMatchDlgAlloGen(Cache.WritingSystemFactory,
                PropTable.GetValue<IHelpTopicProvider>("HelpTopicProvider"), Cache.DefaultVernWs, stylesheet, Cache))
            {
                Matcher agMatcher = Pattern.Matcher;
                dlg.SetDlgValues(agMatcher, stylesheet);
                if (dlg.ShowDialog() != DialogResult.OK || dlg.Pattern.Length == 0)
                    return;
                agMatcher = dlg.GetMatcher();
                Pattern.Matcher = agMatcher;
                tbMatch.Text = agMatcher.Pattern;
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage page = (sender as TabControl).SelectedTab;
            if (page != null)
            {
                LastTab = tabControl.SelectedIndex;
                if (LastTab == 0)
                    FillOperationsListBox();
                else
                    FillApplyOperationsListBox();
            }
        }

        private void btnApplyOperations_Click(object sender, EventArgs e)
        {
            RememberNonChosenEntries(Operation);
            if (lvOperations.CheckedItems.Count == 0)
            {
                MessageBox.Show("No operations are selected, so there's nothing to do");
                // nothing to do
                return;
            }
            AllomorphCreator ac = new AllomorphCreator(Cache, wsForAkh, wsForAcl, wsForAkl, wsForAch, wsForAme);
            string undoRedoPrompt = " Allomorph Generation for '" + Operation.Name;
            UndoableUnitOfWorkHelper.Do("Undo" + undoRedoPrompt, "Redo" + undoRedoPrompt, Cache.ActionHandlerAccessor, () =>
            {
                foreach (ListViewItem lvItem in lvOperations.CheckedItems)
                {
                    Operation op = (Operation)lvItem.Tag;
                    List<ILexEntry> nonChosenEntries = new List<ILexEntry>();
                    if (dictNonChosen.ContainsKey(op))
                    {
                        nonChosenEntries = dictNonChosen[op];
                    }
                    PatternMatcher patMatcher = new PatternMatcher(op.Pattern, Cache);
                    IEnumerable<ILexEntry> matchingEntries = patMatcher.MatchPattern(patMatcher.SingleAllomorphs);
                    if (matchingEntries == null || matchingEntries.Count() == 0 || nonChosenEntries.Count == 0)
                    {
                        continue;
                    }
                    Replacer replacer = new Replacer(Operation.Action.ReplaceOps);
                    foreach (ILexEntry entry in matchingEntries)
                    {
                        if (nonChosenEntries.Contains(entry))
                        {
                            continue;
                        }
                        string citationForm = entry.CitationForm.VernacularDefaultWritingSystem.Text;
                        IMoStemAllomorph form = ac.CreateAllomorph(entry,
                            GetPreviewForm(replacer, citationForm, Dialect.Akh),
                            GetPreviewForm(replacer, citationForm, Dialect.Acl),
                            GetPreviewForm(replacer, citationForm, Dialect.Akl),
                            GetPreviewForm(replacer, citationForm, Dialect.Ach),
                            GetPreviewForm(replacer, citationForm, Dialect.Ame));
                        if (op.Action.StemName.Guid.Length > 0)
                        {
                            ac.AddStemName(form, op.Action.StemName.Guid);
                        }
                        if (op.Action.Environments.Count > 0)
                        {
                            ac.AddEnvironments(form, op.Action.Environments);
                        }
                    }
                }
            });
        }

        private void lvOperations_ItemCheck(object sender, EventArgs e)
        {
            if (lvOperations.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem lvItem = lvOperations.SelectedItems[0];
            Operation = lvItem.Tag as Operation;
            if (Operation != null)
            {
                Operation.Active = lvItem.Checked;
            }
        }

        private void lvOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LastOperationShown != null)
            {
                RememberNonChosenEntries(LastOperationShown);
            }
            string sCount = "0";
            if (lvOperations.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem lvItem = lvOperations.SelectedItems[0];
            Operation = lvItem.Tag as Operation;
            Pattern = Operation.Pattern;
            if (Operation != null)
            {
                PatternMatcher patMatcher = new PatternMatcher(Pattern, Cache);
                IEnumerable<ILexEntry> matchingEntries = patMatcher.MatchPattern(patMatcher.SingleAllomorphs);
                if (matchingEntries == null)
                {
                    string errMsg = string.Format(FwCoreDlgs.kstidErrorInRegEx, patMatcher.ErrorMessage);
                    MessageBox.Show(this, errMsg, FwCoreDlgs.kstidErrorInRegExHeader,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Replacer replacer = new Replacer(Operation.Action.ReplaceOps);
                lvPreview.Items.Clear();
                foreach (ILexEntry entry in matchingEntries)
                {
                    lvItem = new ListViewItem("");
                    lvItem.Tag = entry;
                    lvItem.UseItemStyleForSubItems = false;
                    string citationForm = entry.CitationForm.VernacularDefaultWritingSystem.Text;
                    lvItem.SubItems.Add(citationForm);
                    string previewForm = GetPreviewForm(replacer, citationForm, Dialect.Akh);
                    lvItem.SubItems.Add(previewForm);
                    previewForm = GetPreviewForm(replacer, citationForm, Dialect.Acl);
                    lvItem.SubItems.Add(previewForm);
                    previewForm = GetPreviewForm(replacer, citationForm, Dialect.Akl);
                    lvItem.SubItems.Add(previewForm);
                    previewForm = GetPreviewForm(replacer, citationForm, Dialect.Ach);
                    lvItem.SubItems.Add(previewForm);
                    previewForm = GetPreviewForm(replacer, citationForm, Dialect.Ame);
                    lvItem.SubItems.Add(previewForm);
                    lvItem = SetFontInfoForItem(lvItem);
                    lvPreview.Items.Add(lvItem);
                }
                sCount = matchingEntries.Count().ToString();
                PreviewSelectAll_Click(null, null);
            }
            lbCount.Text = sCount;
            LastOperationShown = Operation;
        }

        private void RememberNonChosenEntries(Operation op)
        {
            List<ILexEntry> uncheckedEntries = new List<ILexEntry>();
            foreach (ListViewItem item in lvPreview.Items)
            {
                if (!item.Checked)
                {
                    uncheckedEntries.Add((ILexEntry)item.Tag);
                }
            }
            if (dictNonChosen.ContainsKey(op))
            {
                dictNonChosen.Remove(op);
            }
            dictNonChosen.Add(op, uncheckedEntries);
        }

        private string GetPreviewForm(Replacer replacer, string citationForm, Dialect dialect)
        {
            string previewForm = replacer.ApplyReplaceOpToOneDialect(citationForm, dialect);
            return previewForm;
        }

        private void lvOperations_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Only show the context menu
            if (e.Column == 0)
            {
                ListView lvSender = (ListView)sender;
                Point ptLowerLeft = new Point(0, 10);
                ptLowerLeft = lvSender.PointToScreen(ptLowerLeft);
                operationsCheckBoxContextMenu.Show(ptLowerLeft);
                return;
            }
        }

        private void lvPreview_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Do not sort the checkboxes column; instead show context menu
            if (e.Column == 0)
            {
                ListView lvSender = (ListView)sender;
                Point ptLowerLeft = new Point(0, 10);
                ptLowerLeft =lvSender.PointToScreen(ptLowerLeft);
                previewCheckBoxContextMenu.Show(ptLowerLeft);
                return;
            }
            // Following code taken from
            // https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/sort-listview-by-column
            // on 2023.01.04
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvPreview.Sort();
        }

    }
}
