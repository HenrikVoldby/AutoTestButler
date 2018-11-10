"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\MSTest" /testcontainer:C:\Work\Test\TestSpecifications\TivoliTests\bin\Debug\TivoliTests.dll /ResultsFile:TestResults.trx
"C:\Work\Test\TestSpecifications\packages\SpecFlow.2.3.2\tools\specflow.exe" mstestexecutionreport "C:\Work\Test\TestSpecifications\TivoliTests\TivoliTests.csproj"  /out:MyReport.html /testresult:TestResults.trx
del TestResults.trx
