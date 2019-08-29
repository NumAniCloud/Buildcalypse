namespace BuildCalypse.CodeGen
{
    public class ShopBook
    {
        public ShopBook(ShopContent[] contents, int playerId, ShopTriggerObjectives triggers)
        {
            Contents = contents;
            PlayerId = playerId;
            Triggers = triggers;
        }

        public ShopContent[] Contents { get; }
        public int PlayerId { get; }
        public ShopTriggerObjectives Triggers { get; }

        public string GetGiveCommand()
        {
            var json = Write(Contents).GetJson();
            return $"give @p[scores={{PlayerId={PlayerId}}}] written_book{{pages={json}}} 1";
        }

        private TellrawBody Write(ShopContent[] contents)
        {
            var body = new TellrawBody();
            body.AddComponent().SetText("建材ショップ\\n");
            body.AddComponent().SetText("BPを消費して購入\\n");

            foreach (var item in contents)
            {
                var command = Triggers.Get(item.Id).TriggerCommand(1);
                body.AddComponent()
                    .SetText($"{item.Name}:{item.Bp}BP\\n")
                    .SetColor($"blue")
                    .SetItalic(true)
                    .SetCommand(command);
            }

            return body;
        }
    }
}