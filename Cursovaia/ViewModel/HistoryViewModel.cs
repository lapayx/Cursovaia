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
    class HistoryViewModel : BaseViewModel
    {
        readonly IGenericRepository<VHistory> repository;
        private IActionParamService actionParam;
        private VHistory _selectedItem;
        public bool IsSelectedItem {get;set;}
        private List<VHistory> source;
        private string _searhKey = "";
        public string Caption { get; set; }

        public string SearchKey
        {
            get { return this._searhKey; }
            set { this._searhKey = value.Trim(); RaisePropertyChanged("SourceForGrid"); }
        }
        public List<VHistory> SourceForGrid
        {
            get
            {
                if (this._searhKey.Length > 0)
                {
                    string key = this._searhKey.ToLower(); 
                    return this.source.Where(x => x.EmployeeSecondName.ToLower().Contains(key)).ToList();
                }
                else
                    return this.source.ToList();
            }
        }

       // ChangeApplicant
        public HistoryViewModel(IGenericRepository<VHistory> hist,IGenericRepository<Organization> org, IGenericRepository<Applicant> app, IActionParamService param)
        {
            this.repository = hist;
            this.actionParam = param;
            this.Caption = "Список историй вакансий";
            if (param.Action == PageAction.Organization) {
                var t = org.SelectById(param.Parameter);
                this.Caption = "Список историй организации " + t.Name;
            }
            if (param.Action == PageAction.Vacancy) {
                this.Caption = "Список историй Вакансии № " + param.Parameter;
            }
            if (param.Action == PageAction.Appplicant) {
                var t = app.SelectById(param.Parameter);
                this.Caption = "Список историй соискателя " + t.FirstName + " "+t.SecondName ;
            }

            
            InitializeCommands();
        }

        public VHistory Current_row
        {
            get {
                return _selectedItem;
        } set {
            _selectedItem = value;
            if (value == null) 
            { 
                this.IsSelectedItem = false;
            } 
            else 
            { 
                IsSelectedItem = true;
            }
            
            RaisePropertyChanged("IsSelectedItem");
         //   RaisePropertyChanged("Current_row"); 
        } }


        protected override void updateSource(string s = null)
        {
            switch (this.actionParam.Action) { 
            
                case PageAction.Appplicant:
                    this.source = this.repository.SelectAll().Where(x => x.IdApplicant == (int)this.actionParam.Parameter).ToList();
                    break;
                case PageAction.Vacancy:
                    this.source = this.repository.SelectAll().Where(x => x.IdVacancy == (int)this.actionParam.Parameter).ToList();
                    break;
                case PageAction.Organization:
                    this.source = this.repository.SelectAll().Where(x => x.IdOrganization == (int)this.actionParam.Parameter).ToList();
                    break;
                default:
                    this.source = this.repository.SelectAll().ToList();
                    break;
            }
            
            RaisePropertyChanged("SourceForGrid");
        }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            Change = new RelayCommand<string>(ChangeApplicant);
            Delete = new RelayCommand<string>(DeleteApplicant);
            Refresh = new RelayCommand<string>(updateSource);

        }        
        
        public ICommand Change { get; set; }
        public ICommand Delete { get; set; }
        public ICommand Refresh { get; set; }
        private void ChangeApplicant(string s)
        {
            try
            {
                _selectedItem.Status = int.Parse(s);
                repository.Update(_selectedItem);
                repository.Save();
               // repository.db.P_UPDATE_STATUS_V_HISTORY(_selectedItem.Id, );
                
            }
            catch (SystemException e)
            {

                DIConfig.MainVindow.ShowMessage("Неудача", "Произошла ошибка, повторите попытку позже");
                Debug.WriteLine(e.Source + "\n" + e.Message);
            }
            this.updateSource();
        }
        private void DeleteApplicant(string s)
        {
            if (this.actionParam.Parameter == null) {
                DIConfig.MainVindow.ShowMessage("Ошибка", "Не выбран Сотрудник");
                return;
            }
            repository.Delete(_selectedItem.Id);
            repository.Save();
            DIConfig.MainVindow.ShowMessage("Успех", "Запись удалена");
            this.updateSource();
        }
    }
}
