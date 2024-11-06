using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GameEngine
{
    internal static class CompilerVariables
    {
        internal static string externFolderPath = "E:\\MODS";
    }

    internal class DynamicCompiler
    {
        internal static bool LoadExternalAssembly()
        {
            var csFiles = Directory.GetFiles(CompilerVariables.externFolderPath, "*.cs");

            // Lese die Inhalte der .cs-Dateien ein
            var syntaxTrees = csFiles.Select(file => CSharpSyntaxTree.ParseText(File.ReadAllText(file))).ToList();

            // Compiler-Einstellungen
            var references = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic)
                .Select(a => MetadataReference.CreateFromFile(a.Location))
                .ToList();

            var compilation = CSharpCompilation.Create(
                "DynamicAssembly",
                syntaxTrees,
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            // Kompiliere in einen Speicherstream
            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var diagnostic in result.Diagnostics)
                    {
                        Console.WriteLine(diagnostic.ToString());
                    }
                    Console.ResetColor();
                    return false;
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    var assembly = Assembly.Load(ms.ToArray());

                    bool hasModClass = false;
                    bool hasModName = false;

                    // Beispiel: Alle Typen der Assembly anzeigen
                    Console.ForegroundColor = ConsoleColor.Green;
                    foreach (var type in assembly.GetTypes())
                    {
                        Console.WriteLine("Externe Klasse geladen: " + type.FullName);

                        if (type.Name == "Mod")
                        {
                            hasModClass = true;

                            // Suche nach einem String-Feld mit einem bestimmten Wert, z.B. "ModName"
                            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                            {
                                if (field.FieldType == typeof(string))
                                {
                                    var fieldValue = field.GetValue(type.IsAbstract && field.IsStatic ? null : Activator.CreateInstance(type)) as string;

                                    if (!string.IsNullOrEmpty(fieldValue))
                                    {
                                        Console.WriteLine($"Gefundenes String-Feld: {field.Name} = {fieldValue}");
                                        hasModName = true;
                                    }
                                }
                            }
                        }
                    }

                    Console.ResetColor();
                    return hasModClass && hasModName;
                }
            }
        }
    }
}
