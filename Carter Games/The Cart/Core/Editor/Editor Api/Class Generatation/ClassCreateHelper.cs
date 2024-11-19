using System.IO;
using UnityEditor.Compilation;

namespace CarterGames.Cart.Core.Editor
{
	public static class ClassCreateHelper
	{
		public static void ApplyHeader(TextWriter writer, string ifDef, string nameSpace, string className)
		{
			writer.WriteLine($"#if {ifDef}");
			writer.WriteLine("");
			writer.WriteLine($"namespace {nameSpace}");
			writer.WriteLine("{");
			writer.WriteLine($"    public struct {className}");
			writer.WriteLine("    {");
		}


		public static void ApplyLine(TextWriter writer, string fieldName, string fieldValue)
		{
			if (string.IsNullOrEmpty(fieldValue)) return;
			writer.WriteLine($"        public const string {fieldName} = \"{fieldValue}\";");
		}
        

		public static void ApplyFooter(TextWriter writer)
		{
			writer.WriteLine("    }");
			writer.WriteLine("}");
			writer.WriteLine("");
			writer.WriteLine("#endif");
			writer.Close();

			CompilationPipeline.RequestScriptCompilation();
		}
	}
}