using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarketApp
{
    public partial class startform : Form
    {
        public startform()
        {
            InitializeComponent();
        }
        int statpoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            statpoint += 1;
            myprogress.Value = statpoint;
            if (myprogress.Value == 100)
            {
                myprogress.Value = 0;
                timer1.Stop();
                Form1 f1 = new Form1();
                this.Hide();
                f1.Show();
            }
        }

        private void startform_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

    

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
