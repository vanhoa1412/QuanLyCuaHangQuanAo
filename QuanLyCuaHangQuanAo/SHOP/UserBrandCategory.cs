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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyCuaHangQuanAo.SHOP
{
    public partial class UserControlBrandCategory : UserControl
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=QUANLYSHOP;Integrated Security=True;";
        private string Id = "";
        private string id = "";

        public UserControlBrandCategory()
        {
            InitializeComponent();
        }

        public void EmptyBox()
        {
            txtCategoryName.Clear();
            cmbStatus.SelectedIndex = 0;
        }
        public void DisplayCategory()
        {
            dgvCategory.Columns[0].Visible = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Category", connection))
                {
                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvCategory.DataSource = dataTable;
                    }
                    lblTotalCategory.Text = dgvCategory.Rows.Count.ToString();
                }
            }
        }

        private void picSearch_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picSearch, "Search");
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập thể loại.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng nhập tình trạng.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Category;", connection))
                    using (SqlCommand command1 = new SqlCommand("INSERT INTO Category  (Category_Name,Category_Status)" +
                        "OUTPUT inserted.Category_Id VALUES (@Category_Name,@Category_Status);", connection))
                    {
                        command1.Parameters.AddWithValue("@Category_Name", txtCategoryName.Text.Trim());
                        command1.Parameters.AddWithValue("@Category_Status", cmbStatus.SelectedItem.ToString());

                        command1.ExecuteNonQuery();
                        EmptyBox();
                        DisplayCategory();
                    }
                }
            }
        }

        private void tpCategory_Enter(object sender, EventArgs e)
        {
            EmptyBox();
            DisplayCategory();
        }

        private void txtSearchCategoryName_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Category WHERE Category_Name LIKE '%" + txtSearchCategoryName.Text + "%';", connection))
                {

                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvCategory.DataSource = dataTable;
                    }
                    lblTotalCategory.Text = dgvCategory.Rows.Count.ToString();
                }
            }
        }

        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvCategory.Rows[e.RowIndex];
                Id = row.Cells[0].Value.ToString();
                txtCategoryName.Text = row.Cells[1].Value.ToString();
                cmbStatus.SelectedItem = row.Cells[2].Value.ToString();

            }
        }

        private void btnChangeCategory_Click(object sender, EventArgs e)
        {
            if (Id == "")
            {
                MessageBox.Show("Vui lòng chọn hàng bạn muố thay đổi.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtCategoryName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng nhập tình trạng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command1 = new SqlCommand("UPDATE Category SET Category_Name = @Category_Name where Category_Id = @Category_Id and not exists (select * from Category where Category_Name = @Category_Name)", connection))
                    using (SqlCommand command2 = new SqlCommand("UPDATE Category SET Category_Status = @Category_Status where Category_Id = @Category_Id", connection))
                    {

                        command1.Parameters.AddWithValue("@Category_Name", txtCategoryName.Text.Trim());
                        command1.Parameters.AddWithValue("@Category_Id", Id);
                        command2.Parameters.AddWithValue("@Category_Status", cmbStatus.SelectedItem.ToString());
                        command2.Parameters.AddWithValue("@Category_Id", Id);

                        command1.ExecuteNonQuery();
                        command2.ExecuteNonQuery();
                        EmptyBox();
                        DisplayCategory();
                    }
                }
            }

        }

        private void btnRemoveCategory_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có thật sự muốn xóa Thể loại này ?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command1 = new SqlCommand("DELETE FROM Category WHERE Category_Id = @categoryid", connection))
                    {

                        command1.Parameters.AddWithValue("@categoryid", Id);
                        int rowsAffected = command1.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            Console.WriteLine($"Đã xóa hàng thứ {Id} ra khỏi bản thành công.");
                        else
                            Console.WriteLine($"Không tồn tại hàng thứ {Id}.");
                        EmptyBox();
                        DisplayCategory();
                    }
                }
            }
        }
        
        public void EmptyBox1()
        {
            txtBrandName.Clear();
            cmbStatus1.SelectedIndex = 0;
        }
        public void DisplayBrand()
        {
            dgvBrand.Columns[0].Visible = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Brand", connection))
                {
                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvBrand.DataSource = dataTable;
                    }
                    lblTotalBrand.Text = dgvBrand.Rows.Count.ToString();
                }
            }
        }

        private void picSearch1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picSearch, "Search");
        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            if (txtBrandName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên thương hiệu.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus1.SelectedIndex == 0)
            {
                MessageBox.Show("vui lòng nhập tình trạng.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Brand;", connection))
                    using (SqlCommand command1 = new SqlCommand("INSERT INTO Brand  (Brand_Name,Brand_Status)" +
                        "OUTPUT inserted.Brand_Id VALUES (@Brand_Name,@Brand_Status);", connection))
                    {
                        command1.Parameters.AddWithValue("@Brand_Name", txtBrandName.Text.Trim());
                        command1.Parameters.AddWithValue("@Brand_Status", cmbStatus1.SelectedItem.ToString());

                        command1.ExecuteNonQuery();
                        EmptyBox1();
                        DisplayBrand();
                    }
                }
            }
        }

        private void tpBrand_Enter(object sender, EventArgs e)
        {
            EmptyBox1();
            DisplayBrand();
        }

        private void txtSearchBrandName_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Brand WHERE Brand_Name LIKE '%" + txtSearchBrandName.Text + "%';", connection))
                {

                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvBrand.DataSource = dataTable;
                    }
                    lblTotalBrand.Text = dgvBrand.Rows.Count.ToString();
                }
            }
        }

        private void dgvBrand_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvBrand.Rows[e.RowIndex];
                id = row.Cells[0].Value.ToString();
                txtBrandName.Text = row.Cells[1].Value.ToString();
                cmbStatus1.SelectedItem = row.Cells[2].Value.ToString();

            }
        }

        private void btnChangeBrand_Click(object sender, EventArgs e)
        {
            if (id == "")
            {
                MessageBox.Show("vui lòng chọn hàng bạn muốn thay đổi.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtBrandName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên thương hiệu.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus1.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng nhập tình trang.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command1 = new SqlCommand("UPDATE Brand SET Brand_Name = @Brand_Name where Brand_Id = @Brand_Id and not exists (select * from Brand where Brand_Name = @Brand_Name)", connection))
                    using (SqlCommand command2 = new SqlCommand("UPDATE Brand SET Brand_Status = @Brand_Status where Brand_Id = @Brand_Id", connection))
                    {

                        command1.Parameters.AddWithValue("@Brand_Name", txtBrandName.Text.Trim());
                        command1.Parameters.AddWithValue("@Brand_Id", id);
                        command2.Parameters.AddWithValue("@Brand_Status", cmbStatus1.SelectedItem.ToString());
                        command2.Parameters.AddWithValue("@Brand_Id", id);

                        command1.ExecuteNonQuery();
                        command2.ExecuteNonQuery();
                        EmptyBox1();
                        DisplayBrand();
                    }
                }
            }

        }

        private void btnRemoveBrand_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("bạn có muốn xóa Thương hiệu này không ?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command1 = new SqlCommand("DELETE FROM Brand WHERE Brand_Id = @brandid", connection))
                    {

                        command1.Parameters.AddWithValue("@brandid", id);
                        int rowsAffected = command1.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            Console.WriteLine($"Đã xóa hàng thứ {id} ra khỏi bản thành công.");
                        else
                            Console.WriteLine($"Không tồn tại hàng thứ {id}.");
                        EmptyBox1();
                        DisplayBrand();
                    }
                }
            }
        }
    }
}
