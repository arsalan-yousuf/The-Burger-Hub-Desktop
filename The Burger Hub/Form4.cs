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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (productTextBox.Text == "" || priceTextBox.Text == "" || (!radioButton1.Checked && !radioButton2.Checked))
            {
                MessageBox.Show("Please Fill All Fields");
            }
            else
            {
                string category;
                if(radioButton1.Checked)
                {
                    category = radioButton1.Text;
                }
                else
                {
                    category = radioButton2.Text;
                }
                //To insert row in database
                string conString = "User Id=scott;Password=tiger;Data Source=localhost:1521/xe;";
                OracleConnection con = new OracleConnection();
                con.ConnectionString = conString;
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                //:ddeptno, :ddname etc are bind variables/ parameters.
                //A bind variable identifier always begins with a single colon ( :).
                cmd.CommandText = "INSERT INTO PRODUCT VALUES (prod_seq.nextval, :name, :price, :cat)";
                cmd.CommandType = CommandType.Text;
                //Parameters/ Bind variables require accordance with names
                //of OracleParameter instances in the collection
                cmd.Parameters.Add(new OracleParameter("name", productTextBox.Text)); //(ParameterName, Value)
                cmd.Parameters.Add(new OracleParameter("price", priceTextBox.Text));
                cmd.Parameters.Add(new OracleParameter("cat", category));
                //Whenever you want to execute an SQL statement that shouldn't return a value or a record set, 
                //the ExecuteNonQuery should be used.
                //So if you want to run an update, delete, or insert statement, you should use the 
                //ExecuteNonQuery.ExecuteNonQuery returns the number of rows affected by the statement.
                //For all other types of statements, the return value is -1.
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

        private void productTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
