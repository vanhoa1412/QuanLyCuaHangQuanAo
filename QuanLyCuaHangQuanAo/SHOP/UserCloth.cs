using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyCuaHangQuanAo.SHOP
{
    public partial class UserCloth : UserControl
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=QUANLYSHOP;Integrated Security=True;";

        private string Id = "";
        byte[] image;
        ImageConverter imageConverter;
        MemoryStream memoryStream;
        public UserCloth()
        {
            InitializeComponent();
        }

        private void ImageUpload(PictureBox picture)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    picture.Image = Image.FromFile(openFileDialog.FileName);
            }
            catch (Exception)
            {
                MessageBox.Show("Ảnh tải lên bị lỗi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EmptyBox()
        {
            txtClothName.Clear();
            picPhoto.Image = null;
            nudRate.Value = 0;
            nudQuantity.Value = 0;
            cmbBrand.DataSource = null;
            cmbBrand.Items.Clear();
            cmbBrand.Items.Add("--Chọn--");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command1 = new SqlCommand("SELECT Brand_Name FROM Brand WHERE Brand_Status = 'Còn Hàng' ORDER BY Brand_Name;", connection))
                {
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string brandName = reader.GetString(0); // Assuming Brand_Name is the first column
                            cmbBrand.Items.Add(brandName);
                        }
                    }

                }

            }
            if (cmbBrand.Items.Count > 0)
            {
                cmbBrand.SelectedIndex = 0;
            }
            cmbCategory.DataSource = null;
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("--Chọn--");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command1 = new SqlCommand("SELECT Category_Name FROM Category WHERE Category_Status = 'Còn Hàng' ORDER BY Category_Name;", connection))
                {
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string categoryName = reader.GetString(0); // Assuming Brand_Name is the first column
                            cmbCategory.Items.Add(categoryName);
                        }
                    }

                }

            }
            if (cmbCategory.Items.Count > 0)
            {
                cmbCategory.SelectedIndex = 0;
            }
            cmbStatus.SelectedIndex = 0;
        }

        private void EmptyBox1()
        {
            txtClothName1.Clear();
            picPhoto1.Image = null;
            nudRate1.Value = 0;
            nudQuantity1.Value = 0;
            ComboBoxAutoFill();
            cmbStatus1.SelectedIndex = 0;
            Id = "";
        }
        private void ComboBoxAutoFill()
        {
            cmbBrand1.Items.Clear();
            cmbBrand1.Items.Add("--Chọn--");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command1 = new SqlCommand("SELECT Brand_Name FROM Brand WHERE Brand_Status = 'Còn Hàng' ORDER BY Brand_Name;", connection))
                {
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string categoryName = reader.GetString(0);
                            cmbBrand1.Items.Add(categoryName);
                        }
                    }
                    cmbBrand1.SelectedIndex = 0;
                }

            }

            cmbCategory1.Items.Clear();
            cmbCategory1.Items.Add("--Chọn--");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command1 = new SqlCommand("SELECT Category_Name FROM Category WHERE Category_Status = 'Còn Hàng' ORDER BY Category_Name;", connection))
                {
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string categoryName = reader.GetString(0);
                            cmbCategory1.Items.Add(categoryName);
                        }
                    }
                    cmbCategory1.SelectedIndex = 0;
                }

            }
        }


        private void picSearch_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picSearch, "Search");
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            ImageUpload(picPhoto);
        }
        private void btnBrowse1_Click(object sender, EventArgs e)
        {
            ImageUpload(picPhoto1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtClothName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Nhập tên của quần áo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (picPhoto.Image == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudRate.Value == 0)
            {
                MessageBox.Show("Vui lòng nhập giá tiền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudQuantity.Value == 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbBrand.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn thương hiệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbCategory.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn thể loại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus.SelectedIndex == 0)
            {
                MessageBox.Show("Please select status.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (MemoryStream picStream = new MemoryStream())
                    {
                        picPhoto.Image.Save(picStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] bytePic = picStream.ToArray();
                        int brandId = 0;
                        using (SqlCommand commandBrand = new SqlCommand("SELECT Brand_Id FROM Brand WHERE Brand_Name = @BrandName", connection))
                        {
                            commandBrand.Parameters.AddWithValue("@BrandName", cmbBrand.SelectedItem.ToString());
                            object result = commandBrand.ExecuteScalar();
                            if (result != DBNull.Value)
                            {
                                brandId = Convert.ToInt32(result);
                            }
                        }

                        // Get the Category_Id
                        int categoryId = 0;
                        using (SqlCommand commandCategory = new SqlCommand("SELECT Category_Id FROM Category WHERE Category_Name = @CategoryName", connection))
                        {
                            commandCategory.Parameters.AddWithValue("@CategoryName", cmbCategory.SelectedItem.ToString());
                            object result = commandCategory.ExecuteScalar();
                            if (result != DBNull.Value)
                            {
                                categoryId = Convert.ToInt32(result);
                            }
                        }
                        using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Cloth;", connection))
                        using (SqlCommand command1 = new SqlCommand("INSERT INTO Cloth  (Cloth_Name,Cloth_Image,Cloth_Price,Cloth_Quantity,Cloth_Brand,Cloth_Category,Cloth_Status) " +
                            " OUTPUT inserted.Cloth_Id VALUES (@Cloth_Name,@Cloth_Image,@Cloth_Price,@Cloth_Quantity,@Cloth_Brand,@Cloth_Category,@Cloth_Status);", connection))
                        {
                            //command1.Parameters.AddWithValue("@Cloth_Id", (int)command.ExecuteScalar() + i++);
                            command1.Parameters.AddWithValue("@Cloth_Name", txtClothName.Text.Trim());
                            command1.Parameters.AddWithValue("@Cloth_Image", bytePic);
                            command1.Parameters.AddWithValue("@Cloth_Price", Convert.ToInt32(nudRate.Value));
                            command1.Parameters.AddWithValue("@Cloth_Quantity", Convert.ToInt32(nudQuantity.Value));
                            command1.Parameters.AddWithValue("@Cloth_Brand", brandId);
                            command1.Parameters.AddWithValue("@Cloth_Category", categoryId);
                            command1.Parameters.AddWithValue("@Cloth_Status", cmbStatus.SelectedItem.ToString());
                            command1.ExecuteNonQuery();
                            EmptyBox();
                        }
                    }
                }
            }
        }

        private void tpAddCloth_Enter(object sender, EventArgs e)
        {
            EmptyBox();
            
        }

        private void tpManagerCloth_Enter(object sender, EventArgs e)
        {
            txtSearchClothName.Clear();
            dgvCloth.Columns[0].Visible = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Cloth", connection))
                {
                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvCloth.DataSource = dataTable;
                    }
                    lblTotal.Text = dgvCloth.Rows.Count.ToString();
                }
            }
        }

        private void txtSearchClothName_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Cloth WHERE Cloth_Name LIKE '%" + txtSearchClothName.Text + "%';", connection))
                {

                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvCloth.DataSource = dataTable;
                    }
                    lblTotal.Text = dgvCloth.Rows.Count.ToString();
                }
            }
        }

        

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (Id == "")
            {
                MessageBox.Show("vui lòng chọn hàng bạn muốn thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (txtClothName1.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên áo quần.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (picPhoto1.Image == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (nudRate1.Value == 0)
            {
                MessageBox.Show("Vui lòng nhập giá tiền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudQuantity1.Value == 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (cmbBrand1.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn thương hiệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbCategory1.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn thể loại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (cmbStatus1.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn trạng thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (MemoryStream picStream = new MemoryStream())
                    {
                        picPhoto1.Image.Save(picStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] bytePic = picStream.ToArray();

                        int brandId = 0;
                        using (SqlCommand commandBrand = new SqlCommand("SELECT Brand_Id FROM Brand WHERE Brand_Name = @BrandName", connection))
                        {
                            commandBrand.Parameters.AddWithValue("@BrandName", cmbBrand.SelectedItem.ToString());
                            object result = commandBrand.ExecuteScalar();
                            if (result != DBNull.Value)
                            {
                                brandId = Convert.ToInt32(result);
                            }
                        }

                        
                        int categoryId = 0;
                        using (SqlCommand commandCategory = new SqlCommand("SELECT Category_Id FROM Category WHERE Category_Name = @CategoryName", connection))
                        {
                            commandCategory.Parameters.AddWithValue("@CategoryName", cmbCategory.SelectedItem.ToString());
                            object result = commandCategory.ExecuteScalar();
                            if (result != DBNull.Value)
                            {
                                categoryId = Convert.ToInt32(result);
                            }
                        }
                        using (SqlCommand command1 = new SqlCommand("UPDATE Cloth SET Cloth_Name = @Cloth_Name where Cloth_Id = @Cloth_Id and not exists(select * from Cloth where Cloth_Name = @Cloth_Name)", connection))
                        using (SqlCommand command2 = new SqlCommand("UPDATE Cloth SET Cloth_Image = @Cloth_Image where Cloth_Id = @Cloth_Id", connection))
                        using (SqlCommand command3 = new SqlCommand("UPDATE Cloth SET Cloth_Price = @Cloth_Price where Cloth_Id = @Cloth_Id", connection))
                        using (SqlCommand command4 = new SqlCommand("UPDATE Cloth SET Cloth_Quantity = @Cloth_Quantity where Cloth_Id = @Cloth_Id", connection))
                        using (SqlCommand command6 = new SqlCommand("UPDATE Cloth SET Cloth_Brand = @Cloth_Brand where Cloth_Id = @Cloth_Id", connection))
                        using (SqlCommand command7 = new SqlCommand("UPDATE Cloth SET Cloth_Category = @Cloth_Category where Cloth_Id = @Cloth_Id", connection))
                        using (SqlCommand command8 = new SqlCommand("UPDATE Cloth SET Cloth_Status = @Cloth_Status where Cloth_Id = @Cloth_Id", connection))
                        //using (SqlCommand command1 = new SqlCommand("UPDATE Cloth SET ( Cloth_Name, Cloth_Image, Cloth_Rate, Cloth_Quantity, Cloth_Brand, Cloth_Category, Cloth_Status) VALUES ( @Cloth_Name, @Cloth_Image, @Cloth_Rate, @Cloth_Quantity, @Cloth_Brand, @Cloth_Category, @Cloth_Status);", connection))
                        {
                            command1.Parameters.AddWithValue("@Cloth_Name", txtClothName1.Text.Trim());
                            command1.Parameters.AddWithValue("@Cloth_Id", Id);
                            command2.Parameters.AddWithValue("@Cloth_Image", bytePic);
                            command2.Parameters.AddWithValue("@Cloth_Id", Id);
                            command3.Parameters.AddWithValue("@Cloth_Price", Convert.ToInt32(nudRate1.Value));
                            command3.Parameters.AddWithValue("@Cloth_Id", Id);
                            command4.Parameters.AddWithValue("@Cloth_Quantity", Convert.ToInt32(nudQuantity1.Value));
                            command4.Parameters.AddWithValue("@Cloth_Id", Id);
                            command6.Parameters.AddWithValue("@Cloth_Brand",brandId);
                            command6.Parameters.AddWithValue("@Cloth_Id", Id);
                            command7.Parameters.AddWithValue("@Cloth_Category",categoryId);
                            command7.Parameters.AddWithValue("@Cloth_Id", Id);
                            command8.Parameters.AddWithValue("@Cloth_Status", cmbStatus1.SelectedItem.ToString());
                            command8.Parameters.AddWithValue("@Cloth_Id", Id);

                            command1.ExecuteNonQuery();
                            command2.ExecuteNonQuery();
                            command3.ExecuteNonQuery();
                            command4.ExecuteNonQuery();
                            command6.ExecuteNonQuery();
                            command7.ExecuteNonQuery();
                            command8.ExecuteNonQuery();
                            tcCloth.SelectedTab = tpManageCloth;
                            EmptyBox1();

                        }
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa sản phẩm này?", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command1 = new SqlCommand("DELETE FROM Cloth WHERE Cloth_Id = @productid", connection))
                    {

                        command1.Parameters.AddWithValue("@productid", Id);
                        int rowsAffected = command1.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            Console.WriteLine($"Đã xóa hàng thứ {Id} ra khỏi bản thành công.");
                        else
                            Console.WriteLine($"Không tồn tại hàng thứ {Id}.");
                        EmptyBox1();
                        tcCloth.SelectedTab = tpManageCloth;
                    }
                }
            }
        }

        private void tpOptions_Enter(object sender, EventArgs e)
        {

        }

        private void dgvCloth_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                ComboBoxAutoFill();
                DataGridViewRow row = dgvCloth.Rows[e.RowIndex];
                Id = row.Cells[0].Value.ToString();
                txtClothName1.Text = row.Cells[1].Value.ToString();
                image = (byte[])row.Cells[2].Value;
                memoryStream = new MemoryStream(image);
                picPhoto1.Image = Image.FromStream(memoryStream);
                nudRate1.Value = Convert.ToInt32(row.Cells[3].Value.ToString());
                nudQuantity1.Value = Convert.ToInt32(row.Cells[4].Value.ToString());
                cmbBrand1.SelectedItem = row.Cells[5].Value.ToString();
                cmbCategory1.SelectedItem = row.Cells[6].Value.ToString();
                cmbStatus1.SelectedItem = row.Cells[7].Value.ToString();
                tcCloth.SelectedTab = tpOptions;
            }
        }
    }
}