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
    class OrganizationViewModel : BaseViewModel
    {
        readonly IGenericRepository<Organization> repository;
        private IActionParamService actionParam;
        private Organization _selectedOrganization;
        public bool IsSelectedRow { get; set; }

        public List<Organization> sourse
        {
            get {
            return this.repository.SelectAll().ToList();
        } }


        public OrganizationViewModel(IGenericRepository<Organization> app, IActionParamService param)
        {
            this.repository = app;
            this.actionParam = param;
            this.actionParam.Set(PageAction.Organization);

            InitializeCommands();
        }

        public Organization Current_row
        {
            get {
                return _selectedOrganization;
        } set {
            _selectedOrganization = value;
            if (value == null) 
            { 
                this.actionParam.Parameter = null;
                this.IsSelectedRow = false;
            } 
            else 
            { 
                this.actionParam.Parameter =  value.Id;
                IsSelectedRow = true;
            }

            RaisePropertyChanged("IsSelectedRow");
             
        } }



        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            Change = new RelayCommand<string>(ChangeApplicant);
            Delete = new RelayCommand<string>(DeleteApplicant);

        }        
        
        public ICommand Change { get; set; }
        public ICommand Delete { get; set; }
        private void ChangeApplicant(string s)
        {
            if (this.actionParam.Parameter == null) {
                DIConfig.MainVindow.ShowMessage("Ошибка", "Не выбрана организация");
                return;
            }
            this.actionParam.TypeAction = TypeAction.Change;
            GoToReferense(s);
        }
        private void DeleteApplicant(string s)
        {

            if (this.actionParam.Parameter == null) {
                DIConfig.MainVindow.ShowMessage("Ошибка", "Не выбрана Организация");
                return;
            }
            bool success = true;
            try
            {
                repository.Delete(this.actionParam.Parameter);
                repository.Save();
            }
            catch (ExecutionEngineException e) {
                success = false;
            }
            if(success)
            
                DIConfig.MainVindow.ShowMessage("Успех", "Запись удалена");
            else
                DIConfig.MainVindow.ShowMessage("Неудача", "Произошла ошибка повторите операцию");
            RaisePropertyChanged("sourse");
        }
    }
}
