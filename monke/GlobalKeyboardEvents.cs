using System;
using System.Runtime.InteropServices;

namespace monke
{
    public class KeypressEventArgs : EventArgs
    {
        public KeypressEventArgs(bool keyDown, GlobalKeyboardEvents.KBDLLHOOKSTRUCT info)
        {
            Info = info;
            KeyDown = keyDown;
        }

        public GlobalKeyboardEvents.KBDLLHOOKSTRUCT Info { get; }
        public bool KeyDown { get; }
    }

    public class GlobalKeyboardEvents
    {
        public static HookProc _hookProc;
        public static GlobalKeyboardEvents Instance = new GlobalKeyboardEvents();
        private readonly IntPtr _hookId;

        private GlobalKeyboardEvents()
        {
            _hookProc = KeyClicked;
            _hookId = SetWindowsHookEx(HookType.WH_KEYBOARD_LL, _hookProc, IntPtr.Zero, 0);
        }

        public event EventHandler<KeypressEventArgs> KeyClickedEvents;

        // TODO accept Alt key
        private IntPtr KeyClicked(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code == 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_KEYUP))
            {
                if (lParam != IntPtr.Zero)
                {
                    var lp = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
                    if (!lp.flags.HasFlag(KBDLLHOOKSTRUCTFlags.LLKHF_INJECTED)) // checks synthetic keycode
                    {
                        KeyClickedEvents(this, new KeypressEventArgs(wParam == (IntPtr)WM_KEYDOWN, lp));
                    }
                }
            }
            return CallNextHookEx(_hookId.ToInt32(), code, wParam, lParam);
        }

        const int WM_KEYDOWN = 0x0100;
        const int WM_KEYUP = 0x0101;

        public enum HookType
        {
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK = 1,
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(int hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public class KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public KBDLLHOOKSTRUCTFlags flags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [Flags]
        public enum KBDLLHOOKSTRUCTFlags : uint
        {
            LLKHF_EXTENDED = 0x01,
            LLKHF_INJECTED = 0x10,
            LLKHF_ALTDOWN = 0x20,
            LLKHF_UP = 0x80,
        }

        public delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);
    }
}
