using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
        }

        private void keyboardChange(object sender, EventArgs e)
        {
            Debug.WriteLine($"Keyboard selected: {keyboardComboBox.SelectedIndex}");
        }

        private void submitKeyboard(object sender, EventArgs e)
        {
            AssetSelector.Instance.Keyboard = KeyboardModel.models[keyboardComboBox.SelectedIndex];
            Debug.WriteLine($"Updated keyboard: {keyboardComboBox.SelectedIndex}");
        }
    }
}
