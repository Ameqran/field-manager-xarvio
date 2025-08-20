package com.xarvio.fieldmanager.controllers;

import java.util.UUID;
import lombok.RequiredArgsConstructor;

import com.xarvio.fieldmanager.models.Field;
import com.xarvio.fieldmanager.models.NutritionRecommendation;
import com.xarvio.fieldmanager.services.DemoDataService;

import org.springframework.web.bind.annotation.RestController;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;

@RestController
@RequiredArgsConstructor
public class NutritionController {

    private final DemoDataService demoDataService;

    @GetMapping("api/fields/{fieldId}/nutrition")
    public ResponseEntity<NutritionRecommendation> getNutrition(@PathVariable UUID fieldId) {
        return demoDataService.getField(fieldId)
                .map(Field::nutrientRequirements)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

}
