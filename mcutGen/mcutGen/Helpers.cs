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
