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
    public partial class Form11 : Form
    {
        public static Form10 prevForm;
        int total;
        string products, prices;
        public Form11(int b,string pro,string pri,Form10 c)
        {
            InitializeComponent();
            prevForm = c;
            products = pro;
            prices = pri;
            total = b;
            label7.Text +="Rs. "+ b;
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {           
        }

        private void Form11_Load(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "Home Delivery")
            {
                label8.Visible = false;
            }
            else
            {
                label8.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(fNameTextBox.Text))
            {
                if (!String.IsNullOrEmpty(lNameTextBox.Text))
                {
                    if (!String.IsNullOrEmpty(contactTextBox.Text))
                    {
                        if (!String.IsNullOrEmpty(comboBox1.Text))
                        {
                            if (comboBox1.Text == "Home Delivery" && addressBox.Text == "")
                            {
                                MessageBox.Show("Address Required For Home Delivery !");
                            }
                            else
                            {
                                //To insert row in database
                                string conString = "User Id=scott;Password=tiger;Data Source=localhost:1521/xe;";
                                OracleConnection con = new OracleConnection(conString);
                                //con.ConnectionString = conString;
                                con.Open();
                                string q1= "insert into customer values(cust_seq.NEXTVAL, :fname,:lname,:contact, :category,:address)";
                                using (OracleCommand cmd1 = new OracleCommand(q1, con))
                                {
                                    cmd1.Parameters.Add(new OracleParameter("fname", fNameTextBox.Text)); //(ParameterName, Value)
                                    cmd1.Parameters.Add(new OracleParameter("lname", lNameTextBox.Text));
                                    cmd1.Parameters.Add(new OracleParameter("contact", contactTextBox.Text));
                                    cmd1.Parameters.Add(new OracleParameter("category", comboBox1.Text));
                                    cmd1.Parameters.Add(new OracleParameter("address", addressBox.Text));
                                    cmd1.ExecuteNonQuery();
                                }
                                string q2 = "INSERT INTO SALES VALUES ( (SELECT MAX(ID) FROM CUSTOMER),:prod,:pric,:tot,:cat)";
                                using (OracleCommand cmd2 = new OracleCommand(q2, con))
                                {
                                    cmd2.Parameters.Add(new OracleParameter("prod", products));
                                    cmd2.Parameters.Add(new OracleParameter("pric", prices));
                                    cmd2.Parameters.Add(new OracleParameter("tot", total));
                                    cmd2.Parameters.Add(new OracleParameter("cat", comboBox1.Text));
                                    cmd2.ExecuteNonQuery();
                                }

                                    //:ddeptno, :ddname etc are bind variables/ parameters.
                                    //A bind variable identifier always begins with a single colon ( :).
                                                                                      
                                    //Parameters/ Bind variables require accordance with names
                                    //of OracleParameter instances in the collection

                                string[] selected = products.Split(',');
                                string q3 = "select category from product where name=:pname";
                                string pcat;
                                for(int i=0;i<selected.Length-1;i++)
                                {
                                    using (OracleCommand cmd3 = new OracleCommand(q3, con))
                                    {
                                        cmd3.Parameters.Add(new OracleParameter("pname", selected[i]));
                                        using(OracleDataReader pitem = cmd3.ExecuteReader())
                                        {
                                            pitem.Read();
                                            pcat = pitem.GetString(0);
                                        }
                                    }
                                    if(pcat.ToLower()=="burger")
                                    {
                                        string q4, q5="update inventory set quantity=quantity-1 where lower(productname)='bun'";
                                        if(selected[i].ToLower()=="quadra reloaded")
                                        {
                                            q4 = "update inventory set quantity=quantity-4 where lower(productname) ='patties'";
                                        }
                                        else
                                        {
                                            q4 = "update inventory set quantity=quantity-1 where lower(productname) ='patties'";
                                        }
                                        using (OracleCommand cmd4 = new OracleCommand(q4, con))
                                        {
                                            cmd4.ExecuteNonQuery();
                                        }
                                        using (OracleCommand cmd5 = new OracleCommand(q5, con))
                                        {
                                            cmd5.ExecuteNonQuery();
                                        }
                                        string q6 = "update inventory set quantity=quantity-1 where lower(productname)='fries'";
                                        using (OracleCommand cmd6 = new OracleCommand(q6, con))
                                        {
                                            if(cmd6.ExecuteNonQuery()>0)
                                            {

                                            }
                                            else
                                            {

                                            }
                                        }
                                    }
                                    else
                                    {
                                        string q7 = "update inventory set quantity=quantity-1 where productname=:dname";
                                        using (OracleCommand cmd7 = new OracleCommand(q7,con))
                                        {
                                            cmd7.Parameters.Add(new OracleParameter("dname", selected[i]));
                                            cmd7.ExecuteNonQuery();
                                        }
                                    }
                                }

                                con.Dispose();
                                //Whenever you want to execute an SQL statement that shouldn't return a value or a record set, 
                                //the ExecuteNonQuery should be used.
                                //So if you want to run an update, delete, or insert statement, you should use the 
                                //ExecuteNonQuery.ExecuteNonQuery returns the number of rows affected by the statement.
                                //For all other types of statements, the return value is -1
                                MessageBox.Show("Order Placed");
                                
                                this.Close();
                                prevForm.Close();
                                Form2 main = new Form2();
                                main.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Category Required !");
                            comboBox1.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Contact Required !");
                        contactTextBox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Last Name Required !");
                    lNameTextBox.Focus();
                }
            }
            else
            {
                MessageBox.Show("First Name Required !");
                fNameTextBox.Focus();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            prevForm.Show();
            this.Hide();
        }
    }
}
