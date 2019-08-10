using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileName
{
    public partial class StringForm : Form
    {
        public StringForm()
        {
            InitializeComponent();
        }

        public void Init(string text)
        {
            textBox1.Text = text;
        }

        public string Get()
        {
            return textBox1.Text;
        }
    }
}
