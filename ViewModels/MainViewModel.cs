using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM_SPA_Template.Views;
using WPF_MVVM_SPA_Template.Services; 
using System.Collections.ObjectModel;  
using WPF_MVVM_SPA_Template.Models;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // --- PROPIETATS DELS VIEWMODELS ---

        // Assegura't que Option1VM és del tipus ClientsViewModel (o Option1ViewModel segons com li diguis a la classe)
        public ClientsViewModel Option1VM { get; set; }

       
        public IniciViewModel IniciVM { get; set; }


        // IMPORTANT: Li he posat el nom en Plural (Clients) perquè així és com el crides des dels altres fitxers
        public AfegirClientsViewModel AfegirClientsVM { get; set; }
        public GraficaViewModel GraficaVM { get; set; }

        // Necessitem declarar la Ruleta aquí perquè l'app la reconegui
        public RuletaViewModel RuletaVM { get; set; }

        // La llista de clients SEMPRE es declara aquí dalt
        public ObservableCollection<Client> Clients { get; set; }

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
            
            

            Clients = XmlService.Carregar();    // CARREGUEM L'XML: Només engegar, cridem al teu servei per llegir el disc dur
           
            RuletaVM = new RuletaViewModel(this);// Inicialitzem la ruleta i li passem "this" perquè pugui veure els clients

            Option1VM = new ClientsViewModel(this); 
            
            IniciVM = new IniciViewModel(this);

          
            AfegirClientsVM = new AfegirClientsViewModel(this);
            GraficaVM = new GraficaViewModel(this);


        
            SelectedView = "Inici";
            ChangeView();
        }

        private void ChangeView()
        {
            switch (SelectedView)
            {
                case "Option1": 
                   
                    CurrentView = new Option1View { DataContext = Option1VM };
                    break;

                case "Inici":
                    CurrentView = new IniciView { DataContext = IniciVM };
                    break;

                
                case "AfegirClients":
                    
                    CurrentView = new AfegirClientsView { DataContext = AfegirClientsVM };
                    break;

                case "Grafica":
                    CurrentView = new GraficaView { DataContext = GraficaVM };
                    break;

                case "Ruleta": // AFEGIR EL CAS DE LA RULETA
                    CurrentView = new RuletaView {DataContext = RuletaVM };
                    break;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}