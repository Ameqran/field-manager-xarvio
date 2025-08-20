package com.xarvio.fieldmanager.models;

import java.util.List;

public record ProtectionRecommendation(
        String diseaseRisk,
        String insectRisk,
        List<String> recommendedProducts) {
}
