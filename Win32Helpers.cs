using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyAddin
{
    // Edit control messages
    enum Edit : uint
    {
        ECM_FIRST = 0x1500,
        EM_SETCUEBANNER = ECM_FIRST + 1,    // Set the cue banner with the lParm = LPCWSTR
        EM_GETCUEBANNER = ECM_FIRST + 2     // Get the cue banner with the wParm = LPWSTR, lParm = buffer length
    }

    class User32
    {
        [DllImport("User32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, UInt32 wParam, UInt32 lParam);

        //[DllImport("User32.dll", CharSet = CharSet.Unicode)]
        //public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, UInt32 wParam, ref HDITEM lParam);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, UInt32 wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

    }

    class EditControl
    {
        public static void SetCueBannerText(Control ctrl, string text)
        {
            User32.SendMessage(ctrl.Handle, (uint)Edit.EM_SETCUEBANNER, 0, text);
        }
    }
}
