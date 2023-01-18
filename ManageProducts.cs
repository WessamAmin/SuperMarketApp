using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarketApp
{
    public partial class ManageProducts : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["market-Conn"].ConnectionString);
        public ManageProducts()
        {
            InitializeComponent();
        }
        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        
        }
        private void populate()
        {
            conn.Open();
            string query = "select * from ProductTb";
            SqlDataAdapter std = new SqlDataAdapter(query, conn);
            SqlCommandBuilder bld = new SqlCommandBuilder(std);
            var ds = new DataSet();
            std.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();


        }
        private void fillcombo() {
            conn.Open();
            string query = "select Cname from Categorytb";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Cname", typeof(string));
            dt.Load(dr);
            comboBox1.ValueMember = "Cname";
            comboBox2.ValueMember = "Cname";
            comboBox1.DataSource = dt;
            comboBox2.DataSource = dt;



            conn.Close();



        }
        private void serchitem()
        {
           
            string query = "select * from ProductTb  where ProdCat='" + comboBox2.SelectedValue.ToString() + "' "; 
            SqlDataAdapter std = new SqlDataAdapter(query, conn);
            SqlCommandBuilder bld = new SqlCommandBuilder(std);
            var ds = new DataSet();
            std.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();


        }
        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void ManageProducts_Load(object sender, EventArgs e)
        {
           
            populate();
            fillcombo();
            clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "insert into ProductTb values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.SelectedValue.ToString() + "' ) ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfuly");
                clear();
                conn.Close();
                populate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("some thing Error try later");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "update ProductTb set ProdName='" + textBox2.Text + "' , ProdQty='" + textBox3.Text + "' , ProdPrice='" + textBox4.Text + "' , ProdCat='" + comboBox1.SelectedValue.ToString()+ "' where ProdID='" + textBox1.Text + "' ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Updated Successfuly");

                clear();
                conn.Close();
                populate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("some thing Error try later");

            }
        }
       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
           comboBox1.SelectedValue= dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
           



        }

        private void button2_Click(object sender, EventArgs e)
        {
          

            try
            {
                conn.Open();
                string query = "delete from ProductTb where ProdID='" + textBox1.Text + "' ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                
                MessageBox.Show("Product Deleted Successfuly");

                clear();
                conn.Close();
                populate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("some thing Error try later");

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            serchitem();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            ManageSellers mg = new ManageSellers();
            this.Hide();
            mg.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            ManageCategory mg = new ManageCategory();
            this.Hide();
            mg.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();
        }
    }
}
