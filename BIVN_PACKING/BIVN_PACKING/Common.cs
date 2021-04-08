using ExcelDataReader;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BIVN_PACKING
{
    public class Common
    {
        public static string GetGUIID()
        {
            try
            {
                var assembly = typeof(Program).Assembly;
                var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
                return attribute.Value;
            }
            catch (Exception)
            {

                return "";
            }

        }

        public static string GetOSFriendlyName()
        {
            var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();
            return name != null ? name.ToString() : "Unknown";
        }

        public static string GetMACAddress()
        {
            ManagementObjectSearcher objMOS = new ManagementObjectSearcher("Select * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMOS.Get();
            string macAddress = String.Empty;
            foreach (ManagementObject objMO in objMOC)
            {
                object tempMacAddrObj = objMO["MacAddress"];

                if (tempMacAddrObj == null) //Skip objects without a MACAddress
                {
                    continue;
                }
                if (macAddress == String.Empty) // only return MAC Address from first card that has a MAC Address
                {
                    macAddress = tempMacAddrObj.ToString();
                }
                objMO.Dispose();
            }
            macAddress = macAddress.Replace(":", "");
            return macAddress;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var a = host.AddressList;
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static bool IsWindowsActivated()
        {
            ManagementScope scope = new ManagementScope(@"\\" + System.Environment.MachineName + @"\root\cimv2");
            scope.Connect();

            SelectQuery searchQuery = new SelectQuery("SELECT * FROM SoftwareLicensingProduct WHERE ApplicationID = '55c92734-d682-4d71-983e-d6ec3f16059f' and LicenseStatus = 1");
            ManagementObjectSearcher searcherObj = new ManagementObjectSearcher(scope, searchQuery);

            using (ManagementObjectCollection obj = searcherObj.Get())
            {
                return obj.Count > 0;
            }
        }

        public static string GetRunningVersion()
        {

            try
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }

        }

        public static void ImportModel(string path)
        {
            IExcelDataReader excelReader2007 = null;
            PVSService.PVSWebServiceSoapClient _pvs_service = new PVSService.PVSWebServiceSoapClient();
            try
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                excelReader2007 = ExcelReaderFactory.CreateOpenXmlReader(stream);
                DataSet result = excelReader2007.AsDataSet();

                foreach (DataTable table in result.Tables)
                {
                    for (int i = 1; i < table.Rows.Count; i++)
                    {
                        string model = table.Rows[i].ItemArray[3].ToString();
                        string serial = table.Rows[i].ItemArray[4].ToString();
                        if(_pvs_service.GetModelInfo(model) == null)
                        {
                            _pvs_service.SaveModelInfo(new PVSService.Base_ModelsEntity()
                            {
                                Product_Id = model,
                                Pcb = 1,
                                Customer = "CS000",
                                Content_Index = 0,
                                Content_Length = serial.Length,
                                Location = 1,
                                Des = "Brother"
                            }, "");
                        }
                       
                    }
                }

                excelReader2007.Close();
                Console.Read();

            }
            catch (Exception e)
            {
                if (excelReader2007 != null)
                    excelReader2007.Close();
            }

        }

    }

    public static class FileShare
    {
        public static string PASSWORD = "umcvn";
        public static string USER = @"VN\Scanner";
        public static string PATH = @"\\172.28.10.12";

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(NetResource netResource,
        string password, string username, int flags);

        [StructLayout(LayoutKind.Sequential)]
        public class NetResource
        {
            public ResourceScope Scope;
            public ResourceType ResourceType;
            public ResourceDisplaytype DisplayType;
            public int Usage;
            public string LocalName;
            public string RemoteName;
            public string Comment;
            public string Provider;
        }

        public enum ResourceScope : int
        {
            Connected = 1,
            GlobalNetwork,
            Remembered,
            Recent,
            Context
        };

        public enum ResourceType : int
        {
            Any = 0,
            Disk = 1,
            Print = 2,
            Reserved = 8,
        }

        public enum ResourceDisplaytype : int
        {
            Generic = 0x0,
            Domain = 0x01,
            Server = 0x02,
            Share = 0x03,
            File = 0x04,
            Group = 0x05,
            Network = 0x06,
            Root = 0x07,
            Shareadmin = 0x08,
            Directory = 0x09,
            Tree = 0x0a,
            Ndscontainer = 0x0b
        }
        public static bool Connect(string networkName, NetworkCredential credentials)
        {
            try
            {
                var netResource = new NetResource
                {
                    Scope = ResourceScope.GlobalNetwork,
                    ResourceType = ResourceType.Disk,
                    DisplayType = ResourceDisplaytype.Share,
                    RemoteName = networkName
                };

                var userName = string.IsNullOrEmpty(credentials.Domain)
                    ? credentials.UserName
                    : string.Format(@"{0}\{1}", credentials.Domain, credentials.UserName);

                var result = WNetAddConnection2(
                    netResource,
                    credentials.Password,
                    userName,
                    0);

                return result == 0;
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                return false;
            }
        }
    }
}
