using System.Windows;
using Clients_Managment.ViewModels;

namespace Clients_Managment
{
    //Finestra principal de l'aplicació (SPA Container)
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}