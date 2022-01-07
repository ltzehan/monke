using NAudio.Wave;
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
    public partial class SettingsForm : Form
    {
        private SubtitleForm subtitleForm;

        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItem;

        const uint VK_CODE_BACKSPACE = 8;
        const uint VK_CODE_ENTER = 13;
        const uint VK_CODE_SPACE = 32;

        public SettingsForm()
        {
            InitializeComponent();
            
            Text = "monke";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = Properties.Resources.Icon;
            MaximizeBox = false;

            InitializeToolbarIcon();

            subtitleForm = new();
            subtitleForm.Show();
            subtitleForm.Opacity = 0;
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
            subtitleForm.Dispose();
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

        private void OnFormLoad(object? sender, EventArgs e)
        {   
            keyboardComboBox.DisplayMember = "DisplayName";
            keyboardComboBox.ValueMember = "Path";
            keyboardComboBox.DataSource = KeyboardModel.models;
            keyboardComboBox.SelectedIndex = 0;

            subtitlesCheckbox.Checked = true;

            GlobalKeyboardEvents.Instance.KeyClickedEvents += OnKeyPress;
        }

        private void OnKeyPress(object? sender, KeypressEventArgs e)
        {
            KeySoundProvider soundProvider = e.KeyDown ? AssetSelector.Instance.PressSoundProvider : AssetSelector.Instance.ReleaseSoundProvider;
            WaveStream keySound = e.Info.vkCode switch
            {
                VK_CODE_BACKSPACE => soundProvider.Backspace,
                VK_CODE_ENTER => soundProvider.Enter,
                VK_CODE_SPACE => soundProvider.Space,
                _ => soundProvider.Generic,
            };
            
            StandaloneAudioPlayer.Instance.PlaySound(keySound);

            if (subtitlesCheckbox.Checked) subtitleForm.TriggerShow();
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
        }

    }
}
