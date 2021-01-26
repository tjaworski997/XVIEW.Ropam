using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL
{
    public class Test
    {
        public void O(BO.SiteConnection sc, int o, bool wlaczony)
        {
            StringBuilder mask = new StringBuilder("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");

            for (int i = 0; i < 32; i++)
            {
                if (i == o)
                {
                    if (wlaczony)
                    {
                        mask[i] = '1';
                    }
                    else
                    {
                        mask[i] = '0';
                    }
                }
            }

            sc.SetOutputs(mask.ToString());
        }
    }
}