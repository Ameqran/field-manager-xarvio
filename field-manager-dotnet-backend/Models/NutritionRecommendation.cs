using System.Collections.Generic;

namespace FieldManagerDotnetBackend.Models
{
    /// <summary>
    /// Provides nutrient requirements and suggested fertilisers for a field.
    /// Nutrient requirements specify the kilograms per hectare for each
    /// nutrient element, while suggested fertilisers list fertiliser products
    /// that can supply those nutrients.
    /// </summary>
    public class NutritionRecommendation
    {
        /// <summary>
        /// Key-value pairs where the key is a nutrient symbol (e.g. "N",
        /// "P", "K") and the value is the amount needed per hectare.
        /// </summary>
        public Dictionary<string, double> NutrientRequirements { get; set; } = new();

        /// <summary>
        /// Names of fertiliser products recommended to satisfy the nutrient
        /// requirements.
        /// </summary>
        public List<string> SuggestedFertilizers { get; set; } = new();
    }
}