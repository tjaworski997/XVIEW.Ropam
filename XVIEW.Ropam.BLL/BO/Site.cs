using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class Site
    {
        public int Id { get; set; }
        public bool CanEditMenu { get; internal set; }
        public string DevicePhone { get; internal set; }

        public string Code { get; set; }

        public string Ip { get; set; }
        public int Port { get; set; }
        public object DeviceLang { get; internal set; }
        public string DeviceId2 { get; internal set; }
        public string MobileKey { get; internal set; }
        public int UserNo { get; internal set; }
        public DateTime LastLoginTime { get; internal set; }
        public string TcpCode { get; internal set; }

        //public string Ip2 { get; set; }
        //public int DefaultPort { get; set; }
    }
}