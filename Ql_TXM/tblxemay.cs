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
    public partial class tblxemay : Form
    {
        public tblxemay()
        {
            InitializeComponent();
        }
        modifyall modifyall = new modifyall();
        xemay xm;
        public bool CheckValue()
        {
            if (string.IsNullOrWhiteSpace(txtMXM.Text) || string.IsNullOrWhiteSpace(txtTenxe.Text) ||
                string.IsNullOrWhiteSpace(txthang.Text) || string.IsNullOrWhiteSpace(txtGiaThue.Text))


            {
                MessageBox.Show("Mời bạn nhập đầy đủ thông tin!");
                return false;
            }

            return true;
        }
        public void Getvaluetextbox()
        {
            string MXM = txtMXM.Text;
            string Name = txtTenxe.Text;
            string hang = txthang.Text;
            float Gia = float.Parse(txtGiaThue.Text);
            xm = new xemay(MXM, Name, hang, Gia);
        }
        public void load_data()
        {
            dgvNv.DataSource = modifyall.Table(" select * from XeMay");
        }
        private void xemay_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckValue()) // Kiểm tra dữ liệu đã nhập
            {
                SqlConnection conn = connection.GetSqlConnection();

                string sqlCheck = "SELECT COUNT(*) FROM XeMay WHERE MaXe = @ID";
                SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn);
                cmdCheck.Parameters.AddWithValue("@ID", txtMXM.Text);

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

                string insert = "INSERT INTO XeMay(MaXe, TenXe, HangXe, GiaThue) VALUES(@MaXe, @TenXe, @HangXe, @GiaThue)";
                SqlCommand insertCmd = new SqlCommand(insert, conn);
                insertCmd.Parameters.AddWithValue("@MaXe", xm.MaXeProperty); // Đổi tên tham số và thuộc tính của xe tương ứng
                insertCmd.Parameters.AddWithValue("@TenXe", xm.TenXeProperty); // Đổi tên tham số và thuộc tính của xe tương ứng
                insertCmd.Parameters.AddWithValue("@HangXe", xm.HangXeProperty); // Đổi tên tham số và thuộc tính của xe tương ứng
                insertCmd.Parameters.AddWithValue("@GiaThue", xm.GiaThueProperty); // Đổi tên tham số và thuộc tính của xe tương ứng


                try
                {
                    conn.Open();
                    insertCmd.ExecuteNonQuery();
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
            if (string.IsNullOrEmpty(txtMXM.Text))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng!");
                return;
            }
            if (CheckValue())
            {
                SqlConnection con = connection.GetSqlConnection();
                string sql = "SELECT COUNT (*) FROM XeMay where MaXe= @IDDT";
                SqlCommand sqlCmd = new SqlCommand(sql, con);
                sqlCmd.Parameters.AddWithValue("@IDDT", txtMXM.Text);
                con.Open();
                int count = (int)sqlCmd.ExecuteScalar();
                if (count == 0)
                {
                    MessageBox.Show("Mã không tồn tại!");
                    return;
                }

                Getvaluetextbox();
                string update = "UPDATE XeMay SET TenXe = @TenXe, HangXe = @HangXe, GiaThue = @GiaThue WHERE MaXe = @ID";
                SqlCommand udt = new SqlCommand(update, con);
                udt.Parameters.AddWithValue("@ID", xm.MaXeProperty); // Đổi tên tham số và thuộc tính của xe tương ứng
                udt.Parameters.AddWithValue("@TenXe", xm.TenXeProperty); // Đổi tên tham số và thuộc tính của xe tương ứng
                udt.Parameters.AddWithValue("@HangXe", xm.HangXeProperty); // Đổi tên tham số và thuộc tính của xe tương ứng
                udt.Parameters.AddWithValue("@GiaThue", xm.GiaThueProperty); // Đổi tên tham số và thuộc tính của xe tương ứng


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
            if (dgvNv.Rows.Count > 1)
            {
                string choose = dgvNv.SelectedRows[0].Cells[0].Value.ToString();
                string query = "DELETE XeMay ";
                query += " WHERE MaXe = '" + choose + "'";
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
                MessageBox.Show("Vui lòng nhập tên xe để tìm!");
                return;
            }
            else
            {
                string query = "SELECT * FROM XeMay WHERE TenXe LIKE '%' + @TenXe + '%'";
                // Sử dụng LIKE để tìm kiếm một phần của tên xe và không cần phải chính xác giống hoàn toàn

                using (SqlConnection conn = connection.GetSqlConnection())
                {
                    DataTable resultTable = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenXe", name);
                        conn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(resultTable);
                    }

                    if (resultTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy xe có tên tương tự!");
                    }
                    dgvNv.DataSource = resultTable;
                }
            }
        }

        private void dgvNv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNv.Rows.Count >= 0)
            {
                txtMXM.Text = dgvNv.SelectedRows[0].Cells[0].Value.ToString();
                txtTenxe.Text = dgvNv.SelectedRows[0].Cells[1].Value.ToString();
                txthang.Text = dgvNv.SelectedRows[0].Cells[2].Value.ToString();
                txtGiaThue.Text = dgvNv.SelectedRows[0].Cells[3].Value.ToString();
            }
        }
    }
}
