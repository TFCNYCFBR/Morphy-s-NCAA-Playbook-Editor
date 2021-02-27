namespace Playbook_Editor
{
    partial class frmEditPSAL
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditPSAL));
            this.cbxPLRR = new System.Windows.Forms.ComboBox();
            this.cbxPSAL = new System.Windows.Forms.ComboBox();
            this.lblPLRR = new System.Windows.Forms.Label();
            this.lblPSAL = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.dgvPSAL = new System.Windows.Forms.DataGridView();
            this.chbEdit = new System.Windows.Forms.CheckBox();
            this.cmsEditPSAL = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsEditPSAL_Insert_Above = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEditPSAL_Insert_Below = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEditPSAL_Delete = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPSAL)).BeginInit();
            this.cmsEditPSAL.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxPLRR
            // 
            this.cbxPLRR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPLRR.FormattingEnabled = true;
            this.cbxPLRR.Location = new System.Drawing.Point(114, 12);
            this.cbxPLRR.Name = "cbxPLRR";
            this.cbxPLRR.Size = new System.Drawing.Size(210, 21);
            this.cbxPLRR.TabIndex = 0;
            this.cbxPLRR.SelectionChangeCommitted += new System.EventHandler(this.cbxPLRR_SelectedIndexChanged);
            // 
            // cbxPSAL
            // 
            this.cbxPSAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPSAL.FormattingEnabled = true;
            this.cbxPSAL.Location = new System.Drawing.Point(114, 39);
            this.cbxPSAL.Name = "cbxPSAL";
            this.cbxPSAL.Size = new System.Drawing.Size(210, 21);
            this.cbxPSAL.TabIndex = 1;
            this.cbxPSAL.SelectionChangeCommitted += new System.EventHandler(this.cbxPSAL_SelectedIndexChanged);
            // 
            // lblPLRR
            // 
            this.lblPLRR.AutoSize = true;
            this.lblPLRR.BackColor = System.Drawing.Color.Transparent;
            this.lblPLRR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPLRR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPLRR.Location = new System.Drawing.Point(9, 17);
            this.lblPLRR.Name = "lblPLRR";
            this.lblPLRR.Size = new System.Drawing.Size(99, 13);
            this.lblPLRR.TabIndex = 18;
            this.lblPLRR.Text = "PSAL Type (PLRR)";
            // 
            // lblPSAL
            // 
            this.lblPSAL.AutoSize = true;
            this.lblPSAL.BackColor = System.Drawing.Color.Transparent;
            this.lblPSAL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPSAL.Location = new System.Drawing.Point(60, 43);
            this.lblPSAL.Name = "lblPSAL";
            this.lblPSAL.Size = new System.Drawing.Size(48, 13);
            this.lblPSAL.TabIndex = 19;
            this.lblPSAL.Text = "PSAL ID";
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.Red;
            this.btnReset.Location = new System.Drawing.Point(330, 12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(47, 48);
            this.btnReset.TabIndex = 20;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // dgvPSAL
            // 
            this.dgvPSAL.AllowUserToAddRows = false;
            this.dgvPSAL.AllowUserToDeleteRows = false;
            this.dgvPSAL.AllowUserToResizeColumns = false;
            this.dgvPSAL.AllowUserToResizeRows = false;
            this.dgvPSAL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPSAL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPSAL.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPSAL.Location = new System.Drawing.Point(12, 72);
            this.dgvPSAL.Name = "dgvPSAL";
            this.dgvPSAL.ReadOnly = true;
            this.dgvPSAL.RowHeadersVisible = false;
            this.dgvPSAL.Size = new System.Drawing.Size(365, 225);
            this.dgvPSAL.TabIndex = 21;
            this.dgvPSAL.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPSAL_CellMouseDown);
            // 
            // chbEdit
            // 
            this.chbEdit.AutoSize = true;
            this.chbEdit.BackColor = System.Drawing.Color.Transparent;
            this.chbEdit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbEdit.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.chbEdit.Location = new System.Drawing.Point(8, 42);
            this.chbEdit.Name = "chbEdit";
            this.chbEdit.Size = new System.Drawing.Size(48, 17);
            this.chbEdit.TabIndex = 22;
            this.chbEdit.Text = "Edit";
            this.chbEdit.UseVisualStyleBackColor = false;
            this.chbEdit.CheckedChanged += new System.EventHandler(this.chbEdit_CheckedChanged);
            // 
            // cmsEditPSAL
            // 
            this.cmsEditPSAL.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsEditPSAL_Insert_Above,
            this.cmsEditPSAL_Insert_Below,
            this.cmsEditPSAL_Delete});
            this.cmsEditPSAL.Name = "cmsEditPSAL";
            this.cmsEditPSAL.Size = new System.Drawing.Size(167, 70);
            // 
            // cmsEditPSAL_Insert_Above
            // 
            this.cmsEditPSAL_Insert_Above.Name = "cmsEditPSAL_Insert_Above";
            this.cmsEditPSAL_Insert_Above.Size = new System.Drawing.Size(166, 22);
            this.cmsEditPSAL_Insert_Above.Text = "Insert Step Above";
            this.cmsEditPSAL_Insert_Above.Click += new System.EventHandler(this.cmsEditPSAL_Insert_Above_Click);
            // 
            // cmsEditPSAL_Insert_Below
            // 
            this.cmsEditPSAL_Insert_Below.Name = "cmsEditPSAL_Insert_Below";
            this.cmsEditPSAL_Insert_Below.Size = new System.Drawing.Size(166, 22);
            this.cmsEditPSAL_Insert_Below.Text = "Insert Step Below";
            this.cmsEditPSAL_Insert_Below.Click += new System.EventHandler(this.cmsEditPSAL_Insert_Below_Click);
            // 
            // cmsEditPSAL_Delete
            // 
            this.cmsEditPSAL_Delete.Name = "cmsEditPSAL_Delete";
            this.cmsEditPSAL_Delete.Size = new System.Drawing.Size(166, 22);
            this.cmsEditPSAL_Delete.Text = "Delete Step";
            this.cmsEditPSAL_Delete.Click += new System.EventHandler(this.cmsEditPSAL_Delete_Click);
            // 
            // frmEditPSAL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::Playbook_Editor.Properties.Resources.chalkboard_2;
            this.ClientSize = new System.Drawing.Size(389, 309);
            this.Controls.Add(this.chbEdit);
            this.Controls.Add(this.dgvPSAL);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblPSAL);
            this.Controls.Add(this.lblPLRR);
            this.Controls.Add(this.cbxPSAL);
            this.Controls.Add(this.cbxPLRR);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEditPSAL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmPLRR";
            this.Deactivate += new System.EventHandler(this.frmPLRR_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPLRR_FormClosing);
            this.Load += new System.EventHandler(this.frmPLRR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPSAL)).EndInit();
            this.cmsEditPSAL.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblPLRR;
        private System.Windows.Forms.Label lblPSAL;
        private System.Windows.Forms.Button btnReset;
        public System.Windows.Forms.ComboBox cbxPLRR;
        public System.Windows.Forms.ComboBox cbxPSAL;
        private System.Windows.Forms.DataGridView dgvPSAL;
        private System.Windows.Forms.CheckBox chbEdit;
        private System.Windows.Forms.ContextMenuStrip cmsEditPSAL;
        private System.Windows.Forms.ToolStripMenuItem cmsEditPSAL_Insert_Above;
        private System.Windows.Forms.ToolStripMenuItem cmsEditPSAL_Delete;
        private System.Windows.Forms.ToolStripMenuItem cmsEditPSAL_Insert_Below;
    }
}