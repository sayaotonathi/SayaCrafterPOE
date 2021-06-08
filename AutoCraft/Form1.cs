using AutoCraft.ControlHandler;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCraft
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        GlobalKeyboardHook gHook;
        int kv; //將keyValue轉成整數用的變數
        bool ctrl, alt, shift; //按下功能鍵時就改為true
        private Thread pt = null; //多緒執行，使終止快捷可以作用

        bool flagActivate = false; //是否運作中flag
        int loopCounter = 0; //功能執行次數
        int ActivatingAction = 1;//正在使用中的腳本，1:點金 2:混沌 3:改造 4:機會

        //IntPtr hwnd = Utilities.FindWindow(null, "Path of Exile");
        IntPtr hwnd;
        IntPtr thisHwnd;
        MouseAndKeyEvent.RECT rc;
        Point relp;
        Point absp;

        //1600*900預設通貨相對位置
        Point relAlt = new Point(100, 250); //改造
        Point relAug = new Point(200, 300); //增幅
        Point relChance = new Point(200, 250); //機會
        Point relScour = new Point(150, 400); //重鑄
        Point relRegal = new Point(360, 250); //富豪
        Point relChaos = new Point(460, 250); //混沌
        Point relAlch = new Point(410, 250); //點金
        Point relTrans = new Point(50, 250); //蛻變
        Point relCraftArea = new Point(290, 408); //做裝區域
        bool flagSetAlt = false;
        bool flagSetAug = false;
        bool flagSetChance = false;
        bool flagSetScour = false;
        bool flagSetRegal = false;
        bool flagSetChaos = false;
        bool flagSetCraftArea = false;
        bool flagSetAlch = false;
        bool flagSetTrans = false;


        Point absAlt;
        Point absAlch;
        Point absAug;
        Point absChance;
        Point absScour;
        Point absRegal;
        Point absChaos;
        Point absTrans;
        Point absCraftArea;

        Utilities ut = new Utilities();
        //Dictionary<string, float> testpre = new Dictionary<string, float>();
        //Dictionary<string, float> testsuf = new Dictionary<string, float>();

        private void Form1_Load(object sender, EventArgs e)
        {
            gHook = new GlobalKeyboardHook(); //根據作者的程式碼(class)創造一個新物件
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);// 連結KeyDown事件
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                gHook.HookedKeys.Add(key);
            }
            ReloadAffix();//讀取詞綴設定檔
            LoadPosSetting();//讀取位置設定
            gHook.hook();//開始監控
            lblAltPos.Text = $"{relAlt.X},{relAlt.Y}";
            lblAugPos.Text = $"{relAug.X},{relAug.Y}";
            lblChancePos.Text = $"{relChance.X},{relChance.Y}";
            lblScourPos.Text = $"{relScour.X},{relScour.Y}";
            lblRegalPos.Text = $"{relRegal.X},{relRegal.Y}";
            lblChaosPos.Text = $"{relChaos.X},{relChaos.Y}";
            lblCraftAreaPos.Text = $"{relCraftArea.X},{relCraftArea.Y}";
            lblAlchPos.Text = $"{relAlch.X},{relAlch.Y}";
            lblTransPos.Text = $"{relTrans.X},{relTrans.Y}";
            hwnd = MouseAndKeyEvent.FindWindow(null, "Path of Exile");
            thisHwnd = MouseAndKeyEvent.FindWindow(null, "POE Auto Crafter by Saya");

        }
        Dictionary<string, float> dictStop = new Dictionary<string, float>();
        Dictionary<string, float> dictAug = new Dictionary<string, float>();
        Dictionary<string, float> dictRegal = new Dictionary<string, float>();



        //測試用按鈕
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        private void button1_Click(object sender, EventArgs e)
        {
            //string i = "傳奇頭目有 21.8% 機率掉落 1 個額外銀幣";
            //string j = "+42% 冰冷抗性";
            //int r;
            //decimal r2;
            //string s = Regex.Match(i, "[0-9.]+([0-9]{0,3})").Value;
            //string s3 = Regex.Match(i, "[0 - 9.]{1,}").Value;
            //string s4 = Regex.Match(j, "[0 - 9.]{1,}").Value;

            //string s2 = Regex.Match(j, "[0-9.]+([0-9]{0,3})").Value;
            //Console.WriteLine(int.TryParse(Regex.Match(i, "[0-9.]+([0-9]{0,3})").Value, out r));
            //Console.WriteLine(int.TryParse(Regex.Match(j, "[0-9.]+([0-9]{0,3})").Value, out r));
            //Console.WriteLine(decimal.TryParse("42", out r2));
            Process[] processes = Process.GetProcessesByName("PathOfExile_x64");

            //InputSimulator inputSimulator = new InputSimulator();
            //inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
            foreach (Process proc in processes)
            {
                SendMessage(proc.MainWindowHandle, 0x0100, 0x10, 0);
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            string s = "完成輿圖征服者勢力地圖，有 3% 機率獲得 2 倍定位他們壁壘位置的進度";
            string[] substr = ut.SplitClipBoard(s, "\r\n");

            //將詞綴區塊逐條分開
            string[] substr2 = Regex.Split(substr[0], "\r\n");

            //分離詞綴&數字並放入字典，沒有數字的詞綴數字給0
            foreach (string i in substr2)
            {
                decimal r;
                string s2 = Regex.Match(i, "[0-9.]+([0-9]{0,3})").Value;
                if (!decimal.TryParse(s2, out r))
                {
                    r = 0;
                }

                string s3 = Regex.Replace(i, "[0-9.]+([0-9]{0,3})", "").Replace(" ", "");
            }
        }

        #region 位置設定按鍵

        //設定座標
        public void setPos(IntPtr h, ref Point p, Label lbl)
        {
            MouseAndKeyEvent.GetWindowRect(h, out rc);
            MouseAndKeyEvent.getRelativePoint(h, out p);
            lbl.Text = $"{p.X},{p.Y}";

        }
        private void btnSetAlchPos_Click(object sender, EventArgs e)
        {
            MouseAndKeyEvent.SetForegroundWindow(hwnd);
            flagSetAlch = true;
            lblAlchPos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }
        private void btnSetAltPos_Click(object sender, EventArgs e)
        {
            MouseAndKeyEvent.SetForegroundWindow(hwnd);
            flagSetAlt = true;
            lblAltPos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }

        private void btnSetAugPos_Click(object sender, EventArgs e)
        {
            MouseAndKeyEvent.SetForegroundWindow(hwnd);
            flagSetAug = true;
            lblAugPos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }

        private void btnSetRegalPos_Click(object sender, EventArgs e)
        {
            MouseAndKeyEvent.SetForegroundWindow(hwnd);
            flagSetRegal = true;
            lblRegalPos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }

        private void btnSetChaosPos_Click(object sender, EventArgs e)
        {
            MouseAndKeyEvent.SetForegroundWindow(hwnd);
            flagSetChaos = true;
            lblChaosPos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }

        private void btnSetChancePos_Click(object sender, EventArgs e)
        {
            MouseAndKeyEvent.SetForegroundWindow(hwnd);
            flagSetChance = true;
            lblChancePos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }

        private void btnSetScourPos_Click(object sender, EventArgs e)
        {
            MouseAndKeyEvent.SetForegroundWindow(hwnd);
            flagSetScour = true;
            lblScourPos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }
        private void btnSetTransPos_Click(object sender, EventArgs e)
        {
            MouseAndKeyEvent.SetForegroundWindow(hwnd);
            flagSetTrans = true;
            lblTransPos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }

        private void btnSetCraftAreaPos_Click(object sender, EventArgs e)
        {
            MouseAndKeyEvent.SetForegroundWindow(hwnd);
            flagSetCraftArea = true;
            lblCraftAreaPos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }

        #endregion
        //選擇腳本
        private void ChangeActivatingAction(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                ActivatingAction = int.Parse(rb.Tag.ToString());
                cb_Augment.Enabled = ActivatingAction == 3 ? true : false;
                cb_Regal.Enabled = ActivatingAction == 3 ? true : false;

                if (ActivatingAction != 3)
                {
                    cb_Augment.Checked = false;
                    cb_Regal.Checked = false;
                    cb_AugBeforeRegal.Checked = false;
                }
            }
        }
        //勾選加入list，取消勾選移除
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            Dictionary<string, float> s = dgv.Name == "dgv_Stop" ? dictStop : dictAug;
            string str = dgv.Name == "dgv_Stop" ? "Stop" : "Aug";

            switch (dgv.Name)
            {
                case "dgv_Stop":
                    s = dictStop;
                    str = "Stop";
                    break;
                case "dgv_Aug":
                    s = dictAug;
                    str = "Aug";
                    break;
                case "dgv_regal":
                    s = dictRegal;
                    str = "Regal";
                    break;
            }


            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if ((bool)dgv.CurrentCell.Value == true)
                {
                    if (dgv.Rows[e.RowIndex].Cells[$"dgv{str}AffixName"].Value == null)
                    {
                        dgv.Rows[e.RowIndex].Cells[$"dgv{str}IsSelected"].Value = ((DataGridViewCheckBoxCell)dgv.Rows[e.RowIndex].Cells[$"dgv{str}IsSelected"]).FalseValue;
                        dgv.EndEdit();
                    }
                    else
                    {
                        string key = dgv.Rows[e.RowIndex].Cells[$"dgv{str}AffixName"].Value.ToString();
                        int value = dgv.Rows[e.RowIndex].Cells[$"dgv{str}AffixMin"].Value == null ? (int)dgv.Rows[e.RowIndex].Cells[$"dgv{str}AffixMin"].Value : 0;
                        s.Add(key, value);
                    }
                }
                else
                {
                    string key = dgv.Rows[e.RowIndex].Cells[$"dgv{str}AffixName"].Value == null ? "" : dgv.Rows[e.RowIndex].Cells[$"dgv{str}AffixName"].Value.ToString();
                    if (s.ContainsKey(key))
                    {
                        s.Remove(key);
                    }
                }
            }

        }
        //儲存詞綴按鈕
        private void btn_SaveAffix_Click(object sender, EventArgs e)
        {
            //停止條件
            List<Affix> prefix = new List<Affix>();
            var prefixrows = dgv_Stop.Rows;
            for (int i = 0; i < prefixrows.Count - 1; i++)
            {
                Affix affix = new Affix
                {
                    IsSelected = prefixrows[i].Cells["dgvStopIsSelected"].Value != null ? (bool)prefixrows[i].Cells["dgvStopIsSelected"].Value : false,
                    AffixName = (string)prefixrows[i].Cells["dgvStopAffixName"].Value,
                    AffixMin = (string)prefixrows[i].Cells["dgvStopAffixMin"].Value,
                    AffixMax = (string)prefixrows[i].Cells["dgvStopAffixMax"].Value
                };
                prefix.Add(affix);
            }

            using (var writer = new StreamWriter("./prefixs.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(prefix);
            }


            //增幅條件
            List<Affix> aug = new List<Affix>();
            var augrows = dgv_Aug.Rows;
            for (int i = 0; i < augrows.Count - 1; i++)
            {
                Affix affix = new Affix
                {
                    IsSelected = augrows[i].Cells["dgvAugIsselected"].Value != null ? (bool)augrows[i].Cells["dgvAugIsselected"].Value : false,
                    AffixName = (string)augrows[i].Cells["dgvAugAffixName"].Value,
                    AffixMin = (string)augrows[i].Cells["dgvAugAffixMin"].Value,
                    AffixMax = (string)augrows[i].Cells["dgvAugAffixMax"].Value
                };
                aug.Add(affix);
            }

            using (var writer = new StreamWriter("./suffixs.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(aug);
            }


            //富豪條件
            List<Affix> regal = new List<Affix>();
            var regalrows = dgv_Regal.Rows;
            for (int i = 0; i < regalrows.Count - 1; i++)
            {
                Affix affix = new Affix
                {
                    IsSelected = regalrows[i].Cells["dgvRegalIsSelected"].Value != null ? (bool)regalrows[i].Cells["dgvRegalIsselected"].Value : false,
                    AffixName = (string)regalrows[i].Cells["dgvRegalAffixName"].Value,
                    AffixMin = (string)regalrows[i].Cells["dgvRegalAffixMin"].Value,
                    AffixMax = (string)regalrows[i].Cells["dgvRegalAffixMax"].Value
                };
                regal.Add(affix);
            }

            using (var writer = new StreamWriter("./regal.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(regal);
            }



        }
        //重新載入按鈕
        private void btn_ReloadAffix_Click(object sender, EventArgs e)
        {
            ReloadAffix();
        }



        //背景快捷功能
        /* https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes */


        //快捷觸發設定
        private void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            kv = (int)e.KeyValue;//把按下的按鍵號碼轉成整數存在kv中

            switch (kv)
            {
                case 115:
                    ThreadStart ptss = new ThreadStart(() =>
                    {
                        this.InvokeIfRequired(() =>
                        {
                            ut.sendCtrlC();
                            MessageBox.Show(ut.getClipBoard());
                        });
                    });
                    var ppt = new Thread(ptss);
                    ppt.Start();

                    break;
                case 114: //F3
                    pt.Abort();
                    flagActivate = false;
                    lblActiveStatus.Text = flagActivate ? "執行中" : "停止中";
                    //ut.sendLShiftKeyUp();
                    //ut.sendLeftClickUp();
                    //ut.sendRightClickUp();
                    loopCounter = 0;
                    break;
                case 113://F2
                    MouseAndKeyEvent.SetForegroundWindow(MouseAndKeyEvent.FindWindow(null, "Path of Exile"));
                    if (!flagActivate)
                    {
                        flagActivate = true;
                        lblActiveStatus.Text = "執行中";
                        ParameterizedThreadStart pts = null;
                        switch (ActivatingAction)
                        {
                            case 1:
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relAlch, out absAlch);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relScour, out absScour);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relCraftArea, out absCraftArea);
                                pts = new ParameterizedThreadStart(Alchemy);
                                break;
                            case 2:
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relChaos, out absChaos);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relCraftArea, out absCraftArea);
                                pts = new ParameterizedThreadStart(Chaos);
                                
                                break;
                            case 3:
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relAlt, out absAlt);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relAug, out absAug);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relCraftArea, out absCraftArea);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relRegal, out absRegal);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relScour, out absScour);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relTrans, out absTrans);
                                pts = new ParameterizedThreadStart(Alter);
                                break;
                            case 4:
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relChance, out absChance);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relScour, out absScour);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relCraftArea, out absCraftArea);
                                pts = new ParameterizedThreadStart(Chance);
                               
                                break;
                        }
                        MouseAndKeyEvent.SetForegroundWindow(hwnd);
                        int delay = (int)nudDelay.Value;
                        pts += (iii) =>
                        {
                            flagActivate = false;
                            this.InvokeIfRequired(() =>
                            {
                                this.lblActiveStatus.Text = "停止中";
                            });
                        };
                        pt = new Thread(pts);
                        pt.Start(delay);
                        loopCounter = 0;
                    }
                   
                    break;

                case 112: //F1設定座標
                    MouseAndKeyEvent.SetForegroundWindow(thisHwnd);
                    if (flagSetAlt)
                    {
                        setPos(hwnd, ref relAlt, lblAltPos);
                        Console.WriteLine(relAlt.X + "," + relAlt.Y);
                        flagSetAlt = false;
                    }
                    if (flagSetAug)
                    {
                        setPos(hwnd, ref relAug, lblAugPos);
                        Console.WriteLine(relAug.X + "," + relAug.Y);
                        flagSetAug = false;
                    }
                    if (flagSetRegal)
                    {
                        setPos(hwnd, ref relRegal, lblRegalPos);
                        Console.WriteLine(relRegal.X + "," + relRegal.Y);
                        flagSetRegal = false;
                    }
                    if (flagSetChaos)
                    {
                        setPos(hwnd, ref relChaos, lblChaosPos);
                        Console.WriteLine(relChaos.X + "," + relChaos.Y);
                        flagSetChaos = false;
                    }
                    if (flagSetChance)
                    {
                        setPos(hwnd, ref relChance, lblChancePos);
                        Console.WriteLine(relChance.X + "," + relChance.Y);
                        flagSetChance = false;
                    }
                    if (flagSetScour)
                    {
                        setPos(hwnd, ref relScour, lblScourPos);
                        Console.WriteLine(relScour.X + "," + relScour.Y);
                        flagSetScour = false;
                    }
                    if (flagSetCraftArea)
                    {
                        setPos(hwnd, ref relCraftArea, lblCraftAreaPos);
                        Console.WriteLine(relCraftArea.X + "," + relCraftArea.Y);
                        flagSetCraftArea = false;
                    }
                    if (flagSetAlch)
                    {
                        setPos(hwnd, ref relAlch, lblAlchPos);
                        Console.WriteLine(relAlch.X + "," + relAlch.Y);
                        flagSetAlch = false;
                    }
                    if (flagSetTrans)
                    {
                        setPos(hwnd, ref relTrans, lblTransPos);
                        Console.WriteLine(relTrans.X + "," + relTrans.Y);
                        flagSetTrans = false;
                    }
                    pnlSetBTNs.Enabled = true;
                    break;
            }
        }

        #region 功能區
        //重新載入詞綴功能
        private void ReloadAffix()
        {
            dictStop.Clear();
            dictAug.Clear();
            dictRegal.Clear();
            dgv_Stop.Rows.Clear();
            dgv_Aug.Rows.Clear();
            dgv_Regal.Rows.Clear();

            //讀取想要的詞綴設定
            using (var reader = new StreamReader("./prefixs.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var record = csv.GetRecords<Affix>();
                    foreach (var i in record)
                    {
                        var index = dgv_Stop.Rows.Add();
                        dgv_Stop.Rows[index].Cells["dgvStopIsSelected"].Value = i.IsSelected;
                        dgv_Stop.Rows[index].Cells["dgvStopAffixname"].Value = i.AffixName;
                        dgv_Stop.Rows[index].Cells["dgvStopAffixMin"].Value = i.AffixMin;
                        dgv_Stop.Rows[index].Cells["dgvStopAffixMax"].Value = i.AffixMax;
                        if (i.IsSelected)
                        {
                            dictStop.Add(i.AffixName, float.Parse(i.AffixMin));
                        }
                    }
                }
            }

            using (var reader = new StreamReader("./suffixs.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var record = csv.GetRecords<Affix>();
                    foreach (var i in record)
                    {
                        var index = dgv_Aug.Rows.Add();
                        dgv_Aug.Rows[index].Cells["dgvAugIsSelected"].Value = i.IsSelected;
                        dgv_Aug.Rows[index].Cells["dgvAugAffixname"].Value = i.AffixName;
                        dgv_Aug.Rows[index].Cells["dgvAugAffixMin"].Value = i.AffixMin;
                        dgv_Aug.Rows[index].Cells["dgvAugAffixMax"].Value = i.AffixMax;
                        if (i.IsSelected)
                        {
                            dictAug.Add(i.AffixName, float.Parse(i.AffixMin));
                        }
                    }
                }
            }
            using (var reader = new StreamReader("./regal.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var record = csv.GetRecords<Affix>();
                    foreach (var i in record)
                    {
                        var index = dgv_Regal.Rows.Add();
                        dgv_Regal.Rows[index].Cells["dgvRegalIsSelected"].Value = i.IsSelected;
                        dgv_Regal.Rows[index].Cells["dgvRegalAffixname"].Value = i.AffixName;
                        dgv_Regal.Rows[index].Cells["dgvRegalAffixMin"].Value = i.AffixMin;
                        dgv_Regal.Rows[index].Cells["dgvRegalAffixMax"].Value = i.AffixMax;
                        if (i.IsSelected)
                        {
                            dictRegal.Add(i.AffixName, float.Parse(i.AffixMin));
                        }
                    }
                }
            }
        }

        //讀取設定
        private void LoadPosSetting()
        {
            //讀取想要的詞綴設定
            using (var reader = new StreamReader("./position.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var record = csv.GetRecords<Position>();
                    foreach (var i in record)
                    {
                        relAlt = new Point(i.relAltX, i.relAltY); //改造
                        relAug = new Point(i.relAugX, i.relAugY); //增幅
                        relChance = new Point(i.relChanceX, i.relChanceY); //機會
                        relScour = new Point(i.relScourX, i.relScourY); //重鑄
                        relRegal = new Point(i.relRegalX, i.relRegalY); //富豪
                        relChaos = new Point(i.relChaosX, i.relChaosY); //混沌
                        relAlch = new Point(i.relAlchX, i.relAlchY); //點金
                        relCraftArea = new Point(i.relCraftAreaX, i.relCraftAreaY); //做裝區域
                        relTrans = new Point(i.relTransX, i.relTransY);//蛻變
                        nudIndex.Value = i.Index;
                    }
                }
            }
        }

        #endregion
        #region 腳本區
        //重鑄點金腳本
        private void Alchemy(object delay)
        {
            int index = (int)nudIndex.Value;
            int idelay = (int)delay;
            int stopPre = (int)nudStopPre.Value;
            int stopSuf = (int)nudStopSuf.Value;
            int fireTimes = (int)nudTime.Value;

            while (loopCounter < fireTimes && flagActivate)
            {
                loopCounter++;
                string clip = "";
                Dictionary<string, float> clipAffix = new Dictionary<string, float>();
                ut.useCurrency(absScour, absCraftArea);
                Thread.Sleep(idelay);
                ut.useCurrency(absAlch, absCraftArea);
                Thread.Sleep(idelay);
                ut.sendCtrlC();
                this.InvokeIfRequired(() =>
                {
                    clip = ut.getClipBoard();
                });
                ut.affixDetermine(clip, index, clipAffix);
                if (ut.checkAffix(clipAffix, dictStop, stopPre))
                {
                    loopCounter = 0;
                    Console.WriteLine(clip);
                    break;
                }
            }
        }
        //混沌石腳本
        private void Chaos(object delay)
        {
            int index = (int)nudIndex.Value;
            int idelay = (int)delay;
            int fireTimes = (int)nudTime.Value;
            int stopPre = (int)nudStopPre.Value;
            int stopSuf = (int)nudStopSuf.Value;
            int counter = 0;
            this.InvokeIfRequired(() =>
            {
                MouseAndKeyEvent.SetForegroundWindow(hwnd);
                ut.useCurrencyContinuously(absChaos, absCraftArea, index, idelay, fireTimes, stopPre, dictStop, ref flagActivate, ref counter);
            });
        }
       

        //todo
        //改造石腳本
        private void Alter(object delay)
        {
            int index = (int)nudIndex.Value;
            int idelay = (int)delay;
            int fireTimes = (int)nudTime.Value;
            int stopPre = (int)nudStopPre.Value;
            int ruleRegal = (int)nudRegal.Value;
            int counter = 0;
            this.InvokeIfRequired(() =>
            {
                MouseAndKeyEvent.SetForegroundWindow(hwnd);
                bool flag = true;
                //var continueRule = cb_Regal.Checked ? dictRegal : dictAug;
                //if (cb_Augment.Checked)
                //{
                //    continueRule = dictAug;
                //}
                string clip = "";
                Dictionary<string, float> clipAffix = new Dictionary<string, float>(); ;
                while (flag && flagActivate)
                {
                    //flag = false;
                    //清空字典避免拿到上個循環的數據
                    clipAffix.Clear();
                    //連續改造
                    Tuple<int,int,int> affixNum = ut.useCurrencyContinuously(absAlt, absCraftArea, index, idelay, fireTimes, stopPre,ruleRegal, dictStop,dictAug,dictRegal, ref flagActivate, ref counter);
                    Thread.Sleep(idelay);

                    //有選擇使用增幅
                    if (cb_Augment.Checked)
                    {
                        //判斷是否增幅
                        clip = ut.getClipBoardLogic();
                        clipAffix = ut.affixDetermine(clip, index);
                        if (affixNum.Item2!=0&&clipAffix.Count<2)
                        {
                            ut.useCurrency(absAug, absCraftArea);
                            Thread.Sleep(idelay);
                            clip = ut.getClipBoardLogic();
                            clipAffix = ut.affixDetermine(clip, index);
                        }
                        //點增幅後判斷是否停止
                        flag = !ut.checkAffix(clipAffix,  dictStop, stopPre);
                    }

                    //有選擇使用富豪
                    if (cb_Regal.Checked)
                    {
                        //判斷是否富豪
                        clip = ut.getClipBoardLogic();
                        clipAffix = ut.affixDetermine(clip, index);
                        if (ut.checkAffix(clipAffix,  dictRegal, ruleRegal))
                        {
                            //要富豪前增幅的話先點一次增幅
                            if (cb_AugBeforeRegal.Checked&&clipAffix.Count<2)
                            {
                                ut.useCurrency(absAug, absCraftArea);
                                Thread.Sleep(idelay);
                            }
                            ut.useCurrency(absRegal, absCraftArea);
                            Thread.Sleep(idelay);
                            clip = ut.getClipBoardLogic();
                            clipAffix = ut.affixDetermine(clip, index);
                            //富豪後判斷是否停止
                            flag = !ut.checkAffix(clipAffix, dictStop, stopPre);
                            //不停止的話重鑄蛻變
                            if (flag)
                            {
                                ut.useCurrency(absScour, absCraftArea);
                                Thread.Sleep(idelay);
                                ut.useCurrency(absTrans, absCraftArea);
                                Thread.Sleep(idelay);
                            }
                        }
                    }
                    //else
                    //{
                    //    flag = false;
                    //}
                    Thread.Sleep(idelay);

                    //符合停止條件 or 連續使用次數到上限回傳0 or 外迴圈次數到上限 停止
                    if (affixNum.Item1 >= stopPre || (affixNum.Item1 == 0&&affixNum.Item2==0&&affixNum.Item3==0) || counter >= fireTimes)
                    {
                        flag = false;
                    }
                }
            });
        }
        //機會石腳本
        private void Chance(object delay)
        {
            int idelay = (int)delay;
            while (loopCounter < nudTime.Value && flagActivate)
            {
                ut.useCurrency(absScour, absCraftArea);
                Thread.Sleep(idelay);
                ut.useCurrency(absChance, absCraftArea);
                Thread.Sleep(idelay);
                loopCounter++;
            }
        }
        #endregion

        private void cb_Augment_CheckedChanged(object sender, EventArgs e)
        {
            //pnl_Aug_Option.Enabled = ((CheckBox)sender).Checked ? true : false;
            panel2.Enabled = ((CheckBox)sender).Checked ? true : false;

        }
        private void cb_Regal_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Enabled = ((CheckBox)sender).Checked ? true : false;
            cb_AugBeforeRegal.Enabled = ((CheckBox)sender).Checked ? true : false;
            groupBox1.Enabled = ((CheckBox)sender).Checked ? true : false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int a = 1;
            int b = 2;
            int c;
            c = a++ + ++a + ++a + b--;
            Console.WriteLine(c);
        }

        private void btnGetClipboard_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string clip = ut.getClipBoard();
            if (clip == "")
            {
                MessageBox.Show("請先複製詞綴");
                return;
            }
            string[] splited = ut.SplitClipBoard(clip);
            foreach (string str in splited)
            {
                listBox1.Items.Add(str);
            }
        }





        //詞綴位置選取
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                Console.WriteLine(listBox1.Items.Count);
                nudIndex.Value = listBox1.SelectedIndex- listBox1.Items.Count;
            }
        }
        //詞綴儲存按鈕
        private void btnSavePos_Click(object sender, EventArgs e)
        {
            List<Position> ls = new List<Position>();

            Position position = new Position
            {
                relAltX = relAlt.X,
                relAltY = relAlt.Y,
                relAlchX = relAlch.X,
                relAlchY = relAlch.Y,
                relAugX = relAug.X,
                relAugY = relAug.Y,
                relChanceX = relChance.X,
                relChanceY = relChance.Y,
                relChaosX = relChaos.X,
                relChaosY = relChaos.Y,
                relCraftAreaX = relCraftArea.X,
                relCraftAreaY = relCraftArea.Y,
                relRegalX = relRegal.X,
                relRegalY = relRegal.Y,
                relScourX = relScour.X,
                relScourY = relScour.Y,
                relTransX = relTrans.X,
                relTransY = relTrans.Y,
                Index = (int)nudIndex.Value
            };
            ls.Add(position);
            using (var writer = new StreamWriter("./position.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(ls);
            }
        }
    }

}


