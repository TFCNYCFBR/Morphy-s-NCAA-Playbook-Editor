using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace PSAL_Visualizer
{
    public partial class PSALVis : Form
    {
        public class PSAL
        {
            public class Steps
            {
                public int code { get; set; }
                public int val1 { get; set; }
                public int val2 { get; set; }
                public int val3 { get; set; }
                public Point XY { get; set; }

                public Steps DeepCopy()
                {
                    Steps other = (Steps)MemberwiseClone();
                    return other;
                }
            }

            public int ID;
            public int PLRR;
            public Color routeColor = Color.Black;
            public double AngleRatio = 0.35556;
            public Point playerXY;
            public int startIndex, endIndex;
            public List<Steps> steps;
            public Point[] route;
            public Point[] routeOption1;
            public Point[] routeOption2;
        }

        private class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer() : base(new MyColors()) { }
        }

        private class MyColors : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return Color.Yellow; }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(0, Color.Yellow); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.Yellow; }
            }
            public override Color MenuItemBorder
            {
                get { return Color.FromArgb(0, Color.Yellow); }
            }

        }

        PSAL psal = new PSAL();
        Image defaultImage, gImage;
        string PSALfileDir, PSALimageDir;
        public Point mouseLocation, mousePos;
        ToolTip toolTip = new ToolTip();

        public PSALVis()
        {
            InitializeComponent();

            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            menuStrip1.Renderer = new MyRenderer();

            btn_Close.MouseEnter += new EventHandler(btn_Close_MouseEnter);
            btn_Close.MouseLeave += new EventHandler(btn_Close_MouseLeave);
            btn_Min.MouseEnter += new EventHandler(btn_Min_MouseEnter);
            btn_Min.MouseLeave += new EventHandler(btn_Min_MouseLeave);
            btn_Max.MouseEnter += new EventHandler(btn_Max_MouseEnter);
            btn_Max.MouseLeave += new EventHandler(btn_Max_MouseLeave);

            defaultImage = picPSAL.Image;

            //set player position
            psal.playerXY = new Point((int)(picPSAL.Width / 2), (int)(picPSAL.Height / 1.2));

            LoadPSAL_CSV("C:\\Users\\Chris\\Documents\\Madden NFL 20\\Modding\\Mods\\playbooks\\offense\\PSLO_Routes.csv");
            dgvPSALs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void openPSALsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
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
                gImage.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
            }
        }

        private void saveAllImagesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.MessageLoop)
            {
                // WinForms app
                Application.Exit();
            }
            else
            {
                // Console app
                Environment.Exit(1);
            }
        }

        private void LoadPSAL_CSV(string PSALfilePath)
        {
            DataTable dt = new DataTable();
            string[] lines = System.IO.File.ReadAllLines(PSALfilePath);
            if (lines.Length > 0)
            {
                //first line to create header

                string firstLine = lines[0];

                string[] headerLabels = firstLine.Split(',');

                foreach (string headerWord in headerLabels)
                {
                    dt.Columns.Add(new DataColumn(headerWord));
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

            dgvPSALs.AutoResizeColumns();
        }

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
            if (Application.MessageLoop)
            {
                // WinForms app
                Application.Exit();
            }
            else
            {
                // Console app
                Environment.Exit(1);
            }
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

        private void pnlTopBorder_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void pnlTopBorder_MouseMove(object sender, MouseEventArgs e)
        {
            while (e.Button == MouseButtons.Left && WindowState == FormWindowState.Normal)
            {
                mousePos = MousePosition;
                mousePos.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePos;
                if (MousePosition.Y < 1)
                {
                    WindowState = FormWindowState.Maximized;
                }
                break;
            }
        }

        private void pnlTopBorder_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                CenterToScreen();
            }
            else
            {
                WindowState = FormWindowState.Maximized;
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
                psal.playerXY = new Point((int)(picPSAL.Width / 2), (int)(picPSAL.Height / 1.2));

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

        #region PSAL Get, Convert and Draw

        private void drawPSAL(bool SavePSAL)
        {
            Int32 selectedRowCount = dgvPSALs.Rows.GetRowCount(DataGridViewElementStates.Selected);
            Int32 startRow = dgvPSALs.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            Int32 endRow = dgvPSALs.Rows.GetLastRow(DataGridViewElementStates.Selected);
            psal.startIndex = startRow;
            bool PSALtoDraw = false;
            if (endRow > 0) { pBarDrawPSAL.Maximum = endRow; }

            if (selectedRowCount > 0)
            {
                //get initial PSAL ID
                psal.ID = Int32.Parse(dgvPSALs[3, startRow].Value.ToString());
                psal.startIndex = startRow;

                for (int i = startRow; i <= endRow; i++)
                {
                    psal.ID = Int32.Parse(dgvPSALs[3, i].Value.ToString());                 //get PSAL ID
                    psal.PLRR = Int32.Parse(dgvPSALs[6, i].Value.ToString());               //get PLRR ID

                    for (int n = i; n <= endRow; n++)
                    {
                        //get PSAL ID
                        int stepPSALID = Int32.Parse(dgvPSALs[3, n].Value.ToString());

                        if (!(stepPSALID == psal.ID))
                        {
                            i = n;
                            psal.endIndex = n - 1;                                          //get end of PSAL index
                            PSALtoDraw = true;                                              //indicate a PSAL should be drawn on the break
                            break;
                        }
                        if (i == endRow)
                        {
                            i = n;
                            psal.endIndex = n;                                              //get end of PSAL index
                            PSALtoDraw = true;                                              //indicate a PSAL should be drawn on the break
                            break;
                        }
                    }

                    if (PSALtoDraw == true)
                    {
                        //clear the canvas
                        picPSAL.Image = defaultImage;
                        gImage = (Image)defaultImage.Clone();

                        //gather PSAL(s)
                        getPSAL(psal.startIndex, psal.endIndex);

                        Graphics g = Graphics.FromImage(gImage);

                        //convert PSAL step vals to xy offsets
                        if (IsOptionRoute(psal.steps) == "option1")
                        {
                            psal.route = convertPSAL(psal.steps, "baseOption");

                            //draw PSAL
                            drawRoute(g, psal.route, psal.routeColor);

                            psal.routeOption1 = convertPSAL(psal.steps, "option1");

                            //draw PSAL
                            drawRoute(g, psal.routeOption1, psal.routeColor);
                        }
                        else if (IsOptionRoute(psal.steps) == "option2")
                        {
                            psal.route = convertPSAL(psal.steps, "baseOption");

                            //draw PSAL
                            drawRoute(g, psal.route, psal.routeColor);

                            psal.routeOption1 = convertPSAL(psal.steps, "option1");

                            //draw PSAL
                            drawRoute(g, psal.routeOption1, psal.routeColor);

                            psal.routeOption2 = convertPSAL(psal.steps, "option2");

                            //draw PSAL
                            drawRoute(g, psal.routeOption2, psal.routeColor);
                        }
                        else if (IsOptionRoute(psal.steps) == "no")
                        {
                            psal.route = convertPSAL(psal.steps, "route");

                            //draw PSAL
                            drawRoute(g, psal.route, psal.routeColor);
                        }

                        //draw player icon
                        drawPlayerCircle(g, Color.White, psal.playerXY.X, psal.playerXY.Y, 12);

                        //assign field and graphics to picture box
                        picPSAL.Image = gImage;

                        //save PSAL as jpeg
                        if (SavePSAL == true)
                        {
                            pBarDrawPSAL.Value = i;
                            string saveFolder = saveFileDialog1.FileName + "\\PLRR_" + psal.PLRR.ToString() + "\\";
                            Directory.CreateDirectory(saveFolder);
                            gImage.Save(saveFolder + psal.ID.ToString() + ".jpg", ImageFormat.Jpeg);
                        }

                        PSALtoDraw = false;
                        psal.startIndex = i;                                                //get start of next PSAL index
                    }
                }
            }
        }

        private void getPSAL(int startRow, int endRow)
        {
            //Read the rows in dgvPSALs from startRow to endRow and store them as a list in route.steps

            psal.steps = new List<PSAL.Steps>();

            for (int i = startRow; i <= endRow; i++)
            {
                psal.steps.Add(new PSAL.Steps
                {
                    code = Int32.Parse(dgvPSALs[4, i].Value.ToString()),
                    val1 = Int32.Parse(dgvPSALs[0, i].Value.ToString()),
                    val2 = Int32.Parse(dgvPSALs[1, i].Value.ToString()),
                    val3 = Int32.Parse(dgvPSALs[2, i].Value.ToString())
                });
            }

            psal.steps.Insert(0, new PSAL.Steps { code = 0, val1 = 0, val2 = 0, val3 = 0, XY = psal.playerXY });
        }

        public string IsOptionRoute(List<PSAL.Steps> steps)
        {
            string isOption = "no";
            foreach (PSAL.Steps step in steps)
            {
                if (step.code == 36)
                {
                    if (!(step.val3 == 255))
                    {
                        isOption = "option2";
                    }
                    else
                    {
                        isOption = "option1";
                    }
                    return isOption;
                }
                else
                {
                    isOption = "no";
                }
            }
            return isOption;
        }

        private Point[] convertPSAL(List<PSAL.Steps> steps, string routeType)
        {
            //translate the PSAL vals to XY in pixels based on the player position and define the route color

            for (int i = 1; i < steps.Count; i++)
            {
                switch (steps[i].code)
                {
                    case 1:
                        #region End of Route

                        steps.RemoveAt(i);
                        i--;
                        break;

                    #endregion

                    case 3:
                        #region MoveDirDist

                        //convert to offest
                        steps[i].XY = MoveDirDistToXY(steps[i].val1, steps[i].val2);
                        steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);

                        //assign route color
                        if (psal.routeColor.Name == "Black")
                        {
                            psal.routeColor = Color.FromArgb(255, 191, 181, 84);
                        }
                        break;

                    #endregion

                    case 4:
                        #region MoveDirDistConst

                        //convert to offest
                        steps[i].XY = MoveDirDistToXY(steps[i].val1, steps[i].val2);
                        steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);

                        //assign route color
                        if (psal.routeColor.Name == "Black")
                        {
                            psal.routeColor = Color.FromArgb(255, 191, 181, 84);
                        }
                        break;

                    #endregion

                    case 8:
                        #region Receiver Run Route

                        //convert to offest
                        steps[i].XY = MoveDirDistToXY(steps[i].val1, steps[i].val2);
                        steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);

                        //assign route color
                        if (psal.routeColor.Name == "Black")
                        {
                            psal.routeColor = Color.FromArgb(255, 191, 181, 84);
                        }
                        break;

                    #endregion

                    case 7:
                        #region QB Scramble

                        steps.RemoveAt(i);

                        //assign route color
                        psal.routeColor = Color.FromArgb(255, 33, 255, 131);
                        i--;
                        break;

                    #endregion

                    case 9:
                        #region Receiver Cut Move
                        //1 = 45 degrees
                        if (steps[i].val2 == 1)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(steps[i - 1].val2 - (45 * psal.AngleRatio)));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(steps[i - 1].val2 + (45 * psal.AngleRatio)));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //2 = 90 degrees
                        if (steps[i].val2 == 2)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(steps[i - 1].val2 - (90 * psal.AngleRatio)));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(steps[i - 1].val2 + (90 * psal.AngleRatio)));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //3 = 22 degrees
                        if (steps[i].val2 == 3)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(steps[i - 1].val2 - (22 * psal.AngleRatio)));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(steps[i - 1].val2 + (22 * psal.AngleRatio)));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //4 = 67 degrees
                        if (steps[i].val2 == 4)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(steps[i - 1].val2 - (67 * psal.AngleRatio)));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(steps[i - 1].val2 + (67 * psal.AngleRatio)));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //5 = Curl
                        if (steps[i].val2 == 5)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(steps[i - 1].val2 - (135 * psal.AngleRatio)));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(steps[i - 1].val2 + (135 * psal.AngleRatio)));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //7 = HitchComback, 8 = HitchGoIn and 9 = HitchGoOut
                        if (steps[i].val2 == 7 || steps[i].val2 == 8 || steps[i].val2 == 9)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(8, (int)(steps[i - 1].val2 - (105 * psal.AngleRatio)));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(8, (int)(steps[i - 1].val2 + (105 * psal.AngleRatio)));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //10 = OutAndUp
                        if (steps[i].val2 == 10)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(steps[i - 1].val2 - (105 * psal.AngleRatio)));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(steps[i - 1].val2 + (105 * psal.AngleRatio)));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //11 = Smash and 17 = SmashQuick
                        if (steps[i].val2 == 11 || steps[i].val2 == 17)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(32, (int)(-10 * psal.AngleRatio));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(32, (int)(190 * psal.AngleRatio));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //13 = WRScrn
                        if (steps[i].val2 == 13)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(-15 * psal.AngleRatio));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(105 * psal.AngleRatio));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);

                            //assign route color
                            if (psal.routeColor.Name == "Black")
                            {
                                psal.routeColor = Color.FromArgb(255, 191, 181, 84);
                            }
                        }

                        //14 = 90Inside
                        if (steps[i].val2 == 14)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(0 * psal.AngleRatio));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(180 * psal.AngleRatio));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //16 = 180Partial
                        if (steps[i].val2 == 16)
                        {
                            steps[i].XY = MoveDirDistToXY(24, (int)(270 * psal.AngleRatio));
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //18 = Hitch
                        if (steps[i].val2 == 18)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(32, (int)(steps[i - 1].val2 - (105 * psal.AngleRatio)));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(32, (int)(steps[i - 1].val2 + (105 * psal.AngleRatio)));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //20 = ShakeCut
                        if (steps[i].val2 == 20)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(steps[i - 1].val2 - (60 * psal.AngleRatio)));
                                steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                                steps[i].val2 = (int)(steps[i - 1].val2 - (60 * psal.AngleRatio));
                                i++;
                                steps.Insert(i, steps[i - 1].DeepCopy());
                                steps[i].XY = MoveDirDistToXY(8, (int)(steps[i - 1].val2 + (120 * psal.AngleRatio)));
                                steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(steps[i - 1].val2 + (60 * psal.AngleRatio)));
                                steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                                steps[i].val2 = (int)(steps[i - 1].val2 + (60 * psal.AngleRatio));
                                i++;
                                steps.Insert(i, steps[i - 1].DeepCopy());
                                steps[i].XY = MoveDirDistToXY(8, (int)(steps[i - 1].val2 - (120 * psal.AngleRatio)));
                                steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                            }
                        }

                        //21 = StutterCut
                        if (steps[i].val2 == 21)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(8, (int)(steps[i - 1].val2 - (67 * psal.AngleRatio)));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(8, (int)(steps[i - 1].val2 + (67 * psal.AngleRatio)));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //22 = HingeCut
                        if (steps[i].val2 == 22)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(32, (int)(145 * psal.AngleRatio));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(32, (int)(35 * psal.AngleRatio));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //23 = PostCorner
                        if (steps[i].val2 == 23)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(48, (int)(steps[i - 1].val2 + (45 * psal.AngleRatio)));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(48, (int)(steps[i - 1].val2 - (45 * psal.AngleRatio)));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //25 = StutterStreak
                        if (steps[i].val2 == 25)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(4, (int)(steps[i - 1].val2 + (90 * psal.AngleRatio)));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(4, (int)(steps[i - 1].val2 - (90 * psal.AngleRatio)));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //26 = WR Swing
                        if (steps[i].val2 == 26)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(-35 * psal.AngleRatio));
                                steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                                steps[i].val2 = (int)(-35 * psal.AngleRatio);
                                i++;
                                steps.Insert(i, steps[i - 1].DeepCopy());
                                steps[i].XY = MoveDirDistToXY(24, (int)(0 * psal.AngleRatio));
                                steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(215 * psal.AngleRatio));
                                steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                                steps[i].val2 = (int)(215 * psal.AngleRatio);
                                i++;
                                steps.Insert(i, steps[i - 1].DeepCopy());
                                steps[i].XY = MoveDirDistToXY(24, (int)(180 * psal.AngleRatio));
                                steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                            }
                        }

                        //28 = Sluggo
                        if (steps[i].val2 == 28)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(22 * psal.AngleRatio));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(158 * psal.AngleRatio));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        //29 = Out n Up
                        if (steps[i].val2 == 29)
                        {
                            if (steps[i].val1 == 1)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(180 * psal.AngleRatio));
                            }
                            else if (steps[i].val1 == 2)
                            {
                                steps[i].XY = MoveDirDistToXY(24, (int)(0 * psal.AngleRatio));
                            }
                            steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        }

                        break;
                    #endregion

                    case 10:
                        #region Receiver Get Open

                        steps.RemoveAt(i);
                        i--;
                        break;

                    #endregion

                    case 12:
                        #region Option Handoff

                        steps.RemoveAt(i);
                        i--;
                        break;

                    #endregion

                    case 13:
                        #region Receive Hand Off

                        //convert to offest
                        steps[i].XY = MoveDirDistToXY(steps[i].val1, steps[i].val2);
                        steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);

                        //assign route color
                        psal.routeColor = Color.FromArgb(255, 202, 48, 84);
                        break;

                    #endregion

                    case 14:
                        #region PassBlock

                        if (steps[i].val1 == 0)
                        {
                            //assign route color
                            psal.routeColor = Color.FromArgb(255, 160, 160, 160);
                            //convert to offest
                            if (i <= 1)
                            {
                                steps[i].XY = MoveDirDistToXY(16, 96);
                            }
                        }
                        else
                        {
                            //assign route color
                            psal.routeColor = Color.FromArgb(255, 59, 106, 233);
                        }
                        steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);

                        break;

                    #endregion

                    case 15:
                        #region RunBlock

                        if (steps[i].val1 == 0)
                        {
                            //assign route color
                            psal.routeColor = Color.FromArgb(255, 160, 160, 160);
                            //convert to offest
                            if (i <= 1)
                            {
                                steps[i].XY = MoveDirDistToXY(16, 32);
                            }
                        }
                        else
                        {
                            //assign route color
                            psal.routeColor = Color.FromArgb(255, 59, 106, 233);
                        }
                        steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);

                        break;

                    #endregion

                    case 18:
                        #region LeadBlock

                        //convert to offest
                        steps[i].XY = MoveDirDistToXY(steps[i].val1, steps[i].val2);
                        steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);

                        //assign route color
                        psal.routeColor = Color.FromArgb(255, 160, 160, 160);
                        break;

                    #endregion

                    case 25:
                        #region Delay

                        steps.RemoveAt(i);
                        i--;
                        break;

                    #endregion

                    case 26:
                        #region Initial Anim

                        steps.RemoveAt(i);
                        i--;
                        break;

                    #endregion

                    case 32:
                        #region Receive Pitch

                        //convert to offest
                        steps[i].XY = MoveDirDistToXY(steps[i].val1, steps[i].val2);
                        steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);

                        //assign route color
                        psal.routeColor = Color.FromArgb(255, 202, 48, 84);
                        break;

                    #endregion

                    case 35:
                        #region Head Turn Run Route

                        //convert to offest
                        steps[i].XY = MoveDirDistToXY(steps[i].val1, steps[i].val2);
                        steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                        break;

                    #endregion

                    case 36:
                        #region Option Route

                        switch (routeType)
                        {
                            case "baseOption":    //Option Route Base
                                steps[i].XY = getOptionOffset(steps[i].val1);
                                steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);
                                break;
                            case "option1":    //Option Route 1
                                steps[i].XY = getOptionOffset(steps[i].val2);
                                steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);

                                //assign route color
                                psal.routeColor = Color.FromArgb(128, psal.routeColor.R, psal.routeColor.G, psal.routeColor.B);
                                break;
                            case "option2":    //Option Route 2
                                steps[i].XY = getOptionOffset(steps[i].val3);
                                steps[i].XY = new Point(steps[i - 1].XY.X + steps[i].XY.X, steps[i - 1].XY.Y + steps[i].XY.Y);

                                //assign route color
                                psal.routeColor = Color.FromArgb(128, psal.routeColor.R, psal.routeColor.G, psal.routeColor.B);
                                break;
                        }
                        break;

                    #endregion

                    case 37:
                        #region Option Route Extra Info

                        steps.RemoveAt(i);
                        i--;
                        break;

                    #endregion

                    case 43:
                        #region Option Follow

                        steps.RemoveAt(i);
                        i--;
                        break;

                    #endregion

                    case 45:
                        #region Auto Motion

                        //convert to offest
                        steps[i].XY = new Point((int)(steps[i].val1 / .8), (int)(Math.Abs(steps[i].val2) / .8));
                        steps[i].XY = new Point(267 + steps[i].XY.X, 500 + steps[i].XY.Y);

                        //assign route color
                        psal.routeColor = Color.FromArgb(255, 161, 233, 238);
                        break;

                    #endregion

                    case 46:
                        #region Auto Motion Snap

                        steps.RemoveAt(i);
                        i--;
                        break;

                    #endregion

                    case 48:
                        #region Player offset

                        steps.RemoveAt(i);
                        i--;
                        break;

                    #endregion

                    case 58:
                        #region Animation

                        steps.RemoveAt(i);
                        i--;
                        break;

                    #endregion

                    case 255:
                        #region End of Route

                        steps.RemoveAt(i);
                        i--;
                        break;

                        #endregion
                }
            }

            Point[] points = new Point[steps.Count];
            for (int i = 0; i < steps.Count; i++)
            {
                points[i] = steps[i].XY;
            }
            return points;
        }

        private Point MoveDirDistToXY(int dist, int dir)
        {
            double angle = Math.PI * (dir / psal.AngleRatio) / 180.0;
            double sinAngle = Math.Sin(angle);
            double cosAngle = Math.Cos(angle);

            if (dist > 128)
            {
                dist = (int)(dist * .5);
            }

            return new Point((int)(cosAngle * (dist / .8)), (int)(sinAngle * (dist / .8)) * -1);
        }

        private Point XYtoMoveDirDist(int offsetX, int offsetY)
        {
            double angle = (Math.Atan2(offsetY, offsetX) * (180 / Math.PI)) * psal.AngleRatio;
            double hypotenuse = Math.Sqrt(Math.Pow(offsetY, 2) + Math.Pow(offsetX, 2)) * .8;

            return new Point((int)hypotenuse, (int)angle);
        }

        private Point getOptionOffset(int code)
        {
            if (code == 0)     //Curl left
            {
                return MoveDirDistToXY(32, (int)(225 * psal.AngleRatio));
            }

            if (code == 1)    //Curl right
            {
                return MoveDirDistToXY(32, (int)(-45 * psal.AngleRatio));
            }

            if (code == 2)    //Post right
            {
                return MoveDirDistToXY(120, (int)(45 * psal.AngleRatio));
            }

            if (code == 3)    //Corner left
            {
                return MoveDirDistToXY(120, (int)(135 * psal.AngleRatio));
            }

            if (code == 5)     //Slant right
            {
                return MoveDirDistToXY(120, (int)(33 * psal.AngleRatio));
            }

            if (code == 6)    //Fade/Streak left
            {
                return MoveDirDistToXY(120, (int)(93 * psal.AngleRatio));
            }

            if (code == 7)    //Slant left
            {
                return MoveDirDistToXY(120, (int)(147 * psal.AngleRatio));
            }

            if (code == 8)    //Fade/Streak right
            {
                return MoveDirDistToXY(120, (int)(87 * psal.AngleRatio));
            }

            if (code == 9)     //In/Out right
            {
                return MoveDirDistToXY(120, (int)(0 * psal.AngleRatio));
            }

            if (code == 10)    //In/Out left
            {
                return MoveDirDistToXY(120, (int)(180 * psal.AngleRatio));
            }

            if (code == 11)   //Fade left
            {
                return MoveDirDistToXY(120, (int)(93 * psal.AngleRatio));
            }

            if (code == 12)    //Hitch left
            {
                return MoveDirDistToXY(32, (int)(225 * psal.AngleRatio));
            }

            if (code == 13)    //Hitch right
            {
                return MoveDirDistToXY(32, (int)(-45 * psal.AngleRatio));
            }

            if (code == 15)   //180 Partial / Drag left
            {
                return MoveDirDistToXY(120, (int)(177 * psal.AngleRatio));
            }

            if (code == 16)   //180 Partial / Drag right
            {
                return MoveDirDistToXY(120, (int)(3 * psal.AngleRatio));
            }

            if (code == 17)   //Drag right
            {
                return MoveDirDistToXY(120, (int)(3 * psal.AngleRatio));
            }

            if (code == 18)   //Drag left
            {
                return MoveDirDistToXY(120, (int)(177 * psal.AngleRatio));
            }

            if (code == 19)   //Hitch right
            {
                return MoveDirDistToXY(32, (int)(-45 * psal.AngleRatio));
            }

            if (code == 20)   //Hitch left
            {
                return MoveDirDistToXY(32, (int)(225 * psal.AngleRatio));
            }
            return new Point(0, 0);
        }

        private void picPSAL_MouseDown(object sender, MouseEventArgs e)
        {
            if (chbMovePlayer.CheckState == CheckState.Checked)
            {
                var mouseEventArgs = e as MouseEventArgs;
                psal.playerXY = new Point(mouseEventArgs.X, mouseEventArgs.Y);
                drawPSAL(false);
            }
            if (e.Button == MouseButtons.Right)
            {
                //reset player position
                psal.playerXY = new Point((int)(picPSAL.Width / 2), (int)(picPSAL.Height / 1.2));

                //update PSAL image
                drawPSAL(false);
            }
        }

        private void btnDrawPSAL_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(btnDrawPSAL, "Select a PSAL range and click 'Draw PSAL' to draw the PSAL. \n\n" + "To save multiple PSALs, select more than one PSAL range\n and select 'PSAL(s)>Save All Selected PSALs to jpeg' from the menu.");
        }

        private void drawRoute(Graphics g, Point[] route, Color routeColor)
        {
            Pen pen = new Pen(routeColor, 6);
            pen.LineJoin = LineJoin.Miter;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsPath blockingPath = new GraphicsPath();
            blockingPath.AddLine((float)-1.5, 0, (float)1.5, 0);
            CustomLineCap blockingEndCap = new CustomLineCap(null, blockingPath);

            // create AdjustableArrowCap or no end cap if blocking
            if (!(routeColor.R == 160) && !(routeColor.G == 160) && !(routeColor.B == 160))
            {
                pen.CustomEndCap = new AdjustableArrowCap(3, 4);
            }
            else
            {
                pen.CustomEndCap = blockingEndCap;
            }

            //draw PSAL
            if (route.Length > 1)
            {
                g.DrawLines(pen, route);
            }

            pen.Dispose();
            psal.routeColor = Color.Black;
        }

        private void drawPlayerCircle(Graphics g, Color color, double x, double y, int playerSize)
        {
            SolidBrush sb = new SolidBrush(color);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillEllipse(sb, (int)(x - (playerSize / 2)), (int)(y - (playerSize / 2)), playerSize, playerSize);
        }

        #endregion
    }
}
