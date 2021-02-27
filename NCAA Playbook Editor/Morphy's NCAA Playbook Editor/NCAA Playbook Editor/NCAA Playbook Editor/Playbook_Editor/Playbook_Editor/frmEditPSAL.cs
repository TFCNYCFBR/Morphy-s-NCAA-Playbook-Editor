using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Playbook_Editor
{
    public partial class frmEditPSAL : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public PLYS DefaultPSAL;
        private frmPlaybook playbookFormRef;
        private List<PSAL> EditedPSALs; 

        public frmEditPSAL(frmPlaybook playbookFormHandle)
        {
            playbookFormRef = playbookFormHandle;
            InitializeComponent();
            AddDrag(this);
            AddDrag(lblPLRR);
            AddDrag(lblPSAL);
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

        private void frmPLRR_Deactivate(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        private void frmPLRR_Load(object sender, EventArgs e)
        {
            if (((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 1 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 2 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 3)
            {
                EditedPSALs = playbookFormRef.PSLO.ConvertAll(psal => psal.Clone());
            }
            else if (((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 11 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 12 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 13)
            {
                EditedPSALs = playbookFormRef.PSLD.ConvertAll(psal => psal.Clone());
            }

            cbxPLRR.ValueMember = null;
            cbxPLRR.DisplayMember = "name";
            cbxPLRR.DataSource = playbookFormRef.PSAL_PLRR_ARTL.Distinct().ToList();
            cbxPLRR.SelectedItem = (cbxPLRR.Items.Cast<PLRR>().ToList()).Find(item => item.plrr == DefaultPSAL.PLRR);

            cbxPSAL.ValueMember = null;
            cbxPSAL.DisplayMember = "psal";
            cbxPSAL.DataSource = playbookFormRef.PSAL_PLRR_ARTL.Where(psal => psal.plrr == ((PLRR)cbxPLRR.SelectedItem).plrr).OrderBy(s => s.psal).ToList();
            cbxPSAL.SelectedItem = (cbxPSAL.Items.Cast<PLRR>().ToList()).Find(item => item.psal == DefaultPSAL.PSAL);

            dgvPSAL.DataSource = EditedPSALs.Where(psal => psal.psal == ((PLRR)cbxPSAL.SelectedItem).psal).OrderBy(s => s.step).ToList();

            dgvPSAL.ShowCellToolTips = true;
            foreach (DataGridViewRow row in dgvPSAL.Rows) foreach (DataGridViewCell cell in row.Cells) cell.ToolTipText = "Right-Click to insert/delete step.";

            dgvPSAL.AutoResizeColumns();
            dgvPSAL.Height = (dgvPSAL.RowCount + 1) * dgvPSAL.ColumnHeadersHeight + dgvPSAL.RowCount + 2;
            Height = dgvPSAL.Location.Y + dgvPSAL.Height + 12;
        }

        private void frmPLRR_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 1 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 2 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 3)
            {
                playbookFormRef.PSLO = EditedPSALs;
            }
            else if (((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 11 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 12 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 13)
            {
                playbookFormRef.PSLD = EditedPSALs;
            }

            int dgvRow = playbookFormRef.dgvPLYS.CurrentCell.RowIndex;
            int dgvCol = playbookFormRef.dgvPLYS.CurrentCell.ColumnIndex;

            playbookFormRef.SetPlay(playbookFormRef.Play);
            playbookFormRef.Play = playbookFormRef.GetPlay();
            playbookFormRef.UpdatePlay(playbookFormRef.Play);

            playbookFormRef.dgvPLYS.CurrentCell = playbookFormRef.dgvPLYS[dgvCol, dgvRow];
        }

        private void cbxPLRR_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxPSAL.ValueMember = null;
            cbxPSAL.DisplayMember = "psal";
            cbxPSAL.DataSource = playbookFormRef.PSAL_PLRR_ARTL.Where(psal => psal.plrr == ((PLRR)cbxPLRR.SelectedItem).plrr).OrderBy(s => s.psal).ToList();
        }

        private void cbxPSAL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dgvRow = playbookFormRef.dgvPLYS.CurrentCell.RowIndex;
            int dgvCol = playbookFormRef.dgvPLYS.CurrentCell.ColumnIndex;

            playbookFormRef.Play.PLYS[playbookFormRef.dgvPLYS.SelectedCells[0].RowIndex] = new PLYS
            {
                rec = playbookFormRef.Play.PLYS[playbookFormRef.dgvPLYS.SelectedCells[0].RowIndex].rec,
                PSAL = ((PLRR)cbxPSAL.SelectedItem).psal,
                ARTL = ((PLRR)cbxPSAL.SelectedItem).artl,
                PLYL = playbookFormRef.Play.PLYS[playbookFormRef.dgvPLYS.SelectedCells[0].RowIndex].PLYL,
                PLRR = ((PLRR)cbxPSAL.SelectedItem).plrr,
                poso = playbookFormRef.Play.PLYS[playbookFormRef.dgvPLYS.SelectedCells[0].RowIndex].poso
            };

            playbookFormRef.SetPlay(playbookFormRef.Play);
            playbookFormRef.Play = playbookFormRef.GetPlay();
            playbookFormRef.UpdatePlay(playbookFormRef.Play);

            playbookFormRef.dgvPLYS.CurrentCell = playbookFormRef.dgvPLYS[dgvCol, dgvRow];

            dgvPSAL.DataSource = EditedPSALs.Where(psal => psal.psal == ((PLRR)cbxPSAL.SelectedItem).psal).OrderBy(s => s.step).ToList();

            dgvPSAL.AutoResizeColumns();
            dgvPSAL.Height = (dgvPSAL.RowCount + 1) * dgvPSAL.ColumnHeadersHeight + dgvPSAL.RowCount + 2;
            Height = dgvPSAL.Location.Y + dgvPSAL.Height + 12;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            int dgvRow = playbookFormRef.dgvPLYS.CurrentCell.RowIndex;
            int dgvCol = playbookFormRef.dgvPLYS.CurrentCell.ColumnIndex;

            cbxPLRR.SelectedItem = (cbxPLRR.Items.Cast<PLRR>().ToList()).Find(item => item.plrr == DefaultPSAL.PLRR);

            cbxPSAL.DataSource = playbookFormRef.PSAL_PLRR_ARTL.Where(psal => psal.plrr == ((PLRR)cbxPLRR.SelectedItem).plrr).OrderBy(s => s.psal).ToList();

            cbxPSAL.SelectedItem = (cbxPSAL.Items.Cast<PLRR>().ToList()).Find(item => item.psal == DefaultPSAL.PSAL);

            playbookFormRef.Play.PLYS[playbookFormRef.dgvPLYS.SelectedCells[0].RowIndex] = new PLYS
            {
                rec = playbookFormRef.Play.PLYS[playbookFormRef.dgvPLYS.SelectedCells[0].RowIndex].rec,
                PSAL = DefaultPSAL.PSAL,
                ARTL = DefaultPSAL.ARTL,
                PLYL = playbookFormRef.Play.PLYS[playbookFormRef.dgvPLYS.SelectedCells[0].RowIndex].PLYL,
                PLRR = DefaultPSAL.PLRR,
                poso = playbookFormRef.Play.PLYS[playbookFormRef.dgvPLYS.SelectedCells[0].RowIndex].poso
            };

            playbookFormRef.SetPlay(playbookFormRef.Play);
            playbookFormRef.Play = playbookFormRef.GetPlay();
            playbookFormRef.UpdatePlay(playbookFormRef.Play);

            playbookFormRef.dgvPLYS.CurrentCell = playbookFormRef.dgvPLYS[dgvCol, dgvRow];

            if (((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 1 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 2 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 3)
            {
                EditedPSALs = playbookFormRef.PSLO.ConvertAll(psal => psal.Clone());
            }
            else if (((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 11 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 12 || ((CPFM)playbookFormRef.cbxFormations.SelectedItem).FTYP == 13)
            {
                EditedPSALs = playbookFormRef.PSLD.ConvertAll(psal => psal.Clone());
            }

            dgvPSAL.DataSource = EditedPSALs.Where(psal => psal.psal == ((PLRR)cbxPSAL.SelectedItem).psal).OrderBy(s => s.step).ToList();

            dgvPSAL.AutoResizeColumns();
            dgvPSAL.Height = (dgvPSAL.RowCount + 1) * dgvPSAL.ColumnHeadersHeight + dgvPSAL.RowCount + 2;
            Height = dgvPSAL.Location.Y + dgvPSAL.Height + 12;
        }

        private void chbEdit_CheckedChanged(object sender, EventArgs e)
        {
            int PSALCountInPLYS = playbookFormRef.PLYS.FindAll(p => p.PSAL == DefaultPSAL.PSAL).Count();
            Console.WriteLine(PSALCountInPLYS);

            if (chbEdit.Checked)
            {
                dgvPSAL.ReadOnly = false;

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
            }
            else if (!chbEdit.Checked)
            {
                dgvPSAL.ReadOnly = true;

                dgvPSAL.Columns["rec"].DefaultCellStyle.BackColor = Color.White;
                dgvPSAL.Columns["rec"].DefaultCellStyle.SelectionBackColor = Color.Yellow;
                dgvPSAL.Columns["rec"].DefaultCellStyle.ForeColor = Color.Black;
                dgvPSAL.Columns["rec"].DefaultCellStyle.SelectionForeColor = Color.Black;

                dgvPSAL.Columns["psal"].DefaultCellStyle.BackColor = Color.White;
                dgvPSAL.Columns["psal"].DefaultCellStyle.SelectionBackColor = Color.Yellow;
                dgvPSAL.Columns["psal"].DefaultCellStyle.ForeColor = Color.Black;
                dgvPSAL.Columns["psal"].DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }

        private void dgvPSAL_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && chbEdit.Checked && e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                dgvPSAL.CurrentCell = dgvPSAL[e.ColumnIndex, e.RowIndex];
                cmsEditPSAL.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void cmsEditPSAL_Insert_Above_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvPSAL.CurrentCell.RowIndex;
            int colIndex = dgvPSAL.Columns["rec"].Index;
            int record = (int)dgvPSAL[colIndex, rowIndex].Value;

            for (int i = rowIndex; i < dgvPSAL.RowCount; i++)
            {
                EditedPSALs[(int)dgvPSAL[colIndex, i].Value].step = EditedPSALs[(int)dgvPSAL[colIndex, i].Value].step + 1;
            }

            EditedPSALs.Add(
                new PSAL()
                {
                    rec = EditedPSALs.Count(),
                    val1 = 0,
                    val2 = 0,
                    val3 = 0,
                    psal = EditedPSALs[(int)dgvPSAL[colIndex, rowIndex].Value].psal,
                    code = EditedPSALs[(int)dgvPSAL[colIndex, rowIndex].Value].code,
                    step = EditedPSALs[(int)dgvPSAL[colIndex, rowIndex].Value].step - 1
                });

            dgvPSAL.DataSource = EditedPSALs.Where(psal => psal.psal == ((PLRR)cbxPSAL.SelectedItem).psal).OrderBy(s => s.step).ToList();

            dgvPSAL.ShowCellToolTips = true;
            foreach (DataGridViewRow row in dgvPSAL.Rows) foreach (DataGridViewCell cell in row.Cells) cell.ToolTipText = "Right-Click to insert/delete step.";

            dgvPSAL.Height = (dgvPSAL.RowCount + 1) * dgvPSAL.ColumnHeadersHeight + dgvPSAL.RowCount + 2;
            Height = dgvPSAL.Location.Y + dgvPSAL.Height + 12;
        }

        private void cmsEditPSAL_Insert_Below_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvPSAL.CurrentCell.RowIndex;
            int colIndex = dgvPSAL.Columns["rec"].Index;
            int record = (int)dgvPSAL[colIndex, rowIndex].Value;

            for (int i = rowIndex + 1; i < dgvPSAL.RowCount; i++)
            {
                EditedPSALs[(int)dgvPSAL[colIndex, i].Value].step = EditedPSALs[(int)dgvPSAL[colIndex, i].Value].step + 1;
            }

            EditedPSALs.Add(
                new PSAL()
                {
                    rec = EditedPSALs.Count(),
                    val1 = 0,
                    val2 = 0,
                    val3 = 0,
                    psal = EditedPSALs[(int)dgvPSAL[colIndex, rowIndex].Value].psal,
                    code = EditedPSALs[(int)dgvPSAL[colIndex, rowIndex].Value].code,
                    step = EditedPSALs[(int)dgvPSAL[colIndex, rowIndex].Value].step + 1
                });

            dgvPSAL.DataSource = EditedPSALs.Where(psal => psal.psal == ((PLRR)cbxPSAL.SelectedItem).psal).OrderBy(s => s.step).ToList();

            dgvPSAL.ShowCellToolTips = true;
            foreach (DataGridViewRow row in dgvPSAL.Rows) foreach (DataGridViewCell cell in row.Cells) cell.ToolTipText = "Right-Click to insert/delete step.";

            dgvPSAL.Height = (dgvPSAL.RowCount + 1) * dgvPSAL.ColumnHeadersHeight + dgvPSAL.RowCount + 2;
            Height = dgvPSAL.Location.Y + dgvPSAL.Height + 12;
        }

        private void cmsEditPSAL_Delete_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvPSAL.CurrentCell.RowIndex;
            int colIndex = dgvPSAL.Columns["rec"].Index;
            int record = (int)dgvPSAL[colIndex, rowIndex].Value;

            Console.WriteLine(EditedPSALs.Where(item => item.rec == record).First().ToString());

            if (rowIndex < dgvPSAL.RowCount - 1)
            {
                for (int i = rowIndex + 1; i < dgvPSAL.RowCount; i++)
                {
                    int RecordToIncrement = (int)dgvPSAL[colIndex, i].Value;
                    EditedPSALs[RecordToIncrement].step = EditedPSALs[RecordToIncrement].step - 1;
                }
            }

            for (int i = record; i < EditedPSALs.Count(); i++)
            {
                EditedPSALs[i].rec = EditedPSALs[i].rec - 1;
            }

            EditedPSALs.RemoveAt(record);

            dgvPSAL.DataSource = EditedPSALs.Where(psal => psal.psal == ((PLRR)cbxPSAL.SelectedItem).psal).OrderBy(s => s.step).ToList();

            dgvPSAL.ShowCellToolTips = true;
            foreach (DataGridViewRow row in dgvPSAL.Rows) foreach (DataGridViewCell cell in row.Cells) cell.ToolTipText = "Right-Click to insert/delete step.";

            dgvPSAL.Height = (dgvPSAL.RowCount + 1) * dgvPSAL.ColumnHeadersHeight + dgvPSAL.RowCount + 2;
            Height = dgvPSAL.Location.Y + dgvPSAL.Height + 12;
        }
    }
}
