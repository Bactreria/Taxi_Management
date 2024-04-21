using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        public static string driverName;
        public static string password;
        public static string contactNumber;
        public static string licenseNumber;
        public static int driverid;
        int err = 0; 
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            driverName = textBox1.Text;
            password = textBox2.Text;
            contactNumber = textBox4.Text;
            licenseNumber = textBox5.Text;
            string connstring = "server=localhost;uid=root;pwd=hello;database=dbspro";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO drivers (driver_name, password, contact_number, license_number) " +
                                         "VALUES (@driverName, @password, @contactNumber, @licenseNumber)";
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@driverName", driverName);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@contactNumber", contactNumber);
                        cmd.Parameters.AddWithValue("@licenseNumber", licenseNumber);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                           
                            string retrieveDriverIdQuery = "SELECT LAST_INSERT_ID() AS DriverID";

                            using (MySqlCommand retrieveDriverCmd = new MySqlCommand(retrieveDriverIdQuery, conn))
                            {
                                driverid = Convert.ToInt32(retrieveDriverCmd.ExecuteScalar());
                            }

                            MessageBox.Show("Data inserted successfully!");
                            

                        }
                        else
                        {
                            MessageBox.Show("Data insertion failed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                err = 1; 
            }
            if (err == 0)
            {
                Form6 form6 = new Form6();
                form6.Show();
                this.Hide();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
