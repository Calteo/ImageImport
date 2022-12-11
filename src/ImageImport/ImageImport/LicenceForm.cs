using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageImport
{
    public partial class LicenceForm : Form
    {
        public LicenceForm()
        {
            InitializeComponent();
        }

        private async void LicenceFormLoad(object sender, EventArgs e)
        {
            await webView.EnsureCoreWebView2Async();
            webView.NavigateToString(Properties.Resources.Licence);
        }
    }
}
