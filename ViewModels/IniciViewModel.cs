using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF_MVVM_SPA_Template.Models;

namespace WPF_MVVM_SPA_Template.ViewModels
{
    //Els ViewModels deriven de INotifyPropertyChanged per poder fer Binding de propietats
    class IniciViewModel : INotifyPropertyChanged
    {
        // Referència al ViewModel principal
        private readonly MainViewModel _mainViewModel;
  
        public IniciViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;


        }


        // Això és essencial per fer funcionar el Binding de propietats entre Vistes i ViewModels
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
