using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;

namespace Cursovaia.ViewModel
{

    public class BaseViewModel : INotifyPropertyChanged,IDataErrorInfo
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected void GoToReferense(string name)
        {
            Navigation.Navigate(name);
        }

        protected ICommand comand;
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

        /// <summary>
        /// Для хранения ошибок
        /// </summary>
        public virtual string Error { get { return string.Empty; } }

        /// <summary>
        /// Для валидации полей и проверки ивывода ошибок полей если есть
        /// http://msdn.microsoft.com/ru-ru/magazine/ff714593.aspx
        /// </summary>
        /// <param name="propertyName"> Названеи поля или ключ</param>
        /// <returns></returns>
        public virtual string this[string propertyName]
        {
            get { return string.Empty; ; }
        }


        protected virtual void InitializeCommands()
        {

            GoToReferen = new RelayCommand<string>(GoToReferense);

        }


    }
}
