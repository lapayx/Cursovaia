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
        public string Caption { get; set; }
        public string CaptionApplicant { get; set; }
        public bool IsSelectedItem { get; set; }


        public List<VVacancy> SourceForGrid
        {
          get{
              return this.source.ToList();
            } 
        }


        public HistoryCreateViewModel(IGenericRepository<VVacancy> vac, IGenericRepository<Applicant> app, IActionParamService param)
        {
            this.repository = vac;
            this.actionParam = param;

           // this.actionParam.Set(PageAction.ProfessionSkill);
            this.Caption = "Добавление истории";
            var t = app.SelectById(this.actionParam.Parameter);
            this.CaptionApplicant = t.FirstName+ " " + t.SecondName;
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
                this.IsSelectedItem = false;
            } 
            else 
            { 
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
            bool success = true;
            try
            {
                repository.db.P_ADD_HISTORY((int)this.actionParam.Parameter, _selectedItem.Id);
            }
            catch (SystemException e)
            {

                success = false;
                Debug.WriteLine(e.Source + "\n" + e.Message);
            }
            if (success)
            {
                DIConfig.MainVindow.ShowMessage("Успех", "В историю соискателя добавлена вакансия №" + _selectedItem.Id);
                GoToReferense("applicant");
            }
            else
                DIConfig.MainVindow.ShowMessage("Неудача", "Произошла ошибка. \n Допускается только одна запись в истории для одной вакансии имеющая статус 0");
            this.updateSource();
            
        }
        private void CancelItem(string s)
        {

            GoToReferense("applicant");
        }
    }
}
