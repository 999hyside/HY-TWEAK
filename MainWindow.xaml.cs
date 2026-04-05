using System.Windows;
using HY_TWEAK.Core;

namespace HY_TWEAK
{
    public partial class MainWindow : Window
    {
        private HardwareDetector _hardwareDetector;

        public MainWindow()
        {
            InitializeComponent();
            _hardwareDetector = new HardwareDetector();
            LoadSystemInfo();
        }

        private void LoadSystemInfo()
        {
            var info = _hardwareDetector.GetSystemInfo();
            SystemInfoText.Text = $"CPU: {info.CPUName}\nGPU: {info.GPUName}\nGPU Vendor: {info.GPUVendor}\nRAM: {info.TotalRAM} GB\nOS: {info.OSInfo}";
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tweaks applied successfully!");
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("System reset to default settings!");
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
                DragMove();
        }
    }
}