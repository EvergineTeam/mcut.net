using CppAst;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcutGen
{
    public static class Helpers
    {
        public static List<string> TypedefList;

        private static readonly Dictionary<string, string> s_csNameMappings = new Dictionary<string, string>()
        {
            { "uint8_t", "byte" },
            { "uint16_t", "ushort" },
            { "uint32_t", "uint" },
            { "uint64_t", "ulong" },
            { "int8_t", "sbyte" },
            { "int32_t", "int" },
            { "int16_t", "short" },
            { "int64_t", "long" },
            { "int64_t*", "long*" },
            { "char", "byte" },
            { "size_t", "UIntPtr" },
            { "DWORD", "uint" },
            { "McFlags", "uint"},
            { "McBool", "uint"}
        };

        public static string ConvertToCSharpType(CppType type, bool isPointer = false)
        {
            if (type is CppPrimitiveType primitiveType)
            {
                return GetCsTypeName(primitiveType, isPointer);
            }

            if (type is CppQualifiedType qualifiedType)
            {
                return GetCsTypeName(qualifiedType.ElementType, isPointer);
            }

            if (type is CppEnum enumType)
            {
                var enumCsName = GetCsCleanName(enumType.Name);
                if (isPointer)
                    return enumCsName + "*";

                return enumCsName;
            }

            if (type is CppTypedef typedef)
            {
                var typeDefCsName = GetCsCleanName(typedef.Name);
                if (isPointer)
                    return typeDefCsName + "*";

                return typeDefCsName;
            }

            if (type is CppClass @class)
            {
                var className = GetCsCleanName(@class.Name);
                if (isPointer)
                    return className + "*";

                return className;
            }

            if (type is CppPointerType pointerType)
            {
                return GetCsTypeName(pointerType);
            }

            if (type is CppArrayType arrayType)
            {
                return GetCsTypeName(arrayType.ElementType, isPointer);
            }

            return string.Empty;
        }

        private static string GetCsTypeName(CppPrimitiveType primitiveType, bool isPointer)
        {
            string result = string.Empty;

            switch (primitiveType.Kind)
            {
                case CppPrimitiveKind.Void:
                    result = "void";
                    break;
                case CppPrimitiveKind.Bool:
                    result = "bool";
                    break;
                case CppPrimitiveKind.Char:
                    result = "char";
                    break;
                case CppPrimitiveKind.Short:
                    result = "short";
                    break;
                case CppPrimitiveKind.Int:
                    result = "int";
                    break;
                case CppPrimitiveKind.UnsignedShort:
                    result = "ushort";
                    break;
                case CppPrimitiveKind.UnsignedInt:
                    result = "uint";
                    break;
                case CppPrimitiveKind.Float:
                    result = "float";
                    break;
                case CppPrimitiveKind.Double:
                    result = "double";
                    break;
                case CppPrimitiveKind.UnsignedChar:
                case CppPrimitiveKind.LongLong:
                case CppPrimitiveKind.UnsignedLongLong:
                case CppPrimitiveKind.LongDouble:
                case CppPrimitiveKind.WChar:
                default:
                    break;
            }

            if (isPointer)
            {
                result += "*";
            }

            return result;
        }

        private static string GetCsTypeName(CppPointerType pointerType)
        {
            if (pointerType.ElementType is CppQualifiedType qualifiedType)
            {
                if (qualifiedType.ElementType is CppPrimitiveType primitiveType)
                {
                    return GetCsTypeName(primitiveType, true);
                }
                else if (qualifiedType.ElementType is CppClass @classType)
                {
                    return GetCsTypeName(@classType, true);
                }
                else if (qualifiedType.ElementType is CppPointerType subPointerType)
                {
                    return GetCsTypeName(subPointerType, true) + "*";
                }
                else if (qualifiedType.ElementType is CppTypedef typedef)
                {
                    return GetCsTypeName(typedef, true);
                }
                else if (qualifiedType.ElementType is CppEnum @enum)
                {
                    return GetCsTypeName(@enum, true);
                }

                return GetCsTypeName(qualifiedType.ElementType, true);
            }

            return GetCsTypeName(pointerType.ElementType, true);
        }

        private static string GetCsTypeName(CppType type, bool isPointer = false)
        {
            if (type is CppPrimitiveType primitiveType)
            {
                return GetCsTypeName(primitiveType, isPointer);
            }

            if (type is CppQualifiedType qualifiedType)
            {
                return GetCsTypeName(qualifiedType.ElementType, isPointer);
            }

            if (type is CppEnum enumType)
            {
                var enumCsName = GetCsCleanName(enumType.Name);
                if (isPointer)
                    return enumCsName + "*";

                return enumCsName;
            }

            if (type is CppTypedef typedef)
            {
                var typeDefCsName = GetCsCleanName(typedef.Name);
                if (isPointer)
                    return typeDefCsName + "*";

                return typeDefCsName;
            }

            if (type is CppClass @class)
            {
                var className = GetCsCleanName(@class.Name);
                if (isPointer)
                    return className + "*";

                return className;
            }

            if (type is CppPointerType pointerType)
            {
                return GetCsTypeName(pointerType);
            }

            if (type is CppArrayType arrayType)
            {
                return GetCsTypeName(arrayType.ElementType, isPointer);
            }

            return string.Empty;
        }

        private static string GetCsCleanName(string name)
        {
            if (s_csNameMappings.TryGetValue(name, out string mappedName))
            {
                return GetCsCleanName(mappedName);
            }

            if (name.StartsWith("PFN"))
            {
                return "IntPtr";
            }

            if (name.Contains("Flags"))
            {
                return name.Remove(name.Count() - 5);
            }

            if (TypedefList.Contains(name))
            {
                return "IntPtr";
            }

            return name;
        }

        internal static void PrintComments(StreamWriter file, CppComment comment, string tabs = "", bool newLine = false)
        {
            if (comment != null)
            {
                if (newLine) file.WriteLine();

                file.WriteLine($"{tabs}/// <summary>");
                GetText(file, comment, tabs);
                file.WriteLine($"{tabs}/// </summary>");
            }
        }

        private static void GetText(StreamWriter file, CppComment comment, string tabs)
        {
            switch (comment.Kind)
            {
                case CppCommentKind.Text:
                    var commentText = comment as CppCommentTextBase;
                    if (!string.IsNullOrEmpty(commentText.Text))
                    {
                        file.WriteLine($"{tabs}/// {commentText.Text}");
                    }
                    break;
                case CppCommentKind.Paragraph:
                case CppCommentKind.Full:
                    if (comment.Children?.Count > 0)
                    {
                        foreach (var child in comment.Children)
                        {
                            GetText(file, child, tabs);
                        }
                    }
                    break;
                default:
                    ;
                    break;
            }
        }
    }
}
