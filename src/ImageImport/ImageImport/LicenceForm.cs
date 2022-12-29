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
