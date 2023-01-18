using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace SuperMarketApp
{
    public partial class ManageSellers : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["market-Conn"].ConnectionString);
        public ManageSellers()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }
        private void populate()
        {
            conn.Open();
            string query = "select * from SellerTb";
            SqlDataAdapter std = new SqlDataAdapter(query, conn);
            SqlCommandBuilder bld = new SqlCommandBuilder(std);
            var ds = new DataSet();
            std.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();


        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "insert into SellerTb values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "' ) ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Seller Added Successfuly");
                clear();
                conn.Close();
                populate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("some thing Error try later");

            }

        }

        private void ManageSellers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "update SellerTb set Sname='" + textBox2.Text + "' , Sage='" + textBox3.Text + "', Sphone='" + textBox4.Text + "', Spassword='" + textBox5.Text + "' where SID='" + textBox1.Text + "' ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Seller Updated Successfuly");

                clear();
                conn.Close();
                populate();

            }
            catch (Exception ex)
            {
                MessageBox.Show("some thing Error try later");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "delete from SellerTb where SID='" + textBox1.Text + "' ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Seller Deleted Successfuly");

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
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            ManageProducts mg = new ManageProducts();
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
