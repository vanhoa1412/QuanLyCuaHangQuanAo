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
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyCuaHangQuanAo.SHOP
{
    public partial class UserHome : UserControl
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=QUANLYSHOP;Integrated Security=True;";

        public UserHome()
        {
            InitializeComponent();
        }
       
        public void DisplayHome()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command1 = new SqlCommand("SELECT COUNT(*) FROM Cloth", connection))
                {
                    lblTotalCloth.Text = ((int)command1.ExecuteScalar()).ToString();
                }
                using (SqlCommand command1 = new SqlCommand("SELECT COUNT(*) FROM Bill", connection))
                {
                    lblTotalBill.Text = ((int)command1.ExecuteScalar()).ToString();
                }
                using (SqlCommand command1 = new SqlCommand("SELECT COUNT(*) FROM Cloth where Cloth_Status = 'Hết Hàng';", connection))
                {
                    lblLowCloth.Text = ((int)command1.ExecuteScalar()).ToString();
                }
                using (SqlCommand command1 = new SqlCommand("SELECT COUNT(*) FROM Cloth where Cloth_Status = 'Còn Hàng';", connection))
                {
                    lblHaveCloth.Text = ((int)command1.ExecuteScalar()).ToString();
                }
                using (SqlCommand command1 = new SqlCommand("SELECT SUM(Grand_Total) FROM Bill", connection))
                {
                    object result = command1.ExecuteScalar();
                    label2.Text = result == DBNull.Value ? "0" : result.ToString(); // Hiển thị 0 nếu không có dữ liệu
                }
            }
        }

        private void DisplayChart()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Lệnh SQL để lấy thông tin sản phẩm và số lượng
                string sql = "SELECT c.Cloth_Name, c.Cloth_Quantity " +
                            "FROM Cloth c " +
                            "GROUP BY c.Cloth_Name, c.Cloth_Quantity";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            chart1.Series.Clear();
                            chart1.ChartAreas.Clear();

                            ChartArea chartArea = new ChartArea("ChartArea1");
                            chart1.ChartAreas.Add(chartArea);

                            Series series = new Series("Sản phẩm");
                            series.ChartType = SeriesChartType.Column;
                            chart1.Series.Add(series);
                            while (reader.Read())
                            {
                                string clothName = reader["Cloth_Name"].ToString();
                                int clothQuantity = (int)reader["Cloth_Quantity"];
                                series.Points.AddXY(clothName, clothQuantity);
                            }

                            chartArea.AxisX.Interval = 0;
                            chartArea.AxisX.MajorGrid.Enabled = false;
                            chartArea.AxisY.MajorGrid.Enabled = true;
                            chart1.Visible = true;
                        }
                    }
                }
            }
        }
        private void DisplayBillChart()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Lệnh SQL để lấy thông tin sản phẩm và số lượng
                string sql = "SELECT c.Category_Name, COUNT(b.Bill_Id) AS TotalBills " +
                            "FROM Bill b " +
                            "JOIN Cloth cl ON b.Bill_Cloth = cl.Cloth_ID " +
                            "JOIN Category c ON cl.Cloth_Category = c.Category_ID " +
                            "GROUP BY c.Category_Name " +
                            "ORDER BY c.Category_Name";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            chart2.Series.Clear();
                            chart2.ChartAreas.Clear();

                            ChartArea chartArea = new ChartArea("ChartArea2");
                            chart2.ChartAreas.Add(chartArea);

                            Series series = new Series("Số lượng hóa đơn");
                            series.ChartType = SeriesChartType.Column;
                            chart2.Series.Add(series);
                            while (reader.Read())
                            {
                                string categoryName = reader["Category_Name"].ToString();
                                int totalBills = (int)reader["TotalBills"];
                                series.Points.AddXY(categoryName, totalBills);
                            }
                            chartArea.AxisX.Interval = 0;
                            chartArea.AxisX.MajorGrid.Enabled = false;
                            chartArea.AxisY.MajorGrid.Enabled = true;
                            chart2.Visible = true;
                        }
                    }
                }
            }
        }
        private void UserHome_Load(object sender, EventArgs e)
        {
            DisplayHome();
            DisplayChart();
            DisplayBillChart();
        }
    }

}
