using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cursovaia.Logic.DataBase;
using Cursovaia.Logic.Interface;
using Ninject;
using System.Windows.Input;
using MahApps.Metro.Controls;

using System.Threading.Tasks;
using Cursovaia.Logic.Model;

namespace Cursovaia.ViewModel
{
    class EmployeeCreateViewModel : BaseViewModel
    {
        readonly IGenericRepository<Employee> repository;
        readonly IGenericRepository<Speciality> repositorySpeciality;

        private Employee item;
        private Dictionary<string, bool> cheakList;
        public string Caption { get; set; }
        private bool _isNewItem = true;

        #region Свойства полей формы
        public string firstName { get { return this.item.FirstName; }
            set {this.item.FirstName = value.Trim(); RaisePropertyChanged("firstName"); }
        }
        public string secondName
        {
            get { return this.item.SecondName; }
            set { this.item.SecondName = value.Trim(); RaisePropertyChanged("secondName"); }
        }
        public string fatherName
        {
            get { return this.item.FatherName; }
            set { this.item.FatherName = value.Trim(); RaisePropertyChanged("fatherName"); }
        }
        public DateTime birthday
        {
            get { return this.item.Birthday; }
            set { this.item.Birthday = value; RaisePropertyChanged("birthday"); }
        }
      
        public int idSpetiality
        {
            get { return this.item.IdSpeciality; }
            set { this.item.IdSpeciality = value;  }
        }
        public List<Speciality> spetialitySourse
        {
            get { return repositorySpeciality.SelectAll().ToList(); }
        }

        /*
        public string education
        {
            get { return this.item.Education; }
            set { this.item.Education = value.Trim(); RaisePropertyChanged("education"); }
        }
        public string family
        {
            get { return this.item.Family; }
            set { this.item.Family = value.Trim(); RaisePropertyChanged("family"); }
        }
        public string socialStatus
        {
            get { return this.item.SocialStatus; }
            set { this.item.SocialStatus = value.Trim(); RaisePropertyChanged("socialStatus"); }
        }*/
        #endregion


        public EmployeeCreateViewModel(IGenericRepository<Employee> app,IGenericRepository<Speciality> apps, IActionParamService param)
        {
            this.repository = app;
            this.repositorySpeciality = apps;
            this.Caption = "Создание нового Сотрудника";
            if (param.Action == PageAction.Employee && param.TypeAction != TypeAction.Create)
            {
                this.item = this.repository.SelectById(param.Parameter);
                if (this.item == null)
                {
                    this.item = new Employee
                   {
                       Birthday = DateTime.Now
                   };
                }
                else {
                    this.Caption = "Изменение данных сотрудника";
                    this._isNewItem = false;
                }

            }
            else
            {
                this.item = new Employee
                {
                    Birthday = DateTime.Now
                };
            }
            this.cheakList = new Dictionary<string, bool>();
            cheakList.Add("firstName", false);
            cheakList.Add("secondName", false);
            cheakList.Add("fatherName", false);

            InitializeCommands();
        }

        #region Comand
        private ICommand savePrivate;
        public ICommand Save
        {
            get
            {
                return savePrivate;
            }
            set
            {
                savePrivate = value;
                RaisePropertyChanged("Save");
            }
        }

        private ICommand cancelPrivate;
        public ICommand Cancel
        {
            get
            {
                return cancelPrivate;
            }
            set
            {
                cancelPrivate = value;
                RaisePropertyChanged("Cancel");
            }
        }
        #endregion
        protected override void InitializeCommands()
            
        {
            base.InitializeCommands();
            Save = new RelayCommand<string>(SaveItem);
            Cancel = new RelayCommand<string>(CancelItem);

        }
        private  void SaveItem( string s)
        {
            if (cheakList.Where(x => x.Value == false).Count() > 0)
            {

                DIConfig.MainVindow.ShowMessage("Ошибка", "Не все поля заполнены верно");
                return; 
            }
            if (this._isNewItem)
                repository.Insert(item);
            else
                repository.Update(item); ;
            repository.Save();
            GoToReferense("employee");
        
        }
        private void CancelItem(string s) {

            GoToReferense("employee");
        }
        public override  string this[string columnName]
        {
            get
            {
                if (cheakList.ContainsKey(columnName)) 
                    cheakList[columnName] = false;
                
                switch (columnName)
                {

                    case "firstName":
                        if (this.item.FirstName != null && this.item.FirstName.Length < 2)
                            return "Слишком короткое имя";
                        cheakList[columnName] = (this.item.FirstName != null)? true : false;
                        break;
                    case "secondName":
                        if (this.item.SecondName != null && this.item.SecondName.Length < 2)
                            return "Слишком короткая Фамилия";
                        cheakList[columnName] = (this.item.SecondName != null) ? true : false;
                        break;
                    case "fatherName":
                        if (this.item.FatherName != null && this.item.FatherName.Length < 2)
                            return "Слишком короткая Отчество";
                        cheakList[columnName] = (this.item.FatherName != null) ? true : false;
                        break;
                    default :
                        break;
                }
                

                return null;
            }
        }
    }
}
