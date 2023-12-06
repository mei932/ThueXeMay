using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ql_TXM.Object
{
    class xemay
    {
        private string MaXe;
        private string TenXe;
        private string HangXe;
        private float GiaThue;

        public xemay(string MaXe, string TenXe, string HangXe, float GiaThue)
        {
            this.MaXe = MaXe;
            this.TenXe = TenXe;
            this.HangXe = HangXe;
            this.GiaThue = GiaThue;
        }

        public string MaXeProperty
        {
            get { return MaXe; }
            set { MaXe = value; }
        }

        public string TenXeProperty
        {
            get { return TenXe; }
            set { TenXe = value; }
        }

        public string HangXeProperty
        {
            get { return HangXe; }
            set { HangXe = value; }
        }

        public float GiaThueProperty
        {
            get { return GiaThue; }
            set { GiaThue = value; }
        }
    }
}
