# FieldManager — .NET → Spring Boot Migration Demo

This repository shows how to migrate a small .NET backend to Java Spring Boot while keeping the same API contracts. It also includes **Karate** [`karate`](karate) tests to check **golden parity** [`parity.feature`](karate/src/test/resources/parity/parity.feature) (the new service returns the same JSON as the old one).

> Goal: build a small demo, prove migration steps, and show a repeatable testing pipeline.

---

## 1) Repo layout

```
FieldManager/
├─ dotnet/     # ASP.NET Core Web API (legacy)
├─ spring/     # Spring Boot (migrated)
├─ karate/     # Karate tests & runner
└─ docker-compose.yml
```

---

## 2) Project prompt (copy‑paste)

Use this prompt to describe the demo to others (or to generate code or test data). It includes **features**, **services**, **endpoints**, and **JSON payloads**.

> **Prompt:**
> Build a single backend for a small “Field Manager” app (similar to xarvio Field Manager) in **ASP.NET Core**. Implement 4 feature areas:
>
> 1. **Field Monitor** (biomass, soil, yield, weather)
> 2. **Seeding** (variable‑rate and average density)
> 3. **Crop Nutrition** (N/P/K requirements + suggested fertilizers)
> 4. **Crop Protection** (disease/insect risk + recommended products)
>
> **Endpoints**:
>
> * `GET /api/fields` → list fields
>
>   ```json
>   [
>     { "id": "ba560920-54d8-4183-9632-ec2443f84cbe", "name": "North Farm" },
>     { "id": "ea6d0b34-b0f7-45b6-8074-174f6047197d", "name": "South Field" }
>   ]
>   ```
>
>   *(Note: discovery may also return ****`{ "fieldSummary": [ {id,name}... ] }`****.)*
>
> * `GET /api/fields/{id}/monitor` → zone monitor data
>
>   ```json
>   {
>     "biomassIndex": 0.62,
>     "soilType": "Loam",
>     "historicYieldTPerHa": 7.4,
>     "weatherForecast": [
>       { "hour": "2025-08-19T12:00Z", "tempC": 24.1, "rainMm": 0.0 },
>       { "hour": "2025-08-19T13:00Z", "tempC": 25.0, "rainMm": 0.2 }
>     ]
>   }
>   ```
>
> * `GET /api/fields/{id}/seeding` → variable‑rate seeding
>
>   ```json
>   {
>     "averageDensity": 300,
>     "variableRateMap": {
>       "zoneA": 280,
>       "zoneB": 320,
>       "zoneC": 300
>     }
>   }
>   ```
>
> * `GET /api/fields/{id}/nutrition` → nutrient needs
>
>   ```json
>   {
>     "nutrientRequirements": { "N": 100, "P": 60, "K": 80 },
>     "suggestedFertilizers": [ "Urea", "DAP", "MOP" ]
>   }
>   ```
>
> * `GET /api/fields/{id}/protection` → risk + products
>
>   ```json
>   {
>     "diseaseRisk": "Medium",
>     "insectRisk": "Low",
>     "recommendedProducts": [ "Fungicide A", "Insecticide B" ]
>   }
>   ```
>
> **Services / layers**: simple controller → service → (in‑memory) repository. Keep JSON keys **exactly** as shown above.

---

## 3) How to run

### 3.1 Run everything with Docker Compose

```bash
# from repo root
docker compose up --build --abort-on-container-exit --exit-code-from karate
```

* Starts **dotnet** and **spring**, waits for health checks, then runs **karate**.
* Reports generated under `karate/target/` (HTML + JUnit XML).

### 3.2 Run Karate locally (without Docker)

```bash
cd karate
mvn -q -Dlegacy=http://localhost:5000 -Dmodern=http://localhost:8080 test
```

* Open `karate/target/karate-reports/karate-summary.html`.

> Tip: Karate is JUnit‑based. It needs no `main()`.

---

## 4) Golden parity (what and why)

**Golden parity** means: for each endpoint, the **new Spring** response is **identical** to the **legacy .NET** response (same keys, types, values). If clients call the new service, they should not notice a difference.

Karate checks this by calling both services and doing a deep JSON match. If there is a mismatch (e.g., `insectRisk` vs `InsectRisk`, or a typo), the report will show a clear diff.

   ```mermaid
---
config:
  look: neo
  layout: elk
---
flowchart LR
 subgraph S1["Step 1 — Golden Record Creation"]
    direction LR
        J["JSON snippets"]
        ST["HTTP Status Codes 200 · 201 · 404 · 500"]
        HD["Headers : Content-Type · X-Trace · Cache-Control"]
        GR[["🟡 Golden Responses : Trusted Truth — Frozen Snapshots"]]
  end
 subgraph S2["Step 2 — Dual System Testing"]
    direction TB
        KAR["👊 Karate Test Suite (Same Test Suite, Different Base URLs)"]
        NET[".NET Legacy Service"]
        SPR["Spring Boot New Service"]
        LEGDB[("Legacy Database")]
  end
 subgraph S3["Step 3 — Comparison Engine"]
    direction TB
        RN["Response A (.NET)"]
        RS["Response B (Spring)"]
        DIFF["🔍 Comparison / Diff Engine Side-by-side JSON view"]
        DEC{"Exact Match?"}
        PASS["✅ Build Passes"]
        FAIL["❌ Build Fails"]
  end
    J --> GR
    ST --> GR
    HD --> GR
    KAR -- BaseURL: /dotnet --> NET
    KAR -- BaseURL: /spring --> SPR
    NET -- Fetch Data --> LEGDB
    SPR -- Fetch Data --> LEGDB
    RN --> DIFF
    RS --> DIFF
    DIFF --> DEC
    DEC -- Yes --> PASS
    DEC -- No --> FAIL
    S1 -- Golden snapshots feed tests --> S2
    S2 -- Captured responses --> S3
     J:::compare
     ST:::compare
     HD:::compare
     GR:::golden
     KAR:::tests
     NET:::legacy
     SPR:::spring
     LEGDB:::legacy
     RN:::legacy
     RS:::spring
     DIFF:::compare
     DEC:::compare
     PASS:::success
     FAIL:::failure
    classDef golden fill:#FFE08A,stroke:#E5B95C,color:#1F2937,rx:14,ry:14
    classDef legacy fill:#D9ECFF,stroke:#9CC4E4,color:#0D2A42,rx:12,ry:12
    classDef spring fill:#E6F6EA,stroke:#B5E3C4,color:#064E3B,rx:12,ry:12
    classDef tests fill:#EDEBFF,stroke:#CFC5FF,color:#312E81,rx:12,ry:12
    classDef compare fill:#F6F8FA,stroke:#CBD5E1,color:#111827,rx:12,ry:12
    classDef success fill:#D1FADF,stroke:#86EFAC,color:#065F46,rx:10,ry:10
    classDef failure fill:#FEE2E2,stroke:#FCA5A5,color:#7F1D1D,rx:10,ry:10
    classDef bottom fill:#F3F4F6,stroke:#E5E7EB,color:#111827,rx:16,ry:16
    classDef invisible fill:transparent,stroke:transparent,color:transparent

```
---

## 5) Migration strategy (expert but simple to follow)

**Approach:** Strangler‑Fig. Migrate one **vertical slice** at a time (Field Monitor → Seeding → Nutrition → Protection). Keep the same URLs and JSON. Use a gateway or feature flags if needed.

**Steps per slice:**

1. **Freeze the contract**: confirm the endpoint shape and sample payloads.
2. **Port the code**: move logic to Spring (controller, service, repo). Keep JSON keys the same (fix typos in code as needed, but do not change the API).
3. **Run Karate**: compare old vs new for that slice. Fix differences until green.
4. **Shadow traffic** (optional): mirror reads to new service in a test/stage env.
5. **Canary**: enable a small % of traffic to Spring. Watch logs/metrics.
6. **Cut over**: route 100% once stable. Keep rollback ready.

**Keep doing this** for the next slice.

---

## 6) Continuous testing after each increment (Karate)

* Add a new scenario (or example) in `parity.feature` for each new endpoint.
* If the listing response is different (e.g., `fieldSummary`), the Background already supports both.
* If you have minor differences (rounding, order of arrays), normalize before `match`:

  * Sort arrays.
  * Map/round numbers.
  * Remove volatile keys (timestamps) with `* remove response.timestamp`.
* Target is **zero differences** before cutover.

---

## 7) CI/CD: run Karate in the pipeline + publish reports

### 7.1 GitHub Actions [`main.yml`](.github/workflows/main.yml)

* The job fails if Karate fails.
* You get the nice HTML report as an artifact.
* The Karate report will be deployed into github pages [`Karate Report`](https://ameqran.github.io/field-manager-xarvio/).

---

## 8) Real‑world add‑ons (how to apply this in a real project)

### 8.1 Security

* Use **JWT/OAuth2** in both stacks. In tests, get a token once, save to a Karate variable, and add `Authorization: Bearer ...` to each request.
* Test authZ (roles/claims) by running Karate with different tokens.
* Keep error format consistent.

### 8.2 Database

* Use **Flyway/Liquibase** for schema migrations.
* For parity tests in CI, run DBs in Docker (e.g., Postgres) with **Testcontainers** (for Java) or a `docker-compose` service.
* Seed the **same data** for both stacks so Karate diffs are fair.

### 8.3 Asynchronous comms

* Use **Kafka** or **RabbitMQ**. For read APIs, parity is simple. For async flows, test the **resulting state** via REST (e.g., `GET /api/fields/{id}` reflects the message consumed).
* In CI, spin up Kafka/RabbitMQ via Docker Compose. Produce a message, wait, assert REST state with Karate.

### 8.4 External APIs

* Add **timeouts**, **retries**, and **circuit breakers** (Spring: Resilience4j). For parity, stub external APIs the same way on both stacks (WireMock).
* Karate can call your stub first to prepare data, then call your service.

### 8.5 Observability

* Include **correlation IDs** in logs. Log the same fields.
* Use **OpenTelemetry + Micrometer** → Prometheus/Grafana. Check p95 latency and error rates during canary.

### 8.6 Error handling

* Map errors to a consistent shape (status, code, message, details). Test with Karate by asserting errors for invalid inputs.

---

## 9) Tips to keep parity green

* Keep JSON keys identical (casing, spelling). Fix typos in code, not the API.
* Normalize timezones (use UTC).
* Define default values (avoid `null` vs `[]` surprises).
* Add a new Karate example **as soon as** you migrate a new endpoint.

---

## 10) Next steps

* Add security; update Karate to send a JWT.
* Swap in Postgres and seed the same demo data.
* Add one async flow (Kafka) and a REST check for the resulting state.
* Add a small API gateway (Traefik or Spring Cloud Gateway) to flip traffic from .NET to Spring per route.

---

## 11) FAQ

* **Why Karate?** It is simple, expressive, works in Docker and CI, and has great reports.
* **What if responses differ by tiny decimals?** Round numbers in the test before `match`.
* **What if arrays come in different order?** Sort them, then match.

---

**That’s it.**

Now we have a clean demo, a migration path, and a way to prove parity on each step.
ps: Keep the tests close to the APIs, and run them on every PR.
