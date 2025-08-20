package com.xarvio.fieldmanager.controllers;

import java.util.UUID;
import lombok.RequiredArgsConstructor;

import com.xarvio.fieldmanager.models.Field;
import com.xarvio.fieldmanager.models.SeedingRecommendation;
import com.xarvio.fieldmanager.services.DemoDataService;

import org.springframework.web.bind.annotation.RestController;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;

@RestController
@RequiredArgsConstructor
public class SeedingController {

    private final DemoDataService demoDataService;

    @GetMapping("api/fields/{fieldId}/seeding")
    public ResponseEntity<SeedingRecommendation> getSeeding(@PathVariable UUID fieldId) {
        return demoDataService.getField(fieldId)
                .map(Field::seedingRecommendation)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

}
