using System;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace Hallo
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
                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                {
                    synth.Speak("Your screen is locked. You cannot get out");
                }
            }
        }
    }
}
