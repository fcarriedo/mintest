## Using Mintest as a compiled library

Although its initial intention was to embed it in your source code and not have any outside dependency, you can also use it as a library (**~5Kb**):

```
#> csc /main:AllTests /reference:Mintest.dll /out:tests.exe
#> tests.exe
```
