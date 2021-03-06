﻿using System;
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

        private Organization item;
        private Dictionary<string, bool> cheakList;
        public string Caption { get; set; }
        private bool _isNewItem = true;

        #region Свойства полей формы
        public string name { get { return this.item.Name; }
            set {this.item.Name = value.Trim(); RaisePropertyChanged("name"); }
        }
        public string about
        {
            get { return this.item.About; }
            set { this.item.About = value.Trim(); RaisePropertyChanged("about"); }
        }
       
        #endregion


        public OrganizationCreateViewModel(IGenericRepository<Organization> reposityry, IActionParamService param)
        {
            this.repository = reposityry;
            this.Caption = "Создание новой Организации";
            this.item = new Organization();
            if (param.Action == PageAction.Organization && param.TypeAction != TypeAction.Create)
            {
                this.item = this.repository.SelectById(param.Parameter);

                if (this.item == null)
                {
                    this.item = new Organization();
                }
                else {
                    this._isNewItem = false;
                    this.Caption = "Изменение данных организации";
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
            if (this._isNewItem)
                repository.Insert(item);
            else
                repository.Update(item); ;
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
                        if (this.item.Name != null && this.item.Name.Length < 2)
                            return "Слишком короткое Название";
                        cheakList[columnName] = (this.item.Name != null)? true : false;
                        break;
                    case "about":
                        if (this.item.About != null && this.item.About.Length < 2)
                            return "Слишком короткая Фамилия";
                        cheakList[columnName] = (this.item.About != null) ? true : false;
                        break;
                    default :
                        break;
                }
                

                return null;
            }
        }
    }
}
