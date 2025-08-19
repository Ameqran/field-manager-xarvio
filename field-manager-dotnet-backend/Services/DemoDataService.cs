using FieldManagerDotnetBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldManagerDotnetBackend.Services
{
    /// <summary>
    /// DemoDataService holds in-memory sample data to imitate a data store.
    /// This allows the API controllers to retrieve information without
    /// connecting to an actual database. In a real-world application this
    /// would be replaced by a proper repository/service layer accessing
    /// persistent storage or external APIs.
    /// </summary>
    public class DemoDataService
    {
        private readonly List<Field> _fields;

        public DemoDataService()
        {
            // Seed with a couple of example fields and associated data.
            _fields = new List<Field>
            {
                new Field
                {
                    Id = Guid.NewGuid(),
                    Name = "North Farm",
                    ZoneData = new ZoneData
                    {
                        BiomassIndex = new[] { 0.65, 0.70, 0.80, 0.75 },
                        WeatherForecast = new[] { "Rain", "Cloudy", "Sunny", "Sunny" },
                        SoilType = "Loam",
                        HistoricYield = 7.5
                    },
                    SeedingRecommendation = new SeedingRecommendation
                    {
                        AverageDensity = 300,
                        VariableRateMap = new Dictionary<string, int>
                        {
                            { "ZoneA", 280 },
                            { "ZoneB", 320 },
                            { "ZoneC", 310 }
                        }
                    },
                    NutritionRecommendation = new NutritionRecommendation
                    {
                        NutrientRequirements = new Dictionary<string, double>
                        {
                            { "N", 100 },
                            { "P", 60 },
                            { "K", 80 }
                        },
                        SuggestedFertilizers = new List<string> { "Urea", "DAP", "MOP" }
                    },
                    ProtectionRecommendation = new ProtectionRecommendation
                    {
                        DiseaseRisk = "Medium",
                        InsectRisk = "Low",
                        RecommendedProducts = new List<string> { "Fungicide A", "Insecticide B" }
                    }
                },
                new Field
                {
                    Id = Guid.NewGuid(),
                    Name = "South Field",
                    ZoneData = new ZoneData
                    {
                        BiomassIndex = new[] { 0.55, 0.60, 0.50, 0.65 },
                        WeatherForecast = new[] { "Sunny", "Sunny", "Rain", "Cloudy" },
                        SoilType = "Clay",
                        HistoricYield = 6.0
                    },
                    SeedingRecommendation = new SeedingRecommendation
                    {
                        AverageDensity = 280,
                        VariableRateMap = new Dictionary<string, int>
                        {
                            { "ZoneA", 260 },
                            { "ZoneB", 290 },
                            { "ZoneC", 250 }
                        }
                    },
                    NutritionRecommendation = new NutritionRecommendation
                    {
                        NutrientRequirements = new Dictionary<string, double>
                        {
                            { "N", 90 },
                            { "P", 50 },
                            { "K", 70 }
                        },
                        SuggestedFertilizers = new List<string> { "Ammonium Nitrate", "MAP", "Potash" }
                    },
                    ProtectionRecommendation = new ProtectionRecommendation
                    {
                        DiseaseRisk = "High",
                        InsectRisk = "Medium",
                        RecommendedProducts = new List<string> { "Fungicide C", "Insecticide D" }
                    }
                }
            };
        }

        public IEnumerable<Field> GetAllFields() => _fields;

        public Field? GetField(Guid id) => _fields.FirstOrDefault(f => f.Id == id);

        // Additional helper methods could be added here to fetch or update data
    }
}