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
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }
        public int totalCount = 0;
        private void ProgressForm_Load(object sender, EventArgs e)
        {
            progressBar1.Maximum = totalCount;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
        }

        public void Plus()
        {
            progressBar1.PerformStep();
        }
    }
}
