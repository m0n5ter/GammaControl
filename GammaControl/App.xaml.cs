using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;

namespace GammaControl
{
    public partial class App
    {
        private NotifyIcon _icon;

        protected override void OnStartup(StartupEventArgs e)
        {
            _icon = new NotifyIcon
            {
                Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                Text = "Gamma Control v.1.0.\nClick to show/hide",
                Visible = true
            };

            _icon.Click += (sender, args) =>
            {
                if (MainWindow == null)
                    return;

                if (MainWindow.IsVisible)
                    MainWindow.Hide();
                else
                    MainWindow.Show();
            };
        }
    }
}
