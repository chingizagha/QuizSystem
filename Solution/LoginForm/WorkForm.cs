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
    public partial class WorkForm : Form
    {
        string correct1;
        Int32 count;
        int i = 0;
        int correctAns = 0;
        int wrongAns = 0;
        static string conString = @"Data Source=DESKTOP-82S0U2V;Initial Catalog=QuizSistem;User ID=sa;Password=murad123";
        SqlConnection con = new SqlConnection(conString);
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt = new DataTable();
        public WorkForm()
        {
            InitializeComponent();
        }
        private void WorkForm_Load(object sender, EventArgs e)
        {
            i++;
            getNextQuestion();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (radioButton1.Text == correct1)
                {
                    correctAns++;

                }
            }
            else if (radioButton2.Checked)
            {
                if (radioButton2.Text == correct1)
                {
                    correctAns++;
                }
            }
            else if (radioButton3.Checked)
            {
                if (radioButton3.Text == correct1)
                {
                    correctAns++;
                }
            }
            else if (radioButton4.Checked)
            {
                if (radioButton4.Text == correct1)
                {
                    correctAns++;
                    
                }
            }
            i++;
            getNextQuestion();


            if (i > count)
            {
                lblQuiz.Visible = false;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                btnNext.Visible = false;
                lblCorrect.Visible = true;
                lblWrong.Visible = true;
                btnEnd.Visible = true;
                lblCorrect.Text = $"Correct Answer: {correctAns}";
                lblWrong.Text = $"Wrong Answer: {count-correctAns}";
            }
            
        }

        public void getNextQuestion()
        {
            con.Open();
            SqlCommand comm = new SqlCommand("SELECT COUNT(Question) FROM [Quiz] WHERE [Title]='" + StudentPanel.passingCmbText + "'", con);
            count = (Int32)comm.ExecuteScalar();
            con.Close();

            string sql = $"SELECT Top {i} Question,A,B,C,D,CorrectAnswer FROM [Quiz] WHERE [Title]='" + StudentPanel.passingCmbText + "' ";
            
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader myreader;

            try
            {
                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string question = myreader.GetString(0);
                    string a = myreader.GetString(1);
                    string b = myreader.GetString(2);
                    string c = myreader.GetString(3);
                    string d = myreader.GetString(4);
                    string correct = myreader.GetString(5);
                    correct1 = correct;


                    lblQuiz.Text = question;
                    radioButton1.Text = a;
                    radioButton2.Text = b;
                    radioButton3.Text = c;
                    radioButton4.Text = d;

                }
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void add(string email, int correctAnswer, int wrongAnswer, string title)
        {
            String sql = "INSERT INTO [QuizAnswer](Email,CorrectAnswer,WrongAnswer,Title) VALUES(@EMAIL,@CORRECTANSWER,@WRONGANSWER,@TITLE)";
            cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@EMAIL", email);
            cmd.Parameters.AddWithValue("@CORRECTANSWER", correctAnswer);
            cmd.Parameters.AddWithValue("@WRONGANSWER", wrongAnswer);
            cmd.Parameters.AddWithValue("@TITLE", title);
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

        private void btnEnd_Click(object sender, EventArgs e)
        {
            add(LoginForm.passingText, correctAns, count - correctAns, StudentPanel.passingCmbText); 
            StudentPanel ff = new StudentPanel();
            this.Close();
            ff.Show();

        }
    }
}
