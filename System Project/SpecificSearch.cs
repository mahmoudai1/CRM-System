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

namespace System_Project
{
    public partial class SpecificSearch : Form
    {
        MySqlConnection Connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;");
        MySqlDataAdapter Adapter;
        MySqlDataReader mdr;
        MySqlCommand Command;

        string select = "";
        string selectFrom = "";
        string where = "";
        string whereFrom = "";
        string equal = "";

        public SpecificSearch()
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

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex < 9)
            {
                selectFrom = "customers";
            }
            else if (comboBox1.SelectedIndex == 0)
            {
                selectFrom = "*";
            }
            else if(comboBox1.SelectedIndex == 10)
            {
                selectFrom = "visits";
            }
            else if (comboBox1.SelectedIndex == 12)
            {
                selectFrom = "salesmen_name";
            }
            else if (comboBox1.SelectedIndex == 14)
            {
                selectFrom = "phone_number";
            }
            else if (comboBox1.SelectedIndex == 18)
            {
                selectFrom = "communicate_way";
            }
            else
            {
                selectFrom = "carpets_info";
            }

            select = comboBox1.Text;

            if (comboBox2.SelectedIndex < 7)
            {
                whereFrom = "customers";
            }
            else if (comboBox2.SelectedIndex == 8)
            {
                whereFrom = "visits";
            }
            else if (comboBox2.SelectedIndex == 10)
            {
                whereFrom = "salesmen_name";
            }
            else if (comboBox2.SelectedIndex == 12)
            {
                whereFrom = "phone_number";
            }
            else if (comboBox2.SelectedIndex == 14)
            {
                whereFrom = "communicate_way";
            }
            else
            {
                whereFrom = "carpets_info";
            }

            where = comboBox2.Text;
            equal = textBox1.Text;
            //RunSelectQueryData();
        }

        void SelectQueryData(string SelectQueryDataString)
        {
            try
            {
                Adapter = new MySqlDataAdapter(SelectQueryDataString, Connection);
                DataSet ds = new DataSet();
                Adapter.Fill(ds, "visits");
                dataGridView1.DataSource = ds.Tables["visits"];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
           
        }

        void RunSelectQueryData()
        {
            try
            {
                Connection.Open();
                // string SelectQueryDataString1 = "SELECT " + select + " FROM crmow." + selectFrom + "INNER JOIN " + whereFrom + " on " + 
                // selectFrom + "." + select + "=" + whereFrom + "." + select
                // +  " WHERE " + whereFrom + "." + where + "= '" + equal + "'";

                string SelectQueryDataString1 = "SELECT visit FROM crmow.visits " +
                                                "INNER JOIN crmow.customers ON visits.visit = customers.first_name " +
                                                "WHERE customers.first_name='mahmoud'";
                SelectQueryData(SelectQueryDataString1);

                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message);
                Connection.Close();

            }

        }
    }
}
