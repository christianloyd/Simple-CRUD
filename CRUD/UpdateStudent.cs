using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace CRUD
{
    public partial class UpdateStudent : Form
    {
        private object studentId;
        private string fname;
        private string mname;
        private string lname;
        private string course;
        private string email;
        private string address;

        public UpdateStudent(int studentId, string fname, string mname, string lname, string course, string email, string address)
        {
            InitializeComponent();
            this.studentId = studentId;
            this.fname = fname;
            this.mname = mname;
            this.lname = lname;
            this.course = course;
            this.email = email;
            this.address = address;

            // Populate the UI components
            txtfirstname.Text = fname;
            txtmiddlename.Text = mname;
            txtlastname.Text = lname;
            txtCourse.Text = course;
            txtEmail.Text = email;
            txtAddress.Text = address;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("server=localhost;port=3307;database=crud;uid=root;pwd=;"))
                {
                    conn.Open();
                    string updateQuery = @"UPDATE student_table 
                                   SET fname = @firstName, mname = @middleName, lname = @lastName, 
                                       course = @course, email = @email, address = @address 
                                   WHERE stid = @studentId";

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@firstName", txtfirstname.Text.Trim());
                        cmd.Parameters.AddWithValue("@middleName", txtmiddlename.Text.Trim());
                        cmd.Parameters.AddWithValue("@lastName", txtlastname.Text.Trim());
                        cmd.Parameters.AddWithValue("@course", txtCourse.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@studentId", studentId);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record updated successfully!");

                        this.Close();  
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}