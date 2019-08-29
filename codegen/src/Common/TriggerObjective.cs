namespace BuildCalypse.CodeGen
{
    public class TriggerObjective
    {
        public TriggerObjective(string triggerName)
        {
            TriggerName = triggerName;
        }

        public string TriggerName { get; }

        public string GetInitializeCommand()
        {
            return $"scoreboard objectives add {TriggerName} trigger";
        }

        public string GetEnableCommand(string selector)
        {
            return $"scoreboard player enable {selector} {TriggerName}";
        }

        public string TriggerCommand(int value)
        {
            return $"trigger {TriggerName} set {value}";
        }
    }
}