package com.xarvio.fieldmanager.models;

import java.util.Map;

public record SeedingRecommendation(
        int averageDensity,
        Map<String, Integer> variableRateMap) {
}
