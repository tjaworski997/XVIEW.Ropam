using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class TempSensor : INotifyPropertyChanged
    {
        // Token: 0x06000451 RID: 1105 RVA: 0x00017E24 File Offset: 0x00016024
        public TempSensor Clone()
        {
            return new TempSensor
            {
                Id = this.Id,
                No = this.No,
                Site = this.Site,
                State = this.State,
                Value = this.Value,
                Name = this.Name,
                Tha = this.Tha,
                Thb = this.Thb,
                Selected = this.Selected
            };
        }

        public int Id
        {
            // Token: 0x06000440 RID: 1088 RVA: 0x00017C55 File Offset: 0x00015E55
            get
            {
                return this.id;
            }
            // Token: 0x0600043F RID: 1087 RVA: 0x00017C25 File Offset: 0x00015E25
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

        // Token: 0x17000170 RID: 368
        public string Name
        {
            // Token: 0x0600044A RID: 1098 RVA: 0x00017D72 File Offset: 0x00015F72
            get
            {
                return this.name;
            }
            // Token: 0x06000449 RID: 1097 RVA: 0x00017D3D File Offset: 0x00015F3D
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

        // Token: 0x1700016C RID: 364
        public int No
        {
            // Token: 0x06000442 RID: 1090 RVA: 0x00017C8D File Offset: 0x00015E8D
            get
            {
                return this.no;
            }
            // Token: 0x06000441 RID: 1089 RVA: 0x00017C5D File Offset: 0x00015E5D
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
            // Token: 0x0600044C RID: 1100 RVA: 0x00017DAA File Offset: 0x00015FAA
            get
            {
                return this.selected;
            }
            // Token: 0x0600044B RID: 1099 RVA: 0x00017D7A File Offset: 0x00015F7A
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

        // Token: 0x1700016D RID: 365
        public int Site
        {
            // Token: 0x06000444 RID: 1092 RVA: 0x00017CC5 File Offset: 0x00015EC5
            get
            {
                return this.site;
            }
            // Token: 0x06000443 RID: 1091 RVA: 0x00017C95 File Offset: 0x00015E95
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
            // Token: 0x06000446 RID: 1094 RVA: 0x00017CFD File Offset: 0x00015EFD
            get
            {
                return this.state;
            }
            // Token: 0x06000445 RID: 1093 RVA: 0x00017CCD File Offset: 0x00015ECD
            set
            {
                if (this.state != value)
                {
                    this.state = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("State"));
                    }
                }
            }
        }

        public float Tha
        {
            // Token: 0x0600044E RID: 1102 RVA: 0x00017DE2 File Offset: 0x00015FE2
            get
            {
                return this.tha;
            }
            // Token: 0x0600044D RID: 1101 RVA: 0x00017DB2 File Offset: 0x00015FB2
            set
            {
                if (this.tha != value)
                {
                    this.tha = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Tha"));
                    }
                }
            }
        }

        public float Thb
        {
            // Token: 0x06000450 RID: 1104 RVA: 0x00017E1A File Offset: 0x0001601A
            get
            {
                return this.thb;
            }
            // Token: 0x0600044F RID: 1103 RVA: 0x00017DEA File Offset: 0x00015FEA
            set
            {
                if (this.thb != value)
                {
                    this.thb = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Thb"));
                    }
                }
            }
        }

        public float Value
        {
            // Token: 0x06000448 RID: 1096 RVA: 0x00017D35 File Offset: 0x00015F35
            get
            {
                return this.val;
            }
            // Token: 0x06000447 RID: 1095 RVA: 0x00017D05 File Offset: 0x00015F05
            set
            {
                if (this.val != value)
                {
                    this.val = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Value"));
                    }
                }
            }
        }

        // Token: 0x1400000F RID: 15
        // Token: 0x0600043D RID: 1085 RVA: 0x00017BB8 File Offset: 0x00015DB8
        // Token: 0x0600043E RID: 1086 RVA: 0x00017BF0 File Offset: 0x00015DF0
        public event PropertyChangedEventHandler PropertyChanged;

        // Token: 0x040001BD RID: 445
        private int id;

        // Token: 0x040001C2 RID: 450
        private string name;

        // Token: 0x040001BE RID: 446
        private int no;

        // Token: 0x040001C5 RID: 453
        private bool selected;

        // Token: 0x040001BF RID: 447
        private int site;

        // Token: 0x040001C0 RID: 448
        private byte state;

        // Token: 0x040001C3 RID: 451
        private float tha;

        // Token: 0x040001C4 RID: 452
        private float thb;

        // Token: 0x040001C1 RID: 449
        private float val;

        // Token: 0x02000116 RID: 278
        public enum States : byte
        {
            // Token: 0x0400051B RID: 1307
            Ok,

            // Token: 0x0400051C RID: 1308
            Fail,

            // Token: 0x0400051D RID: 1309
            Absence
        }
    }
}