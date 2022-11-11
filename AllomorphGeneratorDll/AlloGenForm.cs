// Copyright (c) 2022 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using Microsoft.Win32;
using SIL.AlloGenModel;
using SIL.AlloGenService;
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
    public partial class AlloGenForm : Form
    {

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

        private ContextMenuStrip replaceContextMenu;
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
            replaceContextMenu = new ContextMenuStrip();
            ToolStripMenuItem editItem = new ToolStripMenuItem(cmEdit);
            editItem.Click += new EventHandler(EditReplace_Click);
            editItem.Name = cmEdit;
            ToolStripMenuItem insertBefore = new ToolStripMenuItem(cmInsertBefore);
            insertBefore.Click += new EventHandler(InsertBeforeReplace_Click);
            insertBefore.Name = cmInsertBefore;
            ToolStripMenuItem insertAfter = new ToolStripMenuItem(cmInsertAfter);
            insertAfter.Click += new EventHandler(InsertAfterReplace_Click);
            insertAfter.Name = cmInsertAfter;
            ToolStripMenuItem moveUp = new ToolStripMenuItem(cmMoveUp);
            moveUp.Click += new EventHandler(MoveUpReplace_Click);
            moveUp.Name = cmMoveUp;
            ToolStripMenuItem moveDown = new ToolStripMenuItem(cmMoveDown);
            moveDown.Click += new EventHandler(MoveDownReplace_Click);
            moveDown.Name = cmMoveDown;
            ToolStripMenuItem deleteItem = new ToolStripMenuItem(cmDelete);
            deleteItem.Click += new EventHandler(DeleteReplace_Click);
            deleteItem.Name = cmDelete;
            replaceContextMenu.Items.Add(editItem);
            replaceContextMenu.Items.Add("-");
            replaceContextMenu.Items.Add(insertBefore);
            replaceContextMenu.Items.Add(insertAfter);
            replaceContextMenu.Items.Add("-");
            replaceContextMenu.Items.Add(moveUp);
            replaceContextMenu.Items.Add(moveDown);
            replaceContextMenu.Items.Add("-");
            replaceContextMenu.Items.Add(deleteItem);
        }

        private void lBoxReplaceOps_MouseUp(object sender, MouseEventArgs e)
        {
            int location = lBoxReplaceOps.IndexFromPoint(e.Location);
            if (e.Button == MouseButtons.Right)
            {
                ListBox lBoxSender = (ListBox)sender;
                int indexAtMouse = lBoxSender.IndexFromPoint(e.X, e.Y);
                if (indexAtMouse > -1)
                {
                    lBoxSender.SelectedIndex = indexAtMouse;
                    Point ptClickedAt = e.Location;
                    ptClickedAt = lBoxSender.PointToScreen(ptClickedAt);
                    replaceContextMenu.Show(ptClickedAt);
                }
            }
        }

        void EditReplace_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmEdit)
            {
                MessageBox.Show(cmEdit);
                //String basedir = GetAppBaseDir();
                //Process.Start(Path.Combine(basedir, "doc", "pcpatr.html"));
            }
        }

        void InsertBeforeReplace_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmInsertBefore)
            {
                MessageBox.Show(cmInsertBefore);
                //var dialog = new AboutBox();
                //// for some reason the following is needed to keep the dialog within the form
                //Point pt = dialog.PointToClient(System.Windows.Forms.Cursor.Position);
                //dialog.Location = new Point(this.Location.X + 20, this.Location.Y + 20);
                //Console.WriteLine("dialog result=" + dialog.Location.X + "," + dialog.Location.Y);
                //dialog.Show();
            }
        }

        void InsertAfterReplace_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmInsertAfter)
            {
                MessageBox.Show(cmInsertAfter);
                //var dialog = new AboutBox();
                //// for some reason the following is needed to keep the dialog within the form
                //Point pt = dialog.PointToClient(System.Windows.Forms.Cursor.Position);
                //dialog.Location = new Point(this.Location.X + 20, this.Location.Y + 20);
                //Console.WriteLine("dialog result=" + dialog.Location.X + "," + dialog.Location.Y);
                //dialog.Show();
            }
        }

        void MoveUpReplace_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmMoveUp)
            {
                MessageBox.Show(cmMoveUp);
                //String basedir = GetAppBaseDir();
                //Process.Start(Path.Combine(basedir, "doc", "pcpatr.html"));
            }
        }

        void MoveDownReplace_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmMoveDown)
            {
                MessageBox.Show(cmMoveDown);
                //String basedir = GetAppBaseDir();
                //Process.Start(Path.Combine(basedir, "doc", "pcpatr.html"));
            }
        }

        void DeleteReplace_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmDelete)
            {
                MessageBox.Show(cmDelete);
                //var dialog = new AboutBox();
                //// for some reason the following is needed to keep the dialog within the form
                //Point pt = dialog.PointToClient(System.Windows.Forms.Cursor.Position);
                //dialog.Location = new Point(this.Location.X + 20, this.Location.Y + 20);
                //Console.WriteLine("dialog result=" + dialog.Location.X + "," + dialog.Location.Y);
                //dialog.Show();
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
            lBoxOperations.DataSource = Operations;
            // select last used operation, if any
            if (LastOperation < 0 || LastOperation >= Operations.Count)
                LastOperation = 1;
            var selectedOperation = Operations[LastOperation];
        }

        private void lbOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            Operation operation = lBoxOperations.SelectedItem as Operation;
            LastOperation = lBoxOperations.SelectedIndex;
            tbName.Text = operation.Name;
            tbDescription.Text = operation.Description;
            Pattern pattern = operation.Pattern;
            tbMatch.Text = pattern.Match;
            cbRegEx.Checked = pattern.MatchMode;
            lBoxMorphTypes.DataSource = pattern.MorphTypes;
            if (pattern.MorphTypes.Count > 0)
            {
                var selectedMorphType = pattern.MorphTypes[0];
            }
            lBoxCategories.DataSource = pattern.Categories;
            if (pattern.Categories.Count > 0)
            {
                var selectedCategory = pattern.Categories[0];
            }
            AlloGenModel.Action action = operation.Action;
            lBoxReplaceOps.DataSource = action.ReplaceOps;
            if (action.ReplaceOps.Count > 0)
            {
                var selectedReplace = action.ReplaceOps[0];
            }

        }

        private void lbMorphTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
