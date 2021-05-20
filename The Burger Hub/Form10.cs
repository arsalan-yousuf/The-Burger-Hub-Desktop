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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }
        public static int sum;
        DataTable Inventory = new DataTable();
        private void Form10_Load(object sender, EventArgs e)
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
            //'order by column_name asc' will fetch data in ascending order from database.
            cmd.CommandText = "select name,price from product order by id asc";

            //This Object will use the connection "con"
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            //To retrieve data instantiate an OracleDataReader object
            //OracleCommand ExecuteReader method executes a command specified in the CommandText and
            //returns an OracleDataReader object
            OracleDataReader reader = cmd.ExecuteReader();

            //To display/view table
            //Whenever you want to execute an SQL statement that should
            //return a value or a record set use .Read();
            //But in this case, No need of .Read() as the OracleDataReader object 
            //could be passed to the Load method of the DataTable class object and 
            //then the table is ready to be bound to the DataGridView DataSource property.
            //Subitems are only visible in certain listview modes.
            //A subitem cannot exist without its "parent" / main item.
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            for(int i=0;i<dataTable.Rows.Count;i++)
            {
                DataRow drow = dataTable.Rows[i];
                ListViewItem menuList = new ListViewItem(drow["name"].ToString());
                menuList.SubItems.Add(drow["price"].ToString());
                listView1.Items.Add(menuList);
            }
            /*ListViewItem menu = new ListViewItem();
            //Either declare main/parent item this way or
            //pass main item value to listviewitem constructor.
            menu.Text = "Jalapeno";
            menu.SubItems.Add("5");
            listView1.Items.Add(menu);
            ListViewItem menu1 = new ListViewItem("Quadra");
            menu1.SubItems.Add("6");
            listView1.Items.Add(menu1);
            ListViewItem menu2 = new ListViewItem("Firehouse");
            menu2.SubItems.Add("7");
            listView1.Items.Add(menu2);
            ListViewItem menu3 = new ListViewItem("BBQ flip");
            menu3.SubItems.Add("8");
            listView1.Items.Add(menu3);*/
            string q2 = "select productname,quantity from inventory";
            using (OracleCommand cmd2 = new OracleCommand(q2, con))
            {
                using (OracleDataReader i = cmd2.ExecuteReader())
                {
                    Inventory.Load(i);
                }
            }
            con.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item first");
            }
            else
            {
                string conString = "User Id=scott;Password=tiger;Data Source=localhost:1521/xe;";
                OracleConnection con = new OracleConnection(conString);
                con.Open();
                string q1 = "select category from product where lower(name)=:pname";
                string cat;
                int j;
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    using (OracleCommand cmd1 = new OracleCommand(q1,con))
                    {
                        cmd1.Parameters.Add(new OracleParameter("pname", listView1.SelectedItems[i].Text.ToLower()));
                        using (OracleDataReader pitem = cmd1.ExecuteReader())
                        {
                            pitem.Read();
                            cat = pitem.GetString(0);
                        }
                    }
                    if (cat.ToLower() == "burger")
                    {
                        for (j = 0; j < Inventory.Rows.Count; j++)
                        {
                            if (Inventory.Rows[j]["productname"].ToString().ToLower() == "bun")
                            {
                                break;
                            }
                        }
                        if(int.Parse(Inventory.Rows[j]["quantity"].ToString())==0)
                        {
                            MessageBox.Show("Burgers temporarily unavailable\nSorry for inconvienience");
                            continue;
                        }
                        else
                        {
                            Inventory.Rows[j]["quantity"] = int.Parse(Inventory.Rows[j]["quantity"].ToString()) - 1;
                        }
                        for (j = 0; j < Inventory.Rows.Count; j++)
                        {
                            if (Inventory.Rows[j]["productname"].ToString().ToLower() == "patties")
                            {
                                break;
                            }
                        }
                        if (listView1.SelectedItems[i].Text.ToLower() == "quadra reloaded")
                        {
                            if (int.Parse(Inventory.Rows[j]["quantity"].ToString()) < 4 )
                            {
                                MessageBox.Show("Insufficient patties for Quadra\nSorry for inconvenience");
                                continue;
                            }
                            else
                            {
                                Inventory.Rows[j]["quantity"] = int.Parse(Inventory.Rows[j]["quantity"].ToString()) - 4;
                            }
                        }
                        else
                        {
                            if (int.Parse(Inventory.Rows[j]["quantity"].ToString()) == 0)
                            {
                                MessageBox.Show("Burgers temporarily unavailable\nSorry for inconvienience");
                                continue;
                            }
                            else
                            {
                                Inventory.Rows[j]["quantity"] = int.Parse(Inventory.Rows[j]["quantity"].ToString()) - 1;
                            }
                        }
                        for (j = 0; j < Inventory.Rows.Count; j++)
                        {
                            if (Inventory.Rows[j]["productname"].ToString().ToLower() == "fries")
                            {
                                break;
                            }
                        }
                        if (int.Parse(Inventory.Rows[j]["quantity"].ToString()) == 0)
                        {
                            MessageBox.Show("Fries not included due to unavailibility");
                        }
                        else
                        {
                            Inventory.Rows[j]["quantity"] = int.Parse(Inventory.Rows[j]["quantity"].ToString()) - 1;
                        }
                    }
                    else
                    {
                        for( j=0;j<Inventory.Rows.Count;j++)
                        {
                            if(Inventory.Rows[j]["productname"].ToString()== listView1.SelectedItems[i].Text)
                            {
                                break;
                            }
                        }
                        if(int.Parse(Inventory.Rows[j]["quantity"].ToString())>0)
                        {
                            Inventory.Rows[j]["quantity"] = int.Parse(Inventory.Rows[j]["quantity"].ToString()) - 1;
                        }
                        else
                        {
                            MessageBox.Show(listView1.SelectedItems[i].Text + " currently unavailable");
                            continue;
                        }
                    }
                        //0 is the index of parent/main item so subitem's column start from 1 index.
                        //SelectedItems[i] will target the main item and SubItems[1] will target the 
                        //second column/ first subitem column of that selected item.
                        ListViewItem order = new ListViewItem(listView1.SelectedItems[i].Text);
                        order.SubItems.Add(listView1.SelectedItems[i].SubItems[1].Text);
                        listView2.Items.Add(order);
                }
                con.Dispose();           
            }

            //To calculate price of total items
            sum = 0;
            string convertToString;
            for (int i = 0; i < listView2.Items.Count; i++)
            {
                convertToString = listView2.Items[i].SubItems[1].Text;
                sum = sum + int.Parse(convertToString);
            }
            convertToString = sum.ToString();
            totalDisplayLabel.Text = "Rs. "+convertToString;

            listView1.SelectedItems.Clear();//else item will stay selected even after adding to the cart.
            listView2.SelectedItems.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item first");
            }
            else
            {
                string conString = "User Id=scott;Password=tiger;Data Source=localhost:1521/xe;";
                OracleConnection con = new OracleConnection(conString);
                con.Open();
                string q = "select category from product where lower(name)=:pname";
                string cate;
                int x;
                foreach (ListViewItem eachItem in listView2.SelectedItems)
                {
                    using (OracleCommand cmd1 = new OracleCommand(q, con))
                    {
                        cmd1.Parameters.Add(new OracleParameter("pname", eachItem.Text.ToLower()));
                        using (OracleDataReader pitem = cmd1.ExecuteReader())
                        {
                            pitem.Read();
                            cate = pitem.GetString(0);
                        }
                    }
                    if(cate.ToLower()=="burger")
                    {
                        for (x = 0; x < Inventory.Rows.Count; x++)
                        {
                            if (Inventory.Rows[x]["productname"].ToString().ToLower() == "bun")
                            {
                                Inventory.Rows[x]["quantity"] = int.Parse(Inventory.Rows[x]["quantity"].ToString()) + 1;
                            }
                            if(Inventory.Rows[x]["productname"].ToString().ToLower() == "patties")
                            {
                                if(eachItem.Text.ToLower()=="quadra reloaded")
                                {
                                    Inventory.Rows[x]["quantity"] = int.Parse(Inventory.Rows[x]["quantity"].ToString()) + 4;
                                }
                                else
                                {
                                    Inventory.Rows[x]["quantity"] = int.Parse(Inventory.Rows[x]["quantity"].ToString()) + 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (x=0;x<Inventory.Rows.Count;x++)
                        {
                            if (Inventory.Rows[x]["productname"].ToString() == eachItem.Text)
                            {
                                Inventory.Rows[x]["quantity"] = int.Parse(Inventory.Rows[x]["quantity"].ToString()) + 1;
                                break;
                            }
                        }
                    }
                    listView2.Items.Remove(eachItem);

                }
                con.Dispose();
            }

            //To calculate price of total items
            sum = 0;
            string convertToString;
            for (int i = 0; i < listView2.Items.Count; i++)
            {
                convertToString = listView2.Items[i].SubItems[1].Text;
                sum = sum + int.Parse(convertToString);
            }
            convertToString = sum.ToString();
            totalDisplayLabel.Text = convertToString;
            listView1.SelectedItems.Clear();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView2.Items.Count != 0)
            {
                string products = "", prices = "";
                foreach (ListViewItem item in listView2.Items)
                {
                    products += item.Text + ",";
                    prices += item.SubItems[1].Text + ",";
                }
                Form11 f = new Form11(sum, products, prices, this);
                foreach (ListViewItem item in listView2.Items)
                {
                    f.listView1.Items.Add((ListViewItem)item.Clone());
                }
                f.Show();
            }
            else
            {
                MessageBox.Show("Cart Empty !");
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
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
