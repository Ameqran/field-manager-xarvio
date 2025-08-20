package com.xarvio.fieldmanager.controllers;

import java.util.UUID;
import lombok.RequiredArgsConstructor;

import com.xarvio.fieldmanager.models.Field;
import com.xarvio.fieldmanager.models.ZoneData;
import com.xarvio.fieldmanager.services.DemoDataService;

import org.springframework.web.bind.annotation.RestController;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;

@RestController
@RequiredArgsConstructor
public class FieldMonitorController {

    private final DemoDataService demoDataService;

    @GetMapping("api/fields/{fieldId}/monitor")
    public ResponseEntity<ZoneData> getFieldMonitor(@PathVariable UUID fieldId) {
        return demoDataService.getField(fieldId)
                .map(Field::zoneData)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

}
