// Copyright (c) 2022 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using Microsoft.Win32;
using SIL.AlloGenModel;
using SIL.AlloGenService;
using SIL.FieldWorks.LexText.Controls;
using SIL.LCModel;
using SIL.LCModel.Infrastructure;
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
        public PropertyTable PropTable { get; set;  }

        private RegistryKey regkey;
        public static string m_strRegKey = "Software\\SIL\\AllomorphGenerator";
        const string m_strLastDatabase = "LastDatabase";
        const string m_strLastOperationsFile = "LastOperationsFile";
        const string m_strLastOperation = "Lastoperation";
        const string m_strLocationX = "LocationX";
        const string m_strLocationY = "LocationY";
        const string m_strSizeHeight = "SizeHeight";
        const string m_strSizeWidth = "SizeWidth";
        const string m_strWindowState = "WindowState";

        public Rectangle RectNormal { get; set; }

        public string LastDatabase { get; set; }
        public string LastOperationsFile { get; set; }
        public int LastOperation { get; set; }
        public string LastRootGlossSelection { get; set; }
        public int RetrievedLastOperation { get; set; }

        private String OperationsFile { get; set; }
        AllomorphGenerators AlloGens { get; set; }
        List<Operation> Operations { get; set; }
        Operation Operation { get; set; }
        List<Replace> ReplaceOps { get; set; }
        AlloGenModel.Action ActionOp { get; set; }
        InflectionFeature InflectionFeatures { get; set; }
        StemName StemName { get; set; }
        Pattern Pattern { get; set; }
        Category Category { get; set; }

        private ListBox currentListBox;
        private ContextMenuStrip editContextMenu;
        const string cmEdit = "Edit";
        const string cmInsertBefore = "Insert before";
        const string cmInsertAfter = "Insert after";
        const string cmMoveUp = "Move up";
        const string cmMoveDown = "Move down";
        const string cmDelete = "Delete";

        public AlloGenForm()
        {
            InitializeComponent();
            try
            {
                RememberFormState();
                XmlBackEndProvider provider = new XmlBackEndProvider();
                provider.LoadDataFromFile(OperationsFile);
                AlloGens = provider.AlloGens;
                if (AlloGens != null)
                {
                    Operations = AlloGens.Operations;
                }
                FillOperationsListBox();
                BuildReplaceContextMenu();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.StackTrace);
            }
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
            editContextMenu.Items.Add(editItem);
            editContextMenu.Items.Add("-");
            editContextMenu.Items.Add(insertBefore);
            editContextMenu.Items.Add(insertAfter);
            editContextMenu.Items.Add("-");
            editContextMenu.Items.Add(moveUp);
            editContextMenu.Items.Add(moveDown);
            editContextMenu.Items.Add("-");
            editContextMenu.Items.Add(deleteItem);
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
                    }
                }
            }
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
            RetrievedLastOperation = LastOperation = (int)regkey.GetValue(m_strLastOperation, 1);

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
            }
        }
        private void OnFormClosing(object sender, EventArgs e)
        {
            Console.WriteLine("form closing");
            SaveRegistryInfo();
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
            // select last used operation, if any
            if (LastOperation < 0 || LastOperation >= Operations.Count)
                LastOperation = 1;
            var selectedOperation = Operations[LastOperation];
            lBoxOperations.SetSelected(LastOperation, true);
        }

        private void lBoxOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            Operation = lBoxOperations.SelectedItem as Operation;
            LastOperation = lBoxOperations.SelectedIndex;
            tbName.Text = Operation.Name;
            tbDescription.Text = Operation.Description;
            Pattern = Operation.Pattern;
            tbMatch.Text = Pattern.Match;
            cbRegEx.Checked = Pattern.MatchMode;
            lBoxMorphTypes.DataSource = Pattern.MorphTypes;
            if (Pattern.MorphTypes.Count > 0)
            {
                var selectedMorphType = Pattern.MorphTypes[0];
            }
            lBoxEnvironments.DataSource = ActionOp.Environments;
            Category = Pattern.Category;
            ActionOp = Operation.Action;
            ReplaceOps = Operation.Action.ReplaceOps;
            RefreshReplaceListBox();
            if (ActionOp.ReplaceOps.Count > 0)
            {
                var selectedReplace = ActionOp.ReplaceOps[0];
            }
        }

        private void RefreshReplaceListBox()
        {
            int selectedItem = Math.Max(0,lBoxReplaceOps.SelectedIndex);
            lBoxReplaceOps.Items.Clear();
            foreach (Replace item in ReplaceOps)
            {
                lBoxReplaceOps.Items.Add(item);
            }
            if (ReplaceOps.Count > 0)
                lBoxReplaceOps.SetSelected(selectedItem, true);
        }

        private void lbMorphTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            if (Cache != null)
            {
                ISet<ICmPossibility> allPoses = Cache.LanguageProject.PartsOfSpeechOA.ReallyReallyAllPossibilities;

                CategoryChooser chooser = new CategoryChooser();
                foreach (ICmPossibility pos in allPoses)
                {
                    chooser.Categories.Add(pos.Name.BestAnalysisAlternative.Text);
                }
                chooser.FillCategoriesListBox();
                Category = Pattern.Category;
                if (Category.Name.Length > 0)
                {
                    int index = chooser.Categories.IndexOf(Category.Name);
                    if (index > -1)
                        chooser.SelectCategory(index);
                }
                chooser.ShowDialog();
                if (chooser.DialogResult == DialogResult.OK)
                {
                    Category.Name = chooser.SelectedCategory;
                    tbCategory.Text = Category.Name;
                }

            }
        }

        private void btnInflectionFeatures_Click(object sender, EventArgs e)
        {
            InflectionFeatures = ActionOp.InflectionFeatures;
            tbInflectionFeatures.Text = InflectionFeatures.Name;
            if (InflectionFeatures.Guid.Length > 0)
            {
                //Guid guid = new Guid(InflectionFeatures.Guid.Replace("-", ""));
                //    MessageBox.Show("guid before is " + InflectionFeatures.Guid);
                //    var lastFsSeen = Cache.ServiceLocator.ObjectRepository.GetObject(new Guid(InflectionFeatures.Guid));
                //    if (lastFsSeen == null)
                //        MessageBox.Show("last seen is null");
                //    else
                //        MessageBox.Show("last seen is " + lastFsSeen.ToString());
            }

            MsaInflectionFeatureListDlg dlg = new MsaInflectionFeatureListDlg();
            IPartOfSpeech pos = GetPartOfSpeechToUse("Verb");
            dlg.SetDlgInfo(Cache, Mediator, PropTable, pos);
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                ILcmOwningCollection<IFsFeatStruc> result = pos.ReferenceFormsOC;
                IFsFeatStruc fs = result.Objects.FirstOrDefault() as IFsFeatStruc;
                if (fs != null)
                {
                    InflectionFeatures.Name = fs.ToString();
                    InflectionFeatures.Guid = fs.Guid.ToString();
                    //MessageBox.Show("fs=" + InflectionFeatures.Name + "; guid=" + InflectionFeatures.Guid);
                    tbInflectionFeatures.Text = InflectionFeatures.Name;
                }
                //else
                //    MessageBox.Show("OK");
                NonUndoableUnitOfWorkHelper.Do(Cache.ActionHandlerAccessor, () =>
                {
                    pos.ReferenceFormsOC.Clear();
                });
            }
            dlg.Close();
        }

        private void btnStemName_Click(object sender, EventArgs e)
        {
            IPartOfSpeech pos = GetPartOfSpeechToUse("Verb");
            StemNameChooser chooser = new StemNameChooser();
            foreach (IMoStemName sn in pos.StemNamesOC)
            {
                chooser.StemNames.Add(sn.Name.BestAnalysisAlternative.Text);
            }
            chooser.FillStemNamesListBox();
            StemName = ActionOp.StemName;
            if (StemName.Name.Length > 0)
            {
                int index = chooser.StemNames.IndexOf(StemName.Name);
                if (index > -1)
                    chooser.SelectStemName(index);
            }
            chooser.ShowDialog();
            if (chooser.DialogResult == DialogResult.OK)
            {
                StemName.Name = chooser.SelectedStemName;
                tbStemName.Text = StemName.Name;
            }
        }

        private IPartOfSpeech GetPartOfSpeechToUse(string posName)
        {
            IPartOfSpeech pos = (IPartOfSpeech)Cache.LangProject.PartsOfSpeechOA.PossibilitiesOS.FirstOrDefault(p => p.Name.BestAnalysisAlternative.Text == posName);
            return pos;

        }

        private void btnEnvironments_Click(object sender, EventArgs e)
        {
            ILcmOwningSequence<IPhEnvironment> envs = Cache.LanguageProject.PhonologicalDataOA.EnvironmentsOS;
            MessageBox.Show("envs count=" + envs.Count);
        }
    }
}
