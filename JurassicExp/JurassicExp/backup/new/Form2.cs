using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JurassicExp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            KeyboardHook(this, e);
        }
        private void KeyboardHook(Form2 form2, EventArgs e)
        {
            for (int i = 1; i > 0; i++)
            {
               
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
