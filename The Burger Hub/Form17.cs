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
    public partial class Form17 : Form
    {
        public Form17()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter ID");
            }
            else
            {
                //To delete row in database

                string conString = "User Id=scott;Password=tiger;Data Source=localhost:1521/xe;";
                OracleConnection con = new OracleConnection();
                con.ConnectionString = conString;
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "delete from staff where id=:id";
                cmd.CommandType = CommandType.Text;
                //Parameters/ Bind variables require accordance with names
                //of OracleParameter instances in the collection
                cmd.Parameters.Add(new OracleParameter("id", textBox1.Text));
                int rowUpdated = cmd.ExecuteNonQuery();
                if (rowUpdated > 0)
                {
                    MessageBox.Show("Successful");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Unsuccessful");
                }
                con.Dispose();
            }
        }
    }
}
