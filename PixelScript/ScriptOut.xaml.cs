
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace PixelScript {
    /// <summary>
    /// Interaction logic for ScriptOut.xaml
    /// </summary>
    public partial class ScriptOut : Window {
        public ScriptOut(List<string> outList) {
            InitializeComponent();
            OutText.Text = String.Join("\n", outList);
        }
    }
}
