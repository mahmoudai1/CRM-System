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
    public partial class Register : Form
    {
        MySqlConnection Connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;");
        MySqlDataAdapter Adapter;
        MySqlDataReader mdr;
        MySqlCommand Command;

        string HashText;
        string jobType;

        bool ValidUsername = true;
        bool Validfirst_name = true;
        bool Validlast_name = true;
        bool Validphone_No = true;

        main.Management management = new main.Management();
        main.User user = new main.User();

        public Register()
        {
            InitializeComponent();
        }

        void DMLQuery(string InsertQueryString)
        {
            try
            {
                Command = new MySqlCommand(InsertQueryString, Connection);
                if (Command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Successfully Regsitered", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        void SearchQuery(string SearchQueryString)
        {
            Command = new MySqlCommand(SearchQueryString, Connection);
            mdr = Command.ExecuteReader();
            if (mdr.Read())
            {
                ValidUsername = false;
            }
            else
            {
                ValidUsername = true;
            }
        }

        void RunDMLQuery()
        {
            try
            {
                Connection.Open();
                string InsertQueryString1 = "INSERT INTO crmow.workers(first_name, last_name, username, password, phone_No, email, job_type) " +
                    "VALUES('" + management.getfirst_name() + "', '" + management.getlast_name() + "', '" +
                    management.getusername() + "', '" + management.getpassword() + "', '" + management.getphoneNumber() + "', '" + management.getemail() + "', '"
                    + management.getjobType() + "')";
                DMLQuery(InsertQueryString1);

                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connection.Close();

            }

        }

        void RunSearchQuery()
        {
            try
            {
                Connection.Open();

                string SelectQueryString1 = "SELECT * FROM crmow.workers WHERE username='" + management.getusername() + "'";
               
                SearchQuery(SelectQueryString1);

                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connection.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            main Form = new main();
            Form.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            user.CreateAccount(this);
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        public void CreateAccount()
        {
            if (textBox1.Text != "" && textBox3.Text != ""
                && textBox4.Text != "" && textBox5.Text != "" && (radioButton1.Checked || radioButton2.Checked))
            {
                if (textBox5.Text.Length == 11)
                {
                    if ((textBox6.Text != "" && IsValidEmail(textBox6.Text)) || textBox6.Text == "")
                    {
                        label10.Visible = false;
                        management.setfirst_name(textBox1.Text);
                        management.setlast_name(textBox2.Text);
                        management.setusername(textBox3.Text);

                        management.setphoneNumber(textBox5.Text);
                        management.setemail(textBox6.Text);

                        HashText = EasyEncryption.MD5.ComputeMD5Hash(textBox4.Text);
                        management.setpassword(HashText);
                        if (radioButton1.Checked)
                        {
                            management.setjobType(radioButton1.Text);
                        }
                        else
                        {
                            management.setjobType(radioButton2.Text);
                        }


                        if (ValidUsername && Validfirst_name && Validlast_name && Validphone_No)
                        {
                            label14.Visible = false;
                            label13.Visible = false;
                            label12.Visible = false;
                            label11.Visible = false;
                            label10.Visible = false;
                            RunDMLQuery();
                            this.Hide();
                            Login LogForm = new Login();
                            LogForm.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Please make sure everything is correct", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }

                    else
                    {
                        if (textBox6.Text != "")
                        {
                            label10.Visible = true;
                            MessageBox.Show("Please make sure everything is correct", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Please fill all the required fields", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Phone number must be 11 digits", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please fill all the required fields", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                label10.Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // 65 - 90

            if(Regex.IsMatch(textBox1.Text, @"^[a-zA-Z]+$"))
            {
                label11.Visible = false;
                Validfirst_name = true;
                
            }
            else
            {
                if (textBox1.Text != "")
                {
                    Validfirst_name = false;
                    label11.Visible = true;
                }
            }

            if(textBox1.Text == "")
            {
                label11.Visible = false;
            }
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox5.Text, @"^[0-9]+$"))
            {
                label12.Visible = false;
                Validphone_No = true;
            }
            else
            {
                if (textBox5.Text != "")
                {
                    label12.Visible = true;
                    Validphone_No = false;
                }
            }

            if (textBox5.SelectionStart < 3)
            {
                textBox5.Text = "01";
                textBox5.SelectionStart = 2;
            }

            if (textBox5.Text.Length < 2)
            {
                textBox5.Text = "01";
                textBox5.SelectionStart = 2;
            }

           
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox2.Text, @"^[a-zA-Z]+$"))
            {
                label13.Visible = false;
                Validlast_name = true;
            }
            else
            {
                if (textBox2.Text != "")
                {
                    label13.Visible = true;
                    Validlast_name = false;
                }
            }

            if(textBox2.Text == "")
            {
                label13.Visible = false;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            management.setusername(textBox3.Text);
            RunSearchQuery();
            if(ValidUsername)
            {
                if (textBox3.Text != "")
                {
                    label14.Visible = true;
                    label14.Text = "Username is unique";
                    label14.ForeColor = Color.Green;
                    label14.Location = new Point(927, 490);
                }
                else
                {
                    label14.Visible = false;
                }
            }
            else
            {
                if (textBox3.Text != "")
                {
                    label14.Visible = true;
                    label14.Text = "Username already taken";
                    label14.ForeColor = Color.Red;
                    label14.Location = new Point(887, 490);
                }
                else
                {
                    label14.Visible = false;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           
        }
    }
}
