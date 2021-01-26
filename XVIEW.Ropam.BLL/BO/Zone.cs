using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class Zone : INotifyPropertyChanged
    {
        // Token: 0x060000C6 RID: 198 RVA: 0x00005764 File Offset: 0x00003964
        public Zone Clone()
        {
            return new Zone
            {
                ChangeAfter = this.ChangeAfter,
                No = this.No,
                Name = this.Name,
                Id = this.Id,
                Selected = this.Selected,
                Site = this.Site,
                IsArmed = this.IsArmed,
                IsNightArmed = this.IsNightArmed,
                IsAlarm = this.IsAlarm,
                IsTamper = this.IsTamper,
                isReady = this.IsReady,
                OnEntrance = this.OnEntrance,
                OnDeparture = this.OnDeparture
            };
        }

        // Token: 0x17000053 RID: 83
        public DateTime ChangeAfter
        {
            // Token: 0x060000C3 RID: 195 RVA: 0x00005721 File Offset: 0x00003921
            get
            {
                return this.changeAfter;
            }
            // Token: 0x060000C2 RID: 194 RVA: 0x0000570A File Offset: 0x0000390A
            set
            {
                if (this.changeAfter != value)
                {
                    this.changeAfter = value;
                }
            }
        }

        // Token: 0x17000047 RID: 71
        public int Id
        {
            // Token: 0x060000AB RID: 171 RVA: 0x00005495 File Offset: 0x00003695
            get
            {
                return this.id;
            }
            // Token: 0x060000AA RID: 170 RVA: 0x00005465 File Offset: 0x00003665
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

        // Token: 0x1700004B RID: 75
        public bool IsAlarm
        {
            // Token: 0x060000B3 RID: 179 RVA: 0x0000557A File Offset: 0x0000377A
            get
            {
                return this.isAlarm;
            }
            // Token: 0x060000B2 RID: 178 RVA: 0x0000554A File Offset: 0x0000374A
            set
            {
                if (this.isAlarm != value)
                {
                    this.isAlarm = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsAlarm"));
                    }
                }
            }
        }

        public bool IsArmed
        {
            // Token: 0x060000BD RID: 189 RVA: 0x00005692 File Offset: 0x00003892
            get
            {
                return this.isArmed;
            }
            // Token: 0x060000BC RID: 188 RVA: 0x00005662 File Offset: 0x00003862
            set
            {
                if (this.isArmed != value)
                {
                    this.isArmed = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsArmed"));
                    }
                }
            }
        }

        public bool IsNightArmed
        {
            // Token: 0x060000BF RID: 191 RVA: 0x000056CA File Offset: 0x000038CA
            get
            {
                return this.isNightArmed;
            }
            // Token: 0x060000BE RID: 190 RVA: 0x0000569A File Offset: 0x0000389A
            set
            {
                if (this.isNightArmed != value)
                {
                    this.isNightArmed = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsNightArmed"));
                    }
                }
            }
        }

        public bool IsReady
        {
            // Token: 0x060000B7 RID: 183 RVA: 0x000055EA File Offset: 0x000037EA
            get
            {
                return this.isReady;
            }
            // Token: 0x060000B6 RID: 182 RVA: 0x000055BA File Offset: 0x000037BA
            set
            {
                if (this.isReady != value)
                {
                    this.isReady = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsReady"));
                    }
                }
            }
        }

        public bool IsTamper
        {
            // Token: 0x060000B5 RID: 181 RVA: 0x000055B2 File Offset: 0x000037B2
            get
            {
                return this.isTamper;
            }
            // Token: 0x060000B4 RID: 180 RVA: 0x00005582 File Offset: 0x00003782
            set
            {
                if (this.isTamper != value)
                {
                    this.isTamper = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsTamper"));
                    }
                }
            }
        }

        // Token: 0x1700004A RID: 74
        public string Name
        {
            // Token: 0x060000B1 RID: 177 RVA: 0x00005542 File Offset: 0x00003742
            get
            {
                return this.name;
            }
            // Token: 0x060000B0 RID: 176 RVA: 0x0000550D File Offset: 0x0000370D
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

        // Token: 0x17000049 RID: 73
        public int No
        {
            // Token: 0x060000AF RID: 175 RVA: 0x00005505 File Offset: 0x00003705
            get
            {
                return this.no;
            }
            // Token: 0x060000AE RID: 174 RVA: 0x000054D5 File Offset: 0x000036D5
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

        public bool OnDeparture
        {
            // Token: 0x060000BB RID: 187 RVA: 0x0000565A File Offset: 0x0000385A
            get
            {
                return this.onDeparture;
            }
            // Token: 0x060000BA RID: 186 RVA: 0x0000562A File Offset: 0x0000382A
            set
            {
                if (this.onDeparture != value)
                {
                    this.onDeparture = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("OnDeparture"));
                    }
                }
            }
        }

        public bool OnEntrance
        {
            // Token: 0x060000B9 RID: 185 RVA: 0x00005622 File Offset: 0x00003822
            get
            {
                return this.onEntrance;
            }
            // Token: 0x060000B8 RID: 184 RVA: 0x000055F2 File Offset: 0x000037F2
            set
            {
                if (this.onEntrance != value)
                {
                    this.onEntrance = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("OnEntrance"));
                    }
                }
            }
        }

        // Token: 0x17000052 RID: 82
        public bool Selected
        {
            // Token: 0x060000C1 RID: 193 RVA: 0x00005702 File Offset: 0x00003902
            get
            {
                return this.selected;
            }
            // Token: 0x060000C0 RID: 192 RVA: 0x000056D2 File Offset: 0x000038D2
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

        // Token: 0x17000048 RID: 72
        public int Site
        {
            // Token: 0x060000AD RID: 173 RVA: 0x000054CD File Offset: 0x000036CD
            get
            {
                return this.site;
            }
            // Token: 0x060000AC RID: 172 RVA: 0x0000549D File Offset: 0x0000369D
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

        public ushort Timer
        {
            // Token: 0x060000C5 RID: 197 RVA: 0x00005759 File Offset: 0x00003959
            get
            {
                return this.timer;
            }
            // Token: 0x060000C4 RID: 196 RVA: 0x00005729 File Offset: 0x00003929
            set
            {
                if (this.timer != value)
                {
                    this.timer = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Timer"));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Token: 0x0400004A RID: 74
        private DateTime changeAfter = DateTime.Now;

        // Token: 0x0400003D RID: 61
        private int id;

        // Token: 0x04000043 RID: 67
        private bool isAlarm;

        // Token: 0x04000041 RID: 65
        private bool isArmed;

        // Token: 0x04000042 RID: 66
        private bool isNightArmed;

        // Token: 0x04000045 RID: 69
        private bool isReady;

        // Token: 0x04000044 RID: 68
        private bool isTamper;

        // Token: 0x04000040 RID: 64
        private string name;

        // Token: 0x0400003F RID: 63
        private int no;

        // Token: 0x04000047 RID: 71
        private bool onDeparture;

        // Token: 0x04000046 RID: 70
        private bool onEntrance;

        // Token: 0x04000049 RID: 73
        private bool selected;

        // Token: 0x0400003E RID: 62
        private int site;

        // Token: 0x04000048 RID: 72
        private ushort timer;
    }
}