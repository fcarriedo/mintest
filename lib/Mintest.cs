using System;
using System.Reflection;
using System.Collections.Generic;

/**
 * Minimal testing framework.
 */
namespace Mintest {

  // 'Test' annotation definition. Just a tag attr.
  [AttributeUsage(AttributeTargets.Method)]
  public class Test: Attribute { }

  /**
   * The test runner.
   */
  public class TestRunner {

    private List<Type> allTests = new List<Type>();

    /**
     * Adds a test to the list of execution.
     */
    public void AddTest( Type test ) {
      allTests.Add( test );
    }

    /**
     * Runs all tests. Returns true if all tests passed
     * or false otherwise.
     */
    public bool Run() {
      bool allTestPassed = true;
      foreach( Type test in allTests ) {
        Console.WriteLine( test.Name );
        MethodInfo[] methods = test.GetMethods();
        object testObj = Activator.CreateInstance( test );

        foreach( MethodInfo method in methods ) {
          bool isTestMethod = method.IsDefined( typeof(Test), false ); // Identifying the tagged attribute.
          if( isTestMethod ) {
            Console.Write( "    - "  + method.Name + "...");
            try {
              test.InvokeMember( method.Name, BindingFlags.InvokeMethod, null, testObj, null);
              Console.Write("PASSED ;-)\n");
            } catch( TargetInvocationException tiex ) {
              allTestPassed = false;
              if( tiex.InnerException is AssertError ) {
                Console.Write("FAILED : " + tiex.InnerException.Message + "\n");
              } else {
                Console.Write("ERROR :  \n\t" + tiex.InnerException + "\n");
              }
            }
          }
        }
      }
      return allTestPassed;
    }
  }

  /**
   * Assertion framework
   */
  public class Assert {
    public static void AssertTrue( bool condition ) {
      if( !condition ) throw new AssertError("Condition not met.");
    }
    public static void AssertTrue( string msg, bool condition ) {
      if( !condition ) throw new AssertError( msg );
    }
    public static void AssertEquals<T>(T expected, T actual) {
      if( !expected.Equals( actual ) ) throw new AssertError( "expected '" + expected + "' but got '" + actual + "'" );
    }
    public static void AssertEquals<T>(string msg, T expected, T actual) {
      if( !expected.Equals( actual ) ) throw new AssertError( msg );
    }
    public static void Fail( string msg ) { throw new AssertError( msg ); }
    public static void Pass( ) { /* NoOp */ }
  }

  public class AssertError : Exception {
    public AssertError(string msg) : base( msg ) {}
  }
}
