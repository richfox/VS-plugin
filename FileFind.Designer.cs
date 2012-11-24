namespace MyAddin
{
    partial class FileFind
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileFind));
            this._listFiles = new System.Windows.Forms.ListView();
            this._columnFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._toolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this._toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this._toolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _listFiles
            // 
            this._listFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._columnFile,
            this._columnPath});
            this._listFiles.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._listFiles.FullRowSelect = true;
            this._listFiles.Location = new System.Drawing.Point(0, 33);
            this._listFiles.MultiSelect = false;
            this._listFiles.Name = "_listFiles";
            this._listFiles.Size = new System.Drawing.Size(573, 289);
            this._listFiles.TabIndex = 1;
            this._listFiles.UseCompatibleStateImageBehavior = false;
            this._listFiles.View = System.Windows.Forms.View.Details;
            this._listFiles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this._listFiles_ColumnClick);
            this._listFiles.DoubleClick += new System.EventHandler(this._listFiles_DoubleClick);
            // 
            // _columnFile
            // 
            this._columnFile.Text = "File";
            this._columnFile.Width = 170;
            // 
            // _columnPath
            // 
            this._columnPath.Text = "Path";
            this._columnPath.Width = 293;
            // 
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripTextBox,
            this._toolStripSeparator,
            this._toolStripDropDownButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(573, 25);
            this._toolStrip.TabIndex = 2;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _toolStripTextBox
            // 
            this._toolStripTextBox.Name = "_toolStripTextBox";
            this._toolStripTextBox.Size = new System.Drawing.Size(300, 25);
            this._toolStripTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this._toolStripTextBox_KeyDown);
            // 
            // _toolStripSeparator
            // 
            this._toolStripSeparator.Name = "_toolStripSeparator";
            this._toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // _toolStripDropDownButton
            // 
            this._toolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripMenuItem1,
            this._toolStripMenuItem2,
            this._toolStripMenuItem3});
            this._toolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("_toolStripDropDownButton.Image")));
            this._toolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripDropDownButton.Name = "_toolStripDropDownButton";
            this._toolStripDropDownButton.Size = new System.Drawing.Size(29, 22);
            this._toolStripDropDownButton.Text = "toolStripDropDownButton1";
            // 
            // _toolStripMenuItem1
            // 
            this._toolStripMenuItem1.Name = "_toolStripMenuItem1";
            this._toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this._toolStripMenuItem1.Text = "inc";
            this._toolStripMenuItem1.Click += new System.EventHandler(this._toolStripMenuItem1_Click);
            // 
            // _toolStripMenuItem2
            // 
            this._toolStripMenuItem2.Name = "_toolStripMenuItem2";
            this._toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this._toolStripMenuItem2.Text = "incc";
            this._toolStripMenuItem2.Click += new System.EventHandler(this._toolStripMenuItem2_Click);
            // 
            // _toolStripMenuItem3
            // 
            this._toolStripMenuItem3.Name = "_toolStripMenuItem3";
            this._toolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this._toolStripMenuItem3.Text = "incf";
            this._toolStripMenuItem3.Click += new System.EventHandler(this._toolStripMenuItem3_Click);
            // 
            // FileFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this._toolStrip);
            this.Controls.Add(this._listFiles);
            this.Name = "FileFind";
            this.Size = new System.Drawing.Size(573, 322);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void InitializeComponent2()
        {
            string cueBannerText = "enter search strings";
            EditControl.SetCueBannerText(_toolStripTextBox.TextBox,cueBannerText);
        }

        private System.Windows.Forms.ListView _listFiles;
        private System.Windows.Forms.ColumnHeader _columnFile;
        private System.Windows.Forms.ColumnHeader _columnPath;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripTextBox _toolStripTextBox;
        private System.Windows.Forms.ToolStripSeparator _toolStripSeparator;
        private System.Windows.Forms.ToolStripDropDownButton _toolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem _toolStripMenuItem3;
    }
}
