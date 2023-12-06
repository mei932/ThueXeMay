using Ql_TXM.Modify;
using Ql_TXM.Object;
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
    public partial class tblDoanhThu : Form
    {
        modifyall modifyall = new modifyall();
        doanhthu dt;
        public tblDoanhThu()
        {
            InitializeComponent();
        }

        private void dgvNv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNv.Rows.Count >= 0)
            {
                txtMaHD.Text = dgvNv.SelectedRows[0].Cells[0].Value.ToString();
                txtMaKH.Text = dgvNv.SelectedRows[0].Cells[1].Value.ToString();
                cbXM.Text = dgvNv.SelectedRows[0].Cells[2].Value.ToString();
                txtMNV.Text = dgvNv.SelectedRows[0].Cells[3].Value.ToString();
                txtTimeTT.Text = dgvNv.SelectedRows[0].Cells[4].Value.ToString();
                txtSoTien.Text = dgvNv.SelectedRows[0].Cells[5].Value.ToString();
            }

        }
        void load_data()
        {
            dgvNv.DataSource = modifyall.Table("SELECT * FROM DoanhThu");
            // Lấy dữ liệu từ truy vấn SQL
            DataTable dataTable = modifyall.Table("SELECT MaXe FROM (SELECT MaXe FROM ThueXeTheoGio UNION SELECT MaXe FROM ThueXeNhieuNgay) AS CombinedData");
            // Đặt nguồn dữ liệu cho ComboBox
            cbXM.DisplayMember = "MaXe";
            cbXM.ValueMember = "MaXe";
            cbXM.DataSource = dataTable;
        }
        private void tblDoanhThu_Load(object sender, EventArgs e)
        {
            load_data();
        }
        public void Getvaluetextbox()
        {
            string MHD = txtMaHD.Text;
            string MKH = txtMaKH.Text;
            int MaXe = int.Parse(cbXM.SelectedValue.ToString());
            string MNV = txtMNV.Text;
            DateTime TimeS = DateTime.Parse(txtTimeTT.Text);
            float ST = float.Parse(txtSoTien.Text);

            dt = new doanhthu(MHD, MKH, MaXe, MNV, TimeS, ST);
        }
        public bool CheckValue()
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text) || string.IsNullOrWhiteSpace(txtMaKH.Text) ||
                string.IsNullOrWhiteSpace(cbXM.Text) || string.IsNullOrWhiteSpace(txtMNV.Text) || string.IsNullOrWhiteSpace(txtTimeTT.Text) || string.IsNullOrWhiteSpace(txtSoTien.Text))


            {
                MessageBox.Show("Mời bạn nhập đầy đủ thông tin!");
                return false;
            }

            return true;
        }

        private void ThemDuLieu()
        {
            if (CheckValue())
            {

                SqlConnection conn = connection.GetSqlConnection();

                string sqlCheck = "SELECT COUNT(*) FROM DoanhThu WHERE MaHoaDon = @ID";
                SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn);
                cmdCheck.Parameters.AddWithValue("@ID", txtMaHD.Text);

                try
                {
                    conn.Open();
                    int count = (int)cmdCheck.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Mã đã tồn tại!");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kiểm tra: " + ex.Message);
                    return;
                }
                finally
                {
                    conn.Close();
                }


                /*   int maXe;
                   if (cbXM.SelectedValue != null && int.TryParse(cbXM.SelectedValue.ToString(), out maXe))
                   {
                       try
                       {
                           using (SqlConnection conn = connection.GetSqlConnection())
                           {
                               conn.Open();

                               // Kiểm tra xem mã xe đã tồn tại trong bảng ThueXeTheoGio chưa
                               string queryCheck = "SELECT COUNT(*) FROM ThueXeNhieuNgay WHERE MaXe = @MaXe";
                               SqlCommand commandCheck = new SqlCommand(queryCheck, conn);
                               commandCheck.Parameters.AddWithValue("@MaXe", maXe);
                               int count = (int)commandCheck.ExecuteScalar();

                               if (count > 0)
                               {
                                   MessageBox.Show("Mã hóa đơn đã tồn tại, vui lòng chọn mã  khác!");
                                   return;
                               }*/
                Getvaluetextbox();
                // Nếu mã xe không trùng, thực hiện thêm dữ liệu vào bảng ThueXeTheoGio
                string queryInsert = "INSERT INTO DoanhThu (MaHoaDon, MaKhachHang, MaXe, MaNhanVien, NgayThanhToan, SoTien) " +
                                         "VALUES (@MaHoaDon, @MaKhachHang, @MaXe, @MaNhanVien, @NgayThanhToan,@SoTien)";
                SqlCommand commandInsert = new SqlCommand(queryInsert, conn);

                // Thêm các tham số vào câu truy vấn để tránh lỗ hổng SQL Injection
                commandInsert.Parameters.AddWithValue("@MaHoaDon", dt.MaHoaDonProperty);
                commandInsert.Parameters.AddWithValue("@MaKhachHang", dt.MaKhachHangProperty);
                commandInsert.Parameters.AddWithValue("@MaXe", dt.MaXeProperty);
                commandInsert.Parameters.AddWithValue("@MaNhanVien", dt.MaNhanVienproperty);
                commandInsert.Parameters.AddWithValue("@ngayThanhToan", dt.NgayThanhToanProperty);
                commandInsert.Parameters.AddWithValue("@SoTien", dt.SoTienProperty);

                try
                {
                    conn.Open();
                    commandInsert.ExecuteNonQuery();
                    MessageBox.Show("Thêm  thành công!");
                    load_data(); // Tải lại dữ liệu trong DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void SuaDuLieu()
        {
            if (CheckValue())
            {
                int maHoaDon, maKhachHang, maNhanVien;
                DateTime ngayThanhToan;

                if (int.TryParse(txtMaHD.Text, out maHoaDon) &&
                    int.TryParse(txtMaKH.Text, out maKhachHang) &&
                    int.TryParse(txtMNV.Text, out maNhanVien) &&
                    DateTime.TryParse(txtTimeTT.Text, out ngayThanhToan))
                {
                    float soTien;
                    if (float.TryParse(txtSoTien.Text, out soTien))
                    {
                        int maXe;
                        if (cbXM.SelectedValue != null && int.TryParse(cbXM.SelectedValue.ToString(), out maXe))
                        {
                            try
                            {
                                using (SqlConnection conn = connection.GetSqlConnection())
                                {
                                    conn.Open();

                                    // Sử dụng ID (ở đây là MaHoaDon) để cập nhật thông tin
                                    string queryUpdate = "UPDATE DoanhThu SET MaKhachHang = @MaKhachHang, MaXe = @MaXe, MaNhanVien = @MaNhanVien, " +
                                                         "NgayThanhToan = @NgayThanhToan, SoTien = @SoTien WHERE MaHoaDon = @MaHoaDon";
                                    SqlCommand commandUpdate = new SqlCommand(queryUpdate, conn);

                                    // Thêm các tham số vào câu truy vấn để tránh lỗ hổng SQL Injection
                                    commandUpdate.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                                    commandUpdate.Parameters.AddWithValue("@MaXe", maXe);
                                    commandUpdate.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                                    commandUpdate.Parameters.AddWithValue("@NgayThanhToan", ngayThanhToan);
                                    commandUpdate.Parameters.AddWithValue("@SoTien", soTien);
                                    commandUpdate.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                                    int rowsAffected = commandUpdate.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Cập nhật dữ liệu thành công!");
                                        load_data(); // Load lại dữ liệu vào DataGridView sau khi cập nhật thành công
                                    }
                                    else
                                    {
                                        MessageBox.Show("Cập nhật dữ liệu thất bại!");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi: " + ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng chọn mã xe!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập số tiền hợp lệ!");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng dữ liệu!");
                }
            }
            else
            {
                MessageBox.Show("Mời bạn nhập đầy đủ thông tin!");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ThemDuLieu();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SuaDuLieu();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNv.Rows.Count > 1)
            {
                string choose = dgvNv.SelectedRows[0].Cells[0].Value.ToString();
                string query = "DELETE DoanhThu ";
                query += " WHERE MaHoaDon = '" + choose + "'";
                try
                {
                    if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        modifyall.Command(query);
                        MessageBox.Show("Bạn đã xóa 1 đối tượng thành công!");
                        load_data();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa: " + ex.Message);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string name = txtTim.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên để tìm!");
                return;
            }
            else
            {
                string query = "SELECT * FROM DoanhThu WHERE MaHoaDon =  @sMaNV";

                using (SqlConnection conn = connection.GetSqlConnection())
                {
                    DataTable resultTable = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sMaNV", name);
                        conn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(resultTable);
                    }

                    dgvNv.DataSource = resultTable;
                }
            }
        }
    }
}
