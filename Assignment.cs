using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab
{
    public partial class Form2 : Form 
    {
        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(path);
            display();

        }
       
        DataTable dt;
        string path = @"Data Source=MYDESKTOP\SQLEXPRESS;Initial Catalog=Events;Integrated security=True";
        SqlConnection con;
        SqlCommand cmd;
        private void button1_Click(object sender, EventArgs e)
        {
           
            con = new SqlConnection(path);
            con.Open();
            work();
            cmd = new SqlCommand("Insert into table1 (name,age,product,prize,date) values('" + txtName.Text + "','" + txtAge.Text + "','" + txtProduct.Text + "','" + txtPrize.Text + "','" + txtDate.Text + "')", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Inserted Successfully");
            con.Close();
            display();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Class1.list;
        }
        public void display()
        {
            try
            {   dt=new DataTable();
                con.Open();
                string query = "Select * from table1";
               cmd= new SqlCommand(query, con);
               var result = cmd.ExecuteReader();
                
                while (result.Read())
                {
                    Class1 c = new Class1();
                    c.Name = (string)result[0];
                    c.age = (int)result[1];
                    c.product = (string)result[2];
                    c.prize = (int)result[3];
                    c.ExpiryDate = (string)result[4];
                    Class1.list.Add(c);

                }
                dataGridView1.DataSource = null;
                 dataGridView1.DataSource= dt;
                 con.Close();
            }
            
            catch(Exception e)
            {
                  
                MessageBox.Show(e.Message);
            
            }
        }
        public void work()
        {
            dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Age");
            dt.Columns.Add("Product");
            dt.Columns.Add("Prize");
            dt.Columns.Add("Expiry Date");
            DataRow dr = dt.NewRow();
            dr[0] = txtName.Text;
            dr[1] = txtAge.Text;
            dr[2] = txtProduct.Text;
            dr[3] = txtPrize.Text;
            dr[4] = txtDate.Text;
            dt.Rows.Add(dr);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
        }
      

        private void button2_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtAge.Text = "";
            txtProduct.Text = "";
            txtPrize.Text = "";
            txtDate.Text = "";

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            display();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Class1.list;

        }



        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("Delete from table1 where name='" + txtName.Text + "'",con);
               int x= cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(x+"Seleted Successfully!!!");
                display();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = Class1.list;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAge.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtProduct.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrize.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtDate.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }
    }
}
