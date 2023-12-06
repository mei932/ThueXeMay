using Ql_TXM.Modify;
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
    }
}
