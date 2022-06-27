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
    public partial class main : Form
    {

        public class User 
        {
             string first_name;
             string last_name;
             string username;
             string password;
             string phoneNumber;
             string email;

            public User()
            {
                this.first_name = "";
                this.last_name = "";
                this.username = "";
                this.password = "";
                this.phoneNumber = "";
                this.email = "";
            }

            public void setfirst_name(string X)
            {
                this.first_name = X;
            }

            public string getfirst_name()
            {
                return this.first_name;
            }

            public void setlast_name(string X)
            {
                this.last_name = X;
            }

            public string getlast_name()
            {
                return this.last_name;
            }

            public void setusername(string X)
            {
                this.username = X;
            }

            public string getusername()
            {
                return this.username;
            }

            public void setpassword(string X)
            {
                this.password = X;
            }

            public string getpassword()
            {
                return this.password;
            }

            public void setphoneNumber(string X)
            {
                this.phoneNumber = X;
            }

            public string getphoneNumber()
            {
                return this.phoneNumber;
            }

            public void setemail(string X)
            {
                this.email = X;
            }

            public string getemail()
            {
                return this.email;
            }

            public void CreateAccount(Register Form)//
            {
                Form.CreateAccount();
            }

            public void checkUserInDatabase(Login Form)//
            {
                Form.checkUserInDatabase();
            }

            ~User()
            {
                
            }
        }

        public class Customer : User
        {
             string LastVisit;
             int TotalPaid;
             int TotalPaidLast;
             int NoOfVisits;
             string gender;
             string address;
            string[] prefer_communicate_way;
            string salesman_name;
            public Carpets FavCarpet;

            public Customer()
            {
                this.LastVisit = "";
                this.TotalPaid = 0;
                this.TotalPaidLast = 0;
                this.NoOfVisits = 0;
                this.gender = "";
                this.address = "";
                FavCarpet = new Carpets();
                this.prefer_communicate_way = new string[4];
            }

            public void setLastVisit(string X)
            {
                this.LastVisit = X;
            }

            public string getLastVisit()
            {
                return this.LastVisit;
            }

            public void setTotalPaid(int X)
            {
                this.TotalPaid = X;
            }

            public int getTotalPaid()
            {
                return this.TotalPaid;
            }

            public void setTotalPaidLast(int X)
            {
                this.TotalPaidLast = X;
            }

            public int getTotalPaidLast()
            {
                return this.TotalPaidLast;
            }

            public void setNoOfVisits(int X)
            {
                this.NoOfVisits = X;
            }

            public int getNoOfVisits()
            {
                return this.NoOfVisits;
            }

            public void setgender(string X)
            {
                this.gender = X;
            }

            public string getgender()
            {
                return this.gender;
            }

            public void setaddress(string X)
            {
                this.address = X;
            }

            public string getaddress()
            {
                return this.address;
            }

           

            public void setprefer_communicate_way(string X, int i)
            {
                this.prefer_communicate_way[i] = X;
            }

            public string getprefer_communicate_way(int i)
            {
                return this.prefer_communicate_way[i];
            }

            public void setsalesman_name(string X)
            {
                this.salesman_name = X;
            }

            public string getsalesman_name()
            {
                return this.salesman_name;
            }

            public Order DoOrder(Order X)
            {
                return X;
            }
            public void EditMyAccount(Customer X)
            {
                
            }

            public bool ConfirmOrder(Order O)
            {
                return false;
            }

            ~Customer()
            {

            }
        }

        public class Management : User
        {
            public FinancialProcess FP;
            string jobType;
            public Customer Cu;
            public Carpets C;
            int[][] EntryTime;
            int[][] LeaveTime;

            public Management()
            {
                this.FP = new FinancialProcess();
                this.jobType = "";
                this.Cu = new Customer();
                this.C = new Carpets();
                this.EntryTime = null;
                this.LeaveTime = null;
            }

            public void setjobType(string X)
            {
                this.jobType = X;
            }

            public string getjobType()
            {
                return this.jobType;
            }

            public void setEntryTime(int X, int i, int j)
            {
                this.EntryTime[i][j] = X;
            }

            public int getEntryTime(int i, int j)
            {
                return this.EntryTime[i][j];
            }

            public void setLeaveTime(int X, int i, int j)
            {
                this.LeaveTime[i][j] = X;
            }

            public int getLeaveDates(int i, int j)
            {
                return this.LeaveTime[i][j];
            }

            public void AddnewTransaction(Bill B)
            {

            }
            public void AddnewCarpet(Carpets A)
            {

            }

            public void DeleteTransaction(Bill B)
            {

            }

            public Carpets checkCarpetsHeLike(Customer X, Carpets Y)//
            {
                return Y;
            }

            public void SearchCustomer(SearchCustomer Form)//
            {
                Form.Search_Customer();
            }

            /*public Customer SearchCustomer(Customer X)//
            {
                return X;
            }*/

            public void StartProcess(FinancialProcess FP)
            {

            }

            public Carpets SearchCarpet(Carpets X)
            {
                return X;
            }

           

            public void EditCarpet(Carpets X)
            {

            }



            public void RemoveCarpet(Carpets X)
            {

            }

            public bool CarpetisExist(Carpets X)
            {
                return false;
            }

            public int CarpetQty(Carpets X)
            {
                return 0;
            }

            ~Management()
            {
                
            }
        }

        public class Manager : Management
        {
            public Employee e;

            public Manager()
            {
                this.e = new Employee();
            }

            /*public void EditCustomer(Customer X)//
            {

            }*/

            public void EditCustomer(EditCustomer Form, int i)
            {
                Form.Edit_Customer(i);
            }

            public Employee SearchEmployee(Employee e)
            {
                return e;
            }

            public void AddnewCustomer(AddCustomer Form)//
            {
                Form.AddnewCustomer();
            }

            /*public Customer AddnewCustomer(Customer X)
            {
                return X;
            }*/

            public void AddnewEmployee(Employee e)
            {

            }

            ~Manager()
            {

            }
        }

        public class CoManager : Management
        {
            CoManager()
            {

            }

            public bool VerifyBill(Bill B)
            {
                return false;
            }
            ~CoManager()
            {

            }
        }

        public class Employee : Management
        {
             string EmployeeRank;
             string[] Rewards;
             int Holidays;

            public Employee()
            {
                this.EmployeeRank = "";
                this.Rewards = null;
                this.Holidays = 0;
            }

            public void setEmployeeRank(string X)
            {
                this.EmployeeRank = X;
            }

            public string getEmployeeRank()
            {
                return this.EmployeeRank;
            }

            public void setRewards(string X, int i)
            {
                this.Rewards[i] = X;
            }

            public string getRewards(int i)
            {
                return this.Rewards[i];
            }

            public void setHolidays(int X)
            {
                this.Holidays = X;
            }

            public int getHolidays()
            {
                return this.Holidays;
            }

            public Employee viewMyDetails(Employee e)
            {
                return e;
            }

            ~Employee()
            {

            }
        }

        public class FinancialProcess
        {
             string customerName;
             string PaymentType;
             string SalesManName;

            public FinancialProcess()
            {
                this.customerName = "";
                this.PaymentType = "";
                this.SalesManName = "";
            }

            public void setcustomerName(string X)
            {
                this.customerName = X;
            }

            public string getcustomerName()
            {
                return this.customerName;
            }

            public void setPaymentType(string X)
            {
                this.PaymentType = X;
            }

            public string getPaymentType()
            {
                return this.PaymentType;
            }

            public void setSalesManName(string X)
            {
                this.SalesManName = X;
            }

            public string getSalesManName()
            {
                return this.SalesManName;
            }

            public Bill SearchTransaction(Bill B)
            {
                return B;
            }

            ~FinancialProcess()
            {

            }
        }

        public class Bill
        {
            string BillDate;
            int Discount;
            int NetAmount;
            int invoiceID;

            public Bill()
            {
                this.BillDate = "";
                this.Discount = 0;
                this.NetAmount = 0;
                this.invoiceID = 0;
            }

            public void setBillDate(string X)
            {
                this.BillDate = X;
            }

            public string getBillDate()
            {
                return this.BillDate;
            }

            public void setDiscount(int X)
            {
                this.Discount = X;
            }

            public int get()
            {
                return this.Discount;
            }

            public void setNetAmount(int X)
            {
                this.NetAmount = X;
            }

            public int getNetAmount()
            {
                return this.NetAmount;
            }

            public void setinvoiceID(int X)
            {
                this.invoiceID = X;
            }

            public int getinvoiceID()
            {
                return this.invoiceID;
            }

            public int calculate_order(Bill B)
            {
                return 0;
            }

            ~Bill()
            {

            }

        }

        public class Carpets
        {
            private int qty;
             string type;
             string color;
             string size;
             int price;
            private string supplierName;
            private string location;
            private string dateOfDeliver;
            private bool stillExist;
            private int CarpetID;

            public Carpets()
            {
                this.qty = 0;
                this.type = "";
                this.color = "";
                this.size = "";
                this.price = 0;
                this.supplierName = "";
                this.location = "";
                this.dateOfDeliver = "";
                this.stillExist = false;
                this.CarpetID = 0;
            }

            public void setqty(int X)
            {
                this.qty = X;
            }

            public int getqty()
            {
                return this.qty;
            }

           
            public void settype(string X)
            {
                this.type = X;
            }

            public string gettype()
            {
                return this.type;
            }

            public void setcolor(string X)
            {
                this.color = X;
            }

            public string getcolor()
            {
                return this.color;
            }

            public void setsize(string X)
            {
                this.size = X;
            }

            public string getsize()
            {
                return this.size;
            }

            public void setprice(int X)
            {
                this.price = X;
            }

            public int getprice()
            {
                return this.price;
            }

            public void setsupplierName(string X)
            {
                this.supplierName = X;
            }

            public string getsupplierName()
            {
                return this.supplierName;
            }

            public void setlocation(string X)
            {
                this.location = X;
            }

            public string getlocation()
            {
                return this.location;
            }

            public void setdateOfDeliver(string X)
            {
                this.dateOfDeliver = X;
            }

            public string getdateOfDeliver()
            {
                return this.dateOfDeliver;
            }

            public void setstillExist(bool X)
            {
                this.stillExist = X;
            }

            public bool getstillExist()
            {
                return this.stillExist;
            }

            public void setCarpetID(int X)
            {
                this.CarpetID = X;
            }

            public int getCarpetID()
            {
                return this.CarpetID;
            }
            public int CalculateTotal(string location, int qty)
            {
                return 0;
            }

            ~Carpets()
            {

            }
        }

        public class Order
        {
             int OrderID;
             public Carpets C;
             int Quantity;
             public Bill B;

            public Order()
            {
                this.OrderID = 0;
                this.C = new Carpets();
                this.Quantity = 0;
                this.B = new Bill();
            }

            public void setOrderID(int X)
            {
                this.OrderID = X;
            }

            public int getOrderID()
            {
                return this.OrderID;
            }

            public void setQuantity(int X)
            {
                this.Quantity = X;
            }

            public int getQuantity()
            {
                return this.Quantity;
            }

            public Carpets myCarpet(Carpets Ca)
            {
                return Ca;
            }

            public void MakeBill(Carpets C, Bill B)
            {

            }

            ~Order()
            {

            }
        }



        public main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /*void SelectQuery(string SelectQueryString)
        {
            Command = new MySqlCommand(SelectQueryString, Connection);
            mdr = Command.ExecuteReader();
            if (mdr.Read())
            {
                //textBox3.Text = mdr.GetString("address");
                MessageBox.Show("Successfully Searched");
                //textBox4.Text = mdr.GetInt32("age").ToString();
            }
            else
            {
                MessageBox.Show("No Address for this Name");
            }
        } */

        /*void SelectQueryData(string SelectQueryDataString)
        {
            Adapter = new MySqlDataAdapter(SelectQueryDataString, Connection);
            DataSet ds = new DataSet();
            Adapter.Fill(ds, "workers");
            //dataGridView1.DataSource = ds.Tables["users"];
        }*/

        /*void DMLQuery(string InsertQueryString)
        {
            try
            {
                Command = new MySqlCommand(InsertQueryString, Connection);
                if (Command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Data has queried.");
                }
                else
                {
                    MessageBox.Show("Data has not queried.");
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("" + e.Message);
            }
            
        }*/

        /*void RunQuery()
        {
            try
            {
                Connection.Open();

                //string SelectQueryString1 = "SELECT address FROM crmow.workers WHERE first_name='" + textBox1.Text + "'";
                //SelectQuery(SelectQueryString1);



                //string SelectQueryDataString1 = "SELECT * FROM crmow.workers";
                //SelectQueryData(SelectQueryDataString1);

                //string InsertQueryString1 = "INSERT INTO crmow.workers(first_name, last_name, address) VALUES('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "')";
                //string UpdateQueryString1 = "UPDATE crmow.workers SET address='" + textBox3.Text + "' WHERE first_name= '" + textBox1.Text + "'";
                string DeleteQueryString1 = "DELETE FROM crmow.workers WHERE first_name='Mahmoud'";
                DMLQuery(DeleteQueryString1);

                Connection.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(""+ e.Message);
                Connection.Close();

            }
           
        }*/

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Register RegForm = new Register();
            RegForm.ShowDialog();
            
            this.Close();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login LogForm = new Login();
            LogForm.ShowDialog();
            this.Close();
        }
    }
}
