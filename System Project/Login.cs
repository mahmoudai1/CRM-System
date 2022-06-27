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
    public partial class Login : Form
    {
        MySqlConnection Connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;");
        MySqlDataAdapter Adapter;
        MySqlDataReader mdr;
        MySqlCommand Command;

        public static string username;
        public static string jobType;

        string HashText;
        bool ValidLogin = false;

        main.Management management = new main.Management();
        main.Management user = new main.Management();

        public Login()
        {
            InitializeComponent();
        }

        void SearchQuery(string SearchQueryString)
        {
            Command = new MySqlCommand(SearchQueryString, Connection);
            mdr = Command.ExecuteReader();
            if (mdr.Read())
            {
                ValidLogin = true;
            }
            else
            {
                ValidLogin = false;
            }
        }

        void SearchQueryJobType(string SearchQueryString)
        {
            Command = new MySqlCommand(SearchQueryString, Connection);
            mdr = Command.ExecuteReader();
            if (mdr.Read())
            {
                jobType =  mdr.GetString("job_type");
            }
            else
            {
                jobType = "";
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

                if (ValidLogin)
                {
                    Connection.Open();
                    string SelectQueryString2 = "SELECT job_type FROM crmow.workers WHERE username='" + management.getusername() + "' AND password='" + management.getpassword() + "'";

                    SearchQueryJobType(SelectQueryString2);
                }
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
            user.checkUserInDatabase(this);
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        public void checkUserInDatabase()
        {
            if (this.textBox1.Text != "" && this.textBox2.Text != "")
            {
                string passwd = textBox2.Text;
                HashText = EasyEncryption.MD5.ComputeMD5Hash(passwd);
                management.setusername(textBox1.Text);
                management.setpassword(HashText);
                RunSearchQuery();
                if (ValidLogin)
                {
                   username = textBox1.Text;
                    this.Hide();
                    StartPage StartForm = new StartPage();
                    StartForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please fill all the required fields", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
