package com.xarvio.fieldmanager.models;

import java.util.List;
import java.util.Map;

public record NutritionRecommendation(
        Map<String, Double> nutrientRequirements,
        List<String> suggestedFerilizers) {
}
