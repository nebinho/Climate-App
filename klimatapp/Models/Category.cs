using System;
using System.Collections.Generic;
using System.Text;

namespace klimatapp.Models
{
    /// <summary>
    /// Different measurements
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of category (both base and sub)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// foreign key, self-reference to base
        /// </summary>
        public int? Basecategory_id { get; set; }
        /// <summary>
        /// foreign key
        /// </summary>
        public int? Unit_id { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }

}