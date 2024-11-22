using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class StorageOut : Form
    {
        public StorageOut()
        {
            InitializeComponent();
            this.Text = "SplitContainer Example";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void StorageOut_Load(object sender, EventArgs e)
        {

        }
    }
}
