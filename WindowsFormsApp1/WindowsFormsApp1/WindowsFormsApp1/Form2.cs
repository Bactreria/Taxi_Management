using MySql.Data.MySqlClient;
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
    public partial class Form2 : Form
    {
        public static string dname;
        public static string dpass;
        public static int driver_id;

        public Form2()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dname = textBox1.Text;
            dpass = textBox2.Text;
            string connstring = "server=localhost;uid=root;pwd=hello;database=dbspro";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    
                    string selectQuery = "SELECT driver_id FROM drivers WHERE driver_name = @dname AND password = @dpass";

                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@dname", dname);
                        cmd.Parameters.AddWithValue("@dpass", dpass);
                        object driverIdObj = cmd.ExecuteScalar();

                        if (driverIdObj != null && driverIdObj != DBNull.Value)
                        {
                            driver_id = Convert.ToInt32(driverIdObj);
                           
                            Form9 form9 = new Form9();
                            form9.Show();
                            this.Hide();
                        }
                        else
                        {

                            MessageBox.Show("Login failed. Please check your username and password.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}