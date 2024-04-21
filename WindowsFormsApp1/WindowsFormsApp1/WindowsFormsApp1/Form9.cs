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
    public partial class Form9 : Form
    {
        public static int driverid;
        int d1 = Form2.driver_id;
        int d2 = Form5.driverid; 
       
        public static int taxiid;
        public static string rating;
        public static string amount;
        public static string date;
        public static string type;
        public static string description;
        public static string provider;

        public Form9()
        {
            InitializeComponent();
           
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            amount = textBox3.Text;
            description = textBox2.Text;
            provider = textBox1.Text;
            if (radioButton1.Checked)
            {
                type = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                type = radioButton2.Text;
            }
            DateTime selectedDate = dateTimePicker1.Value;
            date = selectedDate.ToString("yyyy-MM-dd");
            if (d1 == 0)
            {
                driverid = d2;
            }
            else if (d2 == 0)
            {
                driverid = d1;
            }

            string connstring = "server=localhost;uid=root;pwd=hello;database=dbspro";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    
                    string retrieveTaxiIdQuery = "SELECT taxi_id FROM Taxis WHERE driver_id = @driverId";

                    using (MySqlCommand retrieveTaxiIdCmd = new MySqlCommand(retrieveTaxiIdQuery, conn))
                    {
                        retrieveTaxiIdCmd.Parameters.AddWithValue("@driverId", driverid);

                        taxiid = Convert.ToInt32(retrieveTaxiIdCmd.ExecuteScalar());
                    }

        
                    string insertMaintenanceQuery = "INSERT INTO maintenance (taxi_id, maintenance_date, maintenance_type, maintenance_description, cost, maintenance_provider) " +
                                                   "VALUES (@taxiId, @maintenanceDate, @maintenanceType, @maintenanceDescription, @cost, @maintenanceProvider)";

                    using (MySqlCommand maintenanceCmd = new MySqlCommand(insertMaintenanceQuery, conn))
                    {
                        maintenanceCmd.Parameters.AddWithValue("@taxiId", taxiid);
                        maintenanceCmd.Parameters.AddWithValue("@maintenanceDate", date);
                        maintenanceCmd.Parameters.AddWithValue("@maintenanceType", type);
                        maintenanceCmd.Parameters.AddWithValue("@maintenanceDescription", description);
                        maintenanceCmd.Parameters.AddWithValue("@cost", amount);
                        maintenanceCmd.Parameters.AddWithValue("@maintenanceProvider", provider);

                        int maintenanceRowsAffected = maintenanceCmd.ExecuteNonQuery();

                        if (maintenanceRowsAffected > 0)
                        {
                            MessageBox.Show("Data inserted into the 'maintenance' table successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Data insertion into the 'maintenance' table failed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string connstring = "server=localhost;uid=root;pwd=hello;database=dbspro";
            double averageRating = 0;
            int driverid = 0;

            if (d1 == 0)
            {
                driverid = d2;
            }
            else if (d2 == 0)
            {
                driverid = d1;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    // Call the MySQL function to retrieve the average rating for the driver
                    string calculateAverageRatingQuery = "SELECT CalculateAverageRating(@driverId)";

                    using (MySqlCommand calculateAverageRatingCmd = new MySqlCommand(calculateAverageRatingQuery, conn))
                    {
                        calculateAverageRatingCmd.Parameters.AddWithValue("@driverId", driverid);

                        // ExecuteScalar returns the result of the SELECT statement
                        object result = calculateAverageRatingCmd.ExecuteScalar();

                        // Check if the result is not null
                        if (result != DBNull.Value && result != null)
                        {
                            averageRating = Convert.ToDouble(result);
                            MessageBox.Show("Average Rating: " + averageRating.ToString("F2"));
                        }
                        else
                        {
                            MessageBox.Show("No ratings available for the driver.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
        private void Form9_Load_1(object sender, EventArgs e)
        {

        }
    }
}
