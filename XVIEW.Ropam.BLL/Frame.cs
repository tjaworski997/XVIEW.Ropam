using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL
{
    public class Frame
    {
        // Token: 0x06000192 RID: 402 RVA: 0x0000E365 File Offset: 0x0000C565
        public void AddData(int datapart)
        {
            this.AddData(BitConverter.GetBytes(datapart));
        }

        // Token: 0x06000193 RID: 403 RVA: 0x0000E373 File Offset: 0x0000C573
        public void AddData(uint datapart)
        {
            this.AddData(BitConverter.GetBytes(datapart));
        }

        // Token: 0x06000194 RID: 404 RVA: 0x0000E381 File Offset: 0x0000C581
        public void AddData(ushort datapart)
        {
            this.AddData(BitConverter.GetBytes(datapart));
        }

        // Token: 0x06000195 RID: 405 RVA: 0x0000E38F File Offset: 0x0000C58F
        public void AddData(float datapart)
        {
            this.AddData(BitConverter.GetBytes(datapart));
        }

        internal void AddData(object code)
        {
            throw new NotImplementedException();
        }

        // Token: 0x06000197 RID: 407 RVA: 0x0000E3DE File Offset: 0x0000C5DE
        public void AddData(string datapart)
        {
            this.AddData(Frame.encoding.GetBytes(datapart));
        }

        // Token: 0x06000198 RID: 408 RVA: 0x0000E3F1 File Offset: 0x0000C5F1
        public void AddData(byte data)
        {
            this.AddData(new byte[]
            {
                data
            });
        }

        // Token: 0x06000199 RID: 409 RVA: 0x0000E404 File Offset: 0x0000C604
        public void AddData(byte[] datapart)
        {
            if (datapart == null || datapart.Length == 0)
            {
                return;
            }
            if (this.data == null)
            {
                this.data = datapart;
                return;
            }
            byte[] array = new byte[this.data.Length + datapart.Length];
            Array.Copy(this.data, 0, array, 0, this.data.Length);
            Array.Copy(datapart, 0, array, this.data.Length, datapart.Length);
            this.data = array;
        }

        // Token: 0x06000196 RID: 406 RVA: 0x0000E3A0 File Offset: 0x0000C5A0
        public byte[] AddRandomData(int len)
        {
            Random random = new Random();
            byte[] array = new byte[len];
            for (int i = 0; i < len; i++)
            {
                array[i] = (byte)(random.Next() % 256);
            }
            this.AddData(array);
            return array;
        }

        // Token: 0x06000179 RID: 377 RVA: 0x0000DBB1 File Offset: 0x0000BDB1
        public bool CheckCRC1()
        {
            return this.crc1ok;
        }

        // Token: 0x0600017A RID: 378 RVA: 0x0000DBB9 File Offset: 0x0000BDB9
        public bool CheckCRC2()
        {
            return this.crc2ok;
        }

        // Token: 0x06000191 RID: 401 RVA: 0x0000E35C File Offset: 0x0000C55C
        public void ClearData()
        {
            this.data = null;
        }

        // Token: 0x0600017E RID: 382 RVA: 0x0000E080 File Offset: 0x0000C280
        public Frame Clone()
        {
            return new Frame
            {
                index = this.index,
                length = this.length,
                type = this.type,
                data = this.data,
                crc1 = this.crc1,
                crc2 = this.crc2
            };
        }

        // Token: 0x0600017F RID: 383 RVA: 0x0000E0DA File Offset: 0x0000C2DA
        public Frame CloneWithoutData()
        {
            Frame expr_06 = this.Clone();
            expr_06.length = 0;
            expr_06.data = null;
            return expr_06;
        }

        // Token: 0x0600017B RID: 379 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
        private static byte GetCRC(byte[] bytes, int start, int end)
        {
            byte b = 0;
            for (int i = start; i < end; i++)
            {
                byte b2 = bytes[i];
                for (int j = 0; j < 8; j++)
                {
                    if (((b2 ^ b) & 1) == 1)
                    {
                        b = (byte)((byte)((b ^ 24) >> 1) | 128);
                    }
                    else
                    {
                        b = (byte)(b >> 1);
                    }
                    b2 = (byte)(b2 >> 1);
                }
            }
            return b;
        }

        // Token: 0x06000182 RID: 386 RVA: 0x0000E174 File Offset: 0x0000C374
        public byte[] GetData(int idx)
        {
            int num = this.data.Length - idx;
            byte[] array = new byte[num];
            Array.Copy(this.data, idx, array, 0, num);
            return array;
        }

        // Token: 0x06000183 RID: 387 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
        public byte[] GetData(int idx, int len)
        {
            byte[] array = new byte[len];
            Array.Copy(this.data, idx, array, 0, len);
            return array;
        }

        // Token: 0x06000186 RID: 390 RVA: 0x0000E1F7 File Offset: 0x0000C3F7
        public int GetInt()
        {
            return this.GetInt(0);
        }

        // Token: 0x06000187 RID: 391 RVA: 0x0000E200 File Offset: 0x0000C400
        public int GetInt(int idx)
        {
            return BitConverter.ToInt32(new byte[]
            {
                this.data[idx],
                this.data[idx + 1],
                this.data[idx + 2],
                this.data[idx + 3]
            }, 0);
        }

        // Token: 0x06000176 RID: 374 RVA: 0x0000D8C0 File Offset: 0x0000BAC0
        public string GetIp()
        {
            byte[] bytes = BitConverter.GetBytes(this.ip);
            return string.Join<byte>(".", Enumerable.Reverse<byte>(bytes));
        }

        // Token: 0x0600018A RID: 394 RVA: 0x0000E27A File Offset: 0x0000C47A
        public string GetString()
        {
            return this.GetString(Frame.encoding);
        }

        // Token: 0x0600018B RID: 395 RVA: 0x0000E287 File Offset: 0x0000C487
        public string GetString(Encoding encoding)
        {
            return Frame.GetString(this.data, 0, this.data.Length, encoding);
        }

        // Token: 0x0600018C RID: 396 RVA: 0x0000E2A0 File Offset: 0x0000C4A0
        public string GetString(int idx)
        {
            int len = this.data.Length - idx;
            return Frame.GetString(this.data, idx, len, Frame.encoding);
        }

        // Token: 0x0600018D RID: 397 RVA: 0x0000E2CC File Offset: 0x0000C4CC
        public string GetString(int idx, Encoding encoding)
        {
            int len = this.data.Length - idx;
            return Frame.GetString(this.data, idx, len, encoding);
        }

        // Token: 0x0600018E RID: 398 RVA: 0x0000E2F2 File Offset: 0x0000C4F2
        public string GetString(int idx, int len)
        {
            return Frame.GetString(this.data, idx, len, Frame.encoding);
        }

        // Token: 0x0600018F RID: 399 RVA: 0x0000E306 File Offset: 0x0000C506
        public string GetString(int idx, int len, Encoding encoding)
        {
            return Frame.GetString(this.data, idx, len, encoding);
        }

        // Token: 0x06000190 RID: 400 RVA: 0x0000E318 File Offset: 0x0000C518
        public static string GetString(byte[] data, int start, int len, Encoding encoding)
        {
            if (data == null)
            {
                return null;
            }
            int num = -1;
            for (int i = start; i < start + len; i++)
            {
                if (data[i] == 0)
                {
                    num = i;
                    break;
                }
            }
            if (num == -1)
            {
                return encoding.GetString(data, start, len);
            }
            return encoding.GetString(data, start, num - start);
        }

        // Token: 0x06000184 RID: 388 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
        public ushort GetUshort()
        {
            return this.GetUshort(0);
        }

        // Token: 0x06000185 RID: 389 RVA: 0x0000E1D1 File Offset: 0x0000C3D1
        public ushort GetUshort(int idx)
        {
            return BitConverter.ToUInt16(new byte[]
            {
                this.data[idx],
                this.data[idx + 1]
            }, 0);
        }

        // Token: 0x06000177 RID: 375 RVA: 0x0000D8EC File Offset: 0x0000BAEC
        public void Load(byte[] bytes)
        {
            this.index = BitConverter.ToUInt16(new byte[]
            {
                bytes[0],
                bytes[1]
            }, 0);
            this.length = BitConverter.ToUInt16(new byte[]
            {
                bytes[2],
                bytes[3]
            }, 0);
            this.crc2 = bytes[(int)((ushort)Frame.HeaderSize + this.length - 1)];
            this.crc2ok = (this.crc2 == Frame.GetCRC(bytes, 0, (int)((ushort)Frame.HeaderSize + this.length - 1)));
            if (this.index != 65535)
            {
                if (Frame.Encrypt)
                {
                    this.tcpVector[0] = bytes[0];
                    this.tcpVector[1] = bytes[1];
                    VMPC.Encode(bytes, 4, (int)((ushort)Frame.HeaderSize + this.length - 1), this.tcpKey, this.tcpVector, 16);
                }
                this.type = bytes[4];
                this.data = new byte[(int)this.length];
                for (int i = 0; i < (int)this.length; i++)
                {
                    this.data[i] = bytes[5 + i];
                }
            }
            else
            {
                if (Frame.Encrypt)
                {
                    VMPC.Encode(bytes, 13, (int)((ushort)Frame.HeaderSize + this.length - 1), this.tcpKey, this.tcpVector, 16);
                }
                this.type = bytes[4];
                this.data = new byte[(int)(this.length - 8)];
                for (int j = 0; j < (int)(this.length - 8); j++)
                {
                    this.data[j] = bytes[13 + j];
                }
            }
            this.crc1 = bytes[(int)((ushort)Frame.HeaderSize + this.length - 2)];
            this.crc1ok = (this.crc1 == Frame.GetCRC(bytes, 4, (int)((ushort)Frame.HeaderSize + this.length - 2)));
        }

        // Token: 0x06000172 RID: 370 RVA: 0x0000D840 File Offset: 0x0000BA40
        public void Reset()
        {
            this.tcpKey = Frame.encoding.GetBytes("aaaaaaaaaaaaaaaa");
            this.tcpVector = new byte[]
            {
                4,
                90,
                118,
                67,
                90,
                234,
                45,
                11,
                123,
                90,
                87,
                43,
                98,
                23,
                123,
                89
            };
            this.ClearData();
        }

        // Token: 0x06000175 RID: 373 RVA: 0x0000D891 File Offset: 0x0000BA91
        public void SetIp(byte[] bytes)
        {
            this.ip = BitConverter.ToUInt32(new byte[]
            {
                bytes[3],
                bytes[2],
                bytes[1],
                bytes[0]
            }, 0);
        }

        // Token: 0x06000181 RID: 385 RVA: 0x0000E134 File Offset: 0x0000C334
        public byte[] SetRandomData(int len)
        {
            Random random = new Random();
            byte[] array = new byte[len];
            for (int i = 0; i < len; i++)
            {
                array[i] = (byte)(random.Next() % 256);
            }
            this.data = array;
            return array;
        }

        // Token: 0x06000180 RID: 384 RVA: 0x0000E0F0 File Offset: 0x0000C2F0
        public void SetString(string data)
        {
            this.data = new byte[data.Length];
            for (int i = 0; i < this.data.Length; i++)
            {
                this.data[i] = (byte)data[i];
            }
        }

        // Token: 0x06000173 RID: 371 RVA: 0x0000D875 File Offset: 0x0000BA75
        public void SetTcpKey(string tcpKey)
        {
            this.tcpKey = Frame.encoding.GetBytes(tcpKey);
        }

        // Token: 0x06000174 RID: 372 RVA: 0x0000D888 File Offset: 0x0000BA88
        public void SetTcpVector(byte[] tcpVector)
        {
            this.tcpVector = tcpVector;
        }

        // Token: 0x06000188 RID: 392 RVA: 0x0000E240 File Offset: 0x0000C440
        public void SetUshort(ushort val)
        {
            this.data = new byte[2];
            this.SetUshort(val, 0);
        }

        // Token: 0x06000189 RID: 393 RVA: 0x0000E256 File Offset: 0x0000C456
        public void SetUshort(ushort val, int idx)
        {
            this.data[idx] = BitConverter.GetBytes(val)[0];
            this.data[idx + 1] = BitConverter.GetBytes(val)[1];
        }

        // Token: 0x06000178 RID: 376 RVA: 0x0000DA98 File Offset: 0x0000BC98
        public byte[] ToBytes()
        {
            this.length = ((this.data != null) ? ((ushort)this.data.Length) : 0);
            byte[] array = new byte[(int)((ushort)Frame.HeaderSize + this.length)];
            array[0] = BitConverter.GetBytes(this.index)[0];
            array[1] = BitConverter.GetBytes(this.index)[1];
            array[2] = BitConverter.GetBytes(this.length)[0];
            array[3] = BitConverter.GetBytes(this.length)[1];
            array[4] = this.type;
            for (int i = 0; i < (int)this.length; i++)
            {
                array[5 + i] = this.data[i];
            }
            array[(int)((ushort)Frame.HeaderSize + this.length - 2)] = Frame.GetCRC(array, 4, array.Length - 2);
            if (Frame.Encrypt)
            {
                this.tcpVector[0] = array[0];
                this.tcpVector[1] = array[1];
                VMPC.Encode(array, 4, (int)((ushort)Frame.HeaderSize + this.length - 1), this.tcpKey, this.tcpVector, 16);
            }
            array[(int)((ushort)Frame.HeaderSize + this.length - 1)] = Frame.GetCRC(array, 0, array.Length - 1);
            return array;
        }

        // Token: 0x0600017C RID: 380 RVA: 0x0000DC13 File Offset: 0x0000BE13
        public override string ToString()
        {
            return this.ToString(null);
        }

        // Token: 0x0600017D RID: 381 RVA: 0x0000DC1C File Offset: 0x0000BE1C
        public string ToString(string id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("\r\n");
            stringBuilder.Append("Frame " + ((id != null) ? ("(" + id + ") ") : "") + "{\r\n");
            stringBuilder.Append("  index:    " + this.index + "\r\n");
            stringBuilder.Append("  length:   " + ((this.data != null) ? ((ushort)this.data.Length) : 0) + "\r\n");
            switch (this.type)
            {
                case 0:
                    stringBuilder.Append("  type:     " + this.type + " - LOGIN\r\n");
                    break;

                case 1:
                    stringBuilder.Append("  type:     " + this.type + " - NSUPPORTED\r\n");
                    break;

                case 2:
                    stringBuilder.Append("  type:     " + this.type + " - PINGFR\r\n");
                    break;

                case 3:
                    stringBuilder.Append("  type:     " + this.type + " - SETOUT\r\n");
                    break;

                case 4:
                    stringBuilder.Append("  type:     " + this.type + " - SETOUTS\r\n");
                    break;

                case 5:
                    stringBuilder.Append("  type:     " + this.type + " - GETCONFIG\r\n");
                    break;

                case 6:
                    stringBuilder.Append("  type:     " + this.type + " - CONFIG\r\n");
                    break;

                case 7:
                    stringBuilder.Append("  type:     " + this.type + " - ARM\r\n");
                    break;

                case 8:
                    stringBuilder.Append("  type:     " + this.type + " - DISARM\r\n");
                    break;

                case 9:
                    stringBuilder.Append("  type:     " + this.type + " - GETEVENTS\r\n");
                    break;

                case 10:
                    stringBuilder.Append("  type:     " + this.type + " - EVENTS\r\n");
                    break;

                case 11:
                    stringBuilder.Append("  type:     " + this.type + " - MESSAGE\r\n");
                    break;

                case 12:
                    stringBuilder.Append("  type:     " + this.type + " - LOCKINPUTS\r\n");
                    break;

                case 13:
                    stringBuilder.Append("  type:     " + this.type + " - LOGOUT\r\n");
                    break;

                case 14:
                    stringBuilder.Append("  type:     " + this.type + " - ACK\r\n");
                    break;

                case 15:
                    stringBuilder.Append("  type:     " + this.type + " - NACK\r\n");
                    break;

                case 16:
                    stringBuilder.Append("  type:     " + this.type + " - STATUS\r\n");
                    break;

                case 17:
                    stringBuilder.Append("  type:     " + this.type + " - RECONNECT\r\n");
                    break;

                default:
                    stringBuilder.Append("  type:     " + this.type + "\r\n");
                    break;
            }
            if (this.data != null && this.data.Length != 0)
            {
                StringBuilder stringBuilder2 = new StringBuilder("[");
                for (int i = 0; i < this.data.Length; i++)
                {
                    stringBuilder2.Append((i == 0) ? this.data[i].ToString() : ("," + this.data[i].ToString()));
                }
                stringBuilder2.Append("] ");
                stringBuilder.Append("  data:     " + stringBuilder2.ToString() + "\r\n");
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        // Token: 0x04000087 RID: 135
        private byte crc1;

        // Token: 0x04000079 RID: 121
        public bool crc1ok = true;

        // Token: 0x04000088 RID: 136
        private byte crc2;

        // Token: 0x0400007A RID: 122
        public bool crc2ok = true;

        // Token: 0x04000086 RID: 134
        public byte[] data;

        // Token: 0x04000076 RID: 118
        private static Encoding encoding = Encoding.GetEncoding("UTF-8");

        // Token: 0x04000078 RID: 120
        public static bool Encrypt = true;

        // Token: 0x0400007E RID: 126
        public static byte HeaderSize = 7;

        // Token: 0x04000083 RID: 131
        public ushort index = 1;

        // Token: 0x0400007D RID: 125
        public uint ip;

        // Token: 0x04000082 RID: 130
        public static ushort LastIndex = 1;

        // Token: 0x04000084 RID: 132
        public int length;

        // Token: 0x04000077 RID: 119
        public bool log = true;

        // Token: 0x04000080 RID: 128
        public DateTime receiveTime;

        // Token: 0x04000081 RID: 129
        public int sendCnt;

        // Token: 0x0400007F RID: 127
        public DateTime sendTime;

        // Token: 0x0400007B RID: 123
        public byte[] tcpKey = Frame.encoding.GetBytes("aaaaaaaaaaaaaaaa");

        // Token: 0x0400007C RID: 124
        public byte[] tcpVector = new byte[]
        {
            4,
            90,
            118,
            67,
            90,
            234,
            45,
            11,
            123,
            90,
            87,
            43,
            98,
            23,
            123,
            89
        };

        // Token: 0x04000085 RID: 133
        public byte type;

        // Token: 0x02000106 RID: 262
        public enum Types : byte
        {
            // Token: 0x0400045A RID: 1114
            Login,

            // Token: 0x0400045B RID: 1115
            Nsupported,

            // Token: 0x0400045C RID: 1116
            Pingfr,

            // Token: 0x0400045D RID: 1117
            SetOut,

            // Token: 0x0400045E RID: 1118
            SetOuts,

            // Token: 0x0400045F RID: 1119
            GetConfig,

            // Token: 0x04000460 RID: 1120
            Config,

            // Token: 0x04000461 RID: 1121
            Arm,

            // Token: 0x04000462 RID: 1122
            DisArm,

            // Token: 0x04000463 RID: 1123
            GetEvents,

            // Token: 0x04000464 RID: 1124
            Events,

            // Token: 0x04000465 RID: 1125
            Message,

            // Token: 0x04000466 RID: 1126
            LockInputs,

            // Token: 0x04000467 RID: 1127
            Logout,

            // Token: 0x04000468 RID: 1128
            Ack,

            // Token: 0x04000469 RID: 1129
            Nack,

            // Token: 0x0400046A RID: 1130
            Status,

            // Token: 0x0400046B RID: 1131
            Reconnect,

            // Token: 0x0400046C RID: 1132
            SmsVirtual,

            // Token: 0x0400046D RID: 1133
            Ussd,

            // Token: 0x0400046E RID: 1134
            CheckAccess = 30,

            // Token: 0x0400046F RID: 1135
            ReadUserNames,

            // Token: 0x04000470 RID: 1136
            DeleteUserCodes,

            // Token: 0x04000471 RID: 1137
            ChangeUserCode,

            // Token: 0x04000472 RID: 1138
            NewUserCode,

            // Token: 0x04000473 RID: 1139
            ServiceConnectRequest,

            // Token: 0x04000474 RID: 1140
            ThermostatSetMode,

            // Token: 0x04000475 RID: 1141
            ThermostatSetPoint,

            // Token: 0x04000476 RID: 1142
            ThermostatSetProfile,

            // Token: 0x04000477 RID: 1143
            ThermostatGetProfile,

            // Token: 0x04000478 RID: 1144
            ExtendedStatus,

            // Token: 0x04000479 RID: 1145
            OpenWicket,

            // Token: 0x0400047A RID: 1146
            Panic,

            // Token: 0x0400047B RID: 1147
            Fire,

            // Token: 0x0400047C RID: 1148
            UserChange,

            // Token: 0x0400047D RID: 1149
            GetUserMask,

            // Token: 0x0400047E RID: 1150
            SendMessage,

            // Token: 0x0400047F RID: 1151
            SyncUserMask,

            // Token: 0x04000480 RID: 1152
            SilentClose = 100
        }

        internal void SetTcpKey(object tcpCode)
        {
            throw new NotImplementedException();
        }
    }
}