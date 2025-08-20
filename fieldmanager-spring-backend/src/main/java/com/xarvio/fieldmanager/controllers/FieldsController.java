package com.xarvio.fieldmanager.controllers;

import java.util.List;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

import com.xarvio.fieldmanager.models.FieldSummary;
import com.xarvio.fieldmanager.services.DemoDataService;

import lombok.RequiredArgsConstructor;

@RestController
@RequiredArgsConstructor
public class FieldsController {

    private final DemoDataService demoDataService;

    @GetMapping("api/fields")
    public ResponseEntity<List<FieldSummary>> getAllFields() {
        var fields = demoDataService.getAllFields().stream()
                .map(field -> field.fieldSummary()).toList();
        return ResponseEntity.ok(fields);
    }
}
