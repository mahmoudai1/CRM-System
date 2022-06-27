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
using System.Globalization;
using System.Text.RegularExpressions;

namespace System_Project
{
    public partial class SearchCustomer : Form
    {
        MySqlConnection Connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;");
        MySqlDataAdapter Adapter;
        MySqlDataReader mdr;
        MySqlCommand Command;

        main.Management management = new main.Management();

        public SearchCustomer()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartPage Form = new StartPage();
            Form.ShowDialog();
            this.Close();
        }

        private void SearchCustomer_Load(object sender, EventArgs e)
        {
            management.SearchCustomer(this);
        }

        public void Search_Customer()
        {
            RunSelectQueryData();
        }

        void SelectQueryData1(string SelectQueryDataString)
        {
            Adapter = new MySqlDataAdapter(SelectQueryDataString, Connection);
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "customers");
            dataGridView1.DataSource = ds.Tables["customers"];
            dataGridView1.CurrentCell.Selected = false;
        }
        void SelectQueryData2(string SelectQueryDataString)
        {
            Adapter = new MySqlDataAdapter(SelectQueryDataString, Connection);
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "phone_number");
            dataGridView2.DataSource = ds.Tables["phone_number"];
            dataGridView2.CurrentCell.Selected = false;
        }
        void SelectQueryData3(string SelectQueryDataString)
        {
            Adapter = new MySqlDataAdapter(SelectQueryDataString, Connection);
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "visits");
            dataGridView3.DataSource = ds.Tables["visits"];
            dataGridView3.CurrentCell.Selected = false;
        }
        void SelectQueryData4(string SelectQueryDataString)
        {
            Adapter = new MySqlDataAdapter(SelectQueryDataString, Connection);
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "carpets_info");
            dataGridView4.DataSource = ds.Tables["carpets_info"];
            dataGridView4.CurrentCell.Selected = false;
        }
        void SelectQueryData5(string SelectQueryDataString)
        {
            Adapter = new MySqlDataAdapter(SelectQueryDataString, Connection);
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "salesmen_name");
            dataGridView5.DataSource = ds.Tables["salesmen_name"];
            dataGridView5.CurrentCell.Selected = false;
        }
        void SelectQueryData6(string SelectQueryDataString)
        {
            Adapter = new MySqlDataAdapter(SelectQueryDataString, Connection);
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "communicate_way");
            dataGridView6.DataSource = ds.Tables["communicate_way"];
            dataGridView6.CurrentCell.Selected = false;
        }

        void RunSelectQueryData()
        {
            try
            {
                Connection.Open();
                string SelectQueryDataString1 = "SELECT * FROM crmow.customers WHERE id= '" + StartPage.customerID + "'";
                SelectQueryData1(SelectQueryDataString1);

                 SelectQueryDataString1 = "SELECT * FROM crmow.phone_number WHERE customer_id= '" + StartPage.customerID + "'";
                SelectQueryData2(SelectQueryDataString1);

                 SelectQueryDataString1 = "SELECT * FROM crmow.visits WHERE customer_id= '" + StartPage.customerID + "'";
                SelectQueryData3(SelectQueryDataString1);

                 SelectQueryDataString1 = "SELECT * FROM crmow.carpets_info WHERE customer_id= '" + StartPage.customerID + "'";
                SelectQueryData4(SelectQueryDataString1);

                 SelectQueryDataString1 = "SELECT * FROM crmow.salesmen_name WHERE customer_id= '" + StartPage.customerID + "'";
                SelectQueryData5(SelectQueryDataString1);

                 SelectQueryDataString1 = "SELECT * FROM crmow.communicate_way WHERE customer_id= '" + StartPage.customerID + "'";
                SelectQueryData6(SelectQueryDataString1);

                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(""+ e.Message);
                Connection.Close();

            }
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SearchCustomer_MouseClick(object sender, MouseEventArgs e)
        {
            dataGridView1.CurrentCell.Selected = false;
            dataGridView2.CurrentCell.Selected = false;
            dataGridView3.CurrentCell.Selected = false;
            dataGridView4.CurrentCell.Selected = false;
            dataGridView5.CurrentCell.Selected = false;
            dataGridView6.CurrentCell.Selected = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_MouseHover(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Selected = false;
            dataGridView2.CurrentCell.Selected = false;
            dataGridView3.CurrentCell.Selected = false;
            dataGridView4.CurrentCell.Selected = false;
            dataGridView5.CurrentCell.Selected = false;
            dataGridView6.CurrentCell.Selected = false;
        }
    }
}
