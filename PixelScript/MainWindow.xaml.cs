using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
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

            string[] colorArray = new string[pixels.Length / 4];
            int colorIndex = 0;
            for (int index = 0; index < pixels.Length; index += 4) {

                byte blue = pixels[index];
                byte green = pixels[index + 1];
                byte red = pixels[index + 2];

                byte alpha = pixels[index + 3];

                int color = red << 16 | green << 8 | blue;
                colorArray[colorIndex++] = color.ToString("X6");
            }

            var outArray = new List<string>();

            for (int i = 0; i < colorArray.Length; i++) {
                xpos = i % img.PixelWidth;
                ypos = i / img.PixelWidth;

                var outString = $@"<panel xpos=""{xpos * 10}"" ypos=""{ypos * 10}"" width=""10"" height=""10"" background=""#{colorArray[i]}""/>";
                outArray.Add(outString);
                Console.WriteLine(outString);
            }

            var outWindow = new ScriptOut(outArray);
            outWindow.Show();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }
}
