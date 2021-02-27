namespace Playbook_Editor
{
    partial class frmEditPlay
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_Close = new System.Windows.Forms.Button();
            this.mnuEditPlay = new System.Windows.Forms.MenuStrip();
            this.mnuEditPlay_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPlay_Options_PLPD = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPlay_Options_PLPD_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPlay_Options_PLPD_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPlay_Options_PLRD = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPlay_Options_PLRD_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPlay_Options_PLRD_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvPBPL = new System.Windows.Forms.DataGridView();
            this.lblPBPL = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblPLPD = new System.Windows.Forms.Label();
            this.dgvPLPD = new System.Windows.Forms.DataGridView();
            this.lblPLRD = new System.Windows.Forms.Label();
            this.dgvPLRD = new System.Windows.Forms.DataGridView();
            this.lblSRFT = new System.Windows.Forms.Label();
            this.dgvSRFT = new System.Windows.Forms.DataGridView();
            this.cmsEditSRFT = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsEditSRFT_Insert_Above = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEditSRFT_Insert_Below = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEditSRFT_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPBPL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPLPD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPLRD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSRFT)).BeginInit();
            this.cmsEditSRFT.SuspendLayout();
            this.SuspendLayout();
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
            this.btn_Close.Location = new System.Drawing.Point(768, 1);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(22, 22);
            this.btn_Close.TabIndex = 12;
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            this.btn_Close.MouseEnter += new System.EventHandler(this.btn_Close_MouseEnter);
            this.btn_Close.MouseLeave += new System.EventHandler(this.btn_Close_MouseLeave);
            // 
            // mnuEditPlay
            // 
            this.mnuEditPlay.BackgroundImage = global::Playbook_Editor.Properties.Resources.Football_toolbar;
            this.mnuEditPlay.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditPlay_Options});
            this.mnuEditPlay.Location = new System.Drawing.Point(0, 0);
            this.mnuEditPlay.Name = "mnuEditPlay";
            this.mnuEditPlay.Size = new System.Drawing.Size(792, 24);
            this.mnuEditPlay.TabIndex = 15;
            // 
            // mnuEditPlay_Options
            // 
            this.mnuEditPlay_Options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditPlay_Options_PLPD,
            this.mnuEditPlay_Options_PLRD});
            this.mnuEditPlay_Options.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mnuEditPlay_Options.Name = "mnuEditPlay_Options";
            this.mnuEditPlay_Options.Size = new System.Drawing.Size(61, 20);
            this.mnuEditPlay_Options.Text = "Options";
            // 
            // mnuEditPlay_Options_PLPD
            // 
            this.mnuEditPlay_Options_PLPD.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditPlay_Options_PLPD_Add,
            this.mnuEditPlay_Options_PLPD_Remove});
            this.mnuEditPlay_Options_PLPD.Name = "mnuEditPlay_Options_PLPD";
            this.mnuEditPlay_Options_PLPD.Size = new System.Drawing.Size(102, 22);
            this.mnuEditPlay_Options_PLPD.Text = "PLPD";
            // 
            // mnuEditPlay_Options_PLPD_Add
            // 
            this.mnuEditPlay_Options_PLPD_Add.Name = "mnuEditPlay_Options_PLPD_Add";
            this.mnuEditPlay_Options_PLPD_Add.Size = new System.Drawing.Size(117, 22);
            this.mnuEditPlay_Options_PLPD_Add.Text = "Add";
            this.mnuEditPlay_Options_PLPD_Add.Click += new System.EventHandler(this.mnuEditPlay_Options_PLPD_Add_Click);
            // 
            // mnuEditPlay_Options_PLPD_Remove
            // 
            this.mnuEditPlay_Options_PLPD_Remove.Name = "mnuEditPlay_Options_PLPD_Remove";
            this.mnuEditPlay_Options_PLPD_Remove.Size = new System.Drawing.Size(117, 22);
            this.mnuEditPlay_Options_PLPD_Remove.Text = "Remove";
            this.mnuEditPlay_Options_PLPD_Remove.Click += new System.EventHandler(this.mnuEditPlay_Options_PLPD_Remove_Click);
            // 
            // mnuEditPlay_Options_PLRD
            // 
            this.mnuEditPlay_Options_PLRD.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditPlay_Options_PLRD_Add,
            this.mnuEditPlay_Options_PLRD_Remove});
            this.mnuEditPlay_Options_PLRD.Name = "mnuEditPlay_Options_PLRD";
            this.mnuEditPlay_Options_PLRD.Size = new System.Drawing.Size(102, 22);
            this.mnuEditPlay_Options_PLRD.Text = "PLRD";
            // 
            // mnuEditPlay_Options_PLRD_Add
            // 
            this.mnuEditPlay_Options_PLRD_Add.Name = "mnuEditPlay_Options_PLRD_Add";
            this.mnuEditPlay_Options_PLRD_Add.Size = new System.Drawing.Size(117, 22);
            this.mnuEditPlay_Options_PLRD_Add.Text = "Add";
            this.mnuEditPlay_Options_PLRD_Add.Click += new System.EventHandler(this.mnuEditPlay_Options_PLRD_Add_Click);
            // 
            // mnuEditPlay_Options_PLRD_Remove
            // 
            this.mnuEditPlay_Options_PLRD_Remove.Name = "mnuEditPlay_Options_PLRD_Remove";
            this.mnuEditPlay_Options_PLRD_Remove.Size = new System.Drawing.Size(117, 22);
            this.mnuEditPlay_Options_PLRD_Remove.Text = "Remove";
            this.mnuEditPlay_Options_PLRD_Remove.Click += new System.EventHandler(this.mnuEditPlay_Options_PLRD_Remove_Click);
            // 
            // dgvPBPL
            // 
            this.dgvPBPL.AllowUserToAddRows = false;
            this.dgvPBPL.AllowUserToDeleteRows = false;
            this.dgvPBPL.AllowUserToResizeColumns = false;
            this.dgvPBPL.AllowUserToResizeRows = false;
            this.dgvPBPL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPBPL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPBPL.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPBPL.Location = new System.Drawing.Point(12, 52);
            this.dgvPBPL.Name = "dgvPBPL";
            this.dgvPBPL.RowHeadersVisible = false;
            this.dgvPBPL.Size = new System.Drawing.Size(768, 50);
            this.dgvPBPL.TabIndex = 26;
            this.dgvPBPL.Visible = false;
            this.dgvPBPL.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPBPL_CellValueChanged);
            // 
            // lblPBPL
            // 
            this.lblPBPL.AutoSize = true;
            this.lblPBPL.BackColor = System.Drawing.Color.Transparent;
            this.lblPBPL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPBPL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPBPL.Location = new System.Drawing.Point(12, 36);
            this.lblPBPL.Name = "lblPBPL";
            this.lblPBPL.Size = new System.Drawing.Size(127, 16);
            this.lblPBPL.TabIndex = 27;
            this.lblPBPL.Text = "PBPL - Play Data";
            this.lblPBPL.Visible = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdate.Location = new System.Drawing.Point(330, 572);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(141, 23);
            this.btnUpdate.TabIndex = 28;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblPLPD
            // 
            this.lblPLPD.AutoSize = true;
            this.lblPLPD.BackColor = System.Drawing.Color.Transparent;
            this.lblPLPD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPLPD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPLPD.Location = new System.Drawing.Point(12, 119);
            this.lblPLPD.Name = "lblPLPD";
            this.lblPLPD.Size = new System.Drawing.Size(167, 16);
            this.lblPLPD.TabIndex = 30;
            this.lblPLPD.Text = "PLPD - Pass Play Data";
            this.lblPLPD.Visible = false;
            // 
            // dgvPLPD
            // 
            this.dgvPLPD.AllowUserToAddRows = false;
            this.dgvPLPD.AllowUserToDeleteRows = false;
            this.dgvPLPD.AllowUserToResizeColumns = false;
            this.dgvPLPD.AllowUserToResizeRows = false;
            this.dgvPLPD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPLPD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPLPD.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPLPD.Location = new System.Drawing.Point(12, 135);
            this.dgvPLPD.Name = "dgvPLPD";
            this.dgvPLPD.RowHeadersVisible = false;
            this.dgvPLPD.Size = new System.Drawing.Size(768, 50);
            this.dgvPLPD.TabIndex = 29;
            this.dgvPLPD.Visible = false;
            this.dgvPLPD.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPLPD_CellValueChanged);
            // 
            // lblPLRD
            // 
            this.lblPLRD.AutoSize = true;
            this.lblPLRD.BackColor = System.Drawing.Color.Transparent;
            this.lblPLRD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPLRD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPLRD.Location = new System.Drawing.Point(12, 202);
            this.lblPLRD.Name = "lblPLRD";
            this.lblPLRD.Size = new System.Drawing.Size(160, 16);
            this.lblPLRD.TabIndex = 32;
            this.lblPLRD.Text = "PLRD - Run Play Data";
            this.lblPLRD.Visible = false;
            // 
            // dgvPLRD
            // 
            this.dgvPLRD.AllowUserToAddRows = false;
            this.dgvPLRD.AllowUserToDeleteRows = false;
            this.dgvPLRD.AllowUserToResizeColumns = false;
            this.dgvPLRD.AllowUserToResizeRows = false;
            this.dgvPLRD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPLRD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPLRD.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvPLRD.Location = new System.Drawing.Point(12, 218);
            this.dgvPLRD.Name = "dgvPLRD";
            this.dgvPLRD.RowHeadersVisible = false;
            this.dgvPLRD.Size = new System.Drawing.Size(768, 50);
            this.dgvPLRD.TabIndex = 31;
            this.dgvPLRD.Visible = false;
            this.dgvPLRD.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPLRD_CellValueChanged);
            // 
            // lblSRFT
            // 
            this.lblSRFT.AutoSize = true;
            this.lblSRFT.BackColor = System.Drawing.Color.Transparent;
            this.lblSRFT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSRFT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSRFT.Location = new System.Drawing.Point(12, 285);
            this.lblSRFT.Name = "lblSRFT";
            this.lblSRFT.Size = new System.Drawing.Size(207, 16);
            this.lblSRFT.TabIndex = 34;
            this.lblSRFT.Text = "SRFT - Defensive Front Data";
            this.lblSRFT.Visible = false;
            // 
            // dgvSRFT
            // 
            this.dgvSRFT.AllowUserToAddRows = false;
            this.dgvSRFT.AllowUserToDeleteRows = false;
            this.dgvSRFT.AllowUserToResizeColumns = false;
            this.dgvSRFT.AllowUserToResizeRows = false;
            this.dgvSRFT.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSRFT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSRFT.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvSRFT.Location = new System.Drawing.Point(12, 301);
            this.dgvSRFT.Name = "dgvSRFT";
            this.dgvSRFT.RowHeadersVisible = false;
            this.dgvSRFT.Size = new System.Drawing.Size(768, 260);
            this.dgvSRFT.TabIndex = 33;
            this.dgvSRFT.Visible = false;
            this.dgvSRFT.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSRFT_CellMouseDown);
            this.dgvSRFT.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSRFT_CellValueChanged);
            // 
            // cmsEditSRFT
            // 
            this.cmsEditSRFT.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsEditSRFT_Insert_Above,
            this.cmsEditSRFT_Insert_Below,
            this.cmsEditSRFT_Delete});
            this.cmsEditSRFT.Name = "cmsEditSRFT";
            this.cmsEditSRFT.Size = new System.Drawing.Size(141, 70);
            // 
            // cmsEditSRFT_Insert_Above
            // 
            this.cmsEditSRFT_Insert_Above.Name = "cmsEditSRFT_Insert_Above";
            this.cmsEditSRFT_Insert_Above.Size = new System.Drawing.Size(140, 22);
            this.cmsEditSRFT_Insert_Above.Text = "Insert Above";
            this.cmsEditSRFT_Insert_Above.Click += new System.EventHandler(this.cmsEditSRFT_Insert_Above_Click);
            // 
            // cmsEditSRFT_Insert_Below
            // 
            this.cmsEditSRFT_Insert_Below.Name = "cmsEditSRFT_Insert_Below";
            this.cmsEditSRFT_Insert_Below.Size = new System.Drawing.Size(140, 22);
            this.cmsEditSRFT_Insert_Below.Text = "Insert Below";
            this.cmsEditSRFT_Insert_Below.Click += new System.EventHandler(this.cmsEditSRFT_Insert_Below_Click);
            // 
            // cmsEditSRFT_Delete
            // 
            this.cmsEditSRFT_Delete.Name = "cmsEditSRFT_Delete";
            this.cmsEditSRFT_Delete.Size = new System.Drawing.Size(140, 22);
            this.cmsEditSRFT_Delete.Text = "Delete";
            this.cmsEditSRFT_Delete.Click += new System.EventHandler(this.cmsEditSRFT_Delete_Click);
            // 
            // frmEditPlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Playbook_Editor.Properties.Resources.chalkboard_2;
            this.ClientSize = new System.Drawing.Size(792, 607);
            this.Controls.Add(this.lblSRFT);
            this.Controls.Add(this.dgvSRFT);
            this.Controls.Add(this.lblPLRD);
            this.Controls.Add(this.dgvPLRD);
            this.Controls.Add(this.lblPLPD);
            this.Controls.Add(this.dgvPLPD);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblPBPL);
            this.Controls.Add(this.dgvPBPL);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.mnuEditPlay);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEditPlay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Play & Formation Tables";
            this.Load += new System.EventHandler(this.frmEditPlay_Load);
            this.mnuEditPlay.ResumeLayout(false);
            this.mnuEditPlay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPBPL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPLPD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPLRD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSRFT)).EndInit();
            this.cmsEditSRFT.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.MenuStrip mnuEditPlay;
        private System.Windows.Forms.DataGridView dgvPBPL;
        private System.Windows.Forms.Label lblPBPL;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblPLPD;
        private System.Windows.Forms.DataGridView dgvPLPD;
        private System.Windows.Forms.Label lblPLRD;
        private System.Windows.Forms.DataGridView dgvPLRD;
        private System.Windows.Forms.Label lblSRFT;
        private System.Windows.Forms.DataGridView dgvSRFT;
        private System.Windows.Forms.ContextMenuStrip cmsEditSRFT;
        private System.Windows.Forms.ToolStripMenuItem cmsEditSRFT_Insert_Above;
        private System.Windows.Forms.ToolStripMenuItem cmsEditSRFT_Delete;
        private System.Windows.Forms.ToolStripMenuItem mnuEditPlay_Options;
        private System.Windows.Forms.ToolStripMenuItem mnuEditPlay_Options_PLPD;
        private System.Windows.Forms.ToolStripMenuItem mnuEditPlay_Options_PLPD_Add;
        private System.Windows.Forms.ToolStripMenuItem mnuEditPlay_Options_PLPD_Remove;
        private System.Windows.Forms.ToolStripMenuItem mnuEditPlay_Options_PLRD;
        private System.Windows.Forms.ToolStripMenuItem mnuEditPlay_Options_PLRD_Add;
        private System.Windows.Forms.ToolStripMenuItem mnuEditPlay_Options_PLRD_Remove;
        private System.Windows.Forms.ToolStripMenuItem cmsEditSRFT_Insert_Below;
    }
}