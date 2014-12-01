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
    class OrganizationCreateViewModel : BaseViewModel
    {
        readonly IGenericRepository<Organization> repository;

        private Organization Organization;
        private Dictionary<string, bool> cheakList;
        public string Caption { get; set; }
        private bool _isNewApplicant = true;

        #region Свойства полей формы
        public string name { get { return this.Organization.Name; }
            set {this.Organization.Name = value.Trim(); RaisePropertyChanged("name"); }
        }
        public string secondName
        {
            get { return this.Organization.About; }
            set { this.Organization.About = value.Trim(); RaisePropertyChanged("about"); }
        }
       
        #endregion


        public OrganizationCreateViewModel(IGenericRepository<Organization> reposityry, IActionParamService param)
        {
            this.repository = reposityry;
            this.Caption = "Создание новой Организации";
            this.Organization = new Organization();
            if (param.Action == PageAction.Organization && param.TypeAction != TypeAction.Create)
            {
                this.Organization = this.repository.SelectById(param.Parameter);
                if (this.Organization == null)
                {
                    this.Organization = new Organization();
                }
               

            }
            
            this.cheakList = new Dictionary<string, bool>();
            cheakList.Add("name", false);
            cheakList.Add("about", false);

            InitializeCommands();
        }

        #region Comand
        public ICommand Save { get; set; }

        public ICommand Cancel { get;set;}
        #endregion

        protected override void InitializeCommands()
            
        {
            base.InitializeCommands();
            Save = new RelayCommand<string>(SaveApplicant);
            Cancel = new RelayCommand<string>(CancelApplicant);

        }
        private  void SaveApplicant( string s)
        {
            if (cheakList.Where(x => x.Value == false).Count() > 0)
            {

                DIConfig.MainVindow.ShowMessage("Ошибка", "Не все поля заполнены верно");
                return; 
            }
            if (this._isNewApplicant)
                repository.Insert(Organization);
            else
                repository.Update(Organization); ;
            repository.Save();
            GoToReferense("organization");
        
        }
        private void CancelApplicant(string s) {

            GoToReferense("organization");
        }
        public override  string this[string columnName]
        {
            get
            {
                if (cheakList.ContainsKey(columnName)) 
                    cheakList[columnName] = false;
                
                switch (columnName)
                {

                    case "name":
                        if (this.Organization.Name != null && this.Organization.Name.Length < 2)
                            return "Слишком короткое Название";
                        cheakList[columnName] = (this.Organization.Name != null)? true : false;
                        break;
                    case "about":
                        if (this.Organization.About != null && this.Organization.About.Length < 2)
                            return "Слишком короткая Фамилия";
                        cheakList[columnName] = (this.Organization.About != null) ? true : false;
                        break;
                    default :
                        break;
                }
                

                return null;
            }
        }
    }
}
