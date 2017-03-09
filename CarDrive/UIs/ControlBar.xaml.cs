using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;

namespace CarDrive
{
    /// <summary>
    /// ControlBar.xaml 的互動邏輯
    /// </summary>
    public partial class ControlBar : UserControl
    {
        private const string MapDirectory = "../../Maps";
        private Map _selectedMap;
        public delegate void MapChanged(Map map);
        public event MapChanged MapChangeNotify;
        public ObservableCollection<Map> Maps { get; private set; }
        public Map SelectedMap
        {
            get
            {
                return _selectedMap;
            }
            set
            {
                _selectedMap = value;
                MapChangeNotify?.Invoke(SelectedMap);
            }
        }


        public ControlBar()
        {
            LoadMap();
            InitializeComponent();
            
        }

        private void LoadMap()
        {
            Maps = new ObservableCollection<Map>();
            MapLoader loader = new MapLoader();
            string[] files = Directory.GetFiles(MapDirectory);

            foreach(string file in files)
            {
                Maps.Add(loader.LoadMap(file));
            }

            SelectedMap = (Maps.Count != 0) ? Maps[0] : null;
        }
    }
}
