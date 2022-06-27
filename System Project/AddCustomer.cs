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
    public partial class AddCustomer : Form
    {

        MySqlConnection Connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;");
        MySqlDataAdapter Adapter;
        MySqlDataReader mdr;
        MySqlCommand Command;

        main.Manager manager = new main.Manager();
        main.Customer customer = new main.Customer();

        bool EmailIsUnique = false;
        bool Validphone_No1 = false;
        bool Validphone_No2 = false;
        bool Validphone_No3 = false;
        bool ValidDate = false;

        int customerID;
        public AddCustomer()
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

        void RunDMLQuery()
        {
            try
            {
                Connection.Open();
                string InsertQueryString1 = "INSERT INTO crmow.customers(first_name, last_name, gender, email, address, total_visits, total_purcha, total_purcha_last) " +
                    "VALUES('" + customer.getfirst_name() + "', '" + customer.getlast_name() + "', '" +
                    customer.getgender() + "', '" + customer.getemail() + "', '" + customer.getaddress() + "', " + 1 + ", '"
                    + customer.getTotalPaid() + "', '" + customer.getTotalPaidLast() + "')";
                DMLQuery1(InsertQueryString1);

              

                if (EmailIsUnique)
                {
                    if (textBox9.Text != "01" && Validphone_No1)
                    {
                        customer.setphoneNumber(textBox9.Text);
                        InsertQueryString1 = "INSERT INTO crmow.phone_number(customer_id, phone_No) " +
                           "VALUES( (SELECT id FROM crmow.customers WHERE email = '" + customer.getemail() + "'), '" + customer.getphoneNumber() + "')";
                        DMLQuery2(InsertQueryString1);
                    }

                    if (textBox10.Text != "01" && Validphone_No2)
                    {
                        customer.setphoneNumber(textBox10.Text);
                        InsertQueryString1 = "INSERT INTO crmow.phone_number(customer_id, phone_No) " +
                           "VALUES( (SELECT id FROM crmow.customers WHERE email = '" + customer.getemail() + "'), '" + customer.getphoneNumber() + "')";
                        DMLQuery2(InsertQueryString1);
                    }

                    if (textBox11.Text != "01" && Validphone_No3)
                    {
                        customer.setphoneNumber(textBox11.Text);
                        InsertQueryString1 = "INSERT INTO crmow.phone_number(customer_id, phone_No) " +
                           "VALUES( (SELECT id FROM crmow.customers WHERE email = '" + customer.getemail() + "'), '" + customer.getphoneNumber() + "')";
                        DMLQuery2(InsertQueryString1);
                    }

                    InsertQueryString1 = "INSERT INTO crmow.carpets_info(customer_id, carpet_type, carpet_color, carpet_size) " +
                        "VALUES((SELECT id FROM crmow.customers WHERE email ='" + customer.getemail() + "'), '" + customer.FavCarpet.gettype() + "', '" +
                         customer.FavCarpet.getcolor() + "', '" + customer.FavCarpet.getsize() + "')";
                         DMLQuery1(InsertQueryString1);

                    int j = 0;
                    foreach (var i in this.Controls)
                    {
                        if (i is CheckBox)
                        {
                            if (((CheckBox)i).Checked)
                            {
                                customer.setprefer_communicate_way((((CheckBox)i).Text),j);
                                InsertQueryString1 = "INSERT INTO crmow.communicate_way(customer_id, way_type) " +
                                "VALUES((SELECT id FROM crmow.customers WHERE email ='" + customer.getemail() + "'), '" + customer.getprefer_communicate_way(j) + "')";
                                DMLQuery1(InsertQueryString1);
                            }
                            j++;
                        }
                    }

                    InsertQueryString1 = "INSERT INTO crmow.salesmen_name(customer_id, salesman_name) " +
                                "VALUES((SELECT id FROM crmow.customers WHERE email ='" + customer.getemail() + "'), '" + customer.getsalesman_name() + "')";
                    DMLQuery1(InsertQueryString1);

                    InsertQueryString1 = "INSERT INTO crmow.visits(customer_id, visit) " +
                                "VALUES((SELECT id FROM crmow.customers WHERE email ='" + customer.getemail() + "'), '" + customer.getLastVisit() + "')";
                    DMLQuery1(InsertQueryString1);

                }

               

                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connection.Close();

            }
            
        }

        void DMLQuery1(string InsertQueryString)
        {
            try
            {
                Command = new MySqlCommand(InsertQueryString, Connection);
                if (Command.ExecuteNonQuery() == 1)
                {
                    label18.Text = "DATA INSERTED";
                    label18.ForeColor = Color.Green;
                    EmailIsUnique = true;
                }
                else
                {
                    label18.Text = "DATA NOT INSERTED";
                    label18.ForeColor = Color.Red;
                    EmailIsUnique = false;
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EmailIsUnique = false;
            }

        }

        void DMLQuery2(string InsertQueryString)
        {
            try
            {
                Command = new MySqlCommand(InsertQueryString, Connection);
                if (Command.ExecuteNonQuery() == 1)
                {
                    label18.Text = "DATA INSERTED";
                    label18.ForeColor = Color.Green;
                }
                else
                {
                    label18.Text = "DATA NOT INSERTED";
                    label18.ForeColor = Color.Red;
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void AddCustomer_Load(object sender, EventArgs e)
        {
            label18.Text = "STATUS";
            label18.ForeColor = Color.Black;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            manager.AddnewCustomer(this);
            
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox9.Text, @"^[0-9]+$") && textBox9.Text.Length == 11 && textBox9.Text != textBox10.Text && textBox9.Text != textBox11.Text)
            {
                Validphone_No1 = true;
            }
            else
            {
                if (textBox9.Text != "01" || textBox9.Text == textBox10.Text || textBox9.Text != textBox11.Text)
                {
                    Validphone_No1 = false;
                }
            }

            if (textBox9.SelectionStart < 3)
            {
                textBox9.Text = "01";
                textBox9.SelectionStart = 2;
            }

            if (textBox9.Text.Length < 2)
            {
                textBox9.Text = "01";
                textBox9.SelectionStart = 2;
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox10.Text, @"^[0-9]+$") && textBox10.Text.Length == 11 && textBox10.Text != textBox9.Text && textBox10.Text != textBox11.Text)
            {
                Validphone_No2 = true;
            }
            else
            {
                if (textBox10.Text != "01" || textBox10.Text == textBox9.Text || textBox10.Text != textBox11.Text)
                {
                    Validphone_No2 = false;
                }
            }

            if (textBox10.SelectionStart < 3)
            {
                textBox10.Text = "01";
                textBox10.SelectionStart = 2;
            }

            if (textBox10.Text.Length < 2)
            {
                textBox10.Text = "01";
                textBox10.SelectionStart = 2;
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox11.Text, @"^[0-9]+$") && textBox11.Text.Length == 11 && textBox11.Text != textBox9.Text && textBox11.Text != textBox10.Text)
            {
                Validphone_No3 = true;
            }
            else
            {
                if (textBox11.Text != "01" || textBox11.Text == textBox9.Text || textBox11.Text != textBox10.Text)
                {
                    Validphone_No3 = false;
                }
            }

            if (textBox11.SelectionStart < 3)
            {
                textBox11.Text = "01";
                textBox11.SelectionStart = 2;
            }

            if (textBox11.Text.Length < 2)
            {
                textBox11.Text = "01";
                textBox11.SelectionStart = 2;
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if(textBox13.Text.Length == 10)
            {
                ValidDate = true;
            }
            else
            {
                ValidDate = false;
            }
        }

        public void AddnewCustomer()
        {
            if (textBox1.Text != "" && textBox2.Text != "" && (radioButton1.Checked || radioButton2.Checked) && textBox3.Text != ""
                && textBox4.Text != ""  && textBox7.Text != "" && textBox8.Text != ""
                && (textBox9.Text != "" || textBox10.Text != "" || textBox11.Text != "") && comboBox1.Text != "" && comboBox2.Text != ""
                && textBox12.Text != "" && textBox13.Text != "" && (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked)
                )
            {
                if (IsValidEmail(textBox3.Text))
                {
                    if (Validphone_No1 || Validphone_No2 || Validphone_No3)
                    {
                        if (ValidDate)
                        {
                            if (radioButton1.Checked)
                            {
                                customer.setgender(radioButton1.Text);
                            }
                            else
                            {
                                customer.setgender(radioButton2.Text);
                            }
                            customer.setfirst_name(textBox1.Text);
                            customer.setlast_name(textBox2.Text);
                            customer.setemail(textBox3.Text);
                            customer.setaddress(textBox4.Text);
                            customer.setLastVisit(textBox13.Text);
                            customer.setTotalPaid(int.Parse(textBox7.Text));
                            customer.setTotalPaidLast(int.Parse(textBox7.Text));
                            customer.setsalesman_name(textBox8.Text);
                            customer.FavCarpet.settype(comboBox1.Text);
                            customer.FavCarpet.setcolor(comboBox2.Text);
                            customer.FavCarpet.setsize(textBox12.Text);

                            RunDMLQuery();
                            if (EmailIsUnique)
                            {
                                this.Hide();
                                StartPage FormSPv2 = new StartPage();
                                FormSPv2.ShowDialog();
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid date", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Phone Number is not valid", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Email is not valid", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("An error occured", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        // SELECT * FROM crmow.customers WHERE id = (SELECT id from crm.phone_number WHERE phone_No = management.getPhoneNumber());
        // SELECT phone_No FROM crmow.phone_number WHERE customer_id = myID;
        // myID will be detected between (customers) and the (others tables) execution [ at ADD, INSERT]
        // [ at EDIT, UPDATE ] myID will be detected directly when the phone number enters (customer_id from phone_number table)
    }
}
