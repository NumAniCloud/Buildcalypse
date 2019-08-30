namespace BuildCalypse.CodeGen
{
    public class ShopContent
    {
        public int PlayerId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Bp { get; set; }
        public string SpawnEggId { get; set; }
        public TriggerObjective Trigger { get; set; }

        public string GetFunctionBody()
        {
            var playerSelector = $"@p[scores={{PlayerId={PlayerId},Bp={Bp}..}}]";
            var playerIdSelector = $"@p[scores={{PlayerId={PlayerId}}}]";
            var entityTag = $"{{EntityTag={{Tag=[\"{Id}\",\"Player{PlayerId}\"]}}}}";
            var give = $"give {playerSelector} {SpawnEggId}{entityTag} 1";
            var pay = $"scoreboard players remove {playerSelector} BP {Bp}";
            var triggerReset = $"scoreboard players set {playerIdSelector} {Trigger.TriggerName} 0";
            var triggerEnable = $"scoreboard players enable {playerIdSelector} {Trigger.TriggerName}";
            return $"{give}\n{pay}\n{triggerReset}\n{triggerEnable}";
        }

        public string GetCommandBlockCommand()
        {
            return $"function bldclps:placers/{Id}";
        }
    }
}