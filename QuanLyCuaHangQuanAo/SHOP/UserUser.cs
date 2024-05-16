using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangQuanAo.SHOP
{
    public partial class UserUser : UserControl
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=QUANLYSHOP;Integrated Security=True;";
        private string Id = "";

        private string createPass(String pa)
        {
            MD5 md = MD5.Create();
            byte[] inputString = System.Text.Encoding.ASCII.GetBytes(pa);
            byte[] hash = md.ComputeHash(inputString);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public UserUser()
        {
            InitializeComponent();
        }
        public void EmptyBox()
        {
            txtUserName.Clear();
            txtUserEmail.Clear();
            txtUserPassword.Clear();
            cmbType.SelectedIndex = 0;
        }
        public void DisplayUser()
        {
            dgvUser.Columns[0].Visible = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Users", connection))
                {
                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvUser.DataSource = dataTable;
                    }
                    lblTotalUser.Text = dgvUser.Rows.Count.ToString();
                }
            }
        }

        private void picSearch_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picSearch, "Search");
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            
            if (txtUserName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtUserEmail.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập Email.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtUserPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbType.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn chức vụ.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //String pass = "";
                    //pass = txtUserPassword.Text;
                    //String newPass = createPass(pass);

                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Users;", connection))
                    using (SqlCommand command1 = new SqlCommand("INSERT INTO Users  (Users_Name,Users_Email,Users_Password,Users_Type)" +
                        "OUTPUT inserted.Users_Id VALUES (@Users_Name,@Users_Email,@Users_Password,@Users_Type);", connection))
                    {
                        command1.Parameters.AddWithValue("@Users_Name", txtUserName.Text.Trim());
                        command1.Parameters.AddWithValue("@Users_Email", txtUserEmail.Text.Trim());
                        command1.Parameters.AddWithValue("@Users_Password", txtUserPassword.Text.Trim());
                        command1.Parameters.AddWithValue("@Users_Type", cmbType.SelectedItem.ToString());

                        command1.ExecuteNonQuery();
                        EmptyBox();
                        DisplayUser();
                    }
                }
            }
        }

        private void txtSearchUserName_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Users WHERE Users_Name LIKE '%" + txtSearchUserName.Text + "%';", connection))
                {

                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvUser.DataSource = dataTable;
                    }
                    lblTotalUser.Text = dgvUser.Rows.Count.ToString();
                }
            }
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvUser.Rows[e.RowIndex];
                Id = row.Cells[0].Value.ToString();
                txtUserName.Text = row.Cells[1].Value.ToString();
                txtUserEmail.Text = row.Cells[2].Value.ToString();
                txtUserPassword.Text = row.Cells[3].Value.ToString();
                cmbType.SelectedItem = row.Cells[4].Value.ToString();

            }
        }

        private void btnChangeUser_Click(object sender, EventArgs e)
        {
            if (Id == "")
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần thao tác.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtUserName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtUserEmail.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập Email.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtUserPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbType.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn chức vụ.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command1 = new SqlCommand("UPDATE Users SET Users_Name = @Users_Name where Users_Id = @Users_Id and not exists (select * from Users where Users_Name = @Users_Name)", connection))
                        using (SqlCommand command2 = new SqlCommand("UPDATE Users SET Users_Email = @Users_Email where Users_Id = @Users_Id", connection))
                        using (SqlCommand command3 = new SqlCommand("UPDATE Users SET Users_Password = @Users_Password where Users_Id = @Users_Id", connection))
                        using (SqlCommand command4 = new SqlCommand("UPDATE Users SET Users_Type = @Users_Type where Users_Id = @Users_Id", connection))
                        {

                            command1.Parameters.AddWithValue("@Users_Name", txtUserName.Text.Trim());
                            command1.Parameters.AddWithValue("@Users_Id", Id);
                            command2.Parameters.AddWithValue("@Users_Email", txtUserEmail.Text.Trim());
                            command2.Parameters.AddWithValue("@Users_Id", Id);
                            command3.Parameters.AddWithValue("@Users_Password", txtUserName.Text.Trim());
                            command3.Parameters.AddWithValue("@Users_Id", Id);
                            command4.Parameters.AddWithValue("@Users_Type", cmbType.SelectedItem.ToString());
                            command4.Parameters.AddWithValue("@Users_Id", Id);

                            command1.ExecuteNonQuery();
                            command2.ExecuteNonQuery();
                            command3.ExecuteNonQuery();
                            command4.ExecuteNonQuery();
                            EmptyBox();
                            DisplayUser();
                        }
                    }
                }
            }
        }

        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có thật sự muốn xóa tài khoản này.", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command1 = new SqlCommand("DELETE FROM Users WHERE Users_Id = @Usersid", connection))
                    {

                        command1.Parameters.AddWithValue("@Usersid", Id);
                        int rowsAffected = command1.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            Console.WriteLine($"Đã xóa hàng thứ {Id} ra khỏi bản thành công.");
                        else
                            Console.WriteLine($"Không tồn tại hàng thứ {Id}.");
                        EmptyBox();
                        DisplayUser();
                    }
                }
            }
        }

        private void UserUser_Enter(object sender, EventArgs e)
        {
            EmptyBox();
            DisplayUser();
        }
    }
}

