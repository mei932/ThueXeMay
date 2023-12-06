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
    public partial class tblthueTheoNgay : Form
    {
        modifyall modifyall = new modifyall();
        thuetheogio TG;
        public tblthueTheoNgay()
        {
            InitializeComponent();
        }

        private void cbXM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void load_data()
        {
            dgvNv.DataSource = modifyall.Table("SELECT * FROM ThueXeTheoGio");

            // Lấy dữ liệu từ truy vấn SQL
            DataTable dataTable = modifyall.Table("SELECT X.MaXe FROM XeMay X WHERE NOT EXISTS (SELECT 1 FROM ThueXeTheoGio TG WHERE TG.MaXe = X.MaXe) AND NOT EXISTS (SELECT 1 FROM ThueXeNhieuNgay NN WHERE NN.MaXe = X.MaXe)");

            // Đặt nguồn dữ liệu cho ComboBox
            cbXM.DisplayMember = "MaXe";
            cbXM.ValueMember = "MaXe";
            cbXM.DataSource = dataTable;
        }
        private void tblthueTheoNgay_Load(object sender, EventArgs e)
        {
            load_data();
        }
        public bool CheckValue()
        {
            if (string.IsNullOrWhiteSpace(txtMNV.Text) || string.IsNullOrWhiteSpace(txtMaKH.Text) ||
                string.IsNullOrWhiteSpace(cbXM.Text) || string.IsNullOrWhiteSpace(txtMNV.Text) || string.IsNullOrWhiteSpace(txtTineS.Text) || string.IsNullOrWhiteSpace(txtTimeE.Text))


            {
                MessageBox.Show("Mời bạn nhập đầy đủ thông tin!");
                return false;
            }

            return true;
        }
        public void Getvaluetextbox()
        {
            string MTX = txtMaThue.Text;
            string MKH = txtMaKH.Text;
            int MaXe = int.Parse(cbXM.SelectedValue.ToString());
            string MNV = txtMNV.Text;
            DateTime TimeS = DateTime.Parse(txtTineS.Text);
            DateTime TimeE = DateTime.Parse(txtTimeE.Text);
            
            TG = new thuetheogio(MTX, MKH, MaXe, MNV,TimeS,TimeE);
        }
        private void ThemDuLieu()
        {
            /*            // Lấy thông tin từ các điều khiển trên giao diện
                        int maThue = int.Parse(txtMaThue.Text);
                        int maKhachHang = int.Parse(txtMaKH.Text);
                        int maXe = int.Parse(cbXM.SelectedValue.ToString()); // Lấy giá trị đã chọn từ ComboBox
                        int maNhanVien = int.Parse(txtMNV.Text);
                        DateTime thoiGianBatDau = DateTime.Parse(txtTineS.Text);
                        DateTime thoiGianKetThuc = DateTime.Parse(txtTimeE.Text);
                        int maXe;
                        if (cbXM.SelectedValue != null && int.TryParse(cbXM.SelectedValue.ToString(), out maXe))
                        {
                            if (CheckMaXeTrung(maXe))
                            {
                                MessageBox.Show("Mã xe đã tồn tại, vui lòng chọn mã xe khác!");
                                return;
                            }

                            try
                            {

                            // Thực hiện thêm dữ liệu vào bảng ThueXeTheoGio
                            string query = "INSERT INTO ThueXeTheoGio (MaThue, MaKhachHang, MaXe, MaNhanVien, ThoiGianBatDau, ThoiGianKetThuc) " +
                                           "VALUES (@MaThue, @MaKhachHang, @MaXe, @MaNhanVien, @ThoiGianBatDau, @ThoiGianKetThuc)";

                            SqlConnection conn = connection.GetSqlConnection();
                                SqlCommand command = new SqlCommand(query, conn);

                                // Thêm các tham số vào câu truy vấn để tránh lỗ hổng SQL Injection
                                command.Parameters.AddWithValue("@MaThue", maThue);
                                command.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                                command.Parameters.AddWithValue("@MaXe", maXe);
                                command.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                                command.Parameters.AddWithValue("@ThoiGianBatDau", thoiGianBatDau);
                                command.Parameters.AddWithValue("@ThoiGianKetThuc", thoiGianKetThuc);

                                // Mở kết nối và thực thi truy vấn
                                conn.Open();
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Thêm dữ liệu thành công!");
                                    // Sau khi thêm dữ liệu thành công, có thể gọi lại hàm load_data() để cập nhật DataGridView
                                    load_data();
                                }
                                else
                                {
                                    MessageBox.Show("Thêm dữ liệu thất bại!");
                                }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message);
                        }*/

            if (CheckValue())
            {
                
                SqlConnection conn = connection.GetSqlConnection();

                string sqlCheck = "SELECT COUNT(*) FROM ThueXeTheoGio WHERE MaThue = @ID";
                SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn);
                cmdCheck.Parameters.AddWithValue("@ID", txtMaThue.Text);

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
            }

            int maXe;
            if (cbXM.SelectedValue != null && int.TryParse(cbXM.SelectedValue.ToString(), out maXe))
            {
                try
                {
                    using (SqlConnection conn = connection.GetSqlConnection())
                    {
                        conn.Open();

                        // Kiểm tra xem mã xe đã tồn tại trong bảng ThueXeTheoGio chưa
                        string queryCheck = "SELECT COUNT(*) FROM ThueXeTheoGio WHERE MaXe = @MaXe";
                        SqlCommand commandCheck = new SqlCommand(queryCheck, conn);
                        commandCheck.Parameters.AddWithValue("@MaXe", maXe);
                        int count = (int)commandCheck.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Mã xe đã tồn tại, vui lòng chọn mã xe khác!");
                            return;
                        }
                        Getvaluetextbox();
                        // Nếu mã xe không trùng, thực hiện thêm dữ liệu vào bảng ThueXeTheoGio
                        string queryInsert = "INSERT INTO ThueXeTheoGio (MaThue, MaKhachHang, MaXe, MaNhanVien, ThoiGianBatDau, ThoiGianKetThuc) " +
                                             "VALUES (@MaThue, @MaKhachHang, @MaXe, @MaNhanVien, @ThoiGianBatDau, @ThoiGianKetThuc)";
                        SqlCommand commandInsert = new SqlCommand(queryInsert, conn);

                        // Thêm các tham số vào câu truy vấn để tránh lỗ hổng SQL Injection
                        commandInsert.Parameters.AddWithValue("@MaThue", TG.MaThueProperty);
                        commandInsert.Parameters.AddWithValue("@MaKhachHang", TG.MaKhachHangProperty);
                        commandInsert.Parameters.AddWithValue("@MaXe", TG.MaXeProperty);
                        commandInsert.Parameters.AddWithValue("@MaNhanVien", TG.MaNhanVienproperty);
                        commandInsert.Parameters.AddWithValue("@ThoiGianBatDau", TG.ThoiGianBatDauProperty);
                        commandInsert.Parameters.AddWithValue("@ThoiGianKetThuc", TG.ThoiGianKetThucProperty);

                        int rowsAffected = commandInsert.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm dữ liệu thành công!");
                            load_data(); // Load lại dữ liệu vào DataGridView sau khi thêm thành công
                        }
                        else
                        {
                            MessageBox.Show("Thêm dữ liệu thất bại!");
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            ThemDuLieu();
        }

        private void dgvNv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNv.Rows.Count >= 0)
            {
                txtMaThue.Text = dgvNv.SelectedRows[0].Cells[0].Value.ToString();
                txtMaKH.Text = dgvNv.SelectedRows[0].Cells[1].Value.ToString();
                cbXM.Text = dgvNv.SelectedRows[0].Cells[2].Value.ToString();
                txtMNV.Text = dgvNv.SelectedRows[0].Cells[3].Value.ToString();
                txtTineS.Text = dgvNv.SelectedRows[0].Cells[4].Value.ToString();
                txtTimeE.Text = dgvNv.SelectedRows[0].Cells[5].Value.ToString();
               

            }
        }

        private void SuaDuLieu()
        {
            if (CheckValue())
            {
                int maThue, maKhachHang, maNhanVien;
                DateTime thoiGianBatDau, thoiGianKetThuc;

                if (int.TryParse(txtMaThue.Text, out maThue) &&
                    int.TryParse(txtMaKH.Text, out maKhachHang) &&
                    int.TryParse(txtMNV.Text, out maNhanVien) &&
                    DateTime.TryParse(txtTineS.Text, out thoiGianBatDau) &&
                    DateTime.TryParse(txtTimeE.Text, out thoiGianKetThuc))
                {
                    int maXe;
                    if (cbXM.SelectedValue != null && int.TryParse(cbXM.SelectedValue.ToString(), out maXe))
                    {
                        try
                        {
                            using (SqlConnection conn = connection.GetSqlConnection())
                            {
                                conn.Open();

                                // Kiểm tra xem mã xe đã tồn tại trong bảng ThueXeTheoGio chưa
                                string queryCheck = "SELECT COUNT(*) FROM ThueXeTheoGio WHERE MaXe = @MaXe";
                                SqlCommand commandCheck = new SqlCommand(queryCheck, conn);
                                commandCheck.Parameters.AddWithValue("@MaXe", maXe);
                                int count = (int)commandCheck.ExecuteScalar();

                                if (count > 0)
                                {
                                    MessageBox.Show("Mã xe đã tồn tại, vui lòng chọn mã xe khác!");
                                    return;
                                }

                                // Sử dụng ID (ở đây là MaThue) để cập nhật thông tin
                                string queryUpdate = "UPDATE ThueXeTheoGio SET MaKhachHang = @MaKhachHang, MaXe = @MaXe, MaNhanVien = @MaNhanVien, " +
                                                     "ThoiGianBatDau = @ThoiGianBatDau, ThoiGianKetThuc = @ThoiGianKetThuc WHERE MaThue = @MaThue";
                                SqlCommand commandUpdate = new SqlCommand(queryUpdate, conn);

                                // Thêm các tham số vào câu truy vấn để tránh lỗ hổng SQL Injection
                                commandUpdate.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                                commandUpdate.Parameters.AddWithValue("@MaXe", maXe);
                                commandUpdate.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                                commandUpdate.Parameters.AddWithValue("@ThoiGianBatDau", thoiGianBatDau);
                                commandUpdate.Parameters.AddWithValue("@ThoiGianKetThuc", thoiGianKetThuc);
                                commandUpdate.Parameters.AddWithValue("@MaThue", maThue);

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
                    MessageBox.Show("Vui lòng nhập đúng định dạng dữ liệu!");
                }
            }
            else
            {
                MessageBox.Show("Mời bạn nhập đầy đủ thông tin!");
            }
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
                string query = "DELETE ThueXeTheoGio ";
                query += " WHERE MaThue = '" + choose + "'";
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
                string query = "SELECT * FROM ThueXeTheoGio WHERE MaThue =  @sMaNV";

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
