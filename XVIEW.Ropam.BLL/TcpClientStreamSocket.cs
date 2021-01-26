using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using XVIEW.Ropam.BLL.BO;

namespace XVIEW.Ropam.BLL
{
    public class TcpClientStreamSocket
    {
        public TcpClientStreamSocket()
        {
            client = new TcpClient();
        }

        public void ClearReadBuffer()
        {
            client = new TcpClient();

            byte[] array = new byte[4096];
            NetworkStream stream = this.client.GetStream();
            while (stream.DataAvailable)
            {
                stream.Read(array, 0, array.Length);
            }
        }

        public void Connect(string host, int port)
        {
            this.Disconnect();
            Log.Trace($"Łączę się z {host}:{port} via IPV4");
            this.client = new TcpClient();
            this.client.BeginConnect(host, port, null, null).AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds((double)TcpClientStreamSocket.connectionTimeout));
            if (!this.client.Connected)
            {
                throw new TimeoutException();
            }
        }

        public void Disconnect()
        {
            if (this.client != null)
            {
                try
                {
                    this.Flush();
                    //tja4
                    //this.client.Dispose();
                    this.client.Close();
                }
                catch (Exception)
                {
                }
                this.client = null;
            }
        }

        // Token: 0x06000702 RID: 1794 RVA: 0x00028C80 File Offset: 0x00026E80
        public void Flush()
        {
            this.client.GetStream().Flush();
        }

        // Token: 0x06000703 RID: 1795 RVA: 0x00028C94 File Offset: 0x00026E94
        public void Read(byte[] buffer, int idx, int len)
        {
            int i = len;
            while (i > 0)
            {
                int num = this.client.GetStream().Read(buffer, idx + (len - i), len - (len - i));
                i -= num;
                if (num == 0)
                {
                    throw new SocketClosedException();
                }
            }
        }

        // Token: 0x06000704 RID: 1796 RVA: 0x00028CD2 File Offset: 0x00026ED2
        public void Write(byte[] data, int idx, int len)
        {
            this.client.GetStream().Write(data, idx, len);
        }

        // Token: 0x06000705 RID: 1797 RVA: 0x00028CE7 File Offset: 0x00026EE7
        public void WriteByte(byte data)
        {
            this.client.GetStream().WriteByte(data);
        }

        // Token: 0x170001D9 RID: 473
        public int Available
        {
            // Token: 0x060006F8 RID: 1784 RVA: 0x00028B40 File Offset: 0x00026D40
            get
            {
                return this.client.Available;
            }
        }

        // Token: 0x170001DA RID: 474
        public int ConnectionTimeout
        {
            // Token: 0x060006F9 RID: 1785 RVA: 0x00028B4D File Offset: 0x00026D4D
            get
            {
                return TcpClientStreamSocket.connectionTimeout;
            }
            // Token: 0x060006FA RID: 1786 RVA: 0x00028B54 File Offset: 0x00026D54
            set
            {
                TcpClientStreamSocket.connectionTimeout = value;
            }
        }

        public int ReceiveTimeout
        {
            get
            {
                return this.client.ReceiveTimeout;
            }
            set
            {
                this.client.ReceiveTimeout = value;
            }
        }

        public int SendTimeout
        {
            get
            {
                return this.client.SendTimeout;
            }
            set
            {
                this.client.SendTimeout = value;
            }
        }

        public TcpClient client;

        public static int connectionTimeout = 2000;
    }
}