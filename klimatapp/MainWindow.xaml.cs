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
using System.ComponentModel;
using System.Data;
using System.Drawing;


namespace klimatapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KlimatRepos db = new KlimatRepos();
        Country country = new Country();
        Observer observer = new Observer();

        public MainWindow()
        {
            InitializeComponent();
            //db.fillComboBox(cmbCountry, "name", "country");
            //cmbCountry.DisplayMemberPath = "name";
            //cmbCountry.SelectedValuePath = "id";

        }


        private void btnAddObserver_Click(object sender, RoutedEventArgs e)
        {
            string fname = txbFirstName.Text;
            string lname = txbLastName.Text;
            Observer obs = new Observer
            {
                FirstName = fname,
                LastName = lname
            };
            db.AddObserver(obs);
            MessageBox.Show($"{obs.FirstName} {obs.LastName} har lagst till databasen");
        }

        private void btnViewObservers_Click(object sender, RoutedEventArgs e)
        {
            lbObserver.UpdateLayout();
            lbObserver.ItemsSource = db.GetObserversByLastName();
            
        }

        private void cmbAnimals_DropDownOpened(object sender, EventArgs e)
        {
            cmbAnimals.UpdateLayout();
            cmbAnimals.ItemsSource = db.GetCategories();
        }

        //private void btnObservation_Click(object sender, RoutedEventArgs e)
        //{
        //    lbObservation.UpdateLayout();
        //    lbObservation.ItemsSource = (System.Collections.IEnumerable)db.GetCategory(cmbCategory.SelectedItem.ToString());
        //}

        private void btnDeleteObserver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                observer = (Observer)lbObserver.SelectedItem;
                db.DeleteObserver(observer);
                lbObserver.UpdateLayout();
                lbObserver.ItemsSource = db.GetObserversByLastName();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnObservation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                observer = db.GetObserver((Observer)lbObserver.SelectedItem);
                
                lbObservation.ItemsSource = db.GetObservations(observer);
                lbObservation.UpdateLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cmbFur_DropDownOpened(object sender, EventArgs e)
        {
            if (cmbAnimals.SelectedItem.ToString() == "brown bear")
            {
                cmbFur.ItemsSource = db.GetFurs(4);
            }
            else if (cmbAnimals.SelectedItem.ToString() == "wolf")
            {
                cmbFur.ItemsSource = db.GetFurs(4);
            }
            else if (cmbAnimals.SelectedItem.ToString() == "fox")
            {
                cmbFur.ItemsSource = db.GetFurs(4);
            }
            else
                cmbFur.ItemsSource = "";
            cmbFur.UpdateLayout();
        }




        //    private void UpdateUI()
        //    {
        //        var observers = db.GetObservers();
        //        lstTest.ItemsSource = null;
        //        lstTest.ItemsSource = observers;
        //    }
    }
}
