using System.Collections.Generic;

namespace FieldManagerDotnetBackend.Models
{
    /// <summary>
    /// Represents recommendations for seed densities across a field. Variable
    /// rate maps provide the seeds per unit area for specific zones, while
    /// average density denotes a uniform rate if zone differentiation is not
    /// used.
    /// </summary>
    public class SeedingRecommendation
    {
        /// <summary>
        /// Average seeding density for the field when not using zone-specific
        /// rates (e.g. seeds per square metre).
        /// </summary>
        public int AverageDensity { get; set; }

        /// <summary>
        /// Variable rate seeding recommendations keyed by zone identifier.
        /// </summary>
        public Dictionary<string, int> VariableRateMap { get; set; } = new();
    }
}