using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace GammaControl
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern bool SetDeviceGammaRamp(IntPtr hDc, ref RAMP lpRamp);

        public static void SetGamma(double gamma)
        {
            var ramp = new RAMP {Red = new ushort[256], Green = new ushort[256], Blue = new ushort[256]};

            for (var i = 1; i < 256; i++)
                ramp.Red[i] = ramp.Blue[i] = ramp.Green[i] = (ushort) (i * (gamma * 256d));

            SetDeviceGammaRamp(GetDC(IntPtr.Zero), ref ramp);
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetGamma(Slider.Value / Slider.Maximum);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct RAMP
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Red;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Green;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public ushort[] Blue;
        }
    }
}