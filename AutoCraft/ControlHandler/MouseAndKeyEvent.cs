using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoCraft.ControlHandler
{
    public static class MouseAndKeyEvent
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
        //const uint KEYEVENTF_KEYUP = 0x0002;
        //const uint KEYEVENTF_KEYDOWN = 0x0001;
        const uint KEYEVENTF_KEYUP = 2;
        const uint KEYEVENTF_KEYDOWN = 0;
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
        //按住LShift
        public static void sendLShiftKeyDown()
        {
            keybd_event((int)Keys.LShiftKey, 0, KEYEVENTF_KEYDOWN, 0); //160=LShift
        }
        //放開LShift
        public static void sendLShiftKeyUp()
        {
            keybd_event((int)Keys.LShiftKey, 0, KEYEVENTF_KEYUP, 0); //160=LShift
        }
        //左鍵按下
        public static void sendLeftClickDown()
        {
            mouse_event((int)(MouseEventTFlags.LEFTDOWN), 0, 0, 0, 0);
        }
        //左鍵放開
        public static void sendLeftClickUp()
        {
            mouse_event((int)(MouseEventTFlags.LEFTUP), 0, 0, 0, 0);
        }
        //右鍵按下
        public static void sendRightClickDown()
        {
            mouse_event((int)(MouseEventTFlags.RIGHTDOWN), 0, 0, 0, 0);
        }
        //右鍵放開
        public static void sendRightClickUp()
        {
            mouse_event((int)MouseEventTFlags.RIGHTUP, 0, 0, 0, 0);
        }
        //左Ctrl按下
        public static void sendLCtrlDown()
        {
            //keybd_event((int)Keys.LControlKey, 0, KEYEVENTF_KEYDOWN | 0, 0);
            keybd_event((int)Keys.LControlKey, 0, 0, 0);
            
        }
        //左Ctrl放開
        public static void sendLCtrlUp()
        {
            keybd_event((int)Keys.LControlKey, 0, 2, 0);
        }
        //C按下
        public static void sendCDown()
        {
            keybd_event((int)Keys.C, 0, 0, 0);
        }
        //C放開
        public static void sendCUp()
        {
            keybd_event((int)Keys.C, 0, 2, 0);
        }

        //取得視窗內相對座標
        public static void getRelativePoint(IntPtr hwnd, out Point relP)
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
        public static void getAbsolutePoint(IntPtr hwnd, Point p1, out Point p2)
        {
            p2 = new Point();
            RECT rc;
            GetWindowRect(hwnd, out rc);
            Rectangle rect = new Rectangle(rc.left, rc.top, rc.right, rc.bottom);
            p2.X = p1.X + rc.left;
            p2.Y = p1.Y + rc.top;
        }
        //清空剪貼簿
        public static void clearClipCoard()
        {
            Clipboard.Clear();
        }
        //取剪貼簿
        public static string getClipBoard()
        {
            if (Clipboard.ContainsText())
            {
                return Clipboard.GetText();
            }
            return "";
        }
    }
}
