using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {
        public static string regno;
        public static string type;
        public static string add;
        public static int taxiid;
        public static int taxi_stand_id; // New variable to store taxi_stand_id
        public static int driverid = Form5.driverid;
        int err = 0;

        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            regno = textBox1.Text;
            type = comboBox1.Text;
            string selectedAddress = comboBox2.Text;

            int taxiStandId = 1;

            if (selectedAddress == "Udupi")
            {
                taxiStandId = 2;
            }
            else if (selectedAddress == "Mangalore")
            {
                taxiStandId = 1;
            }

            string connstring = "server=localhost;uid=root;pwd=hello;database=dbspro";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    string insertTaxiQuery = "INSERT INTO taxis (registration_number, type, availability_status, driver_id) " +
                                             "VALUES (@regno, @type, 'available', @driverId)";

                    using (MySqlCommand taxiCmd = new MySqlCommand(insertTaxiQuery, conn))
                    {
                        taxiCmd.Parameters.AddWithValue("@regno", regno);
                        taxiCmd.Parameters.AddWithValue("@type", type);
                        taxiCmd.Parameters.AddWithValue("@driverId", driverid);

                        int taxiRowsAffected = taxiCmd.ExecuteNonQuery();

                        if (taxiRowsAffected > 0)
                        {
                            MessageBox.Show("Data inserted into the 'taxis' table successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Data insertion into the 'taxis' table failed.");
                        }
                    }

                    string retrieveTaxiIdQuery = "SELECT LAST_INSERT_ID() AS TaxiID";

                    using (MySqlCommand retrieveTaxiIdCmd = new MySqlCommand(retrieveTaxiIdQuery, conn))
                    {
                        taxiid = Convert.ToInt32(retrieveTaxiIdCmd.ExecuteScalar());
                    }

                    string insertTaxiStandQuery = "INSERT INTO taxi_stand (driver_id, taxi_id, address, taxi_stand_id) " +
                                                 "VALUES (@driverId, @taxiId, @address, @taxiStandId)";

                    using (MySqlCommand taxiStandCmd = new MySqlCommand(insertTaxiStandQuery, conn))
                    {
                        taxiStandCmd.Parameters.AddWithValue("@driverId", driverid);
                        taxiStandCmd.Parameters.AddWithValue("@taxiId", taxiid);
                        taxiStandCmd.Parameters.AddWithValue("@address", selectedAddress);

                        
                        taxiStandCmd.Parameters.AddWithValue("@taxiStandId", taxiStandId);

                        int taxiStandRowsAffected = taxiStandCmd.ExecuteNonQuery();

                        if (taxiStandRowsAffected > 0)
                        {
                            MessageBox.Show("Data inserted into the 'taxi_stand' table successfully!");

                  
                        }
                        else
                        {
                            MessageBox.Show("Data insertion into the 'taxi_stand' table failed.");
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
                Form9 form9 = new Form9();
                form9.Show();
                this.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void Form6_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
