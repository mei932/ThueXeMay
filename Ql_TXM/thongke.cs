using Ql_TXM.Modify;
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
using OfficeOpenXml;

namespace Ql_TXM
{
    public partial class thongke : Form
    {
        modifyall modifyall = new modifyall();
        public thongke()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nam = txtnam.Text;

            string sqlNhanVien = "SELECT TOP 1 NV.HoTen AS 'Tên nhân viên', SUM(DT.SoTien) AS 'Tổng doanh thu' " +
                                 "FROM NhanVien NV " +
                                 "JOIN DoanhThu DT ON NV.MaNhanVien = DT.MaNhanVien " +
                                 "WHERE YEAR(DT.NgayThanhToan) = @Nam " +
                                 "GROUP BY NV.HoTen " +
                                 "ORDER BY SUM(DT.SoTien) DESC";

            using (SqlConnection con = connection.GetSqlConnection())
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlNhanVien, con);
                command.Parameters.AddWithValue("@Nam", nam);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string tenNhanVien = reader["Tên nhân viên"].ToString();
                    string tongDoanhThu = reader["Tổng doanh thu"].ToString();

                    txtName.Text = tenNhanVien;
                    txtTong.Text = tongDoanhThu;
                }
                else
                {
                    txtName.Text = $"Năm {nam} không có dữ liệu để thống kê.";
                    txtTong.Text = $"Năm {nam} không có dữ liệu để thống kê.";
                }

                reader.Close();
            }

            string sqlDichVu = "SELECT TOP 1 DV.TenXe AS 'Tên xe', COUNT(*) AS 'Số lần thuê' " +
                               "FROM XeMay DV " +
                               "JOIN ThueXeTheoGio TG ON DV.MaXe = TG.MaXe " +
                               "JOIN ThueXeNhieuNgay NN ON DV.MaXe = NN.MaXe " +
                               "GROUP BY DV.TenXe " +
                               "ORDER BY COUNT(*) DESC";

            using (SqlConnection con = connection.GetSqlConnection())
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlDichVu, con);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string tenXe = reader["Tên xe"].ToString();
                    string soLanThue = reader["Số lần thuê"].ToString();

                    txxtTenDv.Text = tenXe;
                    txtTongSl.Text = soLanThue;
                }
                else
                {
                    txxtTenDv.Text = "Không có dữ liệu.";
                    txtTongSl.Text = "Không có dữ liệu.";
                }

                reader.Close();
            }

            string sqlTongDoanhThu = "SELECT SUM(DT.SoTien) AS 'Tổng doanh thu' " +
                                     "FROM DoanhThu DT " +
                                     "WHERE YEAR(DT.NgayThanhToan) = @Nam";

            using (SqlConnection con = connection.GetSqlConnection())
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlTongDoanhThu, con);
                command.Parameters.AddWithValue("@Nam", nam);

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    decimal tongDoanhThu = Convert.ToDecimal(result);
                    // Hiển thị kết quả vào ô TextBox hoặc nơi bạn muốn
                    txtTongDoanhThu.Text = tongDoanhThu.ToString("N2");
                }
                else
                {
                    txtTongDoanhThu.Text = $"Năm {nam} không có dữ liệu để tính tổng doanh thu.";
                }
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.FileName = "Thống_kê_" + txtnam.Text + ".xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo file = new FileInfo(sfd.FileName);
                        using (ExcelPackage package = new ExcelPackage(file))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("ThongKeKhachHang");

                            // Gán giá trị cho ô và thiết lập màu sắc
                            ExcelRange range = worksheet.Cells["E1:J1"];
                            range.Merge = true;
                            range.Value = "BẢNG THỐNG KÊ DỮ LIỆU THEO NĂM ";
                            range.Style.Font.Bold = true;
                            range.Style.Font.Size = 10;
                            range.Style.Font.Color.SetColor(System.Drawing.Color.Red);

                            //
                            // Gán giá trị và merge ô từ C4 đến D5
                            ExcelRange mergedCells = worksheet.Cells["C4:D5"];
                            mergedCells.Merge = true;
                            mergedCells.Value = "Nhân viên ưu tú";
                            worksheet.Column(3).Width = 10; // Set width for column C
                            worksheet.Column(4).Width = 10; // Set width for column D
                            worksheet.Column(6).Width = 20; // Set width for column D
                            mergedCells.Style.Font.Bold = true;
                           
                            mergedCells.Style.Font.Color.SetColor(System.Drawing.Color.Red);

                            // Hiển thị các giá trị trong các ô tiếp theo
                            worksheet.Cells["E4"].Value = "Tên nhân viên:";
                            worksheet.Column(5).Width = 20;
                            worksheet.Cells["F4"].Value = txtName.Text;

                            worksheet.Cells["E5"].Value = "Doanh thu kiếm về:";
                            worksheet.Cells["F5"].Value = txtTong.Text;

                            //
                           
                            mergedCells.Style.Font.Color.SetColor(System.Drawing.Color.Red);
                            // Nối và kéo dài ô từ E2 đến F2
                            worksheet.Cells["E2"].Value = "Năm được tìm:";
                            worksheet.Cells["F2"].Value = txtnam.Text;

                            /* worksheet.Cells["E4"].Value = "Tên nhân viên:";
                             worksheet.Cells["F4"].Value = txtName.Text;

                             worksheet.Cells["E5"].Value = "Doanh thu kiếm về:";
                             worksheet.Cells["F5"].Value = txtTong.Text;*/

                            ExcelRange mergedCells1 = worksheet.Cells["C7:D8"];
                            mergedCells1.Merge = true;
                            mergedCells1.Value = "Xe đc chọn nhiều nhất";
                            mergedCells1.Style.Font.Bold = true;
                            //mergedCells1.Style.Font.Size = 50;

                            mergedCells1.Style.Font.Color.SetColor(System.Drawing.Color.Red);

                            worksheet.Cells["E7"].Value = "Xe được chọn:";
                            worksheet.Cells["F7"].Value = txxtTenDv.Text;

                            worksheet.Cells["E8"].Value = "Số lượng:";
                            worksheet.Cells["F8"].Value = txtTongSl.Text;


                            worksheet.Cells["E10"].Value = "Tổng doanh thu:";
                            worksheet.Cells["E10"].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                            worksheet.Cells["E10"].Style.Font.Bold = true;
                            worksheet.Cells["F10"].Value = txtTongDoanhThu.Text;

                            // Thiết lập cột và lưu file Excel
                            // Thiết lập độ rộng của cột
                            /*worksheet.Column().Width = 300; // Cột 1 có độ rộng 30
                            worksheet.Column(2).AutoFit(); // Cột 2 tự động điều chỉnh độ rộng
*/
                            package.Save();
                        }

                        MessageBox.Show("Xuất Excel thành công!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
