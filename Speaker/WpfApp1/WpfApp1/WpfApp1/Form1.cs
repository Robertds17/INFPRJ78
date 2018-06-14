using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;
using Authentication;
using Newtonsoft.Json;
using APICalls;

namespace WpfApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            
        }
        APICalls.APICalls Calls = new APICalls.APICalls("cb5965649f4a4625b90751af48e7fc8b", "11146ff7de8b448580adbc2aa21965a4");
        public string text = "";
        public WaveIn waveSource = null;
        public WaveFileWriter waveFile = null;
        public string directory = @"D:\Code\School\WPFCongnitive\WpfApp1\WpfApp1\Files\temp.wav";
            //@"C:\Users\Dani\Documents\Visual studio code\School\Project\project7-8\WpfApp1\Files\temp.wav";

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button1.Enabled = true;

            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat(16000, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            waveFile = new WaveFileWriter(directory, waveSource.WaveFormat);

            waveSource.StartRecording();
            button2.Enabled = true;
            
        }
       
        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }

            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            waveSource.StopRecording();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;            
            string text1 = Calls.Bing(@"D:\Code\School\WPFCongnitive\WpfApp1\WpfApp1\Files\temp.wav");
            dynamic json = JsonConvert.DeserializeObject(text1);
            Console.WriteLine(json.RecognitionStatus);
            if(json.RecognitionStatus != "Success")
            {
                text1 = "Failed";
            }
            else
            {
                text1 = json.NBest[0].Display;
            }            
            listView1.Items.Add(text1);
            button4.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
        
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
  
    
    
    
}
