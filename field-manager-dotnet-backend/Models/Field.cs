using System;

namespace FieldManagerDotnetBackend.Models
{
    /// <summary>
    /// Represents a field under management. Each field contains zone-specific
    /// data as well as agronomic recommendations for seeding, nutrition and
    /// protection.
    /// </summary>
    public class Field
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ZoneData? ZoneData { get; set; }
        public SeedingRecommendation? SeedingRecommendation { get; set; }
        public NutritionRecommendation? NutritionRecommendation { get; set; }
        public ProtectionRecommendation? ProtectionRecommendation { get; set; }
    }
}