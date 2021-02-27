namespace Playbook_Editor
{
    partial class frmEditFormation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditFormation));
            this.btn_Close = new System.Windows.Forms.Button();
            this.mnuPBPL = new System.Windows.Forms.MenuStrip();
            this.lblSETP = new System.Windows.Forms.Label();
            this.dgvSETP = new System.Windows.Forms.DataGridView();
            this.lblSETG = new System.Windows.Forms.Label();
            this.dgvSETG = new System.Windows.Forms.DataGridView();
            this.btnResetSETP = new System.Windows.Forms.Button();
            this.btnResetSETG = new System.Windows.Forms.Button();
            this.lblSETL = new System.Windows.Forms.Label();
            this.dgvSETL = new System.Windows.Forms.DataGridView();
            this.btnResetSETL = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSETP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSETG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSETL)).BeginInit();
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
            this.btn_Close.Location = new System.Drawing.Point(687, 1);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(22, 22);
            this.btn_Close.TabIndex = 16;
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            this.btn_Close.MouseEnter += new System.EventHandler(this.btn_Close_MouseEnter);
            this.btn_Close.MouseLeave += new System.EventHandler(this.btn_Close_MouseLeave);
            // 
            // mnuPBPL
            // 
            this.mnuPBPL.BackgroundImage = global::Playbook_Editor.Properties.Resources.Football_toolbar;
            this.mnuPBPL.Location = new System.Drawing.Point(0, 0);
            this.mnuPBPL.Name = "mnuPBPL";
            this.mnuPBPL.Size = new System.Drawing.Size(711, 24);
            this.mnuPBPL.TabIndex = 17;
            this.mnuPBPL.Text = "mnuPlaybook";
            // 
            // lblSETP
            // 
            this.lblSETP.AutoSize = true;
            this.lblSETP.BackColor = System.Drawing.Color.Transparent;
            this.lblSETP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSETP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSETP.Location = new System.Drawing.Point(12, 114);
            this.lblSETP.Name = "lblSETP";
            this.lblSETP.Size = new System.Drawing.Size(198, 16);
            this.lblSETP.TabIndex = 29;
            this.lblSETP.Text = "SETP - Formation Positions";
            // 
            // dgvSETP
            // 
            this.dgvSETP.AllowUserToAddRows = false;
            this.dgvSETP.AllowUserToDeleteRows = false;
            this.dgvSETP.AllowUserToResizeColumns = false;
            this.dgvSETP.AllowUserToResizeRows = false;
            this.dgvSETP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSETP.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSETP.Location = new System.Drawing.Point(12, 130);
            this.dgvSETP.Name = "dgvSETP";
            this.dgvSETP.RowHeadersVisible = false;
            this.dgvSETP.Size = new System.Drawing.Size(639, 265);
            this.dgvSETP.TabIndex = 28;
            this.dgvSETP.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSETP_CellClick);
            this.dgvSETP.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSETP_CellClick);
            this.dgvSETP.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSETP_CellValueChanged);
            // 
            // lblSETG
            // 
            this.lblSETG.AutoSize = true;
            this.lblSETG.BackColor = System.Drawing.Color.Transparent;
            this.lblSETG.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSETG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSETG.Location = new System.Drawing.Point(12, 407);
            this.lblSETG.Name = "lblSETG";
            this.lblSETG.Size = new System.Drawing.Size(203, 16);
            this.lblSETG.TabIndex = 31;
            this.lblSETG.Text = "SETG - Formation Alignment";
            // 
            // dgvSETG
            // 
            this.dgvSETG.AllowUserToAddRows = false;
            this.dgvSETG.AllowUserToDeleteRows = false;
            this.dgvSETG.AllowUserToResizeColumns = false;
            this.dgvSETG.AllowUserToResizeRows = false;
            this.dgvSETG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSETG.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSETG.Location = new System.Drawing.Point(12, 423);
            this.dgvSETG.Name = "dgvSETG";
            this.dgvSETG.RowHeadersVisible = false;
            this.dgvSETG.Size = new System.Drawing.Size(639, 265);
            this.dgvSETG.TabIndex = 30;
            this.dgvSETG.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSETG_CellClick);
            this.dgvSETG.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSETG_CellClick);
            this.dgvSETG.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSETG_CellValueChanged);
            // 
            // btnResetSETP
            // 
            this.btnResetSETP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetSETP.ForeColor = System.Drawing.Color.Red;
            this.btnResetSETP.Location = new System.Drawing.Point(237, 110);
            this.btnResetSETP.Name = "btnResetSETP";
            this.btnResetSETP.Size = new System.Drawing.Size(79, 20);
            this.btnResetSETP.TabIndex = 32;
            this.btnResetSETP.Text = "Reset";
            this.btnResetSETP.UseVisualStyleBackColor = true;
            this.btnResetSETP.Click += new System.EventHandler(this.btnResetSETP_Click);
            // 
            // btnResetSETG
            // 
            this.btnResetSETG.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetSETG.ForeColor = System.Drawing.Color.Red;
            this.btnResetSETG.Location = new System.Drawing.Point(237, 405);
            this.btnResetSETG.Name = "btnResetSETG";
            this.btnResetSETG.Size = new System.Drawing.Size(79, 20);
            this.btnResetSETG.TabIndex = 33;
            this.btnResetSETG.Text = "Reset";
            this.btnResetSETG.UseVisualStyleBackColor = true;
            this.btnResetSETG.Click += new System.EventHandler(this.btnResetSETG_Click);
            // 
            // lblSETL
            // 
            this.lblSETL.AutoSize = true;
            this.lblSETL.BackColor = System.Drawing.Color.Transparent;
            this.lblSETL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSETL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSETL.Location = new System.Drawing.Point(12, 36);
            this.lblSETL.Name = "lblSETL";
            this.lblSETL.Size = new System.Drawing.Size(165, 16);
            this.lblSETL.TabIndex = 35;
            this.lblSETL.Text = "SETL - Formation Data";
            // 
            // dgvSETL
            // 
            this.dgvSETL.AllowUserToAddRows = false;
            this.dgvSETL.AllowUserToDeleteRows = false;
            this.dgvSETL.AllowUserToResizeColumns = false;
            this.dgvSETL.AllowUserToResizeRows = false;
            this.dgvSETL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSETL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSETL.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSETL.Location = new System.Drawing.Point(12, 52);
            this.dgvSETL.Name = "dgvSETL";
            this.dgvSETL.RowHeadersVisible = false;
            this.dgvSETL.Size = new System.Drawing.Size(639, 50);
            this.dgvSETL.TabIndex = 34;
            this.dgvSETL.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSETL_CellValueChanged);
            // 
            // btnResetSETL
            // 
            this.btnResetSETL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetSETL.ForeColor = System.Drawing.Color.Red;
            this.btnResetSETL.Location = new System.Drawing.Point(237, 32);
            this.btnResetSETL.Name = "btnResetSETL";
            this.btnResetSETL.Size = new System.Drawing.Size(79, 20);
            this.btnResetSETL.TabIndex = 36;
            this.btnResetSETL.Text = "Reset";
            this.btnResetSETL.UseVisualStyleBackColor = true;
            this.btnResetSETL.Click += new System.EventHandler(this.btnResetSETL_Click);
            // 
            // frmEditFormation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Playbook_Editor.Properties.Resources.chalkboard_2;
            this.ClientSize = new System.Drawing.Size(711, 702);
            this.Controls.Add(this.btnResetSETL);
            this.Controls.Add(this.lblSETL);
            this.Controls.Add(this.dgvSETL);
            this.Controls.Add(this.btnResetSETG);
            this.Controls.Add(this.btnResetSETP);
            this.Controls.Add(this.lblSETG);
            this.Controls.Add(this.dgvSETG);
            this.Controls.Add(this.lblSETP);
            this.Controls.Add(this.dgvSETP);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.mnuPBPL);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEditFormation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEditFormation_FormClosed);
            this.Load += new System.EventHandler(this.frmEditFormation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSETP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSETG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSETL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.MenuStrip mnuPBPL;
        private System.Windows.Forms.Label lblSETP;
        private System.Windows.Forms.Label lblSETG;
        private System.Windows.Forms.Button btnResetSETP;
        private System.Windows.Forms.Button btnResetSETG;
        public System.Windows.Forms.DataGridView dgvSETP;
        public System.Windows.Forms.DataGridView dgvSETG;
        private System.Windows.Forms.Label lblSETL;
        private System.Windows.Forms.DataGridView dgvSETL;
        private System.Windows.Forms.Button btnResetSETL;
    }
}