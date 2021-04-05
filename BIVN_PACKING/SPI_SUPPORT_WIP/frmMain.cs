using SPI_SUPPORT_WIP.Database;
using SPI_SUPPORT_WIP.Entities;
using SPI_SUPPORT_WIP.ServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPI_SUPPORT_WIP
{
    public partial class frmMain : Form
    {
        private PVSWebServiceSoapClient pvsWebService = new PVSWebServiceSoapClient();
        SOFTWAREEntities _db = new SOFTWAREEntities();
        UMC_MESDB_TESTEntities _dbService = new UMC_MESDB_TESTEntities();
        //PVSWebServiceSoapClient pvsWebService = new PVSWebServiceSoapClient();
        //List<WORK_ORDER_ITEMSEntity> listnull = new List<WORK_ORDER_ITEMSEntity>();
        //List<WORK_ORDER_ITEMSEntity> work_order_items = new List<WORK_ORDER_ITEMSEntity>();
        FileSystemWatcher fileWatcher = new FileSystemWatcher();
        Category InfoData = new Category();
        List<Results> listnull = new List<Results>();
        int pass = 0, ng = 0, total = 0;
        string nummmm = "";
        //bool enableCOM = false;
        string fullpath = "";
        string name = "";
        string status = "";
        public frmMain()
        {
            InitializeComponent();
            DataConnect();
            lblVersion.Text = Ultils.GetRunningVersion();
        }
        public void DataConnect()
        {
            InfoData.PathlogICT = Properties.Settings.Default.PathlogICT;
            InfoData.OutputLog = Properties.Settings.Default.OutputLog;
            InfoData.CurrentStation = Properties.Settings.Default.Station;
            InfoData.TimeSleep = Properties.Settings.Default.TimeSleep;
            InfoData.Process = Properties.Settings.Default.Process;
            InfoData.Model = Properties.Settings.Default.Model;
            InfoData.LengthBarcode = Properties.Settings.Default.LengBarcode;
            InfoData.Location = Properties.Settings.Default.Location;
            InfoData.Index = Properties.Settings.Default.Index;
            InfoData.To = Properties.Settings.Default.To;
            InfoData.PCPSheet = Properties.Settings.Default.PCPSheet;
            InfoData.PCName = Properties.Settings.Default.PCName;
            InfoData.StationIndex = Properties.Settings.Default.StationIndex;
            InfoData.StationTo = Properties.Settings.Default.StationTo;
            InfoData.COMPort = Properties.Settings.Default.COMPort;
            InfoData.EnableCOM = Properties.Settings.Default.EnableCOM;
            InfoData.DataMode = Properties.Settings.Default.DataModel;
            InfoData.DataModelStop = Properties.Settings.Default.DataModelStop;
            InfoData.OldStation = Properties.Settings.Default.OldStation;
            //InfoData.SPECIAL = Properties.Settings.Default.SPECIAL;
        }
        void move_file_OK()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyMMddHHmmss");
                string[] cut = name.Split('\\');
                var getcut = cut.LastOrDefault();
                string ipServer = "10.28.5.200";
                //ipServer = "172.28.10.4";
                string path = @"\\10.28.5.200\line s43\SPI";
                // path = @"D:\";
                string checkpath = path + "\\" + getcut;
                Ping x = new Ping();
                PingReply reply = x.Send(IPAddress.Parse(ipServer));
                if (reply.Status == IPStatus.Success)
                {
                    lblthongbao.Text = "";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (!File.Exists(checkpath))
                    {
                        try
                        {
                            File.Move(fullpath, checkpath);
                        }
                        catch
                        {
                            FAIL("FAIL", "Lỗi trong quá trình di chuyển Log");
                            return;
                        }
                    }
                    else
                    {
                        var cur = getcut.Split('.');
                        string oldfile = path + "\\" + cur[0] + "_" + datetime + "." + cur[1];
                        try
                        {
                            File.Move(fullpath, oldfile);
                        }
                        catch
                        {
                            FAIL("FAIL", "Lỗi trong quá trình di chuyển Log");
                            return;
                        }
                    }
                }
                else
                {
                    lblthongbao.Text = "Could not connect to 10.28.5.200";
                }
            }
            catch
            {
            }
        }
        public void SendValueCom(string portName, string data)
        {
            SerialPort com = new SerialPort(portName);
            if (!com.IsOpen) com.Open();
            com.Write(data);
            com.Close();
        }

        public void GetEverywhere()
        {
           // InfoData.PathlogICT = @"C:\Focus\Data";
            if (InfoData.PathlogICT != "")
            {
                DirectoryInfo di = new DirectoryInfo(InfoData.PathlogICT);
                List<FileInfo> files = di.GetFiles("*.CSV").ToList();
                string fullpaths = "";
                string paths = "";
                if (files.Count != 0)
                {
                    try
                    {
                        paths = files.FirstOrDefault().ToString();
                        fullpaths = di + "\\" + paths;
                        fullpath = fullpaths;
                        name = paths;
                        List<Results> datagrid = new List<Results>();
                        this.BeginInvoke(new Action(() => { lblPathLog.Text = fullpaths; }));
                        System.Threading.Thread.Sleep(InfoData.TimeSleep.GetValueOrDefault());
                        var data = Ultils.ReadFile(fullpaths);
                        string datetime = DateTime.Now.ToString("yyMMddHHmmss");
                        var datachange = Ultils.WriteFileName(fullpaths);
                        //
                        if (data.serial.Length != InfoData.LengthBarcode)
                        {
                            FAIL("FAIL", $@"Sai Serial. File [{paths} trong [{InfoData.PathlogICT}]]\r\nsẽ được chuyển vào \\10.28.5.200\line s43\SPI NG BARCODE");
                            try
                            {
                                System.Threading.Thread.Sleep(InfoData.TimeSleep.GetValueOrDefault());
                                //string fullpath = fullpaths;
                                string namefile = paths;
                                string[] cut = namefile.Split('\\');
                                var getcut = cut.LastOrDefault();
                                string path = @"\\10.28.5.200\line s43\SPI NG BARCODE";
                                string checkpath = path + "\\" + getcut;
                                Ping x = new Ping();
                                PingReply reply = x.Send(IPAddress.Parse("10.28.5.200"));
                                if (reply.Status == IPStatus.Success)
                                {
                                    lblthongbao.Text = "";
                                    //lblthongbao.Text = "";
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    if (!File.Exists(checkpath))
                                    {
                                        try
                                        {
                                            File.Move(fullpaths, checkpath);
                                        }
                                        catch
                                        {
                                            FAIL("FAIL", "Lỗi trong quá trình di chuyển Log");
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        var cur = getcut.Split('.');
                                        string oldfile = path + "\\" + cur[0] + "_" + datetime + "." + cur[1];
                                        try
                                        {
                                            File.Move(fullpaths, oldfile);
                                        }
                                        catch
                                        {
                                            FAIL("FAIL", "Lỗi trong quá trình di chuyển Log");
                                            return;
                                        }
                                        //File.Delete(fullpath);
                                    }
                                }
                                else
                                {
                                    lblthongbao.Text = "Could not connect to 10.28.5.200";
                                }
                            }
                            catch
                            {
                            }
                            return;
                        }
                        var serials = data.serial;
                        string theFirst = serials.Substring(0, InfoData.Index.GetValueOrDefault());
                        //lblFirst.Text = theFirst;
                        //int delete = InfoData.LengthBarcode - InfoData.ToBefore;
                        string lastSerial = serials.Substring(InfoData.Index.GetValueOrDefault(), InfoData.To.GetValueOrDefault());
                        //lblNumber.Text = lastSerial;
                        string endSerial = "";
                        string resultSerial = "";
                        int auto = 0;
                        for (int i = 1; i <= InfoData.PCPSheet; i++)
                        {
                            auto = InfoData.Location - i == 1 ? auto = i : auto;
                        }
                        int isCheck = int.Parse(lastSerial) - auto;
                        if ((isCheck - 1) % InfoData.PCPSheet != 0)
                        {
                            FAIL("FAIL", $"Serial [{data.serial}]\r\nNhầm số Serial đầu!.\nCần chạy lại bảng mạch");
                            try
                            {
                                System.Threading.Thread.Sleep(InfoData.TimeSleep.GetValueOrDefault());
                                //string fullpath = fullpaths;
                                string namefile = paths;
                                string[] cut = namefile.Split('\\');
                                var getcut = cut.LastOrDefault();
                                //\\172.28.10.12\Share\18 IT\U33056(Cuongnv)\Backup Data\AOI
                                //\\10.28.5.200\line s43\AOI
                                string path = @"\\10.28.5.200\line s43\SPI NG BARCODE";
                                string checkpath = path + "\\" + getcut;
                                Ping x = new Ping();
                                PingReply reply = x.Send(IPAddress.Parse("10.28.5.200"));
                                if (reply.Status == IPStatus.Success)
                                {
                                    lblthongbao.Text = "";
                                    //lblthongbao.Text = "";
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    if (!File.Exists(checkpath))
                                    {
                                        try
                                        {
                                            File.Move(fullpaths, checkpath);
                                        }
                                        catch
                                        {
                                            FAIL("FAIL", "Lỗi trong quá trình di chuyển Log");
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        var cur = getcut.Split('.');
                                        string oldfile = path + "\\" + cur[0] + "_" + datetime + "." + cur[1];
                                        try
                                        {
                                            File.Move(fullpaths, oldfile);
                                        }
                                        catch
                                        {
                                            FAIL("FAIL", "Lỗi trong quá trình di chuyển Log");
                                            return;
                                        }
                                        //File.Delete(fullpath);
                                    }
                                }
                                else
                                {
                                    lblthongbao.Text = "Could not connect to 10.28.5.200";
                                }
                            }
                            catch
                            {
                            }
                            return;
                        }
                        if (data.state != "F")
                        {
                            if (InfoData.EnableCOM == true)
                            {
                                if (InfoData.DataMode != "")
                                {
                                    try
                                    {
                                        SendValueCom(InfoData.COMPort, InfoData.DataMode);
                                        Console.Write(InfoData.DataMode);
                                    }
                                    catch (Exception ex)
                                    {
                                        FAIL("FAIL", $"Mât kêt nối cổng COM {ex.Message}");
                                        return;
                                    }

                                }
                            }
                            for (int i = 1; i <= InfoData.PCPSheet; i++)
                            {
                                PASS("PASS", $"Serial is [{data.serial}] OK");
                                data.state = "P";
                                pass = pass + 1;
                            }
                        }
                        else
                        {
                            if (InfoData.EnableCOM == true)
                            {
                                if (InfoData.DataModelStop != "")
                                {
                                    try
                                    {
                                        SendValueCom(InfoData.COMPort, InfoData.DataModelStop);
                                        Console.Write(InfoData.DataModelStop);
                                    }
                                    catch (Exception ex)
                                    {
                                        FAIL("FAIL", $"Mât kêt nối cổng COM {ex.Message}");
                                        return;
                                    }
                                }
                            }
                            for (int i = 1; i <= InfoData.PCPSheet; i++)
                            {
                                FAIL("FAIL", $"Serial is [{data.serial}] NG");
                                data.state = "F";
                                ng = ng + 1;
                            }
                        }
                        total = pass + ng;

                        //
                        for (int i = 1; i <= InfoData.PCPSheet; i++)
                        {
                            string changed = "";
                            int delserial = int.Parse(lastSerial) - auto;
                            int deleserial = delserial + i - 1;
                            string delString = deleserial.ToString();
                            if (InfoData.To == 4)
                            {
                                if (delString.Length == 1)
                                {
                                    changed = "000" + delString;
                                }
                                if (delString.Length == 2)
                                {
                                    changed = "00" + delString;
                                }
                                if (delString.Length == 3)
                                {
                                    changed = "0" + delString;
                                }
                                if (delString.Length == 4)
                                {
                                    changed = delString;
                                }
                            }
                            else if (InfoData.To == 5)
                            {
                                if (delString.Length == 1)
                                {
                                    changed = "0000" + delString;
                                }
                                if (delString.Length == 2)
                                {
                                    changed = "000" + delString;
                                }
                                if (delString.Length == 3)
                                {
                                    changed = "00" + delString;
                                }
                                if (delString.Length == 4)
                                {
                                    changed = "0" + delString;
                                }
                                if (delString.Length == 5)
                                {
                                    changed = delString;
                                }
                            }
                            int getEndSerial = InfoData.LengthBarcode.GetValueOrDefault() - (InfoData.Index.GetValueOrDefault() + InfoData.To.GetValueOrDefault());
                            if (getEndSerial != 0)
                            {
                                endSerial = serials.Substring(serials.Length - getEndSerial);
                                resultSerial = theFirst + changed + endSerial;
                            }
                            else
                            {
                                resultSerial = theFirst + changed;
                            }
                            var checkSerial = pvsWebService.GetWorkOrderItem(resultSerial, InfoData.OldStation);
                            if (checkSerial == null)
                            {

                            }
                            string stationChanged = "";
                            if (datachange.filename.Contains("SMTA"))
                            {
                                string theString = InfoData.CurrentStation;
                                var aStringBuilder = new StringBuilder(theString);
                                aStringBuilder.Remove(InfoData.StationIndex.GetValueOrDefault(), InfoData.StationTo.GetValueOrDefault());
                                aStringBuilder.Insert(InfoData.StationIndex.GetValueOrDefault(), "SMTA");
                                stationChanged = aStringBuilder.ToString();

                            }
                            else
                            {
                                stationChanged = InfoData.CurrentStation;
                            }


                            string model = InfoData.Model.Trim();
                            var viewdata = new Results() { model = model, serial = resultSerial, date = DateTime.Now, state = data.state, station = stationChanged };
                            datagrid.Add(viewdata);

                            if (!Directory.Exists(InfoData.OutputLog))
                            {
                                Directory.CreateDirectory(InfoData.OutputLog);
                            }
                            var filepathg = InfoData.OutputLog + "\\" + $"{datetime}_{model}_{resultSerial}.txt";
                            var format = string.Format("{0}|{1}|{2}|{3}|{4}", model, resultSerial, datetime, data.state, stationChanged);
                            TextWriter tw = new StreamWriter(filepathg);
                            tw.WriteLine(format);
                            tw.Close();
                            tw.Dispose();
                        }
                        move_file_OK();
                        this.BeginInvoke(new Action(() => { lblPASS.Text = pass.ToString(); lblNG.Text = ng.ToString(); lblTOTAL.Text = total.ToString(); dataGridView1.DataSource = datagrid; }));
                    }
                    catch (Exception)
                    {

                    }

                }
            }
        }
        public void IS_SPECIAL()
        {
           // InfoData.PathlogICT = @"C:\Focus\Data";
            if (InfoData.PathlogICT != "")
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(InfoData.PathlogICT);
                    List<FileInfo> files = di.GetFiles("*.CSV").ToList();
                    if (files.Count != 0)
                    {
                        System.Threading.Thread.Sleep(InfoData.TimeSleep.GetValueOrDefault());
                        int count = 0;
                        name = files.FirstOrDefault().ToString();
                        fullpath = di.ToString() + "\\" + name;
                        List<string> listinfile = File.ReadAllLines(fullpath).ToList();
                        var serial = listinfile[5].Split(',').Skip(1).ToList();
                        foreach (var item in serial)
                        {
                            if (item.Trim().Length == InfoData.LengthBarcode)
                            {
                                count = count + 1;
                            }
                        }

                        var split_name = name.Split('.').FirstOrDefault().Trim();
                        status = split_name.Substring(split_name.Trim().Length - 2) != "NG" ? "P" : "F";
                        if (status != "F")
                        {
                            if (InfoData.EnableCOM == true)
                            {
                                if (InfoData.DataMode != "")
                                {
                                    try
                                    {
                                        SendValueCom(InfoData.COMPort, InfoData.DataMode);
                                        Console.Write(InfoData.DataMode);
                                    }
                                    catch (Exception ex)
                                    {
                                        FAIL("FAIL", $"Mât kêt nối cổng COM {ex.Message}");
                                        return;
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (InfoData.EnableCOM == true)
                            {
                                if (InfoData.DataModelStop != "")
                                {
                                    try
                                    {
                                        SendValueCom(InfoData.COMPort, InfoData.DataModelStop);
                                        Console.Write(InfoData.DataModelStop);
                                    }
                                    catch (Exception ex)
                                    {
                                        FAIL("FAIL", $"Mât kêt nối cổng COM {ex.Message}");
                                        return;
                                    }
                                }
                            }
                        }
                        List<Results> datagrid = new List<Results>();
                        foreach (var item in serial)
                        {
                            if (item.Trim() != "")
                            {
                                string datetime = DateTime.Now.ToString("yyMMddHHmmss");
                                //if (InfoData.OldStation != "")
                                //{
                                //    if (pvsWebService.GetWorkOrderItem(item.Trim(), InfoData.OldStation) == null)
                                //    {
                                //        FAIL("FAIL", $"Serial [{item}] chưa qua[{InfoData.OldStation}]\r\nhoặc đã qua[{InfoData.CurrentStation}]");
                                //        if (InfoData.EnableCOM == true)
                                //        {
                                //            try
                                //            {
                                //                SendValueCom(InfoData.COMPort, InfoData.DataModelStop);
                                //                Console.Write(InfoData.DataModelStop);
                                //            }
                                //            catch (Exception ex)
                                //            {
                                //                FAIL("FAIL", $"Mât kêt nối cổng COM {ex.Message}");
                                //                return;
                                //            }
                                //        }
                                //        return;
                                //    }
                                //}
                                if (status == "P")
                                {
                                    PASS("PASS", $"Serial {item} OK");
                                    pass = pass + 1;
                                }
                                else
                                {
                                    FAIL("FAIL", $"Serial {item} NG");
                                    ng = ng + 1;
                                }
                                var viewdata = new Results() { model = InfoData.Model, serial = item, date = DateTime.Now, state = status, station = InfoData.CurrentStation };
                                datagrid.Add(viewdata);

                                if (!Directory.Exists(InfoData.OutputLog))
                                {
                                    Directory.CreateDirectory(InfoData.OutputLog);
                                }
                                var filepathg = InfoData.OutputLog + "\\" + $"{datetime}_{InfoData.Model}_{item}.txt";
                                var format = string.Format("{0}|{1}|{2}|{3}|{4}", InfoData.Model, item, datetime, status, InfoData.CurrentStation);
                                TextWriter tw = new StreamWriter(filepathg);
                                tw.WriteLine(format);
                                tw.Close();
                                tw.Dispose();
                            }
                        }
                        move_file_OK();
                        total = pass + ng;
                        this.BeginInvoke(new Action(() => { lblPASS.Text = pass.ToString(); lblNG.Text = ng.ToString(); lblTOTAL.Text = total.ToString(); dataGridView1.DataSource = listnull; dataGridView1.DataSource = datagrid; }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (btnStart.Enabled == false)
            {
                string time = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss tt");
                lblDateTime.Text = time;
                try
                {
                   // InfoData.PathlogICT = @"C:\Focus\Data";//Xóa
                    DirectoryInfo di = new DirectoryInfo(InfoData.PathlogICT);
                    List<FileInfo> files = di.GetFiles("*.CSV").ToList();
                    string fullpaths = "";
                    string paths = "";
                    if (files.Count != 0)
                    {
                        paths = files.FirstOrDefault().ToString();
                        fullpaths = di + "\\" + paths;
                        List<string> info_2 = File.ReadAllLines(fullpaths).ToList();
                        string serial = info_2[5].Trim();
                        var cut = serial.Split(',');
                        string boardNo = cut[1].Trim();
                        var orderItem = pvsWebService.GetWorkOrderItemByBoardNo(boardNo);
                        var order = pvsWebService.GetWorkOrdersByOrderNo(orderItem.ORDER_NO);
                        var info = _db.ALL_SETTINGS_THE_CUSTOMERS.FirstOrDefault(r => r.MODEL == order.PRODUCT_ID && r.PC_NAME == InfoData.PCName);
                        if (info != null)
                        {
                            InfoData.PathlogICT = info.PATH_LOG;
                            InfoData.OutputLog = info.OUTPUT_LOG;
                            InfoData.CurrentStation = info.STATION;
                            InfoData.LengthBarcode = info.LENGTH_SERIAL;
                            InfoData.Location = info.LOCATION;
                            InfoData.To = info.TO;
                            InfoData.Index = info.INDEX;
                            InfoData.PCPSheet = info.PCP_SHEET;
                            InfoData.StationIndex = info.STATION_INDEX;
                            InfoData.StationTo = info.STATION_TO;
                            InfoData.Model = info.MODEL;
                            InfoData.OldStation = info.OLD_STATION;
                            if (info.IS_SPECIAL == true)
                            {
                                IS_SPECIAL();
                            }
                            else
                            {
                                GetEverywhere();
                            }
                           // GetEverywhere();
                        }
                        else
                        {
                            if (InfoData.EnableCOM == true)
                            {
                                //var cuts = paths.Split('_');
                                //if (cuts[cuts.Count() - 1].ToUpper().Contains("OK"))
                                //{
                                SendValueCom(InfoData.COMPort, InfoData.DataMode);
                                PASS("PASS", $"[{paths}]\r\nModel chưa có trong DATA, chỉ gửi tín hiệu COM OK");
                                //}
                                //else
                                //{
                                //    SendValueCom(InfoData.COMPort, InfoData.DataModelStop);
                                //    FAIL("FAIL", $"[{paths}]\r\nModel chưa có trong DATA, chỉ gửi tín hiệu COM NG");
                                //}
                            }
                            try
                            {
                                if (!File.Exists(InfoData.PathlogICT))
                                {
                                    string output = @"C:\Backup data";
                                    if (!Directory.Exists(output))
                                    {
                                        Directory.CreateDirectory(output);
                                    }
                                    List<string> Files = Directory.GetFiles(InfoData.PathlogICT, "*").ToList();
                                    if (Files.Count != 0)
                                    {
                                        WAIT("Moving", $"Vui lòng đợi di chuyển file hoàn tất\r\n hoặc vào ổ [{InfoData.PathlogICT}] xóa toàn bộ file");
                                        foreach (var item in Files)
                                        {
                                            if (!File.Exists(item))
                                            {
                                                try
                                                {
                                                    var cut1 = item.Split('\\');
                                                    File.Move(item, output + "\\" + cut1.LastOrDefault());
                                                }
                                                catch (Exception)
                                                {
                                                    //lblMessage.Text = ex.Message;
                                                }
                                            }
                                            else
                                            {
                                                string datetime = DateTime.Now.ToString("yyMMddHHmmss");
                                                var cut2 = item.Split('\\');
                                                var cur = cut2.LastOrDefault().Split('.');
                                                string oldfile = output + "\\" + cur[0] + "_" + datetime + "." + cur[1];
                                                try
                                                {
                                                    File.Move(item, oldfile);
                                                }
                                                catch (Exception)
                                                {
                                                    //lblMessage.Text = ex.Message;
                                                }
                                                //File.Delete(fullpath);
                                            }
                                        }
                                        //WAIT("Wait", "Waiting for file logs...");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        //var move = _db.ALL_SETTINGS_THE_CUSTOMERS.FirstOrDefault(x => x.LENGTH_SERIAL == boardNo.Length);
                        //var setData = _db.ALL_SETTINGS_THE_CUSTOMERS.FirstOrDefault(x => x.MODEL.Contains(boardNo.Substring(0, 8)) && x.PC_NAME == InfoData.PCName);
                        //var setDataTwo = _db.ALL_SETTINGS_THE_CUSTOMERS.FirstOrDefault(x => x.MODEL.Contains(boardNo.Substring(1, 10)) && x.PC_NAME == InfoData.PCName);
                        //var setDataThree = _db.ALL_SETTINGS_THE_CUSTOMERS.FirstOrDefault(x => x.MODEL.Contains(boardNo.Substring(2, 10)) && x.PC_NAME == InfoData.PCName);
                        //if (setData != null && move != null || setDataTwo != null && move != null || setDataThree != null && move != null)
                        //{
                        //    if (setData != null)
                        //    {
                        //        InfoData.PathlogICT = setData.PATH_LOG;
                        //        InfoData.OutputLog = setData.OUTPUT_LOG;
                        //        InfoData.CurrentStation = setData.STATION;
                        //        //InfoData.TimeSleep = setData.TIME_SLEEP;
                        //        InfoData.LengthBarcode = setData.LENGTH_SERIAL;
                        //        InfoData.Location = setData.LOCATION;
                        //        InfoData.To = setData.TO;
                        //        InfoData.Index = setData.INDEX;
                        //        InfoData.PCPSheet = setData.PCP_SHEET;

                        //        InfoData.StationIndex = setData.STATION_INDEX;
                        //        InfoData.StationTo = setData.STATION_TO;
                        //        InfoData.Model = setData.MODEL;
                        //        InfoData.OldStation = setData.OLD_STATION;
                        //        if (setData.IS_SPECIAL == true)
                        //        {
                        //            IS_SPECIAL();
                        //        }
                        //        else
                        //        {
                        //            GetEverywhere();
                        //        }
                        //    }
                        //    if (setDataTwo != null)
                        //    {
                        //        InfoData.PathlogICT = setDataTwo.PATH_LOG;
                        //        InfoData.OutputLog = setDataTwo.OUTPUT_LOG;
                        //        InfoData.CurrentStation = setDataTwo.STATION;
                        //        //InfoData.TimeSleep = setDataTwo.TIME_SLEEP;
                        //        InfoData.LengthBarcode = setDataTwo.LENGTH_SERIAL;
                        //        InfoData.Location = setDataTwo.LOCATION;
                        //        InfoData.To = setDataTwo.TO;
                        //        InfoData.Index = setDataTwo.INDEX;
                        //        InfoData.PCPSheet = setDataTwo.PCP_SHEET;

                        //        InfoData.StationIndex = setDataTwo.STATION_INDEX;
                        //        InfoData.StationTo = setDataTwo.STATION_TO;
                        //        InfoData.Model = setDataTwo.MODEL;
                        //        InfoData.OldStation = setDataTwo.OLD_STATION;
                        //        if (setDataTwo.IS_SPECIAL == true)
                        //        {
                        //            IS_SPECIAL();
                        //        }
                        //        else
                        //        {
                        //            GetEverywhere();
                        //        }
                        //    }
                        //    if (setDataThree != null)
                        //    {
                        //        InfoData.PathlogICT = setDataThree.PATH_LOG;
                        //        InfoData.OutputLog = setDataThree.OUTPUT_LOG;
                        //        InfoData.CurrentStation = setDataThree.STATION;
                        //        //InfoData.TimeSleep = setDataThree.TIME_SLEEP;
                        //        InfoData.LengthBarcode = setDataThree.LENGTH_SERIAL;
                        //        InfoData.Location = setDataThree.LOCATION;
                        //        InfoData.To = setDataThree.TO;
                        //        InfoData.Index = setDataThree.INDEX;
                        //        InfoData.PCPSheet = setDataThree.PCP_SHEET;

                        //        InfoData.StationIndex = setDataThree.STATION_INDEX;
                        //        InfoData.StationTo = setDataThree.STATION_TO;
                        //        InfoData.Model = setDataThree.MODEL;
                        //        InfoData.OldStation = setDataThree.OLD_STATION;
                        //        if (setDataThree.IS_SPECIAL == true)
                        //        {
                        //            IS_SPECIAL();
                        //        }
                        //        else
                        //        {
                        //            GetEverywhere();
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    if (InfoData.EnableCOM == true)
                        //    {
                        //        //var cuts = paths.Split('_');
                        //        //if (cuts[cuts.Count() - 1].ToUpper().Contains("OK"))
                        //        //{
                        //        SendValueCom(InfoData.COMPort, InfoData.DataMode);
                        //        PASS("PASS", $"[{paths}]\r\nModel chưa có trong DATA, chỉ gửi tín hiệu COM OK");
                        //        //}
                        //        //else
                        //        //{
                        //        //    SendValueCom(InfoData.COMPort, InfoData.DataModelStop);
                        //        //    FAIL("FAIL", $"[{paths}]\r\nModel chưa có trong DATA, chỉ gửi tín hiệu COM NG");
                        //        //}
                        //    }
                        //    try
                        //    {
                        //        if (!File.Exists(InfoData.PathlogICT))
                        //        {
                        //            string output = @"C:\Backup data";
                        //            if (!Directory.Exists(output))
                        //            {
                        //                Directory.CreateDirectory(output);
                        //            }
                        //            List<string> Files = Directory.GetFiles(InfoData.PathlogICT, "*").ToList();
                        //            if (Files.Count != 0)
                        //            {
                        //                WAIT("Moving", $"Vui lòng đợi di chuyển file hoàn tất\r\n hoặc vào ổ [{InfoData.PathlogICT}] xóa toàn bộ file");
                        //                foreach (var item in Files)
                        //                {
                        //                    if (!File.Exists(item))
                        //                    {
                        //                        try
                        //                        {
                        //                            var cut1 = item.Split('\\');
                        //                            File.Move(item, output + "\\" + cut1.LastOrDefault());
                        //                        }
                        //                        catch (Exception)
                        //                        {
                        //                            //lblMessage.Text = ex.Message;
                        //                        }
                        //                    }
                        //                    else
                        //                    {
                        //                        string datetime = DateTime.Now.ToString("yyMMddHHmmss");
                        //                        var cut2 = item.Split('\\');
                        //                        var cur = cut2.LastOrDefault().Split('.');
                        //                        string oldfile = output + "\\" + cur[0] + "_" + datetime + "." + cur[1];
                        //                        try
                        //                        {
                        //                            File.Move(item, oldfile);
                        //                        }
                        //                        catch (Exception)
                        //                        {
                        //                            //lblMessage.Text = ex.Message;
                        //                        }
                        //                        //File.Delete(fullpath);
                        //                    }
                        //                }
                        //                //WAIT("Wait", "Waiting for file logs...");
                        //            }
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        Console.WriteLine(ex.Message);
                        //    }
                        //}
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            section011.Visible = true;
            section021.Visible = false;
            section031.Visible = false;
            lblStatus.Text = "Status";
            lblMessage.Text = "Message";
            lblStatus.BackColor = Color.White;
            lblStatus.ForeColor = Color.Black;
            lblMessage.BackColor = Color.White;
            lblMessage.ForeColor = Color.Black;
            lblpathfile.Text = "";

            dataGridView1.DataSource = listnull;
            //fileWatcher.Created -= new FileSystemEventHandler(OnFileChanged);
            pass = 0; ng = 0; total = 0;
            btnStart.Enabled = true;
            lblPathRoot.Text = "Path root";
            lblPathLog.Text = "Path log";
            btnStop.Enabled = false;
            lblConfigs.Enabled = true;
            this.BeginInvoke(new Action(() => { lblPASS.Text = pass.ToString(); lblNG.Text = ng.ToString(); lblTOTAL.Text = total.ToString(); }));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //lblmodel.Text = "Model:" + InfoData.Model;
            btnStop.Enabled = true;
            btnStart.Enabled = false;
            lblConfigs.Enabled = false;
            WAIT("Wait", "Waiting for file logs...");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            //string time = DateTime.Now.ToString("dd/MM/yyy hh:mm tt");
            // this.Text += "-" + time;
        }


        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                nummmm = row.Cells[3].Value.ToString();
                //int True = Convert.ToInt32(row.Cells[3].Value);
                if (nummmm == "F")
                {
                    row.DefaultCellStyle.BackColor = Color.DarkRed;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                //else
                //{
                //    row.DefaultCellStyle.BackColor = Color.DarkGreen;
                //    row.DefaultCellStyle.ForeColor = Color.White;
                //}
            }
        }

        private void lblConfigs_Click(object sender, EventArgs e)
        {
            new frmSettting().ShowDialog();
            DataConnect();

        }

        private void linkLabel2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblHidden_Click(object sender, EventArgs e)
        {

        }

        void WAIT(string status, string message)
        {
            section011.Visible = true;
            section021.Visible = false;
            section031.Visible = false;
            lblStatus.BackColor = Color.DarkOrange;
            lblStatus.ForeColor = Color.White;
            lblMessage.BackColor = Color.DarkOrange;
            lblMessage.ForeColor = Color.White;
            lblStatus.Text = status;
            lblMessage.Text = message;
        }

        private void linkLabel2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        void PASS(string status, string message)
        {
            section011.Visible = false;
            section031.Visible = false;
            section021.Visible = true;
            lblStatus.BackColor = Color.DarkGreen;
            lblStatus.ForeColor = Color.White;
            lblMessage.BackColor = Color.DarkGreen;
            lblMessage.ForeColor = Color.White;
            lblStatus.Text = status;
            lblMessage.Text = message;
        }
        void FAIL(string status, string message)
        {
            section011.Visible = false;
            section021.Visible = false;
            section031.Visible = true;
            lblStatus.BackColor = Color.DarkRed;
            lblStatus.ForeColor = Color.White;
            lblMessage.BackColor = Color.DarkRed;
            lblMessage.ForeColor = Color.White;
            lblStatus.Text = status;
            lblMessage.Text = message;
        }
    }
}
