namespace BuildCalypse.CodeGen
{
    public class ShopContent
    {
        public int PlayerId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Bp { get; set; }
        public string SpawnEggId { get; set; }
        public int Index { get; internal set; }

        public string GetFunctionBody()
        {
            var playerSelector = $"@p[scores={{PlayerId={PlayerId},Bp={Bp}..}}]";
            var entityTag = $"{{EntityTag={{Tag=[\"{Id}\",\"Player{PlayerId}\"]}}}}";
            var give = $"give {playerSelector} {SpawnEggId}{entityTag} 1";
            var pay = $"scoreboard players remove {playerSelector} BP {Bp}";
            return $"{give}\n{pay}";
        }
    }
}