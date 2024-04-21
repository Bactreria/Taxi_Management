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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        int amount;
        int passenger_id = Form4.passenger_id;
        string password;

        private void button1_Click(object sender, EventArgs e)
        {
            amount = int.Parse(textBox1.Text);
            password = textBox2.Text;

            // Check if the entered password matches the stored password for the passenger
            string connstring = "server=localhost;uid=root;pwd=hello;database=dbspro";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    // Retrieve the password from the Passengers table
                    string getPasswordQuery = "SELECT password FROM Passengers WHERE passenger_id = @passengerId";

                    using (MySqlCommand getPasswordCmd = new MySqlCommand(getPasswordQuery, conn))
                    {
                        getPasswordCmd.Parameters.AddWithValue("@passengerId", passenger_id);

                        string storedPassword = Convert.ToString(getPasswordCmd.ExecuteScalar());

                        // Check if the entered password matches the stored password
                        if (password.Equals(storedPassword))
                        {
                            // Update the amount in the Passengers table
                            string updateAmountQuery = "UPDATE Passengers SET amount = @amount WHERE passenger_id = @passengerId";

                            using (MySqlCommand updateAmountCmd = new MySqlCommand(updateAmountQuery, conn))
                            {
                                updateAmountCmd.Parameters.AddWithValue("@passengerId", passenger_id);
                                updateAmountCmd.Parameters.AddWithValue("@amount", amount);

                                int updateAmountRowsAffected = updateAmountCmd.ExecuteNonQuery();

                                if (updateAmountRowsAffected > 0)
                                {
                                    MessageBox.Show("Amount updated successfully!");
                                    Form10 form10 = new Form10();
                                    form10.Show();
                                    this.Hide(); // Close the form after successful update
                                }
                                else
                                {
                                    MessageBox.Show("Failed to update amount.");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid password. Please try again.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
