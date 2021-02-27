using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Playbook_Editor
{
    public partial class frmEditFormation : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private frmPlaybook playbookFormRef;
        public List<SETL> DefaultSETL;
        public List<SETP> DefaultSETP;
        public List<SETG> DefaultSETG;

        public frmEditFormation(frmPlaybook playbookFormHandle)
        {
            playbookFormRef = playbookFormHandle;
            InitializeComponent();
            AddDrag(mnuPBPL);
        }

        private void frmEditFormation_Load(object sender, EventArgs e)
        {
            playbookFormRef.cbxFormations.Enabled = false;
            playbookFormRef.cbxSubFormations.Enabled = false;
            playbookFormRef.cbxAlignments.Enabled = false;
            playbookFormRef.btnEditFormation.Enabled = false;
            playbookFormRef.btnEditPlayData.Enabled = false;
            playbookFormRef.chbFlipPlay.Enabled = false;

            dgvSETL.DataSource = playbookFormRef.Play.SETL;

            dgvSETL.Columns["rec"].Visible = false;
            dgvSETL.Columns["SETL"].Visible = false;

            dgvSETL.AutoResizeColumns();
            Program.ResizeDataGrid(dgvSETL);

            dgvSETP.DataSource = playbookFormRef.Play.SETP;

            dgvSETP.Columns["rec"].Visible = false;
            dgvSETP.Columns["SETL"].Visible = false;
            //dgvSETP.Columns["SETP"].Visible = false;
            dgvSETP.Columns["poso"].Visible = false;

            dgvSETP.AutoResizeColumns();
            Program.ResizeDataGrid(dgvSETP);

            dgvSETG.DataSource = playbookFormRef.Play.SETG;

            dgvSETG.Columns["active"].Visible = false;
            dgvSETG.Columns["rec"].Visible = false;
            dgvSETG.Columns["SETP"].Visible = false;
            dgvSETG.Columns["SGF_"].Visible = false;

            dgvSETG.AutoResizeColumns();
            Program.ResizeDataGrid(dgvSETG);

            for (int i = 0; i < playbookFormRef.Play.SETG.Count; i++)
            {
                if (!(playbookFormRef.Play.SETG[i].active))
                {
                    dgvSETG.Rows[i].ReadOnly = true;
                    dgvSETG.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    dgvSETG.Rows[i].DefaultCellStyle.SelectionBackColor = Color.White;
                    dgvSETG.Rows[i].DefaultCellStyle.ForeColor = Color.DarkGray;
                    dgvSETG.Rows[i].DefaultCellStyle.SelectionForeColor = Color.DarkGray;
                }
            }
        }

        private void frmEditFormation_FormClosed(object sender, FormClosedEventArgs e)
        {
            playbookFormRef.cbxFormations.Enabled = true;
            playbookFormRef.cbxSubFormations.Enabled = true;
            playbookFormRef.btnEditFormation.Enabled = true;
            playbookFormRef.btnEditPlayData.Enabled = true;
            if (playbookFormRef.mnuOptions_Playart_PSAL.Checked)
            {
                playbookFormRef.chbFlipPlay.Enabled = true;
                playbookFormRef.cbxAlignments.Enabled = true;
            }
            else
            {
                playbookFormRef.chbFlipPlay.Enabled = false;
                playbookFormRef.cbxAlignments.Enabled = false;
            }
        }

        #region Title Bar

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
            }
        }

        #endregion

        private void btnResetSETL_Click(object sender, EventArgs e)
        {
            playbookFormRef.Play.SETL = (from row in DefaultSETL
                                         select new SETL()
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

            dgvSETL.AutoResizeColumns();
            int SETLrec = ((SETL)playbookFormRef.cbxSubFormations.SelectedItem).rec;
            int PBPLrec = ((PBPL)playbookFormRef.cbxPlays.SelectedItem).rec;

            playbookFormRef.SetPlay(playbookFormRef.Play);
            playbookFormRef.Play = playbookFormRef.GetPlay();
            playbookFormRef.UpdateSubFormations();
            playbookFormRef.UpdateAlignments();
            playbookFormRef.UpdatePlays();

            playbookFormRef.cbxSubFormations.SelectedItem = (playbookFormRef.cbxSubFormations.Items.Cast<SETL>().ToList()).Find(item => item.rec == SETLrec);
            playbookFormRef.cbxPlays.SelectedItem = (playbookFormRef.cbxPlays.Items.Cast<PBPL>().ToList()).Find(item => item.rec == PBPLrec);

            playbookFormRef.UpdatePlay(playbookFormRef.Play);
            dgvSETL.DataSource = playbookFormRef.Play.SETL;

            Program.ResizeDataGrid(dgvSETL);
            Focus();
        }

        private void btnResetSETP_Click(object sender, EventArgs e)
        {
            playbookFormRef.Play.SETP = (from row in DefaultSETP select new SETP()
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
            playbookFormRef.SetPlay(playbookFormRef.Play);
            playbookFormRef.Play = playbookFormRef.GetPlay();
            playbookFormRef.UpdatePlay(playbookFormRef.Play);
            dgvSETP.DataSource = playbookFormRef.Play.SETP;

            Program.ResizeDataGrid(dgvSETP);
            Focus();
        }

        private void btnResetSETG_Click(object sender, EventArgs e)
        {
            playbookFormRef.Play.SETG = (from row in DefaultSETG select new SETG()
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
            playbookFormRef.SetPlay(playbookFormRef.Play);
            playbookFormRef.Play = playbookFormRef.GetPlay();
            playbookFormRef.UpdatePlay(playbookFormRef.Play);
            dgvSETG.DataSource = playbookFormRef.Play.SETG;
            for (int i = 0; i < playbookFormRef.Play.SETG.Count; i++)
            {
                if (!(playbookFormRef.Play.SETG[i].active))
                {
                    dgvSETG.Rows[i].ReadOnly = true;
                    dgvSETG.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    dgvSETG.Rows[i].DefaultCellStyle.SelectionBackColor = Color.White;
                    dgvSETG.Rows[i].DefaultCellStyle.ForeColor = Color.DarkGray;
                    dgvSETG.Rows[i].DefaultCellStyle.SelectionForeColor = Color.DarkGray;
                }
            }

            Program.ResizeDataGrid(dgvSETG);
            Focus();
        }

        private void dgvSETL_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dgvSETL.AutoResizeColumns();
            Program.ResizeDataGrid(dgvSETL);
            int SETLrec = ((SETL)playbookFormRef.cbxSubFormations.SelectedItem).rec;
            int PBPLrec = ((PBPL)playbookFormRef.cbxPlays.SelectedItem).rec;

            playbookFormRef.SetPlay(playbookFormRef.Play);
            playbookFormRef.Play = playbookFormRef.GetPlay();
            playbookFormRef.UpdateSubFormations();
            playbookFormRef.UpdateAlignments();
            playbookFormRef.UpdatePlays();

            playbookFormRef.cbxSubFormations.SelectedItem = (playbookFormRef.cbxSubFormations.Items.Cast<SETL>().ToList()).Find(item => item.rec == SETLrec);
            playbookFormRef.cbxPlays.SelectedItem = (playbookFormRef.cbxPlays.Items.Cast<PBPL>().ToList()).Find(item => item.rec == PBPLrec);

            playbookFormRef.UpdatePlay(playbookFormRef.Play);
            dgvSETL.DataSource = playbookFormRef.Play.SETL;

            Focus();
        }

        private void dgvSETP_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            playbookFormRef.SetPlay(playbookFormRef.Play);
            playbookFormRef.Play = playbookFormRef.GetPlay();
            playbookFormRef.UpdatePlay(playbookFormRef.Play);
            dgvSETP.DataSource = playbookFormRef.Play.SETP;

            Program.ResizeDataGrid(dgvSETP);
            Focus();
        }

        private void dgvSETG_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            playbookFormRef.SetPlay(playbookFormRef.Play);
            playbookFormRef.Play = playbookFormRef.GetPlay();
            playbookFormRef.UpdatePlay(playbookFormRef.Play);
            dgvSETG.DataSource = playbookFormRef.Play.SETG;

            Program.ResizeDataGrid(dgvSETG);
            Focus();
        }

        private void dgvSETP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (PSALroute poso in playbookFormRef.Play.posos)
            {
                poso.Highlighted = false;
            }

            playbookFormRef.Play.posos[dgvSETP.CurrentRow.Index].Highlighted = true;

            playbookFormRef.ClearField();
            playbookFormRef.DrawPlayartPSAL(Graphics.FromImage(playbookFormRef.gImage), playbookFormRef.Play);
        }

        private void dgvSETG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (PSALroute poso in playbookFormRef.Play.posos)
            {
                poso.Highlighted = false;
            }

            playbookFormRef.Play.posos[dgvSETG.CurrentRow.Index].Highlighted = true;

            playbookFormRef.ClearField();
            playbookFormRef.DrawPlayartPSAL(Graphics.FromImage(playbookFormRef.gImage), playbookFormRef.Play);
        }
    }
}
