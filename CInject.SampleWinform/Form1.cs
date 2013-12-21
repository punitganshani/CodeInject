using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CInject.SampleWinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnChangeValue_Click(object sender, EventArgs e)
        {
            ChangeValue(txtInputValue);
        }

        private void ChangeValue(TextBox textValue)
        {
            lblValue.Text = textValue.Text;
        }
    }
}
