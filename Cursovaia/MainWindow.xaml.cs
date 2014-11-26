using Cursovaia.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
//using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using Cursovaia.Logic.Interface;
using Cursovaia.Logic.DataBase;
using Ninject.Modules;
using Ninject;
using System.Diagnostics;


using System.Threading.Tasks;

namespace Cursovaia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DIConfig.kernel.Get<WIN_Server>();
            DIConfig.MainVindow = this;
            Loaded += MainWindow_Loaded;
            
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Navigation.Service = MainFrame.NavigationService;
            DataContext = new MainViewModel();
            //var t = new WIN_Server();
            //tt = DIConfig.kernel.Get<IGenericRepository<Applicant>>();
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }


        public void ShowMessage(string title, string message)
        {
            //var result =  this.ShowInputAsync("Hello!", "What is your name?");
            this.ShowMessageAsync(title, message);
        }
    }
}
