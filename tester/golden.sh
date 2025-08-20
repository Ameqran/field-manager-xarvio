#!/usr/bin/env sh
set -eu

HOST_DOTNET="${1:-http://dotnet:5000}"
HOST_SPRING="${2:-http://spring:8080}"
OUT_DIR="${3:-/out}"
mkdir -p "$OUT_DIR"

# Install jq if not present (for alpine base)
if ! command -v jq >/dev/null 2>&1; then
  apk add --no-cache jq >/dev/null
fi

norm() { jq -S '.' 2>/dev/null || cat; }   # stable key ordering

echo "Discovering field id from .NET: $HOST_DOTNET ..."
RESULT="$(curl -sf "$HOST_DOTNET/api/fields" | jq -r '.[0]')"

echo $RESULT

FIELD_ID="$(curl -sf "$HOST_DOTNET/api/fields" | jq -r '.[0].id')"

echo $FIELD_ID
[ -n "$FIELD_ID" ] && [ "$FIELD_ID" != "null" ] || { echo "No field result found ==> $FIELD_ID"; exit 3; }

ENDPOINTS="
/api/fields
/api/fields/$FIELD_ID/monitor
/api/fields/$FIELD_ID/seeding
/api/fields/$FIELD_ID/nutrition
/api/fields/$FIELD_ID/protection
"

FAIL=0
for ep in $ENDPOINTS; do
  echo "==> $ep"
  A="$(curl -sf "$HOST_DOTNET$ep"  | norm)" || { echo "ERROR calling .NET $ep"; FAIL=$((FAIL+1)); continue; }
  B="$(curl -sf "$HOST_SPRING$ep"  | norm)" || { echo "ERROR calling Spring $ep"; FAIL=$((FAIL+1)); continue; }
  echo "dotnet response $A"
  echo "spring response $B"
  if diff -u <(echo "$A") <(echo "$B") >"$OUT_DIR$(echo "$ep" | tr '/' '_').diff"; then
    echo "PASS $ep"
    rm -f "$OUT_DIR$(echo "$ep" | tr '/' '_').diff"
  else
    echo "FAIL $ep (see $OUT_DIR$(echo "$ep" | tr '/' '_').diff)"
    FAIL=$((FAIL+1))
  fi
done

[ $FAIL -eq 0 ] && echo "Golden tests: ALL PASS." || echo "Golden tests: $FAIL failing endpoint(s)."
exit $([ $FAIL -eq 0 ] && echo 0 || echo 2)
