using klimatapp.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace klimatapp.Repositories
{
    public class KlimatRepos
    {
        private static readonly string connectionString = "Server=localhost;Port=5432;Database=Klimatobservationer;User ID=yoda;Password=force;";

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

        ///// <summary>
        ///// Gets list of categories
        ///// </summary>
        ///// <returns>categories</returns>
        //public List<Category> GetCategories()
        //{
        //    string statement = "select * from category";
        //    using var connection = new NpgsqlConnection(connectionString);
        //    connection.Open();
        //    using var command = new NpgsqlCommand(statement, connection);

        //    using var reader = command.ExecuteReader();
        //    Category category = null;
        //    var categories = new List<Category>();
        //    while (reader.Read())
        //    {
        //        category = new Category
        //        {
        //            Id = (int)reader["id"],
        //            Name = (string)reader["name"],
        //            Basecategory_id = Convert.IsDBNull(reader["basecategory_id"]) ? null : (int?)reader["basecategory_id"],
        //            Unit_id = Convert.IsDBNull(reader["unit_id"]) ? null : (int?)reader["unit_id"]
        //        };
        //    }
        //    return categories;
        //}

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
        public Observer GetObserver(int id)
        {
            string statement = "select * from observer where id = @id";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var command = new NpgsqlCommand(statement, connection);

            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();
            Observer observer = null;
            while (reader.Read())
            {
                observer = new Observer
                {
                    Id = (int)reader["id"],
                    FirstName = (string)reader["firstname"],
                    LastName = (string)reader["lastname"]
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
                    Date = (string)reader["date"],
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
        public List<Observation> GetObservations()
        {
            string statement = "select * from observation";
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
                    Date = (string)reader["date"],
                    ObserverId = (int)reader["observer_id"],
                    GeolocationId = (int)reader["geolocation_id"]
                };
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
            string statement = "SELECT firstname, lastname FROM observer ORDER BY lastname";


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
            }
            return observers;
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

        public Observation AddObservation(Observation observation)
        {
            string statement = "INSERT INTO observation(observerid, geolocationid, date) VALUES (@observerid, @geolocationid, @date) RETURNING id";
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
                throw new Exception("Du, det här är fel! Skärp dig, för helvete!");
            }

        }

        public Observation AddObservationWithMultipleValues(Observation observation, Measurement measurement, Category category, Unit unit)
        {
            string statement1 = "INSERT INTO observation(observerid, geolocationid, date) VALUES (@observerid, @geolocationid, @date) RETURNING id";
            string statement2 = "INSERT INTO measurement(value, observationid, categoryid) VALUES (@value, @observationid, @categoryid) RETURNING id";
            string statement3 = "INSERT INTO unit(type, abbreviation) VALUES (@type, @abbreviation) RETURNING id";

            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                using var command = new NpgsqlCommand(statement1, connection);
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
                throw new Exception("Du, det här är fel! Skärp dig, för helvete!");
            }

        }

        #endregion

        #region UPDATE



        #endregion

        #region DELETE

        public int DeleteObserver(Observer observer)
        {
            string statement = "DELETE FROM observer WHERE observer = @id";

            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                using var command = new NpgsqlCommand(statement, connection);
                command.Parameters.AddWithValue("id", observer.Id);

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

