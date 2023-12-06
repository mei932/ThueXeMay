
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Ql_TXM.Modify;
using Ql_TXM.Object;


namespace Ql_TXM
{
    public partial class tblKhachHang : Form
    {
        modifyall modifyall = new modifyall();
        khachhang kh;

        public tblKhachHang()
        {
            InitializeComponent();
        }
        public bool CheckValue()
        {
            if (string.IsNullOrWhiteSpace(txtMKH.Text) || string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtDC.Text) || string.IsNullOrWhiteSpace(txtSDT.Text) )
               

            {
                MessageBox.Show("Mời bạn nhập đầy đủ thông tin!");
                return false;
            }

            return true;
        }
        public void Getvaluetextbox()
        {
            string MKH = txtMKH.Text;
            string Name = txtName.Text;
            string DC = txtDC.Text;
            string SDT = txtSDT.Text;
            kh = new khachhang(MKH, Name, DC, SDT);
        }
        public void load_data()
        {
            dgvKH.DataSource = modifyall.Table(" select * from KhachHang");
        }
        private void tblKhachHang_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckValue()) // Kiểm tra dữ liệu đã nhập
            {
                SqlConnection conn = connection.GetSqlConnection();
    
                string sqlCheck = "SELECT COUNT(*) FROM KhachHang WHERE MaKhachHang = @ID";
                SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn);
                cmdCheck.Parameters.AddWithValue("@ID", txtMKH.Text);

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

                Getvaluetextbox();

                string query = "INSERT INTO KhachHang(MaKhachHang, HoTen, DiaChi, SoDienThoai)" +
                                "VALUES(@MaKH, @HoTen, @DiaChi, @SDT)";

                SqlCommand insert = new SqlCommand(query, conn);
                insert.Parameters.AddWithValue("@MaKH", kh.IDKhachHangProperty);
                insert.Parameters.AddWithValue("@HoTen", kh.NameProperty);
                insert.Parameters.AddWithValue("@DiaChi", kh.DiaChiProperty);
                insert.Parameters.AddWithValue("@SDT", kh.SDTProperty);
                

                try
                {
                    conn.Open();
                    insert.ExecuteNonQuery();
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMKH.Text))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng!");
                return;
            }
            if (CheckValue())
            {
                SqlConnection con = connection.GetSqlConnection();
                string sql = "SELECT COUNT (*) FROM KhachHang where MaKhachHang= @IDDT";
                SqlCommand sqlCmd = new SqlCommand(sql, con);
                sqlCmd.Parameters.AddWithValue("@IDDT", txtMKH.Text);
                con.Open();
                int count = (int)sqlCmd.ExecuteScalar();
                if (count == 0)
                {
                    MessageBox.Show("Mã không tồn tại!");
                    return;
                }

                Getvaluetextbox();
                string update = "UPDATE KhachHang SET HoTen = @HoTen, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai WHERE MaKhachHang = @MaKhachHang";
                SqlCommand udt = new SqlCommand(update, con);
                udt.Parameters.AddWithValue("@MaKhachHang", kh.IDKhachHangProperty); // Đây có thể là tên tham số mới trong mã của bạn
                udt.Parameters.AddWithValue("@HoTen", kh.NameProperty); // Đây có thể là tên tham số mới trong mã của bạn
                udt.Parameters.AddWithValue("@DiaChi", kh.DiaChiProperty); // Đây có thể là tên tham số mới trong mã của bạn
                udt.Parameters.AddWithValue("@SoDienThoai", kh.SDTProperty); // Đây có thể là tên tham số mới trong mã của bạn

                try
                {
                    if (MessageBox.Show("Bạn có muốn sửa lại dữ liệu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        udt.ExecuteNonQuery();
                        MessageBox.Show("Bạn đã sửa thông tin thành công!");
                        load_data();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi sửa: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKH.Rows.Count > 1)
            {
                string choose = dgvKH.SelectedRows[0].Cells[0].Value.ToString();
                string query = "DELETE KhachHang ";
                query += " WHERE MaKhachHang = '" + choose + "'";
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
                MessageBox.Show("Vui lòng nhập mã khách hàng hàng để tìm!");
                return;
            }
            else
            {
                string query = "SELECT * FROM KhachHang WHERE MaKhachHang =  @sMaNV";

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

                    dgvKH.DataSource = resultTable;
                }
            }
        }

        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
                if (dgvKH.Rows.Count >= 0)
                {
                    txtMKH.Text = dgvKH.SelectedRows[0].Cells[0].Value.ToString();
                    txtName.Text = dgvKH.SelectedRows[0].Cells[1].Value.ToString();
                    txtDC.Text = dgvKH.SelectedRows[0].Cells[2].Value.ToString();
                    txtSDT.Text = dgvKH.SelectedRows[0].Cells[3].Value.ToString();
                }
            
        }
    }
}
