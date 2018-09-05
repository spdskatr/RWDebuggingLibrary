using Mono.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace SpdsDebuggingLibrary
{
    public static class DebugConsole
    {
        public const string StartUpCommand = @"using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Verse;
using Verse.AI;";
        public static Evaluator evaluator;
        public static StringWriter stringWriter = new StringWriter();
        public static List<string> history = new List<string>();

        private static IEnumerable<Assembly> AllActiveAssemblies
        {
            get
            {
                yield return Assembly.GetExecutingAssembly();
                foreach (ModContentPack mod in LoadedModManager.RunningMods)
                {
                    for (int i = 0; i < mod.assemblies.loadedAssemblies.Count; i++)
                    {
                        yield return mod.assemblies.loadedAssemblies[i];
                    }
                }
                yield break;
            }
        }

        static DebugConsole()
        {
            LongEventHandler.QueueLongEvent(StartConsole, "StartingDebugConsole", false, e => Log.Error("Exception starting debug console: " + e.ToString()));
        }

        static void StartConsole()
        {
            List<string> list = new List<string>(from a in AllActiveAssemblies select a.FullName);
            list.Add("UnityEngine");
            CompilerSettings settings = new CompilerSettings()
            {
                AssemblyReferences = list
            };
            evaluator = new Evaluator(new CompilerContext(settings, new StreamReportPrinter(stringWriter)))
            {
                InteractiveBaseClass = typeof(DebugConsoleInteractive)
            };

            stringWriter.WriteLine("Spd's Debugging Library - C# 6.0 REPL (Mono.CSharp)\nType 'help' for help.\nTo output anything here, use the in-built 'print' method.");
            InteractiveBase.Output = stringWriter;
            foreach (string str in StartUpCommand.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                Evaluate(str);
            }
        }

        public static void Evaluate(string input)
        {
            if (history.Count == 0 || history[history.Count - 1] != input)
            {
                history.Add(input);
            }
            stringWriter.WriteLine("> " + input);
            try
            {
                evaluator.Evaluate(input, out object result, out bool result_set);
                if (result_set)
                {
                    stringWriter.WriteLine(result ?? "(null)");
                }
            }
            catch (Exception e)
            {
                stringWriter.WriteLine(e);
            }
        }
    }
}
