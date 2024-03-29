﻿using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;

namespace My_ATM_APPLICATION
{
    public partial class Withdraw : Form
    {
        public Withdraw()
        {
            InitializeComponent();
        }
        BusinessLogicLayerClass bll = new BusinessLogicLayerClass();

        private void Withdraw_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void btnWithDraw_Click_1(object sender, EventArgs e)
        {
                            // Validate and parse the withdrawal amount
                if (!decimal.TryParse(txtAmount.Text, out decimal withDrawAmount) || withDrawAmount <= 0)
                {
                    MessageBox.Show("Invalid withdrawal amount. Please enter a valid positive number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Gets the account number from the Home form
                int accNumber = int.Parse(Home.AccNumber);
                
                // Calling business logic layer to check if the withdrawal is valid
                if (withDrawAmount > bll.GetBalance(accNumber))
                {
                    MessageBox.Show("Insufficient funds for withdrawal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Calling business logic layer to update the balance
                if (bll.withDrawal(accNumber, withDrawAmount))
                {
                    MessageBox.Show("Withdrawal Unsuccessful", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Withdrawal Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAmount.Clear();
                    refresh();
                }
        }
     

        private void lblBack_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void lblBack_MouseHover(object sender, EventArgs e)
        {
            lblBack.ForeColor = Color.Red;
        }

        private void lblBack_MouseLeave(object sender, EventArgs e)
        {
            lblBack.ForeColor = Color.Black;
        }


        public void refresh()
        {
            lblBalance.Text = Home.AccNumber;

            decimal balance = bll.GetBalance(int.Parse(lblBalance.Text));
            lblBalance.Text = $"{balance:C}";
        }

       
    }
}
