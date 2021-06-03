using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCraft
{
    class Utilities
    {

        //各種視窗控制
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode, EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_KEYDOWN = 0x0001;
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]

        //視窗結構
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        //滑鼠動作碼
        public enum MouseEventTFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }
        private const string Pattern_enter = "\r\n";

        //使用通貨，右鍵p1位置左鍵p2位置
        public void useCurrency(Point currencyPosition, Point craftArea)
        {
            int delay = 30;
            SetCursorPos(currencyPosition.X, currencyPosition.Y);
            Thread.Sleep(delay);
            rightClickCurrency(currencyPosition);
            SetCursorPos(craftArea.X, craftArea.Y);
            Thread.Sleep(delay);
            leftClickCraftArea(craftArea);
        }
        //連續使用通貨，stopMode 1=前綴, 2=後綴, 3=前或後, 4=前且後
        public void useCurrencyContinuously(Point currencyPosition, Point craftArea, int index, int idelay, int fireTimes, int stopPre, int stopSuf, Dictionary<string, int> pre, Dictionary<string, int> suf,ref bool stopFlag,int stopMode)
        {
            int loopCounter = 0;
            sendLShiftKeyDown();
            SetCursorPos(currencyPosition.X, currencyPosition.Y);
            Thread.Sleep(idelay);
            rightClickCurrency(currencyPosition);
            Thread.Sleep(idelay);
            SetCursorPos(craftArea.X, craftArea.Y);
            while (loopCounter < fireTimes&&stopFlag)
            {
                loopCounter++;
                Dictionary<string, int> clipAffix = new Dictionary<string, int>();
                int int_count_prefix = 0;
                int int_count_suffix = 0;
                Thread.Sleep(idelay);                Console.WriteLine(loopCounter);

                CtrlC();
                affixDetermine(getClipBoard(), index, clipAffix);
                affixCheck(clipAffix, pre, suf, out int_count_prefix, out int_count_suffix);
                bool pattern=true;
                switch (stopMode)
                {
                    case 1:
                        pattern = int_count_prefix > 0;
                        break;
                    case 2:
                        pattern = int_count_suffix > 0;
                        break;
                    case 3:
                        pattern = int_count_prefix > 0 || int_count_suffix >= 0;
                        break;
                    case 4:
                        pattern = (int_count_prefix >= stopPre) && (int_count_suffix >= stopSuf);
                        break;
                }
                if (pattern)
                {
                    break;
                }
                leftClickCraftArea(craftArea);
            }
            sendLShiftKeyUp();
        }

        //按住LShift
        public void sendLShiftKeyDown()
        {
            keybd_event((int)Keys.LShiftKey, 0, KEYEVENTF_KEYDOWN | 0, 0); //160=LShift
        }
        //放開LShift
        public void sendLShiftKeyUp()
        {
            keybd_event((int)Keys.LShiftKey, 0, KEYEVENTF_KEYUP | 0, 0); //160=LShift
        }

        public void rightClickCurrency(Point currencyPosition)
        {
            int delay = 30;
            mouse_event((int)(MouseEventTFlags.RIGHTDOWN), 0, 0, 0, 0);
            Thread.Sleep(delay);
            mouse_event((int)MouseEventTFlags.RIGHTUP, 0, 0, 0, 0);
            Thread.Sleep(delay);
        }
        public void leftClickCraftArea(Point craftArea)
        {
            int delay = 30;
            mouse_event((int)(MouseEventTFlags.LEFTDOWN), 0, 0, 0, 0);
            Thread.Sleep(delay);
            mouse_event((int)MouseEventTFlags.LEFTUP, 0, 0, 0, 0);
            Thread.Sleep(delay);
        }

        //複製
        public void CtrlC()
        {
            SendKeys.SendWait("^C");
        }

        //取得視窗內相對座標
        public void getRelativePoint(IntPtr hwnd, out Point relP)
        {
            RECT rc;
            GetWindowRect(hwnd, out rc);
            Rectangle rect = new Rectangle(rc.left, rc.top, rc.right, rc.bottom);
            Point p = Cursor.Position;
            relP = new Point();
            if (rect.Contains(p))
            {
                relP.X = Cursor.Position.X - rc.left;
                relP.Y = Cursor.Position.Y - rc.top;
            }
        }

        //取得螢幕絕對座標
        public void getAbsolutePoint(IntPtr hwnd, Point p1, out Point p2)
        {
            p2 = new Point();
            RECT rc;
            GetWindowRect(hwnd, out rc);
            Rectangle rect = new Rectangle(rc.left, rc.top, rc.right, rc.bottom);
            p2.X = p1.X + rc.left;
            p2.Y = p1.Y + rc.top;
        }
        //取剪貼簿
        public string getClipBoard()
        {
            if (Clipboard.ContainsText())
            {
                return Clipboard.GetText();
            }
            return "";
        }

        //拆分出詞綴區塊&分離數字，str:詞綴字串，index:第幾個區塊，
        public void affixDetermine(string str, int index, Dictionary<string, int> affix)
        {
            affix.Clear();
            //拆出詞綴區塊  
            string[] substr = SplitClipBoard(str, Pattern_enter);

            //將詞綴區塊逐條分開
            string[] substr2 = Regex.Split(substr[index], Pattern_enter);

            //分離詞綴&數字並放入字典，沒有數字的詞綴數字給0
            foreach (string i in substr2)
            {
                decimal r;
                string s = Regex.Match(i, "[0-9.]+([0-9]{0,3})").Value;
                if (!decimal.TryParse(s, out r))
                {
                    r = 0;
                }
                
                affix.Add(Regex.Replace(i, "[0-9.]+([0-9]{0,3})", "").Replace(" ", "").Replace("\n",""), (int)r);
            }
        }
        //分割剪貼簿
        public string[] SplitClipBoard(string clip,string pattern = Pattern_enter)
        {
            if (clip == null)
            {
                return null;
            }
            return  Regex.Split(clip, $"{pattern}--------{pattern}");
        }

        //檢查是否有符合的詞綴，將傳入字典key(詞綴部分)與當前詞綴取交集，有交集則比value是否符合，ref給前後綴符合數量
        public void affixCheck(Dictionary<string, int> affix, Dictionary<string, int> pre, Dictionary<string, int> suf, out int countpre, out int countsuf)
        {
            countpre = 0;
            countsuf = 0;
            List<string> strpre = new List<string>(pre.Keys);
            List<string> strsuf = new List<string>(suf.Keys);
            List<string> affixStr = new List<string>(affix.Keys);
            IEnumerable<string> interpre = strpre.Intersect(affixStr);
            IEnumerable<string> intersuf = strsuf.Intersect(affixStr);
            if (interpre.Count() > 0)
            {
                foreach (string i in interpre)
                {
                    if (affix[i] >= pre[i])
                    {
                        countpre += 1;
                    }
                }
            }
            if (intersuf.Count() > 0)
            {
                foreach (string i in intersuf)
                {
                    if (affix[i] >= suf[i])
                    {
                        countsuf += 1;
                    }
                }
            }
            if (countsuf > 0 && countpre > 0)
            {
                foreach (var i in affix.Keys)
                {
                    Console.WriteLine(i);
                }
            }
        }


        #region 棄用
        public void affixDetermine(string str, int index, List<string> affixStr, List<int> affixNum)
        {
            //string[] substr = str.Replace("\r\n--------\r\n", "$").Split('$');
            string[] substr = Regex.Split(str, "\r\n--------\r\n");
            //string[] substr2 = substr[substr.Length + index].Replace("\r\n", "$").Split('$');
            string[] substr2 = Regex.Split(substr[substr.Length + index], "\r\n");

            foreach (string i in substr2)
            {
                affixStr.Add(Regex.Replace(i, "[0-9.]{1,}", "").Replace(" ", ""));
                affixNum.Add(int.Parse(Regex.Match(i, "[0-9.]{1,}").Value));

                //Console.WriteLine(int.Parse(Regex.Match(i, "[0-9.]{1,}").Value));
            }
        }
        #endregion
    }
}

/*
稀有度: 稀有
暴風 護頸索
簡純護身符
--------
品質 (生命和魔力詞綴): +20% (augmented)
--------
需求:
等級: 60
--------
物品等級: 85
--------
配置 起源 (enchant)
--------
允許前綴 -1 (implicit)
允許後綴 -1 (implicit)
固定詞綴不能被改變 (implicit)
增加 25% 變動詞綴大小 (implicit)
--------
獲得等級 22 的清晰技能
增加 23% 閃避值
減少 7% 魔力保留
增加 25% 最大能量護盾 (crafted)
--------
塑者之物
救贖者物品
*/