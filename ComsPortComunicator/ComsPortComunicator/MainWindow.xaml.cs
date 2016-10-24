using System;
using System.ComponentModel;
using ComsPortComunicator.ViewModel;

namespace ComsPortComunicator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();

            MainWindowViewModel viewModel = new MainWindowViewModel();

            DataContext = viewModel;

            Closing += viewModel.OnClosing;
        }
    }
}
