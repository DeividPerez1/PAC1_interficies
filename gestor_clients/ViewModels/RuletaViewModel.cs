using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM_SPA_Template.Models;
using WPF_MVVM_SPA_Template.Services;
using System.Threading.Tasks;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    public class RuletaViewModel : INotifyPropertyChanged
    {
        public MainViewModel Main { get; set; }
        public Client ClientSeleccionat { get; set; } 

        private string _textAnimatPremi = "SORT!";
        public string TextAnimatPremi
        {
            get => _textAnimatPremi;
            set { _textAnimatPremi = value; OnPropertyChanged(); }
        }

        public RuletaViewModel(MainViewModel main) { Main = main; }

  
        
        public RelayCommand GirarCommand => new RelayCommand(async obj =>
        {
            if (ClientSeleccionat == null) return;

            // 1. Triem premi a l'atzar
            string[] premis = { "10% Dte", "Sopar Gratis", "Gorra", "Samarreta" };
            Random r = new Random();

            for (int i = 0; i < 15; i++)
            {
                TextAnimatPremi = premis[r.Next(premis.Length)];
                await Task.Delay(100);
            }


            // El resultat final
            string resultat = premis[r.Next(premis.Length)];
            TextAnimatPremi = "🏆 " + resultat;
            ClientSeleccionat.Premi = resultat;

            // Guardem al fitxer XML
            XmlService.Guardar(Main.Clients);
        });

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }













}

