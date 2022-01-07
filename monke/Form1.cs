using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monke
{
    public partial class Form1 : Form
    {
        private Form2 form2;

        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItem;

        const uint VK_CODE_BACKSPACE = 8;
        const uint VK_CODE_ENTER = 13;
        const uint VK_CODE_SPACE = 32;

        public Form1()
        {
            InitializeComponent();
            
            Text = "monke";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = Properties.Resources.Icon;
            MaximizeBox = false;

            InitializeToolbarIcon();

            form2 = new();
            form2.Show();
            form2.Opacity = 0;
        }

        private void InitializeToolbarIcon()
        {
            contextMenu = new ContextMenuStrip();
            menuItem = new ToolStripMenuItem
            {
                Text = "no more monke"
            };
            menuItem.Click += new EventHandler(ToolbarClickHandler);
            contextMenu.Items.Add(menuItem);

            notifyIcon = new NotifyIcon
            {
                Icon = Properties.Resources.Icon,
                Text = "monke time",
                ContextMenuStrip = contextMenu,
                Visible = false
            };
            notifyIcon.Click += new EventHandler(NotifyIconClickHandler);
        }

        private void ToolbarClickHandler(object? sender, EventArgs e)
        {
            form2.Dispose();
            notifyIcon.Dispose();
            Environment.Exit(0);
        }

        private void NotifyIconClickHandler(object? sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Left && WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
                notifyIcon.Visible = false;

                TopMost = true;
                Activate();
            }
        }

        private Thread msgReader;

        // GetMessage
        [DllImport(@"user32.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool GetMessage(ref MSG message, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);
        [DllImport(@"user32.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool TranslateMessage(ref MSG message);
        [DllImport(@"user32.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern long DispatchMessage(ref MSG message);

        private const int WM_CUSTOM_1 = 0x0400 + 2001;

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            long x;
            long y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public UIntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public POINT pt;
        }
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostThreadMessage(uint threadId, uint msg, UIntPtr wParam, IntPtr lParam);

        private void OnFormLoad(object? sender, EventArgs e)
        {   
            keyboardComboBox.DisplayMember = "DisplayName";
            keyboardComboBox.ValueMember = "Path";
            keyboardComboBox.DataSource = KeyboardModel.models;
            keyboardComboBox.SelectedIndex = 0;

            var instance = GlobalKeyboardEvents.Instance;

            var dummy = new Form();
            var s = new Thread(() => {
                while (true)
                {
                    while (true)
                    {
                        while(instance.Events.IsEmpty) { }
                        if (instance.Events.TryDequeue(out var ev))
                        {
                        }
                    }
                }
            });
            s.Start();

            msgReader = new Thread(() =>
            {
                dummy.RightToLeftChanged += (ev, _) =>
                {
                    OnKeyPress(ev as KeypressEventArgs);
                };
                dummy.Width = 0;
                dummy.Width = 0;
                dummy.ShowInTaskbar = false;
                dummy.FormBorderStyle = FormBorderStyle.None;
                dummy.WindowState = FormWindowState.Minimized;
                Application.Run(dummy);

            });
            msgReader.Start();

        }

        private void OnKeyPress(KeypressEventArgs e)
        {
            KeySoundPath soundPath = e.KeyDown ? AssetSelector.Instance.PressSoundPath : AssetSelector.Instance.ReleaseSoundPath;
            Stream soundStream = e.Info.vkCode switch
            {
                VK_CODE_BACKSPACE => soundPath.Backspace,
                VK_CODE_ENTER => soundPath.Enter,
                VK_CODE_SPACE => soundPath.Space,
                _ => soundPath.Generic,
            };
            soundStream.Seek(0, SeekOrigin.Begin);
            StandaloneAudioPlayer.Instance.PlaySound(soundStream);
            // form2.TriggerShow();
        }

        private void OnFormClosing(object? sender, FormClosingEventArgs e)
        {   
            notifyIcon.Visible = true;
            e.Cancel = true;
            WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void OnKeyboardChange(object? sender, EventArgs e)
        {
            AssetSelector.Keyboard = KeyboardModel.models[keyboardComboBox.SelectedIndex];
            Debug.WriteLine($"Keyboard selected: {keyboardComboBox.SelectedIndex}");
        }
    }
}
