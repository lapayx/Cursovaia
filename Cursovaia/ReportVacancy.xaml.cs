using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;


using Cursovaia.Logic.DataBase;
using Cursovaia.Logic.Interface;
using Ninject;
using System.Windows.Input;
using MahApps.Metro.Controls;

using System.Threading.Tasks;
using Cursovaia.Logic.Model;


namespace Cursovaia
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class ReportVacancy : Window
    {
        public ReportVacancy()
        {
            InitializeComponent();
            Loaded += DisplayReport;
        }
        private void DisplayReport(object sender, RoutedEventArgs e)
        {
            DIConfig.kernel.Get<WIN_Server>();
            try
            {
                IGenericRepository<VVacancy> t = DIConfig.kernel.Get<IGenericRepository<VVacancy>>();
                IGenericRepository<Profession> p = DIConfig.kernel.Get<IGenericRepository<Profession>>();
                Combo.ItemsSource = p.SelectAll().ToList();

                var qry = t.SelectAll().ToList(); // 
                Report1.ProcessingMode = ProcessingMode.Local;
                Report1.LocalReport.ReportEmbeddedResource = "Cursovaia.Report.VakancyReport.rdlc"; // .Reports if the report isin the Reports folder not in the root
                ReportDataSource dataSource = new ReportDataSource("DataSet1", qry);
                Report1.LocalReport.DataSources.Clear();
                Report1.LocalReport.DataSources.Add(dataSource);

                Report1.RefreshReport();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
