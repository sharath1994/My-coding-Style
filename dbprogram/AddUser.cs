using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using MySql.Data.MySqlClient;
using AForge.Video;
using AForge.Video.DirectShow;

namespace HMSB
{
    public partial class AddUser : Form
    {
        MySqlConnection mycon;
        MySqlCommand cmddb;
        byte[] imagebt;
        private Bitmap new_fram;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;


        public AddUser()
        {
            InitializeComponent();
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in videoDevices)
            {
                cb_camsource.Items.Add(VideoCaptureDevice.Name);

            }
            videoSource = new VideoCaptureDevice();
            cb_camsource.SelectedIndex = 0;
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
                    mycon.Clone();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_add_Click(object sender, EventArgs e)
        {
            if (this.OpenConnection() == true)
            {
                if (img_pic1.Image != null & img_pic1.Image == Properties.Resources.hostel)
                {
                    //FileStream fsreader = new FileStream(this.picloc, FileMode.Open, FileAccess.Read);
                    //BinaryReader br = new BinaryReader(fsreader);
                    //imagebt = br.ReadBytes((int)fsreader.Length);

                    imagebt = null;
                    MemoryStream mstream = new MemoryStream();
                    img_pic1.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Bmp);
                    imagebt = mstream.ToArray();
                }

                string query = "INSERT INTO `finaldb`.addtable (Name ,FatherName ,MobileNumber ,FamilyContact ,EmailID ,AadhaarNo ,Address ,Occupation ,CollegeName ,CollegePin ,CompanyName ,EmployeeID, FeeAllotted ,RoomAllotted ,Image) VALUES ('" + this.tb_stname.Text + "','" + this.tb_fname.Text + "','" + this.tb_st_no.Text + "','" + this.tb_f_no.Text + "','" + this.tb_email.Text + "','" + this.tb_aadhaar.Text + "','" + this.tb_address.Text + "','" + this.cb_ocp.Text + "','" + this.tb_clgname.Text + "','" + this.tb_clgpin.Text + "','" + this.tb_cmpname.Text + "','" + this.tb_emid.Text + "','" + this.tb_fee.Text + "','" + this.tb_room.Text + "',@imgscr) ;";
                MySqlCommand cmddatabase = new MySqlCommand(query, mycon);
                MySqlDataReader myreader;
                try
                {
                    cmddatabase.Parameters.Add(new MySqlParameter("@imgscr", imagebt));
                    myreader = cmddatabase.ExecuteReader();
                    MessageBox.Show("Saved");
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

        private void bt_browse_Click(object sender, EventArgs e)
        {

            img_pic1.Show();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jgp)|*.jpg|PNG Files(*.png)|*.png";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //string picloc = dlg.FileName.ToString();
                //img_pic1.ImageLocation = picloc;
                img_pic1.Image = Image.FromFile(dlg.FileName);
            }

            if (videoSource.IsRunning)
            {
                videoSource.Stop();
                videoSource.WaitForStop();
            }

        }

        private void bt_view_Click(object sender, EventArgs e)
        {
            //this.cb_camsource.Items.Add("Test Cam");
            //int ct = cb_camsource.Items.Count;
            img_pic1.Hide();
            img_pic2.Show();
            videoSource = new VideoCaptureDevice(videoDevices[cb_camsource.SelectedIndex].MonikerString);
            videoSource.NewFrame += videoSource_NewFrame;
            videoSource.Start();
        }

        void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            new_fram = (Bitmap)eventArgs.Frame.Clone();
            img_pic2.Image = new_fram;
        }

        private void bt_cap_Click(object sender, EventArgs e)
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
                videoSource.WaitForStop();
            }

            MemoryStream mstream = new MemoryStream();
            img_pic2.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Bmp);
            imagebt = mstream.ToArray();

        }
    }
}

/*
imagebt = ImageToByte(new_fram);

public static byte[] ImageToByte(Image img)
{
    ImageConverter converter = new ImageConverter();
    return (byte[])converter.ConvertTo(img, typeof(byte[]));

}
  
  private void new_Fram(object sender, NewFrameEventArgs eventArgs)
{           
   }
*/