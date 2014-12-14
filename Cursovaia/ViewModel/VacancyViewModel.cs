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
    class VacancyViewModel : BaseViewModel
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


        public VacancyViewModel(IGenericRepository<VVacancy> app, IGenericRepository<Organization> org, IActionParamService param)
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
            if (this._organization != null) 
                this.source = this.repository.SelectAll().Where(x => x.IdOrganization == this._organization.Id).ToList();
            else
                this.source = this.repository.SelectAll().ToList();//.Where(x => x.IdApplicant == this._applicant.Id).ToList();
            RaisePropertyChanged("SourceForGrid");

        }
        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            Change = new RelayCommand<string>(ChangeItem);
            Delete = new RelayCommand<string>(DeleteItem);
            Refresh = new RelayCommand<string>(updateSource);
        }

        public ICommand Change { get; set; }
        public ICommand Delete { get; set; }
        public ICommand Refresh { get; set; }
        private void ChangeItem(string s)
        {
            if (this.actionParam.Parameter == null)
            {
                DIConfig.MainVindow.ShowMessage("Ошибка", "Не выбрана строка");
                return;
            }
            this.actionParam.TypeAction = TypeAction.Change;
            GoToReferense(s);
        }
        private void DeleteItem(string s)
        {

            if (this.actionParam.Parameter == null) {
                DIConfig.MainVindow.ShowMessage("Ошибка", "Не выбрана строка");
                return;
            }
            bool success = true;
            try
            {
                repository.Delete(this.actionParam.Parameter);
                repository.Save();
               
            }
            catch (SystemException e)
            {

                success = false;
                Debug.WriteLine(e.Source + "\n" + e.Message);
            }
            if(success)
            
                DIConfig.MainVindow.ShowMessage("Успех", "Запись удалена");
            else
                DIConfig.MainVindow.ShowMessage("Неудача", "Произошла ошибка повторите операцию");
            this.updateSource();
        }
    }
}
