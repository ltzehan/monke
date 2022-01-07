using monke;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testwinforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int i = 0;
            GlobalKeyboardEvents.Instance.KeyClickedEvents += (s, e) =>
            {
                this.textBox1.Text = i++ + " : " + e.Info.vkCode.ToString();
            };
        }
    }
}
