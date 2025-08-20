function fn() {
    var config = {};
    // prefer env vars injected by docker-compose
    var envLegacy = java.lang.System.getenv('LEGACY_BASE');
    var envModern = java.lang.System.getenv('MODERN_BASE');
    var propLegacy = karate.properties['legacy'];
    var propModern = karate.properties['modern'];
  
    config.legacy = propLegacy || envLegacy || 'http://dotnet:5000';
    config.modern = propModern || envModern || 'http://spring:8080';
  
    // numeric tolerance (optional): 3 decimal places
    config.round3 = function(x){ return Math.round(x*1000)/1000; };
  
    return config;
  }
  