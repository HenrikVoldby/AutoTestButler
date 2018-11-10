"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\MSTest" /testcontainer:C:\Work\Test\TestSpecifications\MPPensionTests\bin\Debug\MPPensionTests.dll /ResultsFile:TestResults.trx
"C:\Work\Test\Tivoli\packages\SpecFlow.2.3.2\tools\specflow.exe" mstestexecutionreport "C:\Work\Test\TestSpecifications\MPPensionTests\MPPensionTests.csproj"  /out:MyReport.html /testresult:TestResults.trx
del TestResults.trx
