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
    class ProfessionCreateViewModel : BaseViewModel
    {
        readonly IGenericRepository<Profession> repository;
        readonly IGenericRepository<ShereProfession> repositoryShere;

        private Profession item;
        private Dictionary<string, bool> cheakList;
        public string Caption { get; set; }
        private bool _isNewItem = true;

        #region Свойства полей формы
        public string name { get { return this.item.Name; }
            set {this.item.Name = value.Trim(); RaisePropertyChanged("name"); }
        }
        public int idShera
        {
            get { return this.item.IdShereProfession; }
            set { this.item.IdShereProfession = value; RaisePropertyChanged("idShera"); }
        }
        public List<ShereProfession> shereSourse
        {
            get { return repositoryShere.SelectAll().ToList(); }
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


        public ProfessionCreateViewModel(IGenericRepository<Profession> app, IGenericRepository<ShereProfession> apps, IActionParamService param)
        {
            this.repository = app;
            this.repositoryShere = apps;
            this.Caption = "Создание новой професси";
            if (param.Action == PageAction.Profession && param.TypeAction != TypeAction.Create)
            {
                this.item = this.repository.SelectById(param.Parameter);
                if (this.item == null)
                {
                    this.item = new Profession();
                }
                else {
                    this.Caption = "Изменение данных Професии";
                    this._isNewItem = false;
                }

            }
            else
            {
                this.item = new Profession();
            }
            this.cheakList = new Dictionary<string, bool>();
            cheakList.Add("name", false);
            cheakList.Add("idShera", false);

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
            GoToReferense("profession");
        
        }
        private void CancelItem(string s) {

            GoToReferense("profession");
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
                        if (this.item.Name != null && this.item.Name.Length < 2)
                            return "Слишком короткое название";
                        cheakList[columnName] = (this.item.Name != null)? true : false;
                        break;
                    case "idShera":
                        if (this.item.IdShereProfession != null && this.item.IdShereProfession < 1)
                            return "Не выбрано";
                        cheakList[columnName] = (this.item.IdShereProfession != null) ? true : false;
                        break;
                    
                    default :
                        break;
                }
                

                return null;
            }
        }
    }
}
