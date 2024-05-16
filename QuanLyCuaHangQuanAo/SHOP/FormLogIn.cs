using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace QuanLyCuaHangQuanAo.SHOP
{


    public partial class FormLogIn : Form
    {

        private string connectionString = "Data Source=localhost;Initial Catalog=QUANLYSHOP;Integrated Security=True;";
        public FormLogIn()
        {
            InitializeComponent();
        }

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


        private void EmptyBox()
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }
        private void FormLogIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có thật sự muốn thoát chương trình không?", "Thông Báo",MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            {
                Close();
            }
        }

        private void picShow_Click(object sender, EventArgs e)
        {
            if (picShow.Visible == true)
            {
                txtPassword.UseSystemPasswordChar = false;
                picShow.Visible = false;
                picHide.Visible = true;
            }
        }

        private void picHide_Click(object sender, EventArgs e)
        {
            if (picHide.Visible == true)
            {
                txtPassword.UseSystemPasswordChar = true;
                picShow.Visible = true;
                picHide.Visible = false;
            }
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "Admin" && txtPassword.Text.Trim() == "1")
            {
                FormMain formMain = new FormMain();
                formMain.ShowDialog();
            }
            else if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập Tên Đăng Nhập.", "Thông Báo", MessageBoxButtons.OKCancel);
                return;
            }
            else if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập Mật Khẩu.", "Thông Báo", MessageBoxButtons.OKCancel);
                return;
            }
            else
            {
                //string password = createPass(txtPassword.Text);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Users_Name = @username AND Users_Password = @password", connection))
                    {

                        command.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                        command.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                        int userCount = (int)command.ExecuteScalar();

                        if (userCount > 0)
                        {
                            FormMain formMain = new FormMain();
                            formMain.name = txtUsername.Text;
                            formMain.ShowDialog();
                            EmptyBox();
                        }
                        else
                        {

                            MessageBox.Show("Sai Tên Đăng Nhập hoặc Mật Khẩu.", "Thông Báo", MessageBoxButtons.OKCancel);
                            return;
                        }
                    }
                }
            }
        }
    }
}
