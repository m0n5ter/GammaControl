using System.Runtime.InteropServices;
using System.Windows;

namespace GammaControl
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MagInitialize();
		}

        public static void SetGamma(float gamma)
        {
            var effect = new ColorEffect();

            effect.SetMatrix(new[,]
            {
                {gamma, 0f, 0f, 0f, 0.0f},
                {0f, gamma, 0f, 0f, 0f},
                {0f, 0f, gamma, 0f, 0f},
                {0f, 0f, 0f, 1f, 0f},
                {0.0f, 0f, 0f, 0f, 1f}
            });

            SetMagnificationDesktopColorEffect(ref effect);
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetGamma((float) (Slider.Value / Slider.Maximum));
        }

        [DllImport("Magnification.dll", CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagInitialize();

		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetMagnificationDesktopColorEffect(ref ColorEffect pEffect);

		[StructLayout(LayoutKind.Sequential)]
        public struct ColorEffect
		{
			public float transform00;
			public float transform01;
			public float transform02;
			public float transform03;
			public float transform04;
			public float transform10;
			public float transform11;
			public float transform12;
			public float transform13;
			public float transform14;
			public float transform20;
			public float transform21;
			public float transform22;
			public float transform23;
			public float transform24;
			public float transform30;
			public float transform31;
			public float transform32;
			public float transform33;
			public float transform34;
			public float transform40;
			public float transform41;
			public float transform42;
			public float transform43;
			public float transform44;

			public void SetMatrix(float[,] matrix)
			{
				transform00 = matrix[0, 0];
				transform10 = matrix[1, 0];
				transform20 = matrix[2, 0];
				transform30 = matrix[3, 0];
				transform40 = matrix[4, 0];
				transform01 = matrix[0, 1];
				transform11 = matrix[1, 1];
				transform21 = matrix[2, 1];
				transform31 = matrix[3, 1];
				transform41 = matrix[4, 1];
				transform02 = matrix[0, 2];
				transform12 = matrix[1, 2];
				transform22 = matrix[2, 2];
				transform32 = matrix[3, 2];
				transform42 = matrix[4, 2];
				transform03 = matrix[0, 3];
				transform13 = matrix[1, 3];
				transform23 = matrix[2, 3];
				transform33 = matrix[3, 3];
				transform43 = matrix[4, 3];
				transform04 = matrix[0, 4];
				transform14 = matrix[1, 4];
				transform24 = matrix[2, 4];
				transform34 = matrix[3, 4];
				transform44 = matrix[4, 4];
			}
		}
	}
}