namespace Peasant.Desktop
{
    public partial class ControlPanel : Form
    {
        public ControlPanel()
        {
            InitializeComponent();
        }

        private void startService_Click(object sender, EventArgs e)
        {
            Program.StartService();
        }

        private void stopService_Click(object sender, EventArgs e)
        {
            Program.StopService();
        }
    }
}