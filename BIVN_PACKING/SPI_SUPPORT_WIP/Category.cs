using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI_SUPPORT_WIP
{
    public class Category
    {
        public string Logextension { get; set; }
        public string PathlogICT { get; set; }
        public string OutputLog { get; set; }
        public string CurrentStation { get; set; }
        public string Process { get; set; }
        public int? TimeSleep { get; set; }
        public int? Location { get; set; }
        public int? PCPSheet { get; set; }
        public string Model { get; set; }
        public int? LengthBarcode { get; set; }
        public int? Index { get; set; }
        public int? To { get; set; }
        public string PCName { get; set; }
        public string COMPort { get; set; }
        public int? StationIndex { get; set; }
        public int? StationTo { get; set; }
        public bool EnableCOM { get; set; }
        public string DataMode { get; set; }
        public string DataModelStop { get; set; }
        public string OldStation { get; set; }
        public bool SPECIAL { get; set; }
    }
}
