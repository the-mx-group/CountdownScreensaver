using System;
using System.Windows;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;


namespace CountdownScreensaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Point? _previousMousePosition;

        public MainWindow()
        {
            InitializeComponent();
        }

        private bool TestMousePosition(MouseEventArgs e)
        {
            Point currentMousePosition = e.GetPosition(this);
            if (_previousMousePosition == null)
            {
                _previousMousePosition = currentMousePosition;
            }

            const int threshold = 5;
            if (Math.Abs(((Point)_previousMousePosition).X - currentMousePosition.X) > threshold ||
                Math.Abs(((Point)_previousMousePosition).Y - currentMousePosition.Y) > threshold)
            {
                return true;
            }

            _previousMousePosition = e.GetPosition(this);
            return false;
        }
        private void MainWindow_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Model.Quit();
        }
        private void MainWindow_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (TestMousePosition(e))
            {
                Model.Quit();
            }
        }
        private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            Model.Quit();
        }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //maximize the window here when it is already moved on the correct screen (if there are multiple screens)
            ((Window)sender).WindowState = WindowState.Maximized;
        }
    }
}
