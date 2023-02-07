import org.junit.Rule;
import org.junit.Test;
import org.junit.rules.ExpectedException;



public class UnitTest {
        @Rule
        public ExpectedException expectedException = ExpectedException.none();

        @Test
        public void throwsNothing() {
            // no exception expected, none thrown: passes.
        }

        @Test
        public void throwsExceptionWithSpecificType() {
            expectedException.expect(NullPointerException.class);

            throw new NullPointerException();
        }

        @Test
        public void throwsExceptionWithSpecificTypeAndMessage() {
            expectedException.expect(IllegalArgumentException.class);
            expectedException.expectMessage("Wanted a donut.");
            throw new IllegalArgumentException("Wanted a donut.");
        }

}