using System.Diagnostics;
using System.Linq;
using System.Windows;
using CountdownScreensaver.HelperFunctions;

namespace CountdownScreensaver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        protected override void OnStartup(StartupEventArgs e)
        {

            if (Process.GetProcessesByName("LogonUI").Any())
            {
                NativeMethods.DisplayOff();
                Model.Quit();
            }
            
            base.OnStartup(e);

            if (e.Args.Length > 0)
            {
                switch (e.Args[0].ToLower())
                {
                    default:
                        {
                            Configuration();

                            break;
                        }
                    case "/s":
                        {
                            var screens = new WpfScreens();

                            
                            foreach (var screen in screens.AllScreens)
                            {
                                var left = screen.Bounds.Left;
                                var top = screen.Bounds.Top;

                                (new MainWindow { Top = top, Left = left }).Show();
                            }
                            break;

                        }
                    case "/p":
                        {
                            //do nothing. Preview is not needed.
                            Model.Quit();
                            break;
                        }
                }
            }
            else
            {
                Configuration();
            }

        }

        private static void Configuration()
        {
            MessageBox.Show("Configuration is not available in this version of screensaver.", 
                "CountdownScreensaver",
                MessageBoxButton.OK);
            Model.Quit();
        }
    }
}
