using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Cursovaia.ViewModel
{
    class MainViewModel:BaseViewModel
    {
        private void GoToReferense(string name)
        {
            Navigation.Navigate(name);
        }
        private ICommand comand;
        public ICommand GoToReferen
        {
            get
            {
                return comand;
            }
            set
            {
                comand = value;
                RaisePropertyChanged("GoToReferen");
            }
        }
        private void InitializeCommands()
        {

            GoToReferen = new RelayCommand<string>(GoToReferense);

        }
        public MainViewModel()
        {
            InitializeCommands();
        }
    }
}
