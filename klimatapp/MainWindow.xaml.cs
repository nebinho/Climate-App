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
        Measurement measurement = new Measurement();
        Observer observer = new Observer();
        Area area = new Area();
        Category category = new Category();
        Observation observation = new Observation();

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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            List<Measurement> measurements = new List<Measurement>();

            if (txbWind.Text != "")
            {
                category = db.GetCategory(9);
                measurement.Category_id = category.Id;
                measurement.Value = double.Parse(txbWind.Text);
                measurements.Add(measurement);
            }
            if (txbRain.Text != "")
            {
                category = db.GetCategory(10);
                measurement.Category_id = category.Id;
                measurement.Value = double.Parse(txbRain.Text);
                measurements.Add(measurement);

            }
            if (txbTemperature.Text != "")
            {
                category = db.GetCategory(11);
                measurement.Category_id = category.Id;
                measurement.Value = double.Parse(txbTemperature.Text);
                measurements.Add(measurement);

            }
            if (txbSnow.Text != "")
            {
                category = db.GetCategory(73);
                measurement.Category_id = category.Id;
                measurement.Value = double.Parse(txbSnow.Text);
                measurements.Add(measurement);

            }
            if (txbAnimals.Text != "")
            {
                cmbAnimals.ItemsSource = db.GetCategories();
                measurement.Category_id = category.Id;
                measurement.Value = double.Parse(txbAnimals.Text);
                measurements.Add(measurement);

            }



        }

        private void cmbAreas_DropDownOpened(object sender, EventArgs e)
        {
            cmbAreas.UpdateLayout();
            cmbAreas.ItemsSource = db.GetAreas();
        }

        private void lbObservation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            observation = (Observation)lbObservation.SelectedItem;
            int HowMany = db.GetCategories().Count();
            for (int i = 0; i < HowMany; i++)
            {
                measurement = db.GetMeasurement(observation, i);
                if (measurement == null)
                {
                    if (i == 9)
                        txbWind.Text = "";
                    else if (i == 10)
                        txbRain.Text = "";
                    else if (i == 11)
                        txbTemperature.Text = "";
                    else
                        txbAnimals.Text = "";
                }
                else
                {
                    if (i == 9)
                        txbWind.Text = measurement.ToString();
                    else if (i == 10)
                        txbRain.Text = measurement.ToString();
                    else if (i == 11)
                        txbTemperature.Text = measurement.ToString();
                    else
                        txbAnimals.Text = measurement.ToString();
                }
            }


        }
        //private void lbObservation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    

        //    }


        //    private void UpdateUI()
        //    {
        //        var observers = db.GetObservers();
        //        lstTest.ItemsSource = null;
        //        lstTest.ItemsSource = observers;
        //    }



    }
}