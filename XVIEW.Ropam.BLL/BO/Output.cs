using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class Output : INotifyPropertyChanged
    {
        // Token: 0x0600003F RID: 63 RVA: 0x00004460 File Offset: 0x00002660
        public Output()
        {
            this.state = 1;
            this.selected = (this.state == 1 || this.state == 2 || this.state == 4 || this.state == 6);
        }

        // Token: 0x06000040 RID: 64 RVA: 0x000044B2 File Offset: 0x000026B2
        public Output(byte state)
        {
            this.state = state;
            this.selected = (state == 1 || state == 2 || state == 4 || state == 6);
        }

        // Token: 0x06000053 RID: 83 RVA: 0x000047B8 File Offset: 0x000029B8
        public Output Clone()
        {
            return new Output
            {
                ChangeAfter = this.ChangeAfter,
                No = this.No,
                Name = this.Name,
                Id = this.Id,
                Selected = this.Selected,
                Site = this.Site,
                State = this.State
            };
        }

        public bool AllowChange
        {
            // Token: 0x0600004D RID: 77 RVA: 0x000046A0 File Offset: 0x000028A0
            get
            {
                return this.state != 4 && this.state != 5 && this.state != 6 && this.state != 7 && this.state != 8;
            }
        }

        public DateTime ChangeAfter
        {
            // Token: 0x06000052 RID: 82 RVA: 0x000047B0 File Offset: 0x000029B0
            get
            {
                return this.changeAfter;
            }
            // Token: 0x06000051 RID: 81 RVA: 0x00004799 File Offset: 0x00002999
            set
            {
                if (this.changeAfter != value)
                {
                    this.changeAfter = value;
                }
            }
        }

        // Token: 0x17000013 RID: 19
        public int Id
        {
            // Token: 0x06000042 RID: 66 RVA: 0x00004515 File Offset: 0x00002715
            get
            {
                return this.id;
            }
            // Token: 0x06000041 RID: 65 RVA: 0x000044E5 File Offset: 0x000026E5
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

        // Token: 0x1700001C RID: 28
        public string Image
        {
            // Token: 0x06000050 RID: 80 RVA: 0x0000470C File Offset: 0x0000290C
            get
            {
                string result = "Assets/green30.png";
                switch (this.state)
                {
                    case 0:
                        result = "Assets/claret30.png";
                        break;

                    case 1:
                        result = "Assets/red30.png";
                        break;

                    case 2:
                        result = "Assets/gray30.png";
                        break;

                    case 3:
                        result = "Assets/gray30.png";
                        break;

                    case 4:
                        result = "Assets/red30.png";
                        break;

                    case 5:
                        result = "Assets/claret30.png";
                        break;

                    case 6:
                        result = "Assets/red30.png";
                        break;

                    case 7:
                        result = "Assets/claret30.png";
                        break;

                    case 8:
                        result = "Assets/gray30.png";
                        break;
                }
                return result;
            }
        }

        // Token: 0x17000018 RID: 24
        public bool IsOn
        {
            // Token: 0x0600004B RID: 75 RVA: 0x00004647 File Offset: 0x00002847
            get
            {
                return this.state == 2 || this.state == 6 || this.state == 4 || this.state == 1;
            }
        }

        // Token: 0x17000016 RID: 22
        public string Name
        {
            // Token: 0x06000048 RID: 72 RVA: 0x000045C2 File Offset: 0x000027C2
            get
            {
                return this.name;
            }
            // Token: 0x06000047 RID: 71 RVA: 0x0000458D File Offset: 0x0000278D
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

        // Token: 0x17000015 RID: 21
        public int No
        {
            // Token: 0x06000046 RID: 70 RVA: 0x00004585 File Offset: 0x00002785
            get
            {
                return this.no;
            }
            // Token: 0x06000045 RID: 69 RVA: 0x00004555 File Offset: 0x00002755
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

        // Token: 0x17000019 RID: 25
        public bool NoControl
        {
            // Token: 0x0600004C RID: 76 RVA: 0x0000466F File Offset: 0x0000286F
            get
            {
                return this.state == 8 || this.state == 4 || this.state == 5 || this.state == 6 || this.state == 7;
            }
        }

        // Token: 0x1700001B RID: 27
        public bool Selected
        {
            // Token: 0x0600004F RID: 79 RVA: 0x00004704 File Offset: 0x00002904
            get
            {
                return this.selected;
            }
            // Token: 0x0600004E RID: 78 RVA: 0x000046D4 File Offset: 0x000028D4
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

        // Token: 0x17000014 RID: 20
        public int Site
        {
            // Token: 0x06000044 RID: 68 RVA: 0x0000454D File Offset: 0x0000274D
            get
            {
                return this.site;
            }
            // Token: 0x06000043 RID: 67 RVA: 0x0000451D File Offset: 0x0000271D
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

        // Token: 0x17000017 RID: 23
        public byte State
        {
            // Token: 0x0600004A RID: 74 RVA: 0x0000463F File Offset: 0x0000283F
            get
            {
                return this.state;
            }
            // Token: 0x06000049 RID: 73 RVA: 0x000045CC File Offset: 0x000027CC
            set
            {
                if (this.state != value)
                {
                    this.state = value;
                    bool flag = this.state == 1 || this.state == 2 || this.state == 4 || this.state == 6;
                    if (this.selected != flag)
                    {
                        this.selected = flag;
                    }
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("State"));
                    }
                }
            }
        }

        // Token: 0x14000002 RID: 2
        // Token: 0x0600003D RID: 61 RVA: 0x000043F0 File Offset: 0x000025F0
        // Token: 0x0600003E RID: 62 RVA: 0x00004428 File Offset: 0x00002628
        public event PropertyChangedEventHandler PropertyChanged;

        // Token: 0x04000013 RID: 19
        private DateTime changeAfter = DateTime.Now;

        // Token: 0x0400000D RID: 13
        private int id;

        // Token: 0x04000010 RID: 16
        private string name;

        // Token: 0x0400000F RID: 15
        private int no;

        // Token: 0x04000012 RID: 18
        private bool selected;

        // Token: 0x0400000E RID: 14
        private int site;

        // Token: 0x04000011 RID: 17
        private byte state;

        // Token: 0x020000BE RID: 190
        public enum States : byte
        {
            // Token: 0x040003F1 RID: 1009
            Off,

            // Token: 0x040003F2 RID: 1010
            On,

            // Token: 0x040003F3 RID: 1011
            FailOn,

            // Token: 0x040003F4 RID: 1012
            FailOff,

            // Token: 0x040003F5 RID: 1013
            NoControlOn,

            // Token: 0x040003F6 RID: 1014
            NoControlOff,

            // Token: 0x040003F7 RID: 1015
            NoControlFailOn,

            // Token: 0x040003F8 RID: 1016
            NoControlFailOff,

            // Token: 0x040003F9 RID: 1017
            NoOutput
        }
    }
}