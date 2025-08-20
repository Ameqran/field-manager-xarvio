package parity;

import com.intuit.karate.junit5.Karate;

class ParityRunner {
    @Karate.Test
    Karate testAll() {
        return Karate.run("parity").relativeTo(getClass());
    }
}
