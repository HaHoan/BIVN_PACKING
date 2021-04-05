using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIVN_PACKING.Business
{
    public class SearchView
    {
        public static void ActiveProcess(string title)
        {
            Process[] processes = Process.GetProcesses();
            int windowHandle = 0;
            foreach (Process p in processes)
            {
                if (p.MainWindowTitle.Contains(title))
                {
                    windowHandle = (int)p.MainWindowHandle;
                    break;
                }

            }
            NativeWin32.SetForegroundWindow(windowHandle);
        }
        public string process { set; get; }
        public int timesleep { set; get; }
        public string model { set; get; }
    }
}
