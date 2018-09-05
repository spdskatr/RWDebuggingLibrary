using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace SpdsDebuggingLibrary
{
    public class MainTabWindow_Console : MainTabWindow
    {
        Vector2 scrollPos;
        string input;
        int index = -1;

        public MainTabWindow_Console() : base()
        {
            closeOnAccept = false;
        }

        public override Vector2 RequestedTabSize
        {
            get
            {
                return new Vector2(550f, 640f);
            }
        }

        public override void DoWindowContents(Rect inRect)
        {
            // Capture keycode and eventtype before it gets consumed
            KeyCode kc = Event.current.keyCode;
            EventType et = Event.current.type;

            base.DoWindowContents(inRect);
            Rect rect = inRect.ContractedBy(10f);
            Text.Font = GameFont.Small;
            Rect textRect = inRect;
            textRect.height -= 25f;
            Rect inputRect = new Rect(rect.x, rect.y + rect.height - 25f, rect.width - 100f, 25f);
            Rect buttonRect = new Rect(rect.x + rect.width - 100f, rect.y + rect.height - 25f, 100f, 25f);
            
            CustomWidgets.LabelScrollableMonospace(textRect, DebugConsole.stringWriter.ToString(), ref scrollPos);
            input = Widgets.TextArea(inputRect, input);

            if (et == EventType.keyDown && (kc == KeyCode.UpArrow || kc == KeyCode.DownArrow))
            {
                if (index == -1)
                {
                    index = DebugConsole.history.Count;
                }
                index = Mathf.Clamp((kc == KeyCode.UpArrow) ? index - 1 : index + 1, 0, DebugConsole.history.Count);
                if (index == DebugConsole.history.Count)
                {
                    input = "";
                }
                else
                {
                    input = DebugConsole.history[index];
                }
            }

            if (Widgets.ButtonText(buttonRect, "Evaluate") || input.EndsWith("\n") || input.StartsWith("\r") || input.StartsWith("\n"))
            {
                string trimmed = input.TrimEnd('\r', '\n').TrimStart('\r', '\n');
                if (!string.IsNullOrEmpty(trimmed))
                {
                    DebugConsole.Evaluate(trimmed);
                }
                input = "";
            }
        }
    }
}
