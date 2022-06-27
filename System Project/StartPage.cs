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
    public partial class StartPage : Form
    {
        MySqlConnection Connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;");
        MySqlDataAdapter Adapter;
        MySqlDataReader mdr;
        MySqlCommand Command;

        public static string usernameStart = Login.username;
        public static string jobTypeStart = Login.jobType;
        public static int customerID;
        main.Customer customer = new main.Customer();
        EditCustomer Form = new EditCustomer();

        public StartPage()
        {
            InitializeComponent();
            this.Text = "Welcome back, " + Login.username + " (" + Login.jobType + ")";
            if(Login.jobType == "Manager")
            {
                button5.Enabled = true;
                button4.Enabled = true;
                button3.Enabled = true;
            }
            else if(Login.jobType == "Co-Manager")
            {
                button5.Enabled = false;
                button4.Enabled = false;
                button3.Enabled = true;
            }
            else if (Login.jobType == "Employee")
            {
                button5.Enabled = false;
                button4.Enabled = false;
                button3.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login.username = "";
            this.Hide();
            main Form = new main();
            Form.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 11)
            {
                customer.setphoneNumber(textBox1.Text);
               if(ThisCustomerExist(customer.getphoneNumber()))
               {
                    label1.Visible = false;
                    this.Hide();
                    SearchCustomer Form = new SearchCustomer();
                    Form.ShowDialog();
                    this.Close();
               }
                else
                {
                    label1.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("This Phone Number is not valid", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Settings SetngForm = new Settings();
            SetngForm.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddCustomer Form = new AddCustomer();
            Form.ShowDialog();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0)
            {
                button5.Enabled = false;
            }
            else
            {
                button5.Enabled = true;
            }
            label4.Visible = false;
            label1.Visible = false;
            label6.Visible = false;
        }

        bool ThisCustomerExist(string phone)
        {
            if (RunSearchQuery(phone))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool RunSearchQuery(string phone)
        {
            try
            {
                Connection.Open();

                string SelectQueryString1 = "SELECT * FROM crmow.phone_number WHERE phone_No='" + phone + "'";
                if (SearchQuery(SelectQueryString1))
                {
                    Connection.Close();
                    return true;
                }
                else
                {
                    Connection.Close();
                    return false;
                }
                
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connection.Close();
                return false;

            }
        }

        void RunSearchQuery2()
        {
            try
            {
                Connection.Open();

                string SelectQueryString1 = "SELECT email, address FROM crmow.customers WHERE id='" + customerID + "'";
                SearchQuery2(SelectQueryString1);
                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connection.Close();

            }
        }

        void SearchQuery2(string SearchQueryString)
        {
            Command = new MySqlCommand(SearchQueryString, Connection);
            mdr = Command.ExecuteReader();
            if (mdr.Read())
            {
                Form.textBox3.Text = mdr.GetString("email");
                Form.textBox4.Text = mdr.GetString("address");
            }
        }

        bool SearchQuery(string SearchQueryString)
        {
            Command = new MySqlCommand(SearchQueryString, Connection);
            mdr = Command.ExecuteReader();
            if (mdr.Read())
            {
                customerID = int.Parse(mdr.GetString("customer_id"));
                return true;
            }
            else
            {
                return false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 11)
            {
                customer.setphoneNumber(textBox1.Text);
                if (ThisCustomerExist(customer.getphoneNumber()))
                {
                    label4.Visible = false;
                    this.Hide();
                    Form = new EditCustomer();
                    RunSearchQuery2();
                    Form.ShowDialog();
                    this.Close();
                }
                else
                {
                    label4.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("This Phone Number is not valid", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void StartPage_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 11)
            {
                customer.setphoneNumber(textBox1.Text);
                if (ThisCustomerExist(customer.getphoneNumber()))
                {
                    label6.Visible = false;
                    this.Hide();
                    SpecificSearch Form = new SpecificSearch();
                    Form.ShowDialog();
                    this.Close();
                }
                else
                {
                    label6.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("This Phone Number is not valid", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
