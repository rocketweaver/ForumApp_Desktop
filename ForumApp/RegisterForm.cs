﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ForumApp
{
    public partial class RegisterForm : Form
    {
        RegisterModel register = new RegisterModel();
        public RegisterForm()
        {
            InitializeComponent();
           
        }

        private void txtAcc_Click(object sender, EventArgs e)
        {
            this.Hide();

            LoginForm login = new LoginForm();
            login.ShowDialog();
            login.Show();
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {

            string email = txtEmail.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            bool success = register.CreateUser(email, username, password);

            if (success)
            {

                this.Hide();
                LoginForm login = new LoginForm();
                login.Closed += (s, args) => this.Close();
                login.Show();
            }
            else
            {
                MessageBox.Show("Registrasi gagal. Silakan coba lagi.");
            }
        }
    }
}

     
    

