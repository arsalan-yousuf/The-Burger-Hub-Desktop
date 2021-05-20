using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace The_Burger_Hub
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {         
            if (userName.Text == "Hammad" || userName.Text == "Arsalan")
            {

                //Create a connection to oracle

                //Connection string
                string conString = "User Id=scott;Password=tiger;Data Source=localhost:1521/xe;";

                //Instantiating a connection object from the connection class
                OracleConnection con = new OracleConnection();

                //Associating connection string with the connection object.
                con.ConnectionString = conString;

                //Using open method to make the actual conversation i.e. to open connection to database
                con.Open();

                //Instantiating a command object from its class
                OracleCommand cmd = new OracleCommand();

                //Adding sql statement to OracleCommand class object which is cmd here
                cmd.CommandText = "select password from admin where name=:a";

                //This Object will use the connection "con"
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new OracleParameter("a", userName.Text));
                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (password.Text == reader.GetString(0))
                {
                    Form3 f = new Form3();
                    f.Show();
                    this.Hide();
                    con.Dispose();
                }
                else if (password.Text == "")
                    MessageBox.Show("Enter password");
                else
                    MessageBox.Show("Wrong password");
            }
            else if(userName.Text=="")
            {
                MessageBox.Show("Enter username");
            }
            else
                MessageBox.Show("User does not exist");           
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 openingForm = new Form2();
            openingForm.Show();
            this.Hide();
        }
    }
}
