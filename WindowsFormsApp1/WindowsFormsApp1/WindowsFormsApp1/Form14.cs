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
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowTotalAmount();
        }

        private void ShowTotalAmount()
        {
            string connstring = "server=localhost;uid=root;pwd=hello;database=dbspro";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    string getTotalAmountQuery = "SELECT SUM(payment_amount) FROM Payments";

                    using (MySqlCommand getTotalAmountCmd = new MySqlCommand(getTotalAmountQuery, conn))
                    {
                        object totalAmountObj = getTotalAmountCmd.ExecuteScalar();

                        if (totalAmountObj != DBNull.Value)
                        {
                            int totalAmount = Convert.ToInt32(totalAmountObj);
                            MessageBox.Show("Total Amount from Payments table: " + totalAmount);
                        }
                        else
                        {
                            MessageBox.Show("No amount information available in Payments table.");
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

        private void Form14_Load(object sender, EventArgs e)
        {

        }

    }
}
