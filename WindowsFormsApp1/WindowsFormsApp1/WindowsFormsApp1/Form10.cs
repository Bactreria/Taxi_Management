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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        public static string accno, amt, pin;
        int payamt = Form8.payamt;
        int passenger_id = Form4.passenger_id;
        int insuff = 0;
        int rideid = Form7.rideid;
        int bookingid = Form7.bookingid;
        int passamt;
        int driverid = Form7.driverid;
        public static bool paydone = false;
        int amount; 
        private void Form10_Load(object sender, EventArgs e)
        {
            label5.Text = payamt.ToString();

            // Extract amount from the passenger table
            string connstring = "server=localhost;uid=root;pwd=hello;database=dbspro";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    // Retrieve the amount from the passenger table
                    string getPassengerAmountQuery = "SELECT amount FROM Passengers WHERE passenger_id = @passengerId";

                    using (MySqlCommand getPassengerAmountCmd = new MySqlCommand(getPassengerAmountQuery, conn))
                    {
                        getPassengerAmountCmd.Parameters.AddWithValue("@passengerId", passenger_id);

                        // Execute the query and get the result
                        passamt = Convert.ToInt32(getPassengerAmountCmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving passenger amount: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            accno = textBox1.Text;
            pin = textBox2.Text;
            amt = textBox3.Text;
            amount = int.Parse(amt.ToString());
            // Check if passamt is greater than or equal to the transaction amount
            
            if (passamt >= 0 && passamt >= Convert.ToInt32(amount))
            {
                // Insert into the passenger table
                string connstring = "server=localhost;uid=root;pwd=hello;database=dbspro";
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connstring))
                    {
                        conn.Open();

                        

                        // Insert into the transactions table
                        string insertTransactionQuery = "INSERT INTO Payments (passenger_id, ride_id, booking_id, payment_date, payment_amount) " +
                                                        "VALUES (@passengerId, @rideId, @bookingId, NOW(), @amount)";

                        using (MySqlCommand transactionCmd = new MySqlCommand(insertTransactionQuery, conn))
                        {
                            transactionCmd.Parameters.AddWithValue("@passengerId", passenger_id);
                            transactionCmd.Parameters.AddWithValue("@rideId", rideid);
                            transactionCmd.Parameters.AddWithValue("@bookingId", bookingid);
                            transactionCmd.Parameters.AddWithValue("@amount", amount);

                            int transactionRowsAffected = transactionCmd.ExecuteNonQuery();

                            if (transactionRowsAffected > 0)
                            {
                                MessageBox.Show("Transaction successful!");

                                // Update the passenger amount after the transaction
                                string updatePassengerAmountQuery = "UPDATE Passengers SET amount = amount - @amount WHERE passenger_id = @passengerId";
                                paydone = true;
                                using (MySqlCommand updateAmountCmd = new MySqlCommand(updatePassengerAmountQuery, conn))
                                {
                                    updateAmountCmd.Parameters.AddWithValue("@passengerId", passenger_id);
                                    updateAmountCmd.Parameters.AddWithValue("@amount", amount);

                                    int updateAmountRowsAffected = updateAmountCmd.ExecuteNonQuery();

                                    if (updateAmountRowsAffected > 0)
                                    {
                                        MessageBox.Show("Passenger amount updated successfully!");

                                        // Update the availability status of the selected driver to 'unavailable'
                                        string updateDriverAvailabilityQuery = "UPDATE Taxis SET availability_status = 'unavailable' WHERE driver_id = @driverId";

                                        using (MySqlCommand updateDriverCmd = new MySqlCommand(updateDriverAvailabilityQuery, conn))
                                        {
                                            updateDriverCmd.Parameters.AddWithValue("@driverId", driverid);

                                            int updateDriverRowsAffected = updateDriverCmd.ExecuteNonQuery();

                                            if (updateDriverRowsAffected > 0)
                                            {
                                                MessageBox.Show("Driver set as unavailable.");
                                            }
                                            else
                                            {
                                                MessageBox.Show("Failed to update driver availability.");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Failed to update passenger amount.");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Transaction failed.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Insufficient balance. Transaction failed.");
                insuff = 1;
            }
            if (insuff == 1)
            {
                Form11 form11 = new Form11();
                form11.Show();
                this.Hide();
            }
            else
            {
                this.Hide();
            }
        }

    }
}

