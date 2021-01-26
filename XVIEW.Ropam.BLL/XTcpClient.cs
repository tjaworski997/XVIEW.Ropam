using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL
{
    public class XTcpClient : TcpClient
    {
        public void Read(byte[] buffer, int idx, int len)
        {
            int i = len;
            while (i > 0)
            {
                int num = this.GetStream().Read(buffer, idx + (len - i), len - (len - i));
                i -= num;
                if (num == 0)
                {
                    throw new Exception("!");
                }
            }
        }

        public void Write(byte[] data, int idx, int len)
        {
            this.GetStream().Write(data, idx, len);
        }

        public void WriteByte(byte data)
        {
            this.GetStream().WriteByte(data);
        }
    }
}