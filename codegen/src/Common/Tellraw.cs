using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildCalypse.CodeGen
{
    public class TellrawBody
    {
        public class Component
        {
            public string Text { get; set; } = null;
            public string Color { get; set; } = null;
            public string Command { get; set; } = null;
            public string Selector { get; set; } = null;
            public string Objective { get; set; } = null;
            public bool Italic { get; set; } = false;

            public Component SetText(string text)
            {
                Text = text;
                Selector = null;
                Objective = null;
                return this;
            }

            public Component SetColor(string color)
            {
                Color = color;
                return this;
            }

            public Component SetCommand(string command)
            {
                Command = command;
                return this;
            }

            public Component SetSelector(string selector)
            {
                Selector = selector;
                Text = null;
                Objective = null;
                return this;
            }

            public Component SetScore(string selector, string objective)
            {
                Selector = selector;
                Objective = objective;
                return this;
            }

            public Component SetItalic(bool value)
            {
                Italic = value;
                return this;
            }

            public string GetJson()
            {
                var elements = new List<string>();
                bool textOnly = false;
                if (Text != null)
                {
                    elements.Add($@"\""text\"":\""{Text}\""");
                    textOnly = true;
                }
                else if (Objective != null)
                {
                    elements.Add($@"\""score\"":{{\""name\"":\""{Selector}\"",\""objective\"":\""{Objective}\""}}");
                }
                else if (Selector != null)
                {
                    elements.Add($@"\""selector\"":\""{Selector}\""");
                }

                if (Color != null)
                {
                    elements.Add($@"\""color\"":\""{Color}\""");
                    textOnly = false;
                }
                if (Command != null)
                {
                    elements.Add($@"\""clickEvent\"":{{\""action\"":\""run_command\"",\""value\"":\""{Command}\""}}");
                    textOnly = false;
                }
                if (Italic)
                {
                    elements.Add($@"\""italic\"":{(Italic ? "true" : "false")}");
                }

                if (textOnly)
                {
                    return $@"\""{Text}\""";
                }
                else
                {
                    return $"{{{string.Join(",", elements)}}}";
                }
            }
        }

        private readonly List<Component> components = new List<Component>();

        public Component AddComponent()
        {
            var component = new Component();
            components.Add(component);
            return component;
        }

        public string GetJson()
        {
            return "[" + string.Join(",", components.Select(x => x.GetJson())) + "]";
        }
    }
}