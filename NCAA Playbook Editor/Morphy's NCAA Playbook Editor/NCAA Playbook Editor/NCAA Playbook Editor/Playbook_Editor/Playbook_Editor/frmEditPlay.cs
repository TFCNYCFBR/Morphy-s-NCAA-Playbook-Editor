using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Playbook_Editor
{
    public partial class frmEditPlay : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private frmPlaybook playbookFormRef;
        public PLAY Play;
        public List<SRFT> newSRFT;
        public List<PLPD> newPLPD;
        public List<PLRD> newPLRD;

        public frmEditPlay(frmPlaybook playbookFormHandle)
        {
            playbookFormRef = playbookFormHandle;
            InitializeComponent();
            AddDrag(mnuEditPlay);

            Hide();
            mnuEditPlay.Renderer = new MyRenderer();
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

        private void frmEditPlay_Load(object sender, EventArgs e)
        {
            // newSRFT = playbookFormRef.SRFT.ConvertAll(item => item.Clone());
            newPLPD = playbookFormRef.PLPD.ConvertAll(item => item.Clone());
            newPLRD = playbookFormRef.PLRD.ConvertAll(item => item.Clone());

            #region PBPL

            if (Play.PBPL.Count > 0)
            {
                dgvPBPL.DataSource = Play.PBPL;

                dgvPBPL.Columns["rec"].ReadOnly = true;
                dgvPBPL.Columns["rec"].DefaultCellStyle.BackColor = Color.White;
                dgvPBPL.Columns["rec"].DefaultCellStyle.SelectionBackColor = Color.White;
                dgvPBPL.Columns["rec"].DefaultCellStyle.ForeColor = Color.DarkGray;
                dgvPBPL.Columns["rec"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

                dgvPBPL.Columns["SETL"].ReadOnly = true;
                dgvPBPL.Columns["SETL"].DefaultCellStyle.BackColor = Color.White;
                dgvPBPL.Columns["SETL"].DefaultCellStyle.SelectionBackColor = Color.White;
                dgvPBPL.Columns["SETL"].DefaultCellStyle.ForeColor = Color.DarkGray;
                dgvPBPL.Columns["SETL"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

                dgvPBPL.Columns["PLYL"].ReadOnly = true;
                dgvPBPL.Columns["PLYL"].DefaultCellStyle.BackColor = Color.White;
                dgvPBPL.Columns["PLYL"].DefaultCellStyle.SelectionBackColor = Color.White;
                dgvPBPL.Columns["PLYL"].DefaultCellStyle.ForeColor = Color.DarkGray;
                dgvPBPL.Columns["PLYL"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

                dgvPBPL.Visible = true;
                lblPBPL.Visible = true;
                dgvPBPL.AutoResizeColumns();
                Program.ResizeDataGrid(dgvPBPL);
                Width = dgvPBPL.Width + 24;
            }

            #endregion

            #region PLPD

            if (Play.PLPD.Count > 0)
            {
                mnuEditPlay_Options_PLPD_Add.Enabled = false;

                dgvPLPD.DataSource = Play.PLPD;

                dgvPLPD.Columns["rec"].ReadOnly = true;
                dgvPLPD.Columns["rec"].DefaultCellStyle.BackColor = Color.White;
                dgvPLPD.Columns["rec"].DefaultCellStyle.SelectionBackColor = Color.White;
                dgvPLPD.Columns["rec"].DefaultCellStyle.ForeColor = Color.DarkGray;
                dgvPLPD.Columns["rec"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

                dgvPLPD.Columns["PLYL"].ReadOnly = true;
                dgvPLPD.Columns["PLYL"].DefaultCellStyle.BackColor = Color.White;
                dgvPLPD.Columns["PLYL"].DefaultCellStyle.SelectionBackColor = Color.White;
                dgvPLPD.Columns["PLYL"].DefaultCellStyle.ForeColor = Color.DarkGray;
                dgvPLPD.Columns["PLYL"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

                dgvPLPD.Visible = true;
                lblPLPD.Visible = true;
                dgvPLPD.AutoResizeColumns();
                Program.ResizeDataGrid(dgvPLPD);
                Width = dgvPLPD.Width + 24;
            }
            else
            {
                mnuEditPlay_Options_PLPD_Remove.Enabled = false;
            }

            #endregion

            #region PLRD

            if (Play.PLRD.Count > 0)
            {
                mnuEditPlay_Options_PLRD_Add.Enabled = false;

                dgvPLRD.DataSource = Play.PLRD;

                dgvPLRD.Columns["rec"].ReadOnly = true;
                dgvPLRD.Columns["rec"].DefaultCellStyle.BackColor = Color.White;
                dgvPLRD.Columns["rec"].DefaultCellStyle.SelectionBackColor = Color.White;
                dgvPLRD.Columns["rec"].DefaultCellStyle.ForeColor = Color.DarkGray;
                dgvPLRD.Columns["rec"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

                dgvPLRD.Columns["PLYL"].ReadOnly = true;
                dgvPLRD.Columns["PLYL"].DefaultCellStyle.BackColor = Color.White;
                dgvPLRD.Columns["PLYL"].DefaultCellStyle.SelectionBackColor = Color.White;
                dgvPLRD.Columns["PLYL"].DefaultCellStyle.ForeColor = Color.DarkGray;
                dgvPLRD.Columns["PLYL"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

                dgvPLRD.Visible = true;
                lblPLRD.Visible = true;
                dgvPLRD.AutoResizeColumns();
                Program.ResizeDataGrid(dgvPLRD);
            }
            else
            {
                mnuEditPlay_Options_PLRD_Remove.Enabled = false;
            }

            #endregion

            #region SRFT

            // if (Play.SRFT.Count > 0)
            // {
                // dgvSRFT.DataSource = Play.SRFT;

                // dgvSRFT.Columns["rec"].ReadOnly = true;
                // dgvSRFT.Columns["rec"].DefaultCellStyle.BackColor = Color.White;
                // dgvSRFT.Columns["rec"].DefaultCellStyle.SelectionBackColor = Color.White;
                // dgvSRFT.Columns["rec"].DefaultCellStyle.ForeColor = Color.DarkGray;
                // dgvSRFT.Columns["rec"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

                // dgvSRFT.Columns["PLYL"].ReadOnly = true;
                // dgvSRFT.Columns["PLYL"].DefaultCellStyle.BackColor = Color.White;
                // dgvSRFT.Columns["PLYL"].DefaultCellStyle.SelectionBackColor = Color.White;
                // dgvSRFT.Columns["PLYL"].DefaultCellStyle.ForeColor = Color.DarkGray;
                // dgvSRFT.Columns["PLYL"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

                // lblSRFT.Visible = true;
                // dgvSRFT.Visible = true;
                // dgvSRFT.AutoResizeColumns();
                // Program.ResizeDataGrid(dgvSRFT);
            // }

            // dgvSRFT.ShowCellToolTips = true;
            // foreach (DataGridViewRow row in dgvSRFT.Rows) foreach (DataGridViewCell cell in row.Cells) cell.ToolTipText = "Right-Click to insert/delete SRFT.";

            #endregion

            newPLRD = playbookFormRef.PLRD.ConvertAll(item => item.Clone());
            newPLPD = playbookFormRef.PLPD.ConvertAll(item => item.Clone());

            ResizeForm();
            CenterToParent();
            Show();
        }

        private void dgvPBPL_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Play.PBPL = (from row in dgvPBPL.Rows.OfType<DataGridViewRow>()
                         select new PBPL()
                         {
                             rec = (int)row.Cells["rec"].Value,
                             COMF = (int)row.Cells["COMF"].Value,
                             SETL = (int)row.Cells["SETL"].Value,
                             PLYL = (int)row.Cells["PLYL"].Value,
                             SRMM = (int)row.Cells["SRMM"].Value,
                             SITT = (int)row.Cells["SITT"].Value,
                             PLYT = (int)row.Cells["PLYT"].Value,
                             PLF_ = (int)row.Cells["PLF_"].Value,
                             name = row.Cells["name"].Value.ToString(),
                             risk = (int)row.Cells["risk"].Value,
                             motn = (int)row.Cells["motn"].Value,
                             vpos = (int)row.Cells["vpos"].Value
                         }).ToList();

            dgvPBPL.AutoResizeColumns();
            Program.ResizeDataGrid(dgvPBPL);
            ResizeForm();
        }

        private void dgvPLPD_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Play.PLPD = (from row in dgvPLPD.Rows.OfType<DataGridViewRow>()
                         select new PLPD()
                         {
                             rec = (int)row.Cells["rec"].Value,
                             com1 = (int)row.Cells["com1"].Value,
                             con1 = (int)row.Cells["con1"].Value,
                             per1 = (int)row.Cells["per1"].Value,
                             rcv1 = (int)row.Cells["rcv1"].Value,
                             com2 = (int)row.Cells["com2"].Value,
                             con2 = (int)row.Cells["con2"].Value,
                             per2 = (int)row.Cells["per2"].Value,
                             rcv2 = (int)row.Cells["rcv2"].Value,
                             com3 = (int)row.Cells["com3"].Value,
                             con3 = (int)row.Cells["con3"].Value,
                             per3 = (int)row.Cells["per3"].Value,
                             rcv3 = (int)row.Cells["rcv3"].Value,
                             com4 = (int)row.Cells["com4"].Value,
                             con4 = (int)row.Cells["con4"].Value,
                             per4 = (int)row.Cells["per4"].Value,
                             rcv4 = (int)row.Cells["rcv4"].Value,
                             com5 = (int)row.Cells["com5"].Value,
                             con5 = (int)row.Cells["con5"].Value,
                             per5 = (int)row.Cells["per5"].Value,
                             rcv5 = (int)row.Cells["rcv5"].Value,
                             PLYL = (int)row.Cells["PLYL"].Value
                         }).ToList();

            CenterToParent();
            dgvPLPD.AutoResizeColumns();
            Program.ResizeDataGrid(dgvPLPD);
            ResizeForm();
        }

        private void dgvPLRD_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Play.PLRD = (from row in dgvPLRD.Rows.OfType<DataGridViewRow>()
                         select new PLRD()
                         {
                             rec = (int)row.Cells["rec"].Value,
                             PLYL = (int)row.Cells["PLYL"].Value,
                             hole = (int)row.Cells["hole"].Value
                         }).ToList();

            dgvPLRD.AutoResizeColumns();
            Program.ResizeDataGrid(dgvPLRD);
            ResizeForm();
        }

        private void dgvSRFT_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Play.SRFT = (from row in dgvSRFT.Rows.OfType<DataGridViewRow>()
                         select new SRFT()
                         {
                             rec = (int)row.Cells["rec"].Value,
                             SIDE = (int)row.Cells["SIDE"].Value,
                             YOFF = (int)row.Cells["YOFF"].Value,
                             TECH = (int)row.Cells["TECH"].Value,
                             PLYL = (int)row.Cells["PLYL"].Value,
                             STAN = (int)row.Cells["STAN"].Value,
                             PLYR = (int)row.Cells["PLYR"].Value,
                             PRIS = (int)row.Cells["PRIS"].Value,
                             GAPS = (int)row.Cells["GAPS"].Value,
                             ASSS = (int)row.Cells["ASSS"].Value,
                             PRIW = (int)row.Cells["PRIW"].Value,
                             GAPW = (int)row.Cells["GAPW"].Value,
                             ASSW = (int)row.Cells["ASSW"].Value
                         }).ToList();

            dgvSRFT.AutoResizeColumns();
            dgvSRFT.Height = (dgvSRFT.RowCount + 1) * dgvSRFT.ColumnHeadersHeight + dgvSRFT.RowCount + 2;
            ResizeForm();
        }

        private void ResizeForm()
        {
            int newHeight = dgvPBPL.Height + dgvPBPL.Location.Y;

            if (dgvPLPD.Visible && !dgvPLRD.Visible)
            {
                newHeight = newHeight + 12;
                lblPLPD.Location = new Point(lblPLPD.Location.X, newHeight);
                newHeight = newHeight + lblPLPD.Height + 3;
                dgvPLPD.Location = new Point(dgvPLPD.Location.X, newHeight);
                newHeight = newHeight + dgvPLRD.Height + 47;

                Width = Math.Max(dgvPBPL.Width, dgvPLPD.Width) + 24;
            }
            else if (!dgvPLPD.Visible && dgvPLRD.Visible)
            {
                newHeight = newHeight + 12;
                lblPLRD.Location = new Point(lblPLRD.Location.X, newHeight);
                newHeight = newHeight + lblPLRD.Height + 3;
                dgvPLRD.Location = new Point(dgvPLRD.Location.X, newHeight);
                newHeight = newHeight + dgvPLRD.Height + 47;

                Width = Math.Max(dgvPBPL.Width, dgvPLRD.Width) + 24;
            }
            else if (dgvPLPD.Visible && dgvPLRD.Visible)
            {
                newHeight = newHeight + 12;
                lblPLPD.Location = new Point(lblPLPD.Location.X, newHeight);
                newHeight = newHeight + lblPLPD.Height + 3;
                dgvPLPD.Location = new Point(dgvPLPD.Location.X, newHeight);
                newHeight = newHeight + dgvPLRD.Height;

                newHeight = newHeight + 12;
                lblPLRD.Location = new Point(lblPLRD.Location.X, newHeight);
                newHeight = newHeight + lblPLRD.Height + 3;
                dgvPLRD.Location = new Point(dgvPLRD.Location.X, newHeight);
                newHeight = newHeight + dgvPLRD.Height + 47;

                Width = Math.Max(Math.Max(dgvPBPL.Width, dgvPLPD.Width), dgvPLRD.Width) + 24;
            }
            else if (!dgvPLPD.Visible && !dgvPLRD.Visible && dgvSRFT.Visible)
            {
                newHeight = newHeight + 12;
                lblSRFT.Location = new Point(lblSRFT.Location.X, newHeight);
                newHeight = newHeight + lblSRFT.Height + 3;
                dgvSRFT.Location = new Point(dgvSRFT.Location.X, newHeight);
                newHeight = newHeight + dgvSRFT.Height + 47;

                Width = Math.Max(dgvPBPL.Width, dgvSRFT.Width) + 24;
            }
            else
            {
                newHeight += 47;
                btnUpdate.Enabled = false;
            }

            Height = newHeight;
        }

        private void dgvSRFT_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                dgvSRFT.CurrentCell = dgvSRFT[e.ColumnIndex, e.RowIndex];
                cmsEditSRFT.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void cmsEditSRFT_Insert_Above_Click(object sender, EventArgs e)
        {
            if (Play.SRFT.Count() == 11)
            {
                MessageBox.Show("Number of SRFT records for a play is at the max (11).", "Insert SRFT Canceled", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            int rowIndex = dgvSRFT.CurrentCell.RowIndex;
            int colIndex = dgvSRFT.Columns["rec"].Index;
            int record = (int)dgvSRFT[colIndex, rowIndex].Value;

            Play.SRFT.Insert(rowIndex,
                new SRFT()
                {
                    rec = newSRFT.Count(),
                    SIDE = 0,
                    YOFF = 0,
                    TECH = 0,
                    PLYL = Play.SRFT[0].PLYL,
                    STAN = 0,
                    PLYR = 0,
                    PRIS = 0,
                    GAPS = 0,
                    ASSS = 0,
                    PRIW = 0,
                    GAPW = 0,
                    ASSW = 0
                });

            newSRFT.Add(Play.SRFT[Play.SRFT.FindIndex(item => item.rec == newSRFT.Count())]);

            Console.WriteLine(newSRFT.Count());

            dgvSRFT.DataSource = Play.SRFT.ToList();

            dgvSRFT.Columns["rec"].ReadOnly = true;
            dgvSRFT.Columns["rec"].DefaultCellStyle.BackColor = Color.White;
            dgvSRFT.Columns["rec"].DefaultCellStyle.SelectionBackColor = Color.White;
            dgvSRFT.Columns["rec"].DefaultCellStyle.ForeColor = Color.DarkGray;
            dgvSRFT.Columns["rec"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

            dgvSRFT.Columns["PLYL"].ReadOnly = true;
            dgvSRFT.Columns["PLYL"].DefaultCellStyle.BackColor = Color.White;
            dgvSRFT.Columns["PLYL"].DefaultCellStyle.SelectionBackColor = Color.White;
            dgvSRFT.Columns["PLYL"].DefaultCellStyle.ForeColor = Color.DarkGray;
            dgvSRFT.Columns["PLYL"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

            lblSRFT.Visible = true;
            dgvSRFT.Visible = true;
            dgvSRFT.AutoResizeColumns();
            Program.ResizeDataGrid(dgvSRFT);

            dgvSRFT.ShowCellToolTips = true;
            foreach (DataGridViewRow row in dgvSRFT.Rows) foreach (DataGridViewCell cell in row.Cells) cell.ToolTipText = "Right-Click to insert/delete SRFT.";

            dgvSRFT.AutoResizeColumns();
            dgvSRFT.Height = (dgvSRFT.RowCount + 1) * dgvSRFT.ColumnHeadersHeight + dgvSRFT.RowCount + 2;
            ResizeForm();
        }

        private void cmsEditSRFT_Insert_Below_Click(object sender, EventArgs e)
        {
            if (Play.SRFT.Count() == 11)
            {
                MessageBox.Show("Number of SRFT records for a play is at the max (11).", "Insert SRFT Canceled", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            int rowIndex = dgvSRFT.CurrentCell.RowIndex;
            int colIndex = dgvSRFT.Columns["rec"].Index;
            int record = (int)dgvSRFT[colIndex, rowIndex].Value;

            if (rowIndex < Play.SRFT.Count() - 1)
            {
                Play.SRFT.Insert(rowIndex + 1,
                    new SRFT()
                    {
                        rec = newSRFT.Count(),
                        SIDE = 0,
                        YOFF = 0,
                        TECH = 0,
                        PLYL = Play.SRFT[0].PLYL,
                        STAN = 0,
                        PLYR = 0,
                        PRIS = 0,
                        GAPS = 0,
                        ASSS = 0,
                        PRIW = 0,
                        GAPW = 0,
                        ASSW = 0
                    });
            }
            else
            {
                Play.SRFT.Add(
                    new SRFT()
                    {
                        rec = newSRFT.Count(),
                        SIDE = 0,
                        YOFF = 0,
                        TECH = 0,
                        PLYL = Play.SRFT[0].PLYL,
                        STAN = 0,
                        PLYR = 0,
                        PRIS = 0,
                        GAPS = 0,
                        ASSS = 0,
                        PRIW = 0,
                        GAPW = 0,
                        ASSW = 0
                    });
            }

            newSRFT.Add(Play.SRFT[Play.SRFT.FindIndex(item => item.rec == newSRFT.Count())]);

            Console.WriteLine(newSRFT.Count());

            dgvSRFT.DataSource = Play.SRFT.ToList();

            dgvSRFT.Columns["rec"].ReadOnly = true;
            dgvSRFT.Columns["rec"].DefaultCellStyle.BackColor = Color.White;
            dgvSRFT.Columns["rec"].DefaultCellStyle.SelectionBackColor = Color.White;
            dgvSRFT.Columns["rec"].DefaultCellStyle.ForeColor = Color.DarkGray;
            dgvSRFT.Columns["rec"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

            dgvSRFT.Columns["PLYL"].ReadOnly = true;
            dgvSRFT.Columns["PLYL"].DefaultCellStyle.BackColor = Color.White;
            dgvSRFT.Columns["PLYL"].DefaultCellStyle.SelectionBackColor = Color.White;
            dgvSRFT.Columns["PLYL"].DefaultCellStyle.ForeColor = Color.DarkGray;
            dgvSRFT.Columns["PLYL"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

            lblSRFT.Visible = true;
            dgvSRFT.Visible = true;
            dgvSRFT.AutoResizeColumns();
            Program.ResizeDataGrid(dgvSRFT);

            dgvSRFT.ShowCellToolTips = true;
            foreach (DataGridViewRow row in dgvSRFT.Rows) foreach (DataGridViewCell cell in row.Cells) cell.ToolTipText = "Right-Click to insert/delete SRFT.";

            dgvSRFT.AutoResizeColumns();
            dgvSRFT.Height = (dgvSRFT.RowCount + 1) * dgvSRFT.ColumnHeadersHeight + dgvSRFT.RowCount + 2;
            ResizeForm();
        }

        private void cmsEditSRFT_Delete_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvSRFT.CurrentCell.RowIndex;
            int colIndex = dgvSRFT.Columns["rec"].Index;
            int record = (int)dgvSRFT[colIndex, rowIndex].Value;

            newSRFT.RemoveAt(record);

            Play.SRFT.RemoveAt(Play.SRFT.FindIndex(item => item.rec == record));

            Console.WriteLine(newSRFT.Count());
            for (int i = record; i < newSRFT.Count(); i++)
            {
                if (Play.SRFT.Exists(r => r.rec == i))
                {
                    Play.SRFT[Play.SRFT.FindIndex(r => r.rec == i)].rec -= 1;
                }
                newSRFT[i].rec -= 1;
            }

            dgvSRFT.DataSource = Play.SRFT.ToList();

            dgvSRFT.Columns["rec"].ReadOnly = true;
            dgvSRFT.Columns["rec"].DefaultCellStyle.BackColor = Color.White;
            dgvSRFT.Columns["rec"].DefaultCellStyle.SelectionBackColor = Color.White;
            dgvSRFT.Columns["rec"].DefaultCellStyle.ForeColor = Color.DarkGray;
            dgvSRFT.Columns["rec"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

            dgvSRFT.Columns["PLYL"].ReadOnly = true;
            dgvSRFT.Columns["PLYL"].DefaultCellStyle.BackColor = Color.White;
            dgvSRFT.Columns["PLYL"].DefaultCellStyle.SelectionBackColor = Color.White;
            dgvSRFT.Columns["PLYL"].DefaultCellStyle.ForeColor = Color.DarkGray;
            dgvSRFT.Columns["PLYL"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

            lblSRFT.Visible = true;
            dgvSRFT.Visible = true;
            dgvSRFT.AutoResizeColumns();
            Program.ResizeDataGrid(dgvSRFT);

            dgvSRFT.ShowCellToolTips = true;
            foreach (DataGridViewRow row in dgvSRFT.Rows) foreach (DataGridViewCell cell in row.Cells) cell.ToolTipText = "Right-Click to insert/delete SRFT.";

            dgvSRFT.AutoResizeColumns();
            dgvSRFT.Height = (dgvSRFT.RowCount + 1) * dgvSRFT.ColumnHeadersHeight + dgvSRFT.RowCount + 2;
            ResizeForm();
        }

        private void mnuEditPlay_Options_PLPD_Add_Click(object sender, EventArgs e)
        {
            PLPD _newPLPD = new PLPD()
            {
                rec = newPLPD.Count(),
                com1 = 0,
                con1 = 0,
                per1 = 0,
                rcv1 = 0,
                com2 = 0,
                con2 = 0,
                per2 = 0,
                rcv2 = 0,
                com3 = 0,
                con3 = 0,
                per3 = 0,
                rcv3 = 0,
                com4 = 0,
                con4 = 0,
                per4 = 0,
                rcv4 = 0,
                com5 = 0,
                con5 = 0,
                per5 = 0,
                rcv5 = 0,
                PLYL = Play.PBPL[0].PLYL
            };

            newPLPD.Add(_newPLPD);
            Play.PLPD = new List<PLPD>() { _newPLPD };

            dgvPLPD.DataSource = Play.PLPD;

            dgvPLPD.Columns["rec"].ReadOnly = true;
            dgvPLPD.Columns["rec"].DefaultCellStyle.BackColor = Color.White;
            dgvPLPD.Columns["rec"].DefaultCellStyle.SelectionBackColor = Color.White;
            dgvPLPD.Columns["rec"].DefaultCellStyle.ForeColor = Color.DarkGray;
            dgvPLPD.Columns["rec"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

            dgvPLPD.Columns["PLYL"].ReadOnly = true;
            dgvPLPD.Columns["PLYL"].DefaultCellStyle.BackColor = Color.White;
            dgvPLPD.Columns["PLYL"].DefaultCellStyle.SelectionBackColor = Color.White;
            dgvPLPD.Columns["PLYL"].DefaultCellStyle.ForeColor = Color.DarkGray;
            dgvPLPD.Columns["PLYL"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

            dgvPLPD.Visible = true;
            lblPLPD.Visible = true;
            dgvPLPD.AutoResizeColumns();
            Program.ResizeDataGrid(dgvPLPD);
            Width = dgvPLPD.Width + 24;

            mnuEditPlay_Options_PLPD_Remove.Enabled = true;
            mnuEditPlay_Options_PLPD_Add.Enabled = false;
            ResizeForm();
        }

        private void mnuEditPlay_Options_PLPD_Remove_Click(object sender, EventArgs e)
        {
            newPLPD.RemoveAt(Play.PLPD[0].rec);

            for (int i = Play.PLPD[0].rec; i < newPLPD.Count(); i++)
            {
                newPLPD[i].rec -= 1;
            }

            Play.PLPD = new List<PLPD>();

            dgvPLPD.Visible = false;
            lblPLPD.Visible = false;

            mnuEditPlay_Options_PLPD_Remove.Enabled = false;
            mnuEditPlay_Options_PLPD_Add.Enabled = true;
            ResizeForm();
        }

        private void mnuEditPlay_Options_PLRD_Add_Click(object sender, EventArgs e)
        {
            PLRD _newPLRD = new PLRD()
            {
                rec = newPLRD.Count(),
                PLYL = Play.PBPL[0].PLYL,
                hole = 0
            };

            newPLRD.Add(_newPLRD);
            Play.PLRD = new List<PLRD>() { _newPLRD };

            dgvPLRD.DataSource = Play.PLRD;

            dgvPLRD.Columns["rec"].ReadOnly = true;
            dgvPLRD.Columns["rec"].DefaultCellStyle.BackColor = Color.White;
            dgvPLRD.Columns["rec"].DefaultCellStyle.SelectionBackColor = Color.White;
            dgvPLRD.Columns["rec"].DefaultCellStyle.ForeColor = Color.DarkGray;
            dgvPLRD.Columns["rec"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

            dgvPLRD.Columns["PLYL"].ReadOnly = true;
            dgvPLRD.Columns["PLYL"].DefaultCellStyle.BackColor = Color.White;
            dgvPLRD.Columns["PLYL"].DefaultCellStyle.SelectionBackColor = Color.White;
            dgvPLRD.Columns["PLYL"].DefaultCellStyle.ForeColor = Color.DarkGray;
            dgvPLRD.Columns["PLYL"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

            dgvPLRD.Visible = true;
            lblPLRD.Visible = true;
            dgvPLRD.AutoResizeColumns();
            Program.ResizeDataGrid(dgvPLRD);

            mnuEditPlay_Options_PLRD_Remove.Enabled = true;
            mnuEditPlay_Options_PLRD_Add.Enabled = false;
            ResizeForm();
        }

        private void mnuEditPlay_Options_PLRD_Remove_Click(object sender, EventArgs e)
        {
            newPLRD.RemoveAt(Play.PLRD[0].rec);

            for (int i = Play.PLRD[0].rec; i < newPLRD.Count(); i++)
            {
                newPLRD[i].rec -= 1;
            }

            Play.PLRD = new List<PLRD>();

            dgvPLRD.Visible = false;
            lblPLRD.Visible = false;

            mnuEditPlay_Options_PLRD_Remove.Enabled = false;
            mnuEditPlay_Options_PLRD_Add.Enabled = true;
            ResizeForm();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
