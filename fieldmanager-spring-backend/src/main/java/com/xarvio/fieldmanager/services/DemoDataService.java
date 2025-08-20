package com.xarvio.fieldmanager.services;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.UUID;
import java.util.Optional;

import com.xarvio.fieldmanager.models.*;

import jakarta.annotation.PostConstruct;
import lombok.RequiredArgsConstructor;

import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class DemoDataService {

    private final List<Field> fields = new ArrayList<>();

    @PostConstruct
    void init() {
        var field1Summary = new FieldSummary(UUID.fromString("d1234567-89ab-4cde-f012-3456789abcde"), "North Farm");
        var field1 = new Field(
                field1Summary,
                new ZoneData(new double[] { 0.65, 0.70, 0.80, 0.75 },
                        new String[] { "Rain", "Cloudy", "Sunny", "Sunny" },
                        "Loam",
                        7.5),
                new SeedingRecommendation(300, Map.of(
                        "ZoneA", 280,
                        "ZoneB", 320,
                        "ZoneC", 310)),
                new NutritionRecommendation(Map.of(
                        "N", 100.0,
                        "P", 60.0,
                        "K", 80.0), List.of("Urea", "DAP", "MOP")),
                new ProtectionRecommendation("Medium", "Low", List.of("Fungicide A", "Insecticide B")));
        var field2Summary = new FieldSummary(UUID.fromString("d1234567-89ab-4cde-f012-3456789abcda"), "South Field");
        var field2 = new Field(
                field2Summary,
                new ZoneData(new double[] { 0.55, 0.60, 0.50, 0.65 },
                        new String[] { "Sunny", "Sunny", "Rain", "Cloudy" },
                        "Clay",
                        6.0),
                new SeedingRecommendation(280, Map.of(
                        "ZoneA", 260,
                        "ZoneB", 290,
                        "ZoneC", 250)),
                new NutritionRecommendation(Map.of(
                        "N", 90.0,
                        "P", 50.0,
                        "K", 70.0), List.of("Ammonium Nitrate", "MAP", "Potash")),
                new ProtectionRecommendation("High", "Medium", List.of("Fungicide C", "Insecticide D")));

        fields.addAll(List.of(field1, field2));
    }

    public List<Field> getAllFields() {
        return fields;
    }

    public Optional<Field> getField(UUID id) {
        return fields.stream().filter(field -> id.equals(field.fieldSummary().id())).findFirst();
    }

}
