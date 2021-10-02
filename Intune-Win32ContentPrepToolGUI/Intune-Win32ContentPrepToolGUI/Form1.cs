using System;
using System.Windows.Forms;
using System.IO;

namespace Intune_Win32ContentPrepToolGUI
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// intune winapputil exe location
        /// </summary>
        public string intunewinapputil = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\IntuneWinAppUtil.exe";

        public Form1()
        {
            InitializeComponent();

            // disable start button, until user choose required folders/files
            button4.Enabled = false;

        }

        /// <summary>
        /// choose input folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = openFolderFunc();
            startButtonEnableChange();
        }

        /// <summary>
        /// choose installation file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = openFileFunc();
            startButtonEnableChange();
        }

        /// <summary>
        /// choose output folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = openFolderFunc();
            startButtonEnableChange();
        }

        /// <summary>
        /// start button, run the cmd command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            runIntuneWinAppUtil();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            startButtonEnableChange();

        }

        /// <summary>
        /// open file dialog
        /// </summary>
        /// <returns></returns>
        public string openFileFunc()
        {
            OpenFileDialog o = new System.Windows.Forms.OpenFileDialog();
            o.Multiselect = false;
            o.ShowDialog();

            return o.FileName;
        }

        /// <summary>
        /// open folder dialog
        /// </summary>
        /// <returns></returns>
        public string openFolderFunc()
        {
            FolderBrowserDialog o = new System.Windows.Forms.FolderBrowserDialog();
            o.ShowDialog();

            return o.SelectedPath;
        }

        /// <summary>
        /// enable or disable start button
        /// </summary>
        public void startButtonEnableChange()
        {
            // check if textboxes are not empty and the selected folders/files exist
            if (!string.IsNullOrEmpty(textBox1.Text) && 
                !string.IsNullOrEmpty(textBox2.Text) && 
                !string.IsNullOrEmpty(textBox2.Text))
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }
        
        /// <summary>
        /// run command
        /// </summary>
        public void runIntuneWinAppUtil()
        {
            if (!File.Exists(intunewinapputil))
            {
                MessageBox.Show("IntuneWinAppUtil.exe not found in the directory: " + Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
            }
            else
            {
                string cmd = string.Format("/C {0} -c {1} -s {2} -o {3} -q", intunewinapputil, textBox1.Text, textBox2.Text, textBox3.Text);
                System.Diagnostics.Process.Start("CMD.exe", cmd);
            }
        }


    }
}
