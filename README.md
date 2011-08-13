# Mintest

Minimal test framework for .Net

## Intro

**Mintest** is intended to a minimal testing library in the same spirit
of the xUnit family.

It is easy embedable within any of your projects (being *~80 lines of
code*) and at the same time, fairly complete.

It was born from a C# project where I wasn't allowed to have any
dependencies.

Remember, never go without testing. ;-).

## Usage

Create your test:

``` c#
using Mintest;

public class MyCalculatorTest {

  [Test]
  public void testAdd() {
    int expected = 3;
    int actual = Calculator.Add( 1 , 2 );
    Assert.AssertEquals<int>(expected, actual);
  }

  [Test]
  public void testNegativeAdd() {
    Assert.AssertTrue("Should result in a negative number", Calculator.Add(5,-99) < 0);
  }

  [Test]
  public void testToExponentialString() {
    Assert.AssertEquals<string>("5x10^3", Calculator.ToExpString(5000));
  }

  [Test]
  public void andAnotherScenarioWithException() {
    try {
      Calculator.AddIntStrings( "4", "asf" );
      Assert.Fail("An exception should have been thrown.");
    } catch(Exception ex) {
      Assert.Pass();
    }
  }
}
```

Create the TestRunner which intends to run all your tests.

``` c#
using Mintest;

public class TestsMain {

  public static void Main() {
    // Create the TestRunner
    TestRunner testRunner = new TestRunner();

    // Add as many tests as you want.
    testRunner.AddTest( typeof(MyCalculatorTest) );

    // Run them all!
    bool allTestsPassed = testRunner.Run();

    if( allTestsPassed ) {
      System.Console.WriteLine("This is ready for release!");
    }
  }
}
```

Compile it and run it!

```
#> csc /main:TestsMain /out:tests.exe *.cs
#> tests
```

## Notes

1) *Yes*: 'Mintest' stands for 'the newest', 'in the most pristine
condition'... or whatever.
2) I *hate* Microsoft's general convention (Pascal's?) of first letter uppercasing, specially on method names.
