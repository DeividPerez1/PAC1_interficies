using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM_SPA_Template.Models;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    //Els ViewModels deriven de INotifyPropertyChanged per poder fer Binding de propietats
    class Option1ViewModel : INotifyPropertyChanged
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
        public Option1ViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            // Carreguem estudiants a memòria mode de prova
            Clients.Add(new Client { Id = 1,DNI = "33333", Name = "David", last_name = "juanche",Email ="pablomotos@gmail.com",Tlf = 66777,date = "05/7/26"});
            Clients.Add(new Client { Id = 2,DNI = "44443", Name = "Pablo", last_name = "tictuc",Email ="tiktak@gmail.com",Tlf = 66777,date = "08/7/26"});

            // Inicialitzem els diferents commands disponibles (accions)
            AddClientCommand = new RelayCommand(x => AddClient());
            DelClientCommand = new RelayCommand(x => DelClient());
            VeureGraficaCommand = new RelayCommand(x => VeureGrafica());
        }

        //Mètodes per afegir i eliminar estudiants de la col·lecció
        private void AddClient()
        {
            Clients.Add(new Client { Id = Clients.Count + 1, DNI = "nou", Name = "nou", last_name = "nou", Email = "nou@gmail.com", Tlf = 11111, date = "nou" });
        }

        private void DelClient()
        {
            if (SelectedClient != null)
                Clients.Remove(SelectedClient);
        }
        private void VeureGrafica()
        {
           
        }

        // Això és essencial per fer funcionar el Binding de propietats entre Vistes i ViewModels
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
