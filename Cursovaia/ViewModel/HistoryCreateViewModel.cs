using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cursovaia.Logic.DataBase;
using Cursovaia.Logic.Interface;
using Ninject;
using Cursovaia.Logic.Model;
using System.Windows.Input;
using System.Diagnostics;


namespace Cursovaia.ViewModel
{
    class HistoryCreateViewModel : BaseViewModel
    {
        readonly IGenericRepository<VVacancy> repository;

        private List<VVacancy> source;
        private IActionParamService actionParam;
        private VVacancy _selectedItem;
        private Organization _organization;
        private string _searhKey = "";
        public string Caption { get; set; }
        public bool IsSelectedItem { get; set; }


        public bool IsSelectedNewIdProfessionSkill { get; set; }

        public string SearchKey { 
            get { return this._searhKey; }
            set { this._searhKey = value.Trim(); RaisePropertyChanged("SourceForGrid"); } 
        }



        public List<VVacancy> SourceForGrid
        {
            get {

                if (this._searhKey.Length > 0)
                {
                    string key = this._searhKey.ToLower();
                    return this.source.Where(x => x.NameProfession.ToLower().Contains(key)).ToList();
                }
                else
                    return this.source.ToList();
        } }


        public HistoryCreateViewModel(IGenericRepository<VVacancy> app, IGenericRepository<Organization> org, IActionParamService param)
        {
            this.repository = app;
            this.actionParam = param;

           // this.actionParam.Set(PageAction.ProfessionSkill);
            this.Caption = "Список вакансий";
            if (this.actionParam.Action == PageAction.Organization && this.actionParam.Parameter != null)
            {
                this._organization = org.SelectById(this.actionParam.Parameter);
                this.Caption = "Список вакансий от" + _organization.Name;
            }
            this.actionParam.Set(PageAction.Vacancy);
            InitializeCommands();
        }


        public VVacancy Current_row
        {
            get {
                return _selectedItem;
        } set {
            _selectedItem = value;
            if (value == null) 
            { 
                this.actionParam.Parameter = null;
                this.IsSelectedItem = false;
            } 
            else 
            { 
                this.actionParam.Parameter =  value.Id;
                IsSelectedItem = true;
            }

            RaisePropertyChanged("IsSelectedItem");
             
        } }


        protected override void updateSource(string s = null)
        {
            this.source = this.repository.SelectAll().ToList();
            RaisePropertyChanged("SourceForGrid");

        }
        #region Comand
        public ICommand Save { get; set; }

        public ICommand Cancel { get; set; }
        #endregion

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            Save = new RelayCommand<string>(SaveItem);
            Cancel = new RelayCommand<string>(CancelItem);

        }
        private void SaveItem(string s)
        {
          
            
            GoToReferense("shereProfession");

        }
        private void CancelItem(string s)
        {

            GoToReferense("applicant");
        }
    }
}
