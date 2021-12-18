using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCompatability.CompatabilityClass
{
    public class Compatability
    {
        //Om Ganeshaye Namah

        private static async Task ListAllCamerasAsync()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM win32_PNPEntity WHERE PnPClass='Camera'");
            var collection = await Task.Run(() => { return searcher.Get(); });
            var cameraIndex = 0;
            foreach (ManagementObject cameraDevice in collection)
            {
                Console.WriteLine($"Camera {cameraIndex}: {cameraDevice["Name"]}");
                cameraIndex++;
            }
        }
    }
}
