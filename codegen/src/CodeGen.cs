using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace BuildCalypse.CodeGen
{
    public class CodeGen
    {
        public CodeGenResult Generate(Structure[] structures, Vector3 origin)
        {
            var placers = GetCommandBlockPlacers(structures, origin);
            var circuits = GetCircuitPlacers(structures, origin);
            var triggers = GetTriggerObjectives(structures);
            var shopContents = GetShopContents(structures);
            var shopBooks = GetShopBook(structures, shopContents, triggers);

            return new CodeGenResult
            {
                Placers = placers,
                PlacerCircuitBuilders = circuits,
                ShopContents = shopContents,
                ShopBooks = shopBooks
            };
        }

        private static Dictionary<string, string[]> GetCircuitPlacers(Structure[] structures, Vector3 origin)
        {
            var placerFactory = new PlacerCircuitFactory();
            var placers = new Dictionary<string, string[]>();
            var xCount = (int)Math.Sqrt(structures.Length);
            for (int i = 0; i < structures.Length; i++)
            {
                var x = i % xCount;
                var y = i / xCount / xCount;
                var z = i / xCount % xCount;
                var placerOrigin = origin + new Vector3(x, y, z);
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
                var book = shopBookFactory.GetShopBook(shopContents[i], i + 1, triggers);
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

        private static Dictionary<string, CommandBlock[]> GetCommandBlockPlacers(Structure[] structures, Vector3 origin)
        {
            var placerFactory = new CommandBlockFactory();
            var placers = new Dictionary<string, CommandBlock[]>();
            var xCount = (int)Math.Sqrt(structures.Length);
            for (int i = 0; i < structures.Length; i++)
            {
                var x = i % xCount;
                var y = i / xCount / xCount;
                var z = i / xCount % xCount;
                var placerOrigin = origin + new Vector3(x, y, z);
                var placer = placerFactory.GetCommands(structures[i], placerOrigin).ToArray();
                placers[structures[i].Id] = placer;
            }
            return placers;
        }
    }
}