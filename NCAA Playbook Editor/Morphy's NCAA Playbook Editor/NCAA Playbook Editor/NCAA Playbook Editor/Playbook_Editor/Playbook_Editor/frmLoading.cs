using System;
using System.Drawing;
using System.Windows.Forms;

namespace Playbook_Editor
{
    public partial class frmLoading : Form
    {
        private int FrameNum = 0;
        private frmPlaybook playbookFormRef;

        public frmLoading(frmPlaybook playbookFormHandle)
        {
            playbookFormRef = playbookFormHandle;
            InitializeComponent();
        }

        private void frmLoading_Load(object sender, EventArgs e)
        {
            CenterToForm(lblLoading, true, false);
            Location = new Point(
                playbookFormRef.Location.X + (playbookFormRef.Width / 2) - (Width / 2),
                playbookFormRef.Location.Y + (playbookFormRef.Height / 2) - (Height / 2)
                );
            timer1.Enabled = true;
            timer2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FrameNum = ++FrameNum % Program.Loading_Football.Length;
            BackgroundImage = Program.Loading_Football[FrameNum];
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            switch (lblLoading.Text)
            {
                case "Loading":
                    lblLoading.Text = "Loading.";
                    break;
                case "Loading.":
                    lblLoading.Text = "Loading..";
                    break;
                case "Loading..":
                    lblLoading.Text = "Loading...";
                    break;
                case "Loading...":
                    lblLoading.Text = "Loading";
                    break;
            }
        }

        private void CenterToForm(Control control, bool resizeW, bool resizeH)
        {
            if (resizeW)
            {
                control.Location = new Point(
                (Width / 2) - (control.Width / 2),
                control.Location.Y
                );
            }
            if (resizeH)
            {
                control.Location = new Point(
                control.Location.X,
                (Height / 2) - (control.Height / 2)
                );
            }
        }
    }
}
