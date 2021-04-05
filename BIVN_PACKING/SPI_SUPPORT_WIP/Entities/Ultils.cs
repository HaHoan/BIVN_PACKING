using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPI_SUPPORT_WIP.Entities
{
    public class Ultils
    {
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
        public static GetData ReadFile(string file)
        {
            var list = new List<GetData>();
            var data = new GetData();
            string path = file.ToString();
            var pathsplit = path.Split('.');
            var getsplit = pathsplit[0];
            string lengpath = getsplit.Substring(getsplit.Length - 2).Trim();
            string[] info = File.ReadAllLines(file);
            List<string> info_2 = File.ReadAllLines(file).Skip(10).ToList();
            foreach (string line in info_2)
            {
                var col = line.Split(',');
                if (col[1] != "")
                {
                    int blockno = int.Parse(col[1].Trim());
                    if (blockno == 0)
                    {
                        data.enable_0 = true;
                    }
                    if (blockno == 1)
                    {
                        data.enable_1 = true;
                    }
                    if (blockno == 2)
                    {
                        data.enable_2 = true;
                    }
                    if (blockno == 3)
                    {
                        data.enable_3 = true;
                    }
                    if (blockno == 4)
                    {
                        data.enable_4 = true;
                    }
                    if (blockno == 5)
                    {
                        data.enable_5 = true;
                    }
                    if (blockno == 6)
                    {
                        data.enable_6 = true;
                    }
                    if (blockno == 7)
                    {
                        data.enable_7 = true;
                    }
                    if (blockno == 8)
                    {
                        data.enable_8 = true;
                    }
                    if (blockno == 9)
                    {
                        data.enable_9 = true;
                    }
                    if (blockno == 10)
                    {
                        data.enable_10 = true;
                    }
                    if (blockno == 11)
                    {
                        data.enable_11 = true;
                    }
                    if (blockno == 12)
                    {
                        data.enable_12 = true;
                    }
                    if (blockno == 13)
                    {
                        data.enable_13 = true;
                    }
                    if (blockno == 14)
                    {
                        data.enable_14 = true;
                    }
                    if (blockno == 15)
                    {
                        data.enable_15 = true;
                    }
                    if (blockno == 16)
                    {
                        data.enable_16 = true;
                    }
                    if (blockno == 17)
                    {
                        data.enable_17 = true;
                    }
                    if (blockno == 18)
                    {
                        data.enable_18 = true;
                    }
                    if (blockno == 19)
                    {
                        data.enable_19 = true;
                    }
                    if (blockno == 20)
                    {
                        data.enable_20 = true;
                    }
                    if (blockno == 21)
                    {
                        data.enable_21 = true;
                    }
                    if (blockno == 22)
                    {
                        data.enable_22 = true;
                    }
                }
            }

            var GETSerial = info[5].Split(',');

            //var codesplit = GETSerial.Split(',');
            data.serial = GETSerial[1].Trim();
            //var cutse = serial.Substring(0,28);
            //var alcut = cutse.Substring(5, cutse.Length - 1);
            if (lengpath != "NG")
            {
                data.state = "P";
            }
            else
            {
                data.state = "F";
            }
            return data;
        }
        public static WrireName WriteFileName(string file)
        {
            var data = new WrireName();
            string path = file.ToString();
            //string filename = file.name
            string[] info = File.ReadAllLines(file);
            data.filename = info[3].Trim();
            data.GetNameSerial = info[5].Trim();
            return data;
        }

        public static DateTime GetDateTime()
        {
            string connection = @"Data Source=172.28.10.8;Initial Catalog=UMC_MESDB_TEST;Persist Security Info=True;User ID=sa;Password=$umcevn123;";
            string sql = "SELECT GETDATE() as CurrentTime";
            try
            {
                SqlConnection sqlConnect = new SqlConnection(connection);

                sqlConnect.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlConnect))
                {
                    object obj = cmd.ExecuteScalar();
                    cmd.Prepare();
                    return obj != null ? DateTime.Parse(obj.ToString()) : DateTime.MinValue;
                }
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
    }
}
