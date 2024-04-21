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
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }
        int passengerid = Form7.passenger_id;
        int driverid = Form7.driverid;
        string comments;
        int rideid = Form7.rideid;
        int driverrating;
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form12_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            string comments = textBox1.Text;
            int err = 0;

            try
            {
                int driverrating = DetermineSelectedRating();

                if (driverrating == 0)
                {
                    MessageBox.Show("Please select a rating.");
                    return;
                }

                string connstring = "server=localhost;uid=root;pwd=hello;database=dbspro";

                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    string callProcedureQuery = "CALL InsertRating(@passengerId, @driverId, @rideId, @driverrating, @comments)";
                    using (MySqlCommand callProcedureCmd = new MySqlCommand(callProcedureQuery, conn))
                    {
                        callProcedureCmd.Parameters.AddWithValue("@passengerId", passengerid);
                        callProcedureCmd.Parameters.AddWithValue("@driverId", driverid);
                        callProcedureCmd.Parameters.AddWithValue("@rideId", rideid);
                        callProcedureCmd.Parameters.AddWithValue("@driverrating", driverrating);
                        callProcedureCmd.Parameters.AddWithValue("@comments", comments);

                        int rowsAffected = callProcedureCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Rating inserted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert rating.");
                            err = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                err = 1;
            }

            if (err == 0)
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
        }

        private int DetermineSelectedRating()
        {
            if (radioButton1.Checked)
            {
                return int.Parse(radioButton1.Text);
            }
            else if (radioButton2.Checked)
            {
                return int.Parse(radioButton2.Text);
            }
            else if (radioButton3.Checked)
            {
                return int.Parse(radioButton3.Text);
            }
            else if (radioButton4.Checked)
            {
                return int.Parse(radioButton4.Text);
            }
            else if (radioButton5.Checked)
            {
                return int.Parse(radioButton5.Text);
            }
            else
            {
                return 0;
            }
        }
    }
}
