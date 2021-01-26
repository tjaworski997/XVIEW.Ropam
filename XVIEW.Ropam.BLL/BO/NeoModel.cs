using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class NeoModel
    {
        public Access Access;

        public NeoModel()
        {
        }

        private string content;

        public string FirmwareVersion { get; }
        public string DeviceLang { get; }
        public DateTime ModuleTime { get; }
        public List<Input> Inputs { get; }
        public List<Output> Outputs { get; }
        public List<Zone> Zones { get; }
        public List<TempSensor> TempSensors { get; }
        public bool ModuleAcOk { get; }
        public object SupplyVoltage { get; }
        public int NetworkStatus { get; }
        public int WifiNetworkStatus { get; }
        public bool[] ModFailCodes { get; }
        public ulong ModFailCodeBinary { get; }
        public List<AnalogInput> AnalogInputs { get; }
        public int DeviceMode { get; }
        public bool UsingBridge { get; }
        public bool UsingBridgeGprs { get; }
        public bool UsingBridgeWifi { get; }
        public bool UsingLocalPort1 { get; }
        public bool UsingLocalPort2 { get; }
        public bool UsingRms { get; }
        public bool UsingRmsGprs { get; }
        public bool UsingRmsWifi { get; }
        public List<WirelessSensor> WiredHumSensors { get; }
        public bool DuringSms { get; }
        public bool DuringEmail { get; }
        public bool DuringIncomingCall { get; }
        public bool DuringOutgoingCall { get; }
        public bool DuringVar1U { get; }

        public NeoModel(string content)
        {
            this.content = content;

            try
            {
                this.Access = new Access();
                int num = 0;
                if (content[num] != '#')
                {
                    throw new ParseStatusException("Bad prefix");
                }
                num++;
                if (content[num] != '7')
                {
                    throw new ParseStatusException("Bad hardware version");
                }
                num++;
                this.FirmwareVersion = string.Format("{0},{1}", content.Substring(num, 1), content.Substring(num + 1, 1));
                num += 2;
                string text = content.Substring(num, 1);
                if (!(text == "p"))
                {
                    if (!(text == "e"))
                    {
                        this.DeviceLang = "pl";
                    }
                    else
                    {
                        this.DeviceLang = "en";
                    }
                }
                else
                {
                    this.DeviceLang = "pl";
                }
                num++;
                if (content.Substring(num, 2) == "00")
                {
                    this.ModuleTime = DateTime.ParseExact("0101010000", "yyMMddHHmm", CultureInfo.InvariantCulture);
                }
                else
                {
                    this.ModuleTime = DateTime.ParseExact(content.Substring(num, 10), "yyMMddHHmm", CultureInfo.InvariantCulture);
                }
                num += 10;
                this.Inputs = new List<Input>();
                byte b = 0;
                char c;
                while (b < 32)
                {
                    Input input = new Input();
                    input.No = (int)b;
                    c = content[num + (int)b];
                    if (c <= 'W')
                    {
                        if (c <= '1')
                        {
                            if (c != '!')
                            {
                                switch (c)
                                {
                                    case '-':
                                    case '.':
                                    case '/':
                                        goto IL_24F;
                                    case '0':
                                        input.State = 0;
                                        break;

                                    case '1':
                                        input.State = 1;
                                        break;

                                    default:
                                        goto IL_24F;
                                }
                            }
                            else
                            {
                                input.State = 2;
                            }
                        }
                        else
                        {
                            switch (c)
                            {
                                case '?':
                                    input.State = 10;
                                    break;

                                case '@':
                                    goto IL_24F;
                                case 'A':
                                    input.State = 7;
                                    break;

                                case 'B':
                                    goto IL_1FC;
                                default:
                                    if (c == 'F')
                                    {
                                        goto IL_206;
                                    }
                                    if (c != 'W')
                                    {
                                        goto IL_24F;
                                    }
                                    input.State = 9;
                                    break;
                            }
                        }
                    }
                    else if (c <= 'b')
                    {
                        if (c != 'X')
                        {
                            if (c != 'a')
                            {
                                if (c != 'b')
                                {
                                    goto IL_24F;
                                }
                                goto IL_1FC;
                            }
                            else
                            {
                                input.State = 6;
                            }
                        }
                        else
                        {
                            input.State = 3;
                        }
                    }
                    else
                    {
                        if (c == 'f')
                        {
                            goto IL_206;
                        }
                        if (c != 'i')
                        {
                            if (c != 'w')
                            {
                                goto IL_24F;
                            }
                            input.State = 8;
                        }
                        else
                        {
                            input.State = 12;
                        }
                    }
                    IL_258:
                    this.Inputs.Add(input);
                    b += 1;
                    continue;
                    IL_1FC:
                    input.State = 4;
                    goto IL_258;
                    IL_206:
                    input.State = 5;
                    goto IL_258;
                    IL_24F:
                    input.State = 11;
                    goto IL_258;
                }
                num += 32;
                this.Outputs = new List<Output>();
                byte b2 = 0;
                while (b2 < 32)
                {
                    Output output = new Output();
                    output.No = (int)b2;
                    c = content[num + (int)b2];
                    if (c <= 'N')
                    {
                        switch (c)
                        {
                            case '-':
                            case '.':
                            case '/':
                                goto IL_34D;
                            case '0':
                                output.State = 0;
                                break;

                            case '1':
                                output.State = 1;
                                break;

                            default:
                                if (c != 'F')
                                {
                                    if (c != 'N')
                                    {
                                        goto IL_34D;
                                    }
                                    output.State = 6;
                                }
                                else
                                {
                                    output.State = 2;
                                }
                                break;
                        }
                    }
                    else if (c <= 'f')
                    {
                        if (c != 'X')
                        {
                            if (c != 'f')
                            {
                                goto IL_34D;
                            }
                            output.State = 3;
                        }
                        else
                        {
                            output.State = 4;
                        }
                    }
                    else if (c != 'n')
                    {
                        if (c != 'x')
                        {
                            goto IL_34D;
                        }
                        output.State = 5;
                    }
                    else
                    {
                        output.State = 7;
                    }
                    IL_355:
                    this.Outputs.Add(output);
                    b2 += 1;
                    continue;
                    IL_34D:
                    output.State = 8;
                    goto IL_355;
                }
                num += 32;
                this.Zones = new List<Zone>();
                for (byte b3 = 0; b3 < 2; b3 += 1)
                {
                    Zone zone = new Zone();
                    zone.No = (int)b3;
                    byte b4 = byte.Parse(content.Substring(num, 2), (NumberStyles)515);
                    zone.IsArmed = ((b4 & 1) > 0);
                    zone.IsNightArmed = ((b4 >> 1 & 1) != 0);
                    zone.OnDeparture = ((b4 >> 2 & 1) != 0);
                    zone.OnEntrance = ((b4 >> 3 & 1) != 0);
                    zone.IsAlarm = ((b4 >> 4 & 1) != 0);
                    zone.IsTamper = ((b4 >> 5 & 1) != 0);
                    zone.IsReady = ((b4 >> 6 & 1) != 0);
                    this.Zones.Add(zone);
                    num += 2;
                }
                for (int i = 0; i < 2; i++)
                {
                    ushort timer = ushort.Parse(content.Substring(num, 4), (NumberStyles)515);
                    this.Zones[i].Timer = timer;
                    num += 4;
                }
                this.TempSensors = new List<TempSensor>();
                for (int j = 0; j < 2; j++)
                {
                    TempSensor tempSensor = new TempSensor();
                    tempSensor.No = j;
                    string text2 = content.Substring(num, 4);
                    if ("!!!!".Equals(text2))
                    {
                        tempSensor.State = 1;
                    }
                    else if ("xxxx".Equals(text2))
                    {
                        tempSensor.State = 2;
                    }
                    else
                    {
                        tempSensor.State = 0;
                        tempSensor.Value = Convert.ToSingle((short)int.Parse(text2, (NumberStyles)515)) / 10f;
                    }
                    this.TempSensors.Add(tempSensor);
                    num += 4;
                }
                this.ModuleAcOk = (content.Substring(num, 1) == "0");
                num++;
                this.SupplyVoltage = ((float)long.Parse(content.Substring(num, 2), (NumberStyles)515) / 10f).ToString().Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                num += 2;
                this.NetworkStatus = Convert.ToInt32(content.Substring(num, 1));
                num++;
                this.WifiNetworkStatus = Convert.ToInt32(content.Substring(num, 1));
                num++;
                this.ModFailCodes = new bool[39];
                this.ModFailCodeBinary = ulong.Parse(content.Substring(num, 16), (NumberStyles)515);
                BitArray bitArray = new BitArray(BitConverter.GetBytes(this.ModFailCodeBinary));
                for (int k = 0; k < 39; k++)
                {
                    this.ModFailCodes[k] = bitArray.Get(k);
                }
                num += 16;
                this.AnalogInputs = new List<AnalogInput>();
                for (int l = 0; l < 1; l++)
                {
                    float value = BitConverter.ToSingle(BitConverter.GetBytes((int)long.Parse(content.Substring(num + l * 10, 8), (NumberStyles)515)), 0);
                    AnalogInput analogInput = new AnalogInput();
                    analogInput.No = l;
                    analogInput.Value = value;
                    analogInput.Unit = content.Substring(num + 8 + l * 10, 2).Trim();
                    this.AnalogInputs.Add(analogInput);
                }
                num += 10;
                c = content[num];
                if (c != 'd')
                {
                    if (c != 'n')
                    {
                        if (c == 's')
                        {
                            this.DeviceMode = 0;
                        }
                    }
                    else
                    {
                        this.DeviceMode = 1;
                    }
                }
                else
                {
                    this.DeviceMode = 2;
                }
                num++;
                byte b5 = byte.Parse(content.Substring(num, 2), (NumberStyles)515);
                this.UsingBridge = ((b5 & 1) > 0);
                this.UsingBridgeGprs = ((b5 >> 1 & 1) != 0);
                this.UsingBridgeWifi = ((b5 >> 1 & 1) == 0);
                this.UsingLocalPort1 = ((b5 >> 2 & 1) != 0);
                this.UsingLocalPort2 = ((b5 >> 3 & 1) != 0);
                this.UsingRms = ((b5 >> 4 & 1) != 0);
                this.UsingRmsGprs = ((b5 >> 5 & 1) != 0);
                this.UsingRmsWifi = ((b5 >> 5 & 1) == 0);
                num += 2;
                this.WiredHumSensors = new List<WirelessSensor>();
                for (int m = 0; m < 2; m++)
                {
                    byte b6 = byte.Parse(content.Substring(num, 2), (NumberStyles)515);
                    WirelessSensor wirelessSensor = new WirelessSensor();
                    wirelessSensor.No = m;
                    if (b6 == 254)
                    {
                        wirelessSensor.Available = false;
                        wirelessSensor.Connected = false;
                    }
                    else if (b6 == 255)
                    {
                        wirelessSensor.Available = true;
                        wirelessSensor.Connected = false;
                    }
                    else
                    {
                        wirelessSensor.Available = true;
                        wirelessSensor.Connected = true;
                        wirelessSensor.Humidity = b6;
                    }
                    this.WiredHumSensors.Add(wirelessSensor);
                    num += 2;
                }
                byte b7 = byte.Parse(content.Substring(num, 2), (NumberStyles)515);
                this.DuringSms = ((b7 & 1) > 0);
                this.DuringEmail = ((b7 >> 1 & 1) != 0);
                this.DuringIncomingCall = ((b7 >> 2 & 1) != 0);
                this.DuringOutgoingCall = ((b7 >> 3 & 1) != 0);
                this.DuringVar1U = ((b7 >> 4 & 1) != 0);
                num += 2;
            }
            catch (ParseStatusException arg_85D_0)
            {
                throw arg_85D_0;
            }
            catch (Exception arg_85E_0)
            {
                throw new ParseStatusException(arg_85E_0.Message);
            }
        }

        public int Reason { get; internal set; }

        public List<User> Users { get; internal set; }
        public byte ByteRet { get; internal set; }
        public ThermostatProfile ThermostatProfile { get; internal set; }

        public bool canedit { get; set; }
    }
}