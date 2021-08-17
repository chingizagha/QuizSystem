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
    public partial class AdminPanel : Form
    {
        int IDQuiz = 0;
        int ID = 0;
        int IDTeacher = 0;

        static string conString = @"Data Source=DESKTOP-82S0U2V;Initial Catalog=QuizSistem;User ID=sa;Password=murad123";
        SqlConnection con = new SqlConnection(conString);
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt = new DataTable();
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDataList();
            dataGridView2.DataSource = GetDataListTeacher();
            dataGridView3.DataSource = GetDataListQuiz();

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
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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

        public void hidePanels()
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            hidePanels();
            panel2.Visible = true;
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            hidePanels();
            panel3.Visible = true;
        }

        private void btnTeachers_Click(object sender, EventArgs e)
        {
            hidePanels();
            panel4.Visible = true;
        }

        private void btnQuiz_Click(object sender, EventArgs e)
        {
            hidePanels();
            panel5.Visible = true;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm ff = new LoginForm();
            ff.Show();
        }

        private DataTable GetDataList()
        {
            DataTable dtQuiz = new DataTable();

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ID,Name,Surname,Email,Password FROM [User] Where UserType = 'Student'", con))
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    dtQuiz.Load(reader);
                }
            }
            return dtQuiz;
        }
        private void ClearData()
        {
            txtDataName.Text = "";
            txtDataSurname.Text = "";
            txtDataEmail.Text = "";
            txtDataPassword.Text = "";
            ID = 0;
        }

        private void btnDeleteData_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                cmd = new SqlCommand("delete [User] where ID=@ID", con);
                con.Open();
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                dataGridView1.DataSource = GetDataList();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void btnUpdateData_Click(object sender, EventArgs e)
        {
            if (txtDataName.Text != "" && txtDataSurname.Text != "" && txtDataEmail.Text != "" && txtDataPassword.Text != "")
            {
                cmd = new SqlCommand("UPDATE [User] set Name=@NAME,Surname=@SURNAME,Email=@EMAIL,Password=@PASSWORD where ID=@ID", con);
                con.Open();
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@NAME", txtDataName.Text);
                cmd.Parameters.AddWithValue("@SURNAME", txtDataSurname.Text);
                cmd.Parameters.AddWithValue("@EMAIL", txtDataEmail.Text);
                cmd.Parameters.AddWithValue("@PASSWORD", txtDataPassword.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                con.Close();
                dataGridView1.DataSource = GetDataList();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtDataName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDataSurname.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtDataEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtDataPassword.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }


        //===================================================

        private DataTable GetDataListTeacher()
        {
            DataTable dtQuiz = new DataTable();

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ID,Name,Surname,Email,Password FROM [User] Where UserType = 'Teacher '", con))
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    dtQuiz.Load(reader);
                }
            }
            return dtQuiz;
        }

        private void ClearDataTeacher()
        {
            txtTeacherName.Text = "";
            txtTeacherSurname.Text = "";
            txtTeacherEmail.Text = "";
            txtTeacherPassword.Text = "";
            IDTeacher = 0;
        }

        private void dataGridView2_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            IDTeacher = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtTeacherName.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtTeacherSurname.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtTeacherEmail.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtTeacherPassword.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnTeacherUpdate_Click_1(object sender, EventArgs e)
        {
            if (txtTeacherName.Text != "" && txtTeacherSurname.Text != "" && txtTeacherEmail.Text != "" && txtTeacherPassword.Text != "")
            {
                cmd = new SqlCommand("UPDATE [User] set Name=@NAME1,Surname=@SURNAME1,Email=@EMAIL1,Password=@PASSWORD1 where ID=@IDTeacher", con);
                con.Open();
                cmd.Parameters.AddWithValue("@IDTeacher", IDTeacher);
                cmd.Parameters.AddWithValue("@NAME1", txtTeacherName.Text);
                cmd.Parameters.AddWithValue("@SURNAME1", txtTeacherSurname.Text);
                cmd.Parameters.AddWithValue("@EMAIL1", txtTeacherEmail.Text);
                cmd.Parameters.AddWithValue("@PASSWORD1", txtTeacherPassword.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                con.Close();
                dataGridView2.DataSource = GetDataListTeacher();
                ClearDataTeacher();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        private void btnTeacherDelete_Click_1(object sender, EventArgs e)
        {
            if (IDTeacher != 0)
            {
                cmd = new SqlCommand("DELETE [User] WHERE ID=@IDTeacher", con);
                con.Open();
                cmd.Parameters.AddWithValue("@IDTeacher", IDTeacher);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                dataGridView2.DataSource = GetDataListTeacher();
                ClearDataTeacher();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        //===================================================

        private DataTable GetDataListQuiz()
        {
            DataTable dtQuiz = new DataTable();

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ID,Title,Question,A,B,C,D,CorrectAnswer FROM [Quiz]", con))
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    dtQuiz.Load(reader);
                }
            }
            return dtQuiz;
        }

        private void ClearDataQuiz()
        {
            txtTitle.Text = "";
            txtQuestion.Text = "";
            txtA.Text = "";
            txtB.Text = "";
            txtC.Text = "";
            txtD.Text = "";
            txtCorrectAnswer.Text = "";
            IDQuiz = 0;
        }

        private void dataGridView3_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            IDQuiz = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtTitle.Text = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtQuestion.Text = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtA.Text = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtB.Text = dataGridView3.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtC.Text = dataGridView3.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtD.Text = dataGridView3.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtCorrectAnswer.Text = dataGridView3.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text != "" && txtQuestion.Text != "" && txtA.Text != "" && txtB.Text != "" && txtC.Text != "" && txtD.Text != "" && txtCorrectAnswer.Text != "")
            {
                cmd = new SqlCommand("UPDATE [Quiz] set Title=@TITLE,Question=@QUESTION,A=@A,B=@B,C=@C,D=@D,CorrectAnswer=@CORRECTANSWER where ID=@IDQUIZ", con);
                con.Open();
                cmd.Parameters.AddWithValue("@IDQUIZ", IDQuiz);
                cmd.Parameters.AddWithValue("@TITLE", txtTitle.Text);
                cmd.Parameters.AddWithValue("@QUESTION", txtQuestion.Text);
                cmd.Parameters.AddWithValue("@A", txtA.Text);
                cmd.Parameters.AddWithValue("@B", txtB.Text);
                cmd.Parameters.AddWithValue("@C", txtC.Text);
                cmd.Parameters.AddWithValue("@D", txtD.Text);
                cmd.Parameters.AddWithValue("@CORRECTANSWER", txtCorrectAnswer.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                con.Close();
                dataGridView3.DataSource = GetDataListQuiz();
                ClearDataQuiz();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IDTeacher != 0)
            {
                cmd = new SqlCommand("DELETE [Quiz] WHERE ID=@IDQuiz", con);
                con.Open();
                cmd.Parameters.AddWithValue("@IDQuiz", IDQuiz);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                dataGridView3.DataSource = GetDataListQuiz();
                ClearDataQuiz();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text != "" && txtQuestion.Text != "" && txtA.Text != "" && txtB.Text != "" && txtC.Text != "" && txtD.Text != "" && txtCorrectAnswer.Text != "")
            {
                cmd = new SqlCommand("INSERT INTO  [Quiz](Title,Question,A,B,C,D,CorrectAnswer) values(@TITLE,@QUESTION,@A,@B,@C,@D,@CORRECTANSWER)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@TITLE", txtTitle.Text);
                cmd.Parameters.AddWithValue("@QUESTION", txtQuestion.Text);
                cmd.Parameters.AddWithValue("@A", txtA.Text);
                cmd.Parameters.AddWithValue("@B", txtB.Text);
                cmd.Parameters.AddWithValue("@C", txtC.Text);
                cmd.Parameters.AddWithValue("@D", txtD.Text);
                cmd.Parameters.AddWithValue("@CORRECTANSWER", txtCorrectAnswer.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                dataGridView3.DataSource = GetDataListQuiz();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }
    }
}
