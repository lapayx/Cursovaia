using System;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.ComponentModel;
using Ninject.Modules;
using Ninject;

namespace Cursovaia
{
    public sealed class Navigation
    {
        #region Constants


        #endregion

        #region Fields

        private NavigationService _navService;
      //  private readonly IPageResolver _resolver;

        #endregion


        #region Properties

        public static NavigationService Service
        {
            get { return Instance._navService; }
            set
            {
                if (Instance._navService != null)
                {
                    Instance._navService.Navigated -= Instance._navService_Navigated;
                }

                Instance._navService = value;
                Instance._navService.Navigated += Instance._navService_Navigated;
            }
        }

        #endregion


        #region Public Methods

        public static void Navigate(Page page, object context)
        {
            if (Instance._navService == null || page == null)
            {
                return;
            }

            Instance._navService.Navigate(page, context);
        }

        public static void Navigate(Page page)
        {
            Navigate(page, null);
        }

        public static void Navigate(string name, object context = null)
        {
            if (Instance._navService == null || name == null)
            {
                return;
            }
            var page = DIConfig.kernel.Get<Page>(name);
            if (context == null) {
                context = DIConfig.kernel.Get<INotifyPropertyChanged>(name);
            }
            Navigate(page, context);
        }

        #endregion


        #region Private Methods

        void _navService_Navigated(object sender, NavigationEventArgs e)
        {
            var page = e.Content as Page;

            if (page == null)
            {
                return;
            }

            page.DataContext = e.ExtraData;
        }

        #endregion


        #region Singleton

        private static volatile Navigation _instance;
        private static readonly object SyncRoot = new Object();

        private static Navigation Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new Navigation();
                    }
                }

                return _instance;
            }
        }
        #endregion
    }
}
