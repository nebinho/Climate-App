using System;
using System.Collections.Generic;
using System.Text;

namespace klimatapp.Models
{
    /// <summary>
    /// countries to choose from
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of country
        /// </summary>
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
    
}
