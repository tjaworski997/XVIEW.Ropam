using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class User
    {
        public byte No { get; internal set; }
        public string Name { get; internal set; }
        public bool IsFree { get; internal set; }
    }
}