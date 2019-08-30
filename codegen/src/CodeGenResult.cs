using System.Collections.Generic;

namespace BuildCalypse.CodeGen
{
    public class CodeGenResult
    {
        public Dictionary<string, CommandBlock[]> Placers { get; set; }
        public Dictionary<string, string[]> PlacerCircuitBuilders { get; set; }
        public Dictionary<int, ShopContent[]> ShopContents { get; set; }
        public Dictionary<int, ShopBook> ShopBooks { get; set; }
        public CommandBlock[] ShopTriggerBlocks { get; internal set; }
    }
}