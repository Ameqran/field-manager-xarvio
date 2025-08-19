using System.Collections.Generic;

namespace FieldManagerDotnetBackend.Models
{
    /// <summary>
    /// Contains information about disease and insect risk for a field as
    /// well as products recommended for crop protection. These values
    /// represent simplified risk levels and do not reflect real field data.
    /// </summary>
    public class ProtectionRecommendation
    {
        /// <summary>
        /// Estimated disease risk level (e.g. Low, Medium, High).
        /// </summary>
        public string DiseaseRisk { get; set; } = string.Empty;

        /// <summary>
        /// Estimated insect pressure (e.g. Low, Medium, High).
        /// </summary>
        public string InsectRisk { get; set; } = string.Empty;

        /// <summary>
        /// Names of crop protection products recommended for this field.
        /// </summary>
        public List<string> RecommendedProducts { get; set; } = new();
    }
}