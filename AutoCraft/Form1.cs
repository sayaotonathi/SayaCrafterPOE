using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
        Utilities.RECT rc;
        Point relp;
        Point absp;

        //1600*900預設通貨相對位置
        Point relAlt = new Point(100, 250); //改造
        Point relAug = new Point(200, 300); //增幅
        Point relChance = new Point(200, 250); //機會
        Point relScour = new Point(150, 400); //重鑄
        Point relRegal = new Point(360, 250); //富豪
        Point relChaos = new Point(460, 250); //混沌
        Point relAlch = new Point(460, 250); //點金
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
        Dictionary<string, int> testpre = new Dictionary<string, int>();
        Dictionary<string, int> testsuf = new Dictionary<string, int>();

        private void Form1_Load(object sender, EventArgs e)
        {
            gHook = new GlobalKeyboardHook(); //根據作者的程式碼(class)創造一個新物件
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);// 連結KeyDown事件
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                gHook.HookedKeys.Add(key);
            }
            gHook.hook();//開始監控
            lblAltPos.Text = $"{relAlt.X},{relAlt.Y}";
            lblAugPos.Text = $"{relAug.X},{relAug.Y}";
            lblChancePos.Text = $"{relChance.X},{relChance.Y}";
            lblScourPos.Text = $"{relScour.X},{relScour.Y}";
            lblRegalPos.Text = $"{relRegal.X},{relRegal.Y}";
            lblChaosPos.Text = $"{relChaos.X},{relChaos.Y}";
            lblCraftAreaPos.Text = $"{relCraftArea.X},{relCraftArea.Y}";
            hwnd = Utilities.FindWindow(null, "AAAAAA");

        }



        List<string> affixStr = new List<string>();
        List<int> affixNum = new List<int>();
        Dictionary<string, int> affix = new Dictionary<string, int>();
        Dictionary<string, int> pre = new Dictionary<string, int>();
        Dictionary<string, int> suf = new Dictionary<string, int>();


        int int_count_prefix = 0;
        int int_count_suffix = 0;
        //測試用按鈕
        private void button1_Click(object sender, EventArgs e)
        {
            string str = ut.getClipBoard();
            //ut.affixDetermine(str,-2, affix);
            //for (int i = 0;i< affix.Count; i++)
            //{
            //    Console.WriteLine(affix.ElementAt(i));
            //}
            ut.affixDetermine("增加 23 % 閃避值\r\n減少 7% 魔力保留", -1, testpre);
            Console.WriteLine(testpre.ElementAt(0));
            Console.WriteLine(testpre.ElementAt(1));
            ut.affixDetermine("減少 7% 魔力保留", -1, testsuf);
            Console.WriteLine(testsuf.ElementAt(0));
            ut.affixCheck(affix, testpre, testsuf, ref int_count_prefix, ref int_count_suffix);
            Console.WriteLine(int_count_prefix + "," + int_count_suffix);
        }


        #region 位置設定按鍵

        //設定座標
        public void setPos(IntPtr h, ref Point p, Label lbl)
        {
            Utilities.GetWindowRect(h, out rc);
            ut.getRelativePoint(h, out p);
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
                gb_AltOption.Enabled = ActivatingAction == 3 ? true : false;
            }
        }

        //機會石腳本
        private void chance(object delay)
        {
            int idelay = (int)delay;
            while (loopCounter < nudTime.Value)
            {
                ut.useCurrency(absScour, absCraftArea);
                Thread.Sleep(idelay);
                ut.useCurrency(absChance, absCraftArea);
                Thread.Sleep(idelay);
                loopCounter++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //寫入
            Affix a = new Affix();
            a.AffixName = "增加%閃避值增加%閃避值增加%閃避值增加%閃避值";
            a.AffixMin = 20;
            a.IsSelected = true;
            List<Affix> ls = new List<Affix>();
            ls.Add(a);
            using (var writer = new StreamWriter("./file.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(ls);
            }

            //讀取想要的詞綴設定
            using (var reader = new StreamReader("./file.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var record = csv.GetRecords<Affix>();
                    foreach(var i in record)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells["IsSelected"].Value = i.IsSelected;
                        dataGridView1.Rows[index].Cells["Affixname"].Value = i.AffixName;
                        dataGridView1.Rows[index].Cells["AffixMin"].Value = i.AffixMin;
                        dataGridView1.Rows[index].Cells["AffixMax"].Value = i.AffixMax;
                        if (i.IsSelected)
                        {
                            pre.Add(i.AffixName, i.AffixMin);
                        }
                    }
                }
            }
        }
        //勾選加入list，取消勾選移除
        //todo:前後綴分兩個dgv
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                this.dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            if ((bool)this.dataGridView1.CurrentCell.Value == true)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["AffixName"].Value != null)
                {
                    string key = dataGridView1.Rows[e.RowIndex].Cells["AffixName"].Value.ToString();
                    int value = dataGridView1.Rows[e.RowIndex].Cells["AffixMin"].Value == null ? (int)dataGridView1.Rows[e.RowIndex].Cells["AffixMin"].Value : 0;
                    pre.Add(key, value);
                }
            }
            else
            {
                pre.Remove(dataGridView1.Rows[e.RowIndex].Cells["AffixName"].Value.ToString());
            }
        }


        //重鑄點金腳本
        private void Alchemy(object delay)
        {
            //todo:之後index改成自訂
            int index = 1;
            int idelay = (int)delay;
            while (loopCounter < nudTime.Value)
            {
                ut.useCurrency(absScour, absCraftArea);
                Thread.Sleep(idelay);
                ut.useCurrency(absAlch, absCraftArea);
                Thread.Sleep(idelay);
                ut.CtrlC();
                string clip = ut.getClipBoard();
                Dictionary<string, int> clipAffix = new Dictionary<string, int>();
                ut.affixDetermine(clip, index, clipAffix);
                ut.affixCheck(clipAffix, testpre, testsuf, ref int_count_prefix, ref int_count_suffix);
                if (int_count_prefix >= nudStopPre.Value && int_count_suffix >= nudStopSuf.Value)
                {
                    loopCounter = 0;
                    break;
                }
                loopCounter++;
            }
        }

        //背景快捷功能
        /* https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes */


        //快捷觸發設定
        private void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            kv = (int)e.KeyValue;//把按下的按鍵號碼轉成整數存在kv中

            switch (kv)
            {
                case 114: //F3
                    pt.Abort();
                    flagActivate = false;
                    lblActiveStatus.Text = flagActivate ? "執行中" : "停止中";
                    loopCounter = 0;
                    break;
                case 113://F2
                         //Utilities.SetForegroundWindow(Utilities.FindWindow(null, "Path of Exile"));
                    if (!flagActivate)
                    {
                        lblActiveStatus.Text = "執行中";
                        switch (ActivatingAction)
                        {
                            case 1:
                                ut.getAbsolutePoint(hwnd, relAlch, out absAlch);
                                ut.getAbsolutePoint(hwnd, relScour, out absScour);
                                ut.getAbsolutePoint(hwnd, relCraftArea, out absCraftArea);
                                pt = new Thread(new ParameterizedThreadStart(Alchemy));
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                ut.getAbsolutePoint(hwnd, relChance, out absChance);
                                ut.getAbsolutePoint(hwnd, relScour, out absScour);
                                ut.getAbsolutePoint(hwnd, relCraftArea, out absCraftArea);
                                pt = new Thread(new ParameterizedThreadStart(chance));
                                break;
                        }
                        Utilities.SetForegroundWindow(hwnd);
                        int delay = (int)nudDelay.Value;
                        pt.Start(delay);
                        loopCounter = 0;
                    }
                    break;

                case 112: //F1設定座標
                    Utilities.SetForegroundWindow(hwnd);
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
    }
}


