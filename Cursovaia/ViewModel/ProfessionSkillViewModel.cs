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
    class ProfessionSkillViewModel : BaseViewModel
    {
        readonly IGenericRepository<VProfessionSkill> repository;
        readonly IGenericRepository<Profession> professionRepository;

        private List<VProfessionSkill> source;
        private IActionParamService actionParam;
        private VProfessionSkill _selectedItem;
        private int _newIdProfessionSkill;
        private Applicant _applicant;
        private string _searhKey = "";
        public string Caption { get; set; }
        public bool IsSelectedItem { get; set; }

        public int NewIdProfessionSkill
        {
            get { return this._newIdProfessionSkill; } 
            set{
                this._newIdProfessionSkill = value;
                if (value == 0)
                    IsSelectedNewIdProfessionSkill = false;
                else
                    IsSelectedNewIdProfessionSkill = true;
                RaisePropertyChanged("IsSelectedNewIdProfessionSkill");
            }}

        public bool IsSelectedNewIdProfessionSkill { get; set; }

        public string SearchKey { 
            get { return this._searhKey; }
            set { this._searhKey = value.Trim(); RaisePropertyChanged("SourceForGrid"); } 
        }


        public List<Profession> ProfessionSourse { get{
            var t = this.source.Select(x => x.IdProfession).Distinct().ToList();
            this.NewIdProfessionSkill = 0;
            RaisePropertyChanged("NewIdProfessionSkill");

            return this.professionRepository.SelectAll().Where(x=> ! t.Contains(x.Id)).ToList();
        
        }}

        public List<VProfessionSkill> SourceForGrid
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


        public ProfessionSkillViewModel(IGenericRepository<VProfessionSkill> app, IGenericRepository<Profession> prof,IGenericRepository<Applicant> applicant, IActionParamService param)
        {
            this.repository = app;
            this.actionParam = param;
            this.professionRepository = prof;
           // this.actionParam.Set(PageAction.ProfessionSkill);
            

            this._applicant = applicant.SelectById(this.actionParam.Parameter);
            this.Caption = "Владение професиями "+_applicant.FirstName+" "+_applicant.SecondName;
            InitializeCommands();
        }


        public VProfessionSkill Current_row
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
            this.source = this.repository.SelectAll().Where(x => x.IdApplicant == this._applicant.Id).ToList();
            RaisePropertyChanged("SourceForGrid");
            RaisePropertyChanged("ProfessionSourse");
        }
        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            AddNewProfession = new RelayCommand<string>(addNewProfession);
            Delete = new RelayCommand<string>(DeleteItem);
            Refresh = new RelayCommand<string>(updateSource);
        }

        public ICommand AddNewProfession { get; set; }
        public ICommand Delete { get; set; }
        public ICommand Refresh { get; set; }
        private void addNewProfession(string s)
        {
            try
            {
               /// плохо,оооочень плохо,не делать так
                repository.db.P_INSERT_V_ROFESSION_SKILL(this._applicant.Id,this._newIdProfessionSkill);
            }
            catch (SystemException e)
            {
                DIConfig.MainVindow.ShowMessage("Неудача", "Произошла ошибка повторите операцию");
                Debug.WriteLine(e.Source + "\n" + e.Message);
            }
            this.updateSource();
        }
        private void DeleteItem(string s)
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
