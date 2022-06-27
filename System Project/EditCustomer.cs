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
   

    public partial class EditCustomer : Form
    {

        MySqlConnection Connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;");
        MySqlDataAdapter Adapter;
        MySqlDataReader mdr;
        MySqlCommand Command;

        main.Customer customer = new main.Customer();
        main.Manager manager = new main.Manager();

        int totalV = 0;
        int totalP = 0;
        int totalPL = 0;
        int ctQty = 0;

        public EditCustomer()
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

        void RunUpdateQuery1()
        {
            try
            {
                Connection.Open();

                string SelectQueryString1 = "UPDATE crmow.customers SET email='" + customer.getemail() + "' , address= '" + customer.getaddress() + 
                    "' , total_visits= " + customer.getNoOfVisits() +  " , total_purcha= " + customer.getTotalPaid() + " , total_purcha_last= " + customer.getTotalPaidLast() + 
                    " WHERE id= " + StartPage.customerID +  "";

                UpdateQuery1(SelectQueryString1);
                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connection.Close();

            }
        }

        void UpdateQuery1(string UpdateQueryString)
        {
            try
            {
                Command = new MySqlCommand(UpdateQueryString, Connection);
                if (Command.ExecuteNonQuery() == 1)
                {

                }
                else
                {
                    MessageBox.Show("Something went wrong and data has not queried.", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void RunUpdateQuery2()
        {
            try
            {
                Connection.Open();

                string SelectQueryString1 = "UPDATE crmow.customers SET total_purcha= " + customer.getTotalPaid() + " , total_purcha_last= " + customer.getTotalPaidLast() +
                    " WHERE id= " + StartPage.customerID + "";

                UpdateQuery2(SelectQueryString1);
                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connection.Close();

            }
        }

        void UpdateQuery2(string UpdateQueryString)
        {
            try
            {
                Command = new MySqlCommand(UpdateQueryString, Connection);
                if (Command.ExecuteNonQuery() == 1)
                {

                }
                else
                {
                    MessageBox.Show("Something went wrong and data has not queried.", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void RunSelectQuery()
        {
            try
            {
                Connection.Open();

                string SelectQueryString1 = "SELECT total_visits, total_purcha FROM crmow.customers WHERE id='" + StartPage.customerID + "'";

                SelectQuery(SelectQueryString1);
                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connection.Close();

            }
        }

        void SelectQuery(string SelectQueryString)
        {
            Command = new MySqlCommand(SelectQueryString, Connection);
            mdr = Command.ExecuteReader();
            if (mdr.Read())
            {
                totalV = int.Parse(mdr.GetString("total_visits"));
                totalP = int.Parse(mdr.GetString("total_purcha"));
            }
        }

        void RunInsertQuery1()
        {
            try
            {
                Connection.Open();
                string InsertQueryString1 = "INSERT INTO crmow.carpets_info(customer_id, carpet_type, carpet_color, carpet_size) " +
                    "VALUES(" + StartPage.customerID + ", '" + customer.FavCarpet.gettype() + "', '" +  customer.FavCarpet.getcolor() + "', '" +
                    customer.FavCarpet.getsize() +  "')";
                InsertQuery1(InsertQueryString1);

                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connection.Close();

            }
        }

        void InsertQuery1(string InsertQueryString)
        {
            try
            {
                Command = new MySqlCommand(InsertQueryString, Connection);
                if (Command.ExecuteNonQuery() == 1)
                {
                }
                else
                {
                    MessageBox.Show("Something went wrong and carpets has not queried.", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void RunInsertQuery2()
        {
            try
            {
                Connection.Open();
                string InsertQueryString1 = "INSERT INTO crmow.visits(customer_id, visit) " +
                    "VALUES(" + StartPage.customerID + ", '" + customer.getLastVisit() + "')";
                InsertQuery2(InsertQueryString1);
                InsertQueryString1 = "INSERT INTO crmow.salesmen_name(customer_id, salesman_name) " +
                    "VALUES(" + StartPage.customerID + ", '" + customer.getsalesman_name() + "')";
                InsertQuery2(InsertQueryString1);

                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connection.Close();

            }
        }

        void InsertQuery2(string InsertQueryString)
        {
            try
            {
                Command = new MySqlCommand(InsertQueryString, Connection);
                if (Command.ExecuteNonQuery() == 1)
                {
                }
                else
                {
                    MessageBox.Show("Something went wrong and carpets has not queried.", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            manager.EditCustomer(this, 1);
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            manager.EditCustomer(this, 2);
        }

        public void Edit_Customer(int i)
        {
            if(i == 1)
            {
                if (textBox3.Text != "" && textBox4.Text != "" && textBox13.Text != "" && textBox8.Text != "" && textBox13.Text.Length == 10)
                {
                    customer.setemail(textBox3.Text);
                    customer.setaddress(textBox4.Text);
                    customer.setLastVisit(textBox13.Text);
                    customer.setsalesman_name(textBox8.Text);
                    customer.setNoOfVisits(totalV + 1);
                    customer.setTotalPaid(totalP);
                    customer.setTotalPaidLast(0);
                    RunUpdateQuery1();
                    RunInsertQuery2();
                    label18.Text = "SUCCEED";
                    label18.ForeColor = Color.Green;
                    button2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    textBox13.Enabled = false;
                    textBox8.Enabled = false;
                }
                else
                {
                    MessageBox.Show("There is something wrong, please re-check your inputs", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    label18.Text = "FAILED";
                    label18.ForeColor = Color.Red;
                }
            }
           
            else if(i == 2)
            { 
                if (comboBox1.Text != "" && comboBox2.Text != "" && textBox12.Text != "" && textBox7.Text != "" && textBox7.Text != "0")
                {
                    customer.FavCarpet.settype(comboBox1.Text);
                    customer.FavCarpet.setcolor(comboBox2.Text);
                    customer.FavCarpet.setsize(textBox12.Text);
                    totalP += int.Parse(textBox7.Text);
                    customer.setTotalPaid(totalP);
                    totalPL += int.Parse(textBox7.Text);
                    customer.setTotalPaidLast(totalPL);
                    RunInsertQuery1();
                    RunUpdateQuery2();
                    label1.Text = "SUCCEED";
                    label1.ForeColor = Color.Green;
                    label4.Text = "" + ++ctQty;
                }
                else
                {
                    MessageBox.Show("There is something wrong, please re-check your inputs", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    label1.Text = "FAILED";
                    label1.ForeColor = Color.Red;
                }

               
            }
          

        }

        private void EditCustomer_Load(object sender, EventArgs e)
        {
            RunSelectQuery();
        }

       
    }
}
