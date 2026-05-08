using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Clients_Managment.Models;
using System.Windows; 
using System.Windows.Input;
using FastReport;
using FastReport.Data;
using FastReport.Export.PdfSimple;
using System.Data;
using System.Diagnostics;

namespace Clients_Managment.ViewModels
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

        public RelayCommand InformeCommand { get; set; }

        public ClientsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            // Dades de prova inicials
       

            //Clients.Add(new Client { Id = 2, DNI = "44443",
            //                Name = "Pablo", last_name = "tictuc",
            //                Email = "tiktak@gmail.com", Tlf = 66777,
            //                date = "08/7/26", ChartValues = new double[] { 10,40,25},
            //                ChartLabels = new string [] {"Lunes","martes", "viernes"}
            //});
            
            InformeCommand = new RelayCommand(x => Informe());

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
                Clients_Managment.Services.XmlService.Guardar(_mainViewModel.Clients);
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
                MessageBox.Show("Selecciona un client de la llista primer.");
            }
        }
        DataSet dataSet = new DataSet();
        private void Informe()
        {
            try
            {
                using (Report report = new Report())
                { 
                    report.Load("InformeClient.frx");
                    report.RegisterData(dataSet, "Clients");

                    var datasource = report.GetDataSource("Clients");
                    if (datasource !=null)
                        datasource.Enabled = true;
                    if (report.Prepare())
                    {
                        using (PDFSimpleExport pdfExport = new PDFSimpleExport())
                        { 
                            report.Export(pdfExport, "Informe_Clients.pdf");
                            Process.Start(new ProcessStartInfo("Informe_Clients.pdf") { UseShellExecute = true });
                        }
                        
                    }
                    MessageBox.Show("Informe exportat com a PDF amb èxit!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en generar l'informe: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
