using klimatapp.Models;
using klimatapp.Repositories;
using klimatapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace klimatapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KlimatRepos db = new KlimatRepos();
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void testBtn_Click(object sender, RoutedEventArgs e)
        {
            //string test = testBox.Text;

            //var areas = db.GetAreas();

            //lstTest.ItemsSource = null;
            //lstTest.ItemsSource = areas;

            //MessageBox.Show(area.Name);
            //try
            //{
            //    var observer = db.GetObserver(1);
            //    var observers = db.GetObservers();


            //    observer = new Observer
            //    {
            //        FirstName = "Kiddi",
            //        //LastName = "Kidd"
            //    };

            //    observer = db.AddObserver(observer);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        private void btnAddObserver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var observer = db.GetObserver(1);
                var observers = db.GetObservers();


                observer = new Observer
                {
                    FirstName = "Kiddi",
                    //LastName = "Kidd"
                };

                observer = db.AddObserver(observer);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }


        //    private void UpdateUI()
        //    {
        //        var observers = db.GetObservers();
        //        lstTest.ItemsSource = null;
        //        lstTest.ItemsSource = observers;
        //    }



    }
}
