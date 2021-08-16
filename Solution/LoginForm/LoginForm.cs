using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LoginForm
{
    public partial class LoginForm : Form
    {
        public static string passingText;
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
        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-82S0U2V;Initial Catalog=QuizSistem;User ID=sa;Password=murad123");
            SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE Email='" + textBox1.Text + "' AND Password='" + textBox2.Text + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string cmbItemValue = comboBoxType.SelectedItem.ToString();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["UserType"].ToString() == cmbItemValue)
                    {

                        MessageBox.Show("You are login as " + dt.Rows[i][1]);
                        if (comboBoxType.SelectedIndex == 0)
                        {
                            passingText = textBox1.Text;
                            AdminPanel f = new AdminPanel();
                            f.Show();
                            this.Hide();
                        }
                        else if (comboBoxType.SelectedIndex == 1)
                        {
                            passingText = textBox1.Text;
                            TeacherPanel fr = new TeacherPanel();
                            this.Hide();
                            fr.Show();
                        }
                        else
                        {
                            passingText = textBox1.Text;
                            StudentPanel fr = new StudentPanel();
                            this.Hide();
                            fr.Show();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("You entered invalid email or password");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
