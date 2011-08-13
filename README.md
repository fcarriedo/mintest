# Mintest

Minimal test framework for .Net

## Intro

**Mintest** is intended to a minimal testing library in the same spirit
as the xUnit family.

It was born from a C#

Remember, never go without testing. ;-).

## Usage

Create your test:

``` csharp
using Mintest;

public class MyClassTest {

  [Test]
  public void testScenario() {
    Assert.AssertEquals<int>(1, 1);
    Assert.Pass();
  }

  [Test]
  public void testAnotherScenario() {
    Assert.AssertTrue("Two is never equal to 1 dumbass!!", 2==1);
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
    testRunner.AddTest( typeof(MyClassTest) );

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

*Yes*: 'Mintest' stands for 'the newest', 'in the most pristine
condition'... or whatever.
