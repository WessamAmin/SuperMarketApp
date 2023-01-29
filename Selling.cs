using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarketApp
{
    public partial class Selling : Form
    {
        public Selling()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["market-Conn"].ConnectionString);
        private void populatebill()
        {
            string query = "";
            if (Form1.sellername == "admin")
            {
                 query = "select BillNo,SellerName,Billdate,TotalAmount from SellingTb";


            }
            else
            {
                 query = "select BillNo,SellerName,Billdate,TotalAmount from SellingTb Where SellerName='" + Form1.sellername + "' ";
            }
            SqlDataAdapter std = new SqlDataAdapter(query, conn);
            SqlCommandBuilder bld = new SqlCommandBuilder(std);
            var ds = new DataSet();
            std.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();


        }
        private void populate()
        {
           
            string query = "select ProdName,ProdPrice from ProductTb";
            SqlDataAdapter std = new SqlDataAdapter(query, conn);
            SqlCommandBuilder bld = new SqlCommandBuilder(std);
            var ds = new DataSet();
            std.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            conn.Close();


        }
        private void fillcombo()
        {
            conn.Open();
            string query = "select Cname from Categorytb";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Cname", typeof(string));
            dt.Load(dr);
            comboBox1.ValueMember = "Cname";
            comboBox1.DataSource = dt;
            conn.Close();



        }
        private void serchitem()
        {

            string query = "select  ProdName,ProdPrice from ProductTb  where ProdCat='" + comboBox1.SelectedValue.ToString() + "' ";
            SqlDataAdapter std = new SqlDataAdapter(query, conn);
            SqlCommandBuilder bld = new SqlCommandBuilder(std);
            var ds = new DataSet();
            std.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            conn.Close();


        }

        private void getdate()
        {

            label7.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
    }

            private string dateformat= DateTime.Today.Month.ToString() + "/" + DateTime.Today.Day.ToString() + "/" + DateTime.Today.Year.ToString();

        public int n = 0;
        private void addproduct()
        {
            double grtotal = 0;
            if (textBox4.Text == "" || textBox4.Text.GetType().Equals("string"))
            {
                MessageBox.Show("please Enter Correct quantity");
            }
            else
            {
                try
                {
                    double total = Convert.ToDouble(textBox3.Text.Trim()) * Convert.ToDouble(textBox4.Text.Trim());




                    DataGridViewRow newrow = new DataGridViewRow();
                    newrow.CreateCells(dataGridView3);
                    newrow.Cells[0].Value = n + 1;
                    newrow.Cells[1].Value = textBox2.Text;
                    newrow.Cells[2].Value = textBox3.Text;
                    newrow.Cells[3].Value = textBox4.Text;
                    n++;
                    newrow.Cells[4].Value = total;
                    dataGridView3.Rows.Add(newrow);

                    grtotal = grtotal + total;

                    label12.Text = grtotal.ToString();
                    sum += grtotal;
                    Math.Round(sum);

                    label18.Text = sum.ToString();
                    clear();
                }
                catch(Exception ex)
                {
                  //  MessageBox.Show(ex.Message);
                    MessageBox.Show("please Enter Correct quantity");

                }
            }

        }

        private void editproduct()
        {
            double grtotal = 0;

            double total = Convert.ToDouble(textBox3.Text.Trim()) * Convert.ToDouble(textBox4.Text.Trim());
            dataGridView3.SelectedRows[0].Cells[0].Value = n + 1;
            dataGridView3.SelectedRows[0].Cells[1].Value= textBox2.Text;
            dataGridView3.SelectedRows[0].Cells[2].Value= textBox3.Text;
            dataGridView3.SelectedRows[0].Cells[3].Value= textBox4.Text;
            dataGridView3.SelectedRows[0].Cells[4].Value= total;

            grtotal = grtotal + total;

            label12.Text = grtotal.ToString();

            sum += grtotal;
            Math.Round(sum);

            label18.Text = sum.ToString();



        }
        private void delproduct()
        {
            double grtotal = 0;

            double total = 0;
            dataGridView3.SelectedRows[0].Cells[0].Value = "";
            dataGridView3.SelectedRows[0].Cells[1].Value = "";
            dataGridView3.SelectedRows[0].Cells[2].Value = "";
            dataGridView3.SelectedRows[0].Cells[3].Value = "";
            dataGridView3.SelectedRows[0].Cells[4].Value = total;

            grtotal = grtotal + total;

            // label12.Text = grtotal.ToString();

            sum += grtotal;
            Math.Round(sum);
            label18.Text = sum.ToString();



        }



        private void clear() {
            textBox1.Text = "";
            textBox4.Text = "";
        }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button4.Enabled = false;
            label12.Text = dataGridView3.SelectedRows[0].Cells[4].Value.ToString();

            textBox2.Text = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView3.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView3.SelectedRows[0].Cells[3].Value.ToString();

        }
        private void printbill() { 
        
        
        
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public double sum = 0;
        private void Selling_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            label9.Text = Form1.sellername;
            if (label9.Text == "admin")
            {
                label14.Visible = true;
                label15.Visible = true;
                label16.Visible = true;
            }
            else
            {
                label14.Visible = false;
                label15.Visible = false;
                label16.Visible = false;
            }
            populate();
            fillcombo();
            getdate();
            populatebill();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serchitem();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            textBox3.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addproduct();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
               // string query = "insert into SellingTb values('" + label9.Text + "','" + label12.Text + "','" + dateformat + "','" + textBox1.Text + "') ";
                string query = "insert into SellingTb values('" + label9.Text + "','" + sum + "','" + dateformat + "','" + textBox1.Text + "') ";
             
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bill Added Successfuly");
                clear();
                conn.Close();
                populatebill();

            }
            catch (Exception ex)
            {
                MessageBox.Show("some thing Error try later");

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button2.Enabled = true;
            //textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            //textBox3.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            //textBox3.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            //textBox3.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("My SuperMarket Bill", new Font("Century Gothic", 25, FontStyle.Bold),Brushes.Red, new Point(230));
            e.Graphics.DrawString("Bill No: "+dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold),Brushes.Gray, new Point(70,70));
            e.Graphics.DrawString("Seller Name: "+dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold),Brushes.Gray, new Point(70,120));
            e.Graphics.DrawString("Bill Date: "+label7.Text, new Font("Century Gothic", 20, FontStyle.Bold),Brushes.Gray, new Point(70,170));
            e.Graphics.DrawString("Total : "+dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold),Brushes.Gray, new Point(70,220));
            
       
           
         

        }

        private void button2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
           
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label14_Click_1(object sender, EventArgs e)
        {
            ManageProducts mg = new ManageProducts();
            this.Hide();
            mg.Show();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            ManageCategory mg = new ManageCategory();
            this.Hide();
            mg.Show();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            ManageSellers mg = new ManageSellers();
            this.Hide();
            mg.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            editproduct();
            clear();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            clear();
            button4.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            delproduct();
            clear();
        }
    }
}
