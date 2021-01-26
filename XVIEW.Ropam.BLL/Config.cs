using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL
{
    public static class Config
    {
        public static string AlarmIp { get; set; }
        public static int AlarmPort { get; set; }
        public static int ConnectionTimeOut { get; set; }

        public static string MainBoardId { get; set; }
        public static TimeSpan WorkerInterval { get; set; }

        public static string HasloKomunikacji { get; set; }
        public static string KluczSzyfrowaniaSzyfrowaniaTCP { get; set; }

        public static byte[] ConnectVector
        {
            get { return new byte[] { 34, 52, 68, 162, 244, 205, 91, 85, 33, 52, 67, 18, 152, 173, 36, 236 }; }
        }
    }
}