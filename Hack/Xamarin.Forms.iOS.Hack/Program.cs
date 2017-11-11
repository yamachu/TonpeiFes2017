using System;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace Xamarin.Forms.iOS.Hack
{
    class Program
    {
        // https://github.com/xamarin/Xamarin.Forms/pull/1244
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                System.Console.WriteLine("app Xamarin.Forms.iOS-InputDir Xamarin.iOS.framework-lib-Dir Xamarin.Forms.iOS-OutputDir");
                return;
            }

            var originalAssembly = System.IO.Path.Combine(args[0], "Xamarin.Forms.Platform.iOS.dll");

            System.Console.WriteLine($"Backup {originalAssembly} -> {originalAssembly}.bak");
            System.IO.File.Copy(originalAssembly, $"{originalAssembly}.bak");
            
            var assemblyResolver = new DefaultAssemblyResolver();
            assemblyResolver.AddSearchDirectory(args[0]);
            assemblyResolver.AddSearchDirectory(args[1]);

            var asm = AssemblyDefinition.ReadAssembly(originalAssembly, new ReaderParameters{ AssemblyResolver = assemblyResolver });

            var targetType = asm.MainModule.GetType("Xamarin.Forms.Platform.iOS.ListViewRenderer");
            var targetMethod = targetType.Methods.First(x => x.Name == "UpdateItems");

            targetMethod.Body.SimplifyMacros();

            var proc = targetMethod.Body.GetILProcessor();

            proc.InsertBefore(targetMethod.Body.Instructions[20], Instruction.Create(OpCodes.Ldloc_1));
            var jumpTarget = targetMethod.Body.Instructions[51];

            proc.InsertBefore(targetMethod.Body.Instructions[21], Instruction.Create(OpCodes.Brtrue, jumpTarget));

            targetMethod.Body.OptimizeMacros();

            var writeFilePath = System.IO.Path.Combine(args[2], "Xamarin.Forms.Platform.iOS.dll");

            System.Console.WriteLine($"OutputFile: {writeFilePath}");

            asm.Write(writeFilePath);
        }
    }
}
