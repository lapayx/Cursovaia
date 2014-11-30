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
           // this.Bind<INotifyPropertyChanged>().To<ViewModel2>().Named("page2");
            this.Bind<INotifyPropertyChanged>().To<ApplicantViewModel>().Named("applicant");
            this.Bind<INotifyPropertyChanged>().To<ApplicantCreateViewModel>().Named("applicantCreate");

            this.Bind<Page>().To<Page1>().Named("page1");
            this.Bind<Page>().To<ApplicantPage>().Named("applicant");
            this.Bind<Page>().To<ApplicantCreatePage>().Named("applicantCreate");

            this.Bind<DbContext>().To<WIN_Server>().InSingletonScope();
            this.Bind<IActionParam>().To<ActionParam>().InSingletonScope();
            this.Bind<IGenericRepository<Applicant>>().To<Entities<Applicant>>();
            //container.Register<DbContext, WIN_Server>(new PerScopeLifetime());
        }
    }
}
