using System;
using System.Globalization;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace SystemCompatibilityConsole
{
    public class Program
    {
        //Om Ganeshaye Namah
        static void Main(string[] args)
        {
            try
            {
                bool isCameradetected = ListAllCamerasAsync();

                if (isCameradetected)
                {
                    Console.WriteLine("-------------- Camera Detected. -------------------");
                }
                else
                {
                    Console.WriteLine("------------------ Couldn't Detected any Camera. -------------");
                }


                bool isStableConneciton = CheckForInternetConnection();

                if (isStableConneciton)
                {
                    Console.WriteLine("------------ Stable Conneciton -----------");
                }
                else
                {
                    Console.WriteLine("----------- Unstable Connection ----------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Camera Detection Exception" + ex.Message + ex.StackTrace + "\n");
            }
        }

        private static bool ListAllCamerasAsync()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM win32_PNPEntity WHERE PnPClass='Camera'");
            var collection = Task.Run(() => { return searcher.Get(); }).Result;
            var cameraIndex = 0;
            foreach (ManagementObject cameraDevice in collection)
            {
                Console.WriteLine($"Camera {cameraIndex}: {cameraDevice["Name"]}");
                cameraIndex++;
            }

            if (cameraIndex > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
        {
            //try
            //{
            //    url ??= CultureInfo.InstalledUICulture switch
            //    {
            //        { Name: var n } when n.StartsWith("fa") => // Iran
            //            "http://www.aparat.com",
            //        { Name: var n } when n.StartsWith("zh") => // China
            //            "http://www.baidu.com",
            //        _ =>
            //            "http://www.gstatic.com/generate_204",
            //    };

            //    var request = (HttpWebRequest)WebRequest.Create(url);
            //    request.KeepAlive = false;
            //    request.Timeout = timeoutMs;
            //    using (var response = (HttpWebResponse)request.GetResponse())
            //        return true;
            //}
            //catch
            //{
            //    return false;
            //}

            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}