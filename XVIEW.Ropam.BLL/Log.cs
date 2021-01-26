using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL
{
    public static class Log
    {
        public static void Trace(string l)
        {
            Console.WriteLine(l);
        }

        public static void Warn(string l)
        {
            Console.WriteLine(l);
        }

        public static void Info(string l)
        {
            Console.WriteLine(l);
        }

        public static void Debug(string l)
        {
            Console.WriteLine(l);
        }

        //public static NLog.Logger L = NLog.LogManager.GetCurrentClassLogger();
    }
}