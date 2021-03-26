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
        Measurement measurement;
        Observation observation = new Observation();
        Area area = new Area();
        Category category;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region READ
        /// <summary>
        /// Get a list of all observers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewObservers_Click(object sender, RoutedEventArgs e)
        {
            lbObserver.UpdateLayout();
            lbObserver.ItemsSource = db.GetObserversByLastName();

        }

        /// <summary>
        /// Get list of all animals within Category.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAnimals_DropDownOpened(object sender, EventArgs e)
        {
            cmbAnimals.UpdateLayout();
            cmbAnimals.ItemsSource = db.GetAnimalCategories();
        }

        /// <summary>
        /// Gets observations made by observer, viewed as date of observation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnObservation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lbObservation.ItemsSource = null;
                lbAnimalsCounted.ItemsSource = null;
                observer = db.GetObserver((Observer)lbObserver.SelectedItem);
                lbObservation.ItemsSource = db.GetObservations(observer);
                lbObservation.UpdateLayout();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Gets fur for animals in category. Only animals with fur shown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Gets measurements made in observation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbObservation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            observation = (Observation)lbObservation.SelectedItem;
            List<int> nrId = new List<int>();
            List<Category> categories = new List<Category>();
            List<Measurement> amount = new List<Measurement>();
            List<Category> animals = new List<Category>();
            categories = db.GetCategories();
            foreach (Category id in categories)
            {
                nrId.Add(id.Id);
            }
            for (int i = 0; i < nrId.Count(); i++)
            {
                measurement = db.GetMeasurement(observation, nrId[i]);
                if (measurement == null)
                {
                    if (nrId[i] == 9)
                        txbWind.Text = "";
                    else if (nrId[i] == 10)
                        txbRain.Text = "";
                    else if (nrId[i] == 11)
                        txbTemperature.Text = "";
                    else if (nrId[i] == 73)
                        txbSnow.Text = "";
                    else
                        txbAnimals.Text = "";
                }
                else
                {
                    if (nrId[i] == 9)
                        txbWind.Text = measurement.ToString();
                    if (nrId[i] == 10)
                        txbRain.Text = measurement.ToString();
                    if (nrId[i] == 11)
                        txbTemperature.Text = measurement.ToString();
                    if (nrId[i] == 73)
                        txbSnow.Text = measurement.ToString();
                    foreach (Category ani in db.GetAnimalCategories())
                    {
                        if (nrId[i] == ani.Id)
                        {
                            amount.Add(measurement);
                            Category animal = db.GetAnimal(nrId[i]);
                            animals.Add(animal);
                            
                        }
                        int howMany = animals.Count();
                        List<string> bindtoListView = new List<string>();
                        for (int x = 0; x < howMany; x++)
                        {
                            string first = animals[x].Name.ToString();
                            string last = amount[x].Value.ToString();
                            bindtoListView.Add($"{first} {last}");
                        }
                        lbAnimalsCounted.ItemsSource = bindtoListView;


                    }
                }
            }
            
        }

        /// <summary>
        /// Gets list of areas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbArea_DropDownOpened(object sender, EventArgs e)
        {
            cmbArea.UpdateLayout();
            cmbArea.ItemsSource = db.GetAreas();
        }

        
        #endregion

        #region CREATE
        /// <summary>
        /// write in textbox first name, last name, add an observer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Adds new observation to chosen observer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Geolocation geolocation = new Geolocation();
            area = (Area)cmbArea.SelectedItem;
            int usethis = area.Id;
            geolocation = db.GetGeolocation(usethis);
            observation = db.AddObservation(geolocation, (Observer)lbObserver.SelectedItem, txbDate.Text);
        }

        /// <summary>
        /// Adds measurements to chosen observation by filling textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMeasure_Click(object sender, RoutedEventArgs e)
        {

            List<Measurement> measurements = new List<Measurement>();

            if (txbWind.Text != "")
            {
                measurement = new Measurement();
                category = new Category();
                category = db.GetCategory(9);
                measurement.Category_id = category.Id;
                measurement.Value = double.Parse(txbWind.Text);
                measurements.Add(measurement);
            }
            if (txbRain.Text != "")
            {
                measurement = new Measurement();
                category = new Category();
                category = db.GetCategory(10);
                measurement.Category_id = category.Id;
                measurement.Value = double.Parse(txbRain.Text);
                measurements.Add(measurement);

            }
            if (txbTemperature.Text != "")
            {
                measurement = new Measurement();
                category = new Category();
                category = db.GetCategory(11);
                measurement.Category_id = category.Id;
                measurement.Value = double.Parse(txbTemperature.Text);
                measurements.Add(measurement);

            }
            if (txbSnow.Text != "")
            {
                measurement = new Measurement();
                category = new Category();
                category = db.GetCategory(73);
                measurement.Category_id = category.Id;
                measurement.Value = double.Parse(txbSnow.Text);
                measurements.Add(measurement);

            }
            if (txbAnimals.Text != "")
            {
                measurement = new Measurement();
                category = new Category();
                category = (Category)cmbAnimals.SelectedItem;
                if (cmbFur.SelectedItem != null)
                {
                    measurement = new Measurement();
                    Category cate = new Category();
                    cate = (Category)cmbFur.SelectedItem;
                    measurement.Category_id = category.Id;
                    measurements.Add(measurement);
                }
                measurement.Category_id = category.Id;
                measurement.Value = double.Parse(txbAnimals.Text);
                measurements.Add(measurement);

            }

            db.AddMultipleMeasurementValues(measurements, (Observation)lbObservation.SelectedItem);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete an observer (ATTENTION! Cannot be removed if observer has done observation!)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        #endregion

        #region Update

        /// <summary>
        /// Updates measurements to chosen observation. Only overrides current measurements.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            List<Measurement> measurements = new List<Measurement>();
            List<Measurement> measurements1 = new List<Measurement>();

            measurements = db.GetMeasurements((Observation)lbObservation.SelectedItem);

            foreach (var measurement in measurements)
            {

                if (measurement.Category_id == 9 && txbWind.Text != measurement.Value.ToString())
                {
                    
                    measurement.Value = double.Parse(txbWind.Text);
                    measurements1.Add(measurement);
                }
                if (measurement.Category_id == 10 && txbRain.Text != measurement.Value.ToString())
                {
                   
                    measurement.Value = double.Parse(txbRain.Text);
                    measurements1.Add(measurement);
                }
                if (measurement.Category_id == 11 && txbTemperature.Text != measurement.Value.ToString())
                {
                    
                    measurement.Value = double.Parse(txbTemperature.Text);
                    measurements1.Add(measurement);
                }
                if (measurement.Category_id == 73 && txbSnow.Text != measurement.Value.ToString())
                {
                    
                    measurement.Value = double.Parse(txbSnow.Text);
                    measurements1.Add(measurement);
                }

            }
            db.UpdateMeasurementValues(measurements1);
        }
        #endregion
    }
}
