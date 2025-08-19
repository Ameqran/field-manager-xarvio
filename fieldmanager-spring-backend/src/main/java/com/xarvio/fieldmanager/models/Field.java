package com.xarvio.fieldmanager.models;

public record Field(
        FieldSummary fieldSummary,
        ZoneData zoneData,
        SeedingRecommendation seedingRecommendation,
        NutritionRecommendation nutrientRequirements,
        ProtectionRecommendation protectionRecommendation) {

}
