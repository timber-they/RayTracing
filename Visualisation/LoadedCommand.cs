using System;
using System.Windows.Input;


namespace Visualisation
{
    public class LoadedCommand : ICommand
    {
        public MainViewModel MainViewModel { get; }
        public LoadedCommand (MainViewModel mainViewModel) => MainViewModel = mainViewModel;

        /// <inheritdoc />
        public bool CanExecute (object parameter) => true;

        /// <inheritdoc />
        public void Execute (object parameter) => MainViewModel.GenerateBitmap ();

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;
    }
}