using System;
using System.Management;

namespace HY_TWEAK.Core
{
    public class SystemInfo
    {
        public string CPUName { get; set; }
        public string GPUName { get; set; }
        public string GPUVendor { get; set; }
        public int TotalRAM { get; set; }
        public string OSInfo { get; set; }
    }

    public class HardwareDetector
    {
        public SystemInfo GetSystemInfo()
        {
            var info = new SystemInfo
            {
                CPUName = GetCPUInfo(),
                GPUName = GetGPUInfo(),
                GPUVendor = DetectGPUVendor(),
                TotalRAM = GetTotalRAM(),
                OSInfo = GetOSInfo()
            };

            return info;
        }

        private string GetCPUInfo()
        {
            try
            {
                var query = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                foreach (var obj in query.Get())
                {
                    return obj["Name"].ToString();
                }
            }
            catch { }
            return "Unknown CPU";
        }

        private string GetGPUInfo()
        {
            try
            {
                var query = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                foreach (var obj in query.Get())
                {
                    return obj["Name"].ToString();
                }
            }
            catch { }
            return "Unknown GPU";
        }

        private string DetectGPUVendor()
        {
            try
            {
                var gpuInfo = GetGPUInfo().ToLower();
                if (gpuInfo.Contains("nvidia")) return "NVIDIA";
                if (gpuInfo.Contains("amd")) return "AMD";
                if (gpuInfo.Contains("intel")) return "Intel";
                return "Unknown";
            }
            catch { }
            return "Unknown";
        }

        private int GetTotalRAM()
        {
            try
            {
                var query = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
                foreach (var obj in query.Get())
                {
                    return (int)(Convert.ToInt64(obj["TotalPhysicalMemory"]) / (1024 * 1024 * 1024));
                }
            }
            catch { }
            return 0;
        }

        private string GetOSInfo()
        {
            try
            {
                var query = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                foreach (var obj in query.Get())
                {
                    return obj["Caption"].ToString();
                }
            }
            catch { }
            return Environment.OSVersion.ToString();
        }
    }
}