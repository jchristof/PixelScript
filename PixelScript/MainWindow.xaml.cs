using System;

using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace PixelScript {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();     
        }

        private BitmapSource img;
        private void Select_OnClick(object sender, RoutedEventArgs e) {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == false)
                return;

            img = new BitmapImage(new Uri(openFileDialog.FileNames[0]));
            Image.Source = img;
        }

        private void Process_OnClick(object sender, RoutedEventArgs e) {
            if (img == null)
                return;

            int stride = img.PixelWidth * 4;
            int size = img.PixelHeight * stride;
            byte[] pixels = new byte[size];
            img.CopyPixels(pixels, stride, 0);

            int xpos = 0;
            int ypos = 0;

            string[] colorAray = new string[pixels.Length / 4];
            int colorIndex = 0;
            for (int index = 0; index < pixels.Length; index += 4) {

                byte blue = pixels[index];
                byte green = pixels[index + 1];
                byte red = pixels[index + 2];

                byte alpha = pixels[index + 3];

                int color = red << 16 | green << 8 | blue;
                colorAray[colorIndex++] = color.ToString("X6");
            }

            for (int i = 0; i < colorAray.Length; i++) {
                xpos = i % img.PixelWidth;
                ypos = i / img.PixelWidth;

                colorAray[i] = $@"<panel xpos=""{xpos * 10}"" ypos=""{ypos * 10}"" width=""10"" height=""10"" background=""#{colorAray[i]}""/>";
                Console.WriteLine(colorAray[i]);
            }
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }
}
