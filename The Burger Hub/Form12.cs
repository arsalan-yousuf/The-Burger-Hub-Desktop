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
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(firstNameText.Text!="")
            {
                if(contactText.Text!="")
                {
                    if(addressText.Text!="")
                    {
                        if(comboBox1.Text!="")
                        {
                            if(comboBox2.Text!="")
                            {
                                if(salaryText.Text!="")
                                {
                                    //To insert row in database
                                    string conString = "User Id=scott;Password=tiger;Data Source=localhost:1521/xe;";
                                    OracleConnection con = new OracleConnection();
                                    con.ConnectionString = conString;
                                    con.Open();
                                    OracleCommand cmd = new OracleCommand();
                                    cmd.Connection = con;
                                    //:ddeptno, :ddname etc are bind variables/ parameters.
                                    //A bind variable identifier always begins with a single colon ( :).
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "INSERT INTO STAFF VALUES (staff_seq.nextval, :first, :last,to_date(:dob,'MM/DD/YYYY'),:cnt,:addr,:qual,:desig,:sal)";
                                    //Parameters/ Bind variables require accordance with names
                                    //of OracleParameter instances in the collection
                                    cmd.Parameters.Add(new OracleParameter("first", firstNameText.Text)); //(ParameterName, Value)
                                    cmd.Parameters.Add(new OracleParameter("last", lastNameText.Text));
                                    cmd.Parameters.Add(new OracleParameter("dob", dateTimePicker1.Text));
                                    cmd.Parameters.Add(new OracleParameter("cnt", contactText.Text));
                                    cmd.Parameters.Add(new OracleParameter("addr", addressText.Text));
                                    cmd.Parameters.Add(new OracleParameter("qual", comboBox1.Text));
                                    cmd.Parameters.Add(new OracleParameter("desig", comboBox2.Text));
                                    cmd.Parameters.Add(new OracleParameter("sal", salaryText.Text));
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
                                else
                                {
                                    MessageBox.Show("Enter Salary");
                                    salaryText.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Select Designation");
                                comboBox2.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Select Qualification");
                            comboBox1.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Address Required");
                        addressText.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Enter Contact Number");
                    contactText.Focus();
                }
            }
            else
            {
                MessageBox.Show("First Name Required");
                firstNameText.Focus();
            }
        }
    }   
}
