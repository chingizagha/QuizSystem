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
    public partial class StudentPanel : Form
    {
        public static string passingCmbText;

        static string conString = @"Data Source=DESKTOP-82S0U2V;Initial Catalog=QuizSistem;User ID=sa;Password=murad123";
        SqlConnection con = new SqlConnection(conString);
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt = new DataTable();
        public StudentPanel()
        {
            InitializeComponent();
        }

        private void StudentPanel_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDataList();
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
            labelStudentName.Text = textBox1.Text;


            

            string Sql = "SELECT [Title] FROM [Quiz] GROUP BY [Title]";
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            SqlCommand cmd1 = new SqlCommand(Sql, conn);
            SqlDataReader DR = cmd1.ExecuteReader();
            

            while (DR.Read())
            {
                cmbQuizTitle.Items.Add(DR[0]);

            }
            conn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("UPDATE [User] SET Password=@Password WHERE ID ='" + textBox5.Text + "'", con);
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

        public void hidePanels()
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            hidePanels();
            panel2.Visible = true;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            LoginForm ff = new LoginForm();
            this.Close();
            ff.Show();
        }

        private void btnSelectQuiz_Click(object sender, EventArgs e)
        {
            hidePanels();
            panel3.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            passingCmbText = cmbQuizTitle.SelectedItem.ToString();
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to start?", "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                WorkForm ff = new WorkForm();
                this.Hide();
                ff.Show();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private DataTable GetDataList()
        {
            DataTable dtQuiz = new DataTable();

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Title,CorrectAnswer,WrongAnswer FROM [QuizAnswer] Where EMAIL = '"+ LoginForm.passingText +"' ", con))
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    dtQuiz.Load(reader);
                }
            }
            return dtQuiz;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDataList();
            hidePanels();
            panel4.Visible = true;
        }
    }
}
