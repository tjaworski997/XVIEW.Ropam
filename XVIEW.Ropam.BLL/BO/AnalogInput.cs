using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class AnalogInput : INotifyPropertyChanged
    {
        // Token: 0x0600023A RID: 570 RVA: 0x00013F3C File Offset: 0x0001213C
        public AnalogInput Clone()
        {
            return new AnalogInput
            {
                Unit = this.Unit,
                Value = this.Value
            };
        }

        public int Id
        {
            // Token: 0x0600022C RID: 556 RVA: 0x00013DBD File Offset: 0x00011FBD
            get
            {
                return this.id;
            }
            // Token: 0x0600022B RID: 555 RVA: 0x00013D8D File Offset: 0x00011F8D
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

        // Token: 0x1700007C RID: 124
        public string Name
        {
            // Token: 0x06000232 RID: 562 RVA: 0x00013E6A File Offset: 0x0001206A
            get
            {
                return this.name;
            }
            // Token: 0x06000231 RID: 561 RVA: 0x00013E35 File Offset: 0x00012035
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

        // Token: 0x1700007A RID: 122
        public int No
        {
            // Token: 0x0600022E RID: 558 RVA: 0x00013DF5 File Offset: 0x00011FF5
            get
            {
                return this.no;
            }
            // Token: 0x0600022D RID: 557 RVA: 0x00013DC5 File Offset: 0x00011FC5
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
            // Token: 0x06000234 RID: 564 RVA: 0x00013EA2 File Offset: 0x000120A2
            get
            {
                return this.selected;
            }
            // Token: 0x06000233 RID: 563 RVA: 0x00013E72 File Offset: 0x00012072
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

        // Token: 0x1700007B RID: 123
        public int Site
        {
            // Token: 0x06000230 RID: 560 RVA: 0x00013E2D File Offset: 0x0001202D
            get
            {
                return this.site;
            }
            // Token: 0x0600022F RID: 559 RVA: 0x00013DFD File Offset: 0x00011FFD
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

        public string Unit
        {
            // Token: 0x06000236 RID: 566 RVA: 0x00013EDF File Offset: 0x000120DF
            get
            {
                return this.unit;
            }
            // Token: 0x06000235 RID: 565 RVA: 0x00013EAA File Offset: 0x000120AA
            set
            {
                if (this.unit != value)
                {
                    this.unit = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Unit"));
                    }
                }
            }
        }

        public float Value
        {
            // Token: 0x06000238 RID: 568 RVA: 0x00013F17 File Offset: 0x00012117
            get
            {
                return this.val;
            }
            // Token: 0x06000237 RID: 567 RVA: 0x00013EE7 File Offset: 0x000120E7
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

        public string ValueAndUnit
        {
            // Token: 0x06000239 RID: 569 RVA: 0x00013F1F File Offset: 0x0001211F
            get
            {
                return this.val + " " + this.unit;
            }
        }

        // Token: 0x14000005 RID: 5
        // Token: 0x06000229 RID: 553 RVA: 0x00013D20 File Offset: 0x00011F20
        // Token: 0x0600022A RID: 554 RVA: 0x00013D58 File Offset: 0x00011F58
        public event PropertyChangedEventHandler PropertyChanged;

        // Token: 0x040000D7 RID: 215
        private int id;

        // Token: 0x040000DA RID: 218
        private string name;

        // Token: 0x040000D8 RID: 216
        private int no;

        // Token: 0x040000DD RID: 221
        private bool selected;

        // Token: 0x040000D9 RID: 217
        private int site;

        // Token: 0x040000DB RID: 219
        private string unit;

        // Token: 0x040000DC RID: 220
        private float val;
    }
}