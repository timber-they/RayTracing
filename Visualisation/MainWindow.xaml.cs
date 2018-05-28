using System.Windows;


namespace Visualisation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow ()
        {
            InitializeComponent ();
        }

        private void MainWindow_OnLoaded (object sender, RoutedEventArgs e) =>
            ((MainViewModel) DataContext).LoadedCommand.Execute (e);
    }
}