using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq; // <--- IMPORTANT: Necessari per fer cerques (FirstOrDefault)
using WPF_MVVM_SPA_Template.Models;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    public class AfegirClientsViewModel : INotifyPropertyChanged
    {
        private readonly MainViewModel _mainViewModel;

        // Variable per saber si estem editant (true) o creant (false)
        private bool _esEdicio = false;

        // ObservableCollection no és necessària aquí si només editem UN client, 
        // però la deixo per si la feies servir per alguna altra cosa.
        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();

        private Client _NewClient;
        public Client NewClient
        {
            get => _NewClient;
            set { _NewClient = value; OnPropertyChanged(); }
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public AfegirClientsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _NewClient = new Client();

            SaveCommand = new RelayCommand(x => SaveClient());

            // Tornar enrere (a la llista)
            CancelCommand = new RelayCommand(x => _mainViewModel.SelectedView = "Option1");
        }

        // --- MÈTODE 1: PREPARAR PER AFEGIR (Esborra el formulari) ---
        public void PrepararPerAfegir()
        {
            _esEdicio = false;
            NewClient = new Client(); // Creem un client buit
            OnPropertyChanged("NewClient");
        }

        // --- MÈTODE 2: CARREGAR PER EDITAR (Omple el formulari) ---
        public void CarregarClientPerEditar(Client clientOriginal)
        {
            _esEdicio = true;

            // IMPORTANT: Fem una CÒPIA (Clon) de les dades.
            // Si no ho fem així, quan escriguis al TextBox es canviarà a la taula directament.
            NewClient = new Client
            {
                Id = clientOriginal.Id,
                DNI = clientOriginal.DNI,
                Name = clientOriginal.Name,
                last_name = clientOriginal.last_name,
                Email = clientOriginal.Email,
                Tlf = clientOriginal.Tlf,
                date = clientOriginal.date
            };

            OnPropertyChanged("NewClient");
        }

        // --- LÒGICA DE GUARDAR ---
        private void SaveClient()
        {
            if (NewClient == null) return;

        
            var llistaClients = _mainViewModel.Option1VM.Clients;

            if (_esEdicio)
            {
                // EDITAR: Busquem el client original a la llista pel seu ID
                var clientAEditar = llistaClients.FirstOrDefault(c => c.Id == NewClient.Id);

                if (clientAEditar != null)
                {
                    // Passem les dades del formulari al client de la llista
                    clientAEditar.DNI = NewClient.DNI;
                    clientAEditar.Name = NewClient.Name;
                    clientAEditar.last_name = NewClient.last_name;
                    clientAEditar.Email = NewClient.Email;
                    clientAEditar.Tlf = NewClient.Tlf;
                    clientAEditar.date = NewClient.date;
                }
            }
            else
            {
                // AFEGIR NOU: Calculem ID i afegim
                int nextId = 1;
                if (llistaClients.Count > 0)
                {
                    nextId = llistaClients.Max(c => c.Id) + 1;
                }

                NewClient.Id = nextId;
                llistaClients.Add(NewClient);
            }

            // Tornem a la pantalla de llista
            _mainViewModel.SelectedView = "Option1";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}