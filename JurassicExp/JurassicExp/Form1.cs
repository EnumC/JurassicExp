using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace JurassicExp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RegisterInStartup(true);
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
            Form2 frm = new Form2();
            frm.Show();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
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
