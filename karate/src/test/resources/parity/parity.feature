Feature: API parity .NET vs Spring

Background:
  * def legacy = karate.get('legacy')
  * def modern = karate.get('modern')

  # Discover a fieldId from legacy /api/fields
  Given url legacy + '/api/fields'
  When method get
  Then status 200
  * def fields = response
  * def fieldId =
    """
    function(f){
      if (Array.isArray(f) && f.length) return f[0].id;
      if (f && Array.isArray(f.fieldSummary) && f.fieldSummary.length) return f.fieldSummary[0].id;
      if (f && Array.isArray(f.fields) && f.fields.length) return f.fields[0].id;
      return null;
    }
    """
  * def FIELD_ID = fieldId(fields)
  * match FIELD_ID != null

Scenario Outline: Endpoint parity <ep>
  # Legacy
  Given url legacy + <ep>
  When method get
  Then status 200
  * def A = response

  # Spring
  Given url modern + <ep>
  When method get
  Then status 200
  * def B = response

  # Deep equality; if you need tolerances, adjust here (e.g., map numbers via JS)
  Then match B == A

  Examples:
    | ep |
    | '/api/fields' |
    | '/api/fields/' + FIELD_ID + '/monitor' |
    | '/api/fields/' + FIELD_ID + '/seeding' |
    | '/api/fields/' + FIELD_ID + '/nutrition' |
    | '/api/fields/' + FIELD_ID + '/protection' |
