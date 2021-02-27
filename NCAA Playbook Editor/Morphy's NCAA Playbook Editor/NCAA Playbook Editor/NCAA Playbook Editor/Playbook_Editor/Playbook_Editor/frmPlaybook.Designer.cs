namespace Playbook_Editor
{
    partial class frmPlaybook
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlaybook));
            this.mnuPlaybook = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFile_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBreak_Playbook = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools_PSALVisualizer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions_Playart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions_Playart_PSAL = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions_Playart_ARTL = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOptions_Field = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions_Field_Transparent = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions_Field_Dark = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions_Field_Default = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions_Field_Brown = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions_Field_Green = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions_Field_Offense = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView_Morphy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView_PBModding = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Max = new System.Windows.Forms.Button();
            this.btn_Min = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cbxFormations = new System.Windows.Forms.ComboBox();
            this.lblFormations = new System.Windows.Forms.Label();
            this.lblSubFormations = new System.Windows.Forms.Label();
            this.cbxSubFormations = new System.Windows.Forms.ComboBox();
            this.lblPlays = new System.Windows.Forms.Label();
            this.cbxPlays = new System.Windows.Forms.ComboBox();
            this.picPlay = new System.Windows.Forms.PictureBox();
            this.dgvPLYS = new System.Windows.Forms.DataGridView();
            this.lblAlignments = new System.Windows.Forms.Label();
            this.cbxAlignments = new System.Windows.Forms.ComboBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cmsPlayOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editPlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFileName = new System.Windows.Forms.Label();
            this.btnEditPlayData = new System.Windows.Forms.Button();
            this.lblPlayerAssignments = new System.Windows.Forms.Label();
            this.btnEditFormation = new System.Windows.Forms.Button();
            this.chbFlipPlay = new System.Windows.Forms.CheckBox();
            this.cmsFormationOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmiEditFormation = new System.Windows.Forms.ToolStripMenuItem();
            this.picARTL = new System.Windows.Forms.PictureBox();
            this.cmsARTL = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsARTL_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEditPSAL = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsEditPSAL_FromList = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEditPSAL_New = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEditPSAL_Seperator = new System.Windows.Forms.ToolStripSeparator();
            this.cmsEditPSAL_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEditPSAL_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.lblOpenPlaybook = new Playbook_Editor.CustomLabel();
            this.mnuPlaybook.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPLYS)).BeginInit();
            this.cmsPlayOptions.SuspendLayout();
            this.cmsFormationOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picARTL)).BeginInit();
            this.cmsARTL.SuspendLayout();
            this.cmsEditPSAL.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuPlaybook
            // 
            this.mnuPlaybook.BackgroundImage = global::Playbook_Editor.Properties.Resources.Football_toolbar;
            this.mnuPlaybook.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools,
            this.mnuOptions,
            this.mnuView});
            this.mnuPlaybook.Location = new System.Drawing.Point(0, 0);
            this.mnuPlaybook.Name = "mnuPlaybook";
            this.mnuPlaybook.Size = new System.Drawing.Size(966, 24);
            this.mnuPlaybook.TabIndex = 11;
            this.mnuPlaybook.Text = "mnuPlaybook";
            this.mnuPlaybook.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuPlaybook_ItemClicked);
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile_Open,
            this.toolStripSeparator1,
            this.mnuFile_Save,
            this.mnuBreak_Playbook,
            this.mnuFile_Exit});
            this.mnuFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuFile_Open
            // 
            this.mnuFile_Open.Name = "mnuFile_Open";
            this.mnuFile_Open.Size = new System.Drawing.Size(180, 22);
            this.mnuFile_Open.Text = "Open Playbook";
            this.mnuFile_Open.Click += new System.EventHandler(this.mnuFile_Open_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuFile_Save
            // 
            this.mnuFile_Save.Enabled = false;
            this.mnuFile_Save.Name = "mnuFile_Save";
            this.mnuFile_Save.Size = new System.Drawing.Size(180, 22);
            this.mnuFile_Save.Text = "Save Playbook";
            this.mnuFile_Save.Click += new System.EventHandler(this.mnuFile_Save_Click);
            // 
            // mnuBreak_Playbook
            // 
            this.mnuBreak_Playbook.Name = "mnuBreak_Playbook";
            this.mnuBreak_Playbook.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuFile_Exit
            // 
            this.mnuFile_Exit.Name = "mnuFile_Exit";
            this.mnuFile_Exit.Size = new System.Drawing.Size(180, 22);
            this.mnuFile_Exit.Text = "Exit";
            this.mnuFile_Exit.Click += new System.EventHandler(this.mnuFile_Exit_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTools_PSALVisualizer});
            this.mnuTools.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(46, 20);
            this.mnuTools.Text = "Tools";
            // 
            // mnuTools_PSALVisualizer
            // 
            this.mnuTools_PSALVisualizer.Name = "mnuTools_PSALVisualizer";
            this.mnuTools_PSALVisualizer.Size = new System.Drawing.Size(180, 22);
            this.mnuTools_PSALVisualizer.Text = "PSAL Visualizer";
            this.mnuTools_PSALVisualizer.Click += new System.EventHandler(this.mnuTools_PSALVisualizer_Click);
            // 
            // mnuOptions
            // 
            this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptions_Playart,
            this.toolStripSeparator2,
            this.mnuOptions_Field});
            this.mnuOptions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(61, 20);
            this.mnuOptions.Text = "Options";
            // 
            // mnuOptions_Playart
            // 
            this.mnuOptions_Playart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptions_Playart_PSAL,
            this.mnuOptions_Playart_ARTL});
            this.mnuOptions_Playart.Name = "mnuOptions_Playart";
            this.mnuOptions_Playart.Size = new System.Drawing.Size(180, 22);
            this.mnuOptions_Playart.Text = "Playart";
            // 
            // mnuOptions_Playart_PSAL
            // 
            this.mnuOptions_Playart_PSAL.Name = "mnuOptions_Playart_PSAL";
            this.mnuOptions_Playart_PSAL.Size = new System.Drawing.Size(180, 22);
            this.mnuOptions_Playart_PSAL.Text = "PSAL";
            this.mnuOptions_Playart_PSAL.Click += new System.EventHandler(this.mnuOptions_Playart_PSAL_Click);
            // 
            // mnuOptions_Playart_ARTL
            // 
            this.mnuOptions_Playart_ARTL.Checked = true;
            this.mnuOptions_Playart_ARTL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuOptions_Playart_ARTL.Name = "mnuOptions_Playart_ARTL";
            this.mnuOptions_Playart_ARTL.Size = new System.Drawing.Size(180, 22);
            this.mnuOptions_Playart_ARTL.Text = "ARTL";
            this.mnuOptions_Playart_ARTL.Click += new System.EventHandler(this.mnuOptions_Playart_ARTL_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuOptions_Field
            // 
            this.mnuOptions_Field.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptions_Field_Transparent,
            this.mnuOptions_Field_Dark,
            this.mnuOptions_Field_Default,
            this.mnuOptions_Field_Brown,
            this.mnuOptions_Field_Green,
            this.mnuOptions_Field_Offense});
            this.mnuOptions_Field.Name = "mnuOptions_Field";
            this.mnuOptions_Field.Size = new System.Drawing.Size(180, 22);
            this.mnuOptions_Field.Text = "Field";
            // 
            // mnuOptions_Field_Transparent
            // 
            this.mnuOptions_Field_Transparent.Checked = true;
            this.mnuOptions_Field_Transparent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuOptions_Field_Transparent.Name = "mnuOptions_Field_Transparent";
            this.mnuOptions_Field_Transparent.Size = new System.Drawing.Size(180, 22);
            this.mnuOptions_Field_Transparent.Text = "Transparent";
            this.mnuOptions_Field_Transparent.Click += new System.EventHandler(this.mnuOptions_Field_Transparent_Click);
            // 
            // mnuOptions_Field_Dark
            // 
            this.mnuOptions_Field_Dark.Name = "mnuOptions_Field_Dark";
            this.mnuOptions_Field_Dark.Size = new System.Drawing.Size(180, 22);
            this.mnuOptions_Field_Dark.Text = "Dark (with Hole #)";
            this.mnuOptions_Field_Dark.Click += new System.EventHandler(this.mnuOptions_Field_Dark_Click);
            // 
            // mnuOptions_Field_Default
            // 
            this.mnuOptions_Field_Default.Name = "mnuOptions_Field_Default";
            this.mnuOptions_Field_Default.Size = new System.Drawing.Size(180, 22);
            this.mnuOptions_Field_Default.Text = "Default";
            this.mnuOptions_Field_Default.Click += new System.EventHandler(this.mnuOptions_Field_Default_Click);
            // 
            // mnuOptions_Field_Brown
            // 
            this.mnuOptions_Field_Brown.Name = "mnuOptions_Field_Brown";
            this.mnuOptions_Field_Brown.Size = new System.Drawing.Size(180, 22);
            this.mnuOptions_Field_Brown.Text = "Brown";
            this.mnuOptions_Field_Brown.Click += new System.EventHandler(this.mnuOptions_Field_Brown_Click);
            // 
            // mnuOptions_Field_Green
            // 
            this.mnuOptions_Field_Green.Name = "mnuOptions_Field_Green";
            this.mnuOptions_Field_Green.Size = new System.Drawing.Size(180, 22);
            this.mnuOptions_Field_Green.Text = "Green";
            this.mnuOptions_Field_Green.Click += new System.EventHandler(this.mnuOptions_Field_Green_Click);
            // 
            // mnuOptions_Field_Offense
            // 
            this.mnuOptions_Field_Offense.Name = "mnuOptions_Field_Offense";
            this.mnuOptions_Field_Offense.Size = new System.Drawing.Size(180, 22);
            this.mnuOptions_Field_Offense.Text = "Offense";
            this.mnuOptions_Field_Offense.Click += new System.EventHandler(this.mnuOptions_Field_Offense_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuView_Morphy,
            this.mnuView_PBModding});
            this.mnuView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 20);
            this.mnuView.Text = "View";
            this.mnuView.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // mnuView_Morphy
            // 
            this.mnuView_Morphy.Name = "mnuView_Morphy";
            this.mnuView_Morphy.Size = new System.Drawing.Size(189, 22);
            this.mnuView_Morphy.Text = "Morphy\'s Google Doc";
            this.mnuView_Morphy.ToolTipText = "Navigates to Morphy\'s Google Document (list of codes, PSALs, definitions, etc.)";
            this.mnuView_Morphy.Click += new System.EventHandler(this.mnuView_Morphy_Click);
            // 
            // mnuView_PBModding
            // 
            this.mnuView_PBModding.Name = "mnuView_PBModding";
            this.mnuView_PBModding.Size = new System.Drawing.Size(189, 22);
            this.mnuView_PBModding.Text = "# playbook-modding";
            this.mnuView_PBModding.ToolTipText = "Navigates to the Playbook Modding channel in the Madden Modding Community Discord" +
    "";
            this.mnuView_PBModding.Click += new System.EventHandler(this.mnuView_PBModding_Click);
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
            this.btn_Close.Location = new System.Drawing.Point(942, 1);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(22, 22);
            this.btn_Close.TabIndex = 8;
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            this.btn_Close.MouseEnter += new System.EventHandler(this.btn_Close_MouseEnter);
            this.btn_Close.MouseLeave += new System.EventHandler(this.btn_Close_MouseLeave);
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
            this.btn_Max.Location = new System.Drawing.Point(918, 1);
            this.btn_Max.Name = "btn_Max";
            this.btn_Max.Size = new System.Drawing.Size(22, 22);
            this.btn_Max.TabIndex = 9;
            this.btn_Max.UseVisualStyleBackColor = false;
            this.btn_Max.Click += new System.EventHandler(this.btn_Max_Click);
            this.btn_Max.MouseEnter += new System.EventHandler(this.btn_Max_MouseEnter);
            this.btn_Max.MouseLeave += new System.EventHandler(this.btn_Max_MouseLeave);
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
            this.btn_Min.Location = new System.Drawing.Point(894, 1);
            this.btn_Min.Name = "btn_Min";
            this.btn_Min.Size = new System.Drawing.Size(22, 22);
            this.btn_Min.TabIndex = 10;
            this.btn_Min.UseVisualStyleBackColor = false;
            this.btn_Min.Click += new System.EventHandler(this.btn_Min_Click);
            this.btn_Min.MouseEnter += new System.EventHandler(this.btn_Min_MouseEnter);
            this.btn_Min.MouseLeave += new System.EventHandler(this.btn_Min_MouseLeave);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cbxFormations
            // 
            this.cbxFormations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFormations.FormattingEnabled = true;
            this.cbxFormations.Location = new System.Drawing.Point(12, 51);
            this.cbxFormations.Name = "cbxFormations";
            this.cbxFormations.Size = new System.Drawing.Size(140, 21);
            this.cbxFormations.TabIndex = 12;
            this.cbxFormations.Visible = false;
            this.cbxFormations.SelectedIndexChanged += new System.EventHandler(this.selectFormation);
            // 
            // lblFormations
            // 
            this.lblFormations.AutoSize = true;
            this.lblFormations.BackColor = System.Drawing.Color.Transparent;
            this.lblFormations.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblFormations.Location = new System.Drawing.Point(12, 33);
            this.lblFormations.Name = "lblFormations";
            this.lblFormations.Size = new System.Drawing.Size(85, 16);
            this.lblFormations.TabIndex = 13;
            this.lblFormations.Text = "Formations";
            this.lblFormations.Visible = false;
            // 
            // lblSubFormations
            // 
            this.lblSubFormations.AutoSize = true;
            this.lblSubFormations.BackColor = System.Drawing.Color.Transparent;
            this.lblSubFormations.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubFormations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSubFormations.Location = new System.Drawing.Point(12, 80);
            this.lblSubFormations.Name = "lblSubFormations";
            this.lblSubFormations.Size = new System.Drawing.Size(109, 15);
            this.lblSubFormations.TabIndex = 15;
            this.lblSubFormations.Text = "Sub-Formations";
            this.lblSubFormations.Visible = false;
            // 
            // cbxSubFormations
            // 
            this.cbxSubFormations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSubFormations.FormattingEnabled = true;
            this.cbxSubFormations.Location = new System.Drawing.Point(12, 96);
            this.cbxSubFormations.Name = "cbxSubFormations";
            this.cbxSubFormations.Size = new System.Drawing.Size(140, 21);
            this.cbxSubFormations.Sorted = true;
            this.cbxSubFormations.TabIndex = 14;
            this.cbxSubFormations.Visible = false;
            this.cbxSubFormations.SelectedIndexChanged += new System.EventHandler(this.selectSubFormation);
            this.cbxSubFormations.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbxSubFormations_MouseClick);
            this.cbxSubFormations.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cbxSubFormations_MouseDown);
            this.cbxSubFormations.MouseHover += new System.EventHandler(this.cbxSubFormations_MouseHover);
            // 
            // lblPlays
            // 
            this.lblPlays.AutoSize = true;
            this.lblPlays.BackColor = System.Drawing.Color.Transparent;
            this.lblPlays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlays.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPlays.Location = new System.Drawing.Point(12, 125);
            this.lblPlays.Name = "lblPlays";
            this.lblPlays.Size = new System.Drawing.Size(37, 13);
            this.lblPlays.TabIndex = 17;
            this.lblPlays.Text = "Plays";
            this.lblPlays.Visible = false;
            // 
            // cbxPlays
            // 
            this.cbxPlays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPlays.FormattingEnabled = true;
            this.cbxPlays.Location = new System.Drawing.Point(12, 141);
            this.cbxPlays.Name = "cbxPlays";
            this.cbxPlays.Size = new System.Drawing.Size(140, 21);
            this.cbxPlays.Sorted = true;
            this.cbxPlays.TabIndex = 16;
            this.cbxPlays.Visible = false;
            this.cbxPlays.SelectedIndexChanged += new System.EventHandler(this.selectPlay);
            this.cbxPlays.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbxPlays_MouseClick);
            this.cbxPlays.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cbxPlays_MouseDown);
            this.cbxPlays.MouseHover += new System.EventHandler(this.cbxPlays_MouseHover);
            // 
            // picPlay
            // 
            this.picPlay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picPlay.BackColor = System.Drawing.Color.Transparent;
            this.picPlay.Image = global::Playbook_Editor.Properties.Resources.field_blank_lines;
            this.picPlay.Location = new System.Drawing.Point(401, 35);
            this.picPlay.Name = "picPlay";
            this.picPlay.Size = new System.Drawing.Size(533, 600);
            this.picPlay.TabIndex = 18;
            this.picPlay.TabStop = false;
            this.picPlay.Visible = false;
            this.picPlay.Click += new System.EventHandler(this.picPlay_Click);
            // 
            // dgvPLYS
            // 
            this.dgvPLYS.AllowUserToAddRows = false;
            this.dgvPLYS.AllowUserToDeleteRows = false;
            this.dgvPLYS.AllowUserToResizeColumns = false;
            this.dgvPLYS.AllowUserToResizeRows = false;
            this.dgvPLYS.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvPLYS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPLYS.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPLYS.Location = new System.Drawing.Point(12, 208);
            this.dgvPLYS.Name = "dgvPLYS";
            this.dgvPLYS.RowHeadersVisible = false;
            this.dgvPLYS.RowHeadersWidth = 40;
            this.dgvPLYS.Size = new System.Drawing.Size(343, 265);
            this.dgvPLYS.TabIndex = 20;
            this.dgvPLYS.Visible = false;
            this.dgvPLYS.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPLYS_CellEndEdit);
            this.dgvPLYS.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPLYS_CellClick);
            this.dgvPLYS.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPLYS_CellMouseDown);
            this.dgvPLYS.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPLYS_CellValueChanged);
            this.dgvPLYS.SelectionChanged += new System.EventHandler(this.dgvPLYS_SelectionChanged);
            this.dgvPLYS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvPLYS_KeyDown);
            // 
            // lblAlignments
            // 
            this.lblAlignments.AutoSize = true;
            this.lblAlignments.BackColor = System.Drawing.Color.Transparent;
            this.lblAlignments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblAlignments.Location = new System.Drawing.Point(169, 35);
            this.lblAlignments.Name = "lblAlignments";
            this.lblAlignments.Size = new System.Drawing.Size(90, 13);
            this.lblAlignments.TabIndex = 22;
            this.lblAlignments.Text = "Motion/Alignment";
            this.lblAlignments.Visible = false;
            // 
            // cbxAlignments
            // 
            this.cbxAlignments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAlignments.Enabled = false;
            this.cbxAlignments.FormattingEnabled = true;
            this.cbxAlignments.Location = new System.Drawing.Point(172, 51);
            this.cbxAlignments.Name = "cbxAlignments";
            this.cbxAlignments.Size = new System.Drawing.Size(82, 21);
            this.cbxAlignments.Sorted = true;
            this.cbxAlignments.TabIndex = 21;
            this.cbxAlignments.Visible = false;
            this.cbxAlignments.SelectedIndexChanged += new System.EventHandler(this.selectAlignment);
            // 
            // cmsPlayOptions
            // 
            this.cmsPlayOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editPlayToolStripMenuItem});
            this.cmsPlayOptions.Name = "cmsPlayOptions";
            this.cmsPlayOptions.Size = new System.Drawing.Size(255, 26);
            this.cmsPlayOptions.Text = "Play Options";
            // 
            // editPlayToolStripMenuItem
            // 
            this.editPlayToolStripMenuItem.Name = "editPlayToolStripMenuItem";
            this.editPlayToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.editPlayToolStripMenuItem.Text = "Edit Play (Name, Type, vpos, etc)...";
            this.editPlayToolStripMenuItem.Click += new System.EventHandler(this.EditPlayData_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFileName.AutoSize = true;
            this.lblFileName.BackColor = System.Drawing.Color.Transparent;
            this.lblFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblFileName.Location = new System.Drawing.Point(12, 624);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblFileName.Size = new System.Drawing.Size(0, 16);
            this.lblFileName.TabIndex = 24;
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnEditPlayData
            // 
            this.btnEditPlayData.Location = new System.Drawing.Point(172, 141);
            this.btnEditPlayData.Name = "btnEditPlayData";
            this.btnEditPlayData.Size = new System.Drawing.Size(82, 21);
            this.btnEditPlayData.TabIndex = 25;
            this.btnEditPlayData.Text = "Edit Play Data";
            this.btnEditPlayData.UseVisualStyleBackColor = true;
            this.btnEditPlayData.Visible = false;
            this.btnEditPlayData.Click += new System.EventHandler(this.EditPlayData_Click);
            this.btnEditPlayData.MouseHover += new System.EventHandler(this.btnEditPlayData_MouseHover);
            // 
            // lblPlayerAssignments
            // 
            this.lblPlayerAssignments.AutoSize = true;
            this.lblPlayerAssignments.BackColor = System.Drawing.Color.Transparent;
            this.lblPlayerAssignments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPlayerAssignments.Location = new System.Drawing.Point(12, 192);
            this.lblPlayerAssignments.Name = "lblPlayerAssignments";
            this.lblPlayerAssignments.Size = new System.Drawing.Size(240, 13);
            this.lblPlayerAssignments.TabIndex = 26;
            this.lblPlayerAssignments.Text = "Player Assignments (Right-Click for PSAL options)";
            this.lblPlayerAssignments.Visible = false;
            // 
            // btnEditFormation
            // 
            this.btnEditFormation.Location = new System.Drawing.Point(172, 96);
            this.btnEditFormation.Name = "btnEditFormation";
            this.btnEditFormation.Size = new System.Drawing.Size(82, 21);
            this.btnEditFormation.TabIndex = 27;
            this.btnEditFormation.Text = "Edit Formation";
            this.btnEditFormation.UseVisualStyleBackColor = true;
            this.btnEditFormation.Visible = false;
            this.btnEditFormation.Click += new System.EventHandler(this.EditFormation_Click);
            this.btnEditFormation.MouseHover += new System.EventHandler(this.btnEditFormation_MouseHover);
            // 
            // chbFlipPlay
            // 
            this.chbFlipPlay.AutoSize = true;
            this.chbFlipPlay.BackColor = System.Drawing.Color.Transparent;
            this.chbFlipPlay.Enabled = false;
            this.chbFlipPlay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chbFlipPlay.Location = new System.Drawing.Point(265, 51);
            this.chbFlipPlay.Name = "chbFlipPlay";
            this.chbFlipPlay.Size = new System.Drawing.Size(65, 17);
            this.chbFlipPlay.TabIndex = 28;
            this.chbFlipPlay.Text = "Flip Play";
            this.chbFlipPlay.UseVisualStyleBackColor = false;
            this.chbFlipPlay.Visible = false;
            this.chbFlipPlay.CheckedChanged += new System.EventHandler(this.chbFlipPlay_CheckedChanged);
            // 
            // cmsFormationOptions
            // 
            this.cmsFormationOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiEditFormation});
            this.cmsFormationOptions.Name = "cmsFormationOptions";
            this.cmsFormationOptions.Size = new System.Drawing.Size(293, 26);
            // 
            // tmiEditFormation
            // 
            this.tmiEditFormation.Name = "tmiEditFormation";
            this.tmiEditFormation.Size = new System.Drawing.Size(292, 22);
            this.tmiEditFormation.Text = "Edit Formation (Lineup, Alignment, etc)...";
            this.tmiEditFormation.Click += new System.EventHandler(this.EditFormation_Click);
            // 
            // picARTL
            // 
            this.picARTL.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picARTL.BackColor = System.Drawing.Color.Transparent;
            this.picARTL.Location = new System.Drawing.Point(401, 128);
            this.picARTL.Name = "picARTL";
            this.picARTL.Size = new System.Drawing.Size(539, 512);
            this.picARTL.TabIndex = 29;
            this.picARTL.TabStop = false;
            this.picARTL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picARTL_MouseClick);
            this.picARTL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picARTL_MouseDown);
            // 
            // cmsARTL
            // 
            this.cmsARTL.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsARTL_Save});
            this.cmsARTL.Name = "cmsARTL";
            this.cmsARTL.Size = new System.Drawing.Size(138, 26);
            // 
            // cmsARTL_Save
            // 
            this.cmsARTL_Save.Enabled = false;
            this.cmsARTL_Save.Name = "cmsARTL_Save";
            this.cmsARTL_Save.Size = new System.Drawing.Size(137, 22);
            this.cmsARTL_Save.Text = "Save Playart";
            this.cmsARTL_Save.Click += new System.EventHandler(this.cmsARTL_Save_Click);
            // 
            // cmsEditPSAL
            // 
            this.cmsEditPSAL.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsEditPSAL_FromList,
            this.cmsEditPSAL_New,
            this.cmsEditPSAL_Seperator,
            this.cmsEditPSAL_Copy,
            this.cmsEditPSAL_Paste});
            this.cmsEditPSAL.Name = "cmsEditPSAL";
            this.cmsEditPSAL.Size = new System.Drawing.Size(166, 98);
            // 
            // cmsEditPSAL_FromList
            // 
            this.cmsEditPSAL_FromList.Name = "cmsEditPSAL_FromList";
            this.cmsEditPSAL_FromList.Size = new System.Drawing.Size(165, 22);
            this.cmsEditPSAL_FromList.Text = "Swap/Edit PSAL";
            this.cmsEditPSAL_FromList.Click += new System.EventHandler(this.cmsEditPSAL_FromList_Click);
            // 
            // cmsEditPSAL_New
            // 
            this.cmsEditPSAL_New.Name = "cmsEditPSAL_New";
            this.cmsEditPSAL_New.Size = new System.Drawing.Size(165, 22);
            this.cmsEditPSAL_New.Text = "Create New PSAL";
            this.cmsEditPSAL_New.Click += new System.EventHandler(this.cmsEditPSAL_New_Click);
            // 
            // cmsEditPSAL_Seperator
            // 
            this.cmsEditPSAL_Seperator.Name = "cmsEditPSAL_Seperator";
            this.cmsEditPSAL_Seperator.Size = new System.Drawing.Size(162, 6);
            // 
            // cmsEditPSAL_Copy
            // 
            this.cmsEditPSAL_Copy.Name = "cmsEditPSAL_Copy";
            this.cmsEditPSAL_Copy.ShowShortcutKeys = false;
            this.cmsEditPSAL_Copy.Size = new System.Drawing.Size(165, 22);
            this.cmsEditPSAL_Copy.Text = "Copy PSALs";
            this.cmsEditPSAL_Copy.Click += new System.EventHandler(this.cmsEditPSAL_Copy_Click);
            // 
            // cmsEditPSAL_Paste
            // 
            this.cmsEditPSAL_Paste.Enabled = false;
            this.cmsEditPSAL_Paste.Name = "cmsEditPSAL_Paste";
            this.cmsEditPSAL_Paste.ShowShortcutKeys = false;
            this.cmsEditPSAL_Paste.Size = new System.Drawing.Size(165, 22);
            this.cmsEditPSAL_Paste.Text = "Paste PSALs";
            this.cmsEditPSAL_Paste.Click += new System.EventHandler(this.cmsEditPSAL_Paste_Click);
            // 
            // lblOpenPlaybook
            // 
            this.lblOpenPlaybook.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblOpenPlaybook.BackColor = System.Drawing.Color.Transparent;
            this.lblOpenPlaybook.Font = new System.Drawing.Font("Calibri", 68F);
            this.lblOpenPlaybook.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblOpenPlaybook.Location = new System.Drawing.Point(61, 270);
            this.lblOpenPlaybook.Name = "lblOpenPlaybook";
            this.lblOpenPlaybook.OutlineForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblOpenPlaybook.OutlineWidth = 5F;
            this.lblOpenPlaybook.Size = new System.Drawing.Size(835, 114);
            this.lblOpenPlaybook.TabIndex = 30;
            this.lblOpenPlaybook.Text = "Open NCAA Playbook";
            this.lblOpenPlaybook.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOpenPlaybook.Click += new System.EventHandler(this.mnuFile_Open_Click);
            this.lblOpenPlaybook.MouseEnter += new System.EventHandler(this.lblOpenPlaybook_MouseEnter);
            this.lblOpenPlaybook.MouseLeave += new System.EventHandler(this.lblOpenPlaybook_MouseLeave);
            // 
            // frmPlaybook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Playbook_Editor.Properties.Resources.chalkboard_2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(966, 649);
            this.Controls.Add(this.lblOpenPlaybook);
            this.Controls.Add(this.dgvPLYS);
            this.Controls.Add(this.picARTL);
            this.Controls.Add(this.chbFlipPlay);
            this.Controls.Add(this.btnEditFormation);
            this.Controls.Add(this.lblPlayerAssignments);
            this.Controls.Add(this.btnEditPlayData);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.lblAlignments);
            this.Controls.Add(this.cbxAlignments);
            this.Controls.Add(this.picPlay);
            this.Controls.Add(this.lblPlays);
            this.Controls.Add(this.cbxPlays);
            this.Controls.Add(this.lblSubFormations);
            this.Controls.Add(this.cbxSubFormations);
            this.Controls.Add(this.lblFormations);
            this.Controls.Add(this.cbxFormations);
            this.Controls.Add(this.btn_Min);
            this.Controls.Add(this.btn_Max);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.mnuPlaybook);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuPlaybook;
            this.Name = "frmPlaybook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmPlaybook_Load);
            this.Click += new System.EventHandler(this.frmPlaybook_Click);
            this.mnuPlaybook.ResumeLayout(false);
            this.mnuPlaybook.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPLYS)).EndInit();
            this.cmsPlayOptions.ResumeLayout(false);
            this.cmsFormationOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picARTL)).EndInit();
            this.cmsARTL.ResumeLayout(false);
            this.cmsEditPSAL.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_Open;
        private System.Windows.Forms.ToolStripSeparator mnuBreak_Playbook;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_Exit;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Max;
        private System.Windows.Forms.Button btn_Min;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblFormations;
        private System.Windows.Forms.Label lblSubFormations;
        private System.Windows.Forms.Label lblPlays;
        private System.Windows.Forms.PictureBox picPlay;
        private System.Windows.Forms.Label lblAlignments;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_Save;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuTools_PSALVisualizer;
        private System.Windows.Forms.ContextMenuStrip cmsPlayOptions;
        private System.Windows.Forms.ToolStripMenuItem editPlayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions_Field;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions_Field_Default;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions_Field_Brown;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions_Field_Green;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions_Field_Dark;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions_Field_Transparent;
        private System.Windows.Forms.Label lblFileName;
        public System.Windows.Forms.DataGridView dgvPLYS;
        private System.Windows.Forms.Label lblPlayerAssignments;
        public System.Windows.Forms.CheckBox chbFlipPlay;
        private System.Windows.Forms.ContextMenuStrip cmsFormationOptions;
        private System.Windows.Forms.ToolStripMenuItem tmiEditFormation;
        public System.Windows.Forms.ComboBox cbxFormations;
        public System.Windows.Forms.ComboBox cbxSubFormations;
        public System.Windows.Forms.ComboBox cbxPlays;
        public System.Windows.Forms.ComboBox cbxAlignments;
        public System.Windows.Forms.Button btnEditPlayData;
        public System.Windows.Forms.Button btnEditFormation;
        private System.Windows.Forms.PictureBox picARTL;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip cmsARTL;
        private System.Windows.Forms.ToolStripMenuItem cmsARTL_Save;
        private System.Windows.Forms.ContextMenuStrip cmsEditPSAL;
        private System.Windows.Forms.ToolStripMenuItem cmsEditPSAL_FromList;
        private System.Windows.Forms.ToolStripMenuItem cmsEditPSAL_New;
        public System.Windows.Forms.MenuStrip mnuPlaybook;
        public System.Windows.Forms.ToolStripMenuItem mnuOptions;
        public System.Windows.Forms.ToolStripMenuItem mnuOptions_Playart;
        public System.Windows.Forms.ToolStripMenuItem mnuOptions_Playart_PSAL;
        public System.Windows.Forms.ToolStripMenuItem mnuOptions_Playart_ARTL;
        private CustomLabel lblOpenPlaybook;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions_Field_Offense;
        private System.Windows.Forms.ToolStripSeparator cmsEditPSAL_Seperator;
        private System.Windows.Forms.ToolStripMenuItem cmsEditPSAL_Copy;
        private System.Windows.Forms.ToolStripMenuItem cmsEditPSAL_Paste;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuView_Morphy;
        private System.Windows.Forms.ToolStripMenuItem mnuView_PBModding;
    }
}