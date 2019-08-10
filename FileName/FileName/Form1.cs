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
        public StringForm stringForm1;

        public Form1()
        {
            InitializeComponent();
        }

        private void UpdateListBox()
        {
            listView1.Clear();
            listView1.Columns.Add("oldname", 400);
            listView1.Columns.Add("newname", 400);

            foreach (FileData fd in m_datas)
            {
                string[] arr = new string[2];
                arr[0] = fd.oldname;
                arr[1] = fd.newname;
                ListViewItem lvi = new ListViewItem(arr);

                if (fd.changed)
                {
                    lvi.BackColor = Color.Aquamarine;
                }

                listView1.Items.Add(lvi);
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
            stringForm1 = new StringForm();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgressForm pf = new ProgressForm();
            pf.Show(this);
            
            foreach (FileData fd in m_datas)
            {
                fd.Save();
                pf.Plus();
            }

            UpdateListBox();

            pf.Dispose();
            MessageBox.Show("Done");
        }

        private void LowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (FileData fd in m_datas)
            {
                fd.Lower();
            }

            UpdateListBox();
        }

        private void SubDelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sel = "";
            if (listView1.SelectedItems.Count > 0)
            {
                sel = listView1.SelectedItems[0].SubItems[0].Text;
            }

            stringForm1.Set(sel);
            if (stringForm1.ShowDialog(this) == DialogResult.OK)
            {
                string subDel = stringForm1.Get();
                if (subDel.Length > 0)
                {
                    foreach (FileData fd in m_datas)
                    {
                        fd.SubDel(subDel);
                    }

                    UpdateListBox();
                }
            }
        }

        private void Trim4digitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (FileData fd in m_datas)
            {
                fd.Trim4digit();
            }

            UpdateListBox();
        }

        private void RevertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                int index = lvi.Index;
                FileData fd = m_datas[index];
                fd.Revert();
            }

            UpdateListBox();
        }

        private void FixoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
                return;

            stringForm1.Set(listView1.SelectedItems[0].SubItems[1].Text);
            if (stringForm1.ShowDialog() == DialogResult.OK)
            {
                string res = stringForm1.Get();
                ListViewItem lvi = listView1.SelectedItems[0];
                int index = lvi.Index;
                m_datas[index].Fix(res);

                UpdateListBox();
            }
        }
    }

    public class FileData
    {
        public FileInfo m_fi;
        public string oldname;
        public string newname;

        public bool changed = false;

        public FileData(string filename)
        {
            m_fi = new FileInfo(filename);
            oldname = m_fi.Name;
            newname = oldname;
        }

        public void Save()
        {
            if (changed)
            {
                try
                {
                    string dest = m_fi.DirectoryName + "\\" + newname;
                    m_fi.MoveTo(dest);
                    oldname = newname;
                    changed = false;
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public void Lower()
        {
            newname = newname.ToLower();
        }

        public void SubDel(string subDel)
        {
            if (subDel.Length > 0)
            {
                string temp = newname;
                newname = newname.Replace(subDel, "");
                if (temp != newname)
                {
                    changed = true;
                }
            }
        }

        public void Trim4digit()
        {
            bool result = true;

            for (int i = 0; i < 4; i++)
            {
                result &= Char.IsDigit(newname[i]);
            }

            if (result == false)
                return;

            string temp = newname;
            newname = newname.Remove(0, 4);
            if (temp != newname)
            {
                changed = true;
            }
        }

        public void Revert()
        {
            newname = oldname;
            changed = false;
        }

        public void Fix(string fix)
        {
            newname = fix;
            changed = true;
        }
    }

}
