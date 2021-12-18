using System;
using System.Management;
using System.Threading.Tasks;

namespace SystemCompatibilityConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Program p = new Program();
                p.ListAllCamerasAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Camera Detection Exception" + ex.Message + ex.StackTrace + "\n");
            }
        }

        public void ListAllCamerasAsync()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM win32_PNPEntity WHERE PnPClass='Camera'");
            var collection = Task.Run(() => { return searcher.Get(); }).Result;
            var cameraIndex = 0;
            foreach (ManagementObject cameraDevice in collection)
            {
                Console.WriteLine($"Camera {cameraIndex}: {cameraDevice["Name"]}");
                cameraIndex++;
            }
        }
    }
}