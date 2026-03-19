using System.ComponentModel;
using System.Runtime.CompilerServices;
using Clients_Managment.Views;
using Clients_Managment.Services; 
using System.Collections.ObjectModel;  
using Clients_Managment.Models;

namespace Clients_Managment.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // --- PROPIETATS DELS VIEWMODELS ---

        public ClientsViewModel Option1VM { get; set; }

       
        public IniciViewModel IniciVM { get; set; }


        public AfegirClientsViewModel AfegirClientsVM { get; set; }
        public GraficaViewModel GraficaVM { get; set; }
        public RuletaViewModel RuletaVM { get; set; }
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
            
            

            Clients = XmlService.Carregar();   
           
            RuletaVM = new RuletaViewModel(this);

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

                case "Ruleta": 
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