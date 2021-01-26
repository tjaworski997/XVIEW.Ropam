using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class Input : INotifyPropertyChanged
    {
        public Input Clone()
        {
            return new Input
            {
                ChangeAfter = this.ChangeAfter,
                Id = this.Id,
                Name = this.Name,
                No = this.No,
                Selected = this.Selected,
                Site = this.Site,
                State = this.State
            };
        }

        public string AddImage
        {
            // Token: 0x06000037 RID: 55 RVA: 0x00004325 File Offset: 0x00002525
            get
            {
                if (this.IsLowBattery)
                {
                    return "Assets/bateria20x20.png";
                }
                if (this.IsFailure)
                {
                    return "Assets/uwaga20x20.png";
                }
                if (this.IsAlarm)
                {
                    return "Assets/alarm20x20.png";
                }
                return null;
            }
        }

        public DateTime ChangeAfter
        {
            get
            {
                return this.changeAfter;
            }
            set
            {
                if (this.changeAfter != value)
                {
                    this.changeAfter = value;
                }
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }
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

        // Token: 0x17000010 RID: 16
        public string Image
        {
            // Token: 0x06000036 RID: 54 RVA: 0x0000426C File Offset: 0x0000246C
            get
            {
                string result;
                switch (this.state)
                {
                    case 0:
                        result = "Assets/green50.png";
                        break;

                    case 1:
                        result = "Assets/red50.png";
                        break;

                    case 2:
                        result = "Assets/yellow50.png";
                        break;

                    case 3:
                        result = "Assets/gray50.png";
                        break;

                    case 4:
                        result = "Assets/bordo_dark50.png";
                        break;

                    case 5:
                        result = "Assets/uwaga50";
                        break;

                    case 6:
                        result = "Assets/green50.png";
                        break;

                    case 7:
                        result = "Assets/red50.png";
                        break;

                    case 8:
                        result = "Assets/green50.png";
                        break;

                    case 9:
                        result = "Assets/red50.png";
                        break;

                    case 10:
                        result = "Assets/violet50.png";
                        break;

                    case 11:
                        result = "Assets/gray50.png";
                        break;

                    default:
                        result = "Assets/gray50.png";
                        break;
                }
                return result;
            }
        }

        // Token: 0x1700000E RID: 14
        public bool IsAlarm
        {
            // Token: 0x06000033 RID: 51 RVA: 0x0000421D File Offset: 0x0000241D
            get
            {
                return this.state == 6 || this.state == 7;
            }
        }

        public bool IsFailure
        {
            // Token: 0x06000032 RID: 50 RVA: 0x00004206 File Offset: 0x00002406
            get
            {
                return this.state == 5 || this.state == 10;
            }
        }

        public bool IsLowBattery
        {
            // Token: 0x06000031 RID: 49 RVA: 0x000041EF File Offset: 0x000023EF
            get
            {
                return this.state == 8 || this.state == 9;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
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

        // Token: 0x17000008 RID: 8
        public int No
        {
            // Token: 0x0600002A RID: 42 RVA: 0x00004105 File Offset: 0x00002305
            get
            {
                return this.no;
            }
            // Token: 0x06000029 RID: 41 RVA: 0x000040D5 File Offset: 0x000022D5
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

        // Token: 0x1700000F RID: 15
        public bool Selected
        {
            // Token: 0x06000035 RID: 53 RVA: 0x00004263 File Offset: 0x00002463
            get
            {
                return this.selected;
            }
            // Token: 0x06000034 RID: 52 RVA: 0x00004233 File Offset: 0x00002433
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

        // Token: 0x1700000B RID: 11
        public int Seq
        {
            // Token: 0x06000030 RID: 48 RVA: 0x000041D5 File Offset: 0x000023D5
            get
            {
                if (this.seq > 0)
                {
                    return this.seq;
                }
                return this.no + 1;
            }
            // Token: 0x0600002F RID: 47 RVA: 0x000041A5 File Offset: 0x000023A5
            set
            {
                if (this.seq != value)
                {
                    this.seq = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Seq"));
                    }
                }
            }
        }

        public int Site
        {
            get
            {
                return this.site;
            }
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

        public byte State
        {
            get
            {
                return this.state;
            }
            set
            {
                if (this.state != value)
                {
                    this.state = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("State"));
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Image"));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Token: 0x0400000B RID: 11
        private DateTime changeAfter = DateTime.Now;

        // Token: 0x04000004 RID: 4
        private int id;

        // Token: 0x04000007 RID: 7
        private string name;

        // Token: 0x04000006 RID: 6
        private int no;

        // Token: 0x0400000A RID: 10
        private bool selected;

        // Token: 0x04000009 RID: 9
        private int seq;

        // Token: 0x04000005 RID: 5
        private int site;

        // Token: 0x04000008 RID: 8
        private byte state;

        // Token: 0x020000BD RID: 189
        public enum States : byte
        {
            // Token: 0x040003E3 RID: 995
            Ok,

            // Token: 0x040003E4 RID: 996
            Trigger,

            // Token: 0x040003E5 RID: 997
            Sabotage,

            // Token: 0x040003E6 RID: 998
            Disabled,

            // Token: 0x040003E7 RID: 999
            Locked,

            // Token: 0x040003E8 RID: 1000
            Fail,

            // Token: 0x040003E9 RID: 1001
            AlarmOk,

            // Token: 0x040003EA RID: 1002
            AlarmTrigger,

            // Token: 0x040003EB RID: 1003
            BaterryLowOk,

            // Token: 0x040003EC RID: 1004
            BaterryLowTrigger,

            // Token: 0x040003ED RID: 1005
            LinkFail,

            // Token: 0x040003EE RID: 1006
            NoInfo,

            // Token: 0x040003EF RID: 1007
            Analog
        }
    }
}