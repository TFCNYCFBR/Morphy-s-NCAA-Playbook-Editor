namespace Playbook_Editor
{
    partial class frmCreatePSAL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreatePSAL));
            this.cbxPLRR = new System.Windows.Forms.ComboBox();
            this.cbxPSAL = new System.Windows.Forms.ComboBox();
            this.lblPLRR = new System.Windows.Forms.Label();
            this.lblPSAL = new System.Windows.Forms.Label();
            this.dgvPSAL = new System.Windows.Forms.DataGridView();
            this.chbAssignPSAL = new System.Windows.Forms.CheckBox();
            this.cmsEditPSAL = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsEditPSAL_Insert_Above = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEditPSAL_Insert_Below = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEditPSAL_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCreatePSAL = new System.Windows.Forms.Button();
            this.btnDiscardPSAL = new System.Windows.Forms.Button();
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
            this.cbxPLRR.Size = new System.Drawing.Size(263, 21);
            this.cbxPLRR.TabIndex = 0;
            // 
            // cbxPSAL
            // 
            this.cbxPSAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPSAL.FormattingEnabled = true;
            this.cbxPSAL.IntegralHeight = false;
            this.cbxPSAL.Location = new System.Drawing.Point(114, 39);
            this.cbxPSAL.MaxDropDownItems = 10;
            this.cbxPSAL.Name = "cbxPSAL";
            this.cbxPSAL.Size = new System.Drawing.Size(263, 21);
            this.cbxPSAL.TabIndex = 1;
            this.cbxPSAL.SelectionChangeCommitted += new System.EventHandler(this.cbxPSAL_SelectedIndexChanged);
            // 
            // lblPLRR
            // 
            this.lblPLRR.AutoSize = true;
            this.lblPLRR.BackColor = System.Drawing.Color.Transparent;
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
            this.lblPSAL.Location = new System.Drawing.Point(9, 42);
            this.lblPSAL.Name = "lblPSAL";
            this.lblPSAL.Size = new System.Drawing.Size(99, 13);
            this.lblPSAL.TabIndex = 19;
            this.lblPSAL.Text = "Available PSAL IDs";
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
            this.dgvPSAL.RowHeadersVisible = false;
            this.dgvPSAL.Size = new System.Drawing.Size(365, 225);
            this.dgvPSAL.TabIndex = 21;
            this.dgvPSAL.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPSAL_CellMouseDown);
            // 
            // chbAssignPSAL
            // 
            this.chbAssignPSAL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbAssignPSAL.AutoSize = true;
            this.chbAssignPSAL.BackColor = System.Drawing.Color.Transparent;
            this.chbAssignPSAL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbAssignPSAL.Checked = true;
            this.chbAssignPSAL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbAssignPSAL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chbAssignPSAL.Location = new System.Drawing.Point(12, 309);
            this.chbAssignPSAL.Name = "chbAssignPSAL";
            this.chbAssignPSAL.Size = new System.Drawing.Size(170, 17);
            this.chbAssignPSAL.TabIndex = 22;
            this.chbAssignPSAL.Text = "Assign PSAL to Selected poso";
            this.chbAssignPSAL.UseVisualStyleBackColor = false;
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
            // btnCreatePSAL
            // 
            this.btnCreatePSAL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreatePSAL.BackColor = System.Drawing.SystemColors.Control;
            this.btnCreatePSAL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreatePSAL.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnCreatePSAL.Location = new System.Drawing.Point(221, 305);
            this.btnCreatePSAL.Name = "btnCreatePSAL";
            this.btnCreatePSAL.Size = new System.Drawing.Size(75, 23);
            this.btnCreatePSAL.TabIndex = 23;
            this.btnCreatePSAL.Text = "Create";
            this.btnCreatePSAL.UseVisualStyleBackColor = false;
            this.btnCreatePSAL.Click += new System.EventHandler(this.btnCreatePSAL_Click);
            // 
            // btnDiscardPSAL
            // 
            this.btnDiscardPSAL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDiscardPSAL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDiscardPSAL.ForeColor = System.Drawing.Color.Red;
            this.btnDiscardPSAL.Location = new System.Drawing.Point(302, 305);
            this.btnDiscardPSAL.Name = "btnDiscardPSAL";
            this.btnDiscardPSAL.Size = new System.Drawing.Size(75, 23);
            this.btnDiscardPSAL.TabIndex = 24;
            this.btnDiscardPSAL.Text = "Discard";
            this.btnDiscardPSAL.UseVisualStyleBackColor = true;
            this.btnDiscardPSAL.Click += new System.EventHandler(this.btnDiscardPSAL_Click);
            // 
            // frmCreatePSAL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Playbook_Editor.Properties.Resources.chalkboard_2;
            this.ClientSize = new System.Drawing.Size(389, 338);
            this.Controls.Add(this.btnDiscardPSAL);
            this.Controls.Add(this.btnCreatePSAL);
            this.Controls.Add(this.chbAssignPSAL);
            this.Controls.Add(this.dgvPSAL);
            this.Controls.Add(this.lblPSAL);
            this.Controls.Add(this.lblPLRR);
            this.Controls.Add(this.cbxPSAL);
            this.Controls.Add(this.cbxPLRR);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCreatePSAL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmPLRR";
            this.Load += new System.EventHandler(this.frmPLRR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPSAL)).EndInit();
            this.cmsEditPSAL.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblPLRR;
        private System.Windows.Forms.Label lblPSAL;
        public System.Windows.Forms.ComboBox cbxPLRR;
        public System.Windows.Forms.ComboBox cbxPSAL;
        private System.Windows.Forms.DataGridView dgvPSAL;
        private System.Windows.Forms.CheckBox chbAssignPSAL;
        private System.Windows.Forms.ContextMenuStrip cmsEditPSAL;
        private System.Windows.Forms.ToolStripMenuItem cmsEditPSAL_Insert_Above;
        private System.Windows.Forms.ToolStripMenuItem cmsEditPSAL_Delete;
        private System.Windows.Forms.Button btnCreatePSAL;
        private System.Windows.Forms.Button btnDiscardPSAL;
        private System.Windows.Forms.ToolStripMenuItem cmsEditPSAL_Insert_Below;
    }
}