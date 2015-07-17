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
using System.IO;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Threading;

namespace HMSB
{
    public partial class EditData : Form
    {
        MySqlConnection mycon;
        MySqlCommand cmddb;
        byte[] imagebtud;
        MySqlCommand cmddatabase;
        private Bitmap new_fram;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;

        public EditData()
        {
            InitializeComponent();
        }

        private bool OpenConnection()
        {
            string myconnection = "server=localhost;port=3306;username=root;password=root;allowuservariables=True";
            mycon = new MySqlConnection(myconnection);
            cmddb = new MySqlCommand("select * from  `finaldb`.addtable;", mycon);
            try
            {
                mycon.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void closeconnection()
        {
            try
            {
                if (this.OpenConnection() == true)
                {
                    mycon.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditData_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in videoDevices)
            {
                cb_camsource.Items.Add(VideoCaptureDevice.Name);

            }
            videoSource = new VideoCaptureDevice();
            cb_camsource.SelectedIndex = 0;
        }

        private void bt_edit_Click(object sender, EventArgs e)
        {
            if (this.OpenConnection() == true)
            {
                if (pb_img1.Image != null)
                {
                    imagebtud = null;
                    //FileStream fsreaderud = new FileStream(this.picloc, FileMode.Open, FileAccess.Read);
                    //BinaryReader brud = new BinaryReader(fsreaderud);
                    //imagebtud = brud.ReadBytes((int)fsreaderud.Length);
                    MemoryStream mstream = new MemoryStream();
                    pb_img1.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Bmp);
                    imagebtud = mstream.ToArray();
                }

                MySqlDataReader myreader;
                try
                {
                    //cmddatabase.Parameters.Add(new MySqlParameter("@imgud", imagebtud));
                    string query = "UPDATE  `finaldb`.addtable SET  Name = '" + this.tb_stname.Text + "',FatherName = '" + this.tb_fname.Text + "',MobileNumber = '" + this.tb_st_no.Text + "',FamilyContact = '" + this.tb_f_no.Text + "',EmailID = '" + this.tb_email.Text + "',AadhaarNo = '" + this.tb_aadhaar.Text + "',Address = '" + this.tb_address.Text + "',Occupation = '" + this.cb_ocp.Text + "',CollegeName = '" + this.tb_clgname.Text + "',CollegePin = '" + this.tb_clgpin.Text + "',CompanyName = '" + this.tb_cmpname.Text + "',EmployeeID = '" + this.tb_emid.Text + "',FeeAllotted = '" + this.tb_fee.Text + "',RoomAllotted = '" + this.tb_room.Text + "' ,Image = @imgud  where ID =" + this.tb_ref_no.Text + ";";
                    cmddatabase = new MySqlCommand(query, mycon);
                    cmddatabase.Parameters.Add(new MySqlParameter("@imgud", imagebtud));
                    myreader = cmddatabase.ExecuteReader();
                    MessageBox.Show("Update");
                    while (myreader.Read())
                    {
                    }
                    this.closeconnection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bt_search_Click(object sender, EventArgs e)
        {
            if (this.OpenConnection() == true)
            {
               //pb_img1.Image = null;
                pb_img1.Show();
                pb_img2.Hide();
                string query = "SELECT * FROM `finaldb`.addtable WHERE ID = '" + this.tb_refno_serach.Text + "';";
                MySqlCommand cmddatabase = new MySqlCommand(query, mycon);
                MySqlDataReader myreader;
                try
                {
                    myreader = cmddatabase.ExecuteReader();

                    while (myreader.Read())
                    {
                        tb_ref_no.Text = myreader.GetString("ID");
                        tb_stname.Text = myreader.GetString("Name");
                        tb_fname.Text = myreader.GetString("FatherName");
                        tb_st_no.Text = myreader.GetString("MobileNumber");
                        tb_f_no.Text = myreader.GetString("FamilyContact");
                        tb_email.Text = myreader.GetString("EmailID");
                        tb_aadhaar.Text = myreader.GetString("AadhaarNo");
                        tb_address.Text = myreader.GetString("Address");
                        cb_ocp.Text = myreader.GetString("Occupation");
                        tb_clgname.Text = myreader.GetString("CollegeName");
                        tb_clgpin.Text = myreader.GetString("CollegePin");
                        tb_cmpname.Text = myreader.GetString("CompanyName");
                        tb_emid.Text = myreader.GetString("EmployeeID");
                        tb_fee.Text = myreader.GetString("FeeAllotted");
                        tb_room.Text = myreader.GetString("RoomAllotted");

                        byte[] imgg = (byte[])(myreader["Image"]);
                        if (imgg != null)
                        {
                            MemoryStream picstrm = new MemoryStream(imgg);
                            pb_img1.Image = System.Drawing.Image.FromStream(picstrm);
                        }
                        else
                        {
                            pb_img1.Image = null;
                        }
                    }
                    this.closeconnection();
                }
                catch (Exception ex)
                {

                    if (ex.Message == "Unable to cast object of type 'System.DBNull' to type 'System.Byte[]'.")
                    {
                        //pb_img1.Image = Properties.Resources.hostel;
                        pb_img1.Image = null;
                    }
                    else if (ex.Message == "Data is Null. This method or property cannot be called on Null values.")
                    { }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void img_picupdate_Click(object sender, EventArgs e)
        {
            pb_img1.Show();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jgp)|*.jpg|PNG Files(*.png)|*.png";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // string picloc = dlg.FileName.ToString();
                pb_img1.Image = Image.FromFile(dlg.FileName);
            }
            //if (pb_img1.Image != null)
            //{
            //    pb_img1.Image.Dispose();
            //}
            //else
            //{
            //    if (dlg.ShowDialog() == DialogResult.OK)
            //    {
            //        picloc = dlg.FileName.ToString();
            //        pb_img1.ImageLocation = picloc;
            //    }
        }

        private void bt_preveiw_Click(object sender, EventArgs e)
        {
            pb_img1.Hide();
            pb_img2.Show();
            videoSource = new VideoCaptureDevice(videoDevices[cb_camsource.SelectedIndex].MonikerString);
            videoSource.NewFrame += videoSource_NewFrame;
            videoSource.Start();
        }

        void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            new_fram = (Bitmap)eventArgs.Frame.Clone();
            pb_img2.Image = new_fram;
        }

        //private void new_Fram(object sender, NewFrameEventArgs eventArgs)
        //{

        //    new_fram = (Bitmap)eventArgs.Frame.Clone();
        //    pb_img2.Image = new_fram;
        //}

        private void bt_capture_Click(object sender, EventArgs e)
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
                videoSource.WaitForStop();
            }

            MemoryStream mstream = new MemoryStream();
            pb_img2.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Bmp);
            imagebtud = mstream.ToArray();
        }
    }
}
