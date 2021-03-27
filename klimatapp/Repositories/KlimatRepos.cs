using klimatapp.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace klimatapp.Repositories
{
    public class KlimatRepos
    {
        private static readonly string connectionString = "Server=localhost;Port=5432;Database=klimatapp;User ID=postgres;Password=ct9k5mVZ;";

        #region READ
        /// <summary>
        /// Gets national park area from db
        /// </summary>
        /// <param name="id">Unique primary key</param>
        /// <returns>area</returns>
        //public Area GetArea(int id)
        //{
        //    string statement = "select * from area where id = @id";
        //    using var connection = new NpgsqlConnection(connectionString);
        //    connection.Open();
        //    using var command = new NpgsqlCommand(statement, connection);

        //    command.Parameters.AddWithValue("id", id);

        //    using var reader = command.ExecuteReader();
        //    Area area = null;
        //    while (reader.Read())
        //    {
        //        area = new Area
        //        {
        //            Id = (int)reader["id"],
        //            Name = (string)reader["name"],
        //            CountryId = (int)reader["country_id"]
        //        };
                
        //    }
        //    return area;
        //}

        /// <summary>
        /// Hämta Areas till att lägga i combobox
        /// </summary>
        /// <returns>areas</returns>
        public List<Area> GetAreas()
        {
            string statement = "select * from area";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            using var reader = command.ExecuteReader();
            Area area = null;
            var areas = new List<Area>();
            while (reader.Read())
            {
                area = new Area
                {
                    Id = (int)reader["id"],
                    Name = (string)reader["name"],
                    CountryId = (int)reader["country_id"]
                };
                areas.Add(area);
            }
            return areas;
        }

        /// <summary>
        /// Gets information about measurements in db
        /// </summary>
        /// <param value="id"></param>
        /// <returns>measurement</returns>
        //public Measurement GetMeasurement(Observation observation, int id)
        //{
        //    string statement = $"select value from measurement where observation_id = {observation.Id} AND category_id = {id}";
        //    using var connection = new NpgsqlConnection(connectionString);
        //    connection.Open();
        //    using var command = new NpgsqlCommand(statement, connection);

        //    //command.Parameters.AddWithValue();

        //    using var reader = command.ExecuteReader();
        //    Measurement measurement = null;
        //    //string value = null;
        //    while (reader.Read())
        //    {
        //        measurement = new Measurement
        //        {
        //            //Value = Convert.IsDBNull(reader["value"]) ? null : (float?)reader["value"]
        //            Value = Convert.IsDBNull((double)reader["value"]) ? null : (double?)reader["value"]

        //        };
        //    }
        //    return measurement;
        //}

        /// <summary>
        /// Hämta measurements från databasen
        /// </summary>
        /// <returns>measurements</returns>
        public List<Measurement> GetMeasurements(Observation observation)
        {
            string statement = $"SELECT * FROM measurement WHERE observation_id = {observation.Id}";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            using var reader = command.ExecuteReader();
            
            Measurement measurement = null;
            var measurements = new List<Measurement>();
            while (reader.Read())
            {
                measurement = new Measurement
                {
                    Id = (int)reader["id"],
                    Value = Convert.IsDBNull((double)reader["value"]) ? null : (double?)reader["value"],
                    Observation_id = (int)reader["observation_id"],
                    Category_id = (int)reader["category_id"]
                };
                measurements.Add(measurement);
                
            }
            return measurements;

        }

        /// <summary>
        /// Hämta från category t.ex snow eller temperature
        /// </summary>
        /// <param name = "id" ></ param >
        /// < returns > category </ returns >
        public Category GetCategory(int id)
        {
            string statement = "select * from category where id = @id";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();
            Category category = null;
            while (reader.Read())
            {
                category = new Category
                {
                    Id = (int)reader["id"],
                    Name = (string)reader["name"],
                    Basecategory_id = Convert.IsDBNull(reader["basecategory_id"]) ? null : (int?)reader["basecategory_id"],
                    Unit_id = Convert.IsDBNull(reader["unit_id"]) ? null : (int?)reader["unit_id"]
                };
            }
            return category;
        }

        /// <summary>
        /// Hämta animals till att lägga i combobox
        /// </summary>
        /// <returns>categories</returns>
        public List<Category> GetAnimalCategories()
        {
            string statement = "select * from category WHERE basecategory_id BETWEEN 6 AND 8 ORDER BY name";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);


            using var reader = command.ExecuteReader();
            Category category = null;
            var categories = new List<Category>();
            while (reader.Read())
            {
                category = new Category
                {
                    Id = (int)reader["id"],
                    Name = (string)reader["name"],
                    Basecategory_id = Convert.IsDBNull(reader["basecategory_id"]) ? null : (int?)reader["basecategory_id"],
                    Unit_id = Convert.IsDBNull(reader["unit_id"]) ? null : (int?)reader["unit_id"]
                };
                categories.Add(category);
            }

            return categories;
        }

        /// <summary>
        /// Hämta animal från databasen för att visa i Animals counted listann t.ex
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetAnimal(int id)
        {
            string statement = "select * from category WHERE basecategory_id BETWEEN 6 AND 8 AND id = @id";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);
            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();
            Category category = null;
            
            while (reader.Read())
            {
                category = new Category
                {
                    Id = (int)reader["id"],
                    Name = (string)reader["name"],
                    Basecategory_id = Convert.IsDBNull(reader["basecategory_id"]) ? null : (int?)reader["basecategory_id"],
                    Unit_id = Convert.IsDBNull(reader["unit_id"]) ? null : (int?)reader["unit_id"]
                };
                
            }

            return category;
        }

        //public List<Category> GetCategories()
        //{
        //    string statement = "select * from category ";
        //    using var connection = new NpgsqlConnection(connectionString);
        //    connection.Open();
        //    using var command = new NpgsqlCommand(statement, connection);

        //    var categories = new List<Category>();
        //    using var reader = command.ExecuteReader();
        //    Category category = null;
        //    while (reader.Read())
        //    {
        //        category = new Category
        //        {
        //            Id = (int)reader["id"],
        //        };
        //        categories.Add(category);
        //    }
        //    return categories;
        //}

        /// <summary>
        /// Hämtar observer från databasen
        /// </summary>
        /// <param name="id"></param>
        /// <returns>observer</returns>
        public Observer GetObserver(Observer obs)
        {
            //var lname = name.Substring(name.LastIndexOf(' ') + 1);
            string statement = $"select id from observer where lastname = '{obs.LastName}'";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            //command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();
            Observer observer = null;
            while (reader.Read())
            {
                observer = new Observer
                {
                    Id = (int)reader["id"],
                };

            }
            return observer;
        }

        /// <summary>
        ///hämtar observations från observer och lägger i observation listann. 
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public List<Observation> GetObservations(Observer observer)
        {
            string statement = $"select * from observation WHERE observer_id = {observer.Id}";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            using var reader = command.ExecuteReader();
            Observation observation = null;
            var observations = new List<Observation>();
            while (reader.Read())
            {
                observation = new Observation
                {
                    Id = (int)reader["id"],
                    Date = reader.GetFieldValue<DateTime>(reader.GetOrdinal("date")),
                    ObserverId = (int)reader["observer_id"],
                    GeolocationId = (int)reader["geolocation_id"]
                };
                observations.Add(observation);
            }
            return observations;
        }

        /// <summary>
        /// Hämtar Geolocation från databasen.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>geolocation</returns>
        public Geolocation GetGeolocation(int id)
        {
            string statement = "select * from geolocation where area_id = @id";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();
            Geolocation geolocation = null;
            while (reader.Read())
            {
                geolocation = new Geolocation
                {
                    Id = (int)reader["id"],
                    Latitude = Convert.IsDBNull(reader["latitude"]) ? null : (double?)reader["latitude"],
                    Longitude = Convert.IsDBNull(reader["longitude"]) ? null : (double?)reader["longitude"],
                    AreaId = (int)reader["area_id"]
                };
            }
            return geolocation;
        }

        /// <summary>
        /// Hämta Observers från databasen och lägger till i Observer listann
        /// </summary>
        /// <returns>observers</returns>
        public List<Observer> GetObserversByLastName()
        {
            string statement = $"SELECT * FROM observer ORDER BY lastname";


            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            using var reader = command.ExecuteReader();
            Observer observer = null;
            var observers = new List<Observer>();
            while (reader.Read())
            {
                observer = new Observer
                {
                    Id = (int)reader["id"],
                    FirstName = (string)reader["firstname"],
                    LastName = (string)reader["lastname"]
                };
                observers.Add(observer);
            }
            return observers;
        }

        /// <summary>
        /// hämtar lista av fur i combobox t.ex.
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        public List<Category> GetFurs(int animal)
        {
            string statement = $"select * from category WHERE basecategory_id = {animal}";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            using var reader = command.ExecuteReader();
            Category category = null;
            var categories = new List<Category>();
            while (reader.Read())
            {
                category = new Category
                {
                    Id = (int)reader["id"],
                    Name = (string)reader["name"],
                    Basecategory_id = Convert.IsDBNull(reader["basecategory_id"]) ? null : (int?)reader["basecategory_id"],
                    Unit_id = Convert.IsDBNull(reader["unit_id"]) ? null : (int?)reader["unit_id"]
                };
                categories.Add(category);
            }
            return categories;
        }

        /// <summary>
        /// Hämtar namn för animal som har fur.
        /// </summary>
        /// <param name="fur"></param>
        /// <returns></returns>
        public Category GetFurForAnimal(int fur)
        {
            
            string statement = $"SELECT * FROM category INNER JOIN measurement ON category.id = {fur} WHERE category.id = {fur}; ";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            using var reader = command.ExecuteReader();
            Category category = null;
            while (reader.Read())
            {
                category = new Category
                {
                    Id = (int)reader["id"],
                    Name = (string)reader["name"],
                    Basecategory_id = Convert.IsDBNull(reader["basecategory_id"]) ? null : (int?)reader["basecategory_id"],
                    Unit_id = Convert.IsDBNull(reader["unit_id"]) ? null : (int?)reader["unit_id"]
                };
                
            }
            return category;
        }
        #endregion

        #region Create

        /// <summary>
        /// lägger till en observer från textboxen som ligger under Observer listan.
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public Observer AddObserver(Observer observer)
        {
            string statement = "INSERT INTO observer(firstname, lastname) VALUES(@firstname, @lastname) RETURNING id";
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                using var command = new NpgsqlCommand(statement, connection);

                command.Parameters.AddWithValue("firstname", observer.FirstName);
                command.Parameters.AddWithValue("lastname", observer.LastName);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    observer.Id = (int)reader["id"];
                }
                
                return observer;
            }
            catch (PostgresException ex)
            {
                string errorcode = ex.SqlState;
                throw new Exception("Du måste ange både förnamn och efternamn", ex);
            }

        }

        /// <summary>
        /// lägger til observation till observern som är valt.
        /// </summary>
        /// <param name="geolocation"></param>
        /// <param name="observer"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Observation AddObservation( Geolocation geolocation, Observer observer, string date)
        {
            string statement = $"INSERT INTO observation(observer_id, geolocation_id, date) VALUES ({observer.Id}, {geolocation.Id}, '{date}')";
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                using var command = new NpgsqlCommand(statement, connection);

                Observation observation = new Observation();
                command.Parameters.AddWithValue("observer_id", observer.Id);
                command.Parameters.AddWithValue("geolocation_id", geolocation.Id);
                command.Parameters.AddWithValue("date", date);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    observation.Id = (int)reader["id"];
                }
                return observation;
            }
            catch (PostgresException ex)
            {
                string errorcode = ex.SqlState;
                throw new Exception("Du, det här är fel! Du måste välja rätt date. skriv det såhär: yyyy-MM-dd");
            }
        }

        /// <summary>
        /// Lägger till en eller fler measurements. 
        /// </summary>
        /// <param name="measurements"></param>
        /// <param name="observation"></param>
        public void AddMultipleMeasurementValues(List<Measurement> measurements, Observation observation)
        {
            string stmt = $"INSERT INTO measurement(value, observation_id, category_id) VALUES(@value, {observation.Id}, @category_id)";

            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            var transaction = connection.BeginTransaction(); //skapar transaction till att hantera fler tillägg.
            try
            {
                foreach (var measurement in measurements)
                {
                    using var cmd = new NpgsqlCommand(stmt, connection);
                    cmd.Parameters.AddWithValue("value", measurement.Value ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("category_id", measurement.Category_id);
                    cmd.Parameters.AddWithValue("observation_id", observation.Id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                transaction.Commit(); //slutar med transaction.

            }
            catch (PostgresException ex)
            {
                transaction.Rollback(); //om det blir fel då går det tillbaks.
                string errorCode = ex.SqlState;
                switch (errorCode)
                {
                    default:
                        break;
                }
                throw new Exception("Det gick inte att lägga till en eller flera mätvärden", ex);
            }
        }


        #endregion

        #region UPDATE
        public void UpdateMeasurementValues(List<Measurement> measurements)
        {
            
            string stmt = $"UPDATE measurement SET value = @value WHERE category_id = @category_id AND observation_id = @observation_id";
            
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var transaction = connection.BeginTransaction();
            try
            {
                using var cmd = new NpgsqlCommand(stmt, connection);
                foreach (var measurement in measurements)
                {
                    cmd.Parameters.AddWithValue("value", measurement.Value ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("observation_id", measurement.Observation_id);
                    cmd.Parameters.AddWithValue("category_id", measurement.Category_id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                }

                transaction.Commit();
            }
            catch (PostgresException ex)
            {
                transaction.Rollback();
                string errorCode = ex.SqlState;
                switch (errorCode)
                {
                    default:
                        break;
                }

                throw new Exception("Det gick inte att uppdatera ett eller flera mätvärden", ex);
            }

        }

        #endregion

        #region DELETE

        /// <summary>
        /// Tar iborta observer som är markerad i Observer listann
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public int DeleteObserver(Observer observer)
        {
            
            string statement = $"DELETE FROM observer WHERE lastname = @lastname";

            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                using var command = new NpgsqlCommand(statement, connection);
                command.Parameters.AddWithValue("lastname", observer.LastName);
                command.Parameters.AddWithValue("firstname", observer.FirstName);

                return command.ExecuteNonQuery();
               
            }
            catch (PostgresException ex) //kan inte radera om det finns observationer. Här hanteras det.
            {
                throw new Exception($"{observer} kunde inte raderas från databasen, eftersom observatören har registrerad/e observation/er. Markera vad du vill ska hända med dessa observationer ", ex);
            }

        } 

        #endregion



    }
}

