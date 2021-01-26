using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class Command
    {
        public Command SetLang(string lang)
        {
            this.Lang = lang;
            return this;
        }

        public override string ToString()
        {
            return this.Description[this.Lang];
        }

        // Token: 0x1700008D RID: 141
        public Dictionary<string, string> Description
        {
            // Token: 0x0600025C RID: 604 RVA: 0x000141B5 File Offset: 0x000123B5
            get;
            // Token: 0x0600025D RID: 605 RVA: 0x000141BD File Offset: 0x000123BD
            set;
        }

        // Token: 0x1700008B RID: 139
        public Dictionary<string, string> Format
        {
            // Token: 0x06000258 RID: 600 RVA: 0x00014193 File Offset: 0x00012393
            get;
            // Token: 0x06000259 RID: 601 RVA: 0x0001419B File Offset: 0x0001239B
            set;
        }

        // Token: 0x1700008C RID: 140
        public Dictionary<string, string> FormatShort
        {
            // Token: 0x0600025A RID: 602 RVA: 0x000141A4 File Offset: 0x000123A4
            get;
            // Token: 0x0600025B RID: 603 RVA: 0x000141AC File Offset: 0x000123AC
            set;
        }

        // Token: 0x1700008E RID: 142
        public Dictionary<string, string> Hint
        {
            // Token: 0x0600025E RID: 606 RVA: 0x000141C6 File Offset: 0x000123C6
            get;
            // Token: 0x0600025F RID: 607 RVA: 0x000141CE File Offset: 0x000123CE
            set;
        }

        // Token: 0x1700008A RID: 138
        public string Key
        {
            // Token: 0x06000256 RID: 598 RVA: 0x00014182 File Offset: 0x00012382
            get;
            // Token: 0x06000257 RID: 599 RVA: 0x0001418A File Offset: 0x0001238A
            set;
        }

        // Token: 0x17000089 RID: 137
        public string Lang
        {
            // Token: 0x06000254 RID: 596 RVA: 0x00014171 File Offset: 0x00012371
            get
            {
                return this.lang;
            }
            // Token: 0x06000255 RID: 597 RVA: 0x00014179 File Offset: 0x00012379
            set
            {
                this.lang = value;
            }
        }

        // Token: 0x040000E7 RID: 231
        private string lang = "en";

        // Token: 0x040000E9 RID: 233
        public bool ManualSend;
    }
}