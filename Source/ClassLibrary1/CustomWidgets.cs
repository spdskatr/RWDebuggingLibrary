using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace SpdsDebuggingLibrary
{
    public static class CustomWidgets
    {
        public static void LabelScrollableMonospace(Rect rect, string label, ref Vector2 scrollbarPosition, bool dontConsumeScrollEventsIfNoScrollbar = false, bool takeScrollbarSpaceEvenIfNoScrollbar = true)
        {
            bool flag = takeScrollbarSpaceEvenIfNoScrollbar || Text.CalcHeight(label, rect.width) > rect.height;
            bool flag2 = flag && (!dontConsumeScrollEventsIfNoScrollbar || Text.CalcHeight(label, rect.width - 16f) > rect.height);
            float num = rect.width;
            if (flag)
            {
                num -= 16f;
            }
            Rect rect2 = new Rect(0f, 0f, num, Mathf.Max(Text.CalcHeight(label, num) + 5f, rect.height));
            if (flag2)
            {
                Widgets.BeginScrollView(rect, ref scrollbarPosition, rect2, true);
            }
            else
            {
                GUI.BeginGroup(rect);
            }
            // Begin Code Insertion
            var style = new GUIStyle(Text.CurFontStyle) //Construct our GUIStyle
            {
                alignment = TextAnchor.UpperLeft,
                wordWrap = true
            };
            if (FontResources.Consolas) // Check for exist
            {
                style.font = FontResources.Consolas;
            }
            GUI.Label(rect2, label, style);
            // Widgets.Label(rect2, label);
            // End
            if (flag2)
            {
                Widgets.EndScrollView();
            }
            else
            {
                GUI.EndGroup();
            }
        }
    }
}
