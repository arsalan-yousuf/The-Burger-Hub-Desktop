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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form9 f = new Form9();
            f.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form7 f = new Form7();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form8 f = new Form8();
            f.Show();
        }

        private void productAdd_Click(object sender, EventArgs e)
        {
            Form4 addForm = new Form4();
            addForm.Show();
        }

        private void productEdit_Click(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 displayForm = new Form1();
            displayForm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void editButton_Click(object sender, EventArgs e)
        {
            Form5 editForm = new Form5();
            editForm.Show();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            Form14 deleteForm = new Form14();
            deleteForm.Show();
        }

        private void viewButton_Click(object sender, EventArgs e)
        {
            Form13 displayForm = new Form13();
            displayForm.Show();
        }

        private void addStaff_Click(object sender, EventArgs e)
        {
            Form12 addStaffForm = new Form12();
            addStaffForm.Show();
        }

        private void viewStaff_Click(object sender, EventArgs e)
        {
            Form15 viewstaff = new Form15();
            viewstaff.Show();
        }

        private void editStaff_Click(object sender, EventArgs e)
        {
            Form16 editStaff = new Form16();
            editStaff.Show();
        }

        private void delStaff_Click(object sender, EventArgs e)
        {
            Form17 deletestaff = new Form17();
            deletestaff.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form18 addInven = new Form18();
            addInven.Show();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            Form19 viewInven = new Form19();
            viewInven.Show();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Form20 updateInven = new Form20();
            updateInven.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form21 deleteInven = new Form21();
            deleteInven.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
