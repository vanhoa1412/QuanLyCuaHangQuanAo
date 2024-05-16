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


namespace QuanLyCuaHangQuanAo.SHOP
{
    public partial class FormMain : Form
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=QUANLYSHOP;Integrated Security=True;";

        public string name = "(?)";
        public string type = "{?}";
        public FormMain()
        {
            InitializeComponent();
            DisplayRole();
        }
        public void DisplayRole()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT Users_Type FROM Users WHERE Users_Name = @username", connection))
                {
                    command1.Parameters.AddWithValue("@username", name);
                    using (var reader = command1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string role = reader.GetString(0);
                            lblUserType.Text = role;
                            SetPermissions(role);
                        }
                        
                    }
                }
            }
        }

        private void SetPermissions(string role)
        {
            switch (role)
            {
                case "Admin":
                    break;
                case "Manager":
                    btnAccont.Enabled = false;
                    break;
                case "User":
                    btnAccont.Enabled = false;
                    break;
                default:
                    btnMain.Enabled = false;
                    btnCloth.Enabled = false;
                    btnBrandCategory.Enabled = false;
                    btnBill.Enabled = false;
                    btnAccont.Enabled = false;
                    break;
            }
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có thật sự muốn thoát chương trình không?", "Thông Báo",MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            lblUsername.Text = name;
            DisplayRole();
        }
        private void btnMain_Click(object sender, EventArgs e)
        {
            if (!btnMain.Enabled)
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này.", "Thông Báo", MessageBoxButtons.OK);
                return;
            }
            userHome1.Visible = true;
            userHome1.DisplayHome();
            userCloth1.Visible = false;
            userControlBrandCategory1.Visible = false;
            userBill1.Visible = false;
            userUser1.Visible = false;
        }
        private void btnCloth_Click(object sender, EventArgs e)
        {
            if (!btnCloth.Enabled)
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này.", "Thông Báo", MessageBoxButtons.OK);
                return;
            }
            userHome1.Visible = false;
            userCloth1.Visible = true;
            userCloth1.EmptyBox();
            userControlBrandCategory1.Visible = false;
            userBill1.Visible = false;
            userUser1.Visible = false;
        }
        private void btnBrandCategory_Click(object sender, EventArgs e)
        {
            if (!btnBrandCategory.Enabled)
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này.", "Thông Báo", MessageBoxButtons.OK);
                return;
            }
            userHome1.Visible = false;
            userCloth1.Visible = false;
            userControlBrandCategory1.Visible = true;
            userControlBrandCategory1.EmptyBox();
            userBill1.Visible = false;
            userUser1.Visible = false;
        }
        private void btnBill_Click(object sender, EventArgs e)
        {
            if (!btnBill.Enabled)
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này.", "Thông Báo", MessageBoxButtons.OK);
                return;
            }
            userHome1.Visible = false;
            userCloth1.Visible = false;
            userControlBrandCategory1.Visible = false;
            userUser1.Visible = false;
            userBill1.Visible = true;
            userBill1.EmptyBox();
        }
        private void btnAccont_Click(object sender, EventArgs e)
        {
            if (!btnAccont.Enabled)
            {
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này.", "Thông Báo", MessageBoxButtons.OK);
                return;
            }
            userHome1.Visible = false;
            userCloth1.Visible = false;
            userControlBrandCategory1.Visible = false;
            userBill1.Visible = false;
            userUser1.Visible = true;
            userUser1.EmptyBox();
        }
    }
}
