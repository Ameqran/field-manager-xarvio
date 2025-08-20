FROM alpine:3.20
RUN apk add --no-cache curl ca-certificates diffutils
WORKDIR /tester
COPY golden.sh .
RUN chmod +x golden.sh
ENTRYPOINT ["/tester/golden.sh"]
