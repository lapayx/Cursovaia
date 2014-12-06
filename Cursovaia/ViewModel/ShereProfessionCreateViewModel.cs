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
    class ShereProfessionCreateViewModel : BaseViewModel
    {
        readonly IGenericRepository<ShereProfession> repository;

        private ShereProfession item;
        private Dictionary<string, bool> cheakList;
        public string Caption { get; set; }
        private bool _isNewItem = true;

        #region Свойства полей формы
        public string name { get { return this.item.Name; }
            set {this.item.Name = value.Trim(); RaisePropertyChanged("name"); }
        }

       
        #endregion


        public ShereProfessionCreateViewModel(IGenericRepository<ShereProfession> reposityry, IActionParamService param)
        {
            this.repository = reposityry;
            this.Caption = "Создание новой Сферы";
            this.item = new ShereProfession();
            if (param.Action == PageAction.ShereProfession && param.TypeAction != TypeAction.Create)
            {
                this.item = this.repository.SelectById(param.Parameter);

                if (this.item == null)
                {
                    this.item = new ShereProfession();
                }
                else {
                    this._isNewItem = false;
                    this.Caption = "Изменение данных сферы";
                }
               

            }
            
            this.cheakList = new Dictionary<string, bool>();
            cheakList.Add("name", false);

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
            if (this._isNewItem)
                repository.Insert(item);
            else
                repository.Update(item); ;
            repository.Save();
            GoToReferense("shereProfession");
        
        }
        private void CancelApplicant(string s) {

            GoToReferense("shereProfession");
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
                            return "Слишком короткое Название";
                        cheakList[columnName] = (this.item.Name != null)? true : false;
                        break;
                   
                    default :
                        break;
                }
                

                return null;
            }
        }
    }
}
