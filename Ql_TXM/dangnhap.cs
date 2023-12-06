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
    public partial class dangnhap : Form
    {
        public dangnhap()
        {
            InitializeComponent();
        }
        modifyall modifyall = new modifyall();
        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPW.Text))
            {
                MessageBox.Show("Mời bạn nhập đầy đủ thông tin!");
                return;

            }

            string username = txtUser.Text;
            string password = txtPW.Text;

            string sql = "SELECT COUNT(*) FROM login WHERE username = @Username AND pass = @Password";

            using (SqlConnection con = connection.GetSqlConnection())
            {
                con.Open();

                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count > 0)
                {
                    // Đăng nhập thành công, chuyển hướng đến form trangchu
                    trangchu tc = new trangchu();
                    tc.Show();
                    this.Hide(); // Ẩn form đăng nhập sau khi đăng nhập thành công
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
                }

                con.Close();
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtPW_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
