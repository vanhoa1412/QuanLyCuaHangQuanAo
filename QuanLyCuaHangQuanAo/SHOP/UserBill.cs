using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace QuanLyCuaHangQuanAo.SHOP
{
    public partial class UserBill : UserControl
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=QUANLYSHOP;Integrated Security=True;";
        private string Id = "";
        public UserBill()
        {
            InitializeComponent();
        }

        public void EmptyBox()
        {
            txtCustomerName.Clear();
            mtbCustomerNumber.Clear();
            AddClear();
            dgvClothList.Rows.Clear();
            txtTotalAmount.Text = "0";
            nudDiscount.Value = 0;
            txtGrandTotal.Text = "0";
        }
        public void DisplayCloth()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT Cloth_ID,Cloth_Name, Cloth_Image, Cloth_Price, Cloth_Status FROM Cloth", connection))
                {
                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvClothBill.DataSource = dataTable;
                    }
                    lblTotalCloth.Text = dgvClothBill.Rows.Count.ToString();
                }
            }
        }
        private void AddClear()
        {
            cmbCloth.Items.Clear();
            cmbCloth.Items.Add("-- Chọn --");
            cmbCloth.SelectedIndex = 0;
            txtRate.Clear();
            nudQuantity.Value = 0;
            txtTotal.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT Cloth_Name From Cloth Where Cloth_Status = 'Còn Hàng' order By Cloth_Name", connection))
                {

                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ClothName = reader.GetString(0);
                            cmbCloth.Items.Add(ClothName);
                        }
                    }
                    command1.ExecuteNonQuery();
                }

            }
        }
        RichTextBox richTextBox = new RichTextBox();
        private void btnAdd_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAdd, "Add");
        }
      
        int oTotal = 0;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbCloth.SelectedIndex == 0)
            {
                MessageBox.Show("Hãy lựa chọn sản phẩm.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (nudQuantity.Value == 0)
            {
                MessageBox.Show("Hãy nhập vào số lượng.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                int rate, total;
                Int32.TryParse(txtRate.Text, out rate);
                Int32.TryParse(txtTotal.Text, out total);
                if (dgvClothList.Rows.Count != 0)
                {
                    foreach (DataGridViewRow rows in dgvClothList.Rows)
                    {
                        if (rows.Cells[1].Value.ToString() == cmbCloth.SelectedItem.ToString())
                        {

                            int quantity = Convert.ToInt32(rows.Cells[3].Value.ToString());
                            int total1 = Convert.ToInt32(rows.Cells[4].Value.ToString());
                            quantity += Convert.ToInt32(nudQuantity.Value);
                            total1 += total;
                            rows.Cells[3].Value = quantity;
                            rows.Cells[4].Value = total1;
                            AddClear();

                        }
                        else
                        {
                            if (cmbCloth.SelectedIndex != 0)
                            {
                                txtTotal.Text = (rate * Convert.ToInt32(nudQuantity.Value)).ToString();
                                string[] row =
                                {
                                       lblClothID.Text ,cmbCloth.SelectedItem.ToString().Trim(), txtRate.Text, nudQuantity.Value.ToString(), txtTotal.Text
                                    };
                                dgvClothList.Rows.Add(row);
                                AddClear();

                            }
                        }

                    }
                }
                else
                {
                    txtTotal.Text = (rate * Convert.ToInt32(nudQuantity.Value)).ToString();
                    string[] row =
                    {
                                    lblClothID.Text, cmbCloth.SelectedItem.ToString().Trim(), txtRate.Text, nudQuantity.Value.ToString(), txtTotal.Text
                                    };
                    dgvClothList.Rows.Add(row);
                    AddClear();
                }
                txtTotalAmount.Text = oTotal.ToString();
            }
            foreach (DataGridViewRow row in dgvClothList.Rows)
            {
                oTotal += Convert.ToInt32(row.Cells[4].Value.ToString());
                txtTotalAmount.Text = oTotal.ToString();
            }
            oTotal = 0;

        }

        private void cmbCloth_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var rate_hold = -1;
                var Id_hold = -1;
                using (SqlCommand command1 = new SqlCommand("SELECT Cloth_Price FROM Cloth WHERE Cloth_Name = @ClothName;", connection))
                using (SqlCommand command2 = new SqlCommand("SELECT Cloth_ID FROM Cloth WHERE Cloth_Name = @ClothName;", connection))
                {
                    command1.Parameters.AddWithValue("@ClothName", cmbCloth.SelectedItem.ToString());
                    command2.Parameters.AddWithValue("@ClothName", cmbCloth.SelectedItem.ToString());
                    SqlDataReader reader = command1.ExecuteReader();
                    while (reader.Read())
                    {
                        Id_hold = reader.GetInt32(0);
                        rate_hold = reader.GetInt32(0);
                    }
                    if (Id_hold != -1)
                    {
                        lblClothID.Text = Id_hold.ToString();
                    }
                    if (rate_hold != -1)
                    {
                        txtRate.Text = rate_hold.ToString();
                    }

                    //command1.ExecuteNonQuery();
                    reader.Close();
                }
            }
        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            int rate;
            Int32.TryParse(txtRate.Text, out rate);
            txtTotal.Text = (rate * Convert.ToInt32(nudQuantity.Value)).ToString();
        }

        private void nudDiscount_ValueChanged(object sender, EventArgs e)
        {
            decimal totalAmount = Convert.ToDecimal(txtTotalAmount.Text);
            decimal discountAmount = totalAmount * (nudDiscount.Value / 100);
            decimal grandTotal = totalAmount - discountAmount;
            txtGrandTotal.Text = grandTotal.ToString();
        }

        private void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            nudDiscount_ValueChanged(sender, e);
        }

        private void dgvClothList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                int rowIndex = dgvClothList.CurrentCell.RowIndex;
                dgvClothList.Rows.RemoveAt(rowIndex);
                if (dgvClothList.Rows.Count != 0)
                {
                    foreach (DataGridViewRow row in dgvClothList.Rows)
                    {
                        oTotal += Convert.ToInt32(row.Cells[3].Value.ToString());
                        txtTotalAmount.Text = oTotal.ToString();
                    }
                }
                else
                    txtTotalAmount.Text = "0";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Nhập vào tên khách hàng.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (!mtbCustomerNumber.MaskCompleted)
            {
                MessageBox.Show("Nhập vào số điện thoại khách hàng.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string grandTotalWithoutComma = txtGrandTotal.Text.Replace(",", "");
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Bill;", connection))
                    using (SqlCommand command1 = new SqlCommand("INSERT INTO Bill (Customer_Name, Customer_Number, Bill_Cloth, Bill_Quantity, Total_Amount, Discount, Grand_Total) " +
                                                                "OUTPUT inserted.Bill_Id Values (@CustomerName, @CustomerNumber,@BillCloth, @BillQuantity, @TotalAmount, @Discount, @GrandTotal);", connection))
                    {
                        command1.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text.Trim());
                        command1.Parameters.AddWithValue("@CustomerNumber", mtbCustomerNumber.Text.Trim());

                        command1.Parameters.AddWithValue("@TotalAmount", Convert.ToInt32(txtTotalAmount.Text));
                        command1.Parameters.AddWithValue("@Discount", nudDiscount.Value);
                        command1.Parameters.AddWithValue("@GrandTotal", grandTotalWithoutComma);

                        foreach (DataGridViewRow row in dgvClothList.SelectedRows)
                        {
                            command1.Parameters.AddWithValue("@BillCloth", Convert.ToInt32(dgvClothList.SelectedRows[0].Cells[0].Value));
                            command1.Parameters.AddWithValue("@BillQuantity", Convert.ToInt32(dgvClothList.SelectedRows[0].Cells[3].Value));
                           

                           using (SqlCommand command2 = new SqlCommand("UPDATE Cloth SET Cloth_Quantity = Cloth_Quantity - @Quantity WHERE Cloth_ID = @ClothID", connection))
                            {
                                command2.Parameters.AddWithValue("@ClothID", Convert.ToInt32(dgvClothList.SelectedRows[0].Cells[0].Value)); 
                                command2.Parameters.AddWithValue("@Quantity", Convert.ToInt32(dgvClothList.SelectedRows[0].Cells[3].Value)); 
                                command2.ExecuteNonQuery();
                            }
                            using (SqlCommand command3 = new SqlCommand("UPDATE Cloth SET Cloth_Status = 'Hết Hàng' WHERE Cloth_ID = @ClothID AND Cloth_Quantity < 0 AND Cloth_Status = 'Còn Hàng'", connection))
                            {
                                command3.Parameters.AddWithValue("@ClothID", Convert.ToInt32(dgvClothList.SelectedRows[0].Cells[0].Value));
                                command3.ExecuteNonQuery();
                            }
                        }
                        command1.ExecuteNonQuery();
                        EmptyBox();
                        tcBill.SelectedTab = tpManageBill;
                    }
                }
            }
      
        }


        private void Receipt()
        {
            richTextBox.Clear();
            richTextBox.Text += "\t        CỬA HÀNG QUẦN ÁO DEMO      \n";
            richTextBox.Text += "\t *********************************************************\n\n";
            richTextBox.Text += "  Tên Khách Hàng: " + txtCustomerName.Text.ToString().Trim() + "\n";
            richTextBox.Text += " Số Điện Thoại: " + mtbCustomerNumber.Text.ToString().Trim() + "\n\n";
            richTextBox.Text += "\t *********************************************************\n\n";
            richTextBox.Text += "  Tên\t\tGiá tiền\t\tSố Lượng\t\tTổng\n";
            richTextBox.Text += "\t *********************************************************\n\n";

            foreach (DataGridViewRow row in dgvClothList.Rows)
            {
                richTextBox.Text += "  " + row.Cells[1].Value.ToString() + "\t\t" +
                                   row.Cells[2].Value.ToString() + "\t\t" +
                                   row.Cells[3].Value.ToString() + "\t\t" +
                                   row.Cells[4].Value.ToString() + "\n";
            }
            

            richTextBox.Text += "\t *********************************************************\n\n";
            richTextBox.Text += "\t\t\t\t\tTổng Tiền: $" + txtTotalAmount.Text + "\n";
            richTextBox.Text += "\t\t\t\t\tGiảm Giá: $" + nudDiscount.Text + "\n";
            richTextBox.Text += "\t\t\t\t\tThành Tiền: $" + txtGrandTotal.Text + "\n";
        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {
            Receipt();
            printPreviewDialog.Document = printDocument;
            printDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            printPreviewDialog.ShowDialog();
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox.Text, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new System.Drawing.Point(10, 10));

        }

        private void dgvClothBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvClothBill.Rows[e.RowIndex];
                cmbCloth.SelectedItem = row.Cells[1].Value.ToString();
                lblClothID.Text = row.Cells[0].Value.ToString();
                txtRate.Text = row.Cells[3].Value.ToString();
            }
        }

        private void tpManageBill_Enter(object sender, EventArgs e)
        {
            txtSearchCustomerName.Clear();
            
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("select Bill_Id, Customer_Name, Customer_Number, Bill_Date, Bill_Cloth, Bill_Quantity,  Total_Amount, Discount, Grand_Total from Bill;", connection))
                {

                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvBill.DataSource = dataTable;
                    }
                    lblTotal.Text = dgvBill.Rows.Count.ToString();
                }
            }
        }

        private void txtSearchCustomerName_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Bill WHERE Customer_Name LIKE '%" + txtSearchCustomerName.Text + "%';", connection))
                {

                    using (var reader = command1.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        dgvBill.DataSource = dataTable;
                        //command1.ExecuteNonQuery();
                    }
                    lblTotal.Text = dgvBill.Rows.Count.ToString();

                }

            }
        }

        private void tpBill_Enter(object sender, EventArgs e)
        {
            EmptyBox();
            AddClear();
            DisplayCloth();
        }

       
    }
}
