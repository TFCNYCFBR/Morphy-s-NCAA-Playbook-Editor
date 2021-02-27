namespace PSAL_Visualizer
{
    partial class PSALVis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSALVis));
            this.dgvPSALs = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnDrawPSAL = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pBarDrawPSAL = new System.Windows.Forms.ProgressBar();
            this.btn_Min = new System.Windows.Forms.Button();
            this.btn_Max = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.picPSAL = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPSALsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pSALsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTopBorder = new System.Windows.Forms.Panel();
            this.chbMovePlayer = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPSALs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPSAL)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPSALs
            // 
            this.dgvPSALs.AllowUserToAddRows = false;
            this.dgvPSALs.AllowUserToDeleteRows = false;
            this.dgvPSALs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            this.btn_Min.FlatAppearance.BorderSize = 0;
            this.btn_Min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Min.Image = global::PSAL_Visualizer.Properties.Resources.btn_min;
            this.btn_Min.Location = new System.Drawing.Point(953, 1);
            this.btn_Min.Name = "btn_Min";
            this.btn_Min.Size = new System.Drawing.Size(22, 22);
            this.btn_Min.TabIndex = 7;
            this.btn_Min.UseVisualStyleBackColor = true;
            this.btn_Min.Click += new System.EventHandler(this.btn_Min_Click);
            // 
            // btn_Max
            // 
            this.btn_Max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Max.FlatAppearance.BorderSize = 0;
            this.btn_Max.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Max.Image = global::PSAL_Visualizer.Properties.Resources.btn_max;
            this.btn_Max.Location = new System.Drawing.Point(977, 1);
            this.btn_Max.Name = "btn_Max";
            this.btn_Max.Size = new System.Drawing.Size(22, 22);
            this.btn_Max.TabIndex = 6;
            this.btn_Max.UseVisualStyleBackColor = true;
            this.btn_Max.Click += new System.EventHandler(this.btn_Max_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.FlatAppearance.BorderSize = 0;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Image = global::PSAL_Visualizer.Properties.Resources.btn_close;
            this.btn_Close.Location = new System.Drawing.Point(1001, 1);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(22, 22);
            this.btn_Close.TabIndex = 5;
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // picPSAL
            // 
            this.picPSAL.Image = ((System.Drawing.Image)(resources.GetObject("picPSAL.Image")));
            this.picPSAL.Location = new System.Drawing.Point(480, 64);
            this.picPSAL.Name = "picPSAL";
            this.picPSAL.Size = new System.Drawing.Size(533, 600);
            this.picPSAL.TabIndex = 0;
            this.picPSAL.TabStop = false;
            this.picPSAL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPSAL_MouseDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImage = global::PSAL_Visualizer.Properties.Resources.Football_toolbar;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.pSALsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(104, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPSALsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openPSALsToolStripMenuItem
            // 
            this.openPSALsToolStripMenuItem.Name = "openPSALsToolStripMenuItem";
            this.openPSALsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openPSALsToolStripMenuItem.Text = "Open PSAL(s)";
            this.openPSALsToolStripMenuItem.Click += new System.EventHandler(this.openPSALsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // pSALsToolStripMenuItem
            // 
            this.pSALsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToolStripMenuItem,
            this.saveAllImagesToolStripMenuItem});
            this.pSALsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pSALsToolStripMenuItem.Name = "pSALsToolStripMenuItem";
            this.pSALsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.pSALsToolStripMenuItem.Text = "PSAL(s)";
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.saveImageToolStripMenuItem.Text = "Save PSAL Image";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // saveAllImagesToolStripMenuItem
            // 
            this.saveAllImagesToolStripMenuItem.Name = "saveAllImagesToolStripMenuItem";
            this.saveAllImagesToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.saveAllImagesToolStripMenuItem.Text = "Save All Selected PSALs to jpeg";
            this.saveAllImagesToolStripMenuItem.Click += new System.EventHandler(this.saveAllImagesToolStripMenuItem_Click);
            // 
            // pnlTopBorder
            // 
            this.pnlTopBorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTopBorder.BackColor = System.Drawing.Color.Transparent;
            this.pnlTopBorder.BackgroundImage = global::PSAL_Visualizer.Properties.Resources.Football_toolbar;
            this.pnlTopBorder.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBorder.Name = "pnlTopBorder";
            this.pnlTopBorder.Size = new System.Drawing.Size(1024, 24);
            this.pnlTopBorder.TabIndex = 9;
            this.pnlTopBorder.DoubleClick += new System.EventHandler(this.pnlTopBorder_DoubleClick);
            this.pnlTopBorder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTopBorder_MouseDown);
            this.pnlTopBorder.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTopBorder_MouseMove);
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
            // PSALVis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BackgroundImage = global::PSAL_Visualizer.Properties.Resources.chalkboard;
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
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlTopBorder);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PSALVis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PSAL Visualizer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPSALs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPSAL)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripPlaybook;
        private System.Windows.Forms.ToolStripMenuItem filePlaybookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPlaybookToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator PlaybooktoolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pSALsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllImagesToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvPSALs;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnDrawPSAL;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ProgressBar pBarDrawPSAL;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Max;
        private System.Windows.Forms.Button btn_Min;
        private System.Windows.Forms.Panel pnlTopBorder;
        private System.Windows.Forms.CheckBox chbMovePlayer;
    }
}

