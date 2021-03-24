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
        public Area GetArea(int id)
        {
            string statement = "select * from area where id = @id";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();
            Area area = null;
            while (reader.Read())
            {
                area = new Area
                {
                    Id = (int)reader["id"],
                    Name = (string)reader["name"],
                    CountryId = (int)reader["country_id"]
                };
            }
            return area;
        }

        ///// <summary>
        ///// Gets a list of areas from db
        ///// </summary>
        ///// <returns>areas</returns>
        //public List<Area> GetAreas()
        //{
        //    string statement = "select * from area";
        //    using var connection = new NpgsqlConnection(connectionString);
        //    connection.Open();
        //    using var command = new NpgsqlCommand(statement, connection);

        //    using var reader = command.ExecuteReader();
        //    Area area = null;
        //    var areas = new List<Area>();
        //    while (reader.Read())
        //    {
        //        area = new Area
        //        {
        //            Id = (int)reader["id"],
        //            Name = (string)reader["name"],
        //            CountryId = (int)reader["country_id"]
        //        };
        //    }
        //    return areas;
        //}

        /// <summary>
        /// Gets country from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>country</returns>
        public Country GetCountry(int id)
        {
            string statement = "select * from country where id = @id";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();
            Country country = null;
            while (reader.Read())
            {
                country = new Country
                {
                    Id = (int)reader["id"],
                    Name = (string)reader["name"]
                };
            }
            return country;
        }

        ///// <summary>
        ///// Gets list of countries
        ///// </summary>
        ///// <returns>countries</returns>
        //public List<Country> GetCountries()
        //{
        //    string statement = "select * from country";
        //    using var connection = new NpgsqlConnection(connectionString);
        //    connection.Open();
        //    using var command = new NpgsqlCommand(statement, connection);

        //    using var reader = command.ExecuteReader();
        //    Country country = null;
        //    var countries = new List<Country>();
        //    while (reader.Read())
        //    {
        //        country = new Country
        //        {
        //            Id = (int)reader["id"],
        //            Name = (string)reader["name"]
        //        };
        //    }
        //    return countries;
        //}

        /// <summary>
        /// Gets information about measurements in db
        /// </summary>
        /// <param value="id"></param>
        /// <returns>measurement</returns>
        public Measurement GetMeasurement(int id)
        {
            string statement = "select * from measurement where id = @id";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();
            Measurement measurement = null;
            while (reader.Read())
            {
                measurement = new Measurement
                {
                    Id = (int)reader["id"],
                    Value = Convert.IsDBNull(reader["value"]) ? null : (float?)reader["value"],
                    Observation_id = (int)reader["observation_id"],
                    Category_id = (int)reader["category_id"]
                };
            }
            return measurement;
        }

        ///// <summary>
        ///// Gets list of measurements
        ///// </summary>
        ///// <returns>measurements</returns>
        //public List<Measurement> GetMeasurements()
        //{
        //    string statement = "select * from measurement";
        //    using var connection = new NpgsqlConnection(connectionString);
        //    connection.Open();
        //    using var command = new NpgsqlCommand(statement, connection);

        //    using var reader = command.ExecuteReader();
        //    Measurement measurement = null;
        //    var measurements = new List<Measurement>();
        //    while (reader.Read())
        //    {
        //        measurement = new Measurement
        //        {
        //            Id = (int)reader["id"],
        //            Value = Convert.IsDBNull(reader["value"]) ? null : (float?)reader["value"],
        //            Observation_id = (int)reader["observation_id"],
        //            Category_id = (int)reader["category_id"]
        //        };
        //    }
        //    return measurements;
        //}

        /// <summary>
        /// Gets category from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>category</returns>
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
        /// Gets list of categories
        /// </summary>
        /// <returns>categories</returns>
        public List<Category> GetCategories()
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
        /// Gets unit from db
        /// </summary>
        /// <param type="id"></param>
        /// <returns>unit</returns>
        public Unit GetUnit(int id)
        {
            string statement = "select * from unit where id = @id";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();
            Unit unit = null;
            while (reader.Read())
            {
                unit = new Unit
                {
                    Id = (int)reader["id"],
                    Type = (string)reader["type"],
                    Abbreviation = (string)reader["abbreviation"]
                };
            }
            return unit;
        }

        ///// <summary>
        ///// Gets list of Units
        ///// </summary>
        ///// <returns>units</returns>
        //public List<Unit> GetUnits()
        //{
        //    string statement = "select * from unit";
        //    using var connection = new NpgsqlConnection(connectionString);
        //    connection.Open();
        //    using var command = new NpgsqlCommand(statement, connection);

        //    using var reader = command.ExecuteReader();
        //    Unit unit = null;
        //    var units = new List<Unit>();
        //    while (reader.Read())
        //    {
        //        unit = new Unit
        //        {
        //            Id = (int)reader["id"],
        //            Type = (string)reader["type"],
        //            Abbreviation = (string)reader["abbreviation"],
        //        };
        //    }
        //    return units;
        //}

        /// <summary>
        /// Gets observer from db
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
                    //FirstName = (string)reader["firstname"],
                    //LastName = (string)reader["lastname"]
                };

            }
            return observer;
        }

        /// <summary>
        /// Gets list of observers
        /// </summary>
        /// <returns>observers</returns>
        public List<Observer> GetObservers()
        {
            string statement = "select * from observer";
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
            }
            return observers;
        }

        /// <summary>
        /// Gets observation from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>observation</returns>
        public Observation GetObservation(int id)
        {
            string statement = "select * from observation where id = @id";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();
            Observation observation = null;
            while (reader.Read())
            {
                observation = new Observation
                {
                    Id = (int)reader["id"],
                    Date = reader.GetFieldValue<DateTime>(reader.GetOrdinal("date")),
                    ObserverId = (int)reader["observer_id"],
                    GeolocationId = (int)reader["geolocation_id"]
                };
            }
            return observation;
        }

        /// <summary>
        /// Gets list of observations
        /// </summary>
        /// <returns>observations</returns>
        public List<Observation> GetObservations(Observer observer)
        {
            string statement = $"select date from observation WHERE observer_id = {observer.Id}";
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
                    //Id = (int)reader["id"],
                    Date = reader.GetFieldValue<DateTime>(reader.GetOrdinal("date"))
                    //ObserverId = (int)reader["observer_id"],
                    //GeolocationId = (int)reader["geolocation_id"]
                };
                observations.Add(observation);
            }
            return observations;
        }

        /// <summary>
        /// Gets geolocation from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns>geolocation</returns>
        public Geolocation GetGeolocation(int id)
        {
            string statement = "select * from geolocation where id = @id";
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
                    Latitude = Convert.IsDBNull(reader["latitude"]) ? null : (int?)reader["latitude"],
                    Longitude = Convert.IsDBNull(reader["longitude"]) ? null : (int?)reader["longitude"],
                    AreaId = (int)reader["area_id"]
                };
            }
            return geolocation;
        }

        ///// <summary>
        ///// Gets list of geolocations
        ///// </summary>
        ///// <returns>geolocations</returns>
        //public List<Geolocation> GetGeolocations()
        //{
        //    string statement = "SELECT * FROM geolocation";
        //    using var connection = new NpgsqlConnection(connectionString);
        //    connection.Open();
        //    using var command = new NpgsqlCommand(statement, connection);

        //    using var reader = command.ExecuteReader();
        //    Geolocation geolocation = null;
        //    var geolocations = new List<Geolocation>();
        //    while (reader.Read())
        //    {
        //        geolocation = new Geolocation
        //        {
        //            Id = (int)reader["id"],
        //            Latitude = Convert.IsDBNull(reader["latitude"]) ? null : (int?)reader["latitude"],
        //            Longitude = Convert.IsDBNull(reader["longitude"]) ? null : (int?)reader["longitude"],
        //            AreaId = (int)reader["area_id"]
        //        };
        //    }
        //    return geolocations;
        //}

        /// <summary>
        /// Gets list of observers ordered by last name
        /// </summary>
        /// <returns>observers</returns>
        public List<Observer> GetObserversByLastName()
        {
            string statement = $"SELECT firstname, lastname FROM observer ORDER BY lastname";


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
                    FirstName = (string)reader["firstname"],
                    LastName = (string)reader["lastname"]
                };
                observers.Add(observer);
            }
            return observers;
        }

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

        #endregion

        #region Create

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
                    observer.LastName = (string)reader["lastname"];
                }
                
                return observer;
            }
            catch (PostgresException ex)
            {
                string errorcode = ex.SqlState;
                throw new Exception("Du måste ange både förnamn och efternamn", ex);
            }

        }

        // Behövs datetime som indataparameter ifall man vill skriva in
        public Observation AddObservation(Observation observation, Area area)
        {
            string statement = $"INSERT INTO observation(observerid, geolocationid, date) VALUES (@observerid, @geolocationid, @date) " +
                $"SELECT geolocationid FROM geolocation WHERE areaid = {area.Id} RETURNING id";
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                using var command = new NpgsqlCommand(statement, connection);

                command.Parameters.AddWithValue("observerid", observation.ObserverId);
                command.Parameters.AddWithValue("geolocationid", observation.GeolocationId);
                command.Parameters.AddWithValue("date", observation.Date);
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
                throw new Exception("Du, det här är fel! Du måste välja område för observationen!");
            }
        }

        public Measurement AddMeasurement(Measurement measurement, Observation observation, Category category)
        {
            string statement = $"INSERT INTO measurement(value, observationid, categoryid) VALUES(@value, @observationid, @categoryid) " +
                $"SELECT observationid, categoryid FROM observation, category WHERE observationid = {observation.Id}, categoryid = {category.Id} RETURNING id";
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                using var command = new NpgsqlCommand(statement, connection);

                command.Parameters.AddWithValue("value", measurement.Value ?? Convert.DBNull);
                command.Parameters.AddWithValue("observationid", measurement.Observation_id);
                command.Parameters.AddWithValue("categoryid", measurement.Category_id);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    measurement.Id = (int)reader["id"];
                }

                return measurement;
            }
            catch (PostgresException ex)
            {
                string errorcode = ex.SqlState;
                throw new Exception("Du det här är fel! Du måste välja en kategori för mätpunkten!", ex);
            }

        }


        public Observation AddObservationWithMultipleValues(Observation observation, Measurement measurement, Category category, Area area)
        {
            string stmt1 = "INSERT INTO observation(observerid, geolocationid, date) VALUES (@observerid, @geolocationid, @date) RETURNING id";
            string stmt2 = "INSERT INTO measurement(value, observationid, categoryid) VALUES (@value, @observationid, @categoryid) RETURNING id";

            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var transaction = connection.BeginTransaction();

            using var cmd = new NpgsqlCommand(stmt1, connection);
            try
            {
                transaction.Commit();
                return observation;

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

                throw new Exception("Det gick inte att lägga till en observation", ex);
            }

            using var cmd2 = new NpgsqlCommand(stmt2, connection);
            try
            {
                transaction.Commit();
                return observation;

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

                throw new Exception("Det gick inte att lägga till en observation", ex);
            }

            //try
            //{


            //using (var cmd = new NpgsqlCommand())
            //{
            //    cmd.Connection = connection;
            //    cmd.CommandText = "INSERT INTO observation(observerid, geolocationid, date) VALUES (@observerid, @geolocationid, @date) RETURNING id";
            //    cmd.Parameters.AddWithValue("observerid", observation.ObserverId);
            //    cmd.Parameters.AddWithValue("geolocationid", observation.GeolocationId);
            //    cmd.Parameters.AddWithValue("date", observation.Date);
            //    cmd.Parameters.AddWithValue("observationid", observation.Id);
            //    cmd.ExecuteNonQuery();
            //    cmd.Parameters.Clear();

            //    transaction.Commit();
            //}

            //}
            //catch (PostgresException ex)
            //{
            //    transaction.Rollback();
            //    string errorCode = ex.SqlState;
            //    switch (errorCode)
            //    {
            //        default:
            //            break;
            //    }

            //    throw new Exception("Det gick inte att lägga till en observation", ex);
            //}

            //try
            //{
            //using var command1 = new NpgsqlCommand(statement1, connection);
            //command1.Parameters.AddWithValue("observerid", observation.ObserverId);
            //command1.Parameters.AddWithValue("geolocationid", observation.GeolocationId);
            //command1.Parameters.AddWithValue("date", observation.Date);
            //command1.Parameters.AddWithValue("observationid", observation.Id);
            //command1.ExecuteNonQuery():
            //command1.Parameters.Clear();

            //using var command2 = new NpgsqlCommand(statement2, connection);
            //command2.Parameters.AddWithValue("observerid", observation.ObserverId);
            //command2.Parameters.AddWithValue("geolocationid", observation.GeolocationId);
            //command2.Parameters.AddWithValue("date", observation.Date);
            //command2.Parameters.AddWithValue("observationid", observation.Id);
            //command2.ExecuteNonQuery():
            //command2.Parameters.Clear();



            //    transaction.Commit();
            //    return observation;


            //}
            //catch (PostgresException ex)
            //{
            //    string errorcode = ex.SqlState;
            //    throw new Exception("Du, det här är fel! Skärp dig, för helvete!");
            //}

        }



        #endregion

        #region UPDATE



        #endregion

        #region DELETE

        public int DeleteObserver(Observer observer)
        {
            //var pos = name.IndexOf(' ');
            //var fname = name.Substring(0, pos);
            //var lname = name.Substring(name.LastIndexOf(' ') + 1);
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
            catch (PostgresException ex)
            {
                throw new Exception($"{observer} kunde inte raderas från databasen, eftersom observatören har registrerad/e observation/er. Markera vad du vill ska hända med dessa observationer ", ex);
            }

        }

        #endregion



    }
}

