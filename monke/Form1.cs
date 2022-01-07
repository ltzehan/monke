using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

            InitializeToolbarIcon();

            form2 = new();
            form2.Show();
        }

        private void InitializeToolbarIcon()
        {
            contextMenu = new ContextMenuStrip();
            menuItem = new ToolStripMenuItem();

            contextMenu.Items.Add(menuItem);

            menuItem.Text = "no more monke";
            menuItem.Click += new EventHandler(toolbarClickHandler);

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Properties.Resources.Icon;
            notifyIcon.Text = "monke time";
            notifyIcon.ContextMenuStrip = contextMenu;
            notifyIcon.Visible = false;
            notifyIcon.Click += new EventHandler(notifyIconClickHandler);
        }

        private void toolbarClickHandler(object sender, EventArgs e)
        {
            form2.Dispose();
            notifyIcon.Dispose();
            Environment.Exit(0);
        }

        private void notifyIconClickHandler(object sender, EventArgs e)
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

        private void OnFormLoad(object sender, EventArgs e)
        {   
            keyboardComboBox.DisplayMember = "DisplayName";
            keyboardComboBox.ValueMember = "Path";
            keyboardComboBox.DataSource = KeyboardModel.models;
            keyboardComboBox.SelectedIndex = 0;

            GlobalKeyboardEvents.Instance.KeyClickedEvents += OnKeyPress;
        }

        private void OnKeyPress(object? sender, KeypressEventArgs e)
        {
            KeySoundPath soundPath = e.KeyDown ? AssetSelector.Instance.PressSoundPath : AssetSelector.Instance.ReleaseSoundPath;
            Stream soundStream = e.Info.vkCode switch
            {
                VK_CODE_BACKSPACE => soundPath.Backspace,
                VK_CODE_ENTER => soundPath.Enter,
                VK_CODE_SPACE => soundPath.Space,
                _ => soundPath.Generic,
            };
            Stream streamCopy = new MemoryStream();
            soundStream.Seek(0, SeekOrigin.Begin);
            soundStream.CopyTo(streamCopy);
            streamCopy.Seek(0, SeekOrigin.Begin);
            StandaloneAudioPlayer.Instance.PlaySound(streamCopy);
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {   
            notifyIcon.Visible = true;
            e.Cancel = true;
            WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void OnKeyboardChange(object sender, EventArgs e)
        {
            AssetSelector.Keyboard = KeyboardModel.models[keyboardComboBox.SelectedIndex];
            Debug.WriteLine($"Keyboard selected: {keyboardComboBox.SelectedIndex}");
        }
    }
}
