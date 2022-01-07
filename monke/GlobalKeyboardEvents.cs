using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections.Concurrent;

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
        private static GCHandle _gcHandle;
        private static GCHandle _gcHandle2;
        public static GlobalKeyboardEvents Instance = new GlobalKeyboardEvents();
        private readonly IntPtr _hookId;

        private Dictionary<uint, IntPtr> _prevKeyState = new Dictionary<uint, IntPtr>();

        private GlobalKeyboardEvents()
        {
            _hookProc = KeyClicked;
            _gcHandle = GCHandle.Alloc(_hookProc);
            _hookId = SetWindowsHookEx(HookType.WH_KEYBOARD_LL, _hookProc, IntPtr.Zero, 0);
            _events = new ConcurrentQueue<KeypressEventArgs>();
            _gcHandle2 = GCHandle.Alloc(_events);
        }

        private ConcurrentQueue<KeypressEventArgs> _events;

        public ConcurrentQueue<KeypressEventArgs> Events => _events;

        // TODO accept Fn key
        private IntPtr KeyClicked(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code == 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_KEYUP))
            {
                if (lParam != IntPtr.Zero)
                {
                    var lp = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);

                    var keyCode = lp!.vkCode;

                    // Check if a WM_KEYUP preceeds WM_KEYDOWN, otherwise event is from a key being held down and should not invoke callback
                    bool trigger = false;
                    if (!_prevKeyState.ContainsKey(keyCode) || _prevKeyState[keyCode] != wParam)
                    {
                        _prevKeyState[keyCode] = wParam;
                        trigger = true;
                    }

                    if (trigger && !lp.flags.HasFlag(KBDLLHOOKSTRUCTFlags.LLKHF_INJECTED)) // checks synthetic keycode
                    {
                        _events.Enqueue(new KeypressEventArgs(wParam == (IntPtr)WM_KEYDOWN, lp));
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
