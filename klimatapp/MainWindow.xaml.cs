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
        /// Lägger till Observers i Observers listann.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewObservers_Click(object sender, RoutedEventArgs e)
        {
            lbObserver.UpdateLayout();
            lbObserver.ItemsSource = db.GetObserversByLastName();
        }

        /// <summary>
        /// Lägger till Animals i combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAnimals_DropDownOpened(object sender, EventArgs e)
        {
            cmbAnimals.UpdateLayout();
            cmbAnimals.ItemsSource = db.GetAnimalCategories();
        }

        /// <summary>
        /// Hämtar Observation av den Observer som är dubbel tryckt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbObserver_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
        /// Hämtar fur för animals som har fur.
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
                cmbFur.ItemsSource = db.GetFurs(12);
            }
            else if (cmbAnimals.SelectedItem.ToString() == "fox")
            {
                cmbFur.ItemsSource = db.GetFurs(40);
            }
            else
                cmbFur.ItemsSource = "";
            cmbFur.UpdateLayout();
        }

        /// <summary>
        /// Hämtar measurements från Observation som är dubbel tryckt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbObservation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txbAnimals.Text = ""; // börjar med att rensa från alla textbox.
            txbRain.Text = "";
            txbSnow.Text = "";
            txbTemperature.Text = "";
            txbWind.Text = "";
            lbAnimalsCounted.ItemsSource = null; //rensar också Animals counted listann och comboboxen med animals.
            cmbAnimals.ItemsSource = null;
            cmbFur.ItemsSource = null;
            List<string> showMeAnimals = new List<string>(); //lista av strings som visar animals i Animals Counted
            List<Measurement> measurements = db.GetMeasurements((Observation)lbObservation.SelectedItem); 
            //hämtar measurements beroende av vilken observation är valt.
            foreach(var meas in measurements) //kollar alla measurements i listan.
            {
                if (meas.Category_id == 9) //först kollar om det är väder.
                {
                    txbWind.Text = meas.Value.ToString();
                }
                else if (meas.Category_id == 10)
                {
                    txbRain.Text = meas.Value.ToString();
                }
                else if (meas.Category_id == 11)
                {
                    txbTemperature.Text = meas.Value.ToString();
                }
                else if (meas.Category_id == 73)
                {
                    txbSnow.Text = meas.Value.ToString();
                }
                else //här kollar för animals.
                {
                    if (db.GetAnimal(meas.Category_id) != null)
                    {
                        Category animal = (Category)db.GetAnimal(meas.Category_id); //hämta animal från measurement.
                        string one = animal.Name.ToString(); //skapa string som läggs i string listann.
                        string two = meas.Value.ToString();
                        string final = $"{one} {two}";
                        showMeAnimals.Add(final);
                    }
                    else if (db.GetFurForAnimal(meas.Category_id) != null) //om det är animal med fur då först kolla fur...
                    {
                        Category fur = (Category)db.GetFurForAnimal(meas.Category_id); //...lägga till den i category...
                        Category animal = (Category)db.GetAnimal((int)fur.Basecategory_id); //... och animal hittad baserad på fur.
                        string one = animal.Name.ToString(); //igen, skapa strings och lägga i string lista.
                        string two = fur.Name.ToString();
                        string three = meas.Value.ToString();
                        string final = $"{one} {two} {three}";
                        showMeAnimals.Add(final);
                    }
                }

            }
            lbAnimalsCounted.ItemsSource = showMeAnimals; //göra string listann till source för listbox.

        }

        /// <summary>
        /// Hämta Areas för combobox
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
        /// skriva i textbox first name last name, lägga till observer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddObserver_Click(object sender, RoutedEventArgs e)
        {
            if (txbFirstName.Text == "" || txbLastName.Text == "") //kolla om det finns något i textboxen.
            {
                MessageBox.Show("Måste fylla i first name och last name");
                return;
            }
            string fname = txbFirstName.Text;
            string lname = txbLastName.Text;
            Observer obs = new Observer //skapa det som observer
            {
                FirstName = fname,
                LastName = lname
            };
            db.AddObserver(obs); //lägga till i db.
            MessageBox.Show($"{obs.FirstName} {obs.LastName} har lagst till databasen");
        }

        /// <summary>
        /// lägger till en ny observation till observer som är valt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbObserver.SelectedItem == null) //kolla om det är någon observer valt
                {
                    MessageBox.Show("Välj områd som observationen gjordes i");
                    return;
                }
                if (cmbArea.SelectedItem == null) //kolla om det är något områd valt
                {
                    MessageBox.Show("Välj områd som observationen gjordes i");
                    return;
                }
                if (txbDate.Text == "") //kolla om det finns datum
                {
                    MessageBox.Show("måste lägga till datum");
                    return;
                }
                Geolocation geolocation;
                area = (Area)cmbArea.SelectedItem; //lägga till utifrån combobox
                int usethis = area.Id; // använda id från area till att hitta geolocation för observation.geolocation_id i databasen
                geolocation = db.GetGeolocation(usethis);
                observation = db.AddObservation(geolocation, (Observer)lbObserver.SelectedItem, txbDate.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Lägga till measurements till databasen till observationen valt baserad på textboxen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMeasure_Click(object sender, RoutedEventArgs e)
        {
            if(lbObservation.SelectedItem == null)
            {
                MessageBox.Show("måste välja observation till att lägga measurement till.");
                return;
            }
            List<Measurement> measurements = new List<Measurement>(); //här läggs till measurements innan databasen

            if (txbWind.Text != "") //kolla om textboxen är toma.
            {
                measurement = new Measurement(); 
                category = new Category();
                category = db.GetCategory(9); //först hämta category från db baserad på vilken textbox.
                measurement.Category_id = category.Id; //category läggs till i measurement.
                measurement.Value = double.Parse(txbWind.Text); // ändra text till double.
                measurements.Add(measurement); //lägga till measurement.
            }
            if (txbRain.Text != "") //repeat från första
            {
                measurement = new Measurement();
                category = new Category();
                category = db.GetCategory(10);
                measurement.Category_id = category.Id;
                measurement.Value = double.Parse(txbRain.Text);
                measurements.Add(measurement);

            }
            if (txbTemperature.Text != "") //repeat från första
            {
                measurement = new Measurement();
                category = new Category();
                category = db.GetCategory(11);
                measurement.Category_id = category.Id;
                measurement.Value = double.Parse(txbTemperature.Text);
                measurements.Add(measurement);

            }
            if (txbSnow.Text != "") //repeat från första
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
                if (cmbFur.SelectedItem != null) //kolla först om det är animal
                {
                    measurement = new Measurement();
                    category = new Category();
                    category = (Category)cmbFur.SelectedItem; //measurement läggs till i category som är fur
                    measurement.Value = double.Parse(txbAnimals.Text);
                    measurement.Category_id = category.Id;
                    measurements.Add(measurement);
                }
                else
                {
                    category = new Category();
                    category = (Category)cmbAnimals.SelectedItem; //ingen fur, measurement läggs till animal
                    measurement = new Measurement();
                    measurement.Category_id = category.Id;
                    measurement.Value = double.Parse(txbAnimals.Text);
                    measurements.Add(measurement);
                }

            }
            db.AddMultipleMeasurementValues(measurements, (Observation)lbObservation.SelectedItem); //läggs till i databasen
        }
        #endregion

        #region Delete
        /// <summary>
        /// Radera observer. OBS! Kan inte radera om det finns observation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteObserver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbObserver.SelectedItem == null)
                {
                    MessageBox.Show("Någon observer måste välja till att radera");
                    return;
                }
                observer = (Observer)lbObserver.SelectedItem;
                db.DeleteObserver(observer); //här gör det försök att radera från databasen
                lbObserver.UpdateLayout();
                lbObserver.ItemsSource = null;
                lbObserver.ItemsSource = db.GetObserversByLastName(); //visa nya observer listann.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

        #region Update

        /// <summary>
        /// Updatera measurements till observationen som är valt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lbObservation == null)
                MessageBox.Show("måste välja observation var du vill ändra measurements");
            List<Measurement> measurements;
            List<Measurement> measurements1 = new List<Measurement>();
            

            measurements = db.GetMeasurements((Observation)lbObservation.SelectedItem); //hämtar measurements från database

            foreach (var measurement in measurements)
            {

                if (measurement.Category_id == 9 && txbWind.Text != measurement.Value.ToString()) //kolla om det finns något nytt
                {
                    
                    measurement.Value = double.Parse(txbWind.Text); //ändra text till double och lägga till ny lista
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
                if (cmbAnimals.SelectedItem == (Measurement)cmbAnimals.SelectedItem && txbAnimals.Text != measurement.ToString())
                {
                    measurement.Value = double.Parse(txbAnimals.Text);
                    measurements1.Add(measurement);

                    if (cmbFur.SelectedItem != (Measurement)cmbFur.SelectedItem) //kollar med fur och bestämmer utifrån comboboxen.
                    {
                        category = new Category();
                        category = (Category)cmbFur.SelectedItem;
                        measurement.Category_id = category.Id;
                        measurements1.Add(measurement);
                    }
                }

            }
            db.UpdateMeasurementValues(measurements1); //updaterad med nya listan.
        }
        #endregion


    }
}

