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
    public partial class Settings : Form
    {
        MySqlConnection Connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;");
        MySqlDataAdapter Adapter;
        MySqlDataReader mdr;
        MySqlCommand Command;

        main.Management management = new main.Management();
        main.Management user = new main.Management();

        bool ValidUpdate = true;
        bool Validphone_No = true;

        public Settings()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartPage SPForm = new StartPage();
            SPForm.ShowDialog();
            this.Close();
        }

        void SearchQuery(string SearchQueryString)
        {
            Command = new MySqlCommand(SearchQueryString, Connection);
            mdr = Command.ExecuteReader();
            if (mdr.Read())
            {
                ValidUpdate = true;
            }
            else
            {
                ValidUpdate = false;
            }
        }

        void UpdateQuery(string UpdateQueryString)
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

        void RunSearchQuery()
        {
            try
            {
                Connection.Open();
                string SelectQueryString1 = "SELECT * FROM crmow.workers WHERE username='" + management.getusername() + "' AND password='" + management.getpassword() + "'";

                SearchQuery(SelectQueryString1);
                Connection.Close();

              
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connection.Close();

            }
        }

        void RunUpdateQuery1()
        {
            try
            {
                Connection.Open();

                string SelectQueryString1 = "UPDATE crmow.workers SET password='" + management.getpassword() + "' WHERE username= '" + management.getusername() + "'";

                UpdateQuery(SelectQueryString1);
                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connection.Close();

            }
        }

        void RunUpdateQuery2()
        {
            try
            {
                Connection.Open();

                string SelectQueryString1 = "UPDATE crmow.workers SET email='" + management.getemail() + "' WHERE username= '" + management.getusername() + "'";

                UpdateQuery(SelectQueryString1);
                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connection.Close();

            }
        }

        void RunUpdateQuery3()
        {
            try
            {
                Connection.Open();

                string SelectQueryString1 = "UPDATE crmow.workers SET phone_No='" + management.getphoneNumber() + "' WHERE username= '" + management.getusername() + "'";

                UpdateQuery(SelectQueryString1);
                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connection.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox2.Text != "")
            {
                string HashText = EasyEncryption.MD5.ComputeMD5Hash(textBox1.Text);
                string HashText2 = EasyEncryption.MD5.ComputeMD5Hash(textBox2.Text);
                management.setusername(Login.username);
                management.setpassword(HashText);
                RunSearchQuery();
                
                if(ValidUpdate)
                {
                    management.setpassword(HashText2);
                    RunUpdateQuery1();

                    label8.Visible = false;
                    label9.Visible = false;
                    label7.Visible = true;
                    label7.Text = "Changed";
                    label7.ForeColor = Color.Green;
                    label7.Location = new Point(997, 559);
                    textBox1.Text = "";
                }
                else
                {
                    label8.Visible = false;
                    label9.Visible = false;
                    label7.Visible = true;
                    label7.Text = "Error";
                    label7.ForeColor = Color.Red;
                    label7.Location = new Point(1021, 559);
                }
            }
            else
            {
                MessageBox.Show("Please fill the required fields", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                label7.Text = "";
                label8.Text = "";
                label9.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartPage Form = new StartPage();
            Form.ShowDialog();
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox3.Text != "")
            {
                string HashText = EasyEncryption.MD5.ComputeMD5Hash(textBox1.Text);
                management.setusername(Login.username);
                management.setpassword(HashText);
                RunSearchQuery();

                if (ValidUpdate)
                {
                    label10.Visible = false;
                    if (textBox3.Text != "" && IsValidEmail(textBox3.Text))
                    {
                        management.setemail(textBox3.Text);
                        RunUpdateQuery2();

                        label8.Visible = true;
                        label9.Visible = false;
                        label7.Visible = false;
                        label8.Text = "Changed";
                        label8.ForeColor = Color.Green;
                        label8.Location = new Point(997, 703);
                        textBox1.Text = "";
                        label10.Visible = false;
                    }
                    else
                    {
                        if (textBox3.Text != "")
                        {
                            label10.Visible = true;
                            MessageBox.Show("Please make sure everything is correct", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Please fill the required fields", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    label8.Visible = true;
                    label9.Visible = false;
                    label7.Visible = false;
                    label8.Text = "Error";
                    label8.ForeColor = Color.Red;
                    label8.Location = new Point(1021, 703);
                }
            }
            else
            {
                MessageBox.Show("Please fill the required fields", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                label7.Text = "";
                label8.Text = "";
                label9.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox4.Text != "")
            {
                string HashText = EasyEncryption.MD5.ComputeMD5Hash(textBox1.Text);

                management.setusername(Login.username);
                management.setpassword(HashText);
                RunSearchQuery();
                if (textBox4.Text.Length == 11)
                {
                    if (ValidUpdate)
                    {
                        if (Validphone_No)
                        {
                            management.setphoneNumber(textBox4.Text);
                            RunUpdateQuery3();
                            label8.Visible = false;
                            label9.Visible = true;
                            label7.Visible = false;
                            label9.Text = "Changed";
                            label9.ForeColor = Color.Green;
                            label9.Location = new Point(997, 852);
                            textBox1.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Please make sure everything is correct", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        label8.Visible = false;
                        label9.Visible = true;
                        label7.Visible = false;
                        label9.Text = "Error";
                        label9.ForeColor = Color.Red;
                        label9.Location = new Point(1021, 852);
                    }
                }
                else
                {
                    MessageBox.Show("Phone number must be 11 digits", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please fill the required fields", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                label7.Text = "";
                label8.Text = "";
                label9.Text = "";
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox4.Text, @"^[0-9]+$"))
            {
                label11.Visible = false;
                Validphone_No = true;
            }
            else
            {
                if (textBox4.Text != "")
                {
                    Validphone_No = false;
                    label11.Visible = true;
                }
            }

            if (textBox4.SelectionStart < 3)
            {
                textBox4.Text = "01";
                textBox4.SelectionStart = 2;
            }

            if (textBox4.Text.Length < 2)
            {
                textBox4.Text = "01";
                textBox4.SelectionStart = 2;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
