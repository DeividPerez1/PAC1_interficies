using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM_SPA_Template.Views;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // --- PROPIETATS DELS VIEWMODELS ---

        // Assegura't que Option1VM és del tipus ClientsViewModel (o Option1ViewModel segons com li diguis a la classe)
        public ClientsViewModel Option1VM { get; set; }

        public Option2ViewModel Option2VM { get; set; }
        public IniciViewModel IniciVM { get; set; }


        // IMPORTANT: Li he posat el nom en Plural (Clients) perquè així és com el crides des dels altres fitxers
        public AfegirClientsViewModel AfegirClientsVM { get; set; }
        public GraficaViewModel GraficaVM { get; set; }

        // --- CONTROL DE LA VISTA ---
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        private string _selectedView;
        public string SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                ChangeView();
                OnPropertyChanged();
                
            }
        }
        

        public MainViewModel()
        {
            // 1. INICIALITZEM TOTS ELS VIEWMODELS AQUÍ
            // Passem 'this' perquè tots puguin parlar amb el Main
            Option1VM = new ClientsViewModel(this); // Si la teva classe encara es diu Option1ViewModel, canvia 'ClientsViewModel' per 'Option1ViewModel'
            Option2VM = new Option2ViewModel(this);
            IniciVM = new IniciViewModel(this);

            // Inicialitzem també el del formulari
            AfegirClientsVM = new AfegirClientsViewModel(this);
            GraficaVM = new GraficaViewModel(this);


            // 2. VISTA INICIAL
            SelectedView = "Inici";
            ChangeView();
        }

        private void ChangeView()
        {
            switch (SelectedView)
            {
                case "Option1": // Vista de Llista
                    // Creem la vista i li endollan el ViewModel que ja tenim creat
                    CurrentView = new Option1View { DataContext = Option1VM };
                    break;

                case "Option2":
                    CurrentView = new Option2View { DataContext = Option2VM };
                    break;

                case "Inici":
                    CurrentView = new IniciView { DataContext = IniciVM };
                    break;

                // Aquest cas ha de coincidir amb el string que has posat als botons ("AfegirClients")
                case "AfegirClients":
                    
                    CurrentView = new AfegirClientsView { DataContext = AfegirClientsVM };
                    break;

                case "Grafica":
                    CurrentView = new GraficaView { DataContext = GraficaVM };
                    break;
            }
        }

        // He ELIMINAT el mètode 'public void AfegirClientsVM()' perquè generava conflicte.
        // Ara tot es fa a través de la propietat i el SelectedView.

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}