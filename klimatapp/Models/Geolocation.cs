using System;
using System.Collections.Generic;
using System.Text;

namespace klimatapp.Models
{
    /// <summary>
    /// Geolocation of an observation
    /// </summary>
    public class Geolocation
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Latitude (null allowed)
        /// </summary>
        public double? Latitude { get; set; }
        /// <summary>
        /// Longitude (null allowed)
        /// </summary>
        public double? Longitude { get; set; }
        /// <summary>
        /// Foreign key from Area
        /// </summary>
        public int AreaId { get; set; }

    }
}
