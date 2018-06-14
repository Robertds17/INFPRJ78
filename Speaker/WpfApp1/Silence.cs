using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WpfApp1
{
    public partial class Silence : Form
    {
        public Silence()
        {
            InitializeComponent();
        }
        List<string> files = new List<string>();
        APICalls.APICalls Calls = new APICalls.APICalls();

        private void button1_Click(object sender, EventArgs e)
        {
            files = new List<string>();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Wav files (*.WAV)|*.WAV";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                
                foreach(string filename in ofd.FileNames)
                {
                    files.Add(filename);
                    
                }

            }
        }

        private void DetectSilence_Clicked(object sender, EventArgs e)
        {
            detectsilence.Enabled = false;
            if (files.Count == 0)
            {
                MessageBox.Show("please select a file");
            }
            else
            {
                using (AudioFileReader reader = new AudioFileReader(files.First()))
                {
                    TimeSpan duration = reader.GetSilenceDuration(AudioReader.SilenceLocation.Start);
                    Console.WriteLine(duration.TotalMilliseconds);
                }
            }
            detectsilence.Enabled = true;
        
        }

        private void Transcribe_Clicked(object sender, EventArgs e)
        {
            transcribe.Enabled = false;
            string output = "";
            string ids = "";
            string jsonresponse = Calls.GetAllProfiles();
            JArray profiles = JArray.Parse(jsonresponse);
            dynamic jsonids = JsonConvert.DeserializeObject(jsonresponse);


            
            if (jsonids[0].identificationProfileId == null)
            {
                MessageBox.Show("error");
            }
            else
            {


                foreach (var profile in profiles.Children())
                {

                    //profilelist.Items.Add(profile.First().First().ToString()); //+ " Status:  " + profile.First().Next.Next.Next.Next.Next.Next.First().ToString());
                    ids += profile.First().First().ToString();
                    ids += ",";
                    Console.WriteLine(ids);
                }
                ids = ids.Remove(ids.Length - 1);
                Console.WriteLine(ids);

            }
            foreach (string file in files)
            {
                string STTresponse = Calls.Bing(file);
                dynamic json = JsonConvert.DeserializeObject(STTresponse);

                if (json.RecognitionStatus != "Success")
                {
                    output += "Failed";
                }
                else
                {
                    

                    string IDresponse = Calls.ReturnSpeaker(ids,file);
                    output += json.NBest[0].Display;
                    output += " ID:  ";
                    output += IDresponse;
                }
                output += "   ||   ";
            }
            MessageBox.Show(output);
            transcribe.Enabled = true;
        }
    }
}
