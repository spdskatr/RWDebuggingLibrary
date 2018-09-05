using Mono.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Verse;

namespace SpdsDebuggingLibrary
{
    public class DebugConsoleInteractive : InteractiveBase
    {
        public static new string help
        {
            get
            {
                return @"<b>Static methods:</b>
  Describe (object)       - Describes the object's type
  LoadPackage (package);  - Loads the given Package (like -pkg:FILE)
  LoadAssembly (assembly) - Loads the given assembly (like -r:ASSEMBLY)
  ShowVars ();            - Shows defined local variables.
  ShowUsing ();           - Show active using declarations.
  Time(() -> { })         - Times the specified code
  print (obj)             - Prints the string representation of the object
  help;                   - This help text
  cls                     - Clears the console
  ThingById (string)      - Gets the instance of a Thing by its unique load ID.
  PawnByNick (string)     - Gets the instance of a Pawn by its nickname";
            }
        }
        public static object cls
        {
            get
            {
                DebugConsole.stringWriter = new StringWriter();
                return null;
            }
        }
        public static Thing ThingById(string id)
        {
            return Find.CurrentMap.listerThings.AllThings.Find(t => t.GetUniqueLoadID() == "Thing_" + id);
        }
        public static Pawn PawnByNick(string nick)
        {
            return Find.CurrentMap.mapPawns.AllPawnsSpawned.First(p => p.Name is NameTriple nt && nt.Nick == nick);
        }
    }
}
