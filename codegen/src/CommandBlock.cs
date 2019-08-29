using System.Numerics;

namespace BuildCalypse.CodeGen
{
    public class CommandBlock
    {
        public string Command { get; internal set; }
        public Vector3 Position { get; internal set; }
        public Direction Facing { get; internal set; }
        public bool Conditional { get; internal set; }
        public bool NeedsRedstone { get; internal set; }
        public CommandBlockMode Mode { get; internal set; }

        public string GetCommandSetBlock()
        {
            var position = $"{Position.X} {Position.Y} {Position.Z}";
            var facing = $"facing={Facing.GetFacingString()}";
            var conditional = "conditional=" + (Conditional ? "true" : "false");
            var command = $"Command=\"{Command}\"";
            return $"setblock {ToString(Mode)}[{facing},{conditional}]{{{command}}} {position}";
        }

        private string ToString(CommandBlockMode mode)
        {
            return mode switch
            {
                CommandBlockMode.Impulse => "command_block",
                CommandBlockMode.Repeat => "repeating_command_block",
                CommandBlockMode.Chain => "chain_command_block",
                _ => "command_block"
            };
        }
    }
}