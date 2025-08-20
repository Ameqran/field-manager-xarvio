FROM maven:3.9-eclipse-temurin-21
WORKDIR /karate
COPY pom.xml .
RUN mvn -q -DskipTests dependency:go-offline
COPY src ./src
# default command runs the JUnit5 Karate runner
CMD ["mvn","-q","test"]
