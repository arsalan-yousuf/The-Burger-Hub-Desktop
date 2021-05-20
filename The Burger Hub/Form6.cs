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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form6_Load(object sender, EventArgs e)
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
            cmd.CommandText = "select * from sales order by id asc";

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
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dataGridView1.DataSource = dataTable;
            con.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            { 
                DataGridViewRow a;
                a = dataGridView1.SelectedRows[0];
                Form22 details = new Form22(a);
                details.Show();
            }
            else
            {
                MessageBox.Show("Select any row first");
            }
            
        }
    }
}
