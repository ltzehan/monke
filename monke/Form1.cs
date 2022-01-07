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
        public Form1()
        {
            InitializeComponent();

            Form2 form2 = new();
            form2.Show();
        }

        private void FormLoad(object sender, EventArgs e)
        {   
            keyboardComboBox.DisplayMember = "DisplayName";
            keyboardComboBox.ValueMember = "Path";
            keyboardComboBox.DataSource = KeyboardModel.models;
            keyboardComboBox.SelectedIndex = 0;

            GlobalKeyboardEvents.Instance.KeyClickedEvents += OnKeyPress;
        }

        private void OnKeyPress(object? sender, KeypressEventArgs e)
        {
            Debug.WriteLine("on key press");
            var press = e.KeyDown ? AssetSelector.Instance.PressSoundPath.Generic : AssetSelector.Instance.ReleaseSoundPath.Generic;
            var mem = new MemoryStream();
            press.Seek(0, SeekOrigin.Begin);
            press.CopyTo(mem);
            mem.Seek(0, SeekOrigin.Begin);
            StandaloneAudioPlayer.Instance.PlaySound(mem);
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
