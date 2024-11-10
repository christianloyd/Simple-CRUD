using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CRUD
{
    public partial class StudentForm : Form
    {
        public string dbconnect()
        {
            return "server=localhost;port=3307;database=crud;uid=root;pwd=;";
        }

        private FormStudentInfo _parentForm;

        public StudentForm(FormStudentInfo parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtfname.Text) ||
                    string.IsNullOrEmpty(txtmname.Text) ||
                    string.IsNullOrEmpty(txtlname.Text) ||
                    string.IsNullOrEmpty(txtcourse.Text) ||
                    string.IsNullOrEmpty(txtemail.Text) ||
                    string.IsNullOrEmpty(txtaddress.Text))
                {
                    MessageBox.Show("All fields must be filled in.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (MySqlConnection conn = new MySqlConnection(dbconnect()))
                {
                    conn.Open();
                    string insertQuery = @"INSERT INTO student_table (fname, mname, lname, course, email, address) 
                                 VALUES (@fname, @mname, @lname, @course, @email, @address)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@fname", txtfname.Text.Trim());
                        cmd.Parameters.AddWithValue("@mname", txtmname.Text.Trim());
                        cmd.Parameters.AddWithValue("@lname", txtlname.Text.Trim());
                        cmd.Parameters.AddWithValue("@course", txtcourse.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtemail.Text.Trim());
                        cmd.Parameters.AddWithValue("@address", txtaddress.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record saved successfully!");

                        _parentForm.RefreshData();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtfname.Clear();
            txtmname.Clear();
            txtlname.Clear();
            txtcourse.Clear();
            txtemail.Clear();
            txtaddress.Clear();
            txtfname.Focus();
        }

        private void btnView_Click_1(object sender, EventArgs e)
        {
            FormStudentInfo form = new FormStudentInfo();
            form.ShowDialog();
        }
     

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnCombo.SelectedItem != null)
            {
                string selectedYearLevel = btnCombo.SelectedItem.ToString();

            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker.Value;
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "MM dd yyyy";

        }
    }
}
