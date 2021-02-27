namespace Playbook_Editor
{
    partial class frmPSALVisualizer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPSALVisualizer));
            this.dgvPSALs = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnDrawPSAL = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pBarDrawPSAL = new System.Windows.Forms.ProgressBar();
            this.btn_Min = new System.Windows.Forms.Button();
            this.btn_Max = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.picPSAL = new System.Windows.Forms.PictureBox();
            this.mnuPSALVis = new System.Windows.Forms.MenuStrip();
            this.mnuFile_PSALVis = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenPSAL_PSALVis = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBreak_PSALVis = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit_PSALVis = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPSAL_PSALVis = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSavePSALImage_PSALVis = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAllPSALs_PSALVis = new System.Windows.Forms.ToolStripMenuItem();
            this.chbMovePlayer = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPSALs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPSAL)).BeginInit();
            this.mnuPSALVis.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPSALs
            // 
            this.dgvPSALs.AllowUserToAddRows = false;
            this.dgvPSALs.AllowUserToDeleteRows = false;
            this.dgvPSALs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPSALs.Enabled = false;
            this.dgvPSALs.Location = new System.Drawing.Point(12, 64);
            this.dgvPSALs.Name = "dgvPSALs";
            this.dgvPSALs.ReadOnly = true;
            this.dgvPSALs.Size = new System.Drawing.Size(462, 600);
            this.dgvPSALs.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnDrawPSAL
            // 
            this.btnDrawPSAL.Location = new System.Drawing.Point(12, 35);
            this.btnDrawPSAL.Name = "btnDrawPSAL";
            this.btnDrawPSAL.Size = new System.Drawing.Size(75, 23);
            this.btnDrawPSAL.TabIndex = 3;
            this.btnDrawPSAL.Text = "Draw PSAL";
            this.btnDrawPSAL.UseVisualStyleBackColor = true;
            this.btnDrawPSAL.Click += new System.EventHandler(this.btnDrawPSAL_Click);
            this.btnDrawPSAL.MouseHover += new System.EventHandler(this.btnDrawPSAL_MouseHover);
            // 
            // pBarDrawPSAL
            // 
            this.pBarDrawPSAL.Location = new System.Drawing.Point(93, 35);
            this.pBarDrawPSAL.Name = "pBarDrawPSAL";
            this.pBarDrawPSAL.Size = new System.Drawing.Size(381, 23);
            this.pBarDrawPSAL.TabIndex = 4;
            // 
            // btn_Min
            // 
            this.btn_Min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Min.BackColor = System.Drawing.Color.Transparent;
            this.btn_Min.FlatAppearance.BorderSize = 0;
            this.btn_Min.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_Min.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Min.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Min.Image = global::Playbook_Editor.Properties.Resources.btn_min;
            this.btn_Min.Location = new System.Drawing.Point(953, 1);
            this.btn_Min.Name = "btn_Min";
            this.btn_Min.Size = new System.Drawing.Size(22, 22);
            this.btn_Min.TabIndex = 7;
            this.btn_Min.UseVisualStyleBackColor = false;
            this.btn_Min.Click += new System.EventHandler(this.btn_Min_Click);
            this.btn_Min.MouseEnter += new System.EventHandler(this.btn_Min_MouseEnter);
            this.btn_Min.MouseLeave += new System.EventHandler(this.btn_Min_MouseLeave);
            // 
            // btn_Max
            // 
            this.btn_Max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Max.BackColor = System.Drawing.Color.Transparent;
            this.btn_Max.FlatAppearance.BorderSize = 0;
            this.btn_Max.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_Max.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Max.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Max.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Max.Image = global::Playbook_Editor.Properties.Resources.btn_max;
            this.btn_Max.Location = new System.Drawing.Point(977, 1);
            this.btn_Max.Name = "btn_Max";
            this.btn_Max.Size = new System.Drawing.Size(22, 22);
            this.btn_Max.TabIndex = 6;
            this.btn_Max.UseVisualStyleBackColor = false;
            this.btn_Max.Click += new System.EventHandler(this.btn_Max_Click);
            this.btn_Max.MouseEnter += new System.EventHandler(this.btn_Max_MouseEnter);
            this.btn_Max.MouseLeave += new System.EventHandler(this.btn_Max_MouseLeave);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.FlatAppearance.BorderSize = 0;
            this.btn_Close.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Image = global::Playbook_Editor.Properties.Resources.btn_close;
            this.btn_Close.Location = new System.Drawing.Point(1001, 1);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(22, 22);
            this.btn_Close.TabIndex = 5;
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            this.btn_Close.MouseEnter += new System.EventHandler(this.btn_Close_MouseEnter);
            this.btn_Close.MouseLeave += new System.EventHandler(this.btn_Close_MouseLeave);
            // 
            // picPSAL
            // 
            this.picPSAL.Image = global::Playbook_Editor.Properties.Resources.field_blank_resized;
            this.picPSAL.Location = new System.Drawing.Point(480, 64);
            this.picPSAL.Name = "picPSAL";
            this.picPSAL.Size = new System.Drawing.Size(533, 600);
            this.picPSAL.TabIndex = 0;
            this.picPSAL.TabStop = false;
            this.picPSAL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPSAL_MouseDown);
            this.picPSAL.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picPSAL_MouseMove);
            this.picPSAL.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picPSAL_MouseUp);
            // 
            // mnuPSALVis
            // 
            this.mnuPSALVis.BackgroundImage = global::Playbook_Editor.Properties.Resources.Football_toolbar;
            this.mnuPSALVis.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile_PSALVis,
            this.mnuPSAL_PSALVis});
            this.mnuPSALVis.Location = new System.Drawing.Point(0, 0);
            this.mnuPSALVis.Name = "mnuPSALVis";
            this.mnuPSALVis.Size = new System.Drawing.Size(1025, 24);
            this.mnuPSALVis.TabIndex = 1;
            this.mnuPSALVis.Text = "menuStrip1";
            // 
            // mnuFile_PSALVis
            // 
            this.mnuFile_PSALVis.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenPSAL_PSALVis,
            this.mnuBreak_PSALVis,
            this.mnuExit_PSALVis});
            this.mnuFile_PSALVis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mnuFile_PSALVis.Name = "mnuFile_PSALVis";
            this.mnuFile_PSALVis.Size = new System.Drawing.Size(37, 20);
            this.mnuFile_PSALVis.Text = "File";
            // 
            // mnuOpenPSAL_PSALVis
            // 
            this.mnuOpenPSAL_PSALVis.Name = "mnuOpenPSAL_PSALVis";
            this.mnuOpenPSAL_PSALVis.Size = new System.Drawing.Size(146, 22);
            this.mnuOpenPSAL_PSALVis.Text = "Open PSAL(s)";
            this.mnuOpenPSAL_PSALVis.Click += new System.EventHandler(this.mnuOpenPSAL_PSALVis_Click);
            // 
            // mnuBreak_PSALVis
            // 
            this.mnuBreak_PSALVis.Name = "mnuBreak_PSALVis";
            this.mnuBreak_PSALVis.Size = new System.Drawing.Size(143, 6);
            // 
            // mnuExit_PSALVis
            // 
            this.mnuExit_PSALVis.Name = "mnuExit_PSALVis";
            this.mnuExit_PSALVis.Size = new System.Drawing.Size(146, 22);
            this.mnuExit_PSALVis.Text = "Exit";
            this.mnuExit_PSALVis.Click += new System.EventHandler(this.mnuExit_PSALVis_Click);
            // 
            // mnuPSAL_PSALVis
            // 
            this.mnuPSAL_PSALVis.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSavePSALImage_PSALVis,
            this.mnuSaveAllPSALs_PSALVis});
            this.mnuPSAL_PSALVis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mnuPSAL_PSALVis.Name = "mnuPSAL_PSALVis";
            this.mnuPSAL_PSALVis.Size = new System.Drawing.Size(59, 20);
            this.mnuPSAL_PSALVis.Text = "PSAL(s)";
            // 
            // mnuSavePSALImage_PSALVis
            // 
            this.mnuSavePSALImage_PSALVis.Name = "mnuSavePSALImage_PSALVis";
            this.mnuSavePSALImage_PSALVis.Size = new System.Drawing.Size(237, 22);
            this.mnuSavePSALImage_PSALVis.Text = "Save PSAL Image";
            this.mnuSavePSALImage_PSALVis.Click += new System.EventHandler(this.mnuSavePSALImage_PSALVis_Click);
            // 
            // mnuSaveAllPSALs_PSALVis
            // 
            this.mnuSaveAllPSALs_PSALVis.Name = "mnuSaveAllPSALs_PSALVis";
            this.mnuSaveAllPSALs_PSALVis.Size = new System.Drawing.Size(237, 22);
            this.mnuSaveAllPSALs_PSALVis.Text = "Save All Selected PSALs to jpeg";
            this.mnuSaveAllPSALs_PSALVis.Click += new System.EventHandler(this.mnuSaveAllPSALs_PSALVis_Click);
            // 
            // chbMovePlayer
            // 
            this.chbMovePlayer.Appearance = System.Windows.Forms.Appearance.Button;
            this.chbMovePlayer.BackColor = System.Drawing.Color.White;
            this.chbMovePlayer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chbMovePlayer.Location = new System.Drawing.Point(696, 35);
            this.chbMovePlayer.Name = "chbMovePlayer";
            this.chbMovePlayer.Size = new System.Drawing.Size(101, 23);
            this.chbMovePlayer.TabIndex = 10;
            this.chbMovePlayer.Text = "Move Player";
            this.chbMovePlayer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chbMovePlayer.UseVisualStyleBackColor = false;
            this.chbMovePlayer.CheckedChanged += new System.EventHandler(this.chbMovePlayer_CheckedChanged);
            this.chbMovePlayer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chbMovePlayer_MouseDown);
            this.chbMovePlayer.MouseHover += new System.EventHandler(this.chbMovePlayer_MouseHover);
            // 
            // frmPSALVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BackgroundImage = global::Playbook_Editor.Properties.Resources.chalkboard_2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1025, 678);
            this.Controls.Add(this.chbMovePlayer);
            this.Controls.Add(this.btn_Min);
            this.Controls.Add(this.btn_Max);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.pBarDrawPSAL);
            this.Controls.Add(this.btnDrawPSAL);
            this.Controls.Add(this.dgvPSALs);
            this.Controls.Add(this.picPSAL);
            this.Controls.Add(this.mnuPSALVis);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuPSALVis;
            this.Name = "frmPSALVisualizer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgvPSALs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPSAL)).EndInit();
            this.mnuPSALVis.ResumeLayout(false);
            this.mnuPSALVis.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picPSAL;
        private System.Windows.Forms.MenuStrip mnuPSALVis;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_PSALVis;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenPSAL_PSALVis;
        private System.Windows.Forms.ToolStripSeparator mnuBreak_PSALVis;
        private System.Windows.Forms.ToolStripMenuItem mnuExit_PSALVis;
        private System.Windows.Forms.ToolStripMenuItem mnuPSAL_PSALVis;
        private System.Windows.Forms.ToolStripMenuItem mnuSavePSALImage_PSALVis;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAllPSALs_PSALVis;
        private System.Windows.Forms.DataGridView dgvPSALs;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnDrawPSAL;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ProgressBar pBarDrawPSAL;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Max;
        private System.Windows.Forms.Button btn_Min;
        private System.Windows.Forms.CheckBox chbMovePlayer;
    }
}

