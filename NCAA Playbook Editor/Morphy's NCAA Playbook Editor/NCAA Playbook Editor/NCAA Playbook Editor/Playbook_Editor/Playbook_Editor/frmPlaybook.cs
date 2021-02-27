using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Playbook_Editor
{
    public partial class frmPlaybook : Form
    {
        #region Variables

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private frmLoading objfrmLoading;
        private string DBfileDir;
        private int OpenIndex;
        private bool BigEndian = false;
        private bool FBChunks = false;
        private string OpenFileDialogTMP = "";
        public Image blankField, gImage;
        private Bitmap bmpARTL;
        private DataTable dtPLYS = new DataTable();
        private List<TableNames> TableNames;
        private List<CPFM> CPFM;
        private List<SETL> SETL;
        private List<PBPL> PBPL;
        public List<PLPD> PLPD;
        public List<PLRD> PLRD;
        public List<PLYS> PLYS;
        private List<SETP> SETP;
        private List<SETG> SETG;
        private List<SGFF> SGFF;
        public List<SRFT> SRFT;
        public List<PSAL> PSLO;
        public List<PSAL> PSLD;
        private List<ARTL> ARTO;
        private List<ARTL> ARTD;
        public List<PLRR> PSAL_PLRR_ARTL;
        public PLAY Play;
        public PLAY PlayCopy;
        private Point LOS;
        private bool flag_cell_edited;
        private int currentDGVRow = 0;
        private int currentDGVCol = 0;

        #endregion

        public frmPlaybook()
        {
            InitializeComponent();
            AddDrag(mnuPlaybook);

            blankField = picPlay.Image;

            LOS = new Point((int)(picPlay.Width / 2), (int)(picPlay.Height / 1.333333333));
            mnuPlaybook.Renderer = new MyRenderer();
        }

        static void gotoSite(string url)
        {
            System.Diagnostics.Process.Start(url);
        }
            private string StrReverse(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public void UpdatePlay(PLAY play)
        {
            //Get the selected play detail (PSALs, etc) information

            chbFlipPlay.Visible = true;
            lblPlayerAssignments.Visible = true;
            dgvPLYS.Visible = true;
            dgvPLYS.DataSource = Play.PLYS;
            dgvPLYS.Columns["rec"].Visible = false;
            dgvPLYS.Columns["PLYL"].Visible = true;
            dgvPLYS.Columns["PLYL"].ReadOnly = true;
            dgvPLYS.Columns["poso"].ReadOnly = true;
            dgvPLYS.AutoResizeColumns();
            Program.ResizeDataGrid(dgvPLYS);
            dgvPLYS.ShowCellToolTips = true;
            foreach (DataGridViewRow row in dgvPLYS.Rows) foreach (DataGridViewCell cell in row.Cells) cell.ToolTipText = "Right-Click for PSAL options.";

            ClearField();

            //Convert PSALs
            for (int i = 0; i < play.posos.Count(); i++)
            {
                play.posos[i] = Program.ConvertPSAL(play.posos[i], LOS, chbFlipPlay.Checked);
            }

            Console.WriteLine(play.PBPL[0].PLYL);

            DrawPlayartPSAL(Graphics.FromImage(gImage), Play);
            DrawPlayartARTL(Play, true);
        }

        public void UpdateSubFormations()
        {
            //Fill the SubFormations list and show it
            lblSubFormations.Visible = true;
            cbxSubFormations.Visible = true;
            cbxSubFormations.Items.Clear();
            cbxSubFormations.ValueMember = null;
            cbxSubFormations.DisplayMember = "name";

            for (int i = 0; i < SETL.Count; i++)
            {
                if (SETL[i].FORM == ((CPFM)cbxFormations.SelectedItem).FORM)
                {
                    cbxSubFormations.Items.Add(SETL[i]);
                }
            }

            cbxSubFormations.SelectedIndex = 0;

            ClearField();
        }

        public void UpdateAlignments()
        {
            //Fill the Alignments list and show it
            lblAlignments.Visible = true;
            cbxAlignments.Visible = true;
            cbxAlignments.Items.Clear();
            cbxAlignments.ValueMember = null;
            cbxAlignments.DisplayMember = "name";
            btnEditFormation.Visible = true;

            for (int i = 0; i < SGFF.Count; i++)
            {
                if (SGFF[i].SETL == ((SETL)cbxSubFormations.SelectedItem).setl)
                {
                    cbxAlignments.Items.Add(SGFF[i]);
                }
            }

            cbxAlignments.SelectedItem = (cbxAlignments.Items.Cast<SGFF>().ToList()).Find(item => item.name == "Norm");
        }

        public void UpdatePlays()
        {
            //Fill the Play list and show it
            lblPlays.Visible = true;
            cbxPlays.Visible = true;
            cbxPlays.Items.Clear();
            cbxPlays.ValueMember = null;
            cbxPlays.DisplayMember = "name";

            for (int i = 0; i < PBPL.Count; i++)
            {
                if (PBPL[i].SETL == ((SETL)cbxSubFormations.SelectedItem).setl)
                {
                    cbxPlays.Items.Add(PBPL[i]);
                }
            }

            cbxPlays.SelectedIndex = 0;
            cbxPlays.Focus();
        }

        #region Loading Form

        private void StartProgress()
        {
            objfrmLoading = new frmLoading(this);
            ShowProgress();
        }

        private void CloseProgress()
        {
            Thread.Sleep(200);
            objfrmLoading.Invoke(new Action(objfrmLoading.Close));
        }

        private void ShowProgress()
        {
            try
            {
                if (InvokeRequired)
                {
                    try
                    {
                        objfrmLoading.ShowDialog();
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    Thread th = new Thread(ShowProgress);
                    th.IsBackground = false;
                    th.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Title Bar

        private void btn_Min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btn_Max_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                CenterToScreen();
            }
            else
            {
                MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
                WindowState = FormWindowState.Maximized;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Close_MouseEnter(object sender, EventArgs e)
        {
            btn_Close.Image = ((Image)(Properties.Resources.btn_close_hover));
        }

        private void btn_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_Close.Image = ((Image)(Properties.Resources.btn_close));
        }

        private void btn_Min_MouseEnter(object sender, EventArgs e)
        {
            btn_Min.Image = ((Image)(Properties.Resources.btn_min_hover));
        }

        private void btn_Min_MouseLeave(object sender, EventArgs e)
        {
            btn_Min.Image = ((Image)(Properties.Resources.btn_min));
        }

        private void btn_Max_MouseEnter(object sender, EventArgs e)
        {
            btn_Max.Image = ((Image)(Properties.Resources.btn_max_hover));
        }

        private void btn_Max_MouseLeave(object sender, EventArgs e)
        {
            btn_Max.Image = ((Image)(Properties.Resources.btn_max));
        }

        private void AddDrag(Control Control)
        {
            Control.MouseDown += new MouseEventHandler(DragForm_MouseDown);
        }

        private void DragForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                if (Location.Y == 0)
                {
                    MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
                    WindowState = FormWindowState.Maximized;
                }
            }
            if (e.Clicks == 2)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Normal;
                }
                else
                {
                    MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
                    WindowState = FormWindowState.Maximized;
                }
            }
        }

        #endregion

        #region Menu/Context Strips

        private void mnuFile_Open_Click(object sender, EventArgs e)
        {
            lblOpenPlaybook.Visible = false;

            openFileDialog1.Filter = "DB Files | *.db";
            openFileDialog1.FileName = "";

            DialogResult result = openFileDialog1.ShowDialog();
            if (!(result == DialogResult.OK) || openFileDialog1.FileName == "")
            {
                lblOpenPlaybook.Visible = true;
                return;
            }

            BigEndian = false;
            FBChunks = false;
            CheckDB(openFileDialog1.FileName);

            OpenIndex = TDB.TDBOpen(openFileDialog1.FileName);

            //store the folder where the DB file is located
            DBfileDir = openFileDialog1.FileName.Substring(0, openFileDialog1.FileName.LastIndexOf('\\'));

            if (OpenIndex == -1)
            {
                MessageBox.Show("Unable to open the database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                lblFileName.Text = openFileDialog1.SafeFileName;
                LoadDB();
                mnuFile_Save.Enabled = true;
            }
        }

        private void mnuFile_Save_Click(object sender, EventArgs e)
        {
            //saveFileDialog1.Filter = "DB Files | *.db";
            //saveFileDialog1.FileName = "";

            //DialogResult result = saveFileDialog1.ShowDialog();
            //if (!(result == DialogResult.OK) || saveFileDialog1.FileName == "")
            //{
            //    return;
            //}

            //if (OpenIndex == -1)
            //{
            //    MessageBox.Show("Unable to open the database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            UpdateDB();
            SaveDB();
            MessageBox.Show("Playbook saved", "Save Playbook");
            //}          
        }

        private void mnuFile_Exit_Click(object sender, EventArgs e)
        {
            if (Application.MessageLoop)
            {
                Application.Exit();
            }
            else
            {
                Environment.Exit(1);
            }
        }

        private void mnuTools_PSALVisualizer_Click(object sender, EventArgs e)
        {
            frmPSALVisualizer PSAL_Visualizer = new frmPSALVisualizer();
            PSAL_Visualizer.ShowDialog();
        }

        private void mnuOptions_Field_Default_Click(object sender, EventArgs e)
        {
            picPlay.Image = ((Image)(Properties.Resources.field_blank_resized));
            blankField = picPlay.Image;
            mnuOptions_Field_Default.Checked = true;
            mnuOptions_Field_Brown.Checked = false;
            mnuOptions_Field_Green.Checked = false;
            mnuOptions_Field_Dark.Checked = false;
            mnuOptions_Field_Transparent.Checked = false;
            mnuOptions_Field_Offense.Checked = false;
            if (!(Play.PBPL == null)) UpdatePlay(Play);
        }

        private void mnuOptions_Field_Brown_Click(object sender, EventArgs e)
        {
            picPlay.Image = ((Image)(Properties.Resources.field_blank_resized_dead));
            blankField = picPlay.Image;
            mnuOptions_Field_Default.Checked = false;
            mnuOptions_Field_Brown.Checked = true;
            mnuOptions_Field_Green.Checked = false;
            mnuOptions_Field_Dark.Checked = false;
            mnuOptions_Field_Transparent.Checked = false;
            mnuOptions_Field_Offense.Checked = false;
            if (!(Play.PBPL == null)) UpdatePlay(Play);
        }

        private void mnuOptions_Field_Green_Click(object sender, EventArgs e)
        {
            picPlay.Image = ((Image)(Properties.Resources.field_blank_resized_green));
            blankField = picPlay.Image;
            mnuOptions_Field_Default.Checked = false;
            mnuOptions_Field_Brown.Checked = false;
            mnuOptions_Field_Green.Checked = true;
            mnuOptions_Field_Dark.Checked = false;
            mnuOptions_Field_Transparent.Checked = false;
            mnuOptions_Field_Offense.Checked = false;
            if (!(Play.PBPL == null)) UpdatePlay(Play);
        }

        private void mnuOptions_Field_Dark_Click(object sender, EventArgs e)
        {
            picPlay.Image = ((Image)(Properties.Resources.field_blank_resized_Desaturated));
            blankField = picPlay.Image;
            mnuOptions_Field_Default.Checked = false;
            mnuOptions_Field_Brown.Checked = false;
            mnuOptions_Field_Green.Checked = false;
            mnuOptions_Field_Dark.Checked = true;
            mnuOptions_Field_Transparent.Checked = false;
            mnuOptions_Field_Offense.Checked = false;
            if (!(Play.PBPL == null)) UpdatePlay(Play);
        }

        private void mnuOptions_Field_Transparent_Click(object sender, EventArgs e)
        {
            picPlay.Image = ((Image)(Properties.Resources.field_blank_lines));
            blankField = picPlay.Image;
            mnuOptions_Field_Default.Checked = false;
            mnuOptions_Field_Brown.Checked = false;
            mnuOptions_Field_Green.Checked = false;
            mnuOptions_Field_Dark.Checked = false;
            mnuOptions_Field_Transparent.Checked = true;
            mnuOptions_Field_Offense.Checked = false;
            if (!(Play.PBPL == null)) UpdatePlay(Play);
        }

        private void mnuOptions_Field_Offense_Click(object sender, EventArgs e)
        {
            picPlay.Image = ((Image)(Properties.Resources.field_blank_resized_Offense));
            blankField = picPlay.Image;
            mnuOptions_Field_Default.Checked = false;
            mnuOptions_Field_Brown.Checked = false;
            mnuOptions_Field_Green.Checked = false;
            mnuOptions_Field_Dark.Checked = false;
            mnuOptions_Field_Offense.Checked = true;
            mnuOptions_Field_Transparent.Checked = false;
            if (!(Play.PBPL == null)) UpdatePlay(Play);
        }

        private void mnuOptions_Playart_PSAL_Click(object sender, EventArgs e)
        {
            mnuOptions_Playart_ARTL.Checked = false;
            picARTL.Visible = false;
            mnuOptions_Playart_PSAL.Checked = true;
            picPlay.Visible = true;
            chbFlipPlay.Enabled = true;
            cbxAlignments.Enabled = true;
        }

        private void mnuOptions_Playart_ARTL_Click(object sender, EventArgs e)
        {
            mnuOptions_Playart_ARTL.Checked = true;
            picARTL.Visible = true;
            mnuOptions_Playart_PSAL.Checked = false;
            picPlay.Visible = false;
            chbFlipPlay.Enabled = false;
            cbxAlignments.Enabled = false;
            try { cbxAlignments.SelectedItem = (cbxAlignments.Items.Cast<SGFF>().ToList()).Find(item => item.name == "Norm"); }
            catch { }
        }

        #endregion

        #region UI Interaction

        private void lblOpenPlaybook_MouseEnter(object sender, EventArgs e)
        {
            lblOpenPlaybook.ForeColor = Color.FromArgb(255, 150, 150, 150);
            lblOpenPlaybook.OutlineForeColor = Color.FromArgb(255, 57, 123, 255);
        }

        private void lblOpenPlaybook_MouseLeave(object sender, EventArgs e)
        {
            lblOpenPlaybook.ForeColor = Color.FromArgb(255, 244, 244, 244);
            lblOpenPlaybook.OutlineForeColor = Color.FromArgb(255, 57, 123, 255);
        }

        private void frmPlaybook_Click(object sender, EventArgs e)
        {
            picPlay.Focus();
        }

        private void picPlay_Click(object sender, EventArgs e)
        {
            picPlay.Focus();
        }

        private void picARTL_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsARTL.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void picARTL_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsARTL.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void cmsARTL_Save_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ValidateNames = true;
            saveFileDialog1.CheckPathExists = true;

            saveFileDialog1.Filter = "PNG | *.png";
            saveFileDialog1.DefaultExt = "png";

            saveFileDialog1.FileName = saveFileDialog1.InitialDirectory + Play.PBPL[0].PLYL.ToString() + ".png";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //save PSAL image
                DrawPlayartARTL(Play, false);
                picARTL.Image.Save(saveFileDialog1.FileName, ImageFormat.Png);
                DrawPlayartARTL(Play, true);
            }
        }

        private void cbxSubFormations_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(cbxSubFormations, "Right-Click to edit Play Formation." + "\n\n" + "(Name, Positions, Alignments, etc)...");
        }

        private void cbxSubFormations_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsFormationOptions.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void cbxSubFormations_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsFormationOptions.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void btnEditFormation_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnEditFormation, "Click to edit Play Formation." + "\n\n" + "(Name, Positions, Alignments, etc)...");
        }

        private void EditFormation_Click(object sender, EventArgs e)
        {
            frmEditFormation frmEditFormation = new frmEditFormation(this);
            frmEditFormation.DefaultSETL = (from row in Play.SETL select new SETL()
            {
                rec = row.rec,
                setl = row.setl,
                FORM = row.FORM,
                MOTN = row.MOTN,
                CLAS = row.CLAS,
                SETT = row.SETT,
                SITT = row.SITT,
                SLF_ = row.SLF_,
                name = row.name,
                poso = row.poso
            }).Cast<SETL>().ToList();
            frmEditFormation.DefaultSETP = (from row in Play.SETP select new SETP()
            {
                rec = row.rec,
                SETL = row.SETL,
                setp = row.setp,
                SGT_ = row.SGT_,
                arti = row.arti,
                opnm = row.opnm,
                tabo = row.tabo,
                poso = row.poso,
                odep = row.odep,
                flas = row.flas,
                DPos = row.DPos,
                EPos = row.EPos,
                fmtx = row.fmtx,
                artx = row.artx,
                fmty = row.fmty,
                arty = row.arty
            }).Cast<SETP>().ToList();
            frmEditFormation.DefaultSETG = (from row in Play.SETG select new SETG()
            {
                rec = row.rec,
                SETP = row.SETP,
                SGF_ = row.SGF_,
                x___ = row.x___,
                y___ = row.y___,
                fx__ = row.fx__,
                fy__ = row.fy__,
                anm_ = row.anm_,
                dir_ = row.dir_,
                fanm = row.fanm,
                fdir = row.fdir,
                active = row.active
            }).Cast<SETG>().ToList();

            frmEditFormation.Location = new Point(Cursor.Position.X - (frmEditFormation.Width / 2), Cursor.Position.Y + (frmEditFormation.Height / 3));
            frmEditFormation.Show();
        }

        private void cbxPlays_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(cbxPlays, "Right-Click to edit Play Data." + "\n\n" + "(Name, Type, vpos, etc)...");
        }

        private void cbxPlays_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsPlayOptions.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void cbxPlays_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsPlayOptions.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void btnEditPlayData_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnEditPlayData, "Click to edit Play Data." + "\n\n" + "(Name, Type, vpos, etc)...");
        }

        private void EditPlayData_Click(object sender, EventArgs e)
        {
            int playREC = ((PBPL)cbxPlays.SelectedItem).rec;
            int SubFormREC = ((SETL)cbxSubFormations.SelectedItem).rec;

            frmEditPlay frmEditPlay = new frmEditPlay(this);
            frmEditPlay.Play = Play.Clone();
            if (frmEditPlay.ShowDialog() == DialogResult.OK)
            {
                // SRFT = frmEditPlay.newSRFT;
                PLPD = frmEditPlay.newPLPD;
                PLRD = frmEditPlay.newPLRD;
                Play = frmEditPlay.Play;
                SetPlay(Play);
                UpdateSubFormations();
                cbxSubFormations.SelectedItem = (cbxSubFormations.Items.Cast<SETL>().ToList()).Find(item => item.rec == SubFormREC);
                UpdateAlignments();
                UpdatePlays();
                cbxPlays.SelectedItem = (cbxPlays.Items.Cast<PBPL>().ToList()).Find(item => item.rec == playREC);
                Play = GetPlay();
                UpdatePlay(Play);
            }
        }

        private void dgvPLYS_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                dgvPLYS.CurrentCell = dgvPLYS[e.ColumnIndex, e.RowIndex];
                cmsEditPSAL.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void dgvPLYS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (!flag_cell_edited)
            //{
            //    currentDGVRow = dgvPLYS.CurrentCell.RowIndex;
            //    currentDGVCol = dgvPLYS.CurrentCell.ColumnIndex;
            //}

            foreach (PSALroute poso in Play.posos) poso.Highlighted = false;
            Play.posos[dgvPLYS.CurrentRow.Index].Highlighted = true;

            ClearField();
            DrawPlayartPSAL(Graphics.FromImage(gImage), Play);
            DrawPlayartARTL(Play, true);
        }

        private void dgvPLYS_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SetPlay(Play);
            Play = GetPlay();
            UpdatePlay(Play);
        }

        private void dgvPLYS_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            flag_cell_edited = true;
            PSAL_PLRR_ARTL = GetPSAL_PLRR_ARTL();
        }

        private void dgvPLYS_SelectionChanged(object sender, EventArgs e)
        {
            //if (flag_cell_edited)
            //{
            //    DataGridViewCell cell = dgvPLYS.Rows[currentDGVRow].Cells[currentDGVCol];
            //    dgvPLYS.CurrentCell = cell;
            //    flag_cell_edited = false;
            //}
        }

        private void cmsEditPSAL_FromList_Click(object sender, EventArgs e)
        {
            frmEditPSAL frmEditPSAL = new frmEditPSAL(this);
            frmEditPSAL.DefaultPSAL = new PLYS
            {
                ARTL = Play.PLYS[dgvPLYS.SelectedCells[0].RowIndex].ARTL,
                PLRR = Play.PLYS[dgvPLYS.SelectedCells[0].RowIndex].PLRR,
                PSAL = Play.PLYS[dgvPLYS.SelectedCells[0].RowIndex].PSAL
            };

            frmEditPSAL.Location = new Point(Location.X + dgvPLYS.Location.X, Cursor.Position.Y + (frmEditPSAL.Height / 3));
            frmEditPSAL.Show();
        }

        private void cmsEditPSAL_New_Click(object sender, EventArgs e)
        {
            frmCreatePSAL frmCreatePSAL = new frmCreatePSAL(this);
            frmCreatePSAL.DefaultPSAL = new PLYS
            {
                ARTL = Play.PLYS[dgvPLYS.SelectedCells[0].RowIndex].ARTL,
                PLRR = Play.PLYS[dgvPLYS.SelectedCells[0].RowIndex].PLRR,
                PSAL = Play.PLYS[dgvPLYS.SelectedCells[0].RowIndex].PSAL
            };

            frmCreatePSAL.Location = new Point(Location.X + dgvPLYS.Location.X, Cursor.Position.Y + (frmCreatePSAL.Height / 3));
            frmCreatePSAL.Show();
        }

        private void selectFormation(object sender, EventArgs e)
        {
            PSAL_PLRR_ARTL = GetPSAL_PLRR_ARTL();

            UpdateSubFormations();
        }

        private void selectSubFormation(object sender, EventArgs e)
        {
            UpdateAlignments();
            UpdatePlays();
        }

        private void selectAlignment(object sender, EventArgs e)
        {
            try{ Play = GetPlay(); UpdatePlay(Play); }
            catch{}
        }

        private void selectPlay(object sender, EventArgs e)
        {
            cmsARTL_Save.Enabled = true;
            btnEditPlayData.Visible = true;
            Play = GetPlay();
            UpdatePlay(Play);
        }

        private void chbFlipPlay_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Play = GetPlay();
                UpdatePlay(Play);
            }
            catch
            {
            }
        }

        #endregion

        #region Load/Edit Tables

        private void CheckDB(string filename)
        {
            byte[] array = File.ReadAllBytes(filename);  // Load DB into memory.

            // This really should be wrapped in a try-catch to prevent people trying to open the db file while it is already opened

            BinaryReader binreader = new BinaryReader(File.Open(filename, FileMode.Open));
            UInt16 check = binreader.ReadUInt16();      // Read 16bit unsigned int

            // DB file  0x means hex, so this is checking to see that file begins with 44 42, which is the "DB" ID
            // This ID works even if the file is bigendian
            if (check == 0x4244)
            {
                binreader.BaseStream.Position = 4;      // Advance the binreader to offset 4 of the file
                UInt32 endian = binreader.ReadUInt32(); // Read in 32bit  unsigned int
                if (endian == 1)
                    BigEndian = true;
            }
            else if (check == 0x4246)                   // Checking to see that file begins with 46 42, which is the "FB" ID (FBCHUNKS).
            {
                OpenFileDialogTMP = filename;
                FBChunks = true;
                // Save FBCHUNKS information.
                BinaryWriter fbbinwriter = new BinaryWriter(File.Open(filename + ".bin", FileMode.Create));
                for (int i = 0; i < 62; i++)
                {
                    fbbinwriter.Write(array[i]);
                }
                fbbinwriter.Close();  // end Save FBCHUNKS information.

                // Check if DB is BigEndian.
                if (Convert.ToUInt32(array[66]) == 1)
                    BigEndian = true;

                // Save DB information.
                BinaryWriter dbbinwriter = new BinaryWriter(File.Open(filename + ".db", FileMode.Create));
                for (int i = 62; i < array.Length; i++)
                {
                    dbbinwriter.Write(array[i]);
                }
                dbbinwriter.Close();  // end Save DB information.
                openFileDialog1.FileName = filename + ".db";
            }
            binreader.Close();
        }

        private void LoadDB()
        {
            //Open the loading window
            StartProgress();

            //Load the tables into memory
            TableNames = GetTables();
            CPFM = GetCPFM();
            SETL = GetSETL();
            SETP = GetSETP();
            SETG = GetSETG();
            SGFF = GetSGFF();
            // SRFT = GetSRFT();
            PBPL = GetPBPL();
            PLPD = GetPLPD();
            PLRD = GetPLRD();
            PLYS = GetPLYL();
            PSLO = GetPSLO();
            PSLD = GetPSLD();
            ARTO = GetARTO();
            ARTD = GetARTD();

            lblFormations.Visible = true;
            cbxFormations.Visible = true;
            cbxFormations.Items.Clear();
            cbxFormations.ValueMember = null;
            cbxFormations.DisplayMember = "name";

            for (int i = 0; i < CPFM.Count; i++)
            {
                cbxFormations.Items.Add(CPFM[i]);
            }

            CloseProgress();
        }

        private List<TableNames> GetTables()
        {
            //Number of tables
            int TableCount = TDB.TDBDatabaseGetTableCount(OpenIndex);

            //Table Properties
            TdbTableProperties TableProps = new TdbTableProperties();

            //Table Name
            StringBuilder TableName = new StringBuilder("    ", 5);

            List<TableNames> tablenames = new List<TableNames>();

            for (int i = 0; i < TableCount; i++)
            {
                // Init the tdbtableproperties name
                TableProps.Name = TableName.ToString();

                // Get the tableproperties for the given table number
                if (TDB.TDBTableGetProperties(OpenIndex, i, ref TableProps))
                {
                    if (BigEndian)
                        TableProps.Name = StrReverse(TableProps.Name);

                    tablenames.Add(new TableNames { rec = i, name = TableProps.Name });
                }
            }
            return tablenames;
        }

        private void SaveDB()
        {
            if (!TDB.TDBSave(OpenIndex))
            {
                MessageBox.Show("Error Saving");
            }
        }

        private void UpdateDB()
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            foreach (PLYS item in PLYS)
            {
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLYS"), StrReverse("PSAL"), item.rec, item.PSAL);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLYS"), StrReverse("ARTL"), item.rec, item.ARTL);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLYS"), StrReverse("PLRR"), item.rec, item.PLRR);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLYS"), StrReverse("poso"), item.rec, item.poso);
            }

            foreach (SETL item in SETL)
            {
                //TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("SETL"), item.rec, item.setl);
                //TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("FORM"), item.rec, item.FORM);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("MOTN"), item.rec, item.MOTN);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("CLAS"), item.rec, item.CLAS);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("SETT"), item.rec, item.SETT);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("SITT"), item.rec, item.SITT);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("SLF_"), item.rec, item.SLF_);
                TDB.TDBFieldSetValueAsString(OpenIndex, StrReverse("SETL"), StrReverse("name"), item.rec, item.name);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("poso"), item.rec, item.poso);
            }

            foreach (PBPL item in PBPL)
            {
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("COMF"), item.rec, item.COMF);
                //TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("SETL"), rec.rec, rec.SETL);
                //TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("PLYL"), rec.rec, rec.PLYL);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("SRMM"), item.rec, item.SRMM);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("SITT"), item.rec, item.SITT);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("PLYT"), item.rec, item.PLYT);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("PLF_"), item.rec, item.PLF_);
                TDB.TDBFieldSetValueAsString(OpenIndex, StrReverse("PBPL"), StrReverse("name"), item.rec, item.name);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("risk"), item.rec, item.risk);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("motn"), item.rec, item.motn);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("vpos"), item.rec, item.vpos);
            }

            foreach (SETP item in SETP)
            {
                //TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("SETL"), item.rec, item.SETL);
                //TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("SETP"), item.rec, item.setp);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("SGT_"), item.rec, item.SGT_);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("arti"), item.rec, item.arti);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("opnm"), item.rec, item.opnm);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("tabo"), item.rec, item.tabo);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("poso"), item.rec, item.poso);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("odep"), item.rec, item.odep);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("flas"), item.rec, item.flas);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("DPos"), item.rec, item.DPos);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("EPos"), item.rec, item.EPos);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("fmtx"), item.rec, item.fmtx);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("artx"), item.rec, item.artx);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("fmty"), item.rec, item.fmty);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("arty"), item.rec, item.arty);
            }

            foreach (SETG item in SETG)
            {
                //TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("SETP"), item.rec, item.SETP);
                //TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("SGF_"), item.rec, item.SGF_);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("SF__"), item.rec, item.SF__);
                TDB.TDBFieldSetValueAsFloat(OpenIndex, StrReverse("SETG"), StrReverse("x___"), item.rec, item.x___);
                TDB.TDBFieldSetValueAsFloat(OpenIndex, StrReverse("SETG"), StrReverse("y___"), item.rec, item.y___);
                TDB.TDBFieldSetValueAsFloat(OpenIndex, StrReverse("SETG"), StrReverse("fx__"), item.rec, item.fx__);
                TDB.TDBFieldSetValueAsFloat(OpenIndex, StrReverse("SETG"), StrReverse("fy__"), item.rec, item.fy__);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("anm_"), item.rec, item.anm_);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("dir_"), item.rec, item.dir_);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("fanm"), item.rec, item.fanm);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("fdir"), item.rec, item.fdir);
            }

            #region Update PLPD

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "PLPD").rec, ref TableProps);

            if (PLPD.Count() > TableProps.RecordCount)
            {
                for (int i = 0; i < PLPD.Count() - TableProps.RecordCount; i++)
                {
                    TDB.TDBTableRecordAdd(OpenIndex, StrReverse("PLPD"), true);
                }
            }
            else if (PLPD.Count() < TableProps.RecordCount)
            {
                for (int i = 0; i < TableProps.RecordCount - PLPD.Count(); i++)
                {
                    TDB.TDBTableRecordRemove(OpenIndex, StrReverse("PLPD"), i);
                }
            }

            foreach (PLPD item in PLPD)
            {
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("com1"), item.rec, item.com1);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("con1"), item.rec, item.con1);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("per1"), item.rec, item.per1);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("rcv1"), item.rec, item.rcv1);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("com2"), item.rec, item.com2);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("con2"), item.rec, item.con2);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("per2"), item.rec, item.per2);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("rcv2"), item.rec, item.rcv2);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("com3"), item.rec, item.com3);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("con3"), item.rec, item.con3);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("per3"), item.rec, item.per3);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("rcv3"), item.rec, item.rcv3);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("com4"), item.rec, item.com4);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("con4"), item.rec, item.con4);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("per4"), item.rec, item.per4);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("rcv4"), item.rec, item.rcv4);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("com5"), item.rec, item.com5);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("con5"), item.rec, item.con5);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("per5"), item.rec, item.per5);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("rcv5"), item.rec, item.rcv5);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("PLYL"), item.rec, item.PLYL);
            }

            #endregion

            #region Update PLRD

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "PLRD").rec, ref TableProps);

            if (PLRD.Count() > TableProps.RecordCount)
            {
                for (int i = 0; i < PLRD.Count() - TableProps.RecordCount; i++)
                {
                    TDB.TDBTableRecordAdd(OpenIndex, StrReverse("PLRD"), true);
                }
            }
            else if (PLRD.Count() < TableProps.RecordCount)
            {
                for (int i = 0; i < TableProps.RecordCount - PLRD.Count(); i++)
                {
                    TDB.TDBTableRecordRemove(OpenIndex, StrReverse("PLRD"), i);
                }
            }

            foreach (PLRD item in PLRD)
            {
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLRD"), StrReverse("PLYL"), item.rec, item.PLYL);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PLRD"), StrReverse("hole"), item.rec, item.hole);
            }

            #endregion

            #region Update SRFT

            // Get Tableprops based on the selected index
            // TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "SRFT").rec, ref TableProps);

            // if (SRFT.Count() > TableProps.RecordCount)
            // {
                // for (int i = 0; i < SRFT.Count() - TableProps.RecordCount; i++)
                // {
                   //  TDB.TDBTableRecordAdd(OpenIndex, StrReverse("SRFT"), true);
                // }
            // }
            // else if (SRFT.Count() < TableProps.RecordCount)
            // {
               //  for (int i = 0; i < TableProps.RecordCount - SRFT.Count(); i++)
               //  {
                  //  TDB.TDBTableRecordRemove(OpenIndex, StrReverse("SRFT"), i);
                // }
            // }

            // foreach (SRFT item in SRFT)
            // {
               //  TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("SIDE"), item.rec, item.SIDE);
               //  TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("YOFF"), item.rec, item.YOFF);
               //  TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("TECH"), item.rec, item.TECH);
               //  TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("PLYL"), item.rec, item.PLYL);
               //  TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("STAN"), item.rec, item.STAN);
               //  TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("PLYR"), item.rec, item.PLYR);
               //  TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("PRIS"), item.rec, item.PRIS);
               //  TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("GAPS"), item.rec, item.GAPS);
               //  TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("ASSS"), item.rec, item.ASSS);
               //  TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("PRIW"), item.rec, item.PRIW);
               //  TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("GAPW"), item.rec, item.GAPW);
               //  TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("ASSW"), item.rec, item.ASSW);
            // }

            #endregion

            #region Update PSLO & PSLD

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "PSLO").rec, ref TableProps);

            if (PSLO.Count() > TableProps.RecordCount)
            {
                for (int i = 0; i < PSLO.Count() - TableProps.RecordCount; i++)
                {
                    TDB.TDBTableRecordAdd(OpenIndex, StrReverse("PSLO"), true);
                }
            }
            else if (PSLO.Count() < TableProps.RecordCount)
            {
                for (int i = 0; i < TableProps.RecordCount - PSLO.Count(); i++)
                {
                    TDB.TDBTableRecordRemove(OpenIndex, StrReverse("PSLO"), i);
                }
            }

            foreach (PSAL item in PSLO)
            {
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PSLO"), StrReverse("val1"), item.rec, item.val1);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PSLO"), StrReverse("val2"), item.rec, item.val2);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PSLO"), StrReverse("val3"), item.rec, item.val3);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PSLO"), StrReverse("PSAL"), item.rec, item.psal);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PSLO"), StrReverse("code"), item.rec, item.code);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PSLO"), StrReverse("step"), item.rec, item.step);
            }

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "PSLD").rec, ref TableProps);

            if (PSLD.Count() > TableProps.RecordCount)
            {
                for (int i = 0; i < PSLD.Count() - TableProps.RecordCount; i++)
                {
                    TDB.TDBTableRecordAdd(OpenIndex, StrReverse("PSLD"), true);
                }
            }
            else if (PSLD.Count() < TableProps.RecordCount)
            {
                for (int i = 0; i < TableProps.RecordCount - PSLD.Count(); i++)
                {
                    TDB.TDBTableRecordRemove(OpenIndex, StrReverse("PSLD"), i);
                }
            }

            foreach (PSAL item in PSLD)
            {
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PSLD"), StrReverse("val1"), item.rec, item.val1);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PSLD"), StrReverse("val2"), item.rec, item.val2);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PSLD"), StrReverse("val3"), item.rec, item.val3);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PSLD"), StrReverse("PSAL"), item.rec, item.psal);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PSLD"), StrReverse("code"), item.rec, item.code);
                TDB.TDBFieldSetValueAsInteger(OpenIndex, StrReverse("PSLD"), StrReverse("step"), item.rec, item.step);
            }

            #endregion
        }

        private List<CPFM> GetCPFM()
        {
            List<CPFM> _Formations = new List<CPFM>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "CPFM").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);

                TDB.TDBFieldGetProperties(OpenIndex, StrReverse("CPFM"), 2, ref FieldProps);

                string _name = new string((char)0, (FieldProps.Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(OpenIndex, StrReverse("CPFM"), StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                _Formations.Add(new CPFM
                {
                    rec = i,
                    FORM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("CPFM"), StrReverse("FORM"), i),
                    FTYP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("CPFM"), StrReverse("FTYP"), i),
                    name = _name
                });
            }

            _Formations = _Formations.ToList().OrderBy(s => s.FTYP).ThenBy(s => s.name).Cast<CPFM>().ToList();

            return _Formations;
        }

        private List<SETL> GetSETL()
        {
            List<SETL> _SubFormations = new List<SETL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "SETL").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);

                TDB.TDBFieldGetProperties(OpenIndex, StrReverse("SETL"), 7, ref FieldProps);

                string _name = new string((char)0, (FieldProps.Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(OpenIndex, StrReverse("SETL"), StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                _SubFormations.Add(new SETL
                {
                    rec = i,
                    setl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("SETL"), i),
                    FORM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("FORM"), i),
                    MOTN = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("MOTN"), i),
                    CLAS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("CLAS"), i),
                    SETT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("SETT"), i),
                    SITT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("SITT"), i),
                    SLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("SLF_"), i),
                    name = _name,
                    poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETL"), StrReverse("poso"), i)
                });
            }
            return _SubFormations;
        }

        private List<PBPL> GetPBPL()
        {
            List<PBPL> _Plays = new List<PBPL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "PBPL").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);

                TDB.TDBFieldGetProperties(OpenIndex, StrReverse("PBPL"), 7, ref FieldProps);

                string _name = new string((char)0, (FieldProps.Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(OpenIndex, StrReverse("PBPL"), StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                _Plays.Add(new PBPL
                {
                    rec = i,
                    COMF = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("COMF"), i),
                    SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("SETL"), i),
                    PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("PLYL"), i),
                    SRMM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("SRMM"), i),
                    SITT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("SITT"), i),
                    PLYT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("PLYT"), i),
                    PLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("PLF_"), i),
                    name = _name,
                    risk = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("risk"), i),
                    motn = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("motn"), i),
                    vpos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PBPL"), StrReverse("vpos"), i)
                });
            }
            return _Plays;
        }

        private List<PLPD> GetPLPD()
        {
            List<PLPD> _PLPD = new List<PLPD>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "PLPD").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                _PLPD.Add(new PLPD
                {
                    rec = i,
                    com1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("com1"), i),
                    con1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("con1"), i),
                    per1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("per1"), i),
                    rcv1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("rcv1"), i),
                    com2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("com2"), i),
                    con2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("con2"), i),
                    per2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("per2"), i),
                    rcv2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("rcv2"), i),
                    com3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("com3"), i),
                    con3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("con3"), i),
                    per3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("per3"), i),
                    rcv3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("rcv3"), i),
                    com4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("com4"), i),
                    con4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("con4"), i),
                    per4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("per4"), i),
                    rcv4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("rcv4"), i),
                    com5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("com5"), i),
                    con5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("con5"), i),
                    per5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("per5"), i),
                    rcv5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("rcv5"), i),
                    PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLPD"), StrReverse("PLYL"), i)
                });
            }
            return _PLPD;

        }

        private List<PLRD> GetPLRD()
        {
            List<PLRD> _PLRD = new List<PLRD>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "PLRD").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                _PLRD.Add(new PLRD
                {
                    rec = i,
                    PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLRD"), StrReverse("PLYL"), i),
                    hole = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLRD"), StrReverse("hole"), i)
                });
            }
            return _PLRD;
        }

        public PLAY GetPlay()
        {
            PLAY _PLAY = new PLAY();
            _PLAY.posos = new PSALroute[11];

            #region Get SETL for current play

            _PLAY.SETL = new List<SETL>();

            for (int i = 0; i < SETL.Count; i++)
            {
                if (SETL[i].setl == ((SETL)cbxSubFormations.SelectedItem).setl)
                {
                    _PLAY.SETL.Add(new SETL
                    {
                        rec = SETL[i].rec,
                        setl = SETL[i].setl,
                        FORM = SETL[i].FORM,
                        MOTN = SETL[i].MOTN,
                        CLAS = SETL[i].CLAS,
                        SETT = SETL[i].SETT,
                        SITT = SETL[i].SITT,
                        SLF_ = SETL[i].SLF_,
                        name = SETL[i].name,
                        poso = SETL[i].poso
                    });
                    break;
                }
            }

            #endregion

            #region Get PLYS for current play

            _PLAY.PLYS = new List<PLYS>();

            for (int i = 0; i < PLYS.Count; i++)
            {
                if (PLYS[i].PLYL == ((PBPL)cbxPlays.SelectedItem).PLYL)
                {
                    _PLAY.PLYS.Add(new PLYS
                    {
                        rec = PLYS[i].rec,
                        PSAL = PLYS[i].PSAL,
                        ARTL = PLYS[i].ARTL,
                        PLYL = PLYS[i].PLYL,
                        PLRR = PLYS[i].PLRR,
                        poso = PLYS[i].poso
                    });
                }
                if (_PLAY.PLYS.Count == 11)
                {
                    _PLAY.PLYS = _PLAY.PLYS.ToList().OrderBy(s => s.poso).Cast<PLYS>().ToList();
                    break;
                }
            }

            #endregion

            #region Get PBPL for current play

            _PLAY.PBPL = new List<PBPL>();

            for (int i = 0; i < PBPL.Count; i++)
            {
                if (PBPL[i].PLYL == ((PBPL)cbxPlays.SelectedItem).PLYL)
                {
                    _PLAY.PBPL.Add(new PBPL
                    {
                        rec = PBPL[i].rec,
                        COMF = PBPL[i].COMF,
                        SETL = PBPL[i].SETL,
                        PLYL = PBPL[i].PLYL,
                        SRMM = PBPL[i].SRMM,
                        SITT = PBPL[i].SITT,
                        PLYT = PBPL[i].PLYT,
                        PLF_ = PBPL[i].PLF_,
                        name = PBPL[i].name,
                        risk = PBPL[i].risk,
                        motn = PBPL[i].motn,
                        vpos = PBPL[i].vpos
                    });
                    break;
                }
            }

            #endregion

            #region Get PLPD for current play

            _PLAY.PLPD = new List<PLPD>();

            for (int i = 0; i < PLPD.Count; i++)
            {
                if (PLPD[i].PLYL == ((PBPL)cbxPlays.SelectedItem).PLYL)
                {
                    _PLAY.PLPD.Add(new PLPD
                    {
                        rec = PLPD[i].rec,
                        com1 = PLPD[i].com1,
                        con1 = PLPD[i].con1,
                        per1 = PLPD[i].per1,
                        rcv1 = PLPD[i].rcv1,
                        com2 = PLPD[i].com2,
                        con2 = PLPD[i].con2,
                        per2 = PLPD[i].per2,
                        rcv2 = PLPD[i].rcv2,
                        com3 = PLPD[i].com3,
                        con3 = PLPD[i].con3,
                        per3 = PLPD[i].per3,
                        rcv3 = PLPD[i].rcv3,
                        com4 = PLPD[i].com4,
                        con4 = PLPD[i].con4,
                        per4 = PLPD[i].per4,
                        rcv4 = PLPD[i].rcv4,
                        com5 = PLPD[i].com5,
                        con5 = PLPD[i].con5,
                        per5 = PLPD[i].per5,
                        rcv5 = PLPD[i].rcv5,
                        PLYL = PLPD[i].PLYL
                    });
                    break;
                }
            }

            #endregion

            #region Get PLRD for current play

            _PLAY.PLRD = new List<PLRD>();

            for (int i = 0; i < PLRD.Count; i++)
            {
                if (PLRD[i].PLYL == ((PBPL)cbxPlays.SelectedItem).PLYL)
                {
                    _PLAY.PLRD.Add(new PLRD
                    {
                        rec = PLRD[i].rec,
                        PLYL = PLRD[i].PLYL,
                        hole = PLRD[i].hole
                    });
                    break;
                }
            }

            #endregion

            #region Get SETP for current play

            _PLAY.SETP = new List<SETP>();

            for (int i = 0; i < SETP.Count; i++)
            {
                if (SETP[i].SETL == ((SETL)cbxSubFormations.SelectedItem).setl)
                {
                    _PLAY.SETP.Add(new SETP
                    {
                        rec = SETP[i].rec,
                        SETL = SETP[i].SETL,
                        setp = SETP[i].setp,
                        SGT_ = SETP[i].SGT_,
                        arti = SETP[i].arti,
                        opnm = SETP[i].opnm,
                        tabo = SETP[i].tabo,
                        poso = SETP[i].poso,
                        odep = SETP[i].odep,
                        flas = SETP[i].flas,
                        DPos = SETP[i].DPos,
                        EPos = SETP[i].EPos,
                        fmtx = SETP[i].fmtx,
                        artx = SETP[i].artx,
                        fmty = SETP[i].fmty,
                        arty = SETP[i].arty
                    });
                    if (_PLAY.SETP.Count == 11)
                    {
                        _PLAY.SETP = _PLAY.SETP.ToList().OrderBy(s => s.poso).Cast<SETP>().ToList();
                        break;
                    }
                }
            }

            #endregion

            #region Get SGFF for current play

            _PLAY.SGFF = new List<SGFF>();

            for (int i = 0; i < SGFF.Count; i++)
            {
                if (SGFF[i].SETL == ((SETL)cbxSubFormations.SelectedItem).setl)
                {
                    _PLAY.SGFF.Add(new SGFF
                    {
                        rec = SGFF[i].rec,
                        SETL = SGFF[i].SETL,
                        SGF_ = SGFF[i].SGF_,
                        name = SGFF[i].name,
                        dflt = SGFF[i].dflt
                    });
                }
            }
            _PLAY.SGFF = _PLAY.SGFF.ToList().OrderBy(s => s.SGF_).Cast<SGFF>().ToList();

            #endregion

            #region Get SRFT for current play

            // _PLAY.SRFT = new List<SRFT>();

            // for (int i = 0; i < SRFT.Count; i++)
            // {
               // if (SRFT[i].PLYL == ((PBPL)cbxPlays.SelectedItem).PLYL)
                // {
                   //  _PLAY.SRFT.Add(new SRFT
                    // {
                       //  rec = SRFT[i].rec,
                       //  SIDE = SRFT[i].SIDE,
                       //  YOFF = SRFT[i].YOFF,
                       //  TECH = SRFT[i].TECH,
                       //  PLYL = SRFT[i].PLYL,
                       //  STAN = SRFT[i].STAN,
                       //  PLYR = SRFT[i].PLYR,
                       //  PRIS = SRFT[i].PRIS,
                       //  GAPS = SRFT[i].GAPS,
                       //  ASSS = SRFT[i].ASSS,
                       //  PRIW = SRFT[i].PRIW,
                       //  GAPW = SRFT[i].GAPW,
                       //  ASSW = SRFT[i].ASSW
                   //  });
               //  }
           //  }
           //  _PLAY.SRFT = _PLAY.SRFT.ToList().OrderBy(s => s.PLYR).Cast<SRFT>().ToList();

            #endregion

            #region Get SETG for current play

            _PLAY.SETG = new List<SETG>();

            for (int i = 0; i < SETG.Count; i++)
            {
                if (SETG[i].SGF_ == (cbxAlignments.Items.Cast<SGFF>().ToList()).Find(item => item.name == "Norm").SGF_)
                {
                    _PLAY.SETG.Add(new SETG
                    {
                        rec = SETG[i].rec,
                        SETP = SETG[i].SETP,
                        SGF_ = SETG[i].SGF_,
                        SF__ = SETG[i].SF__,
                        x___ = SETG[i].x___,
                        y___ = SETG[i].y___,
                        fx__ = SETG[i].fx__,
                        fy__ = SETG[i].fy__,
                        anm_ = SETG[i].anm_,
                        dir_ = SETG[i].dir_,
                        fanm = SETG[i].fanm,
                        fdir = SETG[i].fdir,
                        active = true
                    });
                    if (_PLAY.SETG.Count == 11)
                    {
                        _PLAY.SETG = _PLAY.SETG.ToList().OrderBy(s => s.SETP).Cast<SETG>().ToList();
                        break;
                    }
                }
            }

            if (!(((SGFF)cbxAlignments.SelectedItem).name == "Norm"))
            {
                List<SETG> alignmentSETG =
                    (from setg in SETG
                     where setg.SGF_ == ((SGFF)cbxAlignments.SelectedItem).SGF_
                     select new SETG()
                     {
                         rec = setg.rec,
                         SETP = setg.SETP,
                         SGF_ = setg.SGF_,
                         SF__ = setg.SF__,
                         x___ = setg.x___,
                         y___ = setg.y___,
                         fx__ = setg.fx__,
                         fy__ = setg.fy__,
                         anm_ = setg.anm_,
                         dir_ = setg.dir_,
                         fanm = setg.fanm,
                         fdir = setg.fdir,
                         active = true
                     }).ToList().OrderBy(s => s.SETP).Cast<SETG>().ToList();

                _PLAY.SETG.Where(w => w.active == true).ToList().ForEach(f => f.active = false);

                int alignSETGindex;

                for (int i = 0; i < alignmentSETG.Count; i++)
                {
                    alignSETGindex = _PLAY.SETG.FindIndex(n => n.SETP == alignmentSETG[i].SETP);
                    _PLAY.SETG.RemoveAt(alignSETGindex);
                    _PLAY.SETG.Insert(alignSETGindex, new SETG()
                    {
                        rec = alignmentSETG[i].rec,
                        SETP = alignmentSETG[i].SETP,
                        SGF_ = alignmentSETG[i].SGF_,
                        SF__ = alignmentSETG[i].SF__,
                        x___ = alignmentSETG[i].x___,
                        y___ = alignmentSETG[i].y___,
                        fx__ = alignmentSETG[i].fx__,
                        fy__ = alignmentSETG[i].fy__,
                        anm_ = alignmentSETG[i].anm_,
                        dir_ = alignmentSETG[i].dir_,
                        fanm = alignmentSETG[i].fanm,
                        fdir = alignmentSETG[i].fdir,
                        active = alignmentSETG[i].active
                    });
                }
            }

            #endregion

            #region Get PSAL info for current play

            //Get PSAL steps for each poso
            for (int i = 0; i < _PLAY.posos.Count(); i++)
            {
                _PLAY.posos[i] = getSTEPs(_PLAY.PLYS[i].PSAL);
            }

            if (chbFlipPlay.Checked)
            {
                PSALroute[] tempPoso = (PSALroute[])_PLAY.posos.Clone();
                foreach (SETP poso in _PLAY.SETP)
                {
                    _PLAY.posos[poso.poso] = tempPoso[poso.flas];
                }
            }

            //Get player position for each poso
            for (int i = 0; i < _PLAY.posos.Count(); i++)
            {
                _PLAY.posos[i].playerXY = GetPlayerXY(_PLAY.SETG[i]);
            }

            //Set primary reciever route color
            if (((CPFM)cbxFormations.SelectedItem).FTYP == 1 || ((CPFM)cbxFormations.SelectedItem).FTYP == 2 || ((CPFM)cbxFormations.SelectedItem).FTYP == 3)
            {
                if (!chbFlipPlay.Checked)
                {
                    _PLAY.posos[_PLAY.PBPL[0].vpos].RouteColor = PSALColor.PrimaryRoute;
                }
                else
                {
                    _PLAY.posos[_PLAY.SETP.IndexOf(_PLAY.SETP.Where(f => f.flas == _PLAY.PBPL[0].vpos).FirstOrDefault())].RouteColor = PSALColor.PrimaryRoute;
                }
            }

            //Set Position names
            for (int i = 0; i < _PLAY.posos.Count(); i++)
            {
                _PLAY.posos[i].Position = _PLAY.PLYS[i].Position = GetPlayerPosition(_PLAY.SETP[i].EPos, _PLAY.SETP[i].DPos);
            }

            //Get SRFT Info
            // for (int i = 0; i < _PLAY.SRFT.Count(); i++)
            // {
               //  _PLAY.posos[_PLAY.SRFT[i].PLYR].SRFT = new SRFT
               //  {
                   // SIDE = _PLAY.SRFT[i].SIDE,
                   // YOFF = _PLAY.SRFT[i].YOFF,
                   // TECH = _PLAY.SRFT[i].TECH,
                   // PLYL = _PLAY.SRFT[i].PLYL,
                   // STAN = _PLAY.SRFT[i].STAN,
                   // PLYR = _PLAY.SRFT[i].PLYR,
                   // PRIS = _PLAY.SRFT[i].PRIS,
                   // GAPS = _PLAY.SRFT[i].GAPS,
                   // ASSS = _PLAY.SRFT[i].ASSS,
                   // PRIW = _PLAY.SRFT[i].PRIW,
                   // GAPW = _PLAY.SRFT[i].GAPW,
                   // ASSW = _PLAY.SRFT[i].ASSW
                //};
            //}

            #endregion

            #region Get ARTL info for current play

            if (((CPFM)cbxFormations.SelectedItem).FTYP == 1 || ((CPFM)cbxFormations.SelectedItem).FTYP == 2 || ((CPFM)cbxFormations.SelectedItem).FTYP == 3)
            {
                for (int i = 0; i < _PLAY.posos.Count(); i++)
                {
                    _PLAY.posos[i].aRTL = ARTO[ARTO.FindIndex(artl => artl.artl == _PLAY.PLYS[i].ARTL)];
                    _PLAY.posos[i].CPFMtype = "Offense";

                    _PLAY.posos[i].fmtx = _PLAY.SETP[i].fmtx;
                    _PLAY.posos[i].artx = _PLAY.SETP[i].artx;
                    _PLAY.posos[i].fmty = _PLAY.SETP[i].fmty;
                    _PLAY.posos[i].arty = _PLAY.SETP[i].arty;
                }
            }

            if (((CPFM)cbxFormations.SelectedItem).FTYP == 11 || ((CPFM)cbxFormations.SelectedItem).FTYP == 12 || ((CPFM)cbxFormations.SelectedItem).FTYP == 13)
            {
                for (int i = 0; i < _PLAY.posos.Count(); i++)
                {
                    _PLAY.posos[i].aRTL = ARTD[ARTD.FindIndex(artl => artl.artl == _PLAY.PLYS[i].ARTL)];
                    _PLAY.posos[i].CPFMtype = "Defense";

                    _PLAY.posos[i].fmtx = _PLAY.SETP[i].fmtx;
                    _PLAY.posos[i].artx = _PLAY.SETP[i].artx;
                    _PLAY.posos[i].fmty = _PLAY.SETP[i].fmty;
                    _PLAY.posos[i].arty = _PLAY.SETP[i].arty;
                }
            }

            Console.WriteLine(_PLAY.PBPL[0].PLYL);

            #endregion

            return _PLAY;
        }

        public void SetPlay(PLAY Play)
        {
            foreach (PLYS item in Play.PLYS)
            {
                PLYS[item.rec].PSAL = item.PSAL;
                PLYS[item.rec].ARTL = item.ARTL;
                PLYS[item.rec].PLRR = item.PLRR;
                PLYS[item.rec].poso = item.poso;
            }

            foreach (SETL item in Play.SETL)
            {
                SETL[item.rec].setl = item.setl;
                SETL[item.rec].FORM = item.FORM;
                SETL[item.rec].MOTN = item.MOTN;
                SETL[item.rec].CLAS = item.CLAS;
                SETL[item.rec].SETT = item.SETT;
                SETL[item.rec].SITT = item.SITT;
                SETL[item.rec].SLF_ = item.SLF_;
                SETL[item.rec].name = item.name;
                SETL[item.rec].poso = item.poso;
            }

            foreach (PBPL item in Play.PBPL)
            {
                PBPL[item.rec].COMF = item.COMF;
                PBPL[item.rec].SRMM = item.SRMM;
                PBPL[item.rec].SITT = item.SITT;
                PBPL[item.rec].PLYT = item.PLYT;
                PBPL[item.rec].PLF_ = item.PLF_;
                PBPL[item.rec].name = item.name;
                PBPL[item.rec].risk = item.risk;
                PBPL[item.rec].motn = item.motn;
                PBPL[item.rec].vpos = item.vpos;
            }

            foreach (SETP item in Play.SETP)
            {
                //SETP[item.rec].SETL = item.SETL;
                SETP[item.rec].setp = item.setp;
                SETP[item.rec].SGT_ = item.SGT_;
                SETP[item.rec].arti = item.arti;
                SETP[item.rec].opnm = item.opnm;
                SETP[item.rec].tabo = item.tabo;
                SETP[item.rec].poso = item.poso;
                SETP[item.rec].odep = item.odep;
                SETP[item.rec].flas = item.flas;
                SETP[item.rec].DPos = item.DPos;
                SETP[item.rec].EPos = item.EPos;
                SETP[item.rec].fmtx = item.fmtx;
                SETP[item.rec].artx = item.artx;
                SETP[item.rec].fmty = item.fmty;
                SETP[item.rec].arty = item.arty;
            }

            foreach (SETG item in Play.SETG)
            {
                //SETG[item.rec].SETP = item.SETP;
                //SETG[item.rec].SGF_ = item.SGF_;
                SETG[item.rec].SF__ = item.SF__;
                SETG[item.rec].x___ = item.x___;
                SETG[item.rec].y___ = item.y___;
                SETG[item.rec].fx__ = item.fx__;
                SETG[item.rec].fy__ = item.fy__;
                SETG[item.rec].anm_ = item.anm_;
                SETG[item.rec].dir_ = item.dir_;
                SETG[item.rec].fanm = item.fanm;
                SETG[item.rec].fdir = item.fdir;
            }

            if (Play.PLPD.Count > 0)
            {
                foreach (PLPD item in Play.PLPD)
                {
                    PLPD[item.rec].com1 = item.com1;
                    PLPD[item.rec].con1 = item.con1;
                    PLPD[item.rec].per1 = item.per1;
                    PLPD[item.rec].rcv1 = item.rcv1;
                    PLPD[item.rec].com2 = item.com2;
                    PLPD[item.rec].con2 = item.con2;
                    PLPD[item.rec].per2 = item.per2;
                    PLPD[item.rec].rcv2 = item.rcv2;
                    PLPD[item.rec].com3 = item.com3;
                    PLPD[item.rec].con3 = item.con3;
                    PLPD[item.rec].per3 = item.per3;
                    PLPD[item.rec].rcv3 = item.rcv3;
                    PLPD[item.rec].com4 = item.com4;
                    PLPD[item.rec].con4 = item.con4;
                    PLPD[item.rec].per4 = item.per4;
                    PLPD[item.rec].rcv4 = item.rcv4;
                    PLPD[item.rec].com5 = item.com5;
                    PLPD[item.rec].con5 = item.con5;
                    PLPD[item.rec].per5 = item.per5;
                    PLPD[item.rec].rcv5 = item.rcv5;
                    PLPD[item.rec].PLYL = item.PLYL;
                }
            }

            if (Play.PLRD.Count > 0)
            {
                foreach (PLRD item in Play.PLRD)
                {
                    PLRD[item.rec].PLYL = item.PLYL;
                    PLRD[item.rec].hole = item.hole;
                }

            }

            // if (Play.SRFT.Count > 0)
            // {
                // foreach (SRFT item in Play.SRFT)
                // {
                    // SRFT[item.rec].SIDE = item.SIDE;
                    // SRFT[item.rec].YOFF = item.YOFF;
                    // SRFT[item.rec].TECH = item.TECH;
                    // SRFT[item.rec].PLYL = item.PLYL;
                    // SRFT[item.rec].STAN = item.STAN;
                    // SRFT[item.rec].PLYR = item.PLYR;
                    // SRFT[item.rec].PRIS = item.PRIS;
                    // SRFT[item.rec].GAPS = item.GAPS;
                    // SRFT[item.rec].ASSS = item.ASSS;
                    // SRFT[item.rec].PRIW = item.PRIW;
                    // SRFT[item.rec].GAPW = item.GAPW;
                    // SRFT[item.rec].ASSW = item.ASSW;
                // }
            // }
        }

        public List<PLYS> GetPLYL()
        {
            List<PLYS> _PLYL = new List<PLYS>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "PLYS").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                _PLYL.Add(new PLYS
                {
                    rec = i,
                    PSAL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLYS"), StrReverse("PSAL"), i),
                    ARTL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLYS"), StrReverse("ARTL"), i),
                    PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLYS"), StrReverse("PLYL"), i),
                    PLRR = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLYS"), StrReverse("PLRR"), i),
                    poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PLYS"), StrReverse("poso"), i)
                });
            }
            return _PLYL;
        }

        private List<SETP> GetSETP()
        {
            List<SETP> _SETP = new List<SETP>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "SETP").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                _SETP.Add(new SETP
                {
                    rec = i,
                    SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("SETL"), i),
                    setp = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("SETP"), i),
                    SGT_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("SGT_"), i),
                    arti = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("arti"), i),
                    opnm = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("opnm"), i),
                    tabo = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("tabo"), i),
                    poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("poso"), i),
                    odep = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("odep"), i),
                    flas = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("flas"), i),
                    DPos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("DPos"), i),
                    EPos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("EPos"), i),
                    fmtx = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("fmtx"), i),
                    artx = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("artx"), i),
                    fmty = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("fmty"), i),
                    arty = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("arty"), i)
                });
            }
            return _SETP;
        }

        private List<SETG> GetSETG()
        {
            List<SETG> _SETG = new List<SETG>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "SETG").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                _SETG.Add(new SETG
                {
                    rec = i,
                    SETP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("SETP"), i),
                    SGF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("SGF_"), i),
                    SF__ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("SF__"), i),
                    x___ = (float)TDB.TDBFieldGetValueAsFloat(OpenIndex, StrReverse("SETG"), StrReverse("x___"), i),
                    y___ = (float)TDB.TDBFieldGetValueAsFloat(OpenIndex, StrReverse("SETG"), StrReverse("y___"), i),
                    fx__ = (float)TDB.TDBFieldGetValueAsFloat(OpenIndex, StrReverse("SETG"), StrReverse("fx__"), i),
                    fy__ = (float)TDB.TDBFieldGetValueAsFloat(OpenIndex, StrReverse("SETG"), StrReverse("fy__"), i),
                    anm_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("anm_"), i),
                    dir_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("dir_"), i),
                    fanm = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("fanm"), i),
                    fdir = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("fdir"), i)
                });
            }
            return _SETG;
        }

        private List<SGFF> GetSGFF()
        {
            List<SGFF> _SGFF = new List<SGFF>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "SGFF").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);

                TDB.TDBFieldGetProperties(OpenIndex, StrReverse("SGFF"), 2, ref FieldProps);

                string _name = new string((char)0, (FieldProps.Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(OpenIndex, StrReverse("SGFF"), StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                _SGFF.Add(new SGFF
                {
                    rec = i,
                    SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SGFF"), StrReverse("SETL"), i),
                    SGF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SGFF"), StrReverse("SGF_"), i),
                    name = _name,
                    dflt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SGFF"), StrReverse("dflt"), i)
                });
            }
            return _SGFF;
        }

        // private List<SRFT> GetSRFT()
        // {
           // List<SRFT> _SRFT = new List<SRFT>();

           // TdbTableProperties TableProps = new TdbTableProperties();
           // TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
          // if (!TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "SRFT").rec, ref TableProps))
              //  return null;

            // for (int i = 0; i < TableProps.RecordCount; i++)
           // {
                // _SRFT.Add(new SRFT
               // {
                   //  rec = i,
                    // SIDE = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("SIDE"), i),
                    // YOFF = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("YOFF"), i),
                    // TECH = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("TECH"), i),
                    // PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("PLYL"), i),
                    // STAN = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("STAN"), i),
                    // PLYR = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("PLYR"), i),
                    // PRIS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("PRIS"), i),
                    // GAPS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("GAPS"), i),
                    // ASSS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("ASSS"), i),
                    // PRIW = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("PRIW"), i),
                    // GAPW = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("GAPW"), i),
                    // ASSW = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SRFT"), StrReverse("ASSW"), i)
                // });
           // }
            // return _SRFT;
       //  }

        private List<SETP> GetFormationSETP()
        {
            List<SETP> _SETP = new List<SETP>();

            for (int i = 0; i < SETP.Count; i++)
            {
                if (SETP[i].SETL == ((SETL)cbxSubFormations.SelectedItem).setl)
                {
                    _SETP.Add(new SETP
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("SETL"), i),
                        setp = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("SETP"), i),
                        SGT_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("SGT_"), i),
                        arti = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("arti"), i),
                        opnm = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("opnm"), i),
                        tabo = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("tabo"), i),
                        poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("poso"), i),
                        odep = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("odep"), i),
                        flas = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("flas"), i),
                        DPos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("DPos"), i),
                        EPos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("EPos"), i),
                        fmtx = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("fmtx"), i),
                        artx = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("artx"), i),
                        fmty = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("fmty"), i),
                        arty = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETP"), StrReverse("arty"), i),
                    });
                }
            }

            return _SETP;
        }

        private List<SETG> GetFormationSETG(List<SETP> _SETP)
        {
            List<SETG> _SETG = new List<SETG>();

            for (int p = 0; p < Play.SETP.Count; p++)
            {
                for (int g = 0; g < SETG.Count; g++)
                {
                    if (SETG[g].SETP == _SETP[p].setp)
                    {
                        _SETG.Add(new SETG
                        {
                            rec = g,
                            SETP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("SETP"), g),
                            SGF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("SGF_"), g),
                            SF__ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("SF__"), g),
                            x___ = (float)TDB.TDBFieldGetValueAsFloat(OpenIndex, StrReverse("SETG"), StrReverse("x___"), g),
                            y___ = (float)TDB.TDBFieldGetValueAsFloat(OpenIndex, StrReverse("SETG"), StrReverse("y___"), g),
                            fx__ = (float)TDB.TDBFieldGetValueAsFloat(OpenIndex, StrReverse("SETG"), StrReverse("fx__"), g),
                            fy__ = (float)TDB.TDBFieldGetValueAsFloat(OpenIndex, StrReverse("SETG"), StrReverse("fy__"), g),
                            anm_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("anm_"), g),
                            dir_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("dir_"), g),
                            fanm = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("fanm"), g),
                            fdir = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SETG"), StrReverse("fdir"), g),
                        });
                    }
                }
            }

            return _SETG;
        }

        private List<SGFF> GetFormationSGFF()
        {
            List<SGFF> _SGFF = new List<SGFF>();

            for (int i = 0; i < SGFF.Count; i++)
            {
                if (SGFF[i].SETL == ((SETL)cbxSubFormations.SelectedItem).setl)
                {
                    TdbFieldProperties FieldProps = new TdbFieldProperties();
                    FieldProps.Name = new string((char)0, 5);

                    TDB.TDBFieldGetProperties(OpenIndex, StrReverse("SGFF"), 2, ref FieldProps);

                    string _name = new string((char)0, (FieldProps.Size / 8) + 1);

                    TDB.TDBFieldGetValueAsString(OpenIndex, StrReverse("SGFF"), StrReverse("name"), i, ref _name);
                    _name = _name.Replace(",", "");

                    _SGFF.Add(new SGFF
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SGFF"), StrReverse("SETL"), i),
                        SGF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SGFF"), StrReverse("SGF_"), i),
                        name = _name,
                        dflt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("SGFF"), StrReverse("dflt"), i),
                    });
                }
            }

            return _SGFF;
        }

        private List<PSAL> GetPSLO()
        {
            List<PSAL> PSLO = new List<PSAL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "PSLO").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                PSLO.Add(new PSAL
                {
                    rec = i,
                    val1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PSLO"), StrReverse("val1"), i),
                    val2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PSLO"), StrReverse("val2"), i),
                    val3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PSLO"), StrReverse("val3"), i),
                    psal = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PSLO"), StrReverse("PSAL"), i),
                    code = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PSLO"), StrReverse("code"), i),
                    step = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PSLO"), StrReverse("step"), i)
                });
            }
            return PSLO;
        }

        private List<PSAL> GetPSLD()
        {
            List<PSAL> PSLD = new List<PSAL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "PSLD").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                PSLD.Add(new PSAL
                {
                    rec = i,
                    val1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PSLD"), StrReverse("val1"), i),
                    val2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PSLD"), StrReverse("val2"), i),
                    val3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PSLD"), StrReverse("val3"), i),
                    psal = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PSLD"), StrReverse("PSAL"), i),
                    code = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PSLD"), StrReverse("code"), i),
                    step = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("PSLD"), StrReverse("step"), i)
                });
            }
            return PSLD;
        }

        public List<PLRR> GetPSAL_PLRR_ARTL()
        {
            List<PLRR> PLRR = new List<PLRR>();
            List<CPFM> RelevantFormations;
            List<SETL> RelevantSubFormations;
            List<PBPL> RelevantPlays;
            List<PLYS> RelevantPSALs;
            List<PSAL> PSLO_Unique;
            List<PSAL> PSLD_Unique;

            int index;

            if (((CPFM)cbxFormations.SelectedItem).FTYP == 1 || ((CPFM)cbxFormations.SelectedItem).FTYP == 2 || ((CPFM)cbxFormations.SelectedItem).FTYP == 3)
            {
                RelevantFormations = CPFM.Where(form => form.FTYP == 1 || form.FTYP == 2 || form.FTYP == 3).ToList();
                RelevantSubFormations = (from sub in SETL
                                         join form in RelevantFormations on sub.FORM equals form.FORM
                                         select sub).ToList();
                RelevantPlays = (from play in PBPL
                                 join sub in RelevantSubFormations on play.SETL equals sub.setl
                                 select play).ToList();
                RelevantPSALs = (from psal in PLYS
                                 join play in RelevantPlays on psal.PLYL equals play.PLYL
                                 select psal).Distinct().ToList().OrderBy(s => s.PLRR).ThenBy(s => s.PSAL).Cast<PLYS>().ToList();
                PSLO_Unique = PSLO.Distinct().ToList().OrderBy(s => s.psal).Cast<PSAL>().ToList();
                foreach (PSAL psal in PSLO_Unique)
                {
                    try
                    {
                        index = RelevantPSALs.FindIndex(x => x.PSAL == psal.psal);
                        PLRR.Add(new PLRR() { psal = RelevantPSALs[index].PSAL, artl = RelevantPSALs[index].ARTL, plrr = RelevantPSALs[index].PLRR, name = PLRRname.PLRRnameDic[RelevantPSALs[index].PLRR] });
                    }
                    catch
                    {
                        PLRR.Add(new PLRR() { psal = psal.psal, artl = 0, plrr = 0, name = "UNUSED PSALS" });
                    }
                }
                PLRR = PLRR.ToList().OrderBy(s => s.name).ThenBy(s => s.psal).Cast<PLRR>().ToList();
            }

            if (((CPFM)cbxFormations.SelectedItem).FTYP == 11 || ((CPFM)cbxFormations.SelectedItem).FTYP == 12 || ((CPFM)cbxFormations.SelectedItem).FTYP == 13)
            {
                RelevantFormations = CPFM.Where(form => form.FTYP == 11 || form.FTYP == 12 || form.FTYP == 13).ToList();
                RelevantSubFormations = (from sub in SETL
                                         join form in RelevantFormations on sub.FORM equals form.FORM
                                         select sub).ToList();
                RelevantPlays = (from play in PBPL
                                 join sub in RelevantSubFormations on play.SETL equals sub.setl
                                 select play).ToList();
                RelevantPSALs = (from psal in PLYS
                                 join play in RelevantPlays on psal.PLYL equals play.PLYL
                                 select psal).Distinct().ToList().OrderBy(s => s.PLRR).ThenBy(s => s.PSAL).Cast<PLYS>().ToList();
                PSLD_Unique = PSLD.Distinct().ToList().OrderBy(s => s.psal).Cast<PSAL>().ToList();
                foreach (PSAL psal in PSLD_Unique)
                {
                    try
                    {
                        index = RelevantPSALs.FindIndex(x => x.PSAL == psal.psal);
                        PLRR.Add(new PLRR() { psal = RelevantPSALs[index].PSAL, artl = RelevantPSALs[index].ARTL, plrr = RelevantPSALs[index].PLRR, name = PLRRname.PLRRnameDic[RelevantPSALs[index].PLRR] });
                    }
                    catch
                    {
                        PLRR.Add(new PLRR() { psal = psal.psal, artl = 0, plrr = 0, name = "UNUSED PSALS" });
                    }
                }
                PLRR = PLRR.ToList().OrderBy(s => s.plrr).ThenBy(s => s.psal).Cast<PLRR>().ToList();
            }

            return PLRR;
        }

        private List<ARTL> GetARTD()
        {
            List<ARTL> _ARTD = new List<ARTL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "ARTD").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                _ARTD.Add(new ARTL
                {
                    rec = i,
                    artl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ARTL"), i),
                    acnt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("acnt"), i),

                    sp = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("sp01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("sp02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("sp03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("sp04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("sp05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("sp06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("sp07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("sp08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("sp09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("sp10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("sp11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("sp12"), i)
                    },

                    ls = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ls01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ls02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ls03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ls04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ls05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ls06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ls07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ls08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ls09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ls10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ls11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ls12"), i)
                    },

                    at = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("at01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("at02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("at03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("at04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("at05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("at06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("at07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("at08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("at09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("at10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("at11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("at12"), i)
                    },

                    ct = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ct01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ct02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ct03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ct04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ct05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ct06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ct07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ct08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ct09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ct10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ct11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ct12"), i)
                    },

                    lt = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("lt01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("lt02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("lt03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("lt04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("lt05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("lt06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("lt07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("lt08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("lt09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("lt10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("lt11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("lt12"), i)
                    },

                    au = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("au01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("au02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("au03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("au04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("au05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("au06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("au07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("au08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("au09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("au10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("au11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("au12"), i)
                    },

                    av = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("av01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("av02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("av03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("av04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("av05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("av06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("av07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("av08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("av09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("av10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("av11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("av12"), i)
                    },

                    ax = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ax01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ax02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ax03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ax04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ax05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ax06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ax07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ax08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ax09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ax10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ax11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ax12"), i)
                    },

                    ay = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ay01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ay02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ay03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ay04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ay05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ay06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ay07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ay08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ay09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ay10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ay11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTD"), StrReverse("ay12"), i)
                    },
                });
            }
            return _ARTD;
        }

        private List<ARTL> GetARTO()
        {
            List<ARTL> _ARTO = new List<ARTL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(OpenIndex, TableNames.Find(item => item.name == "ARTO").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                _ARTO.Add(new ARTL
                {
                    rec = i,
                    artl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ARTL"), i),
                    acnt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("acnt"), i),

                    sp = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("sp01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("sp02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("sp03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("sp04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("sp05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("sp06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("sp07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("sp08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("sp09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("sp10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("sp11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("sp12"), i)
                    },

                    ls = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ls01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ls02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ls03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ls04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ls05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ls06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ls07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ls08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ls09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ls10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ls11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ls12"), i)
                    },

                    at = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("at01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("at02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("at03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("at04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("at05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("at06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("at07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("at08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("at09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("at10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("at11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("at12"), i)
                    },

                    ct = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ct01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ct02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ct03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ct04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ct05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ct06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ct07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ct08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ct09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ct10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ct11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ct12"), i)
                    },

                    lt = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("lt01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("lt02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("lt03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("lt04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("lt05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("lt06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("lt07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("lt08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("lt09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("lt10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("lt11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("lt12"), i)
                    },

                    au = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("au01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("au02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("au03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("au04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("au05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("au06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("au07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("au08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("au09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("au10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("au11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("au12"), i)
                    },

                    av = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("av01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("av02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("av03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("av04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("av05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("av06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("av07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("av08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("av09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("av10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("av11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("av12"), i)
                    },

                    ax = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ax01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ax02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ax03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ax04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ax05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ax06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ax07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ax08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ax09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ax10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ax11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ax12"), i)
                    },

                    ay = new int[12]
                    {
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ay01"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ay02"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ay03"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ay04"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ay05"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ay06"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ay07"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ay08"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ay09"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ay10"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ay11"), i),
                        (int)(UInt32)TDB.TDBFieldGetValueAsInteger(OpenIndex, StrReverse("ARTO"), StrReverse("ay12"), i)
                    },
                });
            }
            return _ARTO;
        }

        #endregion

        public void ClearField()
        {
            //Clear the canvas
            picPlay.Image = blankField;

            //Create a clone of the field to draw the formation
            gImage = (Image)blankField.Clone();
        }

        private PSALroute getSTEPs(int PSALposo)
        {
            //Return all PSAL entries for given PSALposo (PSAL ID), sorted by steps
            PSALroute pSALroute = new PSALroute();

            if (((CPFM)cbxFormations.SelectedItem).FTYP == 1 || ((CPFM)cbxFormations.SelectedItem).FTYP == 2 || ((CPFM)cbxFormations.SelectedItem).FTYP == 3)
            {
                pSALroute.steps = (from row in PSLO
                                   where row.psal == PSALposo
                                   select new PSALroute.Steps()
                                   {
                                       rec = row.rec,
                                       val1 = row.val1,
                                       val2 = row.val2,
                                       val3 = row.val3,
                                       PSAL = row.psal,
                                       code = row.code,
                                       step = row.step
                                   }).ToList().OrderBy(s => s.step).Cast<PSALroute.Steps>().ToList();
            }
            else if (((CPFM)cbxFormations.SelectedItem).FTYP == 11 || ((CPFM)cbxFormations.SelectedItem).FTYP == 12 || ((CPFM)cbxFormations.SelectedItem).FTYP == 13)
            {
                pSALroute.steps = (from row in PSLD
                                   where row.psal == PSALposo
                                   select new PSALroute.Steps()
                                   {
                                       rec = row.rec,
                                       val1 = row.val1,
                                       val2 = row.val2,
                                       val3 = row.val3,
                                       PSAL = row.psal,
                                       code = row.code,
                                       step = row.step
                                   }).ToList().OrderBy(s => s.step).Cast<PSALroute.Steps>().ToList();
            }

            return pSALroute;
        }

        private XY GetPlayerXY(SETG setg)
        {
            XY playerXY;
            if (chbFlipPlay.Checked)
            {
                playerXY = new XY { X = (int)(setg.fx__ * 10), Y = (int)(setg.fy__ * -10) };
            }
            else
            {
                playerXY = new XY { X = (int)(setg.x___ * 10), Y = (int)(setg.y___ * -10) };
            }
            playerXY.X = playerXY.X + LOS.X;
            playerXY.Y = playerXY.Y + LOS.Y;
            return playerXY;
        }

        private string GetPlayerPosition(int Epos, int Dpos)
        {
            string Position = "";

            switch (Epos)
            {
                case 0:
                    Position = "QB";
                    break;
                case 1:
                    Position = "HB" + Dpos.ToString();
                    break;
                case 2:
                    Position = "FB" + Dpos.ToString();
                    break;
                case 3:
                    Position = "WR" + Dpos.ToString();
                    break;
                case 4:
                    Position = "TE" + Dpos.ToString();
                    break;
                case 5:
                    Position = "T";
                    break;
                case 6:
                    Position = "G";
                    break;
                case 7:
                    Position = "C";
                    break;
                case 8:
                    Position = "G";
                    break;
                case 9:
                    Position = "T";
                    break;
                case 10:
                    Position = "LE";
                    break;
                case 11:
                    Position = "RE";
                    break;
                case 12:
                    Position = "DT" + Dpos.ToString();
                    break;
                case 13:
                    Position = "LOLB";
                    break;
                case 14:
                    Position = "MLB" + Dpos.ToString();
                    break;
                case 15:
                    Position = "ROLB";
                    break;
                case 16:
                    Position = "CB" + Dpos.ToString();
                    break;
                case 17:
                    Position = "FS";
                    break;
                case 18:
                    Position = "SS";
                    break;
                case 19:
                    Position = "K";
                    break;
                case 20:
                    Position = "P";
                    break;
                case 21:
                    Position = "KR";
                    break;
                case 22:
                    Position = "PR";
                    break;
                case 23:
                    Position = "KOS";
                    break;
                case 24:
                    Position = "LS";
                    break;
                case 25:
                    Position = "3RB";
                    break;
                case 26:
                    Position = "PRB";
                    break;
                case 27:
                    Position = "SWR";
                    break;
                case 28:
                    Position = "RLE";
                    break;
                case 29:
                    Position = "RRE";
                    break;
                case 30:
                    Position = "RDT" + Dpos.ToString();
                    break;
                case 31:
                    Position = "SLB" + Dpos.ToString();
                    break;
                case 32:
                    Position = "SCB";
                    break;
            }

            return Position;
        }

        public void DrawPlayartPSAL(Graphics g, PLAY play)
        {
            //Draw Offense PSALs in order of type (Blocking, Base Route, Primary Reciever, Delay Route, Motion Route, QB Rollout/Scramble, Run)
            if (((CPFM)cbxFormations.SelectedItem).FTYP == 1 || ((CPFM)cbxFormations.SelectedItem).FTYP == 2 || ((CPFM)cbxFormations.SelectedItem).FTYP == 3)
            {
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.Block)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.QBScramble)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.QBHandoff)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.BaseRoute)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.PrimaryRoute)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.DelayRoute)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.MotionRoute)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.Run)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.Kickoff)))
                    Program.DrawPSAL(g, poso);
            }

            //Draw Defense PSALs in order of type (Deep Zone, Flat, Hook, RushQB)
            else if (((CPFM)cbxFormations.SelectedItem).FTYP == 11 || ((CPFM)cbxFormations.SelectedItem).FTYP == 12 || ((CPFM)cbxFormations.SelectedItem).FTYP == 13)
            {
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.DeepZone)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.CloudFlat)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.HardFlat)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.SoftSquat)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.CurlFlat)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.SeamFlat)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.QuarterFlat)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.HookCurl)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.VertHook)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.ThreeReceiverHook)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.MidRead)))
                    Program.DrawPSAL(g, poso);
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.RushQB)))
                    Program.DrawPSAL(g, poso);
            }

            //draw player highlight below icons
            foreach (PSALroute poso in play.posos.Where(h => h.Highlighted)) Program.DrawPSALHighlight(g, 12, poso.playerXY.X, poso.playerXY.Y, 1);

            //draw player icons
            foreach (PSALroute poso in play.posos) Program.DrawPSALIcon(g, Color.White, poso, 12, LOS);

            picPlay.Image = gImage;
            g.Dispose();
        }
        private void cmsEditPSAL_Copy_Click(object sender, EventArgs e)
        {
            cmsEditPSAL_Paste.Enabled = true;
            PlayCopy = Play.Clone();
            List<PLYS> PLYS_Copy = new List<PLYS>();
            for (int poso = 0; poso <= 10; poso++)
            {
                foreach (DataGridViewCell cell in dgvPLYS.SelectedCells)
                {
                    if (cell.RowIndex == poso)
                    {
                        PLYS_Copy.Add(PlayCopy.PLYS[poso]);
                        break;
                    }
                }
            }
            PlayCopy.PLYS = PLYS_Copy;
        }

        private void cmsEditPSAL_Paste_Click(object sender, EventArgs e)
        {
            foreach (PLYS ply in PlayCopy.PLYS)
            {
                Play.PLYS[ply.poso].PSAL = PlayCopy.PLYS.Where(plys => plys.poso == ply.poso).FirstOrDefault().PSAL;
                Play.PLYS[ply.poso].ARTL = PlayCopy.PLYS.Where(plys => plys.poso == ply.poso).FirstOrDefault().ARTL;
                Play.PLYS[ply.poso].PLRR = PlayCopy.PLYS.Where(plys => plys.poso == ply.poso).FirstOrDefault().PLRR;
            }

            SetPlay(Play);
            Play = GetPlay();
            UpdatePlay(Play);
        }


        private void mnuView_Morphy_Click(object sender, EventArgs e)
        {
            {
                string url = "https://docs.google.com/spreadsheets/d/1TiC_zqTpr_KPj8m22fECYZVXJRNZPJNg8aLlAuXoI4k/edit#gid=53391816";
                gotoSite(url);
                // Open's Morphy's Doc

            }
        }

        private void mnuView_Click(object sender, EventArgs e)
        {

        }

        private void mnuView_PBModding_Click(object sender, EventArgs e)
        {
            {
                string url = "https://discord.com/channels/483324610968944651/519202665448734723";
                gotoSite(url);
                // Opens MMC # playbook-modding channel

            }

        }

        private void frmPlaybook_Load(object sender, EventArgs e)
        {

        }

        private void mnuPlaybook_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dgvPLYS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.C))
            {
                cmsEditPSAL_Copy_Click(sender, e);
            }
            else if (e.KeyData == (Keys.Control | Keys.V))
            {
                cmsEditPSAL_Paste_Click(sender, e);
            }
            else if (e.KeyData == (Keys.Control | Keys.S))
            {
                mnuFile_Save_Click(sender, e);
            }
        }

        private void DrawPlayartARTL(PLAY Play, bool Highlight)
        {
            bmpARTL = new Bitmap(picARTL.Width, picARTL.Height);
            Graphics g = Graphics.FromImage(bmpARTL);
            g.Clear(Color.Transparent);

            //Convert ARTLs
            for (int i = 0; i < Play.posos.Count(); i++)
                Play.posos[i] = Program.ConvertARTL(g, Play.posos[i], new Point(Play.posos[i].artx, Play.posos[i].arty));

            //Draw player highlight below icons
            if (Highlight)
                foreach (PSALroute poso in Play.posos.Where(h => h.Highlighted))
                    Program.DrawARTLHighlight(g, 30f, poso.artx, poso.arty);

            //Draw Offense ARTL in order of type (Blocking, Base Route, Primary Reciever, Delay Route, Motion Route, QB Rollout/Scramble, Run)
            if (((CPFM)cbxFormations.SelectedItem).FTYP == 1 || ((CPFM)cbxFormations.SelectedItem).FTYP == 2 || ((CPFM)cbxFormations.SelectedItem).FTYP == 3)
            {
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.Block)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.QBScramble)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.QBHandoff)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.BaseRoute)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.PrimaryRoute)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.DelayRoute)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.MotionRoute)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.Run)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.Kickoff)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
            }

            //Draw Defense ARTL in order of type (Deep Zone, Flat, Hook, RushQB)
            else if (((CPFM)cbxFormations.SelectedItem).FTYP == 11 || ((CPFM)cbxFormations.SelectedItem).FTYP == 12 || ((CPFM)cbxFormations.SelectedItem).FTYP == 13)
            {
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.RushQB)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.DeepZone)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.CloudFlat)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.HardFlat)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.SoftSquat)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.CurlFlat)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.SeamFlat)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.QuarterFlat)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.HookCurl)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.VertHook)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.ThreeReceiverHook)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.MidRead)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
                foreach (PSALroute poso in Play.posos.Where(poso => poso.RouteColor.Equals(PSALColor.QBSpy)))
                    Program.DrawARTL(g, poso, new Point(poso.artx, poso.arty));
            }

            foreach (PSALroute poso in Play.posos)
            Program.DrawARTLIcon(g, poso, new Point(poso.artx, poso.arty));

            picARTL.Image = bmpARTL;
        }
    }
}