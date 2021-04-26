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
        private const string Pattern_enter = "\r\n";

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

        //使用通貨，右鍵p1位置左鍵p2位置
        public void useCurrency(Point p1, Point p2)
        {
            SetCursorPos(p1.X, p1.Y);
            Thread.Sleep(40);
            mouse_event((int)(MouseEventTFlags.RIGHTDOWN), 0, 0, 0, 0);
            Thread.Sleep(40);
            mouse_event((int)MouseEventTFlags.RIGHTUP, 0, 0, 0, 0);
            Thread.Sleep(40);
            SetCursorPos(p2.X, p2.Y);
            Thread.Sleep(40);
            mouse_event((int)(MouseEventTFlags.LEFTDOWN), 0, 0, 0, 0);
            Thread.Sleep(40);
            mouse_event((int)MouseEventTFlags.LEFTUP, 0, 0, 0, 0);
            Thread.Sleep(40);
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
            //string[] substr = str.Replace("\r\n--------\r\n", "$").Split('$');            
            string[] substr = Regex.Split(str, $"{Pattern_enter}--------{Pattern_enter}");

            //將詞綴區塊逐條分開
            //string[] substr2 = substr[substr.Length + index].Replace("\r\n", "$").Split('$');            
            string[] substr2 = Regex.Split(substr[substr.Length + index], Pattern_enter);

            //分離詞綴&數字並放入字典，沒有數字的詞綴數字給0
            foreach (string i in substr2)
            {
                int r;
                if (!int.TryParse(Regex.Match(i, "[0-9.]{1,}").Value, out r))
                {
                    r = 0;
                }
                affix.Add(Regex.Replace(i, "[0-9.]{1,}", "").Replace(" ", ""), r);
            }
        }
        //List+class版
        public void affixDetermine(string str, int index, List<Affix> affix)
        {
            affix.Clear();
            //拆出詞綴區塊
            //string[] substr = str.Replace("\r\n--------\r\n", "$").Split('$');            
            string[] substr = Regex.Split(str, $"{Pattern_enter}--------{Pattern_enter}");

            //將詞綴區塊逐條分開
            //string[] substr2 = substr[substr.Length + index].Replace("\r\n", "$").Split('$');            
            string[] substr2 = Regex.Split(substr[substr.Length + index], Pattern_enter);

            //分離詞綴&數字並放入字典，沒有數字的詞綴數字給0
            foreach (string i in substr2)
            {
                int r;
                if (!int.TryParse(Regex.Match(i, "[0-9.]{1,}").Value, out r))
                {
                    r = 0;
                }
                affix.Add(new Affix {AffixName= Regex.Replace(i, "[0-9.]{1,}", "").Replace(" ", ""),AffixMin= r });
            }
        }

        //檢查是否有符合的詞綴，將傳入字典key(詞綴部分)與當前詞綴取交集，有交集則比value是否符合，ref給前後綴符合數量
        public void affixCheck(Dictionary<string, int> affix, Dictionary<string, int> pre, Dictionary<string, int> suf, ref int countpre, ref int countsuf)
        {
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
                foreach (string i in intersuf)
                {
                    if (affix[i] >= suf[i])
                    {
                        countsuf += 1;
                    }
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