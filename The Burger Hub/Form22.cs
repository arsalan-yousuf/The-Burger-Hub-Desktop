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
    public partial class Form22 : Form
    {
        DataGridViewRow salesInfo;
        public Form22(DataGridViewRow b)
        {
            InitializeComponent();
            salesInfo = b;
        }

        private void Form22_Load(object sender, EventArgs e)
        {
            string conString = "User Id=scott;Password=tiger;Data Source=localhost:1521/xe;";
            string cust="select * from customer where id=:id";
            OracleConnection con = new OracleConnection(conString);
            con.Open();
            using (OracleCommand cmd = new OracleCommand(cust,con))
            {
                cmd.Parameters.Add(new OracleParameter("id", salesInfo.Cells[0].Value));
                OracleDataReader reader = cmd.ExecuteReader();                
                reader.Read();
                label2.Text = label2.Text + reader.GetInt32(0).ToString();
                label3.Text = label3.Text + reader.GetString(1);
                label4.Text = label4.Text + reader.GetString(2);
                label5.Text = label5.Text + reader.GetString(3);
                label6.Text = label6.Text + reader.GetString(4);
                if(reader.GetValue(5)!=DBNull.Value)
                {
                    label7.Text = label7.Text + reader.GetString(5);
                    label7.Visible=true;
                }
                reader.Close();
            }
            con.Close();

            string[] items,prices;
            items=salesInfo.Cells[1].Value.ToString().Split(',');
            prices= salesInfo.Cells[2].Value.ToString().Split(',');
            for(int i=0;i<items.Length-1;i++)
            {
                ListViewItem purchaseList = new ListViewItem(items[i]);
                purchaseList.SubItems.Add(prices[i]);
                listView1.Items.Add(purchaseList);
            }
            label9.Text += salesInfo.Cells[3].Value.ToString();
        }
    }
}
