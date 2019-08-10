using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileName
{
    
    public partial class Form1 : Form
    {
        public List<FileData> m_datas = new List<FileData>();

        public Form1()
        {
            InitializeComponent();
        }

        private void LowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (FileData fd in m_datas)
            {
                fd.newname = fd.oldname.ToLower();
            }

            UpdateListBox();
        }


        private void UpdateListBox()
        {
            listBox1.Items.Clear();
            foreach (FileData fd in m_datas)
            {
                listBox1.Items.Add(fd.oldname);
            }

            listBox2.Items.Clear();
            foreach (FileData fd in m_datas)
            {
                listBox2.Items.Add(fd.newname);
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                m_datas.Clear();
                foreach (string name in openFileDialog1.FileNames)
                {
                    FileData fd = new FileData(name);
                    m_datas.Add(fd);
                }

                UpdateListBox();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (FileData fd in m_datas)
            {
                fd.Change();
            }

            UpdateListBox();
            MessageBox.Show("Done");
        }

        private void SubDelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

    public class FileData
    {
        public FileInfo m_fi;
        public string oldname;
        public string newname;

        public FileData(string filename)
        {
            m_fi = new FileInfo(filename);
            oldname = m_fi.Name;
            newname = oldname;
        }

        public void Change()
        {
            string dest = m_fi.DirectoryName + "\\" + newname;
            m_fi.MoveTo(dest);
            oldname = newname;
        }
    }

}
