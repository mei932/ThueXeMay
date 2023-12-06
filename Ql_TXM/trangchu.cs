using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ql_TXM
{
    public partial class trangchu : Form
    {
        public trangchu()
        {
            InitializeComponent();
        }

        private Form currentFormChild;
        // Sử lí sự kiện bấm vào icon logo
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
         
        private void btnKhanhHang_Click(object sender, EventArgs e)
        {
            OpenChildForm( new tblKhachHang() );
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new tblNhanVien() );
        }

        private void btnXeMay_Click(object sender, EventArgs e)
        {
            OpenChildForm(new tblxemay());
        }

        private void btnThuetheogio_Click(object sender, EventArgs e)
        {
            OpenChildForm(new tblthueTheoNgay());
        }

        private void btnThuenhieungay_Click(object sender, EventArgs e)
        {
            OpenChildForm(new tblThueNhieuNgay());
        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new tblDoanhThu());
        }

        private void thốngKêTheoNămToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new thongke());
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            dangnhap tc = new dangnhap();
            tc.Show();
            this.Hide();
        }
    }


}
