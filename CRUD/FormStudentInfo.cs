using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CRUD
{
    public partial class FormStudentInfo : Form
    {
        private readonly string connectionString = "server=localhost;port=3307;database=crud;uid=root;pwd=;Allow User Variables=True;";

        public FormStudentInfo()
        {
            InitializeComponent();
            LoadData();
            FormatDataGridView();
        }

        private void LoadData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string selectQuery = "SELECT * FROM student_table";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, conn))
                    {
                        DataTable tbl = new DataTable();
                        adapter.Fill(tbl);

                         
                        dataGridView1.AutoGenerateColumns = false;

                        
                        dataGridView1.Columns["StudentID"].DataPropertyName = "stid";
                        dataGridView1.Columns["Firstname"].DataPropertyName = "fname";
                        dataGridView1.Columns["Middlename"].DataPropertyName = "mname";
                        dataGridView1.Columns["Lastname"].DataPropertyName = "lname";
                        dataGridView1.Columns["Course"].DataPropertyName = "course";
                        dataGridView1.Columns["Email"].DataPropertyName = "email";
                        dataGridView1.Columns["Address"].DataPropertyName = "address";

                        dataGridView1.DataSource = tbl;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            dataGridView1.AutoResizeColumns();
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }

        public void RefreshData()
        {
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this student?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string stid = dataGridView1.SelectedRows[0].Cells["StudentID"].Value.ToString();

                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            string deleteQuery = "DELETE FROM student_table WHERE stid = @stid";
                            MySqlCommand cmd = new MySqlCommand(deleteQuery, conn);
                            cmd.Parameters.AddWithValue("@stid", stid);
                            cmd.ExecuteNonQuery();
                        }

                        RefreshData();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                    int studentId = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["StudentID"].Value);
                    string fname = dataGridView1.Rows[selectedRowIndex].Cells["Firstname"].Value.ToString();
                    string mname = dataGridView1.Rows[selectedRowIndex].Cells["Middlename"].Value.ToString();
                    string lname = dataGridView1.Rows[selectedRowIndex].Cells["Lastname"].Value.ToString();
                    string course = dataGridView1.Rows[selectedRowIndex].Cells["Course"].Value.ToString();
                    string email = dataGridView1.Rows[selectedRowIndex].Cells["Email"].Value.ToString();
                    string address = dataGridView1.Rows[selectedRowIndex].Cells["Address"].Value.ToString();

                    
                    UpdateStudent updateStudentForm = new UpdateStudent(studentId, fname, mname, lname, course, email, address);
                    updateStudentForm.ShowDialog();
                    RefreshData();  
                }
                else
                {
                    MessageBox.Show("Please select a row to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
             
            StudentForm form = new StudentForm(this);
            form.ShowDialog();
            RefreshData();
        }

        private void btnlgt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
        }
    }
}