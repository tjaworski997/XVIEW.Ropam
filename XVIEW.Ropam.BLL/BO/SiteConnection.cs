using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class SiteConnection
    {
        private DB DB = new DB();

        public SiteConnection()
        {
            Site = new Site();

            Site.TcpCode = Config.KluczSzyfrowaniaSzyfrowaniaTCP;
            Site.Code = Config.HasloKomunikacji;
            Site.Ip = Config.AlarmIp;
            Site.Port = Config.AlarmPort;

            Client = new TcpClientStreamSocket();
        }

        // Token: 0x06000200 RID: 512 RVA: 0x00010BF4 File Offset: 0x0000EDF4
        public void ArmZone(byte mask, bool night)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("ArmZone");
                }
                this.frame.ClearData();
                this.frame.type = 7;
                this.frame.AddData(new byte[]
                {
                    mask
                });
                this.frame.AddData(new byte[]
                {
                   (byte)( night ? 1 : 0)
                });
                this.frame.AddData(this.Site.Code);
                this.SendFrame();
            }
        }

        // Token: 0x06000210 RID: 528 RVA: 0x00011418 File Offset: 0x0000F618
        public void ChangeUserCode(string newCode)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("ChangeUserCode");
                }
                this.frame.type = 33;
                this.frame.ClearData();
                this.frame.AddData(this.Site.Code);
                this.frame.AddData(0);
                this.frame.AddData(newCode);
                this.SendFrame();
            }
        }

        // Token: 0x0600020E RID: 526 RVA: 0x00011348 File Offset: 0x0000F548
        public void CheckAccess(string code)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("CheckAccess");
                }
                this.frame.type = 30;
                this.frame.ClearData();
                this.frame.AddData(code);
                this.SendFrame();
            }
        }

        // Token: 0x060001F3 RID: 499 RVA: 0x0000FC08 File Offset: 0x0000DE08
        public void Close(int reason)
        {
            try
            {
                this.Stop();
                if (this.Logged)
                {
                    this.Logout();
                }
                if (this.Connected)
                {
                    if (reason != 2 && reason != 5)
                    {
                        Action<Site, int> expr_2A = this.OnClosed;
                        if (expr_2A != null)
                        {
                            expr_2A.Invoke(this.Site, reason);
                        }
                    }
                    this.Client.Disconnect();
                    this.Connected = false;
                    this.frame.Reset();
                }
            }
            catch (Exception)
            {
            }
        }

        public bool Connect()
        {
            Client.ConnectionTimeout = 2000;
            var flag = this.ConnectEndpoint(this.Site.Ip, this.Site.Port);
            if (flag)
            {
                this.Channel = SiteConnection.Channels.External;
            }

            return flag;
        }

        // Token: 0x060001F2 RID: 498 RVA: 0x0000FB9C File Offset: 0x0000DD9C
        public bool ConnectEndpoint(string host, int port)
        {
            bool result;
            try
            {
                try
                {
                    this.Close(0);
                }
                catch (Exception)
                {
                }
                this.Client.Connect(host, port);
                this.Connected = true;
                result = true;
            }
            catch (Exception ex)
            {
                this.Connected = false;
                throw new TcpConnectException("Error: " + ex.Message);
            }
            return result;
        }

        // Token: 0x06000211 RID: 529 RVA: 0x000114AC File Offset: 0x0000F6AC
        public void DeleteUserCodes(bool[] mask)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("DeleteUserCodes");
                }
                this.frame.type = 32;
                this.frame.ClearData();
                this.frame.AddData(this.Site.Code);
                this.frame.AddData(0);
                int num = 0;
                for (int i = 0; i < 32; i++)
                {
                    if (mask[i])
                    {
                        num |= 1 << i;
                    }
                }
                this.frame.AddData(num);
                this.SendFrame();
            }
        }

        // Token: 0x06000201 RID: 513 RVA: 0x00010CA0 File Offset: 0x0000EEA0
        public void DisarmZone(byte mask)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("DisarmZone");
                }
                this.frame.ClearData();
                this.frame.type = 8;
                this.frame.AddData(new byte[]
                {
                    mask
                });
                this.frame.AddData(this.Site.Code);
                this.SendFrame();
            }
        }

        public Task Delay(TimeSpan ts)
        {
            var tcs = new TaskCompletionSource<bool>();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += (obj, args) =>
            {
                tcs.TrySetResult(true);
            };
            timer.Interval = ts.TotalMilliseconds;
            timer.AutoReset = false;
            timer.Start();
            return tcs.Task;
        }

        // Token: 0x060001EC RID: 492 RVA: 0x0000EDCC File Offset: 0x0000CFCC
        public void DoWork()
        {
            while (this.working)
            {
                try
                {
                    Delay(TimeSpan.FromMilliseconds(200.0)).Wait();

                    lock (this)
                    {
                        if (this.sleep == 1 || this.sleep == 2)
                        {
                            if (this.sleep == 1)
                            {
                                this.sleep = 2;
                            }
                            Delay(TimeSpan.FromSeconds(1.0)).Wait();
                        }
                        else
                        {
                            DateTime arg_7F_0 = this.lastFrame;
                            DateTime now = DateTime.Now;
                            if (arg_7F_0 < now.AddSeconds((double)(this.LongTakingAction ? -60 : -15)))
                            {
                                this.working = false;
                                this.Close(3);
                            }
                            else
                            {
                                DateTime arg_B5_0 = this.lastPingTime;
                                now = DateTime.Now;
                                if (arg_B5_0 < now.AddSeconds(-30.0))
                                {
                                    this.Ping();
                                    this.lastPingTime = DateTime.Now;
                                }
                                this.ReceiveFrame();

                                Log.Trace($"Odebrano ramkę. Typ: {this.frame.type}");

                                if (this.frame.type == 100)
                                {
                                    this.Close(4);
                                }
                                else if (this.frame.type != 46 && this.frame.type != 47)
                                {
                                    if (this.frame.type == 16)
                                    {
                                        this.LongTakingAction = false;
                                        if (this.OnStatus != null)
                                        {
                                            this.LastStatus = new NeoModel(this.frame.GetString());
                                            this.LastStatus.Reason = 16;
                                            this.OnStatus.Invoke(this.Site, this.LastStatus);
                                        }
                                    }
                                    else if (this.frame.type == 40)
                                    {
                                        this.LongTakingAction = false;
                                        if (this.OnExtStatus != null)
                                        {
                                            this.LastExtStatus = new NeoModelExt(this.frame.GetString());
                                            this.OnExtStatus.Invoke(this.Site, this.LastExtStatus);
                                        }
                                    }
                                    else if (this.frame.type == 7)
                                    {
                                        this.LongTakingAction = false;
                                        this.LastStatus.Reason = 7;
                                        this.OnStatus.Invoke(this.Site, this.LastStatus);
                                    }
                                    else if (this.frame.type == 8)
                                    {
                                        this.LongTakingAction = false;
                                        this.LastStatus.Reason = 8;
                                        this.OnStatus.Invoke(this.Site, this.LastStatus);
                                    }
                                    else if (this.frame.type == 12)
                                    {
                                        this.LastStatus.Reason = 12;
                                        this.OnStatus.Invoke(this.Site, this.LastStatus);
                                    }
                                    else if (this.frame.type == 11)
                                    {
                                        Message message = new Message
                                        {
                                            Site = this.Site.Id,
                                            Channel = 0,
                                            Type = this.frame.data[0],
                                            Text = this.frame.GetString().Substring(1),
                                            Time = DateTime.Now
                                        };
                                        if (message.Type == 92)
                                        {
                                            this.LongTakingAction = true;
                                        }
                                        if (message.Type != 91 && message.Type != 90 && message.Type != 92)
                                        {
                                            DB.SaveMessage(message);
                                        }
                                        if (this.OnMessage != null && this.frame.length > 1)
                                        {
                                            this.OnMessage.Invoke(this.Site, message);
                                        }
                                    }
                                    else if (this.frame.type == 10)
                                    {
                                        if (this.OnEvents != null)
                                        {
                                            string[] array = this.frame.GetString().Split(new char[]
                                            {
                                            '\n'
                                            });
                                            List<Event> list = new List<Event>();
                                            for (int i = 0; i < array.Length; i++)
                                            {
                                                string text = array[i].Trim();
                                                if (!string.IsNullOrEmpty(text))
                                                {
                                                    string[] array2 = text.Split(new char[]
                                                    {
                                                    '#'
                                                    });
                                                    Event @event = new Event
                                                    {
                                                        Type = 4,
                                                        Time = array2[1],
                                                        Name = array2[2]
                                                    };
                                                    try
                                                    {
                                                        @event.Type = Convert.ToByte(array2[0]);
                                                    }
                                                    catch (Exception)
                                                    {
                                                    }
                                                    list.Add(@event);
                                                }
                                            }
                                            this.OnEvents.Invoke(this.Site, list);
                                        }
                                    }
                                    else if (this.frame.type == 31)
                                    {
                                        string[] array3 = this.frame.GetString().Split(new char[]
                                        {
                                        '\n'
                                        });
                                        List<User> list2 = new List<User>();
                                        User user = new User();
                                        for (int j = 0; j < array3.Length; j++)
                                        {
                                            string text2 = array3[j].Trim();
                                            if (j % 2 == 0)
                                            {
                                                user.No = (byte)(j / 2 + 1);
                                                user.Name = text2;
                                            }
                                            else
                                            {
                                                user.IsFree = (text2 == "0");
                                                list2.Add(user);
                                                user = new User();
                                            }
                                        }
                                        this.LastStatus.Reason = 31;
                                        this.LastStatus.Users = list2;
                                        this.OnStatus.Invoke(this.Site, this.LastStatus);
                                    }
                                    else if (this.frame.type == 30)
                                    {
                                        this.LastStatus.Reason = 30;
                                        this.LastStatus.Access.UserNo = this.frame.data[0];
                                        byte b = this.frame.data[1];
                                        this.LastStatus.Access.IsZone1 = ((b & 1) > 0);
                                        this.LastStatus.Access.IsZone2 = ((b >> 1 & 1) != 0);
                                        this.LastStatus.Access.IsRemote = ((b >> 4 & 1) != 0);
                                        this.LastStatus.Access.CanLockInputs = ((b >> 5 & 1) != 0);
                                        this.OnStatus.Invoke(this.Site, this.LastStatus);
                                    }
                                    else if (this.frame.type == 45)
                                    {
                                        this.LastStatus.Reason = 45;
                                        byte b2 = this.frame.data[0];
                                        this.LastStatus.Access.IsZone1 = ((b2 & 1) > 0);
                                        this.LastStatus.Access.IsZone2 = ((b2 >> 1 & 1) != 0);
                                        this.LastStatus.Access.IsRemote = ((b2 >> 4 & 1) != 0);
                                        this.LastStatus.Access.CanLockInputs = ((b2 >> 5 & 1) != 0);
                                        this.LastStatus.Access.UserName = this.frame.GetString(1);
                                        this.OnStatus.Invoke(this.Site, this.LastStatus);
                                    }
                                    else if (this.frame.type == 33)
                                    {
                                        byte arg_6F5_0 = this.frame.data[0];
                                    }
                                    else if (this.frame.type == 32)
                                    {
                                        this.LastStatus.Reason = 32;
                                        this.LastStatus.ByteRet = this.frame.data[0];
                                        this.OnStatus.Invoke(this.Site, this.LastStatus);
                                    }
                                    else if (this.frame.type == 34)
                                    {
                                        this.LastStatus.Reason = 34;
                                        this.LastStatus.ByteRet = this.frame.data[0];
                                        this.OnStatus.Invoke(this.Site, this.LastStatus);
                                    }
                                    else if (this.frame.type == 44)
                                    {
                                        this.LastStatus.Reason = 44;
                                        this.LastStatus.ByteRet = this.frame.data[0];
                                        this.OnStatus.Invoke(this.Site, this.LastStatus);
                                    }
                                    else if (this.frame.type != 35 && this.frame.type != 41 && this.frame.type != 42 && this.frame.type != 43)
                                    {
                                        if (this.frame.type == 46)
                                        {
                                            byte arg_84F_0 = this.frame.data[8];
                                            byte arg_85E_0 = this.frame.data[9];
                                            byte arg_86D_0 = this.frame.data[10];
                                            this.frame.GetString(11, 30);
                                            this.frame.GetString(41);
                                        }
                                        else if (this.frame.type == 47)
                                        {
                                            this.frame.GetInt(8);
                                        }
                                        else if (this.frame.type == 39)
                                        {
                                            this.LastStatus.ThermostatProfile = ThermostatProfile.FromBytes(this.frame.data);
                                            this.LastStatus.Reason = 39;
                                            this.OnStatus.Invoke(this.Site, this.LastStatus);
                                        }
                                        else if (this.frame.type != 38 && this.frame.type == 6)
                                        {
                                            string @string = this.frame.GetString();
                                            this.LastConfig = JsonConvert.DeserializeObject<NeoConfig>(@string);
                                            this.Site.CanEditMenu = this.LastConfig.canedit;
                                            if (!string.IsNullOrEmpty(this.LastConfig.phone) && this.LastConfig.phone != this.Site.DevicePhone)
                                            {
                                                this.Site.DevicePhone = this.LastConfig.phone;
                                                DB.SaveSiteDevicePhone(this.Site.Id, this.Site.DevicePhone);
                                            }
                                            DB.SaveSiteInputs(this.Site.Id, this.LastConfig.inputs);
                                            DB.SaveSiteOutputs(this.Site.Id, this.LastConfig.outputs);
                                            DB.SaveSiteZones(this.Site.Id, this.LastConfig.zones);
                                            DB.SaveSiteTempSensors(this.Site.Id, this.LastConfig.temps);
                                            List<string> list3 = new List<string>();
                                            list3.AddRange(this.LastConfig.wiredsens);
                                            list3.AddRange(this.LastConfig.wirelessSensors);
                                            DB.SaveSiteWirelessSensors(this.Site.Id, list3);
                                            int arg_AA5_1 = this.Site.Id;
                                            List<string> expr_A94 = new List<string>();
                                            expr_A94.Add(this.LastConfig.analog);
                                            DB.SaveSiteAnalogInputs(arg_AA5_1, expr_A94);
                                            DB.SaveSiteCanEditMenu(this.Site.Id, this.LastConfig.canedit);
                                            DB.SaveStringParameter("site." + this.Site.Id + ".settings", @string);
                                            DB.SaveIntParameter("site." + this.Site.Id + ".settings.crc", this.SettingsCrc);
                                            if (this.OnConfig != null && this.frame.length > 1)
                                            {
                                                this.OnConfig.Invoke(this.Site, this.LastConfig);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (TimeoutException)
                {
                    Log.Warn("TimeoutException");
                }
                catch (SocketClosedException)
                {
                    Log.Warn("SocketClosedException");
                    Reconnect();
                }
                catch (ParseStatusException)
                {
                    Log.Warn("ParseStatusException");
                }
                catch (BadFrameException)
                {
                    this.Client.ClearReadBuffer();
                }
                catch (Exception ex)
                {
                    Log.Warn($"Other exception [{ex.Message}]");
                }
            }
        }

        // Token: 0x0600020C RID: 524 RVA: 0x00011208 File Offset: 0x0000F408
        public void Fire()
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("Fire");
                }
                this.frame.type = 43;
                this.frame.ClearData();
                this.SendFrame();
            }
        }

        // Token: 0x06000207 RID: 519 RVA: 0x00010F7C File Offset: 0x0000F17C
        public void GetConfig()
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("GetConfig");
                }
                this.frame.type = 5;
                this.frame.ClearData();
                this.SendFrame();
            }
        }

        // Token: 0x06000208 RID: 520 RVA: 0x00010FE4 File Offset: 0x0000F1E4
        public void GetEvents(short idx, short len, byte type)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("GetEvents");
                }
                this.LongTakingAction = true;
                this.frame.type = 9;
                this.frame.data = new byte[5];
                this.frame.data[0] = BitConverter.GetBytes(idx)[0];
                this.frame.data[1] = BitConverter.GetBytes(idx)[1];
                this.frame.data[2] = BitConverter.GetBytes(len)[0];
                this.frame.data[3] = BitConverter.GetBytes(len)[1];
                this.frame.data[4] = type;
                this.SendFrame();
            }
        }

        // Token: 0x06000214 RID: 532 RVA: 0x00011734 File Offset: 0x0000F934
        public void GetUserMask(byte codeNo)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("GetUserMask");
                }
                this.frame.type = 45;
                this.frame.ClearData();
                this.frame.AddData(this.Site.Code);
                this.frame.AddData(0);
                this.frame.AddData(codeNo);
                this.SendFrame();
            }
        }

        // Token: 0x06000203 RID: 515 RVA: 0x00010DA4 File Offset: 0x0000EFA4
        public void LockInputs(string mask)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("LockInputs");
                }
                this.frame.type = 12;
                this.frame.ClearData();
                this.frame.AddData(mask);
                this.SendFrame();
            }
        }

        public int Login2()
        {
            Log.Info($"Logowanie");
            Log.Info($"Płyta ID: {Config.MainBoardId} ...");

            int num = 48;
            byte[] vector = Config.ConnectVector;
            char[] array5 = Config.MainBoardId.ToCharArray();
            byte[] array6 = new byte[array5.Length];
            for (int i = 0; i < array5.Length; i++)
            {
                array6[i] = (byte)array5[i];
            }
            byte[] array7 = new byte[num];
            for (int j = 0; j < array5.Length; j++)
            {
                array7[j] = (byte)array5[j];
            }
            Random random = new Random();
            for (int k = 16; k < num; k++)
            {
                array7[k] = (byte)(random.Next() % 256);
            }
            VMPC.Encode(array7, 0, num, array6, vector, 16);
            Client.Write(array7, 0, array7.Length);
            byte[] array8 = new byte[num];
            Client.Read(array8, 0, num);

            if (array8[0] == 0)
            {
                byte[] ip = {
                                array8[9],
                                array8[8],
                                array8[7],
                                array8[6]
                };
                this.frame.SetIp(ip);
                var result = LoginCentral();
                Log.Info($"Zalogowano. Ip:{ array8[9]}.{array8[8]}.{array8[7]}.{array8[6]}");
            }

            return 1;
        }

        // Token: 0x060001FC RID: 508 RVA: 0x00010010 File Offset: 0x0000E210
        public int Login()
        {
            int result;
            lock (this)
            {
                this.isBadFrame = false;
                this.Client.ReceiveTimeout = 3000;
                try
                {
                    int num = 48;
                    byte[] vector = new byte[]
                    {
                            34,
                            52,
                            68,
                            162,
                            244,
                            205,
                            91,
                            85,
                            33,
                            52,
                            67,
                            18,
                            152,
                            173,
                            36,
                            236
                    };
                    char[] array5 = "1234567890ABCDEF".ToArray(); //deviceid
                    byte[] array6 = new byte[array5.Length];
                    for (int i = 0; i < array5.Length; i++)
                    {
                        array6[i] = (byte)array5[i];
                    }
                    byte[] array7 = new byte[num];
                    for (int j = 0; j < array5.Length; j++)
                    {
                        array7[j] = (byte)array5[j];
                    }
                    Random random = new Random();
                    for (int k = 16; k < num; k++)
                    {
                        array7[k] = (byte)(random.Next() % 256);
                    }
                    VMPC.Encode(array7, 0, num, array6, vector, 16);
                    this.Client.Write(array7, 0, array7.Length);
                    byte[] array8 = new byte[num];
                    this.Client.Read(array8, 0, num);
                    if (array8[0] == 0)
                    {
                        byte[] ip = new byte[]
                        {
                                array8[9],
                                array8[8],
                                array8[7],
                                array8[6]
                        };
                        this.frame.SetIp(ip);
                        result = this.LoginCentral();
                    }
                    else
                    {
                        if (array8[0] != 1)
                        {
                            throw new Exception("Bad Neo server dialog");
                        }
                        result = -100;
                    }
                }
                catch (SocketClosedException ex)
                {
                    result = -1;
                }
                catch (TcpConnectException)
                {
                    if (this.isBadFrame)
                    {
                        result = -3;
                    }
                    else
                    {
                        result = -1;
                    }
                }
                catch (BadFrameException)
                {
                    result = -1;
                }
                catch (Exception ex)
                {
                    result = -1;
                }
            }
            return result;
        }

        // Token: 0x060001FE RID: 510 RVA: 0x000109C0 File Offset: 0x0000EBC0
        private int LoginCentral()
        {
            bool flag = false;
            for (int i = 0; i < 3; i++)
            {
                if (this.TryLogin() == 0)
                {
                    flag = true;
                    break;
                }
                Delay(TimeSpan.FromMilliseconds(500.0)).Wait();
            }
            if (flag)
            {
                if (this.frame.data[0] == 0)
                {
                    this.Access.UserNo = this.frame.data[1];
                    byte b = this.frame.data[2];
                    this.Access.IsZone1 = ((b & 1) > 0);
                    this.Access.IsZone2 = ((b >> 1 & 1) != 0);
                    this.Access.IsRemote = ((b >> 4 & 1) != 0);
                    this.Access.CanLockInputs = ((b >> 5 & 1) != 0);
                    this.frame.SetTcpVector(this.frame.GetData(3, 16));
                    this.SettingsCrc = (int)BitConverter.ToUInt16(new byte[]
                    {
                        this.frame.data[19],
                        this.frame.data[20]
                    }, 0);
                    string @string = this.frame.GetString(21, 16);
                    string string2 = this.frame.GetString(37, 16);
                    if (this.Channel == SiteConnection.Channels.Bridge)
                    {
                        this.Site.DeviceId2 = @string;
                        this.Site.MobileKey = string2;
                    }
                    this.Site.UserNo = (int)this.Access.UserNo;
                    this.Site.LastLoginTime = DateTime.Now;
                    DB.SaveSite(this.Site);
                    this.Client.ReceiveTimeout = 3000;
                    this.LoginTime = DateTime.Now;
                    this.Logged = true;
                }
                else
                {
                    this.Logged = false;
                }
                return (int)this.frame.data[0];
            }
            return -2;
        }

        public void Logout()
        {
        }

        // Token: 0x06000212 RID: 530 RVA: 0x00011560 File Offset: 0x0000F760
        public void NewUserCode(string newCode, string userName, bool zone1Access, bool zone2Access, bool ipSmsAccess, bool canLockInputs)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("NewUserCode");
                }
                this.frame.type = 34;
                this.frame.ClearData();
                this.frame.AddData(this.Site.Code);
                this.frame.AddData(0);
                this.frame.AddData(newCode);
                this.frame.AddData(0);
                byte b = 0;
                if (zone1Access)
                {
                    b += 1;
                }
                if (zone2Access)
                {
                    b += 2;
                }
                if (ipSmsAccess)
                {
                    b += 16;
                }
                if (canLockInputs)
                {
                    b += 32;
                }
                this.frame.AddData(b);
                this.frame.AddData(userName);
                this.frame.AddData(0);
                this.SendFrame();
            }
        }

        // Token: 0x0600020A RID: 522 RVA: 0x00011138 File Offset: 0x0000F338
        public void OpenWicket()
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("OpenWicket");
                }
                this.frame.type = 41;
                this.frame.ClearData();
                this.SendFrame();
            }
        }

        // Token: 0x0600020B RID: 523 RVA: 0x000111A0 File Offset: 0x0000F3A0
        public void Panic()
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("Panic");
                }
                this.frame.type = 42;
                this.frame.ClearData();
                this.SendFrame();
            }
        }

        // Token: 0x060001FF RID: 511 RVA: 0x00010B8C File Offset: 0x0000ED8C
        public void Ping()
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("Ping");
                }
                this.frame.ClearData();
                this.frame.type = 2;
                this.SendFrame();
            }
        }

        // Token: 0x0600020F RID: 527 RVA: 0x000113BC File Offset: 0x0000F5BC
        public void ReadUserNames()
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("ReadUserNames");
                }
                this.frame.type = 31;
                this.SendFrame();
            }
        }

        // Token: 0x060001FA RID: 506 RVA: 0x0000FDF4 File Offset: 0x0000DFF4
        public void ReceiveFrame()
        {
            try
            {
                this.Client.Read(this.buffer, 0, 4);
                BitConverter.ToUInt16(new byte[]
                {
                    this.buffer[0],
                    this.buffer[1]
                }, 0);
                int len = (int)(BitConverter.ToUInt16(new byte[]
                {
                    this.buffer[2],
                    this.buffer[3]
                }, 0) + (ushort)Frame.HeaderSize - 4);
                this.Client.Read(this.buffer, 4, len);
                this.frame.Load(this.buffer);
                if (this.frame.index != 65535)
                {
                    if (!this.frame.crc1ok)
                    {
                        throw new BadFrameException("badcrc1");
                    }
                    if (!this.frame.crc2ok)
                    {
                        throw new BadFrameException("badcrc2");
                    }
                }
                this.lastFrame = DateTime.Now;
            }
            catch (BadFrameException arg_D7_0)
            {
                throw arg_D7_0;
            }
            catch (SocketClosedException arg_D8_0)
            {
                throw arg_D8_0;
            }
            catch (TimeoutException arg_D9_0)
            {
                throw arg_D9_0;
            }
            catch (Exception ex)
            {
                if (ex.Message == null || (!(ex.Message == "The operation is not allowed on non-connected sockets.") && !ex.Message.Contains("Network subsystem is down")))
                {
                    throw new TcpConnectException("---" + DateTime.Now.ToString("HH:mm:ss") + " ReceiveFrame: " + ex.Message);
                }
                if (this.Logged && this.Connected)
                {
                    this.Stop();

                    this.Reconnect();
                }
            }
        }

        // Token: 0x060001F9 RID: 505 RVA: 0x0000FDAC File Offset: 0x0000DFAC
        public void ReceiveFrame(int tryCnt, byte type)
        {
            int i = tryCnt;
            while (i > 0)
            {
                try
                {
                    i--;
                    this.ReceiveFrame();
                    if (this.frame.type == type)
                    {
                        break;
                    }
                }
                catch (TimeoutException)
                {
                }
            }
        }

        // Token: 0x060001F7 RID: 503 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
        public string ReceiveLine()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string text;
            do
            {
                text = this.ReceiveString(1);
                if (text != "\n")
                {
                    stringBuilder.Append(text);
                }
            }
            while (text != "\n");
            return stringBuilder.ToString();
        }

        // Token: 0x060001F6 RID: 502 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
        public string ReceiveString(int size)
        {
            this.Client.Read(this.buffer, 0, size);
            return Encoding.UTF8.GetString(this.buffer, 0, size);
        }

        // Token: 0x060001FB RID: 507 RVA: 0x0000FF9C File Offset: 0x0000E19C
        public void Reconnect()
        {
            bool flag = false;
            for (int i = 0; i < 6; i++)
            {
                Delay(TimeSpan.FromSeconds(5.0)).Wait();
                this.Logged = false;
                this.Connected = false;
                this.Close(2);
                flag = this.Connect();
                if (flag)
                {
                    if (this.Login() == 0)
                    {
                        this.Start();
                        flag = true;
                        break;
                    }
                    flag = false;
                }
            }
            if (!flag)
            {
                this.Close(3);
            }
        }

        // Token: 0x060001F8 RID: 504 RVA: 0x0000FD30 File Offset: 0x0000DF30
        public void SendFrame()
        {
            try
            {
                Frame.LastIndex += 2;
                this.frame.index = Frame.LastIndex;
                byte[] array = this.frame.ToBytes();
                this.Client.Write(array, 0, array.Length);
            }
            catch (Exception ex)
            {
                if (ex.Message != null && ex.Message == "The operation is not allowed on non-connected sockets")
                {
                    this.Close(3);
                }
            }
        }

        // Token: 0x060001F5 RID: 501 RVA: 0x0000FCAD File Offset: 0x0000DEAD
        public void SendLine(string str)
        {
            this.SendString(str + "\n");
        }

        // Token: 0x0600020D RID: 525 RVA: 0x00011270 File Offset: 0x0000F470
        public void SendMessage(byte type, byte classe, byte mask, byte priority, byte collapseKey, ushort ttl, string title, string body)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("v");
                }
                this.frame.type = 46;
                this.frame.ClearData();
                this.frame.AddData(type);
                this.frame.AddData(classe);
                this.frame.AddData(mask);
                this.frame.AddData(priority);
                this.frame.AddData(collapseKey);
                this.frame.AddData(ttl);
                this.frame.AddData(title.PadLeft(30, ' '));
                this.frame.AddData(body);
                this.SendFrame();
            }
        }

        // Token: 0x060001F4 RID: 500 RVA: 0x0000FC84 File Offset: 0x0000DE84
        public void SendString(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            this.Client.Write(bytes, 0, bytes.Length);
        }

        // Token: 0x06000206 RID: 518 RVA: 0x00010F28 File Offset: 0x0000F128
        public void SendUssd(string command)
        {
            if (this.Logged)
            {
                this.LongTakingAction = true;
                this.frame.type = 19;
                this.frame.ClearData();
                this.frame.AddData(command);
                this.SendFrame();
                return;
            }
            throw new NotLoggedException("SendUssd");
        }

        // Token: 0x06000209 RID: 521 RVA: 0x000110BC File Offset: 0x0000F2BC
        public void ServiceConnectRequest()
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("ServiceConnectRequest");
                }
                this.frame.type = 35;
                this.frame.ClearData();
                this.frame.AddData(this.Site.Code);
                this.SendFrame();
            }
        }

        // Token: 0x06000202 RID: 514 RVA: 0x00010D30 File Offset: 0x0000EF30
        public void SetOutputs(string mask)
        {
            lock (this)
            {
                Log.Trace($"SetOutputs [{mask}]");

                if (!this.Logged)
                {
                    throw new NotLoggedException("SetOutputs");
                }
                this.frame.type = 4;
                this.frame.ClearData();
                this.frame.AddData(mask);
                this.SendFrame();
            }
        }

        // Token: 0x060001EF RID: 495 RVA: 0x0000FA1D File Offset: 0x0000DC1D
        public void Sleep()
        {
            this.sleep = 1;
            while (this.sleep == 1)
            {
                Delay(TimeSpan.FromSeconds(1.0)).Wait();
            }
        }

        // Token: 0x06000205 RID: 517 RVA: 0x00010E98 File Offset: 0x0000F098
        public void SmsVirtual(string command, string lang, params object[] args)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("SmsVirtual");
                }
                string datapart = string.Format(SiteConnection.Commands[command].Format[lang], args);
                this.frame.type = 18;
                this.frame.ClearData();
                this.frame.AddData(datapart);
                this.SendFrame();
            }
        }

        // Token: 0x06000204 RID: 516 RVA: 0x00010E18 File Offset: 0x0000F018
        public void SmsVirtualNative(string code, string command)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("SmsVirtual");
                }
                this.frame.type = 18;
                this.frame.ClearData();
                this.frame.AddData(code + " " + command);
                this.SendFrame();
            }
        }

        // Token: 0x060001ED RID: 493 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
        public void Start()
        {
            this.working = true;
            Task.Factory.StartNew(new Action(this.DoWork));
        }

        // Token: 0x060001EE RID: 494 RVA: 0x0000FA14 File Offset: 0x0000DC14
        public void Stop()
        {
            this.working = false;
        }

        // Token: 0x06000217 RID: 535 RVA: 0x000118B0 File Offset: 0x0000FAB0
        public void ThermostatGetProfile()
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("ThermostatGetProfile");
                }
                this.frame.type = 39;
                this.frame.ClearData();
                this.SendFrame();
            }
        }

        // Token: 0x06000215 RID: 533 RVA: 0x000117C8 File Offset: 0x0000F9C8
        public void ThermostatSetMode(byte mode)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("ThermostatSetMode");
                }
                this.frame.type = 36;
                this.frame.ClearData();
                this.frame.AddData(mode);
                this.SendFrame();
            }
        }

        // Token: 0x06000216 RID: 534 RVA: 0x0001183C File Offset: 0x0000FA3C
        public void ThermostatSetPoint(float setpoint)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("ThermostatSetPoint");
                }
                this.frame.type = 37;
                this.frame.ClearData();
                this.frame.AddData(setpoint);
                this.SendFrame();
            }
        }

        // Token: 0x06000218 RID: 536 RVA: 0x00011918 File Offset: 0x0000FB18
        public void ThermostatSetProfile(ThermostatProfile profile)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("ThermostatSetProfile");
                }
                this.frame.type = 38;
                this.frame.ClearData();
                this.frame.AddData(profile.ToBytes());
                this.SendFrame();
            }
        }

        // Token: 0x060001FD RID: 509 RVA: 0x000108B8 File Offset: 0x0000EAB8
        private int TryLogin()
        {
            int result;
            try
            {
                this.frame.Reset();
                this.frame.SetTcpKey(this.Site.TcpCode);
                this.frame.type = 0;
                this.frame.AddData(this.Site.Code);
                this.frame.AddData(this.frame.ip);
                byte[] tcpVector = this.frame.AddRandomData(16);
                this.SendFrame();
                this.Client.Flush();
                this.frame.SetTcpVector(tcpVector);
                try
                {
                    this.ReceiveFrame();
                }
                catch (BadFrameException)
                {
                    this.isBadFrame = true;
                    this.ReceiveFrame();
                }
                if (this.frame.length != 53)
                {
                    this.ReceiveFrame();
                }
                result = (int)this.frame.type;
            }
            catch (BadFrameException)
            {
                this.isBadFrame = true;
                result = -1;
            }
            catch (Exception)
            {
                result = -1;
            }
            return result;
        }

        // Token: 0x06000213 RID: 531 RVA: 0x00011650 File Offset: 0x0000F850
        public void UserChange(byte codeNo, string userName, bool zone1Access, bool zone2Access, bool ipSmsAccess, bool canLockInputs)
        {
            lock (this)
            {
                if (!this.Logged)
                {
                    throw new NotLoggedException("UserChange");
                }
                this.frame.type = 44;
                this.frame.ClearData();
                this.frame.AddData(this.Site.Code);
                this.frame.AddData(0);
                this.frame.AddData(codeNo);
                byte b = 0;
                if (zone1Access)
                {
                    b += 1;
                }
                if (zone2Access)
                {
                    b += 2;
                }
                if (ipSmsAccess)
                {
                    b += 16;
                }
                if (canLockInputs)
                {
                    b += 32;
                }
                this.frame.AddData(b);
                this.frame.AddData(userName);
                this.frame.AddData(0);
                this.SendFrame();
            }
        }

        // Token: 0x060001F0 RID: 496 RVA: 0x0000FA49 File Offset: 0x0000DC49
        public void WakeUp()
        {
            this.sleep = 0;
        }

        // Token: 0x17000073 RID: 115
        public static SiteConnection Instance
        {
            // Token: 0x060001DF RID: 479 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
            get
            {
                return SiteConnection.con;
            }
            // Token: 0x060001E0 RID: 480 RVA: 0x0000ECEF File Offset: 0x0000CEEF
            set
            {
                if (SiteConnection.con != null)
                {
                    SiteConnection.con.Logout();
                    SiteConnection.con.Close(0);
                }
                SiteConnection.con = value;
            }
        }

        // Token: 0x17000078 RID: 120
        public NeoConfig LastConfig
        {
            // Token: 0x060001E9 RID: 489 RVA: 0x0000ED57 File Offset: 0x0000CF57
            get;
            // Token: 0x060001EA RID: 490 RVA: 0x0000ED5F File Offset: 0x0000CF5F
            set;
        }

        // Token: 0x17000077 RID: 119
        public NeoModelExt LastExtStatus
        {
            // Token: 0x060001E7 RID: 487 RVA: 0x0000ED46 File Offset: 0x0000CF46
            get;
            // Token: 0x060001E8 RID: 488 RVA: 0x0000ED4E File Offset: 0x0000CF4E
            set;
        }

        // Token: 0x17000076 RID: 118
        public NeoModel LastStatus
        {
            // Token: 0x060001E5 RID: 485 RVA: 0x0000ED35 File Offset: 0x0000CF35
            get;
            // Token: 0x060001E6 RID: 486 RVA: 0x0000ED3D File Offset: 0x0000CF3D
            set;
        }

        // Token: 0x17000074 RID: 116
        public int SettingsCrc
        {
            // Token: 0x060001E1 RID: 481 RVA: 0x0000ED13 File Offset: 0x0000CF13
            get;
            // Token: 0x060001E2 RID: 482 RVA: 0x0000ED1B File Offset: 0x0000CF1B
            set;
        }

        // Token: 0x17000075 RID: 117
        public Site Site
        {
            // Token: 0x060001E3 RID: 483 RVA: 0x0000ED24 File Offset: 0x0000CF24
            get;
            // Token: 0x060001E4 RID: 484 RVA: 0x0000ED2C File Offset: 0x0000CF2C
            set;
        }

        // Token: 0x040000B6 RID: 182
        public Access Access = new Access();

        // Token: 0x040000A3 RID: 163
        public static string BridgeHost = "iropam.com";

        // Token: 0x040000A4 RID: 164
        public static int BridgePort = 8080;

        // Token: 0x040000BB RID: 187
        private byte[] buffer = new byte[SiteConnection.BUFFER_SIZE];

        // Token: 0x040000BA RID: 186
        private static int BUFFER_SIZE = 65536 + (int)Frame.HeaderSize;

        // Token: 0x040000B5 RID: 181
        public SiteConnection.Channels Channel;

        // Token: 0x040000AE RID: 174
        private TcpClientStreamSocket Client;

        // Token: 0x040000C6 RID: 198
        public static Dictionary<string, Command> Commands;

        // Token: 0x040000AD RID: 173
        private static SiteConnection con;

        // Token: 0x040000B0 RID: 176
        public bool Connected;

        // Token: 0x040000A5 RID: 165
        public static int DefaultPort = 9999;

        // Token: 0x040000AB RID: 171
        public static string DemoCode = "5555";

        // Token: 0x040000A9 RID: 169
        public static string DemoDeviceId = "9999999999999999";

        // Token: 0x040000AA RID: 170
        public static string DemoDevicePhone = "";

        // Token: 0x040000A6 RID: 166
        public static string DemoHost = "ropam.net";

        // Token: 0x040000A7 RID: 167
        public static int DemoPort = 9999;

        // Token: 0x040000A8 RID: 168
        public static string DemoTcpCode = "9999999999999999";

        // Token: 0x040000B7 RID: 183
        private Frame frame = new Frame();

        // Token: 0x040000C2 RID: 194
        private bool isBadFrame;

        // Token: 0x040000B1 RID: 177
        public bool IsNeoLicence;

        // Token: 0x040000C1 RID: 193
        private DateTime lastFrame = DateTime.Now;

        // Token: 0x040000BD RID: 189
        private DateTime lastPingTime = DateTime.Now;

        // Token: 0x040000B4 RID: 180
        public string LicenseId;

        // Token: 0x040000B3 RID: 179
        public DateTime LicenseTime;

        // Token: 0x040000B2 RID: 178
        public byte LicenseType;

        // Token: 0x040000AF RID: 175
        public bool Logged;

        // Token: 0x040000BC RID: 188
        public DateTime LoginTime;

        // Token: 0x040000C5 RID: 197
        public bool LongTakingAction;

        // Token: 0x040000CE RID: 206
        public Action<Site, int> OnClosed;

        // Token: 0x040000CA RID: 202
        public Action<Site, NeoConfig> OnConfig;

        // Token: 0x040000CB RID: 203
        public Action<Site, List<Event>> OnEvents;

        // Token: 0x040000C8 RID: 200
        public Action<Site, NeoModelExt> OnExtStatus;

        // Token: 0x040000C9 RID: 201
        public Action<Site, Message> OnMessage;

        // Token: 0x040000CC RID: 204
        public Action<Site, List<Contact>> OnPhones;

        // Token: 0x040000CD RID: 205
        public Action<Site, List<NeoModel>> OnStates;

        // Token: 0x040000C7 RID: 199
        public Action<Site, NeoModel> OnStatus;

        // Token: 0x040000AC RID: 172
        private static byte[] OperatorVector = new byte[]
        {
            103,
            82,
            187,
            28,
            161,
            237,
            191,
            65,
            115,
            43,
            151,
            95,
            187,
            36,
            205,
            32
        };

        // Token: 0x040000C4 RID: 196
        private int sleep;

        // Token: 0x040000C3 RID: 195
        private bool working = true;

        // Token: 0x02000109 RID: 265
        public enum Channels
        {
            // Token: 0x0400048A RID: 1162
            Local,

            // Token: 0x0400048B RID: 1163
            External,

            // Token: 0x0400048C RID: 1164
            Bridge
        }

        // Token: 0x0200010A RID: 266
        public enum CloseReasons
        {
            // Token: 0x0400048E RID: 1166
            Connect,

            // Token: 0x0400048F RID: 1167
            User,

            // Token: 0x04000490 RID: 1168
            Reconnect,

            // Token: 0x04000491 RID: 1169
            ConnectionLost,

            // Token: 0x04000492 RID: 1170
            AnotherUserLogged,

            // Token: 0x04000493 RID: 1171
            Exit,

            // Token: 0x04000494 RID: 1172
            Error
        }

        // Token: 0x02000108 RID: 264
        public enum LicenseTypes : byte
        {
            // Token: 0x04000486 RID: 1158
            NoLicense,

            // Token: 0x04000487 RID: 1159
            SubscriptionYear,

            // Token: 0x04000488 RID: 1160
            Durable
        }

        // Token: 0x0200010B RID: 267
        public enum Responses
        {
            // Token: 0x04000496 RID: 1174
            Ok,

            // Token: 0x04000497 RID: 1175
            NeoNotAvailable = -1,

            // Token: 0x04000498 RID: 1176
            BadDialog = -2,

            // Token: 0x04000499 RID: 1177
            BadTcpKey = -3,

            // Token: 0x0400049A RID: 1178
            NoConnectData = -4,

            // Token: 0x0400049B RID: 1179
            GeneralProblems = -5,

            // Token: 0x0400049C RID: 1180
            UnableConnect = -6,

            // Token: 0x0400049D RID: 1181
            ConnectionsProblems = -7,

            // Token: 0x0400049E RID: 1182
            Error = -8,

            // Token: 0x0400049F RID: 1183
            Nok = 1,

            // Token: 0x040004A0 RID: 1184
            UserNoPermission,

            // Token: 0x040004A1 RID: 1185
            BadLogin,

            // Token: 0x040004A2 RID: 1186
            BadPhone,

            // Token: 0x040004A3 RID: 1187
            NotRady,

            // Token: 0x040004A4 RID: 1188
            NoNightlines,

            // Token: 0x040004A5 RID: 1189
            LoginLocked,

            // Token: 0x040004A6 RID: 1190
            EspBusy = -100,

            // Token: 0x040004A7 RID: 1191
            BridgeOtherError = -200,

            // Token: 0x040004A8 RID: 1192
            BridgeDeviceNotFound = -201,

            // Token: 0x040004A9 RID: 1193
            BridgeOtherOperatorBinded = -202,

            // Token: 0x040004AA RID: 1194
            BridgeAppVersionNotSupported = -203,

            // Token: 0x040004AB RID: 1195
            BridgeBadDeviceType = -204,

            // Token: 0x040004AC RID: 1196
            BridgeNoLicense = -205,

            // Token: 0x040004AD RID: 1197
            BridgeLoginAgain = -206
        }
    }
}