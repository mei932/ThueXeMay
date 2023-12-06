using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ql_TXM.Object
{
    class thuetheogio
    {
        private string MaThue;
        private string MaKhachHang;
        private int MaXe;
        private string MaNhanVien;
        private DateTime ThoiGianBatDau;
        private DateTime ThoiGianKetThuc;

        public thuetheogio(string MaThue, string MaKhachHang, int MaXe,string MaNhanVien, DateTime ThoiGianBatDau, DateTime ThoiGianKetThuc)
        {
            this.MaThue = MaThue;
            this.MaKhachHang = MaKhachHang;
            this.MaXe = MaXe;
            this.MaNhanVien = MaNhanVien;
            this.ThoiGianBatDau = ThoiGianBatDau;
            this.ThoiGianKetThuc = ThoiGianKetThuc;
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
        public DateTime ThoiGianBatDauProperty
        {
            get { return ThoiGianBatDau; }
            set { ThoiGianBatDau = value; }
        }

        public DateTime ThoiGianKetThucProperty
        {
            get { return ThoiGianKetThuc; }
            set { ThoiGianKetThuc = value; }
        }
    }
}
