using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace HMSB
{
    public partial class ViewData : Form
    {
        MySqlDataAdapter mydataAdapter;
        DataTable dbtable;
        BindingSource dsource;
        MySqlConnection mycon;
        MySqlCommand cmddb;
        string picloc;
        byte[] imagebtud;

        public ViewData()
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

        private void bt_search_Click(object sender, EventArgs e)
        {
            
            if (this.OpenConnection() == true)
            {
                string query = "SELECT * FROM `finaldb`.addtable WHERE ID = '" + this.tb_refno.Text + "' and Name = '" + this.tb_name.Text + "' or Mobilenumber = '" + this.tb_mobile.Text + "';";
                MySqlCommand cmddatabase = new MySqlCommand(query, mycon);
                MySqlDataReader myreader;

                try
                {
                    myreader = cmddatabase.ExecuteReader();

                    mydataAdapter = new MySqlDataAdapter();
                    mydataAdapter.SelectCommand = cmddb;
                    dbtable = new DataTable();
                    mydataAdapter.Fill(dbtable);

                    dsource = new BindingSource();
                    dsource.DataSource = dbtable;
                    dataGridView1.DataSource = dsource;
                    
                    this.closeconnection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }
    }
}
