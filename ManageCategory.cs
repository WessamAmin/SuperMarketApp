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
    public partial class ManageCategory : Form
    {

       
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["market-Conn"].ConnectionString);
       public static int z;
        public ManageCategory()
        {
            InitializeComponent();
        }
        public static string cat2 = "";
        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();
        }
        private void clear()
        {
            Cidtext.Text = "";
            cnametext.Text = "";
            textBox3.Text = "";
        }
        private void populate()
        {
            conn.Open();
            string query = "select * from Categorytb";
            SqlDataAdapter std = new SqlDataAdapter(query, conn);
            SqlCommandBuilder bld = new SqlCommandBuilder(std);
            var ds = new DataSet();
            std.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();


        }
        public static List<string> l = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cnametext.Text == "" || textBox3.Text == "")
                {

                    MessageBox.Show("Please Complete All Infromations");

                }
                else
                {
                    conn.Open();
                    string query = "insert into Categorytb values('" + cnametext.Text + "','" + textBox3.Text + "' ) ";
                    
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Add Successfuly");
                    clear();
                    conn.Close();
                    populate();
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                conn.Close();
            }

        }
      
    

        private void ManageCategory_Load_1(object sender, EventArgs e)
        {
            populate();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                conn.Open();
                string query = "update Categorytb set Cname='"+cnametext.Text+"' , Cdisc='"+ textBox3.Text + "' where CID='"+Cidtext.Text+"' ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Updated Successfuly");
                
                clear();
                conn.Close();
                populate();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.Enabled = false;
            Cidtext.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            cnametext.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
          
        }
       
        private void button2_Click(object sender, EventArgs e)
        {


            try
            {
                conn.Open();
                string query = "(select count(*)  from ProductTb where prodcatid = (select top(1) CID from Categorytb where CID = '"+Cidtext.Text+"' )) " ;
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader dr = cmd.ExecuteReader();
               
                while (dr.Read())
                {
                    z = Convert.ToInt32(dr[0]);
               
                }
                dr.Close();
                    if ( z> 0)
                    {


                        MessageBox.Show(" you can't delete this category ");
                        clear();
                        conn.Close();
                       
                    }

                    else
                    {

                    conn.Close();

                        conn.Open();
                        string query2 = "delete from Categorytb where CID='" + Cidtext.Text + "' ";
                        SqlCommand cmd2 = new SqlCommand(query2, conn);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Category Deleted Successfuly");

                        clear();
                        conn.Close();
                   
                        populate();


                    }

                   
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            ManageSellers mg = new ManageSellers();
            this.Hide();
            mg.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            ManageProducts mg = new ManageProducts();
            this.Hide();
            mg.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Selling s = new Selling();
            this.Hide();
            s.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            clear();
            button1.Enabled = true;
        }

       

    
    }
}
