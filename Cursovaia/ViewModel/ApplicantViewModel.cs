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
    class ApplicantViewModel : BaseViewModel
    {
        readonly IGenericRepository<Applicant> applicant;
        private IActionParam actionParam;
        public List<Applicant> sourse { get {
            return this.applicant.SelectAll().ToList();
        } }

       // ChangeApplicant
        public ApplicantViewModel(IGenericRepository<Applicant> app, IActionParam param)
        {
            this.applicant = app;
            this.actionParam = param;
            this.actionParam.Set(PageAction.Appplicant);

            InitializeCommands();
        }
        public void SelectRow() {
            return;
        }
        Applicant t = null;
        public Object Current_row
        {
            get {
            return t;
        } set {
             t = (Applicant)value;
             var o = 2 + 9;
        } }
        private ICommand changePrivate;
        public ICommand Change
        {
            get
            {
                return changePrivate;
            }
            set
            {
                changePrivate = value;
                RaisePropertyChanged("Change");
            }
        }
        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            Change = new RelayCommand<string>(ChangeApplicant);


        }
        private void ChangeApplicant(string s)
        {
            if (this.actionParam.Parameter == null) {
                DIConfig.MainVindow.ShowMessage("Ошибка", "Не выбран Соискатель");
                return;
            }
            GoToReferense(s);
        }
    }
}
