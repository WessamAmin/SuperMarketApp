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
    public partial class Form1 : Form
    {
        public static string sellername = "";
        SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["market-Conn"].ConnectionString);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {


                if (usernametext.Text == "" || passwordtext.Text == "")
                {
                    MessageBox.Show(" Please complete your infromation  ");

                }


                else 
                {
                    if (selectrolecombobox.SelectedIndex > -1)
                    {
                        if (selectrolecombobox.SelectedItem.ToString() == "ADMIN")
                        {
                            if (usernametext.Text == "admin" && passwordtext.Text == "1")
                            {
                                ManageProducts mg = new ManageProducts();
                                this.Hide();
                                mg.Show();
                            }
                            else
                            {
                                MessageBox.Show(" Login faoled you are not Admin  ");
                            }
                        }

                        else if (selectrolecombobox.SelectedItem.ToString() == "SALLER")
                        {

                            Connection.Open();
                            string query = " select count(8) from SellerTb where Sname='"+usernametext.Text+"' and Spassword='"+passwordtext.Text+"' ";
                            sellername = usernametext.Text;
                            SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            if (dt.Rows[0][0].ToString() == "1"){

                                Selling sl = new Selling();
                                this.Hide();
                                sl.Show();
                            }
                            else
                            {
                                MessageBox.Show(" Login failed ! user name or password not correct  ");
                            }
                          
                            Connection.Close();
                          
                          
                           

                        }
                         }
                    else
                    {
                        MessageBox.Show(" Select A Role ");
                    }
                }

                 
                



            }
            catch(Exception ex)
            {
                MessageBox.Show(" there is Some thing Error");
            }

        }

        private void label10_Click(object sender, EventArgs e)
        {
            usernametext.Text = "";
            passwordtext.Text = "";
            selectrolecombobox.Text = "";
           
        }

       
    }
}
