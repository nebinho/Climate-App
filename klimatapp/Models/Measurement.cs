using System;
using System.Collections.Generic;
using System.Text;

namespace klimatapp.Models
{
    /// <summary>
    /// Different measurements
    /// </summary>
    public class Measurement
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Type of measurement
        /// </summary>
        public float? Value { get; set; }
        /// <summary>
        /// foreign key
        /// </summary>
        public int Observation_id { get; set; }
        /// <summary>
        /// foreign key
        /// </summary>
        public int Category_id { get; set; }
    }

}