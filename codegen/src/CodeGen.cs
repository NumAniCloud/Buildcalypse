using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace BuildCalypse.CodeGen
{
    public class CodeGen
    {
        private int placerClusterXCount = 3;

        public CodeGenResult Generate(Structure[] structures, Vector3 origin)
        {
            placerClusterXCount = Math.Min(3, (int)Math.Sqrt(structures.Length));
            var placers = GetCommandBlockPlacers(structures, origin);
            var circuits = GetCircuitPlacers(structures, origin);
            var triggers = GetTriggerObjectives(structures);
            var shopContents = GetShopContents(structures);
            var shopBooks = GetShopBook(structures, shopContents, triggers);
            var shopTriggerBlocks = GetShopTriggerBlocks(shopContents[1], origin);

            return new CodeGenResult
            {
                Placers = placers,
                PlacerCircuitBuilders = circuits,
                ShopContents = shopContents,
                ShopBooks = shopBooks,
                ShopTriggerBlocks = shopTriggerBlocks,
            };
        }

        private CommandBlock[] GetShopTriggerBlocks(ShopContent[] contents, Vector3 origin)
        {
            IEnumerable<CommandBlock> Select(ShopContent[] contents)
            {
                var shopOrigin = origin + new Vector3(-8, 0, 0);
                for (int i = 0; i < contents.Length; i++)
                {
                    yield return new CommandBlock()
                    {
                        Command = contents[i].GetCommandBlockCommand(),
                        Conditional = false,
                        Facing = Direction.East,
                        Mode = CommandBlockMode.Repeat,
                        NeedsRedstone = false,
                        Position = shopOrigin + new Vector3(0, 0, i),
                    };
                }
            }
            return Select(contents).ToArray();
        }

        private Vector3 GetClusterPosition(int index)
        {
            var x = index % placerClusterXCount;
            var y = index / placerClusterXCount / placerClusterXCount;
            var z = index / placerClusterXCount % placerClusterXCount;
            return new Vector3(x * 10, y * 6, z * 8);
        }

        private Dictionary<string, string[]> GetCircuitPlacers(Structure[] structures, Vector3 origin)
        {
            var placerFactory = new PlacerCircuitFactory();
            var placers = new Dictionary<string, string[]>();
            for (int i = 0; i < structures.Length; i++)
            {
                var placerOrigin = origin + GetClusterPosition(i);
                var placer = placerFactory.GetCommandsToPlace(placerOrigin).ToArray();
                placers[structures[i].Id] = placer;
            }
            return placers;
        }

        private static Dictionary<int, ShopBook> GetShopBook(Structure[] structures,
            Dictionary<int, ShopContent[]> shopContents,
            ShopTriggerObjectives triggers)
        {
            var shopBookFactory = new ShopBookFactory();
            var shopBookOfPlayer = new Dictionary<int, ShopBook>();
            for (int i = 0; i < 8; i++)
            {
                var book = shopBookFactory.GetShopBook(shopContents[i+1], i + 1, triggers);
                shopBookOfPlayer[i + 1] = book;
            }
            return shopBookOfPlayer;
        }

        private static Dictionary<int, ShopContent[]> GetShopContents(Structure[] structures)
        {
            var shopContentsFactory = new ShopBookContentsFactory();
            var shopContentsOfPlayer = new Dictionary<int, ShopContent[]>();
            for (int i = 0; i < 8; i++)
            {
                var contents = shopContentsFactory.GetContents(structures, i + 1).ToArray();
                shopContentsOfPlayer[i + 1] = contents;
            }
            return shopContentsOfPlayer;
        }

        private static ShopTriggerObjectives GetTriggerObjectives(Structure[] structures)
        {
            var triggers = new ShopTriggerObjectives();
            foreach (var item in structures)
            {
                triggers.AddTriggerFor(item);
            }

            return triggers;
        }

        private Dictionary<string, CommandBlock[]> GetCommandBlockPlacers(Structure[] structures, Vector3 origin)
        {
            var placerFactory = new CommandBlockFactory();
            var placers = new Dictionary<string, CommandBlock[]>();
            for (int i = 0; i < structures.Length; i++)
            {
                var placerOrigin = origin + GetClusterPosition(i);
                var placer = placerFactory.GetCommands(structures[i], placerOrigin).ToArray();
                placers[structures[i].Id] = placer;
            }
            return placers;
        }
    }
}