using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM_SPA_Template.Models;
using System.Windows; 
using System.Windows.Input; 

namespace WPF_MVVM_SPA_Template.ViewModels
{
    
    public class ClientsViewModel : INotifyPropertyChanged
    {
        private readonly MainViewModel _mainViewModel;

        public ObservableCollection<Client> Clients => _mainViewModel.Clients;

        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient; 
            set { _selectedClient = value; OnPropertyChanged(); }
        }

        
        public RelayCommand AddClientCommand { get; set; }
        public RelayCommand DelClientCommand { get; set; }
        public RelayCommand EditClientCommand { get; set; } 
        public RelayCommand VeureGraficaCommand { get; set; }

        public ClientsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            // Dades de prova inicials
            /*
            Clients.Add(new Client { Id = 1, DNI = "33333",
                            Name = "David", last_name = "juanche",
                            Email = "pablomotos@gmail.com", Tlf = 66777,
                            date = "05/7/26", ChartValues = new double[] { 5,15,60},
                            ChartLabels = new string [] {"Lunes","martes","viernes"}
            });

            Clients.Add(new Client { Id = 2, DNI = "44443",
                            Name = "Pablo", last_name = "tictuc",
                            Email = "tiktak@gmail.com", Tlf = 66777,
                            date = "08/7/26", ChartValues = new double[] { 10,40,25},
                            ChartLabels = new string [] {"Lunes","martes", "viernes"}
            });
            */
        
            AddClientCommand = new RelayCommand(x =>
            {
           
                _mainViewModel.AfegirClientsVM.PrepararPerAfegir();

               
                _mainViewModel.SelectedView = "AfegirClients";
            });

            // --- BOTÓ EDITAR (Blau) ---
            EditClientCommand = new RelayCommand(parametre =>
            {
              
                if (parametre is Client clientPerEditar)
                {
                   
                    _mainViewModel.AfegirClientsVM.CarregarClientPerEditar(clientPerEditar);


                    _mainViewModel.SelectedView = "AfegirClients";
                }
            });

            // --- BOTÓ ELIMINAR (Vermell) ---
            DelClientCommand = new RelayCommand(parametre =>
            {
           
                Client clientAEliminar = parametre as Client ?? SelectedClient;

                if (clientAEliminar != null)
                {
                    var result = MessageBox.Show($"Segur que vols eliminar a {clientAEliminar.Name}?",
                                                 "Confirmar eliminació",
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        Clients.Remove(clientAEliminar);
                    }
                    WPF_MVVM_SPA_Template.Services.XmlService.Guardar(_mainViewModel.Clients);
                }
            });

            VeureGraficaCommand = new RelayCommand(x => VeureGrafica());
        }

        private void VeureGrafica()
        {
            if (SelectedClient != null)
            {
                _mainViewModel.SelectedView = "Grafica";
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un cliente de la lista primero.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
