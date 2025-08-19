## Field Manager Backend (Demo)

This repository contains a lightweight ASP.NET Core Web API that emulates
the core services offered by **xarvio FIELD MANAGER**. It provides
endpoints for:

* **Field Monitor** – View zone‑specific biomass indices, weather
  forecasts, soil type and historic yields.
* **Seeding** – Obtain variable‑rate and average seeding density
  recommendations for each field.
* **Crop Nutrition** – Inspect nutrient requirements (N, P, K) and
  suggested fertiliser products.
* **Crop Protection** – Discover disease/insect risk levels and
  recommended protection products.

### Structure

* `Controllers` – API endpoints for each feature.
* `Models` – Plain CLR classes representing the domain objects.
* `Services/DemoDataService` – In‑memory data store used by the
  controllers.
* `Program.cs` – Application entry point configuring dependency
  injection and routing.

### Running the API

You need the .NET 8 SDK installed. Navigate to the project folder and
run:

```sh
dotnet run --project field-manager-backend/FieldManagerDotnetBackend.csproj
```

The API will start on `http://localhost:5000`. You can explore the
endpoints via cURL or Postman. For example:

```sh
curl http://localhost:5000/api/fields
# → returns list of fields

curl http://localhost:5000/api/fields/<FIELD_ID>/monitor
# → returns zone data for the field

curl http://localhost:5000/api/fields/<FIELD_ID>/seeding

curl http://localhost:5000/api/fields/<FIELD_ID>/nutrition

curl http://localhost:5000/api/fields/<FIELD_ID>/protection
```

### Extending

This demo uses static sample data. In a real application, you would
replace `DemoDataService` with a repository connected to a database or
external services, and implement authentication/authorisation. You
could also add endpoints for uploading soil maps, generating variable
application maps, or integrating with external machinery terminals to
resemble the full Field Manager offering.