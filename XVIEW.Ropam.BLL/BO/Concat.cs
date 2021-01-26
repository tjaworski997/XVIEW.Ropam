using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class Contact : INotifyPropertyChanged
    {
        public Contact Clone()
        {
            return new Contact
            {
                idx = this.idx,
                name = this.name,
                phone = this.phone,
                group = this.group
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Contact contact = obj as Contact;
            return contact.Group == this.group && contact.Idx == this.idx && !(contact.Name != this.name) && !(contact.Phone != this.phone);
        }

        //public override int GetHashCode()
        //{
        //    return (((((((((
        //        -804043712 * -1521134295 + this.idx.GetHashCode()) * -1521134295 +
        //        EqualityComparer<string>.Default.GetHashCode(this.name)) * -1521134295 +
        //        this.group.GetHashCode()) * -1521134295 +
        //        EqualityComparer<string>.Default.GetHashCode(this.phone)) * -1521134295 +
        //        this.action.GetHashCode()) * -1521134295 +
        //        this.Idx.GetHashCode()) * -1521134295 +
        //        EqualityComparer<string>.Default.GetHashCode(this.Name)) * -1521134295 +
        //        this.Group.GetHashCode()) * -1521134295 +
        //        EqualityComparer<string>.Default.GetHashCode(this.Phone)) * -1521134295 +
        //        this.Action.GetHashCode();
        //}

        // Token: 0x06000408 RID: 1032 RVA: 0x000173E0 File Offset: 0x000155E0
        public string ToLine()
        {
            return string.Concat(new object[]
            {
                this.idx,
                ",",
                (this.name != null) ? this.name.Replace(",", "") : "",
                ",",
                (this.phone != null) ? this.phone.Replace(",", "") : "",
                ",",
                this.group.ToString(),
                ",",
                (this.action == 0) ? "+" : ((this.action == 1) ? "-" : ">"),
                "\n"
            });
        }

        // Token: 0x17000152 RID: 338
        private byte action
        {
            // Token: 0x06000406 RID: 1030 RVA: 0x000173CD File Offset: 0x000155CD
            get;
            // Token: 0x06000407 RID: 1031 RVA: 0x000173D5 File Offset: 0x000155D5
            set;
        }

        // Token: 0x17000157 RID: 343
        public byte Action
        {
            // Token: 0x06000415 RID: 1045 RVA: 0x0001775A File Offset: 0x0001595A
            get
            {
                return this.action;
            }
            // Token: 0x06000414 RID: 1044 RVA: 0x0001772A File Offset: 0x0001592A
            set
            {
                if (this.action != value)
                {
                    this.action = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Action"));
                    }
                }
            }
        }

        // Token: 0x17000150 RID: 336
        private char group
        {
            // Token: 0x06000402 RID: 1026 RVA: 0x000173AB File Offset: 0x000155AB
            get;
            // Token: 0x06000403 RID: 1027 RVA: 0x000173B3 File Offset: 0x000155B3
            set;
        }

        // Token: 0x17000155 RID: 341
        public char Group
        {
            // Token: 0x06000411 RID: 1041 RVA: 0x000176E5 File Offset: 0x000158E5
            get
            {
                return this.group;
            }
            // Token: 0x06000410 RID: 1040 RVA: 0x000176B5 File Offset: 0x000158B5
            set
            {
                if (this.group != value)
                {
                    this.group = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Group"));
                    }
                }
            }
        }

        // Token: 0x1700014E RID: 334
        private ushort idx
        {
            // Token: 0x060003FE RID: 1022 RVA: 0x00017389 File Offset: 0x00015589
            get;
            // Token: 0x060003FF RID: 1023 RVA: 0x00017391 File Offset: 0x00015591
            set;
        }

        // Token: 0x17000153 RID: 339
        public ushort Idx
        {
            // Token: 0x0600040D RID: 1037 RVA: 0x00017670 File Offset: 0x00015870
            get
            {
                return this.idx;
            }
            // Token: 0x0600040C RID: 1036 RVA: 0x00017640 File Offset: 0x00015840
            set
            {
                if (this.idx != value)
                {
                    this.idx = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Idx"));
                    }
                }
            }
        }

        // Token: 0x1700014F RID: 335
        private string name
        {
            // Token: 0x06000400 RID: 1024 RVA: 0x0001739A File Offset: 0x0001559A
            get;
            // Token: 0x06000401 RID: 1025 RVA: 0x000173A2 File Offset: 0x000155A2
            set;
        }

        // Token: 0x17000154 RID: 340
        public string Name
        {
            // Token: 0x0600040F RID: 1039 RVA: 0x000176AD File Offset: 0x000158AD
            get
            {
                return this.name;
            }
            // Token: 0x0600040E RID: 1038 RVA: 0x00017678 File Offset: 0x00015878
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

        // Token: 0x17000151 RID: 337
        private string phone
        {
            // Token: 0x06000404 RID: 1028 RVA: 0x000173BC File Offset: 0x000155BC
            get;
            // Token: 0x06000405 RID: 1029 RVA: 0x000173C4 File Offset: 0x000155C4
            set;
        }

        // Token: 0x17000156 RID: 342
        public string Phone
        {
            // Token: 0x06000413 RID: 1043 RVA: 0x00017722 File Offset: 0x00015922
            get
            {
                return this.phone;
            }
            // Token: 0x06000412 RID: 1042 RVA: 0x000176ED File Offset: 0x000158ED
            set
            {
                if (this.phone != value)
                {
                    this.phone = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Phone"));
                    }
                }
            }
        }

        // Token: 0x1400000D RID: 13
        // Token: 0x060003FC RID: 1020 RVA: 0x0001731C File Offset: 0x0001551C
        // Token: 0x060003FD RID: 1021 RVA: 0x00017354 File Offset: 0x00015554
        public event PropertyChangedEventHandler PropertyChanged;

        // Token: 0x02000113 RID: 275
        public enum Actions : byte
        {
            // Token: 0x04000508 RID: 1288
            Add,

            // Token: 0x04000509 RID: 1289
            Delete,

            // Token: 0x0400050A RID: 1290
            Edit
        }
    }
}