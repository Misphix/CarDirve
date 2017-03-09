using System.Collections.ObjectModel;
using System.Windows.Controls;
using CarDrive.Maps;

namespace CarDrive
{
    /// <summary>
    /// ControlBar.xaml 的互動邏輯
    /// </summary>
    public partial class ControlBar : UserControl
    {
        public ObservableCollection<Map> Maps { get; private set; }
        public Map SelectedMap { get; set; }

        public ControlBar()
        {
            InitializeMaps();
            InitializeComponent();
        }

        private void InitializeMaps()
        {
            Maps = new ObservableCollection<Map>();
            var newMap = new BasicMap("456");
            SelectedMap = newMap;
            Maps.Add(new BasicMap("123"));
            Maps.Add(newMap);
        }
    }
}
