using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL.BO
{
    public class ThermostatProfile
    {
        public byte Id { get; private set; }
        public float[] Presents { get; private set; }

        public static ThermostatProfile FromBytes(byte[] bytes)
        {
            return new ThermostatProfile();
        }

        internal int ToBytes()
        {
            throw new NotImplementedException();
        }
    }
}