using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ql_TXM.Object
{
    class khachhang
    {
        private string IDKhachHang;
        private string name;
        private string DiaChi;
        private string SDT;

        public khachhang(string IDKhachHang, string name,  string DiaChi, string SDT)
        {
            this.IDKhachHang = IDKhachHang;
            this.name = name;
            this.DiaChi = DiaChi;
            this.SDT = SDT;
           
        }

        public string IDKhachHangProperty
        {
            get { return IDKhachHang; }
            set { IDKhachHang = value; }
        }

        public string NameProperty
        {
            get { return name; }
            set { name = value; }
        }
        public string DiaChiProperty
        {
            get { return DiaChi; }
            set { DiaChi = value; }
        }

        public string SDTProperty
        {
            get { return SDT; }
            set { SDT = value; }
        }

        
    }
}
