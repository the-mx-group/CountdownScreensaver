using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace CountdownScreensaver
{
    public class WpfScreens
    {
        public readonly IList<WpfScreen> AllScreens = new List<WpfScreen>();
        public WpfScreens()
        {
           var dpiY = ConvertToIndependent(Screen.PrimaryScreen.Bounds.Height, (float)SystemParameters.PrimaryScreenHeight);
            var dpiX = ConvertToIndependent(Screen.PrimaryScreen.Bounds.Width,
                (float)SystemParameters.PrimaryScreenWidth);

            foreach (var currentScreen in Screen.AllScreens)
            {
                var x = ConvertToIndependent(currentScreen.Bounds.X, dpiX);
                var width = ConvertToIndependent(currentScreen.Bounds.Width, dpiX);

                var y = ConvertToIndependent(currentScreen.Bounds.Y, dpiY);
                var height = ConvertToIndependent(currentScreen.Bounds.Height, dpiY);


                AllScreens.Add(new WpfScreen(new Rectangle(x, y, width, height)));
            }

        }

        private static int ConvertToIndependent(float value, float dpi)
        {
            return (int)((value / dpi) * 96.0F);
        }


    }

    public class WpfScreen
    {
        public Rectangle Bounds { get; private set; }

        public WpfScreen(Rectangle bounds)
        {
            if (bounds == null)
            {
                throw new ArgumentNullException("bounds");
            }

            Bounds = bounds;

        }
    }
}
