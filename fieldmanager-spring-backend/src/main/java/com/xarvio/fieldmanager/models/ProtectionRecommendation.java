package com.xarvio.fieldmanager.models;

import java.util.List;

public record ProtectionRecommendation(
        String diseaseRisk,
        String InsectRisk,
        List<String> recommendedProducts) {
}
