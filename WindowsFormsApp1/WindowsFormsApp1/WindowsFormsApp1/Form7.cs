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
    public partial class Form7 : Form
    {
        public static string pickup;
        public static string drop;
        public static string type;
        public static string rating;
        public static int passenger_id = Form4.passenger_id;
        public static int driverid;
        public static int rideid;
        public static int bookingid;
        int err = 0;

        public Form7()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pickup = comboBox2.Text;
            drop = comboBox3.Text;
            type = comboBox1.Text;
            if (pickup.Equals(drop, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Pickup and drop locations cannot be the same.");
                err = 1;
            }
            int taxiStandId;
            if (err == 0)
            {
                // Determine taxi stand based on pickup location
                if (pickup == "Udupi Town" || pickup == "Manipal" || pickup == "Kaup" || pickup == "Karkala" || pickup == "Malpe" || pickup == "Padubidri")
                {
                    taxiStandId = 2; // Udupi taxi stand
                }
                else
                {
                    taxiStandId = 1; // Mangalore taxi stand
                }

                string connstring = "server=localhost;uid=root;pwd=hello;database=dbspro";

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connstring))
                    {
                        conn.Open();

                        // Retrieve the available driver ID and taxi type from the specified taxi stand
                        string getDriverIdQuery = "SELECT t.driver_id, t.type FROM taxis t " +
                                                 "JOIN Taxi_Stand ts ON t.driver_id = ts.driver_id " +
                                                 "WHERE ts.taxi_stand_id = @taxiStandId AND t.availability_status = 'available' LIMIT 1";

                        using (MySqlCommand getDriverIdCmd = new MySqlCommand(getDriverIdQuery, conn))
                        {
                            getDriverIdCmd.Parameters.AddWithValue("@taxiStandId", taxiStandId);

                            using (MySqlDataReader reader = getDriverIdCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Driver is available, get the driver ID and taxi type
                                    driverid = reader.GetInt32("driver_id");
                                    string taxiType = reader.GetString("type");

                                    // Close the DataReader before executing the next query
                                    reader.Close();

                                    // Check if taxi type matches the selected ride type
                                    if (string.Equals(taxiType, type, StringComparison.OrdinalIgnoreCase))
                                    {
                                        // Continue with the rest of your code for ride and booking insertion
                                        string insertRideQuery = "INSERT INTO rides (passenger_id, driver_id, pickup_location, dropoff_location, ride_date, type, status) " +
                                                                 "VALUES (@passengerId, @driverId, @pickup, @drop, NOW(), @type, 'NotBooked')";

                                        using (MySqlCommand rideCmd = new MySqlCommand(insertRideQuery, conn))
                                        {
                                            rideCmd.Parameters.AddWithValue("@passengerId", passenger_id);
                                            rideCmd.Parameters.AddWithValue("@driverId", driverid);
                                            rideCmd.Parameters.AddWithValue("@pickup", pickup);
                                            rideCmd.Parameters.AddWithValue("@drop", drop);
                                            rideCmd.Parameters.AddWithValue("@type", type);

                                            int rideRowsAffected = rideCmd.ExecuteNonQuery();

                                            if (rideRowsAffected > 0)
                                            {
                                                MessageBox.Show("Data inserted into the 'rides' table successfully!");

                                                // Retrieve the ride ID
                                                string getRideIdQuery = "SELECT LAST_INSERT_ID() AS ride_id";
                                                using (MySqlCommand getRideIdCmd = new MySqlCommand(getRideIdQuery, conn))
                                                {
                                                    rideid = Convert.ToInt32(getRideIdCmd.ExecuteScalar());
                                                }

                                                string insertBookingQuery = "INSERT INTO bookings (ride_id, booking_date) " +
                                                                           "VALUES (@rideId, NOW())";

                                                using (MySqlCommand bookingCmd = new MySqlCommand(insertBookingQuery, conn))
                                                {
                                                    bookingCmd.Parameters.AddWithValue("@rideId", rideid);

                                                    int bookingRowsAffected = bookingCmd.ExecuteNonQuery();

                                                    if (bookingRowsAffected > 0)
                                                    {
                                                        MessageBox.Show("Data inserted into the 'bookings' table successfully!");

                                                        // Retrieve the booking ID
                                                        string getBookingIdQuery = "SELECT LAST_INSERT_ID() AS booking_id";
                                                        using (MySqlCommand getBookingIdCmd = new MySqlCommand(getBookingIdQuery, conn))
                                                        {
                                                            bookingid = Convert.ToInt32(getBookingIdCmd.ExecuteScalar());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Data insertion into the 'bookings' table failed.");
                                                        err = 1;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Data insertion into the 'rides' table failed.");
                                                err = 1;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No available drivers for the selected pickup location and ride type.");
                                        err = 1;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No available drivers for the selected pickup location.");
                                    err = 1;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    err = 1;
                }
            }
            if (err == 0)
            {
                Form8 form8 = new Form8();
                form8.Show();
                this.Hide();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
