using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using UnityEngine;

namespace SpdsDebuggingLibrary
{
    [StaticConstructorOnStartup]
    public static class FontResources
    {
        public static Font Consolas = Font.CreateDynamicFontFromOSFont("Consolas", 12);
    }
}
