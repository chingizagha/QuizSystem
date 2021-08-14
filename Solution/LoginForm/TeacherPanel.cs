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



    public partial class TeacherPanel : Form
    {
        static string conString = @"Data Source=DESKTOP-82S0U2V;Initial Catalog=QuizSistem;User ID=sa;Password=murad123";
        SqlConnection con = new SqlConnection(conString);
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt = new DataTable();
        public TeacherPanel()
        {
            InitializeComponent();
        }
        private void TeacherPanel_Load(object sender, EventArgs e)
        {

            textBox3.Text = LoginForm.passingText;
            string sql = "select * from [User] where Email ='" + textBox3.Text + "' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;
            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    int id = myreader.GetInt32(0);
                    string name = myreader.GetString(1);
                    string surname = myreader.GetString(2);
                    string email = myreader.GetString(3);
                    string password = myreader.GetString(4);


                    textBox1.Text = name;
                    textBox2.Text = surname;
                    textBox4.Text = password;
                    textBox5.Text = id.ToString();

                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            labelTeacherName.Text = "Welcome, " + textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("UPDATE [User] SET Name =@Name, Surname=@Surname, Email=@Email, Password=@Password WHERE ID ='" + textBox5.Text + "'", con);
            sqlCommand.Parameters.AddWithValue("@Name", textBox1.Text);
            sqlCommand.Parameters.AddWithValue("@SurName", textBox2.Text);
            sqlCommand.Parameters.AddWithValue("@Email", textBox3.Text);
            sqlCommand.Parameters.AddWithValue("@Password", textBox4.Text);
            try
            {
                con.Open();
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Changed");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Close();
            LoginForm ff = new LoginForm();
            ff.Show();
        }
    }
}
