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

        // Propietat per controlar seleccionat a la vista
        private Client _NewClient;
        public Client NewClient
        {

            get => _NewClient;
            set { _NewClient = value; OnPropertyChanged(); }
        }

        // RelayCommands connectats via Binding als botons de la vista
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
 
        public AfegirClientsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            _NewClient = new Client();
            // Lógica del comando Guardar
            SaveCommand = new RelayCommand(x => SaveClient());

            // Lógica del comando Cancelar (volver atrás)
            CancelCommand = new RelayCommand(x => _mainViewModel.SelectedView = "Option1");

        }

        private void SaveClient()
        {

            if (NewClient == null) return;
            //  Generar un ID automático basado en el último de la lista
            int nextId = _mainViewModel.Option1VM.Clients.Count + 1;
            NewClient.Id = nextId;

            //  Añadir el cliente a la ObservableCollection de Option1ViewModel
            _mainViewModel.Option1VM.Clients.Add(NewClient);

            //  Navegar de vuelta a la lista de clientes
            _mainViewModel.SelectedView = "Option1";
        }

        // Això és essencial per fer funcionar el Binding de propietats entre Vistes i ViewModels
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
