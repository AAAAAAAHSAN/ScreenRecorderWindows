using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenRecorder
{
    public partial class Form1 : Form
    {
        bool folderSelected = false;
        string outputPath = "";
        string finalVideoName = "FinalVideo.mp4";

        ScreenRecorder screenRecorder = new ScreenRecorder(new Rectangle(), "");

        public Form1()
        {
            InitializeComponent();
            this.Text = "Screen Recorder by AHSAN";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(folderSelected)
            {
                screenRecorder.setVideoName(finalVideoName);
                timerRecord.Start();
            }
            else
            {
                MessageBox.Show("You must select an output folder before recording", "Error");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "Select an output folder";

            if(folderBrowser.ShowDialog()== DialogResult.OK)
            {
                outputPath = folderBrowser.SelectedPath;
                folderSelected = true;

                Rectangle bounds = Screen.FromControl(this).Bounds;
                screenRecorder = new ScreenRecorder(bounds, outputPath);
            }
            else
            {
                MessageBox.Show("Please select a folder", "Error");
            }
        }

        private void TimerRecord_Tick(object sender, EventArgs e)
        {
            screenRecorder.RecordAudio();
            screenRecorder.RecordVideo();

            lblTime.Text = screenRecorder.GetElapsed();
        }

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        private void Button2_Click(object sender, EventArgs e)
        {
            

            timerRecord.Stop();
            screenRecorder.Stop();
            //Application.Restart();

            
        }
    }
}
