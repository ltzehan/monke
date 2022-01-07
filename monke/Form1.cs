using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

    }
}
