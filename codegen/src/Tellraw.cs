using System.Collections.Generic;
using System.Text;

namespace BuildCalypse.CodeGen
{
    public class Tellraw
    {
        public class Component
        {
            public string Text { get; set; }
            public string Color { get; set; }
            public string Command { get; set; }

            public Component SetText(string text)
            {
                Text = text;
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

            public string GetJson()
            {
                var builder = new StringBuilder();
                builder.Append("{");
                if (Text != null)
                {
                    builder.Append($"\"text\":{Text}");
                }
                return builder.ToString();
            }
        }

        private readonly List<Component> components;

        public Component AddComponent()
        {
            var component = new Component();
            components.Add(component);
            return component;
        }

        public string GetJson()
        {

        }
    }
}