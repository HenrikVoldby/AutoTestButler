using System;
using System.Diagnostics;
using System.Reflection;

namespace TestExecutor
{
    class Program
    {
        static void Main(string[] args)
        {
            VerifyArgs(args);

            string sProcess = @"C:\windows\system32\cmd.exe";
            string sParam = $@"C:\Work\Test\TestSpecifications\{args[0]}Tests\TestReporting\TestReporting.cmd";
            string cmd = String.Format(" /k {0}", sParam);
            Process.Start(sProcess, cmd);
        }

        static void VerifyArgs(string[] args)
        {
            if (args.Length != 1)
                throw new ArgumentException(nameof(args));
        }
    }
}
