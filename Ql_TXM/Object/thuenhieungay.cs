using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ql_TXM.Object
{
    class thuenhieungay
    {
        private string MaThue;
        private string MaKhachHang;
        private int MaXe;
        private string MaNhanVien;
        private DateTime NgayBatDau;
        private DateTime NgayKetThuc;

        public thuenhieungay(string MaThue, string MaKhachHang, int MaXe, string MaNhanVien, DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            this.MaThue = MaThue;
            this.MaKhachHang = MaKhachHang;
            this.MaXe = MaXe;
            this.MaNhanVien = MaNhanVien;
            this.NgayBatDau = NgayBatDau;
            this.NgayKetThuc = NgayKetThuc;
        }

        public string MaThueProperty
        {
            get { return MaThue; }
            set { MaThue = value; }
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
        public DateTime NgayBatDauProperty
        {
            get { return NgayBatDau; }
            set { NgayBatDau = value; }
        }

        public DateTime ngayKetThucProperty
        {
            get { return NgayKetThuc; }
            set { NgayKetThuc = value; }
        }
    }
}
