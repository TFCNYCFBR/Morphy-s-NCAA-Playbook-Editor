using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Playbook_Editor
{
    public partial class frmPSALVisualizer : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private PSALroute psal = new PSALroute();
        private Image defaultImage, gImagePSALVis;
        private string PSALfileDir, PSALimageDir;
        private ToolTip toolTip = new ToolTip();
        private Point LOS;
        private Point cursor = Point.Empty;
        private int x, y;
        private bool dragging = false;
        private Color psalIconColor = Color.White;

        public frmPSALVisualizer()
        {
            InitializeComponent();
            AddDrag(mnuPSALVis);

            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            mnuPSALVis.Renderer = new MyRenderer();
            dgvPSALs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvPSALs.AllowUserToResizeRows = false;

            defaultImage = picPSAL.Image;

            //set player position
            psal.playerXY = new XY{ X = (int)(picPSAL.Width / 2), Y = (int)(picPSAL.Height / 1.2) };

            //set the LOS
            LOS = new Point((int)(picPSAL.Width / 2), (int)(picPSAL.Height / 1.2));

            try { LoadPSAL_CSV("PSAL_PLRR_ARTL.csv"); }
            catch { MessageBox.Show("Can not find PSAL csv.\n\n" + "Open a PSLO, PSLD or PSAL csv from the file nemu", "Failed to load PSAL.csv", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
            dgvPSALs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        #region Menu Strip

        private void mnuOpenPSAL_PSALVis_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV Files | *.csv";
            openFileDialog1.DefaultExt = "csv";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //store the folder where the PSAL CSV is located
                PSALfileDir = openFileDialog1.FileName.Substring(0, openFileDialog1.FileName.LastIndexOf('\\'));

                //Load the PSAL CSV to the dataGrid
                LoadPSAL_CSV(openFileDialog1.FileName);
            }
        }

        private void mnuSavePSALImage_PSALVis_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ValidateNames = true;
            saveFileDialog1.CheckPathExists = true;

            saveFileDialog1.Filter = "JPEG | *.jpg";
            saveFileDialog1.DefaultExt = "jpg";

            saveFileDialog1.FileName = saveFileDialog1.InitialDirectory + PSALimageDir + "\\PLRR_" + psal.PLRR.ToString() + "\\" + psal.ID.ToString() + ".jpg";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //store the folder where the PSAL images are saved
                PSALimageDir = saveFileDialog1.FileName.Substring(0, saveFileDialog1.FileName.LastIndexOf('\\'));

                //save PSAL image
                gImagePSALVis.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
            }
        }

        private void mnuSaveAllPSALs_PSALVis_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ValidateNames = false;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.FileName = "Folder Selection.";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFileDialog1.FileName = saveFileDialog1.FileName.Substring(0, saveFileDialog1.FileName.LastIndexOf('\\'));
                drawPSAL(true);
            }
        }

        private void mnuExit_PSALVis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadPSAL_CSV(string PSALfilePath)
        {
            DataTable dt = new DataTable();
            string[] lines = File.ReadAllLines(PSALfilePath);

            dgvPSALs.Enabled = false;
            if (lines.Length > 0)
            {
                //first line to create header

                string firstLine = lines[0];

                string[] headerLabels = firstLine.Split(',');

                foreach (string headerWord in headerLabels)
                {
                    dt.Columns.Add(new DataColumn(headerWord).ToString(), typeof(int));
                }

                //for data

                for (int r = 1; r < lines.Length; r++)
                {
                    string[] dataWords = lines[r].Split(',');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;
                    foreach (string headerWord in headerLabels)
                    {
                        dr[headerWord] = dataWords[columnIndex++];
                    }

                    dt.Rows.Add(dr);
                }
            }

            if (dt.Rows.Count > 0)
            {
                dgvPSALs.DataSource = dt;
            }

            dgvPSALs.Enabled = true;
            dgvPSALs.AutoResizeColumns();
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
            }
            else
            {
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

        private void chbMovePlayer_CheckedChanged(object sender, EventArgs e)
        {
            if (chbMovePlayer.CheckState == CheckState.Checked)
            {
                chbMovePlayer.BackColor = Color.Yellow;
                chbMovePlayer.Font = new Font(chbMovePlayer.Font, FontStyle.Bold);
            }
            else
            {
                chbMovePlayer.BackColor = Color.White;
                chbMovePlayer.Font = new Font(chbMovePlayer.Font, FontStyle.Regular);
            }
        }

        private void chbMovePlayer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //reset player position
                psal.playerXY = new XY { X = (int)(picPSAL.Width / 2), Y = (int)(picPSAL.Height / 1.2) };

                //update PSAL image
                drawPSAL(false);
            }
        }

        private void chbMovePlayer_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(chbMovePlayer, "Check this box, then click a spot on the field\n to move the player's position.\n\n" + "Right-click to reset the player position.");
        }

        private void btnDrawPSAL_Click(object sender, EventArgs e)
        {
            drawPSAL(false);
        }

        private void drawPSAL(bool SavePSAL)
        {
            psal.steps = getSTEPs();

            //Re-order list
            psal.steps = psal.steps.OrderBy(s => s.PSAL).ThenBy(s => s.step).Cast<PSALroute.Steps>().ToList();

            //Print list to console
            foreach (PSALroute.Steps step in psal.steps) Console.WriteLine(step);

            PSALroute currentPSAL = new PSALroute();
            currentPSAL.playerXY = psal.playerXY;
            currentPSAL.RouteColor = PSALColor.Undefined;

            bool DrawPSAL = false;

            if (psal.steps.Count > 0)
            {
                pBarDrawPSAL.Maximum = psal.steps.Count - 1;

                for (int i = 0; i < psal.steps.Count; i++)
                {
                    currentPSAL.ID = psal.steps[i].PSAL;                                           //get PSAL ID
                    currentPSAL.PLRR = psal.steps[i].PLRR;                                         //get PLRR ID
                    currentPSAL.ARTL = psal.steps[i].ARTL;                                         //get ARTL ID
                    currentPSAL.steps = new List<PSALroute.Steps>();

                    for (int n = i; n < psal.steps.Count; n++)
                    {
                        if (!(psal.steps[n].PSAL == currentPSAL.ID))
                        {
                            i = n - 1;
                            DrawPSAL = true;                                              //indicate a PSAL should be drawn on the break
                            break;
                        }
                        else if (n == (psal.steps.Count - 1))
                        {
                            currentPSAL.steps.Add(psal.steps[n]);
                            i = n;
                            DrawPSAL = true;                                              //indicate a PSAL should be drawn on the break
                            break;
                        }
                        else
                        {
                            currentPSAL.steps.Add(psal.steps[n]);
                        }
                    }

                    if (DrawPSAL == true)
                    {
                        ClearField();
                        Graphics g = Graphics.FromImage(gImagePSALVis);

                        //Convert PSAL
                        Program.ConvertPSAL(currentPSAL, LOS, false);

                        //draw PSAL
                        Program.DrawPSAL(g, currentPSAL);

                        //draw player icon
                        Program.DrawPSALIcon(g, psalIconColor, currentPSAL, 12, LOS);

                        //assign field and graphics to picture box
                        picPSAL.Image = gImagePSALVis;

                        pBarDrawPSAL.Value = i;

                        //save PSAL as jpeg
                        if (SavePSAL == true)
                        {
                            string saveFolder = saveFileDialog1.FileName + "\\PLRR_" + currentPSAL.PLRR.ToString() + "\\";
                            Directory.CreateDirectory(saveFolder);
                            gImagePSALVis.Save(saveFolder + currentPSAL.ID.ToString() + "_ARTL_" + currentPSAL.ARTL.ToString() + ".jpg", ImageFormat.Jpeg);
                        }

                        currentPSAL = new PSALroute();
                        currentPSAL.playerXY = psal.playerXY;
                        currentPSAL.RouteColor = PSALColor.Undefined;
                        DrawPSAL = false;
                    }
                }
            }
        }

        private List<PSALroute.Steps> getSTEPs()
        {
            //Read the rows in dgvPSALs from startRow to endRow and store them as a list in route.steps

            return (from row in dgvPSALs.SelectedRows.OfType<DataGridViewRow>()
            select new PSALroute.Steps()
            {
                rec = row.Index,
                code = (int)row.Cells["code"].Value,
                val1 = (int)row.Cells["val1"].Value,
                val2 = (int)row.Cells["val2"].Value,
                val3 = (int)row.Cells["val3"].Value,
                PSAL = (int)row.Cells["PSAL"].Value,
                step = (int)row.Cells["step"].Value,
                PLRR = (int)row.Cells["PLRR"].Value,
                ARTL = (int)row.Cells["ARTL"].Value
            }).ToList();
        }

        private void ClearField()
        {
            //Clear the canvas
            picPSAL.Image = defaultImage;

            //Create a clone of the field to draw the formation
            gImagePSALVis = (Image)defaultImage.Clone();
        }

        private void picPSAL_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            drawPSAL(false);
        }

        private void picPSAL_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle playerIconArea = new Rectangle(psal.playerXY.X - 9, psal.playerXY.Y - 9, 18, 18);
            cursor.X = MousePosition.X - this.Location.X - picPSAL.Location.X;
            cursor.Y = MousePosition.Y - this.Location.Y - picPSAL.Location.Y;

            if (playerIconArea.Contains(cursor))
            {
                psalIconColor = Color.Red;
            }
            else
            {
                psalIconColor = Color.White;
            }

            if (dragging)
            {
                psal.playerXY.X = cursor.X + x;
                psal.playerXY.Y = cursor.Y + y;
            }

            drawPSAL(false);
        }

        private void picPSAL_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle playerIconArea = new Rectangle(psal.playerXY.X - 9, psal.playerXY.Y - 9, 18, 18);

            if (playerIconArea.Contains(cursor))
            {
                psalIconColor = Color.Red;

                dragging = true;

                x = psal.playerXY.X - cursor.X;
                y = psal.playerXY.Y - cursor.Y;
            }
        }

        private void btnDrawPSAL_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(btnDrawPSAL, "Select a PSAL range and click 'Draw PSAL' to draw the PSAL. \n\n" + "To save multiple PSALs, select more than one PSAL range\n and select 'PSAL(s)>Save All Selected PSALs to jpeg' from the menu.");
        }
    }
}
