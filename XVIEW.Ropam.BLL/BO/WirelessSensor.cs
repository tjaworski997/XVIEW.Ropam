using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class WirelessSensor : INotifyPropertyChanged
    {
        // Token: 0x0600048E RID: 1166 RVA: 0x00003FBC File Offset: 0x000021BC
        public WirelessSensor()
        {
        }

        // Token: 0x0600048F RID: 1167 RVA: 0x00018684 File Offset: 0x00016884
        public WirelessSensor(string data)
        {
            this.Humidity = byte.Parse(data.Substring(4, 2), (NumberStyles)515);
            this.Vbat = (float)(byte.Parse(data.Substring(6, 2), (NumberStyles)515) / 10);
            float num = (float)short.Parse(data.Substring(0, 4), (NumberStyles)515) / 10f;
            this.Temperature = num;
            string text = data.Substring(8, 1);
            if (text == "0")
            {
                this.available = false;
                this.connected = false;
                return;
            }
            if (text == "1")
            {
                this.available = true;
                this.connected = true;
                if (this.temperature < -998f)
                {
                    this.connected = false;
                    return;
                }
            }
            else
            {
                if (text == "2")
                {
                    this.available = true;
                    this.connected = false;
                    return;
                }
                this.available = false;
                this.connected = false;
            }
        }

        // Token: 0x060004A7 RID: 1191 RVA: 0x00018A7A File Offset: 0x00016C7A
        public void Copy(WirelessSensor sensor)
        {
            sensor.Available = this.Available;
            sensor.Connected = this.Connected;
            sensor.Humidity = this.Humidity;
            sensor.Temperature = this.Temperature;
            sensor.Vbat = this.vbat;
        }

        public bool Available
        {
            // Token: 0x060004A2 RID: 1186 RVA: 0x00018A02 File Offset: 0x00016C02
            get
            {
                return this.available;
            }
            // Token: 0x060004A1 RID: 1185 RVA: 0x000189D2 File Offset: 0x00016BD2
            set
            {
                if (this.available != value)
                {
                    this.available = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Available"));
                    }
                }
            }
        }

        public bool Connected
        {
            // Token: 0x060004A4 RID: 1188 RVA: 0x00018A3A File Offset: 0x00016C3A
            get
            {
                return this.connected;
            }
            // Token: 0x060004A3 RID: 1187 RVA: 0x00018A0A File Offset: 0x00016C0A
            set
            {
                if (this.connected != value)
                {
                    this.connected = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Connected"));
                    }
                }
            }
        }

        public byte Humidity
        {
            // Token: 0x0600049C RID: 1180 RVA: 0x00018925 File Offset: 0x00016B25
            get
            {
                return this.humidity;
            }
            // Token: 0x0600049B RID: 1179 RVA: 0x000188D4 File Offset: 0x00016AD4
            set
            {
                if (this.humidity != value)
                {
                    this.humidity = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Humidity"));
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("HumidityStr"));
                    }
                }
            }
        }

        public string HumidityStr
        {
            // Token: 0x0600049D RID: 1181 RVA: 0x0001892D File Offset: 0x00016B2D
            get
            {
                if (!this.connected)
                {
                    return "";
                }
                return this.humidity + " %";
            }
        }

        public int Id
        {
            // Token: 0x06000491 RID: 1169 RVA: 0x0001879B File Offset: 0x0001699B
            get
            {
                return this.id;
            }
            // Token: 0x06000490 RID: 1168 RVA: 0x0001876B File Offset: 0x0001696B
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Id"));
                    }
                }
            }
        }

        // Token: 0x1700018C RID: 396
        public string Name
        {
            // Token: 0x06000497 RID: 1175 RVA: 0x00018848 File Offset: 0x00016A48
            get
            {
                return this.name;
            }
            // Token: 0x06000496 RID: 1174 RVA: 0x00018813 File Offset: 0x00016A13
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
                    }
                }
            }
        }

        // Token: 0x1700018B RID: 395
        public int No
        {
            // Token: 0x06000495 RID: 1173 RVA: 0x0001880B File Offset: 0x00016A0B
            get
            {
                return this.no;
            }
            // Token: 0x06000494 RID: 1172 RVA: 0x000187DB File Offset: 0x000169DB
            set
            {
                if (this.no != value)
                {
                    this.no = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("No"));
                    }
                }
            }
        }

        public bool Selected
        {
            // Token: 0x060004A6 RID: 1190 RVA: 0x00018A72 File Offset: 0x00016C72
            get
            {
                return this.selected;
            }
            // Token: 0x060004A5 RID: 1189 RVA: 0x00018A42 File Offset: 0x00016C42
            set
            {
                if (this.selected != value)
                {
                    this.selected = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Selected"));
                    }
                }
            }
        }

        // Token: 0x1700018A RID: 394
        public int Site
        {
            // Token: 0x06000493 RID: 1171 RVA: 0x000187D3 File Offset: 0x000169D3
            get
            {
                return this.site;
            }
            // Token: 0x06000492 RID: 1170 RVA: 0x000187A3 File Offset: 0x000169A3
            set
            {
                if (this.site != value)
                {
                    this.site = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Site"));
                    }
                }
            }
        }

        public float Temperature
        {
            // Token: 0x06000499 RID: 1177 RVA: 0x000188A1 File Offset: 0x00016AA1
            get
            {
                return this.temperature;
            }
            // Token: 0x06000498 RID: 1176 RVA: 0x00018850 File Offset: 0x00016A50
            set
            {
                if (this.temperature != value)
                {
                    this.temperature = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Temperature"));
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TemperatureStr"));
                    }
                }
            }
        }

        public string TemperatureStr
        {
            // Token: 0x0600049A RID: 1178 RVA: 0x000188A9 File Offset: 0x00016AA9
            get
            {
                if (!this.connected)
                {
                    return "";
                }
                return this.temperature.ToString("0.0") + " °C";
            }
        }

        public float Vbat
        {
            // Token: 0x0600049F RID: 1183 RVA: 0x000189A5 File Offset: 0x00016BA5
            get
            {
                return this.vbat;
            }
            // Token: 0x0600049E RID: 1182 RVA: 0x00018954 File Offset: 0x00016B54
            set
            {
                if (this.vbat != value)
                {
                    this.vbat = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Vbat"));
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("VbatStr"));
                    }
                }
            }
        }

        public string VbatStr
        {
            // Token: 0x060004A0 RID: 1184 RVA: 0x000189AD File Offset: 0x00016BAD
            get
            {
                if (!this.connected)
                {
                    return "";
                }
                return this.humidity + " V";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Token: 0x040001E5 RID: 485
        private bool available;

        // Token: 0x040001E6 RID: 486
        private bool connected;

        // Token: 0x040001E3 RID: 483
        private byte humidity;

        // Token: 0x040001DE RID: 478
        private int id;

        // Token: 0x040001E1 RID: 481
        private string name;

        // Token: 0x040001E0 RID: 480
        private int no;

        // Token: 0x040001E7 RID: 487
        private bool selected;

        // Token: 0x040001DF RID: 479
        private int site;

        // Token: 0x040001E2 RID: 482
        private float temperature;

        // Token: 0x040001E4 RID: 484
        private float vbat;
    }
}