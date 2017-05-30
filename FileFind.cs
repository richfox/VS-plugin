using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;

using IronPython.Runtime; //PythonDictionary
using IronPython.Hosting; //PythonEngine
using Microsoft.Scripting; //ScriptDomainManager
using Microsoft.Scripting.Hosting;


namespace MyAddin
{
    public partial class FileFind : UserControl
    {
        // The following properties will be set up by code that calls Windows2.CreateToolWindow2
        public DTE2 Application;
        public AddIn AddInInstance;
        public Window FileFindWnd;

        /// <summary>
        /// Main IronPython ScriptEngine
        /// </summary>
        public ScriptEngine engine;

        /// <summary>
        /// Main IronPython ScriptScope
        /// </summary>
        public ScriptScope scope;

        public FileFind()
        {
            InitializeComponent();
            InitializeComponent2();
        }

        ~FileFind()
        {

        }

        public void Activate()
        {
            FileFindWnd.Activate();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            bool handled = false;
            switch (keyData)
            {
                case Keys.Return:
                    OpenSelectedItems();
                    handled = true;
                    break;
                case Keys.Escape:
                    CloseMyself();
                    handled = true;
                    break;
                default:
                    handled = base.ProcessDialogKey(keyData);
                    break;
            }

            return handled;
        }

        private void _listFiles_ColumnClick(object sender, ColumnClickEventArgs e)
        {

        }

        private void _listFiles_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedItems();
        }

        private void OpenSelectedItems()
        {
            Window wnd = null;

            foreach (int idx in _listFiles.SelectedIndices)
            {
                string path = _listFiles.Items[idx].SubItems[1].Text + "\\" + _listFiles.Items[idx].SubItems[0].Text;
                if (File.Exists(path))
                {
                    try
                    {
                        wnd = Application.ItemOperations.OpenFile(path, Constants.vsViewKindPrimary);
                        wnd.Activate();
                    }
                    catch (IOException)
                    {
                        MessageBox.Show(this, "this file cannot be opened.", "My Tool", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            if (wnd != null) // at least one document was opened
                CloseMyself();
        }

        private void CloseMyself()
        {
            if (FileFindWnd != null)
                FileFindWnd.Close();
        }

        private void SelectFirstItem()
        {
            if (_listFiles.Items.Count > 0)
            {
                if (_listFiles.SelectedIndices.Count > 0)
                    _listFiles.SelectedIndices.Clear();

                _listFiles.Items[0].Focused = true;
                _listFiles.Items[0].Selected = true;
            }
        }

        private void _toolStripTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    SelectFirstItem();
                    _listFiles.Focus();
                    break;
            }
        }


        private void _toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string path = "F:\\usr3\\hicad\\220X\\dev\\Source\\inc";

            if (_toolStripMenuItem1.Checked == false)
            {
                _toolStripMenuItem1.Checked = true;

                fill_list(path);
            }
            else
            {
                _toolStripMenuItem1.Checked = false;

                clear_list(path);
            }
        }

        private void _toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string path = "F:\\usr3\\hicad\\220X\\dev\\Source\\incc";

            if (_toolStripMenuItem2.Checked == false)
            {
                _toolStripMenuItem2.Checked = true;

                fill_list(path);
            }
            else
            {
                _toolStripMenuItem2.Checked = false;

                clear_list(path);
            }
        }

        private void _toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            string path = "F:\\usr3\\hicad\\220X\\dev\\Source\\incf";

            if (_toolStripMenuItem3.Checked == false)
            {
                _toolStripMenuItem3.Checked = true;

                fill_list(path);
            }
            else
            {
                _toolStripMenuItem3.Checked = false;

                clear_list(path);
            }
        }

        private void fill_list(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                ListViewItem item = new ListViewItem(file.Name);
                item.SubItems.Add(file.DirectoryName);
                item.Tag = path;
                _listFiles.Items.Add(item);
            }
        }

        private void clear_list(string path)
        {
            foreach (ListViewItem item in _listFiles.Items)
            {
                if (item.Tag.ToString() == path)
                {
                    item.Remove();
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Create the ScriptRuntime
            engine = Python.CreateEngine();
            //Create the scope for the ScriptEngine
            scope = engine.CreateScope();
            //Add IronPython Libs
            var paths = engine.GetSearchPaths();
            paths.Add(@"C:\IronPython-2.7.6.3\Lib");
            engine.SetSearchPaths(paths);


            var rt = engine.ExecuteFile("D:\\Cskill\\PythonScript\\dangdang\\dangdangPic.py", scope);

            //var name = scope.GetVariable("lineContent");
        }
    }
}
