using System;
using System.Collections.Generic;
using System.Text;

namespace klimatapp.Models
{
    /// <summary>
    /// Different measurements
    /// </summary>
    public class Unit
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Type of measurement
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// unit of measurement
        /// </summary>
        public string Abbreviation { get; set; }
    }

}