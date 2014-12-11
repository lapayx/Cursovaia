using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cursovaia.Logic.DataBase;
using Cursovaia.Logic.Interface;
using Ninject;
using Cursovaia.Logic.Model;
using System.Windows.Input;


namespace Cursovaia.ViewModel
{
    class EmployeeViewModel : BaseViewModel
    {
        readonly IGenericRepository<VEmployee> repository;
        private IActionParamService actionParam;
        private VEmployee _selectedItem;
        public bool IsSelectedItem {get;set;}
        private List<VEmployee> source;
        private string _searhKey = "";

        public string SearchKey
        {
            get { return this._searhKey; }
            set { this._searhKey = value.Trim(); RaisePropertyChanged("SourceForGrid"); }
        }
        public List<VEmployee> SourceForGrid
        {
            get
            {
                if (this._searhKey.Length > 0)
                {
                    string key = this._searhKey.ToLower(); 
                    return this.source.Where(x => x.SecondName.ToLower().Contains(key)).ToList();
                }
                else
                    return this.source;
            }
        }

       // ChangeApplicant
        public EmployeeViewModel(IGenericRepository<VEmployee> app, IActionParamService param)
        {
            this.repository = app;
            this.actionParam = param;
            this.actionParam.Set(PageAction.Employee);

            InitializeCommands();
        }

        public VEmployee Current_row
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
         //   RaisePropertyChanged("Current_row"); 
        } }


        protected override void updateSource(string s = null)
        {
            this.source = this.repository.SelectAll().ToList();
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
            if (this.actionParam.Parameter == null) {
                DIConfig.MainVindow.ShowMessage("Ошибка", "Не выбран Сотрудник");
                return;
            }
            this.actionParam.TypeAction = TypeAction.Change;
            GoToReferense(s);
        }
        private void DeleteApplicant(string s)
        {
            if (this.actionParam.Parameter == null) {
                DIConfig.MainVindow.ShowMessage("Ошибка", "Не выбран Сотрудник");
                return;
            }
            repository.Delete(this.actionParam.Parameter);
            repository.Save();
            DIConfig.MainVindow.ShowMessage("Успех", "Запись удалена");
            this.updateSource();
        }
    }
}
