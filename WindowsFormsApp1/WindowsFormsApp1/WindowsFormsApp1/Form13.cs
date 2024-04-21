using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
        }
        string auser = "Admin";
        string apass = "Admin"; 
        private void button1_Click(object sender, EventArgs e)
        {
            if(auser == textBox1.Text && apass == textBox2.Text) {
                Form14 form14 = new Form14();
                form14.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Credentials"); 
            }
        }
    }
}
