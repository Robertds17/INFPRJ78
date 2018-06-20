using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    public partial class CreateUserID : Form
    {
        public CreateUserID()
        {
            InitializeComponent();
            
        }

        APICalls.APICalls Calls = new APICalls.APICalls();
        string file = "";
        DatabaseConnection DB = new DatabaseConnection();


        private void CreateProfile_Clicked(object sender, EventArgs e)
        {
            createnewprofile.Enabled = false;
            string id;
            if (namebox.Text == "" || namebox.Text == "'Name here'")
            {
                MessageBox.Show("Please enter a name");
            }
            else
            {
                string name = namebox.Text;
                string jsonresponse = Calls.CreateProfile();
                dynamic json = JsonConvert.DeserializeObject(jsonresponse);
                if (json.identificationProfileId == null)
                {
                    id = "error";
                }
                else
                {
                    id = json.identificationProfileId;
                    DB.InsertID(id, name);

                }

                dataGridView1.Rows.Add(id, "ToDo",name);
            }
            createnewprofile.Enabled = true; 
        }

        private void ProfileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        

        private void GetAllProfiles_Clicked(object sender, EventArgs e)
        {
            getallprofiles.Enabled = false;
            string jsonresponse = Calls.GetAllProfiles();
            JArray profiles = JArray.Parse(jsonresponse);
            dynamic json = JsonConvert.DeserializeObject(jsonresponse);

            
            dataGridView1.Rows.Clear();
            if (json[0].identificationProfileId == null)
            {
                MessageBox.Show("error");
            }
            else
            {
                
                
                foreach(var profile in profiles.Children())
                {
                    string name = DB.GetName(profile.First().First().ToString());                    
                    //profilelist.Items.Add(profile.First().First().ToString()); //+ " Status:  " + profile.First().Next.Next.Next.Next.Next.Next.First().ToString());
                    dataGridView1.Rows.Add(profile.First().First().ToString(), profile.First().Next.Next.Next.Next.Next.Next.First().ToString(),name);
                    
                }

            }

            getallprofiles.Enabled = true;

        }

        private void EnrollProfile_Clicked(object sender, EventArgs e)
        {
            enrollprofile.Enabled = false;
            var selecteditem = dataGridView1.SelectedCells[0];
            if (selecteditem == null)
            {
                MessageBox.Show("Please select a profile");
            }
            else
            {
                Console.WriteLine(selecteditem.Value.ToString());
                if (file == "")
                {
                    MessageBox.Show("Please select a file!");
                }
                else
                {
                    Calls.EnrollProfile(selecteditem.Value.ToString(), file);
                }
            }
            enrollprofile.Enabled = true;
        }

        private void SelectFile_Clicked(object sender, EventArgs e)
        {
            file = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Wav files (*.WAV)|*.WAV";
            if (ofd.ShowDialog() == DialogResult.OK)
            {                
                file += ofd.FileName;                
                
            }
        }

        
        private void DeleteProfile_Clicked(object sender, EventArgs e)
        {
            deleteprofile.Enabled = false;
            var selecteditem = dataGridView1.SelectedCells[0];
            if (selecteditem == null)
            {
                MessageBox.Show("Please select a profile");
            }
            else
            {
                Calls.DeleteProfile(selecteditem.Value.ToString());
                
            }
            GetAllProfiles_Clicked(sender, e);
            deleteprofile.Enabled = true;
        }

        private void profilelist_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void IdentifySpeaker_Clicked(object sender, EventArgs e)
        {
            identifyspeaker.Enabled = false;
            var selecteditem = dataGridView1.SelectedCells;
            if (selecteditem.Count == 0)
            {
                MessageBox.Show("Please select a profile");
            }
            else if(file == "")
            {
                MessageBox.Show("Please select a file!");
            }
            else
            {
                string ids = "";
                for(int i = 0;i < selecteditem.Count; i++)
                {
                    ids += selecteditem[i].Value.ToString() + ",";
                }
                ids = ids.Remove(ids.Length - 1);

                Console.WriteLine(ids);
                string jsonrepsonse = Calls.FindSpeakers(ids, file);
                Console.WriteLine(jsonrepsonse[0]);
                if(jsonrepsonse[0].ToString() != "h")
                {
                    MessageBox.Show("error while processing request!");
                }
                else
                {
                    System.Threading.Thread.Sleep(5000);
                    string jsonresponsestatus = Calls.GetOperationStatus(jsonrepsonse);
                    
                    dynamic json = JsonConvert.DeserializeObject(jsonresponsestatus);
                    
                    while (json.status == "running" || json.status == "notstarted")
                    {
                        
                        
                        jsonresponsestatus = Calls.GetOperationStatus(jsonrepsonse);
                        json = JsonConvert.DeserializeObject(jsonresponsestatus);
                        
                    }
                    if (json.status == "failed")
                    {
                        MessageBox.Show("error");
                    }
                    else
                    {
                        json = JObject.Parse(jsonresponsestatus);

                        if (json.processingResult.identifiedProfileId == "00000000-0000-0000-0000-000000000000")
                        {
                            MessageBox.Show("Could not recognize speaker");
                        }
                        else
                        {
                            string speaker = json.processingResult.identifiedProfileId;
                            
                            MessageBox.Show(speaker);
                        }
                        
                        //"00000000-0000-0000-0000-000000000000";
                        //json.processingResult.identifiedProfileId
                    }



                }
            }
            
            identifyspeaker.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CreateUserID_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
