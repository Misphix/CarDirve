using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace CarDrive.Ui
{
    /// <summary>
    /// ControlBar.xaml 的互動邏輯
    /// </summary>
    public partial class ControlBar
    {
        private const string MapDirectory = "Maps";
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

            Debug.Assert(files.Length > 0);

            foreach(string file in files)
            {
                Maps.Add(loader.LoadMap(file));
            }

            SelectedMap = Maps[0];
        }
    }
}
