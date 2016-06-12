using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileMap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateGraphics().Clear(Color.White);
            CreateGraphics().DrawLine(new Pen(Color.Black), 10, 50, (float)numericUpDown1.Value + 10, 50);
            CreateGraphics().DrawLine(new Pen(Color.Black), 10, 50, 10, (float)numericUpDown2.Value + 50);
            CreateGraphics().DrawLine(new Pen(Color.Black), (float)numericUpDown1.Value + 10, 50, (float)numericUpDown1.Value + 10, (float)numericUpDown2.Value + 50);
            CreateGraphics().DrawLine(new Pen(Color.Black), 10, (float)numericUpDown2.Value + 50, (float)numericUpDown1.Value + 10, (float)numericUpDown2.Value + 50);
        }
    }
}
