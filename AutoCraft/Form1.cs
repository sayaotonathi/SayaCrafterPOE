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
        Point relCraftArea = new Point(290, 408); //做裝區域
        bool flagSetAlt = false;
        bool flagSetAug = false;
        bool flagSetChance = false;
        bool flagSetScour = false;
        bool flagSetRegal = false;
        bool flagSetChaos = false;
        bool flagSetCraftArea = false;
        bool flagSetAlch = false;

        Point absAlt;
        Point absAlch;
        Point absAug;
        Point absChance;
        Point absScour;
        Point absRegal;
        Point absChaos;
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
            //hwnd = Utilities.FindWindow(null, "AAAAAA");
            hwnd = MouseAndKeyEvent.FindWindow(null, "Path of Exile");

        }

        //List<string> affixStr = new List<string>();
        //List<int> affixNum = new List<int>();
        //Dictionary<string, float> affix = new Dictionary<string, float>();
        Dictionary<string, float> pre = new Dictionary<string, float>();
        Dictionary<string, float> suf = new Dictionary<string, float>();



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
            flagSetAlch = true;
            lblAlchPos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }
        private void btnSetAltPos_Click(object sender, EventArgs e)
        {
            flagSetAlt = true;
            lblAltPos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }

        private void btnSetAugPos_Click(object sender, EventArgs e)
        {
            flagSetAug = true;
            lblAugPos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }

        private void btnSetRegalPos_Click(object sender, EventArgs e)
        {
            flagSetRegal = true;
            lblRegalPos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }

        private void btnSetChaosPos_Click(object sender, EventArgs e)
        {
            flagSetChaos = true;
            lblChaosPos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }

        private void btnSetChancePos_Click(object sender, EventArgs e)
        {
            flagSetChance = true;
            lblChancePos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }

        private void btnSetScourPos_Click(object sender, EventArgs e)
        {
            flagSetScour = true;
            lblScourPos.Text = "設定中，按F1確定";
            pnlSetBTNs.Enabled = false;
        }

        private void btnSetCraftAreaPos_Click(object sender, EventArgs e)
        {
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
                if (ActivatingAction != 3)
                {
                    cb_Augment.Checked = false;
                }
            }
        }
        //勾選加入list，取消勾選移除
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            var s = dgv.Name == "dgv_Prefix" ? pre : suf;
            string str = dgv.Name == "dgv_Prefix" ? "pre" : "suf";
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
            List<Affix> prefix = new List<Affix>();
            var prefixrows = dgv_Prefix.Rows;
            for (int i = 0; i < prefixrows.Count - 1; i++)
            {
                Affix affix = new Affix
                {
                    IsSelected = prefixrows[i].Cells["dgvpreIsSelected"].Value != null ? (bool)prefixrows[i].Cells["dgvpreIsSelected"].Value : false,
                    AffixName = (string)prefixrows[i].Cells["dgvpreAffixName"].Value,
                    AffixMin = (string)prefixrows[i].Cells["dgvpreAffixMin"].Value,
                    AffixMax = (string)prefixrows[i].Cells["dgvpreAffixMax"].Value
                };
                prefix.Add(affix);
            }
            using (var writer = new StreamWriter("./prefixs.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(prefix);
            }

            List<Affix> suffix = new List<Affix>();
            var suffixrows = dgv_Suffix.Rows;
            for (int i = 0; i < suffixrows.Count - 1; i++)
            {
                //Affix affix = new Affix();
                //affix.IsSelected=(bool)suffixrows[i].Cells["dgvsufIsSelected"].Value;
                //affix.AffixName = (string)suffixrows[i].Cells["dgvsufAffixName"].Value;
                //affix.AffixMin = (string)suffixrows[i].Cells["dgvsufAffixMin"].Value;
                //affix.AffixMax = (string)suffixrows[i].Cells["dgvsufAffixMax"].Value;
                Affix affix = new Affix
                {
                    IsSelected = suffixrows[i].Cells["dgvsufIsselected"].Value != null ? (bool)suffixrows[i].Cells["dgvsufIsselected"].Value : false,
                    AffixName = (string)suffixrows[i].Cells["dgvsufAffixName"].Value,
                    AffixMin = (string)suffixrows[i].Cells["dgvsufAffixMin"].Value,
                    AffixMax = (string)suffixrows[i].Cells["dgvsufAffixMax"].Value
                };
                suffix.Add(affix);
            }
            using (var writer = new StreamWriter("./suffixs.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(suffix);
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
                    ut.sendCtrlC();
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
                                //Alchemy((int)nudDelay.Value);
                                //flagActivate = false;
                                //this.lblActiveStatus.Text = "停止中";
                                //ut.sendLShiftKeyUp();
                                //ut.sendLeftClickUp();
                                //ut.sendRightClickUp();

                                break;
                            case 2:
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relChaos, out absChaos);
                                //MouseAndKeyEvent.getAbsolutePoint(hwnd, relScour, out absScour);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relCraftArea, out absCraftArea);
                                pts = new ParameterizedThreadStart(Chaos);
                                //Chaos((int)nudDelay.Value);
                                //flagActivate = false;
                                //this.lblActiveStatus.Text = "停止中";
                                break;
                            case 3:
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relAlt, out absAlt);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relAug, out absAug);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relCraftArea, out absCraftArea);
                                pts = new ParameterizedThreadStart(Alter);
                                break;
                            case 4:
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relChance, out absChance);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relScour, out absScour);
                                MouseAndKeyEvent.getAbsolutePoint(hwnd, relCraftArea, out absCraftArea);
                                pts = new ParameterizedThreadStart(Chance);
                                //Chance((int)nudDelay.Value);
                                //flagActivate = false;
                                //this.lblActiveStatus.Text = "停止中";
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
                    //ut.sendLShiftKeyUp();
                    //ut.sendLeftClickUp();
                    //ut.sendRightClickUp();
                    break;

                case 112: //F1設定座標
                    MouseAndKeyEvent.SetForegroundWindow(hwnd);
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
                    pnlSetBTNs.Enabled = true;
                    break;
            }
        }

        #region 功能區
        //重新載入詞綴功能
        private void ReloadAffix()
        {
            pre.Clear();
            suf.Clear();
            dgv_Prefix.Rows.Clear();
            dgv_Suffix.Rows.Clear();

            //讀取想要的詞綴設定
            using (var reader = new StreamReader("./prefixs.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var record = csv.GetRecords<Affix>();
                    foreach (var i in record)
                    {
                        var index = dgv_Prefix.Rows.Add();
                        dgv_Prefix.Rows[index].Cells["dgvpreIsSelected"].Value = i.IsSelected;
                        dgv_Prefix.Rows[index].Cells["dgvpreAffixname"].Value = i.AffixName;
                        dgv_Prefix.Rows[index].Cells["dgvpreAffixMin"].Value = i.AffixMin;
                        dgv_Prefix.Rows[index].Cells["dgvpreAffixMax"].Value = i.AffixMax;
                        if (i.IsSelected)
                        {
                            pre.Add(i.AffixName, float.Parse(i.AffixMin));
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
                        var index = dgv_Suffix.Rows.Add();
                        dgv_Suffix.Rows[index].Cells["dgvsufIsSelected"].Value = i.IsSelected;
                        dgv_Suffix.Rows[index].Cells["dgvsufAffixname"].Value = i.AffixName;
                        dgv_Suffix.Rows[index].Cells["dgvsufAffixMin"].Value = i.AffixMin;
                        dgv_Suffix.Rows[index].Cells["dgvsufAffixMax"].Value = i.AffixMax;
                        if (i.IsSelected)
                        {
                            suf.Add(i.AffixName, float.Parse(i.AffixMin));
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
                if (ut.checkAffix(clipAffix, pre, suf,4, stopPre,stopSuf))
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
                ut.useCurrencyContinuously(absChaos, absCraftArea, index, idelay, fireTimes, stopPre, stopSuf, pre, suf, ref flagActivate, 4,ref counter);
            });
        }
        private void Chaos(int delay)
        {
            int index = (int)nudIndex.Value;
            int idelay = (int)delay;
            int fireTimes = (int)nudTime.Value;
            int stopPre = (int)nudStopPre.Value;
            int stopSuf = (int)nudStopSuf.Value;
            int counter = 0;
            ut.useCurrencyContinuously(absChaos, absCraftArea, index, idelay, fireTimes, stopPre, stopSuf, pre, suf, ref flagActivate, 4,ref counter);

        }

        //todo
        //改造石腳本
        private void Alter(object delay)
        {
            int index = (int)nudIndex.Value;
            int idelay = (int)delay;
            int fireTimes = (int)nudTime.Value;
            int stopPre = (int)nudStopPre.Value;
            int stopSuf = (int)nudStopSuf.Value;
            int stopMode = 4;
            int counter = 0;
            if (cb_Augment.Checked)
            {
                //var checkedAug = pnl_Aug_Option.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked).Tag;
                //if (checkedAug != null)
                //{
                //    stopMode = (int)checkedAug;
                //}
                stopMode = 2;
            }
            //if (cb_Regal.Checked)
            //{
            //    var checkedRegal = pnl_Regal_Option.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked).Tag;
            //    if (checkedRegal != null)
            //    {
            //        stopMode = (int)checkedRegal;
            //    }
            //}
            this.InvokeIfRequired(() =>
            {
                bool flag = true;
                while (flag&&flagActivate)
                {
                    Tuple<int, int> affixNum = ut.useCurrencyContinuously(absAlt, absCraftArea, index, idelay, fireTimes, stopPre, stopSuf, pre, suf, ref flagActivate, stopMode,ref counter);
                    Thread.Sleep(idelay);
                    if (affixNum.Item1>= stopPre||(affixNum.Item1==0&&affixNum.Item2==0)||counter>=fireTimes)
                    {
                        flag = false;
                    }
                    else if (cb_Augment.Checked)
                    {
                        ut.useCurrency(absAug, absCraftArea);
                        string clip = ut.getClipBoardLogic();
                        Dictionary<string, float> clipAffix = ut.affixDetermine(clip, index);
                        flag = !ut.checkAffix(clipAffix, pre, suf, 4, stopPre, stopSuf);
                    }
                    else
                    {
                        flag = false;
                    }
                    Thread.Sleep(idelay);
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
            panel2.Visible = ((CheckBox)sender).Checked ? true : false;

        }

        private void cb_Regal_CheckedChanged(object sender, EventArgs e)
        {
            pnl_Regal_Option.Enabled = ((CheckBox)sender).Checked ? true : false;
        }


        #region 增幅富豪互斥
        //增幅富豪僅前/後綴選項互斥
        private void rb_Aug_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                rb_Regal_Pre.Checked = false;
                rb_Regal_Suf.Checked = false;
            }
        }
        private void rb_Regal_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                rb_Aug_Pre.Checked = false;
                rb_Aug_Pre.Checked = false;
            }
        }

        //增幅富豪前或後選項互斥
        private void rb_AugRegal_PreOrSuf_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                rb_Aug_Or.Checked = rb == rb_Aug_Or ? true : false;
                rb_Regal_Or.Checked = rb == rb_Regal_Or ? true : false;
            }
        }
        #endregion
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
                nudIndex.Value = listBox1.SelectedIndex;
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


