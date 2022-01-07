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

        public Form1()
        {
            InitializeComponent();
            
            Text = "monke";
            FormBorderStyle = FormBorderStyle.FixedDialog;

            setupToolbarIcon();

            form2 = new();
            form2.Show();
        }
        private void setupToolbarIcon()
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

        private void onFormLoad(object sender, EventArgs e)
        {   
            keyboardComboBox.DisplayMember = "DisplayName";
            keyboardComboBox.ValueMember = "Path";
            keyboardComboBox.DataSource = KeyboardModel.models;
            keyboardComboBox.SelectedIndex = 0;

            GlobalKeyboardEvents.Instance.KeyClickedEvents += OnKeyPress;
        }

        private void OnKeyPress(object? sender, KeypressEventArgs e)
        {
            var press = e.KeyDown ? AssetSelector.Instance.PressSoundPath.Generic : AssetSelector.Instance.ReleaseSoundPath.Generic;
            var mem = new MemoryStream();
            press.Seek(0, SeekOrigin.Begin);
            press.CopyTo(mem);
            mem.Seek(0, SeekOrigin.Begin);

            StandaloneAudioPlayer.Instance.PlaySound(mem);
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {   
            notifyIcon.Visible = true;
            e.Cancel = true;
            WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void keyboardChange(object sender, EventArgs e)
        {
            Debug.WriteLine($"Keyboard selected: {keyboardComboBox.SelectedIndex}");
        }

        private void submitKeyboard(object sender, EventArgs e)
        {
            AssetSelector.Keyboard = KeyboardModel.models[keyboardComboBox.SelectedIndex];
            Debug.WriteLine($"Updated keyboard: {keyboardComboBox.SelectedIndex}");
        }
    }
}
