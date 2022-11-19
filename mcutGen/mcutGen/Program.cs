using CppAst;
using System;
using System.Diagnostics;
using System.IO;

namespace mcutGen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string include = "..\\..\\..\\..\\..\\Headers\\";
            string mcuth = Path.Combine(include, "mcut.h");

            var options = new CppParserOptions
            {
                ParseMacros = true,
                IncludeFolders = { include },
            };

            var compilation = CppParser.ParseFile(mcuth, options);

            // Print diagnostic messages
            if (compilation.HasErrors)
            {
                foreach (var message in compilation.Diagnostics.Messages)
                {
                    Console.WriteLine(message);
                }
            }
            else
            {
                string outputPath = "..\\..\\..\\..\\Evergine.Bindings.mcut\\Generated";
                CsCodeGenerator.Instance.Generate(compilation, outputPath);
            }
        }
    }
}