using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM_SPA_Template.Models;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    //Els ViewModels deriven de INotifyPropertyChanged per poder fer Binding de propietats
    class AfegirClientsViewModel : INotifyPropertyChanged
    {
        // Referència al ViewModel principal
        private readonly MainViewModel _mainViewModel;

        // Col·lecció de Students (podrien carregar-se d'una base de dades)
        // ObservableCollection és una llista que notifica els canvis a la vista
        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();

        // Propietat per controlar l'estudiant seleccionat a la vista
        private Client _selectedClient;
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; OnPropertyChanged(); }
        }

        // RelayCommands connectats via Binding als botons de la vista
        public RelayCommand AddClientCommand { get; set; }
        public RelayCommand DelClientCommand { get; set; }
        public RelayCommand VeureGraficaCommand { get; set; }
        public AfegirClientsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;


 
        }

        // Això és essencial per fer funcionar el Binding de propietats entre Vistes i ViewModels
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
