﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Data.Entity;
using Ninject.Modules;
using Ninject;
using Cursovaia.Pages;
using Cursovaia.Logic.Service;
using Cursovaia.ViewModel;
using Cursovaia.Logic.DataBase;
using Cursovaia.Logic.Interface;

namespace Cursovaia
{
    /// <summary>
    /// Конфигурация Dependency Injection.
    /// </summary>
    public static  class DIConfig
    {
        public static MainWindow MainVindow;
        static DIConfig() 
        {
            DIConfig.Register();
        }
         public static IKernel  kernel;
        /// <summary>
        /// Регистрация зависимостей.
        /// </summary>
        public static void Register()
        {
            kernel = new StandardKernel(new InjectModule());
            //container.RegisterControllers(Assembly.GetExecutingAssembly());
        }
       
    }
    public class InjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<INotifyPropertyChanged>().To<vievmodel1>().Named("page1");
            this.Bind<INotifyPropertyChanged>().To<AboutViewModel>().Named("about");
            this.Bind<INotifyPropertyChanged>().To<ApplicantViewModel>().Named("applicant");
            this.Bind<INotifyPropertyChanged>().To<ApplicantCreateViewModel>().Named("applicantCreate");
            this.Bind<INotifyPropertyChanged>().To<OrganizationViewModel>().Named("organization");
            this.Bind<INotifyPropertyChanged>().To<OrganizationCreateViewModel>().Named("organizationCreate");
            this.Bind<INotifyPropertyChanged>().To<ShereProfessionViewModel>().Named("shereProfession");
            this.Bind<INotifyPropertyChanged>().To<ShereProfessionCreateViewModel>().Named("shereProfessionCreate");
            this.Bind<INotifyPropertyChanged>().To<SpecialityViewModel>().Named("speciality");
            this.Bind<INotifyPropertyChanged>().To<SpecialityCreateViewModel>().Named("specialityCreate");
            this.Bind<INotifyPropertyChanged>().To<EmployeeViewModel>().Named("employee");
            this.Bind<INotifyPropertyChanged>().To<EmployeeCreateViewModel>().Named("employeeCreate");
            this.Bind<INotifyPropertyChanged>().To<ProfessionViewModel>().Named("profession");
            this.Bind<INotifyPropertyChanged>().To<ProfessionCreateViewModel>().Named("professionCreate");
            this.Bind<INotifyPropertyChanged>().To<ProfessionSkillViewModel>().Named("professionSkill");
            this.Bind<INotifyPropertyChanged>().To<VacancyViewModel>().Named("vacancy");
            this.Bind<INotifyPropertyChanged>().To<VacancyCreateViewModel>().Named("vacancyCreate");
            this.Bind<INotifyPropertyChanged>().To<HistoryViewModel>().Named("history");
            this.Bind<INotifyPropertyChanged>().To<HistoryCreateViewModel>().Named("historyCreate");

            this.Bind<Page>().To<Page1>().Named("page1");
            this.Bind<Page>().To<AboutPage>().Named("about");
            this.Bind<Page>().To<ApplicantPage>().Named("applicant");
            this.Bind<Page>().To<ApplicantCreatePage>().Named("applicantCreate");
            this.Bind<Page>().To<OrganizationPage>().Named("organization");
            this.Bind<Page>().To<OrganizationCreatePage>().Named("organizationCreate");
            this.Bind<Page>().To<ShereProfessionPage>().Named("shereProfession");
            this.Bind<Page>().To<ShereProfessionCreatePage>().Named("shereProfessionCreate");
            this.Bind<Page>().To<SpecialityPage>().Named("speciality");
            this.Bind<Page>().To<SpecialityCreatePage>().Named("specialityCreate");
            this.Bind<Page>().To<EmployeePage>().Named("employee");
            this.Bind<Page>().To<EmployeeCreatePage>().Named("employeeCreate");
            this.Bind<Page>().To<ProfessionPage>().Named("profession");
            this.Bind<Page>().To<ProfessionCreatePage>().Named("professionCreate");
            this.Bind<Page>().To<ProfessionSkillPage>().Named("professionSkill");
            this.Bind<Page>().To<VacancyPage>().Named("vacancy");
            this.Bind<Page>().To<VacancyCreatePage>().Named("vacancyCreate");
            this.Bind<Page>().To<HistoryPage>().Named("history");
            this.Bind<Page>().To<HistoryCreatePage>().Named("historyCreate");



            this.Repozitory();
            this.Service();
        }
        private void Service()
        {
            this.Bind<IActionParamService>().To<ActionParamService>().InSingletonScope();
        }
        private void Repozitory() {
            this.Bind<WIN_Server>().To<WIN_Server>().InSingletonScope();
            this.Bind<IGenericRepository<Applicant>>().To<Entities<Applicant>>();
            this.Bind<IGenericRepository<Organization>>().To<Entities<Organization>>();
            this.Bind<IGenericRepository<ShereProfession>>().To<Entities<ShereProfession>>();
            this.Bind<IGenericRepository<Speciality>>().To<Entities<Speciality>>();
            this.Bind<IGenericRepository<Employee>>().To<Entities<Employee>>();
            this.Bind<IGenericRepository<VEmployee>>().To<Entities<VEmployee>>();
            this.Bind<IGenericRepository<Profession>>().To<Entities<Profession>>();
            this.Bind<IGenericRepository<VProfession>>().To<Entities<VProfession>>();
            this.Bind<IGenericRepository<VProfessionSkill>>().To<Entities<VProfessionSkill>>();
            this.Bind<IGenericRepository<VVacancy>>().To<Entities<VVacancy>>(); 
            this.Bind<IGenericRepository<Vacancy>>().To<Entities<Vacancy>>();
            this.Bind<IGenericRepository<VHistory>>().To<Entities<VHistory>>();
            //container.Register<DbContext, WIN_Server>(new PerScopeLifetime());
        
        }
    }
}
