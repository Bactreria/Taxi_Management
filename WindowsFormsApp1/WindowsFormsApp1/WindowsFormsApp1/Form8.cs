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
    public partial class Form8 : Form
    {
        public static string amount;
        string pickup = Form7.pickup;
        string drop = Form7.drop;
        public static int payamt = 0;
        int dist = 0;
        int rideid = Form7.rideid;
        int driverid = Form7.driverid;

        // Timer for intervals
        private Timer timer;
        private int intervalCount = 0;

        public Form8()
        {
            InitializeComponent();

            // Initialize the timer
            timer = new Timer();
            timer.Interval = 30000; // 30 seconds
            timer.Tick += Timer_Tick;
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            intervalCount++;

            switch (intervalCount)
            {
                case 1:
                    UpdateRideStatus("Arriving");
                    MessageBox.Show("Arriving");
                    break;
                case 2:
                    UpdateRideStatus("OntheWay");
                    MessageBox.Show("On the Way");
                    break;
                case 3:
                    UpdateRideStatus("Completed");
                    UpdateDriverAvailability("available"); // Set driver status as available
                    MessageBox.Show("Destination Reached");
                    timer.Stop();
                    Form12 form12 = new Form12();
                    form12.Show();
                    this.Hide();
                    break;
                default:
                    timer.Stop();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();
            form10.Show();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            int dist = 0;
            payamt = 0;
            CalculateDistance();
        }
        private void CalculateDistance()
        {
            // Use a switch statement to find the distance between pickup and drop locations
            switch (pickup)
            {
                case "Udupi Town":
                    switch (drop)
                    {
                        case "Manipal": dist = 5; break;
                        case "Kaup": dist = 13; break;
                        case "Karkala": dist = 38; break;
                        case "Malpe": dist = 6; break;
                        case "Padubidri": dist = 24; break;
                        case "Mangaluru Station": dist = 54; break;
                        case "Mulki": dist = 46; break;
                        case "Surathkal": dist = 50; break;
                        case "Moodabidri": dist = 34; break;
                        case "Bajpe": dist = 56; break;
                        case "Mangaluru Airport": dist = 55; break;
                        default: break;
                    }
                    dist = dist + 2; 
                    break;
                case "Manipal":
                    switch (drop)
                    {
                        case "Udupi Town": dist = 5; break;
                        case "Kaup": dist = 15; break;
                        case "Karkala": dist = 33; break;
                        case "Malpe": dist = 8; break;
                        case "Padubidri": dist = 22; break;
                        case "Mangaluru Station": dist = 65; break;
                        case "Mulki": dist = 58; break;
                        case "Surathkal": dist = 62; break;
                        case "Moodabidri": dist = 26; break;
                        case "Bajpe": dist = 68; break;
                        case "Mangaluru Airport": dist = 65; break;
                        default: break;
                    }
                    dist = dist + 5;
                    break;
                case "Kaup":
                    switch (drop)
                    {
                        case "Udupi Town": dist =13; break;
                        case "Manipal": dist = 15; break;
                        case "Karkala": dist = 30; break;
                        case "Malpe": dist = 21; break;
                        case "Padubidri": dist = 9; break;
                        case "Mangaluru Station": dist = 66; break;
                        case "Mulki": dist = 59; break;
                        case "Surathkal": dist = 63; break;
                        case "Moodabidri": dist = 25; break;
                        case "Bajpe": dist = 70; break;
                        case "Mangaluru Airport": dist = 67; break;
                        default: break;
                    }
                    dist = dist + 14; 
                    break;
                case "Karkala":
                    switch (drop)
                    {
                        case "Udupi Town": dist = 38; break;
                        case "Manipal": dist = 33; break;
                        case "Kaup": dist = 30; break;
                        case "Malpe": dist = 44; break;
                        case "Padubidri": dist = 35; break;
                        case "Mangaluru Station": dist = 87; break;
                        case "Mulki": dist = 79; break;
                        case "Surathkal": dist = 83; break;
                        case "Moodabidri": dist = 15; break;
                        case "Bajpe": dist = 91; break;
                        case "Mangaluru Airport": dist = 85; break;
                        default: break;
                    }
                    dist = dist + 38;
                    break;
                case "Malpe":
                    switch (drop)
                    {
                        case "Udupi Town": dist = 6; break;
                        case "Manipal": dist = 8; break;
                        case "Kaup": dist = 21; break;
                        case "Karkala": dist = 44; break;
                        case "Padubidri": dist = 15; break;
                        case "Mangaluru Station": dist = 62; break;
                        case "Mulki": dist = 54; break;
                        case "Surathkal": dist = 58; break;
                        case "Moodabidri": dist = 30; break;
                        case "Bajpe": dist = 64; break;
                        case "Mangaluru Airport": dist = 65; break;
                        default: break;
                    }
                    dist = dist + 6;
                    break;
                case "Padubidri":
                    switch (drop)
                    {
                        case "Udupi Town": dist = 24; break;
                        case "Manipal": dist = 22; break;
                        case "Kaup": dist = 9; break;
                        case "Karkala": dist = 35; break;
                        case "Malpe": dist = 15; break;
                        case "Mangaluru Station": dist = 49; break;
                        case "Mulki": dist = 41; break;
                        case "Surathkal": dist = 45; break;
                        case "Moodabidri": dist = 24; break;
                        case "Bajpe": dist = 55; break;
                        case "Mangaluru Airport": dist = 52; break;
                        default: break;
                    }
                    dist = dist + 24;
                    break;

                case "Mangaluru Station":
                    switch (drop)
                    {
                        case "Udupi Town": dist = 54; break;
                        case "Manipal": dist = 65; break;
                        case "Kaup": dist = 66; break;
                        case "Karkala": dist = 87; break;
                        case "Malpe": dist = 62; break;
                        case "Padubidri": dist = 49; break;
                        case "Mulki": dist = 14; break;
                        case "Surathkal": dist = 18; break;
                        case "Moodabidri": dist = 68; break;
                        case "Bajpe": dist = 13; break;
                        case "Mangaluru Airport": dist = 17; break;
                        default: break;
                    }
                    dist = dist + 2; 
                    break;

                case "Mulki":
                    switch (drop)
                    {
                        case "Udupi Town": dist = 46; break;
                        case "Manipal": dist = 58; break;
                        case "Kaup": dist = 59; break;
                        case "Karkala": dist = 79; break;
                        case "Malpe": dist = 54; break;
                        case "Padubidri": dist = 41; break;
                        case "Mangaluru Station": dist = 14; break;
                        case "Surathkal": dist = 4; break;
                        case "Moodabidri": dist = 63; break;
                        case "Bajpe": dist = 18; break;
                        case "Mangaluru Airport": dist = 14; break;
                        default: break;
                    }
                    dist = dist + 25;
                    break;
                case "Surathkal":
                    switch (drop)
                    {
                        case "Udupi Town": dist = 50; break;
                        case "Manipal": dist = 62; break;
                        case "Kaup": dist = 63; break;
                        case "Karkala": dist = 83; break;
                        case "Malpe": dist = 58; break;
                        case "Padubidri": dist = 45; break;
                        case "Mangaluru Station": dist = 18; break;
                        case "Mulki": dist = 4; break;
                        case "Moodabidri": dist = 61; break;
                        case "Bajpe": dist = 15; break;
                        case "Mangaluru Airport": dist = 22; break;
                        default: break;
                    }
                    dist = dist + 16;
                    break;

                case "Moodabidri":
                    switch (drop)
                    {
                        case "Udupi Town": dist = 34; break;
                        case "Manipal": dist = 26; break;
                        case "Kaup": dist = 25; break;
                        case "Karkala": dist = 15; break;
                        case "Malpe": dist = 30; break;
                        case "Padubidri": dist = 24; break;
                        case "Mangaluru Station": dist = 68; break;
                        case "Mulki": dist = 63; break;
                        case "Surathkal": dist = 61; break;
                        case "Bajpe": dist = 76; break;
                        case "Mangaluru Airport": dist = 73; break;
                        default: break;
                    }
                    dist = dist + 35;
                    break;

                case "Bajpe":
                    switch (drop)
                    {
                        case "Udupi Town": dist = 56; break;
                        case "Manipal": dist = 68; break;
                        case "Kaup": dist = 70; break;
                        case "Karkala": dist = 91; break;
                        case "Malpe": dist = 64; break;
                        case "Padubidri": dist = 55; break;
                        case "Mangaluru Station": dist = 13; break;
                        case "Mulki": dist = 18; break;
                        case "Surathkal": dist = 15; break;
                        case "Moodabidri": dist = 76; break;
                        case "Mangaluru Airport": dist = 5; break;
                        default: break;
                    }
                    dist = dist + 16;
                    break;

                case "Mangaluru Airport":
                    switch (drop)
                    {
                        case "Udupi Town": dist = 55; break;
                        case "Manipal": dist = 65; break;
                        case "Kaup": dist = 67; break;
                        case "Karkala": dist = 85; break;
                        case "Malpe": dist = 65; break;
                        case "Padubidri": dist = 52; break;
                        case "Mangaluru Station": dist = 17; break;
                        case "Mulki": dist = 14; break;
                        case "Surathkal": dist = 22; break;
                        case "Moodabidri": dist = 73; break;
                        case "Bajpe": dist = 5; break;
                        default: break;
                    }
                    dist = dist + 14;
                    break;
                default:
                    MessageBox.Show("Distance information not available for the selected locations.");
                    break;
            }
            label5.Text = dist.ToString() + " km";
            payamt = dist * 10;
            label2.Text ="Rs." + payamt.ToString(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool paydone = Form10.paydone;
            if (paydone == false)
            {
                MessageBox.Show("Complete the Payment!!!");
            }
            else
            {
                intervalCount = 0; // Reset interval count
                timer.Start();
            }
        }

        private void UpdateRideStatus(string status)
        {
            string connstring = "server=localhost;uid=root;pwd=hello;database=dbspro";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    // Update the status in the 'rides' table
                    string updateStatusQuery = "UPDATE rides SET status = @status WHERE ride_id = @rideId";

                    using (MySqlCommand updateStatusCmd = new MySqlCommand(updateStatusQuery, conn))
                    {
                        updateStatusCmd.Parameters.AddWithValue("@status", status);
                        updateStatusCmd.Parameters.AddWithValue("@rideId", rideid);

                        int rowsAffected = updateStatusCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Ride status updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update ride status.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UpdateDriverAvailability(string availability)
        {
            string connstring = "server=localhost;uid=root;pwd=hello;database=dbspro";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    // Update the availability status in the 'taxis' table
                    string updateAvailabilityQuery = "UPDATE taxis SET availability_status = @availability WHERE driver_id = @driverId";

                    using (MySqlCommand updateAvailabilityCmd = new MySqlCommand(updateAvailabilityQuery, conn))
                    {
                        updateAvailabilityCmd.Parameters.AddWithValue("@availability", availability);
                        updateAvailabilityCmd.Parameters.AddWithValue("@driverId", driverid);

                        int rowsAffected = updateAvailabilityCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Driver availability status updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update driver availability status.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}