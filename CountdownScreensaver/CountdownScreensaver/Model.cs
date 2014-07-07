using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using CountdownScreensaver.HelperFunctions;

namespace CountdownScreensaver
{
    public class Model : INotifyPropertyChanged
    {
        private Visibility _expired = Visibility.Visible;
        private string _message;

        public Visibility Expired
        {
            get { return _expired; }
            set
            {
                if (value == _expired) return;
                _expired = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Expired"));
            }
        }

        public Brush Foreground
        {
            get
            {
                return new SolidColorBrush(Settings.Default.Foreground);
            }
        }

        public Brush Background
        {
            get
            {
                return new SolidColorBrush(Settings.Default.Background);
            }
        }

        
        public string Message
        {
            get { return _message; }
            set
            {
                if (value == _message) return;
                _message = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Message"));
            }
        }

        public Model()
        {
            int timeout = Settings.Default.UseGracePeriod ? GetGracePeriod() : Settings.Default.Timeout;

            var screensaver = new Screensaver(new Counter(timeout), new Translator(CultureInfo.CurrentUICulture.ThreeLetterISOLanguageName));

            screensaver.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Message")
                {
                    Message = ((Screensaver)sender).Message;  
                }

                if (args.PropertyName != "Expired") return;
                Expired = ((Screensaver)sender).Expired ? Visibility.Hidden : Visibility.Visible;
                if (!Settings.Default.LockOnTimeout) return;
                NativeMethods.LockWorkStation();
                //if you dont sleep here Desktop will be shown for split second before system handles the locking
                Thread.Sleep(1000);
                Quit();
            };
            Message = screensaver.Message;

            GC.KeepAlive(screensaver);
        }
        


        private int GetGracePeriod()
        {
            const string path = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
            const string property = "ScreenSaverGracePeriod";

            var graceperiod = Registry.GetValue(path, property, 0).ToString();
            
            int result;
            int.TryParse(graceperiod, out result);
            return result;
        }


        public static void Quit()
        {
            Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

