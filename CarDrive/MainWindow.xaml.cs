using System;
using System.Windows;

namespace CarDrive
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeMap()
        {
            ControlBar.MapChangeNotify += OriginSimulator.ChangeMap;
            ControlBar.MapChangeNotify += ContractSimulator.ChangeMap;
            OriginSimulator.ChangeMap(ControlBar.SelectedMap);
            ContractSimulator.ChangeMap(ControlBar.SelectedMap);
        }

        private void MainWindow_OnContentRendered(object sender, EventArgs e)
        {
            InitializeMap();
        }
    }
}
