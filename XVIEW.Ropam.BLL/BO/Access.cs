using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class Access
    {
        public byte UserNo { get; internal set; }
        public bool IsZone1 { get; internal set; }
        public bool IsRemote { get; internal set; }
        public bool CanLockInputs { get; internal set; }
        public bool IsZone2 { get; internal set; }
        public string UserName { get; internal set; }
    }
}