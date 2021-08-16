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
            labelTeacherName.Text = textBox1.Text;
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

        private void button2_Click(object sender, EventArgs e)
        {
            AddTestForm ff = new AddTestForm();
            this.Hide();
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
        private void btnAddQuiz_Click(object sender, EventArgs e)
        {
            hidePanels();
            panel3.Visible = true;
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            hidePanels();
            panel4.Visible = true;
            dataGridView1.DataSource = GetDataList();
        }
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm ff = new LoginForm();
            ff.Show();
        }


        private void add1(string title,string questionName, string optionA, string optionB, string optionC, string optionD, string correctAnswer)
        {
            String sql = "INSERT INTO [Quiz](Title,Question,A,B,C,D,CorrectAnswer) VALUES(@TITLE,@QUESTIONNAME,@A,@B,@C,@D,@CORRECTANSWER)";
            cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@TITLE", title);
            cmd.Parameters.AddWithValue("@QUESTIONNAME", questionName);
            cmd.Parameters.AddWithValue("@A", optionA);
            cmd.Parameters.AddWithValue("@B", optionB);
            cmd.Parameters.AddWithValue("@C", optionC);
            cmd.Parameters.AddWithValue("@D", optionD);
            cmd.Parameters.AddWithValue("@CORRECTANSWER", correctAnswer);

            try
            {
                con.Open();

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Inserted");

                    txtQuestion.Text = "";
                    txtA.Text = "";
                    txtB.Text = "";
                    txtC.Text = "";
                    txtD.Text = "";
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void add2(string title, string questionName, string optionA, string optionB, string optionC, string optionD, string correctAnswer)
        {
            String sql = "INSERT INTO [Quiz](Title,Question,A,B,C,D,CorrectAnswer) VALUES(@Title,@QUESTIONNAME,@A,@B,@C,@D,@CORRECTANSWER)";
            cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@TITLE", title);
            cmd.Parameters.AddWithValue("@QUESTIONNAME", questionName);
            cmd.Parameters.AddWithValue("@A", optionA);
            cmd.Parameters.AddWithValue("@B", optionB);
            cmd.Parameters.AddWithValue("@C", optionC);
            cmd.Parameters.AddWithValue("@D", optionD);
            cmd.Parameters.AddWithValue("@CORRECTANSWER", correctAnswer);

            try
            {
                con.Open();

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Inserted");

                    txtTitle.Text = "";
                    txtQuestion.Text = "";
                    txtA.Text = "";
                    txtB.Text = "";
                    txtC.Text = "";
                    txtD.Text = "";
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            string answer="";
            foreach (RadioButton item in groupBoxAnswer.Controls)
            {
                if (item.Checked)
                {
                    answer += item.Text + " ";
                }
            }
            add1(txtTitle.Text,txtQuestion.Text, txtA.Text, txtB.Text, txtC.Text, txtD.Text, answer);
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            string answer = "";
            foreach (RadioButton item in groupBoxAnswer.Controls)
            {
                if (item.Checked)
                {
                    if(item.Text == "radioButton1")
                    {
                        answer += txtA.Text;
                    }
                    else if (item.Text == "radioButton2")
                    {
                        answer += txtB.Text;
                    }
                    else if (item.Text == "radioButton3")
                    {
                        answer += txtC.Text;
                    }
                    else if (item.Text == "radioButton4")
                    {
                        answer += txtD.Text;
                    }
                }
            }
            add2(txtTitle.Text, txtQuestion.Text, txtA.Text, txtB.Text, txtC.Text, txtD.Text, answer);
        }

        private DataTable GetDataList() 
        {
            DataTable dtQuiz = new DataTable();

            using (SqlConnection con = new SqlConnection(conString))
            {
                using(SqlCommand cmd = new SqlCommand("SELECT [Title],Count(Question) as 'Number of questions' FROM [Quiz] GROUP BY Title", con))
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    dtQuiz.Load(reader);
                }
            }
            return dtQuiz;
        }
    }
}
