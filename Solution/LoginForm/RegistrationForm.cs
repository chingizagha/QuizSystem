using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;


namespace LoginForm
{
    public partial class RegistrationForm : Form
    {
        static string conString = @"Data Source=DESKTOP-82S0U2V;Initial Catalog=QuizSistem;User ID=sa;Password=murad123";
        SqlConnection con = new SqlConnection(conString);
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt = new DataTable();

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            LoginForm ff = new LoginForm();
            this.Close();
            ff.Show();
        }

        private void add(string name, string surname, string email, string password, string usertype)
        {
            String sql = "INSERT INTO [User](Name,Surname,Email,Password,UserType) VALUES(@NAME,@SURNAME,@EMAIL,@PASSWORD,@USERTYPE)";
            cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@NAME", name);
            cmd.Parameters.AddWithValue("@SURNAME", surname);
            cmd.Parameters.AddWithValue("@EMAIL", email);
            cmd.Parameters.AddWithValue("@PASSWORD", password);
            cmd.Parameters.AddWithValue("@USERTYPE", usertype);
            try
            {
                con.Open();

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Inserted", "Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            int minLength = 8;
            int maxLength = 10;

            string charAvailable = "abcdefghijklmnopqrstuvwxyz0123456789";

            StringBuilder password = new StringBuilder();
            Random rdm = new Random();

            int passwordLength = rdm.Next(minLength, maxLength + 1);

            while (passwordLength-- > 0)
            {
                password.Append(charAvailable[rdm.Next(charAvailable.Length)]);
            }
            
            string name = (txtName.Text).ToString();
            string surname = (txtSurname).Text.ToString();
            
            string to, from, pass, mail;
            to = (txtReceiver.Text).ToString();
            from = "quizsystem1@gmail.com";
            mail = $"Hi, {name} {surname}. Your Password is {password}";
            pass = "SAlam%2002";
            MailMessage message = new MailMessage();
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = mail;
            message.Subject = "Quiz System Registration";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);
            try
            {
                smtp.Send(message);
                MessageBox.Show("Email send successfully", "Email", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            add(txtName.Text, txtSurname.Text, txtReceiver.Text, password.ToString(), comboBoxType.SelectedItem.ToString());

        }
    }
}
