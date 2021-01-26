using System;
using System.Collections.Generic;

namespace XVIEW.Ropam.BLL.BO
{
    public class NeoConfig
    {
        public string analog { get; set; }
        public bool canedit { get; set; }
        public string deviceID { get; set; }
        public List<string> inputs { get; set; }

        public uint inp_s1
        {
            set
            {
                this.Zone1Inputs = new List<bool>(new bool[32]);
                for (int i = 0; i < 32; i++)
                {
                    this.Zone1Inputs[i] = ((ulong)value & (ulong)(1L << (i & 31))) > 0uL;
                }
            }
        }

        public uint inp_s2
        {
            set
            {
                this.Zone2Inputs = new List<bool>(new bool[32]);
                for (int i = 0; i < 32; i++)
                {
                    this.Zone2Inputs[i] = ((ulong)value & (ulong)(1L << (i & 31))) > 0uL;
                }
            }
        }

        public List<string> outputs { get; set; }

        public uint out_s1
        {
            set
            {
                this.Zone1Outputs = new List<bool>(new bool[32]);
                for (int i = 0; i < 32; i++)
                {
                    this.Zone1Outputs[i] = ((ulong)value & (ulong)(1L << (i & 31))) > 0uL;
                }
            }
        }

        public uint out_s2
        {
            // Token: 0x06000283 RID: 643 RVA: 0x000143F0 File Offset: 0x000125F0
            set
            {
                this.Zone2Outputs = new List<bool>(new bool[32]);
                for (int i = 0; i < 32; i++)
                {
                    this.Zone2Outputs[i] = ((ulong)value & (ulong)(1L << (i & 31))) > 0uL;
                }
            }
        }

        public string phone { get; set; }
        public List<string> temps { get; set; }
        public int th_esens { get; set; }
        public int th_isens { get; set; }
        public string th_name { get; set; }
        public List<Widget> widgets { get; set; }
        public List<string> wiredsens { get; set; }

        public List<string> wirelessSensors { get; set; }

        public string wrlsens
        {
            set
            {
                if (value != null)
                {
                    this.wirelessSensors = new List<string>(value.Split(new char[]
                    {
                        '\n'
                    }));
                }
            }
        }

        public List<bool> Zone1Inputs { get; set; }
        public List<bool> Zone1Outputs { get; set; }
        public List<bool> Zone2Inputs { get; set; }
        public List<bool> Zone2Outputs { get; set; }
        public List<string> zones { get; set; }
    }
}