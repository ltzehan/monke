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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            BackColor = Color.White;
            TransparencyKey = Color.White;
            TopMost = true;
            ShowInTaskbar = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var bounds = Screen.FromControl(this).Bounds;
            Debug.WriteLine(bounds.Width + ", " + bounds.Height);
            Location = new Point(bounds.Width / 2 - Width / 2, bounds.Height - Height - 100);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
