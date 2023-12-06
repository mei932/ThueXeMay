using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ql_TXM.Object
{
    class nhanvien
    {
        private string MaNhanVien;
        private string HoTen;
        private string DiaChi;
        private string SoDienThoai;

        public nhanvien(string MaNhanVien, string HoTen, string DiaChi, string SoDienThoai)
        {
            this.MaNhanVien = MaNhanVien;
            this.HoTen = HoTen;
            this.DiaChi = DiaChi;
            this.SoDienThoai = SoDienThoai;
        }

        public string MaNhanVienProperty
        {
            get { return MaNhanVien; }
            set { MaNhanVien = value; }
        }

        public string HoTenProperty
        {
            get { return HoTen; }
            set { HoTen = value; }
        }

        public string DiaChiProperty
        {
            get { return DiaChi; }
            set { DiaChi = value; }
        }

        public string SoDienThoaiProperty
        {
            get { return SoDienThoai; }
            set { SoDienThoai = value; }
        }
    }
}
