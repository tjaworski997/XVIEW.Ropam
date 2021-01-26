using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class Message
    {
        public int Site { get; set; }
        public int Channel { get; set; }
        public byte Type { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}