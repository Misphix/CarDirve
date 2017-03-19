using System;
using System.Windows.Input;

namespace CarDrive
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow
    {
        internal enum Status
        {
            Play, Pause, Stop
        }
        static internal Status _status = Status.Stop;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeMap()
        {
            ControlBar.MapChangeNotify += OriginSimulator.ChangeMap;
            //ControlBar.MapChangeNotify += ContractSimulator.ChangeMap;
            OriginSimulator.ChangeMap(ControlBar.SelectedMap);
            //ContractSimulator.ChangeMap(ControlBar.SelectedMap);
        }

        private void MainWindow_OnContentRendered(object sender, EventArgs e)
        {
            InitializeMap();
        }

        private void PlayCommandOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OriginSimulator.Start(ControlBar.SpeedValue.Value);
            _status = Status.Play;
        }

        private void PlayCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _status != Status.Play;
        }

        private void PauseCommandOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OriginSimulator.Pause();
            _status = Status.Pause;
        }

        private void PauseCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _status == Status.Play;
        }

        private void StopCommandOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OriginSimulator.Stop();
            _status = Status.Stop;
        }

        private void StopCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _status != Status.Stop;
        }
    }
}
