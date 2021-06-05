using AutoCraft.ControlHandler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class Utilities
    {
        private const string Pattern_enter = "\r\n";

        //使用通貨，右鍵p1位置左鍵p2位置
        public void useCurrency(Point currencyPosition, Point craftArea)
        {
            int delay = 30;
            MouseAndKeyEvent.SetCursorPos(currencyPosition.X, currencyPosition.Y);
            Thread.Sleep(delay);
            rightClickCurrency(currencyPosition);
            MouseAndKeyEvent.SetCursorPos(craftArea.X, craftArea.Y);
            Thread.Sleep(delay);
            leftClickCraftArea(craftArea);
        }
        //連續使用通貨，stopMode 1=前綴, 2=後綴, 3=前或後, 4=前且後
        public Tuple<int,int> useCurrencyContinuously(Point currencyPosition, Point craftArea, int index, int idelay, int fireTimes, int stopPre, int stopSuf, Dictionary<string, int> pre, Dictionary<string, int> suf, ref bool stopFlag, int stopMode,ref int loopCounter)
        {
            //int loopCounter = 0;
            MouseAndKeyEvent.sendLShiftKeyDown();
            MouseAndKeyEvent.SetCursorPos(currencyPosition.X, currencyPosition.Y);
            Thread.Sleep(idelay);
            rightClickCurrency(currencyPosition);
            Thread.Sleep(idelay);
            MouseAndKeyEvent.SetCursorPos(craftArea.X, craftArea.Y);

            //判定詞綴一次

            Dictionary<string, int> clipAffix = new Dictionary<string, int>();
            Thread.Sleep(idelay);
            //Console.WriteLine(loopCounter);
            clearClipCoard();
            string cliptmp = getClipBoardLogic();
            //for (int i = 0; i < 3; i++)
            //{
            //    sendCtrlC();
            //    cliptmp = getClipBoard();
            //    if (cliptmp != "")
            //    {
            //        break;
            //    }
            //}
            if (cliptmp == "")
            {
                MessageBox.Show("剪貼簿取得錯誤");
                return null;
            }
            clipAffix = affixDetermine(cliptmp, index);

            Tuple<int, int> affixnum = new Tuple<int, int>(0,0);
            if (!checkAffix(clipAffix, pre, suf, 4, stopPre, stopSuf, out affixnum))
            {
                while (loopCounter < fireTimes && stopFlag)
                {
                    leftClickCraftArea(craftArea);
                    loopCounter++;

                    #region 剪貼簿取得&檢查
                    clipAffix.Clear();
                    Thread.Sleep(idelay);
                    //clearClipCoard();
                    cliptmp = getClipBoardLogic();
                    //for (int i = 0; i < 3; i++)
                    //{
                    //    sendCtrlC();
                    //    cliptmp = getClipBoard();
                    //    if (cliptmp != "")
                    //    {
                    //        break;
                    //    }
                    //}

                    if (cliptmp == "")
                    {
                        MessageBox.Show("剪貼簿取得錯誤");
                        break;
                    }
                    #endregion
                    clipAffix = affixDetermine(cliptmp, index);

                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine($"第{loopCounter}次");
                    foreach (var i in clipAffix)
                    {
                        Console.WriteLine(i.Key + ":" + i.Value);
                    }
                    Console.WriteLine("----------------------------------------------");
                    
                    
                    if (checkAffix(clipAffix, pre, suf, stopMode, stopPre, stopSuf,out affixnum))
                    {
                        MouseAndKeyEvent.sendLShiftKeyUp();
                        return affixnum;
                    }
                    //leftClickCraftArea(craftArea);
                }
            }
            MouseAndKeyEvent.sendLShiftKeyUp();
            return affixnum;
        }

        //詞綴是否符合條件判斷
        public bool checkAffix(Dictionary<string, int> clipAffix, Dictionary<string, int> pre, Dictionary<string, int> suf, int stopMode, int stopPre, int stopSuf)
        {
            int int_count_prefix = prefixCheck(clipAffix, pre);
            int int_count_suffix = suffixCheck(clipAffix, suf);
            bool pattern = true;
            switch (stopMode)
            {
                //case 1:
                //    pattern = int_count_prefix > 0;
                //    break;
                case 2://點到符合條件或可增幅
                    pattern = (int_count_prefix >= stopPre)|| int_count_suffix > 0;
                    break;
                //case 3:
                //    pattern = int_count_prefix > 0 || int_count_suffix >= 0;
                //    break;
                case 4://連點到符合條件
                    pattern = (int_count_prefix >= stopPre) /*&& (int_count_suffix >= stopSuf)*/;
                    break;
            }
            return pattern;
        }
        public bool checkAffix(Dictionary<string, int> clipAffix, Dictionary<string, int> pre, Dictionary<string, int> suf, int stopMode, int stopPre, int stopSuf, out Tuple<int, int> affixNum)
        {
            int int_count_prefix = prefixCheck(clipAffix, pre);
            int int_count_suffix = suffixCheck(clipAffix, suf);
            bool pattern = true;
            switch (stopMode)
            {
                //case 1:
                //    pattern = int_count_prefix > 0;
                //    break;
                case 2://點到符合條件或可增幅
                    pattern = (int_count_prefix >= stopPre) || int_count_suffix > 0;
                    break;
                //case 3:
                //    pattern = int_count_prefix > 0 || int_count_suffix >= 0;
                //    break;
                case 4://連點到符合條件
                    pattern = (int_count_prefix >= stopPre) /*&& (int_count_suffix >= stopSuf)*/;
                    break;
            }
            affixNum = new Tuple<int, int>(int_count_prefix, int_count_suffix);
            return pattern;
        }

        //右鍵點擊
        public void rightClickCurrency(Point currencyPosition)
        {
            int delay = 30;
            MouseAndKeyEvent.sendRightClickDown();
            Thread.Sleep(delay);
            MouseAndKeyEvent.sendRightClickUp();
            Thread.Sleep(delay);
        }
        //左鍵點擊
        public void leftClickCraftArea(Point craftArea)
        {
            int delay = 30;
            MouseAndKeyEvent.sendLeftClickDown();
            Thread.Sleep(delay);
            MouseAndKeyEvent.sendLeftClickUp();
            Thread.Sleep(delay);
        }

        //複製
        public void sendCtrlC()
        {
            int delay = 30;
            SendKeys.SendWait("^C");
            //MouseAndKeyEvent.sendLCtrlDown();
            //MouseAndKeyEvent.sendCDown();
            //MouseAndKeyEvent.sendCUp();
            //MouseAndKeyEvent.sendLCtrlUp();
        }


        //清空剪貼簿
        public void clearClipCoard()
        {
            Clipboard.Clear();
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

        //取得剪貼簿邏輯
        public string getClipBoardLogic()
        {
            clearClipCoard();
            string cliptmp = "";
            for (int i = 0; i < 3; i++)
            {
                sendCtrlC();
                cliptmp = getClipBoard();
                if (cliptmp != "")
                {
                    break;
                }
            }
            return cliptmp;
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

                affix.Add(Regex.Replace(i, "[0-9.]+([0-9]{0,3})", "").Replace(" ", "").Replace("\n", ""), (int)r);
            }
        }
        //詞綴拆解成字典
        public Dictionary<string, int> affixDetermine(string str, int index)
        {
            Dictionary<string, int> affix = new Dictionary<string, int>();
            //拆出詞綴區塊  
            string[] substr = SplitClipBoard(str, Pattern_enter);

            //將詞綴區塊逐條分開
            string[] substr2 = Regex.Split(substr[index], Pattern_enter);

            //分離詞綴&數字並放入字典，沒有數字的詞綴數字給0
            foreach (string i in substr2)
            {
                decimal outputValue;
                string value = Regex.Match(i, "[0-9.]+([0-9]{0,3})").Value;
                if (!decimal.TryParse(value, out outputValue))
                {
                    outputValue = 0;
                }
                string key = Regex.Replace(i, "[0-9.]+([0-9]{0,3})", "").Replace(" ", "").Replace("\n", "");
                if (String.IsNullOrEmpty(key))
                {
                    continue;
                }
                affix.Add(key, (int)outputValue);
            }
            return affix;
        }
        //拆出詞綴區塊
        public string[] SplitClipBoard(string clip, string pattern = Pattern_enter)
        {
            if (clip == null)
            {
                return null;
            }
            return Regex.Split(clip, $"{pattern}--------{pattern}");
        }
      
        //符合條件前綴數量檢查
        public int prefixCheck(Dictionary<string, int> affix, Dictionary<string, int> pre)
        {
            int countpre = 0;
            List<string> strpre = new List<string>(pre.Keys);
            List<string> affixStr = new List<string>(affix.Keys);
            var interpre = strpre.Intersect(affixStr);
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
            return countpre;
        }
        //符合條件後綴數量檢查
        public int suffixCheck(Dictionary<string, int> affix, Dictionary<string, int> suf)
        {
            int countsuf = 0;
            List<string> strsuf = new List<string>(suf.Keys);
            List<string> affixStr = new List<string>(affix.Keys);
            var interpre = strsuf.Intersect(affixStr);
            if (interpre.Count() > 0)
            {
                foreach (string i in interpre)
                {
                    if (affix[i] >= suf[i])
                    {
                        countsuf += 1;
                    }
                }
            }
            return countsuf;
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