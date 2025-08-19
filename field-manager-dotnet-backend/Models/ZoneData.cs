using System;

namespace FieldManagerDotnetBackend.Models
{
    /// <summary>
    /// Encapsulates information about field zones used in the Field Monitor
    /// feature. This includes biomass indices, weather forecasts, soil type
    /// and historic yield data.
    /// </summary>
    public class ZoneData
    {
        /// <summary>
        /// Normalised biomass indices for the field's zones.
        /// </summary>
        public double[] BiomassIndex { get; set; } = Array.Empty<double>();

        /// <summary>
        /// Short range weather forecast for the field. Each entry corresponds
        /// to a day.
        /// </summary>
        public string[] WeatherForecast { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Type of soil (e.g. loam, clay) for the field.
        /// </summary>
        public string SoilType { get; set; } = string.Empty;

        /// <summary>
        /// Historic average yield (e.g. tonnes per hectare).
        /// </summary>
        public double HistoricYield { get; set; }
    }
}