using System;
using System.Collections.Generic;
using System.Text;

namespace klimatapp.Models
{
    /// <summary>
    /// National park area
    /// </summary>
    public class Area
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of area
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Foreign key
        /// </summary>
        public int CountryId { get; set; }

        public override string ToString()
        {
            return Name; 
        }

    }
}
