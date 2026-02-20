using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq; // <--- IMPORTANT: Necessari per fer cerques (FirstOrDefault)
using WPF_MVVM_SPA_Template.Models;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    class GraficaViewModel : INotifyPropertyChanged
    {
        private readonly MainViewModel _mainViewModel;
        public GraficaViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            _mainViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedView" && _mainViewModel.SelectedView == "Grafica")
                {
                    OnPropertyChanged(nameof(Valores));
                    OnPropertyChanged(nameof(Etiquetas));
                    OnPropertyChanged(nameof(Titulo));

                }
            };
        }
        public double[] Valores => _mainViewModel.Option1VM.SelectedClient?.ChartValues ?? new double[] { 0 };
        public string[] Etiquetas => _mainViewModel.Option1VM.SelectedClient?.ChartLabels ?? new string[] { "N/A" };
        public string Titulo => "Gráfica de " + (_mainViewModel.Option1VM.SelectedClient?.Name ?? "Sin nombre");

        // Això és essencial per fer funcionar el Binding de propietats entre Vistes i ViewModels
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}