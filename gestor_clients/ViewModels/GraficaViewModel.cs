using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Clients_Managment.Models;
using LiveCharts;
using LiveCharts.Wpf;

namespace Clients_Managment.ViewModels
{
    public class GraficaViewModel : INotifyPropertyChanged
    {
        private readonly MainViewModel _mainViewModel;

   
        public SeriesCollection MisSeries { get; set; }
        public string[] Etiquetas => _mainViewModel.Option1VM.SelectedClient?.ChartLabels ?? new string[] { "Sin datos" };
        public string Titulo => "Facturació de " + (_mainViewModel.Option1VM.SelectedClient?.Name ?? "Client");

        // Lógica para cambiar el tipo
        private string _tipoSeleccionado;
        public string TipoSeleccionado
        {
            get { return _tipoSeleccionado; }
            set
            {
                _tipoSeleccionado = value;
                OnPropertyChanged();
                ActualizarGrafica(); 
            }
        }

        public ObservableCollection<string> TiposDisponibles { get; set; }

        public ICommand EnreraCommand { get; set; }

        public GraficaViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            
            TiposDisponibles = new ObservableCollection<string> { "Línies", "Barres" };
            _tipoSeleccionado = "Línies";

            EnreraCommand = new RelayCommand(x => _mainViewModel.SelectedView = "Option1");

         
            _mainViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedView" && _mainViewModel.SelectedView == "Grafica")
                {
                    ActualizarGrafica();
                    OnPropertyChanged(nameof(Titulo));
                    OnPropertyChanged(nameof(Etiquetas));
                }
            };
        }

        private void ActualizarGrafica()
        {
            var cliente = _mainViewModel.Option1VM.SelectedClient;
            if (cliente == null || cliente.ChartValues == null) return;

            var valores = new ChartValues<double>(cliente.ChartValues);

            
            if (TipoSeleccionado == "Línies")
            {
                MisSeries = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Facturació (m€)",
                        Values = valores,
                        PointGeometrySize = 10,
                        AreaLimit = 0 
                    }
                };
            }
            else
            {
                MisSeries = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Facturació (m€)",
                        Values = valores
                    }
                };
            }

            OnPropertyChanged(nameof(MisSeries));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}