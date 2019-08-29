using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using BuildCalypse.CodeGen;

namespace codegen
{
    class Program
    {
        static void Main(string[] args)
        {
            var structure = new Structure()
            {
                Id = "floor",
                FullName = "bldclps:floor",
                MobName = "pig",
                Offset = new Vector3(-1, 0, 0),
                RedstoneOffset = new Vector3(0, 1, 0),
                Bp = 5,
                Name = "床",
                SpawnEggId = "pig_spawn_egg"
            };
            var structures = new []
            {
                structure
            };

            var placerFactory = new CommandBlockFactory();
            var shopContentsFactory = new ShopBookContentsFactory();
            var shopBookFactory = new ShopBookFactory();

            var placer = placerFactory.GetCommands(structure, new Vector3(10, 5, 10));

            var triggers = new ShopTriggerObjectives();
            foreach (var item in structures)
            {
                triggers.AddTriggerFor(item);
            }

            var shopContentsOfPlayer = new Dictionary<int, ShopContent[]>();
            var shopBookOfPlayer = new Dictionary<int, ShopBook>();
            for (int i = 0; i < 8; i++)
            {
                var contents = shopContentsFactory.GetContents(structures, i + 1).ToArray();
                var book = shopBookFactory.GetShopBook(contents, i + 1, triggers);
                shopContentsOfPlayer[i+1] = contents;
                shopBookOfPlayer[i+1] = book;
            }

            // テスト用出力
            foreach (var item in placer)
            {
                System.Console.WriteLine(item.GetCommandSetBlock());
            }
            foreach (var item in triggers)
            {
                System.Console.WriteLine(item.GetInitializeCommand());
                System.Console.WriteLine(item.GetEnableCommand("@p"));
                System.Console.WriteLine(item.TriggerCommand(1));
            }
            for (int i = 0; i < 8; i++)
            {
                foreach (var item in shopContentsOfPlayer[i+1])
                {
                    System.Console.WriteLine(item.GetFunctionBody());
                }
                System.Console.WriteLine(shopBookOfPlayer[i+1].GetGiveCommand());
            }
        }
    }
}
