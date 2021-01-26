using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using XVIEW.Ropam.BLL.Services;

namespace XVIEW.Ropam.SERV
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BLL.Config.AlarmIp = "10.0.0.1";
            BLL.Config.AlarmPort = 5003;
            BLL.Config.MainBoardId = "1500067211111111";
            BLL.Config.ConnectionTimeOut = 3000;
            BLL.Config.WorkerInterval = TimeSpan.FromMilliseconds(200);

            BLL.Config.HasloKomunikacji = "1111";
            BLL.Config.KluczSzyfrowaniaSzyfrowaniaTCP = "dac211111111111";

            var sc = new BLL.BO.SiteConnection();
            sc.Connect();

            sc.OnStatus = (a, b) =>
            {
            };

            var res = sc.Login2();
            sc.Start();

            WebServer ws = new WebServer(SendResponse, "http://192.168.1.101:7999/test/");
            ws.Run();

            while (true)
            {
                var c = Console.ReadKey();

                if (c.Key == ConsoleKey.N)
                {
                    new BLL.Test().O(sc, 1, true);
                }

                if (c.Key == ConsoleKey.F)
                {
                    new BLL.Test().O(sc, 1, false);
                }
            }
        }

        public static string SendResponse(HttpListenerRequest request)
        {
            return string.Format("<HTML><BODY>My web page.<br>{0}</BODY></HTML>", DateTime.Now);
        }
    }
}