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
    public partial class Form16 : Form
    {
        public Form16()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (firstNameText.Text != "")
            {
                if (contactText.Text != "")
                {
                    if (addressText.Text != "")
                    {
                        if (comboBox1.Text != "")
                        {
                            if (comboBox2.Text != "")
                            {
                                if (salaryText.Text != "")
                                {
                                    //To update row in database

                                    string conString = "User Id=scott;Password=tiger;Data Source=localhost:1521/xe;";
                                    OracleConnection con = new OracleConnection();
                                    con.ConnectionString = conString;
                                    con.Open();
                                    OracleCommand cmd = new OracleCommand();
                                    cmd.Connection = con;
                                    cmd.CommandText = "update staff set firstname=:first, lastname=:last, dateofbirth=to_date(:dob,'MM/DD/YYYY'), contact=:cnt, address=:addr, qualification=:qual, designation=:desig, salary=:sal where id=:id";
                                    cmd.CommandType = CommandType.Text;
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
