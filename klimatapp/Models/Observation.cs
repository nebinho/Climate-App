using System;
using System.Collections.Generic;
using System.Text;

namespace klimatapp.Models
{
    /// <summary>
    /// Made observations
    /// </summary>
    public class Observation
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Date of made observation
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Foreign key from observer
        /// </summary>
        public int ObserverId { get; set; }
        /// <summary>
        /// Foreign key from geolocation
        /// </summary>
        public int GeolocationId { get; set; }
        public override string ToString()
        {
            return $"{Date}";
        }
    }
}
