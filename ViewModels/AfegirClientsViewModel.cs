using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using WPF_MVVM_SPA_Template.Models;
using System;


namespace WPF_MVVM_SPA_Template.ViewModels
{
    public class AfegirClientsViewModel : INotifyPropertyChanged
    {
        private readonly MainViewModel _mainViewModel;


        private bool _esEdicio = false;


        

        private Client _NewClient;
        public Client NewClient
        {
            get => _NewClient;
            set { _NewClient = value; OnPropertyChanged(); }
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand ResetPremiCommand { get; set; }




        private Random _random = new Random();

        private Tuple<double[], string[]> GenerarDatosAleatorios()
        {
            string[] meses = { "Gener", "Febrer", "Març", "Abril", "Maig", "Juny",
                       "Juliol", "Agost", "Setembre", "Octubre", "Novembre", "Desembre" };

            double[] valores = new double[meses.Length];

            for (int i = 0; i < meses.Length; i++)
            {
                valores[i] = Math.Round(_random.NextDouble() * (20.0 - 2.0) + 2.0, 1);
            }

            return Tuple.Create(valores, meses);
        }



        public AfegirClientsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _NewClient = new Client();

            SaveCommand = new RelayCommand(x => SaveClient());

            CancelCommand = new RelayCommand(x => _mainViewModel.SelectedView = "Option1");

            ResetPremiCommand = new RelayCommand(obj =>
            {
                if (NewClient != null)
                {
                    NewClient.Premi = "";
                    OnPropertyChanged("NewClient");
                }
            });

        }




            // --- MÈTODE 1: PREPARAR PER AFEGIR (Esborra el formulari) ---
            public void PrepararPerAfegir()
        {
            _esEdicio = false;
            NewClient = new Client();
            OnPropertyChanged("NewClient");
        }

        // --- MÈTODE 2: CARREGAR PER EDITAR (Omple el formulari) ---
        public void CarregarClientPerEditar(Client clientOriginal)
        {
            _esEdicio = true;


            NewClient = new Client
            {
                Id = clientOriginal.Id,
                DNI = clientOriginal.DNI,
                Name = clientOriginal.Name,
                last_name = clientOriginal.last_name,
                Email = clientOriginal.Email,
                Tlf = clientOriginal.Tlf,
                date = clientOriginal.date,
                Premi =clientOriginal.Premi
            };

            OnPropertyChanged("NewClient");
        }

        // --- LÒGICA DE GUARDAR ---
        private void SaveClient()




        {
            if (NewClient == null ||
                string.IsNullOrWhiteSpace(NewClient.Name) ||
                string.IsNullOrWhiteSpace(NewClient.DNI))
            {
                System.Windows.MessageBox.Show("Has d'omplir com a mínim el Nom i el DNI!");
                return; 
            }


            var llistaGlobal = _mainViewModel.Clients;
            if (!string.IsNullOrEmpty(NewClient.date) && NewClient.date.Contains(" "))
            {
                NewClient.date = NewClient.date.Split(' ')[0];
            }
            if (_esEdicio)
            {
                var clientAEditar = llistaGlobal.FirstOrDefault(c => c.Id == NewClient.Id);
                if (clientAEditar != null)
                {
                    clientAEditar.DNI = NewClient.DNI;
                    clientAEditar.Name = NewClient.Name; 
                    clientAEditar.last_name = NewClient.last_name;
                    clientAEditar.Email = NewClient.Email;
                    clientAEditar.Tlf = NewClient.Tlf;
                    clientAEditar.date = NewClient.date;
                    clientAEditar.Premi = NewClient.Premi;
                }
            }
            else
            {
                var datosAleatorios = GenerarDatosAleatorios();
                NewClient.ChartValues = datosAleatorios.Item1;
                NewClient.ChartLabels = datosAleatorios.Item2;

                NewClient.Id = llistaGlobal.Count > 0 ? llistaGlobal.Max(c => c.Id) + 1 : 1;

            
                llistaGlobal.Add(NewClient);
            }


            // 2. IMPORTANTÍSSIM: Guardar al fitxer perquè no es perdi en tancar
            WPF_MVVM_SPA_Template.Services.XmlService.Guardar(llistaGlobal);

            _mainViewModel.SelectedView = "Option1";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}