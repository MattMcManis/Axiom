using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Axiom.Troubleshooter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Axiom UI");

            if (!Directory.Exists(path))
            {
                MessageBox.Show("Directory does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var p = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            };
            Process.Start(p);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Axiom UI", "axiom.conf");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Config file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(filePath, "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Delete(filePath);
            }
        }
    }
}
