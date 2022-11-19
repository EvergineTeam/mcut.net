using CppAst;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcutGen
{
    public class CsCodeGenerator
    {
        private CsCodeGenerator() { }

        public static CsCodeGenerator Instance { get; } = new CsCodeGenerator();

        public void Generate(CppCompilation compilation, string outputPath)
        {
            Helpers.TypedefList = compilation.Typedefs
                    .Where(t => t.TypeKind == CppTypeKind.Typedef
                           && t.ElementType is CppPointerType
                           && ((CppPointerType)t.ElementType).ElementType.TypeKind != CppTypeKind.Function)
                    .Select(t => t.Name).ToList();

            GenerateEnums(compilation, outputPath);
            GenerateDelegates(compilation, outputPath);
            GenerateFunctions(compilation, outputPath);
        }

        private void GenerateEnums(CppCompilation compilation, string outputPath)
        {
            Debug.WriteLine("Generating Enums...");

            using (StreamWriter file = File.CreateText(Path.Combine(outputPath, "Enums.cs")))
            {
                file.WriteLine("using System;\n");
                file.WriteLine("namespace Evergine.Bindings.Mcut");
                file.WriteLine("{");

                foreach (var cppEnum in compilation.Enums)
                {
                    Helpers.PrintComments(file, cppEnum.Comment, "\t");
                    if (compilation.Typedefs.Any(t => t.Name == cppEnum.Name + "Flags"))
                    {
                        file.WriteLine("\t[Flags]");
                    }

                    file.WriteLine($"\tpublic enum {cppEnum.Name}");
                    file.WriteLine("\t{");

                    foreach (var member in cppEnum.Items)
                    {
                        Helpers.PrintComments(file, member.Comment, "\t\t", true);
                        file.WriteLine($"\t\t{member.Name} = {member.Value},");
                    }

                    file.WriteLine("\t}\n");
                }

                file.WriteLine("}");
            }
        }

        private void GenerateDelegates(CppCompilation compilation, string outputPath)
        {
            Debug.WriteLine("Generating Delegates...");

            var delegates = compilation.Typedefs
                .Where(t => t.TypeKind == CppTypeKind.Typedef
                       && t.ElementType is CppPointerType
                       && ((CppPointerType)t.ElementType).ElementType.TypeKind == CppTypeKind.Function)
                .ToList();

            using (StreamWriter file = File.CreateText(Path.Combine(outputPath, "Delegates.cs")))
            {
                file.WriteLine("using System;");
                file.WriteLine("using System.Runtime.InteropServices;\n");
                file.WriteLine("namespace Evergine.Bindings.Mcut");
                file.WriteLine("{");

                foreach (var funcPointer in delegates)
                {
                    Helpers.PrintComments(file, funcPointer.Comment, "\t");
                    CppFunctionType pointerType = ((CppPointerType)funcPointer.ElementType).ElementType as CppFunctionType;

                    var returnType = Helpers.ConvertToCSharpType(pointerType.ReturnType);
                    file.Write($"\tpublic unsafe delegate {returnType} {funcPointer.Name}(");

                    if (pointerType.Parameters.Count > 0)
                    {
                        file.Write("\n");

                        for (int i = 0; i < pointerType.Parameters.Count; i++)
                        {
                            if (i > 0)
                                file.Write(",\n");

                            var parameter = pointerType.Parameters[i];
                            var convertedType = Helpers.ConvertToCSharpType(parameter.Type);
                            file.Write($"\t\t {convertedType} {parameter.Name}");
                        }
                    }

                    file.Write(");\n\n");
                }

                file.WriteLine("}");
            }
        }

        private void GenerateFunctions(CppCompilation compilation, string outputPath)
        {
            Debug.WriteLine("Generating Functions...");

            using (StreamWriter file = File.CreateText(Path.Combine(outputPath, "Functions.cs")))
            {
                file.WriteLine("using System;");
                file.WriteLine("using System.Runtime.InteropServices;\n");
                file.WriteLine("namespace Evergine.Bindings.Mcut");
                file.WriteLine("{");
                file.WriteLine($"\tpublic static unsafe class McutNative");
                file.WriteLine("\t{");
                foreach (var function in compilation.Functions)
                {
                    Helpers.PrintComments(file, function.Comment, "\t\t");
                    file.WriteLine($"\t\t[DllImport(\"mcut\", CallingConvention = CallingConvention.Cdecl)]");
                    var returnType = Helpers.ConvertToCSharpType(function.ReturnType);
                    file.Write($"\t\tpublic static extern {returnType} {function.Name}(");

                    if (function.Parameters.Count > 0)
                    {
                        for (int i = 0; i < function.Parameters.Count; i++)
                        {
                            if (i > 0)
                                file.Write(", ");

                            var parameter = function.Parameters[i];
                            var convertedType = Helpers.ConvertToCSharpType(parameter.Type);
                            file.Write($"{convertedType} {parameter.Name}");
                        }
                    }

                    file.Write(");\n\n");
                }

                file.WriteLine("\t}");
                file.WriteLine("}");
            }
        }
    }
}
