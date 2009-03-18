using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Visualizer
{
    public partial class ChooseDataSourceForm : Form
    {
        public ChooseDataSourceForm()
        {
            InitializeComponent();
        }

        private void buttonLocalhost_Click(object sender, EventArgs e)
        {
            dataSource = DataSource.Localhost;
            cancel = false;
            Close();
        }

        private void buttonLogs_Click(object sender, EventArgs e)
        {
            dataSource = DataSource.Logs;
            cancel = false;
            Close();
        }

        internal DataSource DataSource { get { return dataSource; } }
        private DataSource dataSource;

        internal bool Cancel { get { return cancel; } }
        private bool cancel = true;
    }
}
