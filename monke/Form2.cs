using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monke
{
    public partial class Form2 : Form
    {
        private Timer timer;

        public Form2()
        {
            InitializeComponent();
            BackColor = Color.Beige;
            TransparencyKey = Color.Beige;
            TopMost = true;
            ShowInTaskbar = false;
            timer = new Timer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = 50;
            timer.Start();
        }

        public void TimerTick (object? sender, EventArgs e)
        {
            Opacity *= 0.95;
        }

    public void TriggerShow()
        {
            Opacity = 1;
            // after 5 seconds hide
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var bounds = Screen.FromControl(this).Bounds;
            Debug.WriteLine(bounds.Width + ", " + bounds.Height);
            Location = new Point(bounds.Width / 2 - Width / 2, bounds.Height - Height - 100);
        }
    }
}
