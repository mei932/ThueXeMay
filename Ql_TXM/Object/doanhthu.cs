using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ql_TXM.Object
{
    class doanhthu
    {
        private string MaHoaDon;
        private string MaKhachHang;
        private int MaXe;
        private string MaNhanVien;
        private DateTime NgayThanhToan;
        private float SoTien;

        public doanhthu(string MaHoaDon, string MaKhachHang, int MaXe,string MaNhanVien, DateTime NgayThanhToan, float SoTien)
        {
            this.MaHoaDon = MaHoaDon;
            this.MaKhachHang = MaKhachHang;
            this.MaXe = MaXe;
            this.MaNhanVien = MaNhanVien;
            this.NgayThanhToan = NgayThanhToan;
            this.SoTien = SoTien;
        }

        public string MaHoaDonProperty
        {
            get { return MaHoaDon; }
            set { MaHoaDon = value; }
        }

        public string MaKhachHangProperty
        {
            get { return MaKhachHang; }
            set { MaKhachHang = value; }
        }

        public int MaXeProperty
        {
            get { return MaXe; }
            set { MaXe = value; }
        }
        public string MaNhanVienproperty
        {
            get { return MaNhanVien; }
            set { MaNhanVien = value; }
        }
        public DateTime NgayThanhToanProperty
        {
            get { return NgayThanhToan; }
            set { NgayThanhToan = value; }
        }

        public float SoTienProperty
        {
            get { return SoTien; }
            set { SoTien = value; }
        }
    }
}
