package com.xarvio.fieldmanager.models;

public record ZoneData(
        double[] biomassIndex,
        String[] weatherForecast,
        String soilType,
        double historicYield) {
}
