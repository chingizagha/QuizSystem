﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void labelCreate_Click(object sender, EventArgs e)
        {
            RegistrationForm ff = new RegistrationForm();
            this.Hide();
            ff.Show();
        }
    }
}
