using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Playbook_Editor
{
    public partial class frmCreatePSAL : Form
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public PLYS DefaultPSAL;
        private frmPlaybook playbookFormRef;
        private List<PSAL> NewPSAL;
        private List<int> availablePSALs;

        public frmCreatePSAL(frmPlaybook playbookFormHandle)
        {
            playbookFormRef = playbookFormHandle;
            InitializeComponent();
            AddDrag(this);
            AddDrag(lblPLRR);
            AddDrag(lblPSAL);
            SetWindowPos(Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

        #region Title Bar

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

        private void frmPLRR_Load(object sender, EventArgs e)
        {
            availablePSALs = GetAvailablePSALs();

            cbxPLRR.ValueMember = null;
            cbxPLRR.DisplayMember = "name";
            cbxPLRR.DataSource = playbookFormRef.PSAL_PLRR_ARTL.Distinct().ToList();
            cbxPLRR.SelectedItem = (cbxPLRR.Items.Cast<PLRR>().ToList()).Find(item => item.plrr == DefaultPSAL.PLRR);

            cbxPSAL.DataSource = availablePSALs;
            cbxPSAL.SelectedIndex = 0;

            if (((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 1 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 2 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 3)
            {
                NewPSAL = playbookFormRef.PSLO.Where(p => p.psal == DefaultPSAL.PSAL).ToList().ConvertAll(psal => psal.Clone()).OrderBy(s => s.step).ToList();
                for (int i = 0; i < NewPSAL.Count(); i++)
                {
                    NewPSAL[i].rec = playbookFormRef.PSLO.Count() + i;
                    NewPSAL[i].psal = (int)cbxPSAL.SelectedItem;
                }
            }
            else if (((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 11 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 12 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 13)
            {
                NewPSAL = playbookFormRef.PSLD.Where(p => p.psal == DefaultPSAL.PSAL).ToList().ConvertAll(psal => psal.Clone()).OrderBy(s => s.step).ToList();
                for (int i = 0; i < NewPSAL.Count(); i++)
                {
                    NewPSAL[i].rec = playbookFormRef.PSLD.Count() + i;
                    NewPSAL[i].psal = (int)cbxPSAL.SelectedItem;
                }
            }

            dgvPSAL.DataSource = NewPSAL;

            dgvPSAL.ShowCellToolTips = true;
            foreach (DataGridViewRow row in dgvPSAL.Rows) foreach (DataGridViewCell cell in row.Cells) cell.ToolTipText = "Right-Click to insert/delete step.";

            dgvPSAL.Columns["rec"].ReadOnly = true;
            dgvPSAL.Columns["rec"].DefaultCellStyle.BackColor = Color.White;
            dgvPSAL.Columns["rec"].DefaultCellStyle.SelectionBackColor = Color.White;
            dgvPSAL.Columns["rec"].DefaultCellStyle.ForeColor = Color.DarkGray;
            dgvPSAL.Columns["rec"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

            dgvPSAL.Columns["psal"].ReadOnly = true;
            dgvPSAL.Columns["psal"].DefaultCellStyle.BackColor = Color.White;
            dgvPSAL.Columns["psal"].DefaultCellStyle.SelectionBackColor = Color.White;
            dgvPSAL.Columns["psal"].DefaultCellStyle.ForeColor = Color.DarkGray;
            dgvPSAL.Columns["psal"].DefaultCellStyle.SelectionForeColor = Color.DarkGray;

            dgvPSAL.AutoResizeColumns();
            dgvPSAL.Height = (dgvPSAL.RowCount + 1) * dgvPSAL.ColumnHeadersHeight + dgvPSAL.RowCount + 2;
            Height = dgvPSAL.Location.Y + dgvPSAL.Height + 41;
        }

        private void cbxPSAL_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (PSAL step in NewPSAL)
            {
                step.psal = (int)cbxPSAL.SelectedItem;
            }

            dgvPSAL.DataSource = NewPSAL.ToList();

            dgvPSAL.ShowCellToolTips = true;
            foreach (DataGridViewRow row in dgvPSAL.Rows) foreach (DataGridViewCell cell in row.Cells) cell.ToolTipText = "Right-Click to insert/delete step.";
        }

        private void dgvPSAL_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                dgvPSAL.CurrentCell = dgvPSAL[e.ColumnIndex, e.RowIndex];
                cmsEditPSAL.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void cmsEditPSAL_Insert_Above_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvPSAL.CurrentCell.RowIndex;

            for (int i = rowIndex; i < NewPSAL.Count(); i++)
            {
                NewPSAL[i].step += 1;
                NewPSAL[i].rec += 1;
            }

            NewPSAL.Insert(rowIndex,
                new PSAL()
                {
                    rec = NewPSAL[rowIndex].rec - 1,
                    val1 = 0,
                    val2 = 0,
                    val3 = 0,
                    psal = NewPSAL[rowIndex].psal,
                    code = 0,
                    step = NewPSAL[rowIndex].step - 1
                });

            dgvPSAL.DataSource = NewPSAL.ToList();

            dgvPSAL.ShowCellToolTips = true;
            foreach (DataGridViewRow row in dgvPSAL.Rows) foreach (DataGridViewCell cell in row.Cells) cell.ToolTipText = "Right-Click to insert/delete step.";

            dgvPSAL.Height = (dgvPSAL.RowCount + 1) * dgvPSAL.ColumnHeadersHeight + dgvPSAL.RowCount + 2;
            Height = dgvPSAL.Location.Y + dgvPSAL.Height + 41;
        }

        private void cmsEditPSAL_Delete_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvPSAL.CurrentCell.RowIndex;

            if (rowIndex < dgvPSAL.RowCount - 1)
            {
                for (int i = rowIndex + 1; i < NewPSAL.Count(); i++)
                {
                    NewPSAL[i].step -= 1;
                    NewPSAL[i].rec -= 1;
                }
            }

            NewPSAL.RemoveAt(rowIndex);

            dgvPSAL.DataSource = NewPSAL.ToList();

            dgvPSAL.ShowCellToolTips = true;
            foreach (DataGridViewRow row in dgvPSAL.Rows) foreach (DataGridViewCell cell in row.Cells) cell.ToolTipText = "Right-Click to insert/delete step.";

            dgvPSAL.Height = (dgvPSAL.RowCount + 1) * dgvPSAL.ColumnHeadersHeight + dgvPSAL.RowCount + 2;
            Height = dgvPSAL.Location.Y + dgvPSAL.Height + 41;
        }

        private List<int> GetAvailablePSALs()
        {
            List<int> availablePSALs = new List<int>();

            for (int i = 1; i <= 8191; i++)
            {
                availablePSALs.Add(i);
            }

            foreach (PLRR psalID in playbookFormRef.PSAL_PLRR_ARTL)
            {
                availablePSALs.Remove(psalID.psal);
            }

            return availablePSALs;
        }

        private void btnCreatePSAL_Click(object sender, EventArgs e)
        {
            if (((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 1 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 2 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 3)
            {
                playbookFormRef.PSLO.AddRange(NewPSAL);
            }
            else if (((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 11 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 12 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 13)
            {
                playbookFormRef.PSLD.AddRange(NewPSAL);
            }

            if (chbAssignPSAL.Checked)
            {
                int dgvRow = playbookFormRef.dgvPLYS.CurrentCell.RowIndex;
                int dgvCol = playbookFormRef.dgvPLYS.CurrentCell.ColumnIndex;

                playbookFormRef.Play.PLYS[playbookFormRef.dgvPLYS.SelectedCells[0].RowIndex] = new PLYS
                {
                    rec = playbookFormRef.Play.PLYS[playbookFormRef.dgvPLYS.SelectedCells[0].RowIndex].rec,
                    PSAL = NewPSAL[0].psal,
                    ARTL = playbookFormRef.Play.PLYS[playbookFormRef.dgvPLYS.SelectedCells[0].RowIndex].ARTL,
                    PLYL = playbookFormRef.Play.PLYS[playbookFormRef.dgvPLYS.SelectedCells[0].RowIndex].PLYL,
                    PLRR = ((PLRR)cbxPLRR.SelectedItem).plrr,
                    poso = playbookFormRef.Play.PLYS[playbookFormRef.dgvPLYS.SelectedCells[0].RowIndex].poso
                };

                playbookFormRef.SetPlay(playbookFormRef.Play);
                playbookFormRef.Play = playbookFormRef.GetPlay();
                playbookFormRef.UpdatePlay(playbookFormRef.Play);

                playbookFormRef.dgvPLYS.CurrentCell = playbookFormRef.dgvPLYS[dgvCol, dgvRow];
            }

            playbookFormRef.PSAL_PLRR_ARTL = playbookFormRef.GetPSAL_PLRR_ARTL();

            Close();
        }

        private void btnDiscardPSAL_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmsEditPSAL_Insert_Below_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvPSAL.CurrentCell.RowIndex;

            for (int i = rowIndex + 1; i < NewPSAL.Count(); i++)
            {
                NewPSAL[i].step += 1;
                NewPSAL[i].rec += 1;
            }

            if (rowIndex < NewPSAL.Count())
            {
                NewPSAL.Insert(rowIndex + 1,
                    new PSAL()
                    {
                        rec = NewPSAL[rowIndex].rec + 1,
                        val1 = 0,
                        val2 = 0,
                        val3 = 0,
                        psal = NewPSAL[rowIndex].psal,
                        code = 0,
                        step = NewPSAL[rowIndex].step + 1
                    });
            }
            else
            {
                NewPSAL.Add(
                    new PSAL()
                    {
                        rec = NewPSAL[rowIndex].rec + 1,
                        val1 = 0,
                        val2 = 0,
                        val3 = 0,
                        psal = NewPSAL[rowIndex].psal,
                        code = 0,
                        step = NewPSAL[rowIndex].step + 1
                    });
            }

            dgvPSAL.DataSource = NewPSAL.ToList();

            dgvPSAL.ShowCellToolTips = true;
            foreach (DataGridViewRow row in dgvPSAL.Rows) foreach (DataGridViewCell cell in row.Cells) cell.ToolTipText = "Right-Click to insert/delete step.";

            dgvPSAL.Height = (dgvPSAL.RowCount + 1) * dgvPSAL.ColumnHeadersHeight + dgvPSAL.RowCount + 2;
            Height = dgvPSAL.Location.Y + dgvPSAL.Height + 41;
        }
    }
}
